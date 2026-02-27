using System.Web;
using System.Web.Mvc;
using MvcTrainingProject.Filters;
namespace MvcTrainingProject
{
    public class FilterConfig
{
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
        filters.Clear();   // IMPORTANT
        filters.Add(new ExceptionFilter());
    }
}
}