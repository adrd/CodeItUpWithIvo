namespace Blog.Tests.Infrastructure
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExtensions
    {
        public static TController WithTestUser<TController>(
            this TController controller,
            string userName = TestConstants.TestUsername)
            where TController : Controller
        {
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userName)
                    }))
                }
            };

            return controller;
        }
    }
}
