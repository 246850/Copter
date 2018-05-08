using System.Collections.Generic;
using System.Net.Mail;

namespace Copter.Infrastructure.Utils
{
    /// <summary>
    /// 邮件信息 实体类
    /// </summary>
    public sealed class EmailInfoModel
    {
        public EmailInfoModel()
        {
            Priority = MailPriority.Normal;
            EnableSsl = true;
            UseDefaultCredentials = false;
            Port = 80;
            ToList = new List<string>(10);
            CCList = new List<string>();
            AttachmentList = new List<string>();
        }
        /// <summary>
        /// 邮件服务器地址
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 邮件服务器 端口 - 默认：80
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 是否采用默认证书 - 默认：False
        /// </summary>
        public bool UseDefaultCredentials { get; set; }
        /// <summary>
        /// 是否开启 SSL
        /// </summary>
        public bool EnableSsl { get; set; }
        /// <summary>
        /// 主题|标题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 优先级|重要性
        /// </summary>
        public MailPriority Priority { get; set; }
        /// <summary>
        /// 发送人
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// 接收人列表
        /// </summary>
        public IList<string> ToList { get; set; }
        /// <summary>
        /// 抄送人列表
        /// </summary>
        public IList<string> CCList { get; set; }
        /// <summary>
        /// 附件全路径列表
        /// </summary>
        public IList<string> AttachmentList { get; set; }
        /// <summary>
        /// NetworkCredential 授权账号 - 通常为发送人 邮箱登录账号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// NetworkCredential 授权密码 - 通常为发送人 邮箱登录密码
        /// </summary>
        public string Password { get; set; }
    }
}
