using RedisQueueDemo.Core;
using Shiny.Redis;

namespace RedisQueueDemo.Stream
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
            for (int i = 0; i < 4; i++)
            {
                _redisCacheManager.AddSteamQueue(RedisConfig.Stream, new RedisMessageModel { });
            }


        }
    }
}