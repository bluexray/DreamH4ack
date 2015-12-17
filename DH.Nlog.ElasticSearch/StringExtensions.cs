using System;

namespace DH.Nlog.ElasticSearch
{
    public static class StringExtensions
    {
        public static object ToSystemType(this string field, Type type)
        {
            string fullName;
            if ((fullName = type.FullName) != null)
            {
                if (fullName == "System.Boolean")
                {
                    return Convert.ToBoolean(field);
                }
                if (fullName == "System.Double")
                {
                    return Convert.ToDouble(field);
                }
                if (fullName == "System.DateTime")
                {
                    return Convert.ToDateTime(field);
                }
                if (fullName == "System.Int32")
                {
                    return Convert.ToInt32(field);
                }
                if (fullName == "System.Int64")
                {
                    return Convert.ToInt64(field);
                }
            }
            return field;
        }
    }
}