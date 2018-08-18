using System;
using System.Threading;
using adub_netcoremasstransit.IoC;
using adub_netcoremasstransit.MessageBus;
using adub_netcoremasstransit.MessageContracts;

namespace adub_netcoremasstransit
{
    public class Program
    {
        static void Main(string[] args)
        {
            var bus = DependencyFactory.Instance.GetInstance<ITransitBus>().Start();
            
            do
            {
                bus.Publish(new SampleMessage { Text = $"Hi {DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}" });
                Thread.Sleep(30000);
            } while (true);
        }
    }
}
