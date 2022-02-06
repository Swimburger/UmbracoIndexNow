using System.Configuration;

namespace UmbracoIndexNow.IndexNow
{
    public static class IndexNowConfiguration
    {
        public static string BaseUrl => ConfigurationManager.AppSettings["IndexNow.BaseUrl"];
        public static string Url => ConfigurationManager.AppSettings["IndexNow.Url"];
        public static string Host => ConfigurationManager.AppSettings["IndexNow.Host"];
        public static string Key => ConfigurationManager.AppSettings["IndexNow.Key"];
        public static string KeyLocation => ConfigurationManager.AppSettings["IndexNow.KeyLocation"];
    }
}