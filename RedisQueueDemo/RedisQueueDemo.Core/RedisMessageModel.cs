namespace RedisQueueDemo.Core
{
    /// <summary>
    /// 消息实体
    /// </summary>
    public class RedisMessageModel
    {

        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Time { get; set; } = DateTime.Now;


        /// <summary>
        /// 消息内容
        /// </summary>
        public string Data { get; set; } = $"时间:{DateTime.Now}";

    }
}
