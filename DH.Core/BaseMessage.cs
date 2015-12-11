using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DH.Common;


namespace DH.Core
{
    /// <summary>
    /// 消息主体
    /// </summary>
    [Serializable]
    public class BaseMessage
    {
        public virtual BaseMessage BuildMessageResult(byte[] e)
        {

            var result = (BaseMessage)SerializationHelper.FromByteArray(e);

            return result;
        }
    }
}
