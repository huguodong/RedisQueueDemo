using Shiny.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisQueueDemo.General
{
    public class Consumer1 : BackgroundService
    {
        private readonly IRedisCacheManager _redisCacheManager;

        public Consumer1(IRedisCacheManager redisCacheManager)
        {
            this._redisCacheManager = redisCacheManager;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                //向队列中取数据,可以取多条
                var data = _redisCacheManager.GetQueue<RedisMessageModel>(RedisConfig.GeneralKey, 1);
                if (data.Count > 0)
                {
                    data.ForEach(msg =>
                    {
                        Console.WriteLine($"Consume1收到消息,消息ID:{msg.Id},内容:{msg.Data}");
                    });
                }
                else
                {
                    Console.WriteLine("Consume1从队列中没有拿到数据:" + DateTime.Now);
                    await Task.Delay(1000, stoppingToken);
                }

            }
        }
    }
}
