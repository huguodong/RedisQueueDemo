using RedisQueueDemo.Core;
using Shiny.Redis;

namespace RedisQueueDemo.Stream
{
    public class Group1Consumer1 : BackgroundService
    {
        private readonly IRedisCacheManager _redisCacheManager;

        public Group1Consumer1(IRedisCacheManager redisCacheManager)
        {
            this._redisCacheManager = redisCacheManager;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var groupName = "消费组1";
            var consumerName = "消费者1";
            //这里封装了一下,新的消费组将不会消费创建消费组之前的消息
            //默认新的消费组将会从头开始消费队列，可以使用FromLastOffset属性来设置从当前最新一条消息开始消费
            var queue = _redisCacheManager.GetAutoSteamQueue<RedisMessageModel>(RedisConfig.Stream, groupName, consumerName);
            //queue.FromLastOffset = true;
            while (!stoppingToken.IsCancellationRequested)
            {
                //一次拿1条，如果只拿一条就用queue.TakeOneAsync(5);5是超时时间，默认10秒。
                var data = await queue.TakeMessagesAsync(1, 5);
                if (data != null)
                {
                    var messages = data.ToList();//消息列表
                    Console.WriteLine($"{groupName}-{consumerName}拿到了:{data.Count}条消息");
                    var msgIds = messages.Select(it => it.Id).ToArray();//消息ID
                    messages.ForEach(it =>
                    {
                        var msg = it.GetBody<RedisMessageModel>();//获取实体
                        Console.WriteLine($"{groupName}-{consumerName}收到消息,消息ID:{msg.Id},内容:{msg.Data}");
                    });
                    queue.Acknowledge(msgIds);//告诉队列已经消费了的数据
                }
                else
                {
                    //Console.WriteLine("消费者从队列中没有拿到数据:" + DateTime.Now);
                    //await Task.Delay(1000, stoppingToken);
                }
            }
        }
    }
}
