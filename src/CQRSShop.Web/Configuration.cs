namespace CQRSShop.Web
{
    using System.Configuration;
    using System.Net;

    using EventStore.ClientAPI;
    using EventStore.ClientAPI.SystemData;

    public static class Configuration
    {
        private static IEventStoreConnection connection;

        public static IPAddress EventStoreIp
        {
            get
            {
                var hostname = ConfigurationManager.AppSettings["EventStoreHostName"];
                if (string.IsNullOrEmpty(hostname))
                {
                    return IPAddress.Loopback;
                }

                var addresses = Dns.GetHostAddresses(hostname);
                return addresses[0];
            }
        }

        public static int EventStorePort
        {
            get
            {
                var eventStorePort = ConfigurationManager.AppSettings["EventStorePort"];
                return string.IsNullOrEmpty(eventStorePort) ? 1113 : int.Parse(eventStorePort);
            }
        }

        public static IEventStoreConnection CreateConnection()
        {
            return connection = connection ?? Connect();
        }

        private static IEventStoreConnection Connect()
        {
            ConnectionSettings settings =
                ConnectionSettings.Create()
                    .UseConsoleLogger()
                    .SetDefaultUserCredentials(new UserCredentials("admin", "changeit"));

            var endPoint = new IPEndPoint(EventStoreIp, EventStorePort);
            var eventStoreConnection = EventStoreConnection.Create(settings, endPoint, null);
            eventStoreConnection.Connect();

            return eventStoreConnection;
        }
    }
}