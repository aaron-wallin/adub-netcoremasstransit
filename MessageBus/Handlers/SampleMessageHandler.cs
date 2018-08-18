using adub_netcoremasstransit.Configuration;
using adub_netcoremasstransit.MessageContracts;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace adub_netcoremasstransit.MessageBus.Handlers
{
    public class SampleMessageHandler : IConsumer<SampleMessage>
    {
        public SampleMessageHandler(IRabbitConnectionInfo rabbitConnectionInfo)
        {
            Console.WriteLine($"Testing dependency injection: {rabbitConnectionInfo.Host}");
        }

        public Task Consume(ConsumeContext<SampleMessage> context)
        {
            Console.WriteLine($"CONSUMING MESSAGE IN SAMPLEMESSAGEHANDLER: {context.Message.Text}");
            return Task.CompletedTask;
        }
    }
}
