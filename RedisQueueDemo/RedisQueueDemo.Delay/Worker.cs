using RedisQueueDemo.Core;
using Shiny.Redis;

namespace RedisQueueDemo.Delay
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
            var queue = _redisCacheManager.GetDelayQueue<RedisMessageModel>(RedisConfig.DelayKey);
            //单个添加
            queue.Add(new RedisMessageModel { }, 1);
            queue.Add(new RedisMessageModel { }, 5);
            queue.Add(new RedisMessageModel { }, 10);

            //批量添加
            //queue.Delay = 5;//延迟时间,默认60s
            //queue.Add(new List<RedisMessageModel> { new RedisMessageModel { } }.ToArray());

        }
    }
}