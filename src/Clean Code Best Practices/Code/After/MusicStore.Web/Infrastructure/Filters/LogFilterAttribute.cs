namespace MusicStore.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.DependencyInjection;

    public class LogFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService<ILogger>();

            logger.LogInformation($"Action started: {context.RouteData.Values["action"]}");

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService<ILogger>();

            logger.LogInformation($"Action finished - {context.RouteData.Values["action"]}");

            base.OnActionExecuted(context);
        }
    }
}
