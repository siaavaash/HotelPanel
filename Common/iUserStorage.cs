using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class iUserStorage
    {
        private static JsonSerializerSettings JsonConvertSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.None,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            //PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            ReferenceLoopHandling = ReferenceLoopHandling.Error
        };
        public static bool Store(string key, object value)
        {
            try
            {
                var json = JsonConvert.SerializeObject(value, JsonConvertSettings);
                HttpContext.Current.Session[key] = json;
                //log(" SUCCEED! " + key + Environment.NewLine);
                return true;
            }
            catch (Exception ex)
            {
                log(value.GetType().ToString() + " FAILED! with key: " + key + Environment.NewLine);
                return false;
            }
        }

        public static T Retrive<T>(string key)
        {
            try { return JsonConvert.DeserializeObject<T>(HttpContext.Current.Session[key].ToString(), JsonConvertSettings); } catch { return default(T); }
        }
        /// <summary>
        /// delete all session
        /// </summary>
        public static void Clear()
        {
            try { HttpContext.Current.Session.Clear(); } catch { }
        }
        /// <summary>
        /// delete a session by key
        /// </summary>
        /// <param name="key">session name</param>
        public static void Remove(string key)
        {
            try { HttpContext.Current.Session.Remove(key); } catch { }
        }
        public static void log(string s)
        {
            System.IO.File.AppendAllText(System.IO.Path.Combine(AppSettings.GlobalTempPath, "sessionstorage.txt"), s);
        }
    }
}
