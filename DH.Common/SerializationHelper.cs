using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DH.Common
{
    public static class SerializationHelper
    {
        public static byte[] ToByteArray<T>(this T t)
        {
            using (var memoryStream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, t);
                return memoryStream.ToArray();
            }
        }

        public static object FromByteArray(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                BinaryFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(stream);
            }
        }
    }
}
