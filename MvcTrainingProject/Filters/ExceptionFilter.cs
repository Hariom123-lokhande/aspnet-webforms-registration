using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MvcTrainingProject.Filters
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null || filterContext.ExceptionHandled)
                return;

            if (filterContext.IsChildAction)
                return;

            Exception ex = filterContext.Exception;

            try
            {
                // Create structured properties and a sanitized stack trace (remove file paths)
                string controllerName =
                    filterContext.RouteData.Values["controller"]?.ToString() ?? "Unknown";

                string actionName =
                    filterContext.RouteData.Values["action"]?.ToString() ?? "Unknown";

                string sanitizedStack = string.Empty;
                if (!string.IsNullOrEmpty(ex.StackTrace))
                {
                    // Remove patterns like " in C:\...:line 123" to avoid leaking file paths
                    sanitizedStack = Regex.Replace(ex.StackTrace, @" in .*?:line \d+", "");

                    // Remove any remaining absolute Windows paths
                    sanitizedStack = Regex.Replace(sanitizedStack, @"[A-Za-z]:\\\\[^\r\n]*", "");

                    // Normalize and trim lines
                    var lines = sanitizedStack.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    var sb = new StringBuilder();
                    foreach (var line in lines)
                    {
                        sb.AppendLine(line.Trim());
                    }
                    sanitizedStack = sb.ToString();

                    if (sanitizedStack.Length > 2000)
                        sanitizedStack = sanitizedStack.Substring(0, 2000);
                }

                // Structured logging: include properties instead of logging the exception object
                Log.ForContext("Controller", controllerName)
                   .ForContext("Action", actionName)
                   .ForContext("ExceptionType", ex.GetType().FullName)
                   .ForContext("StackTrace", sanitizedStack)
                   .Error("Unhandled exception occurred" + Environment.NewLine + "Message: {ErrorMessage}" + Environment.NewLine, ex.Message);

                // ✅ Database Logging (reuse sanitizedStack)
                string connectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]?.ConnectionString;

                if (!string.IsNullOrEmpty(connectionString))
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = @"INSERT INTO ExceptionLogs
                                         (ErrorMessage, StackTrace, ControllerName, ActionName, CreatedDate)
                                         VALUES
                                         (@Message, @StackTrace, @Controller, @Action, GETDATE())";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@Message",
                                ex?.Message?.Length > 500
                                    ? ex.Message.Substring(0, 500)
                                    : ex?.Message ?? "No Message");

                            cmd.Parameters.AddWithValue("@StackTrace",
                                sanitizedStack ?? string.Empty);

                            cmd.Parameters.AddWithValue("@Controller", controllerName);
                            cmd.Parameters.AddWithValue("@Action", actionName);

                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception loggingEx)
            {
                // Never crash application due to logging failure
                Log.Warning(loggingEx, "Logging failed");
                // swallow the exception to avoid affecting the main flow
            }

            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            filterContext.Result = new ViewResult
            {
                ViewName = "Error"
            };

            filterContext.ExceptionHandled = true;
        }
    }
}