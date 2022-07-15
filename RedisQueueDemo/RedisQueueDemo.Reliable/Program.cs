global using RedisQueueDemo.Core;
global using RedisQueueDemo.Reliable;
global using Shiny.Redis;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddRedisCacheManager(RedisConfig.RedisString);
        services.AddHostedService<Worker>();
        services.AddHostedService<Consumer>();
    })
    .Build();

await host.RunAsync();
