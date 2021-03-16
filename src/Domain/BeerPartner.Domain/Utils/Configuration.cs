using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace BeerPartner.Domain.Utils
{
    public class Configuration
    {
        private IConfiguration Configurations;
        private static Configuration _instance;

        private Configuration()
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);

            Configurations = builder.Build();
        }

        public static string Get(string key)
        {
            try
            {
                _instance = _instance ?? new Configuration();                
                return _instance.Configurations[key] ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}