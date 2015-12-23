using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;


using Elasticsearch.Net;
using NLog.Common;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using NLog;

namespace DH.Nlog.ElasticSearch
{
    
    [Target("ElasticSearch")]
    public class ElasticSearchTarget : TargetWithLayout
    {
        
        public ElasticSearchTarget()
        {
            this.Uri = "http://localhost:9200";
            this.DocumentType = "logevent";
            this.Index = "logstash-${date:format=yyyy.MM.dd}";
            this.Fields = new List<ElasticSearchField>();
        }

        
        private string GetConnectionString(string name)
        {
            string environmentVariable = this.GetEnvironmentVariable(name);
            if (!string.IsNullOrEmpty(environmentVariable))
            {
                return environmentVariable;
            }
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[name];
            if (connectionStringSettings == null)
            {
                return null;
            }
            return connectionStringSettings.ConnectionString;
        }

        
        private string GetEnvironmentVariable(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return Environment.GetEnvironmentVariable(name);
        }

        
        protected override void InitializeTarget()
        {
            base.InitializeTarget();
            string text = this.GetConnectionString(this.ConnectionStringName) ?? this.Uri;

            var u = new Uri(text);

            List<Uri> enumerable = new List<Uri>();

            enumerable.Add(u);

            //IEnumerable<Uri> enumerable = from url in text.Split(',')
            //                              select new Uri(url);

            //var host = text.Split(',').ToList<Uri>();

            StaticConnectionPool staticConnectionPool = new StaticConnectionPool(enumerable, true, null);
            ConnectionConfiguration connectionConfiguration = new ConnectionConfiguration(staticConnectionPool);
            //this._client = new ElasticsearchClient(connectionConfiguration, null, null, this.ElasticsearchSerializer);
            this._client = new ElasticsearchClient(connectionConfiguration);
            if (!string.IsNullOrEmpty(this.ExcludedProperties))
            {
                this._excludedProperties = new List<string>(this.ExcludedProperties.Split(new char[]
                {
                    ','
                }, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        
        private void SendBatch(IEnumerable<AsyncLogEventInfo> events)
        {
            IEnumerable<LogEventInfo> enumerable = from e in events
                                                   select e.LogEvent;
            List<object> list = new List<object>();
            foreach (LogEventInfo current in enumerable)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("@timestamp", current.TimeStamp);
                dictionary.Add("level", current.Level.Name.ToUpper());
                if (current.Exception != null)
                {
                    dictionary.Add("exception", current.Exception.ToString());
                }
                dictionary.Add("APPID", APPID);
                dictionary.Add("applicationName", current.LoggerName);
                dictionary.Add("message", current.Message);
                dictionary.Add("Stack", current.StackTrace) ;
                
                
                //dictionary.Add("message", this.Layout.Render(current));
                foreach (ElasticSearchField current2 in this.Fields)
                {
                    string text = current2.Layout.Render(current);
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        dictionary[current2.Name] = text.ToSystemType(current2.LayoutType);
                    }
                }
                if (this.IncludeAllProperties)
                {
                    foreach (KeyValuePair<object, object> current3 in from p in current.Properties
                                                                      where !this._excludedProperties.Contains(p.Key)
                                                                      select p)
                    {
                        if (!dictionary.ContainsKey(current3.Key.ToString()))
                        {
                            dictionary[current3.Key.ToString()] = current3.Value;
                        }
                    }
                }
                string index = this.Index.Render(current).ToLowerInvariant();
                string type = this.DocumentType.Render(current);
                list.Add(new
                {
                    index = new
                    {
                        _index = index,
                        _type = type
                    }
                });
                list.Add(dictionary);
            }
            try
            {
                ElasticsearchResponse<byte[]> elasticsearchResponse = this._client.Bulk<byte[]>(list, null);
                if (!elasticsearchResponse.Success)
                {
                    InternalLogger.Error("Failed to send log messages to ElasticSearch: status={0} message=\"{1}\"", new object[]
                    {
                        elasticsearchResponse.HttpStatusCode,
                        elasticsearchResponse.OriginalException.Message
                    });
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Error("Error while sending log messages to ElasticSearch: message=\"{0}\"", new object[]
                {
                    ex.Message
                });
            }
        }

        
        protected override void Write(AsyncLogEventInfo logEvent)
        {
            this.Write(new AsyncLogEventInfo[]
            {
                logEvent
            });
        }

        
        protected override void Write(AsyncLogEventInfo[] logEvents)
        {
            this.SendBatch(logEvents);
        }

        
        public string ConnectionStringName
        {
            
            get;
            
            set;
        }

        
        [RequiredParameter]
        public Layout DocumentType
        {

            get;

            set;
        }


        public IElasticsearchSerializer ElasticsearchSerializer
        {

            get;

            set;
        }

        public string ExcludedProperties
        {

            get;

            set;
        }


        [ArrayParameter(typeof(ElasticSearchField), "field")]
        public IList<ElasticSearchField> Fields
        {

            get;

            private set;
        }


        public bool IncludeAllProperties
        {

            get;

            set;
        }


        public Layout Index
        {

            get;

            set;
        }


        public string Uri
        {

            get;

            set;
        }


        //用来区分不同的程序域
        public string APPID
        {
            get;
            set;
        }


        private IElasticsearchClient _client;


        private List<string> _excludedProperties = new List<string>(new string[]
        {
            "CallerMemberName",
            "CallerFilePath",
            "CallerLineNumber",
            "MachineName",
            "ThreadId"
        });
    }
}