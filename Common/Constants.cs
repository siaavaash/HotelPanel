using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constants
{
    public static class URL
    {
        public static class Routes
        {
            public const string Home = "/Admin/Index";
            public const string Login = "/Login/Index";
        }
    }
    public static class Key
    {
        public static class Routes
        {
            public const string Login = "Login";
        }
    }    
    public static class PublicConstants
    {
        public static class Session
        {
            public const string CurrentCurrency = "CurrentCurrency";
            public const string CurrentLanguage = "CurrentLanguage";
            public const string CurrentUserId = "CurrentUserId";
            public const string HSession = "HSession";
            public const string Link = "Link";
            public const string LastKeepSessionAliveCall = "LastKeepSessionAliveCall";
            public const string Fare = "Fare";
            public const string iWallet = "iWallet";
            public const string iCIP = "iCIP";
            public const string AddFundWallet = "AddFundWallet";
            public const string Notification = "Notification";
            public const string Suggestion = "Suggestion";
            public const string PaymentError = "PaymentError";
            public const string iCaptcha = "iCaptcha";
            public const string Message_Error = "Message_Error";
            public const string Message_Success = "Message_Success";
            public const string Message_Warning = "Message_Warning";
        }
        public static class Message
        {            
            public const string Faild = "Sorry, your request could not be completed please try again";
            public const string Success = "Your request has been successfully completed";
            public const string ModelState = "Your Model State Is Not Valid";
        }
    }
}
