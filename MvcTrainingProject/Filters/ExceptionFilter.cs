using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace MvcTrainingProject.Filters
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            try
            {
                var ex = filterContext.Exception;

                string connectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string cleanStack = "";

                    if (!string.IsNullOrEmpty(ex.StackTrace))
                    {
                        var lines = ex.StackTrace.Split(new[] { Environment.NewLine },
                                                         StringSplitOptions.RemoveEmptyEntries);

                        StringBuilder sb = new StringBuilder();

                        foreach (var line in lines)
                        {
                            if (line.Contains("MvcTrainingProject"))
                            {
                                // Remove " in C:\...."
                                int inIndex = line.IndexOf(" in ");
                                if (inIndex > 0)
                                {
                                    sb.AppendLine(line.Substring(0, inIndex));
                                }
                                else
                                {
                                    sb.AppendLine(line);
                                }
                            }
                        }

                        cleanStack = sb.ToString();
                    }

                    string query = @"INSERT INTO ExceptionLogs
                                    (ErrorMessage, StackTrace, ControllerName, ActionName)
                                    VALUES
                                    (@Message, @StackTrace, @Controller, @Action)";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Message", ex.Message);
                    cmd.Parameters.AddWithValue("@StackTrace", cleanStack);
                    cmd.Parameters.AddWithValue("@Controller",
                        filterContext.RouteData.Values["controller"].ToString());
                    cmd.Parameters.AddWithValue("@Action",
                        filterContext.RouteData.Values["action"].ToString());

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
            }

            filterContext.Result = new ContentResult()
            {
                Content = "Something went wrong. Please contact admin."
            };

            filterContext.ExceptionHandled = true;
        }
    }
}