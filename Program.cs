using System;
using MassTransit;
using System.Threading;

namespace adub_netcoremasstransit
{

    public class YourMessage { public string Text { get; set; } }
    public class Program
    {
        static void Main(string[] args)
        {
            var msgqueueconn = new RabbitConnectionInfo();
            
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri($"rabbitmq://{msgqueueconn.Host}:{msgqueueconn.Port}/{msgqueueconn.VHost}"), h =>
                {
                    h.Username(msgqueueconn.User);
                    h.Password(msgqueueconn.Password);
                });

                 
                sbc.ReceiveEndpoint(host, "test_queue", ep =>
                {
                    ep.Handler<YourMessage>(context =>
                    {
                        Console.WriteLine("Some output");
                        return Console.Out.WriteLineAsync($"Received: {context.Message.Text}");
                    });
                });


            
            });

            bus.Start();

            do
            {
                bus.Publish(new YourMessage { Text = $"Hi {DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}" });
                Thread.Sleep(10000);
            } while (true);

            //Console.WriteLine("Press any key to exit");

            //bus.Stop();

            //    Console.WriteLine("Hello World!");
        }
    }
}
