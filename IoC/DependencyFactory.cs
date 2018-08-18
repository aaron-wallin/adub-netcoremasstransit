using adub_netcoremasstransit.Configuration;
using adub_netcoremasstransit.MessageBus;
using Lamar;

namespace adub_netcoremasstransit.IoC
{
    public sealed class DependencyFactory
    {
        private static DependencyFactory instance = null;
        private static readonly object padlock = new object();
        public IContainer Container { get; set; }

        DependencyFactory()
        {
            this.Container = new Container((config) => {
                config.For<IRabbitConnectionInfo>().Use(new RabbitConnectionInfo().Initialize("masstransit-poc")).Singleton();
                config.For<ITransitBus>().Use<TransitBus>().Singleton();
            });
        }

        public static DependencyFactory Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DependencyFactory();
                    }
                    return instance;
                }
            }
        }

        public T GetInstance<T>()
        {
            return Container.GetInstance<T>();
        }
    }
}