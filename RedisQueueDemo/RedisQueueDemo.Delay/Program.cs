using RedisQueueDemo.Core;
using RedisQueueDemo.Delay;
using Shiny.Redis;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddRedisCacheManager(RedisConfig.RedisString);
        services.AddHostedService<Worker>();
        services.AddHostedService<Consumer>();
    })
    .Build();

await host.RunAsync();
