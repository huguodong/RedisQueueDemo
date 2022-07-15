using RedisQueueDemo.Core;
using Shiny.Redis;

namespace RedisQueueDemo.Delay
{
    public class Consumer : BackgroundService
    {
        private readonly IRedisCacheManager _redisCacheManager;

        public Consumer(IRedisCacheManager redisCacheManager)
        {
            this._redisCacheManager = redisCacheManager;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queue = _redisCacheManager.GetDelayQueue<RedisMessageModel>(RedisConfig.DelayKey);
            while (!stoppingToken.IsCancellationRequested)
            {
                List<RedisMessageModel> acknowledges = new List<RedisMessageModel>();//已消费消息列表

                //一次拿十条，如果拿一条就用queue.TakeOneAsync(-1);-1是超时时间，默认0秒永远阻塞；负数表示直接返回，不阻塞。
                var data = queue.Take(10).ToList();
                if (data.Count > 0)
                {
                    Console.WriteLine($"消费者拿到了:{data.Count}条消息");

                    data.ForEach(msg =>
                    {
                        Console.WriteLine($"消费者收到消息,消息ID:{msg.Id},内容:{msg.Data},时间：{DateTime.Now}");
                        acknowledges.Add(msg);//添加到已消费消息列表,这里需要转成Json字符串,如果是用直接queue.TakeOneAsync取的直接queue.Acknowledge(mqMsg);
                        Console.WriteLine("消费成功");
                    });
                    queue.Acknowledge(acknowledges.ToArray());//告诉队列已经消费了的数据
                }
                else
                {
                    Console.WriteLine("消费者从队列中没有拿到数据:" + DateTime.Now);
                    await Task.Delay(1000, stoppingToken);
                }
            }
        }
    }
}
