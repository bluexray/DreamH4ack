using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH.Log4Net
{
    public class MessageLog
    {
        public string TraceID { get; set; }

        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime LOG_DATE { get; set; }

        /// <summary>
        /// 日志错误信息
        /// </summary>
        public string LOG_MESSAGE { get; set; }

        /// <summary>
        /// 异常信息详情
        /// </summary>
        public string LOG_EXCEPTION { get; set; }

        /// <summary>
        /// 错误级别
        /// </summary>
        public string LOG_LEVEL { get; set; }

        /// <summary>
        /// 记录器（PRMMS.Logger）
        /// </summary>
        public string LOGGER { get; set; }

        /// <summary>
        /// 日志产生位置
        /// </summary>
        public string LOG_SOURCE { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 操作账户名
        /// </summary>
        public string OperatorAccountName { get; set; }

        /// <summary>
        /// 自动创建ID
        /// </summary>
        public MessageLog()
        {
            this.TraceID = Guid.NewGuid().ToString("N").ToLower();
        }
    }
}
