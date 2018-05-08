using System;
using System.Net.Http;
using Copter.Logger.Models;
using Copter.Net.Esb;
using Copter.Infrastructure.MessageHandler;

namespace Copter.Logger
{
    /// <summary>
    /// 远程Http请求 日志记录类
    /// </summary>
    public class EsbLoggerProvider:ILogger
    {
        private static readonly Lazy<IEsbClient> Lazy = new Lazy<IEsbClient>(()=> new ESBClient(new HttpClient(new Sha1AuthorizationMessageHandler()))); 

        /// <summary>
        /// 日志推送接口
        /// </summary>
        protected string LogUrl { get; private set; }
        public EsbLoggerProvider(string logUrl)
        {
            LogUrl = logUrl;
        }
        public void Log(LogEntity entity)
        {
            Lazy.Value.Post(LogUrl, entity);
        }
    }
}
