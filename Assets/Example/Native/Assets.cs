namespace Assets.Scripts
{
    /// <summary>
    ///     ��������ģ������Э��
    ///     ����ʹ��ʱ,�����Զ���
    /// </summary>
    public class DataProtocol
    {
        /// <summary>
        ///     ��Ϣ����,��ͨѶ˫��Լ��
        /// </summary>
        public string MessageCode { get; set; }



        /// <summary>
        ///     ��Ϣ����,���Է�ҵ��JSON��
        /// </summary>
        public string MessageContent { get; set; }



        /// <summary>
        ///     ������Ϣ��ʱ��,ȡ����ʱ��
        /// </summary>
        public string SendTimeLocal { get; set; }
    }
}