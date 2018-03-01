using MassTransit;
using MassTransitConsumerDemoHost.FakeDataStorage;
using Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitConsumerDemoHost.Consumers
{
    public class ProjectRowsPublishedConsumer : IConsumer<IProjectRowsPublished>
    {
        const int NumberOfValidations = 3;
        public Task Consume(ConsumeContext<IProjectRowsPublished> context)
        {
            var msg = context.Message;
            if (State.ProjectsStatus.Any(s => s.ProjectId == msg.ProjectId))
            {
                //Swallow the message we got a replay
            }
            else
            {
                State.ProjectsStatus.Add(new ProjectState(msg.RowNumber * 3, msg.ProjectId));
            }
            return Task.FromResult(0);
        }
    }
}
