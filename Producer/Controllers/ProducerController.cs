using Confluent.Kafka;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Producer : ControllerBase
    {
        [HttpPost("PublishMessage")]
        public async Task<IActionResult> PublishMessage(string message)
        {
            var config = new ProducerConfig { BootstrapServers = "192.168.1.20:9092" };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                await producer.ProduceAsync("new-material", new Message<Null, string> { Value = message });
                producer.Flush();
            }

            return Ok();
        }
    }
}
