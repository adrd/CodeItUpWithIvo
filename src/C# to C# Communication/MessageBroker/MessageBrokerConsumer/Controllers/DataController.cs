namespace MessageBrokerConsumer.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;

    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IData data;

        public DataController(IData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IActionResult Get() => this.Ok(this.data.Get());
    }
}
