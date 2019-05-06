using System.Web.Mvc;

namespace FirstApp.Filters
{
    public class OperaDetected : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Write("Действие выполнено");
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Browser.Browser == "Opera")
            {
                filterContext.Result = new HttpNotFoundResult();
            }
        }
    }
}