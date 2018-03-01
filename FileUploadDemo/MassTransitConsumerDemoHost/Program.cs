using GreenPipes;
using MassTransit;
using MassTransitConsumerDemoHost.Consumers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitConsumerDemoHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
              {
                  var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                  {
                      h.Username("guest");
                      h.Password("guest");
                  });
                 
                  cfg.ReceiveEndpoint(host, "unprocessed_file_row_received_consumer", e =>
                  {
                      e.Consumer<UnProcessedFileRowReceivedConsumer>();
                      /*e.Consumer<UnProcessedFileRowReceivedConsumer>(consumerConfig=> {
                          consumerConfig.UseRetry(r => {
                              r.Immediate(10);
                              });
                          consumerConfig.UseRateLimit(10, new TimeSpan(0, 1, 0));
                          
                      });*/
                  });

                  cfg.ReceiveEndpoint(host, "critical_validation_consumer", e =>
                  {
                      e.Consumer<CriticalValidationConsumer>();
                  });

                  cfg.ReceiveEndpoint(host, "fast_validation_consumer", e =>
                  {
                      e.Consumer<FastValidationConsumer>();
                  });

                  cfg.ReceiveEndpoint(host, "project_rows_published_consumer", e =>
                  {
                      e.Consumer<ProjectRowsPublishedConsumer>();
                  });

                  cfg.ReceiveEndpoint(host, "slow_validation_consumer", e =>
                  {
                      
                      e.Consumer<SlowValidationConsumer>();
                  });

                  cfg.ReceiveEndpoint(host, "validation_passed_consumer", e =>
                  {
                      e.Consumer<ValidationPassedConsumer>();
                  });
              });
            bus.Start();

            var text = "";
            while (text != "quit")
            {
                Console.Write("Enter a message: ");
                text = Console.ReadLine();
            }

            bus.Stop();
        }
    }
}
