using RedisQueueDemo.Core;
using RedisQueueDemo.Stream;
using Shiny.Redis;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddRedisCacheManager(RedisConfig.RedisString);
        services.AddHostedService<Worker>();
        services.AddHostedService<Group1Consumer1>();
        services.AddHostedService<Group1Consumer2>();
        services.AddHostedService<Group2Consumer1>();
        services.AddHostedService<Group2Consumer2>();

    })
    .Build();

await host.RunAsync();
