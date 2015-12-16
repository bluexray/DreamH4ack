﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;
using System.Linq;
using log4net.Core;
using DH.log4net.ElasticSearch.Infrastructure;
using log4net.Util;


namespace DH.log4net.ElasticSearch
{
    public static class ExtensionMethods
    {
        public static void Do<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var item in self)
            {
                action(item);
            }
        }

        public static string With(this string self, params object[] args)
        {
            return string.Format(self, args);
        }

        public static IEnumerable<KeyValuePair<string, string>> Properties(this LoggingEvent self)
        {
            return self.GetProperties().AsPairs();
        }

        public static string ToJson<T>(this T self)
        {
            return new JavaScriptSerializer().Serialize(self);
        }

        public static bool Contains(this StringDictionary self, string key)
        {
            return self.ContainsKey(key) && !self[key].IsNullOrEmpty();
        }

        public static bool ToBool(this string self)
        {
            return bool.Parse(self);
        }

        public static StringDictionary ConnectionStringParts(this string self)
        {
            var builder = new DbConnectionStringBuilder
            {
                ConnectionString = self.Replace("{", "\"").Replace("}", "\"")
            };

            var parts = new StringDictionary();
            foreach (string key in builder.Keys)
            {
                parts[key] = Convert.ToString(builder[key]);
            }
            return parts;
        }

        static IEnumerable<KeyValuePair<string, string>> AsPairs(this ReadOnlyPropertiesDictionary self)
        {
            return self.GetKeys().Select(key => Pair.For(key, self[key].ToStringOrNull()));
        }

        static string ToStringOrNull(this object self)
        {
            return self != null ? self.ToString() : null;
        }

        static bool IsNullOrEmpty(this string self)
        {
            return string.IsNullOrEmpty(self);
        }
    }
}