using RedisQueueDemo.Core;
using Shiny.Redis;

namespace RedisQueueDemo.General
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRedisCacheManager _redisCacheManager;

        public Worker(ILogger<Worker> logger, IRedisCacheManager redisCacheManager)
        {
            _logger = logger;
            this._redisCacheManager = redisCacheManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                //_redisCacheManager.Set("test", DateTimeOffset.Now.ToString());
                //_logger.LogInformation("Worker running at: {time}", _redisCacheManager.Get<string>("test"));

                _redisCacheManager.AddQueue(RedisConfig.GeneralKey, new RedisMessageModel());
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}