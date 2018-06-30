using System.Security.Authentication;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace question_metrics_api
{
    public static class MongoSupportUtils
    {
        public static void AddMongo(this IServiceCollection services, string connectionString)
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(
                    new MongoUrl(connectionString)
                );

                settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

                var client = new MongoClient(settings);

            services.AddSingleton(client);
        }
    }
}