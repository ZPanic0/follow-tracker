using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Web
{
    public class ExceptionLoggingFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine($"[{DateTime.Now}] Exception thrown: '{context.Exception.Message}'");
            Console.WriteLine(context.Exception.ToString());
        }
    }
}
