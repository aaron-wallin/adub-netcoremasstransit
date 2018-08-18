using System.Threading.Tasks;
using MassTransit;

namespace adub_netcoremasstransit.MessageBus
{
    public interface ITransitBus
    {
        IBusControl ServiceBus { get; set; }
        ITransitBus Start();
        Task Publish<T>(T message) where T : class;
    }
}