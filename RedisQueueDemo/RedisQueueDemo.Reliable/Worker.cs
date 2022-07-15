namespace RedisQueueDemo.Reliable
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

            List<RedisMessageModel> redisMessageModels = new List<RedisMessageModel>();
            redisMessageModels.Add(new RedisMessageModel());
            redisMessageModels.Add(new RedisMessageModel());
            redisMessageModels.Add(new RedisMessageModel());
            //_redisCacheManager.AddReliableQueue(RedisConfig.ReliableKey, new RedisMessageModel());//添加一条
            _redisCacheManager.AddReliableQueueList(RedisConfig.ReliableKey, redisMessageModels);//一次添加三条

        }
    }
}