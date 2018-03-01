using MassTransit;
using MassTransitConsumerDemoHost.FakeDataStorage;
using Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MassTransitConsumerDemoHost.Consumers
{
    public class ValidationPassedConsumer : IConsumer<IValidationComplete>
    {
        public Task Consume(ConsumeContext<IValidationComplete> context)
        {
            var msg = context.Message;
            var status = State.ProjectsStatus.FirstOrDefault(s => s.ProjectId == msg.ProjectId);
            if (status != null && !status.HasTerminated)
            {
                if (!msg.ValidationPassed) //Critical Validation Passed We are done with this item is failed forever
                {
                    if (msg.CriticalValidation)
                    {
                        //This is a failed project
                        status.HasTerminated = true;
                        status.ValidationPhasePassed = false;
                    }
                    status.ValidationErrorMessages.Add(msg.ErrorMessage);
                    status.ValidationFailed();
                }
                else
                {
                    //One validation passed
                    status.ValidationPassed();
                }
                if (status.HasTerminated)
                {
                    Console.WriteLine(new JavaScriptSerializer().Serialize(status));
                }
                return Task.FromResult(0); 
            }
            else
            {
                throw new Exception("Project Status Creation did not occur");//This will cause the message to retry
            }
        }
    }
}
