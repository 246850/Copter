using System;
using System.Net;
using System.Net.Mail;

namespace Copter.Infrastructure.Utils
{
    /// <summary>
    /// 邮件 工具类
    /// </summary>
    public sealed class EmailUtil
    {
        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="model"></param>
        public static void Send(EmailInfoModel model)
        {
            if (model.ToList == null) throw new ArgumentNullException("model.ToList");

            //  host:邮件服务器，port:端口
            using (SmtpClient smtpClient = new SmtpClient(model.Host, model.Port)
            {
                EnableSsl = model.EnableSsl, // 是否启用安全套接字层加密连接
                UseDefaultCredentials = model.UseDefaultCredentials
            })
            {
                if (!model.UseDefaultCredentials)
                {
                    //  发送方身份验证
                    smtpClient.Credentials = new NetworkCredential(model.UserName, model.Password);
                }

                //  邮件对象
                MailMessage mailMessage = new MailMessage
                {
                    Subject = model.Subject,
                    Body = model.Body,
                    IsBodyHtml = true,
                    Priority = model.Priority,
                    From = new MailAddress(model.From)
                };

                //  添加收件人列表
                foreach (string to in model.ToList)
                {
                    mailMessage.To.Add(to);
                }

                //  添加抄送人列表
                if (model.CCList != null)
                {
                    foreach (string cc in model.CCList)
                    {
                        mailMessage.CC.Add(cc);
                    }
                }

                // 添加附件列表
                if (model.AttachmentList != null)
                {
                    foreach (string attachment in model.AttachmentList)
                    {
                        mailMessage.Attachments.Add(new Attachment(attachment));
                    }
                }

                //  发送邮件
                smtpClient.Send(mailMessage);

                mailMessage.Dispose();
            }
        }
    }
}
