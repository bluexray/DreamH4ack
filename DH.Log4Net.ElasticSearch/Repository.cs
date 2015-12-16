﻿using System.Collections.Generic;
using DH.log4net.ElasticSearch.Infrastructure;
using DH.log4net.ElasticSearch.Models;

namespace DH.log4net.ElasticSearch
{
    public interface IRepository
    {
        void Add(IEnumerable<logEvent> logEvents);
    }

    public class Repository : IRepository
    {
        readonly Uri uri;
        readonly IHttpClient httpClient;

        Repository(Uri uri, IHttpClient httpClient)
        {
            this.uri = uri;
            this.httpClient = httpClient;
        }

        public void Add(IEnumerable<logEvent> logEvents)
        {
            logEvents.Do(logEvent => httpClient.Post(uri, logEvent));
        }

        public static IRepository Create(string connectionString)
        {
            return Create(connectionString, new HttpClient());
        }

        public static IRepository Create(string connectionString, IHttpClient httpClient)
        {
            return new Repository(Uri.For(connectionString), httpClient);
        }
    }
}