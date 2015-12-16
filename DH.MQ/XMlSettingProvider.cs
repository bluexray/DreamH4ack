using System;
using System.IO;
using System.Collections.Concurrent;
using System.Xml.Serialization;

namespace DH.MQ
{
    public class XMlSettingProvider:ISettingProvider
    {

        private readonly object _loadLock = new object();
        private readonly ConcurrentDictionary<Type, object> _settingsCache = new ConcurrentDictionary<Type, object>();

        private string path = AppDomain.CurrentDomain.BaseDirectory + @"\Config";

        private string FilePath = string.Empty;


        private string CombFullName()
        {
            return Path.Combine(path, "setting.xml");
        }

        public ConfigSetting GetCurrentSetting()
        {

            XMLConfig config = new XMLConfig();

            try
            {
                if (!File.Exists(FilePath))
                {
                    throw new Exception("不能找到MQ所需要的XML配置文件！");
                }

                using (var fs  = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer xmlserializer = new XmlSerializer(typeof(XMLConfig));

                   config = (XMLConfig)xmlserializer.Deserialize(fs);
                }

            }
            catch (Exception)
            {
                
                throw;
            }

            return config;
        }

        public XMlSettingProvider()
        {

            FilePath = CombFullName();
        }
       
    }
}
