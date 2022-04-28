namespace Assets.Scripts
{
    /// <summary>
    ///     此类用来模拟数据协议
    ///     具体使用时,可以自定义
    /// </summary>
    public class DataProtocol
    {
        /// <summary>
        ///     消息类型,由通讯双方约定
        /// </summary>
        public string MessageCode { get; set; }



        /// <summary>
        ///     消息内容,可以放业务JSON串
        /// </summary>
        public string MessageContent { get; set; }



        /// <summary>
        ///     发送消息的时间,取本地时间
        /// </summary>
        public string SendTimeLocal { get; set; }
    }
}