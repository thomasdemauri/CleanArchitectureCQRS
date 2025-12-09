using Azure.Messaging.ServiceBus;

namespace EventBusService.AzureBusService
{
    public interface IServiceBusPersisterConnection
    {
        ServiceBusClient Client { get; }
    }
    public class ServiceBusPersisterConnection : IServiceBusPersisterConnection
    {
        private readonly string _connectionString;
        private ServiceBusClient _client;

        public ServiceBusPersisterConnection(string connectionString)
        {
            _connectionString = connectionString;
            _client = new ServiceBusClient(_connectionString);
        }

        public ServiceBusClient Client
        {
            get
            {
                if (_client.IsClosed)
                {
                    _client = new ServiceBusClient(_connectionString);
                }
                return _client;
            }
        }
    }
}
