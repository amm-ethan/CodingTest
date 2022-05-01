using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Presentation.ActionFilters
{
    public class ObjectNullAttribute : IActionFilter
    {
        public ObjectNullAttribute()
        { }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];
            var param = context.ActionArguments.SingleOrDefault().Value as string;
            if (string.IsNullOrEmpty(param) || string.IsNullOrWhiteSpace(param))
            {
                context.Result = new BadRequestObjectResult($"Value is null or empty or whitespace. Controller: { controller }, action: { action}");
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}