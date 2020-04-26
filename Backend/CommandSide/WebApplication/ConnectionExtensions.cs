using System;
using Microsoft.Extensions.Configuration;

namespace WebApplication
{
    public static class ConnectionExtensions
    {
        public static Uri EventStoreConnectionString(this IConfiguration configuration) =>
            new Uri(configuration["AppSettings:EventStoreConnectionString"]);
    }
}