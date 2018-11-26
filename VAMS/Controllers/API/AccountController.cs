using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VAMS.Common;

using MCB = Kowni.Common.BusinessLogic;

namespace VAMS.Controllers.API
{
    public class AccountController : ApiController
    {
        MCB.BLSingleSignOn objSingleSignOn = new MCB.BLSingleSignOn();
        MCB.BLAnnouncement objAnnouncement = new MCB.BLAnnouncement();

        //[HttpPost]
        [HttpGet]
        public HttpResponseMessage CheckLogin(string UserName, string Password)
        {
            try
            {
                string _res = string.Empty;
                int RoleID = 0;
                int UserID = 0;
                int CompanyID = 0;
                string CompanyName = string.Empty;
                int LocationID = 0;
                string LocationName = string.Empty;
                int GroupID = 0;
                int LanguageID = 0;
                string UserFName = string.Empty;
                string UserLName = string.Empty;
                string UserMailID = string.Empty;
                string ThemeFolderPath = string.Empty;
                if (!objSingleSignOn.Login(UserName, Password, 0, out RoleID, out UserID, out CompanyID, out CompanyName, out LocationID, out LocationName, out GroupID, out LanguageID, out UserFName, out UserLName, out UserMailID, out ThemeFolderPath))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Unauthorized Access.", Status = false });
                }
                else
                {
                    SessionHelper.Current.FromFramework = false;
                    SessionHelper.Current.CountryID = 1;
                    SessionHelper.Current.CountryIDCurrent = 1;
                    SessionHelper.Current.RoleID = RoleID;
                    SessionHelper.Current.GroupID = GroupID;
                    SessionHelper.Current.UserID = UserID;
                    SessionHelper.Current.CompanyIDUser = CompanyID;
                    SessionHelper.Current.LocationIDUser = LocationID;
                    SessionHelper.Current.CompanyName = CompanyName;
                    SessionHelper.Current.CompanyIDCurrent = CompanyID;
                    SessionHelper.Current.LocationName = LocationName;
                    SessionHelper.Current.LocationIDCurrent = LocationID;
                    SessionHelper.Current.GroupID = GroupID;
                    SessionHelper.Current.GroupIDCurrent = GroupID;
                    SessionHelper.Current.LanguageID = LanguageID;
                    SessionHelper.Current.UserFirstName = UserFName;
                    SessionHelper.Current.UserLastName = UserLName;
                    SessionHelper.Current.ThemeFolderPath = ThemeFolderPath;
                    DataSet dsAnn = new DataSet();
                    dsAnn = objAnnouncement.GetAnnouncement(Convert.ToInt32(UserSession.CompanyIDCurrent), Convert.ToInt32(Kowni.Common.BusinessLogic.BLMenu.ToolID.MasterFramework));
                    if (dsAnn.Tables.Count > 0 && dsAnn.Tables[0].Rows.Count > 0)
                    {
                        if (dsAnn.Tables[0].Rows[0]["AnnouncementType"].ToString() == "1")
                        {
                            SessionHelper.Current.Announcement = dsAnn.Tables[0].Rows[0]["Announcement"].ToString();
                        }
                    }
                    else
                    {
                        SessionHelper.Current.Announcement = string.Empty;
                    }
                }
                var cmp = SessionHelper.Current.CompanyName;
                return Request.CreateResponse(HttpStatusCode.OK, cmp);
             

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}