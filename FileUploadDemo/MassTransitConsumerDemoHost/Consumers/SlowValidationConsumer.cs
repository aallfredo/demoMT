using MassTransit;
using MassTransitConsumerDemoHost.FakeDataStorage;
using Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MassTransitConsumerDemoHost.Consumers
{
    public class SlowValidationConsumer: IConsumer<IProcessedFileRowCreated>
    {
        static Random r = new Random();
        public Task Consume(ConsumeContext<IProcessedFileRowCreated> context)
        {
            //Making this slow
            Thread.Sleep(1000);
            var msg = context.Message;
            var number = r.Next(10);
            if (State.FakeErrors)
            {
                if (number < 3)
                {
                    throw new Exception();
                }
            }

            if (State.SimulateNonPassingValidations)
            {
                if (number < 2)
                {
                    context.Publish<IValidationComplete>(new ValidationComplete(msg.ProjectId, msg.RowData, false, false, "A slow non critical issue"));
                    return Task.FromResult(0);
                }
            }
            else
            {
                context.Publish<IValidationComplete>(new ValidationComplete(msg.ProjectId, msg.RowData, true, false, null));
            }

            return Task.FromResult(0);
        }
    }
}
