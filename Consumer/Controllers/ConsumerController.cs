using Confluent.Kafka;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Consumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        [HttpPost("ConsumeMessage")]
        public IActionResult ConsumeMessage()
        {
            var config = new ConsumerConfig 
            { 
                BootstrapServers = "192.168.1.20:9092",

                GroupId = "my-consumer-group", // Unique group ID
                AutoOffsetReset = AutoOffsetReset.Latest // Start consuming from the beginning

            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe("new-material");

                while (true)
                {
                    var consumeResult = consumer.Consume();
                    Console.WriteLine($"Received message: {consumeResult.Message.Value}");
                }
            }

        }
    }
}
