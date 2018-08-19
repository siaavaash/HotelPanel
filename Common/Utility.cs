using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace Common
{
    public static class Utility
    {
        public static string ConvertNumbersToPersian(this string str)
        {
            return str.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴").Replace("5", "۵").Replace("6", "۶").Replace("7", "v").Replace("8", "۸").Replace("9", "۹");
        }
        public static string ToMD5(this string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
        public static List<string> CharSeparator(string inputString, char inputFromat)
        {
            char[] commaSeparator = new char[] { inputFromat };
            //استفاه از تابع برای شکستن متن
            string[] authors = inputString?.Split(commaSeparator);

            return authors.ToList();
        }
        public static string ParsDate(List<string> Date, string Format)
        {
            var ParsDate = "";
            switch (Format)
            {
                case "dd/MM/yyyy":
                    ParsDate = Date[1] + "/" + Date[2] + "/" + Date[0];
                    break;
                case "MM/dd/yyyy":
                    ParsDate = Date[2] + "/" + Date[1] + "/" + Date[0];
                    break;
                case "yyyy/MM/dd":
                    ParsDate = Date[0] + "/" + Date[2] + "/" + Date[1];
                    break;
                case "yyyy/dd/mm":
                    ParsDate = Date[0] + "/" + Date[1] + "/" + Date[2];
                    break;
            }
            return ParsDate;
        }
        public static URLdata GetURL(string fullUrl)
        {
            var questionMarkIndex = fullUrl.IndexOf('?');
            string queryString = null;
            string url = fullUrl;
            if (questionMarkIndex != -1) // There is a QueryString
            {
                url = fullUrl.Substring(0, questionMarkIndex);
                queryString = fullUrl.Substring(questionMarkIndex + 1);
            }

            // Arranges
            var request = new HttpRequest(null, url, queryString);
            var response = new HttpResponse(new StringWriter());
            var httpContext = new HttpContext(request, response);

            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            // Extract the data    
            var values = routeData.Values;
            var controllerName = values["controller"];
            var actionName = values["action"];
            var areaName = values["area"];

            URLdata returndata = new URLdata()
            {
                ControllerName = controllerName?.ToString(),
                ActionName = actionName?.ToString(),
                AreaName = areaName?.ToString()
            };
            return returndata;
        }
        public class URLdata
        {
            public string ControllerName { get; set; }
            public string ActionName { get; set; }
            public string AreaName { get; set; }
        }
        public static TResult GetConfig<TResult>(string key)
        {
            try
            {
                return (TResult)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(TResult));
            }
            catch (ConfigurationErrorsException configEx)
            {
                throw configEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public static class ObjectExtensions
    {
        public static T DeepCopy<T>(this T source) where T : class
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (source is null)
            {
                return default(T);
            }
            using (var stream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, source);
                stream.Position = 0;
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }
}
