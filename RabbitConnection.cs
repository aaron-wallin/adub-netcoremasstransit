using System;
using Newtonsoft.Json.Linq;

namespace adub_netcoremasstransit
{
    public class RabbitConnectionInfo
    {
        public string Host { get; private set; }
        public string Port { get; private set; }
        public string VHost { get; private set; }
        public string User { get; private set; }
        public string Password { get; private set; }

        public RabbitConnectionInfo()
        {
            Load();
        }
        private void Load()
        {
            var baseData = "$..[?(@.name=='masstransit-poc')].credentials.protocols.amqp";
            var jObj = JObject.Parse(Environment.GetEnvironmentVariable("VCAP_SERVICES"));

            if (jObj.SelectToken($"{baseData}") == null)
                throw new Exception("Expects a PCF managed RabbitMQ service binding named 'masstransit-poc'");

            this.Host = (string)jObj.SelectToken($"{baseData}.host");
            this.VHost = (string)jObj.SelectToken($"{baseData}.vhost");
            this.User = (string)jObj.SelectToken($"{baseData}.username");
            this.Password = (string)jObj.SelectToken($"{baseData}.password");
            this.Port = (string)jObj.SelectToken($"{baseData}.port");
        }
    }
}