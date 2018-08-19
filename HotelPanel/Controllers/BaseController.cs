using Common;
using Constants;
using Data;
using Data.DataModel;
using Logic.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelPanel.Controllers
{
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// در این متد مشخص میکند ایا کاربر اجازه دسترسی به صفحه درخواست شده را دارد یا خیر
        /// </summary>
        /// <returns>true or false</returns>
        public bool Accessably()
        {
            //var access = Utility.GetURL(Request.Url.ToString());
            //var _PrivilegeID = DataContext.Context.Privileges.FirstOrDefault(x => x.Title == access.ActionName && x.ControllerName == access.ControllerName)?.PrivilegeID ?? 0;
            //var _UserID = long.Parse(Session["User"].ToString());
            //var _RoleID = DataContext.Context.Users.FirstOrDefault(u => u.UserId == _UserID).Roles.Select(r => r.RoleID).First();
            //var _AccessResult = DataContext.Context.RolePrivileges.FirstOrDefault(rp => rp.RoleID == _RoleID && rp.PrivilegeID == _PrivilegeID);
            //if (_AccessResult == null && access.ActionName != "AccessDenied")
            //{
            //    return false;
            //}
            return true;
        }
        /// <summary>
        /// Current User While Process Application
        /// </summary>
        public User1 CurrentUser
        {
            get
            {
                var userId = iUser.CurrentUserId;
                if (userId <= 0) return null;
                return new PublicBusiness().GetUser(userId);
            }
            set => iUser.CurrentUserId = value?.UserID ?? 0;
        }
        /// <summary>
        /// در خواست لاگوت ارسال شده است؟ request آیا در
        /// </summary>
        public bool DoLogout
        {
            get
            {
                return !String.IsNullOrEmpty(Request.QueryString["logout"]);
            }
        }
        public bool AntiAFK
        {
            get
            {
                if (!SessionIsValid)
                    return false;
                Boolean Result = true;
                DateTime Guardian = (DateTime)Session["Guardian"];
                DateTime DO = DateTime.Now;
                if (DO < Guardian.AddMinutes(5))
                {
                    Result = false;
                }
                return Result;
            }
        }
        /// <summary>
        /// سشن جاری کاربر حذف و کاربر را لاگوت می کند.
        /// </summary>
        public void LogoutUser()
        {
            Session["User"] = null;
        }
        public void Eror(string er)
        {
            Session["Eror"] = er;
        }
        /// <summary>
        /// مشخص می کند که آیا کاربر لاگین است یا نه
        /// </summary>
        public bool SessionIsValid => CurrentUser != null;
        /// <summary>
        /// آیا در صفحه لاگین هستیم؟
        /// </summary>
        public bool IsLoginPage
        {
            get
            {
                try
                {
                    var loginPages = Key.Routes.Login.ToLower();
                    var currentUrl = Request.RequestContext.RouteData.Values.Values.First().ToString().ToLower();
                    Boolean Check= loginPages.Contains(currentUrl);
                    return Check;
                }
                catch { }
                return false;
            }
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {


            }
            catch (Exception)
            {

            }
            base.OnActionExecuted(filterContext);
        }
        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            if (DoLogout)
            {
                LogoutUser();
                return;
            }
            //if (AntiAFK)
            //{
            //    LogoutUser();
            //    context.Result = new RedirectResult("/Login" + "?u=" + context.HttpContext.Request.Url.AbsolutePath);
            //    return;
            //}
            if (!SessionIsValid && !IsLoginPage)
            {
                //اگر سشن کاربر معایر نباشد و در صفحه لاگین هم نباشد او را به صفحه لاگین می فرستیم .
                context.Result = new RedirectResult("/Login" + "?u=" + context.HttpContext.Request.Url.AbsolutePath);
                return;
            }
            // برای غیر فعال شدن اجازه دسترسی کد زیر را کامنت میکنیم
            if (!Accessably())
                context.Result = new RedirectResult("/Home/AccessDenied");
            base.OnActionExecuting(context);
        }

    }
}