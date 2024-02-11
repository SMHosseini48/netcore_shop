using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ncorep.Filters;

public class MyFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Do something before the action executes.
        Console.WriteLine("action executed ");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Do something after the action executes.
        Console.WriteLine("action executed2 ");
    }
}