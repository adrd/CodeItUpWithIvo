namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using BlogSystem.Data.Contracts;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class BaseController : Controller
    {
        private IRepository<Setting> settings;

        public IRepository<Setting> Settings
        {
            get
            {
                if (this.settings == null)
                {
                    this.settings = this
                        .HttpContext
                        .RequestServices
                        .GetService<IRepository<Setting>>();
                }

                return this.settings;
            }
        }

        protected ActionResult RedirectToAction<TController>(Expression<Action<TController>> action)
            where TController : Controller
        {
            var method = action.Body as MethodCallExpression;
            if (method == null)
            {
                throw new ArgumentException("Expected method call");
            }

            return this.RedirectToAction(method.Method.Name);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.ViewBag.Settings = new SettingsManager(this.GetSettings);
        }

        protected IDictionary<string, string> GetSettings()
        {
            return this.Settings.All().ToDictionary(x => x.Name, x => x.Value);
        }
    }
}