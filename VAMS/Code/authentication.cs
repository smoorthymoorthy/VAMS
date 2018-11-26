using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAMS.Common;

namespace VAMS.Code
{
    public static class authentication
    {
        public static class User
        {
            public static bool LoginStatus
            {
                get { if (UserSession.RoleID > 0) return true; else return false; }
            }
        }

        public static string CookieUserName
        {
            get { return GetCookie("UserName"); }
            //set { SetCookie("UserName") = value; }
        }

        public static void AutoLogin()
        {
            if (!HttpContext.Current.Request.Path.EndsWith("/login.aspx"))
            {
                //string UserName = CookieUserName;
                //if (!string.IsNullOrEmpty(UserName))
                //{
                //    User User = User.Load(UserName);
                //    if (User != null)
                //    {
                //        if (!string.IsNullOrEmpty(User.Generate))
                //        {
                //            //Block clone of banned user
                //            User AliasUser = User.Load(User.Generate);
                //            if (AliasUser != null && AliasUser.GeneralRole == Authentication.User.RoleType.Excluded)
                //            {
                //                LockUser(User);
                //            }
                //        }
                //        if (User.AutoLogIn)
                //        {
                //            Authentication.LogIn(User);
                //        }
                //    }
                //}
            }
        }

        public static string GetCookie(string name)
        {
            name = name.ToLower();
            //Session is notting when cookie is disabled!
            if ((HttpContext.Current.Session != null))
            {
                if ((HttpContext.Current.Session[name] != null))
                {
                    return HttpContext.Current.Session[name].ToString();
                }
            }
            if ((HttpContext.Current.Request.Cookies[name] != null))
            {
                string Value = HttpContext.Current.Request.Cookies[name].Value;
                //Session is notting when cookie is disabled!
                if ((HttpContext.Current.Session != null))
                {
                    HttpContext.Current.Session[name] = Value;
                }
                return Value;
            }

            return null;
        }
        public static void SetCookie(string name, string value, DateTime expires = new DateTime())
        {
            HttpCookie aCookie = new HttpCookie(name);
            aCookie.Value = value;
            System.DateTime DateNotSet = new DateTime();
            if (expires == DateNotSet)
            {
                aCookie.Expires = DateTime.Now.AddYears(1);
            }
            HttpContext.Current.Response.Cookies.Add(aCookie);
            //Session is notting when cookie is disabled!
            if ((HttpContext.Current.Session != null))
            {
                HttpContext.Current.Session[name] = value;
            }
        }

        //public User CurrentUser(System.Web.SessionState.HttpSessionState session)
        //{
        //    get
        //    {
        //        if (session == null)
        //        {
        //            try                 //NOT remove this command! "Try" is necessary!
        //            {
        //                session = HttpContext.Current.Session;
        //            }
        //            catch (Exception ex) {}
        //        }

        //        if (session != null)
        //        {
        //            if (session["user"] != null)
        //            {
        //                return (User)session["user"];
        //            }
        //            else
        //            {
        //                User user = new User();
        //                session["user"] = user;
        //                return user;
        //            }
        //        }
        //        else
        //        {
        //            return new Authentication.User();
        //        }
        //    }
        //}
    }
}