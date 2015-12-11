using DH.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH.Core
{
    [Serializable]
    public class TextMessage:BaseMessage
    {
        public string Text { get; set; }

        public override BaseMessage BuildMessageResult(byte[] e)
        {
            var result = (TextMessage)SerializationHelper.FromByteArray(e);

            Console.WriteLine(result.Text.ToString());
            return result;
        }
    }
}
