using NewLife.Serialization;
using Shiny.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisQueueDemo.Reliable
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
            var queue = _redisCacheManager.GetReliableQueue<RedisMessageModel>(RedisConfig.ReliableKey);
            queue.RetryInterval = 5;//重新处理确认队列中死信的间隔。默认60s
            while (!stoppingToken.IsCancellationRequested)
            {
                List<string> acknowledges = new List<string>();//已消费消息列表

                //一次拿十条，如果拿一条就用queue.TakeOneAsync(-1);-1是超时时间，默认0秒永远阻塞；负数表示直接返回，不阻塞。
                var data = queue.Take(10).ToList();
                if (data.Count > 0)
                {
                    Console.WriteLine($"消费者拿到了:{data.Count}条消息");
                    int i = 0;
                    data.ForEach(msg =>
                    {
                        Console.WriteLine($"消费者收到消息,消息ID:{msg.Id},内容:{msg.Data}");
                        if (i < 2)//3条消息，设置一条消费失败
                        {
                            acknowledges.Add(msg.ToJson());//添加到已消费消息列表,这里需要转成Json字符串,如果是用直接queue.TakeOneAsync取的直接queue.Acknowledge(mqMsg);
                            Console.WriteLine("消费成功");
                        }
                        else
                        {
                            Console.WriteLine($"消费消息失败:消息ID:{msg.Id},时间:{DateTime.Now}");
                        }
                        i++;
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
