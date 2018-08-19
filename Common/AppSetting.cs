using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace Common
{
    public static class AppSettings
    {
        private static string ValueOf(string key)
        {
            try { return Utility.GetConfig<string>(key); } catch { return ""; }
        }
        public static byte UserSessionRecheckTimeout
        {
            get
            {
                byte r = 0;
                byte.TryParse(ValueOf("User.Session.Recheck.Timeout"), out r);
                return r;
            }
        }

        //get and set GloablTempPath key in web.config
        public static string GlobalTempPath
        {
            get => WebConfigurationManager.AppSettings["GlobalTempPath"].ToString();
            set => WebConfigurationManager.AppSettings["GlobalTempPath"] = value;
        }
    }

}
