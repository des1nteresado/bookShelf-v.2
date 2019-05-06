using System;
using System.Web.Mvc;

namespace FirstApp.Filters
{
    public class IndexException : FilterAttribute, IExceptionFilter
    {

        public void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext.ExceptionHandled && exceptionContext.Exception is IndexOutOfRangeException)
            {
                exceptionContext.Result = new RedirectResult("/Content/Welcome.html");
                exceptionContext.ExceptionHandled = true;
            }
        }
    }
}