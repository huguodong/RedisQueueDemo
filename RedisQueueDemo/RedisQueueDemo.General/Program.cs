global using RedisQueueDemo.Core;
using NewLife.Caching.Models;
using RedisQueueDemo.General;
using Shiny.Redis;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddRedisCacheManager(RedisConfig.RedisString);
        services.AddHostedService<Worker>();
        services.AddHostedService<Consumer1>();
        services.AddHostedService<Consumer2>();
    })
    .Build();

await host.RunAsync();
