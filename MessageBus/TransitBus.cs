using adub_netcoremasstransit.Configuration;
using adub_netcoremasstransit.IoC;
using adub_netcoremasstransit.MessageBus.Handlers;
using adub_netcoremasstransit.MessageContracts;
using MassTransit;
using MassTransit.LamarIntegration;
using System;
using System.Threading.Tasks;

namespace adub_netcoremasstransit.MessageBus
{
    public class TransitBus : ITransitBus
    {
        private IBusControl _serviceBus;
        private readonly IRabbitConnectionInfo rabbitConnectionInfo;
        private bool started = false;
        public IBusControl ServiceBus { get { return _serviceBus; } set { } }

        public TransitBus(IRabbitConnectionInfo rabbitConnectionInfo)
        {
            this.rabbitConnectionInfo = rabbitConnectionInfo;
        }

        public ITransitBus Start()
        {
            if (started) return this;
            
            _serviceBus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var conn = $"rabbitmq://{rabbitConnectionInfo.Host}:{rabbitConnectionInfo.Port}/{rabbitConnectionInfo.VHost}";
                Console.WriteLine($"Rabbit Connection: {conn}");

                var host = sbc.Host(new Uri(conn), h =>
                {
                    h.Username(rabbitConnectionInfo.User);
                    h.Password(rabbitConnectionInfo.Password);
                });
                
                sbc.ReceiveEndpoint(host, "sample_queue" ,ep =>
                {
                    ep.Consumer(typeof(SampleMessageHandler), type => DependencyFactory.Instance.Container.GetInstance(type));
                });
            });

            _serviceBus.Start();

            started = true;

            return this;
        }

        public async Task Publish<T>(T message) where T : class
        {   
            await _serviceBus.Publish<T>(message);
        }
        
    }
}
