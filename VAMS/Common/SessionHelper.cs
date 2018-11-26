using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMS.Common
{
    public class SessionHelper
    {
        SessionHelper()
        {

        }

        public static SessionHelper Current
        {
            get
            {
                SessionHelper session = (SessionHelper)HttpContext.Current.Session["__MySession__"];
                if (session == null)
                {
                    session = new SessionHelper();
                    HttpContext.Current.Session["__MySession__"] = session;
                }
                return session;
            }
        }

        public int UserID { get; set; }
        public int CompanyIDCurrent { get; set; }
        public long LoginCompanyIDCurrent { get; set; }

        public string Lang { get; set; }
        public long GroupIDCurrent { get; set; }
        public string Email { get; set; }
        public long IsRegistration { get; set; }
        public long RoleID { get; set; }
        public string CurrentPageName { get; set; }
        public string CompanyName { get; set; }
        public bool IsPrimary { get; set; }

        public bool FromFramework { get;set; }
        public int CountryID { get; set; }
        public int CountryIDCurrent { get; set; }
        public int GroupID { get; set; }
        public int CompanyIDUser { get; set; }
        public int LocationIDUser { get; set; }
        public int LocationIDCurrent { get; set; }
        public int LanguageID { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string ThemeFolderPath { get; set; }
        public string LocationName { get; set; }
        public string Announcement { get; internal set; }

        public static void ClearSession()
        {
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }


    }
}