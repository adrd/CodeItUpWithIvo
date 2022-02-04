namespace Blog.Test.Extensions
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public static class ControllerTestExtensions
    {
        public static TController WithTestUser<TController>(this TController controller)
            where TController : Controller
        {
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, TestConstants.TestUsername)
                    }))
                }
            };

            return controller;
        }
    }
}
