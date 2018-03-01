using MassTransit;
using Messages.DataObjects;
using Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitConsumerDemoHost.Consumers
{
    class UnProcessedFileRowReceivedConsumer : IConsumer<IUnProcessedFileRowReceived>
    {
        public async Task Consume(ConsumeContext<IUnProcessedFileRowReceived> context)
        {
            var msg = context.Message;
            var collection = msg.RowData.Split(',');

            if (collection.Length > 2)
            {
                await context.Publish<IProcessedFileRowCreated>(new ProcessedFileRowCreated(msg.ProjectId,
                    new ProcessedDataRow()
                    {
                        Name = collection[0],
                        Description = collection[1],
                        Price = decimal.Parse(collection[2])
                    }));
                } else
            {
                throw new Exception("Bad Row");
            }
        }
    }
}
