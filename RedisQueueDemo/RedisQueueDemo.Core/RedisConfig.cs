namespace RedisQueueDemo.Core
{
    public class RedisConfig
    {
        /// <summary>
        /// redis链接字符串
        /// </summary>
        public const string RedisString = "server=127.0.0.1:6379;password=Shiny123456;db=4";

        /// <summary>
        /// 普通队列Key
        /// </summary>

        public const string GeneralKey = "General";

        /// <summary>
        /// 可信队列Key
        /// </summary>
        public const string ReliableKey = "Reliable";


        /// <summary>
        /// 延迟对队列Key
        /// </summary>
        public const string DelayKey = "Delay";

        /// <summary>
        /// 可重复消费队列
        /// </summary>
        public const string Stream = "Stream";

    }
}