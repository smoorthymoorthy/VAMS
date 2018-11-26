using System;
using System.Collections.Generic;
using System.Linq;
using log4net.Repository;
using log4net;
using log4net.Appender;
using log4net.Config;
using KCB = Kowni.Common.BusinessLogic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Configuration;
using System.Text;
using VAMS.Code;
using VAMS.Common;

public class PageBase : Page
{
    KCB.BLMenu objMenu = new KCB.BLMenu();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (authentication.User.LoginStatus)
        {
            BuildMenu();
        }
        else
        {
            Response.Redirect("~/login.aspx", true);
        }
    }

    private void BuildMenu()
    {
        DataSet ds = new DataSet();
        if (UserSession.MenuDetails != null)
        {
            if (UserSession.MenuDetails.Tables.Count > 0 && UserSession.MenuDetails.Tables[0].Rows.Count > 0)
            {
                //ds = UserSession.MenuDetails;
                ds = objMenu.GetMenuItems(UserSession.RoleID, Convert.ToInt32(ConfigurationManager.AppSettings.Get("toolid")), UserSession.CompanyIDCurrent, UserSession.LanguageID);
                UserSession.MenuDetails = ds;
            }
            else
            {
                ds = objMenu.GetMenuItems(UserSession.RoleID, Convert.ToInt32(ConfigurationManager.AppSettings.Get("toolid")), UserSession.CompanyIDCurrent, UserSession.LanguageID);
                UserSession.MenuDetails = ds;
            }
        }
        else
        {
            ds = objMenu.GetMenuItems(UserSession.RoleID, Convert.ToInt32(ConfigurationManager.AppSettings.Get("toolid")), UserSession.CompanyIDCurrent, UserSession.LanguageID);
            UserSession.MenuDetails = ds;
        }

        IList<MenuManager.MenuItem> menuItems = new MenuManager(UserSession.MenuDetails).MenuItems;

        StringBuilder sbSubMenuItems = new StringBuilder();
        //sbSubMenuItems.Append("<ul>");


        foreach (MenuManager.MenuItem mi in menuItems)
        {
            //sbMainMenuItems.Append("<li><a href=\"" + ResolveUrl("~/" + mi.MasterFormName) + "\">" + mi.MasterFormText + "</a></li>");

            foreach (MenuManager.SubMenuGroup smg in mi.SubMenuGroup)
            {
                if (smg.SubMenuItem.Count > 0)
                {
                    //sbSubMenuItems.Append("<li><a href=\"" + ResolveUrl("~/" + mi.MasterFormName) + "\"><i class=" + mi.CssClass + "></i>" + mi.MasterFormText + "</a>");

                    sbSubMenuItems.Append(
                            "<li><a href='" + mi.MasterFormName + "'><i class='" + mi.CssClass + "'></i>" + mi.MasterFormText + "</a>");

                    sbSubMenuItems.Append("<ul>");
                    foreach (MenuManager.SubMenuItem smi in smg.SubMenuItem)
                    {
                        //sbSubMenuItems.Append(
                        //    "<li id='" + SettheMenuName(smi.SubFormName) + "' runat='server'><a title='" + smi.SubFormText + "'");
                        //sbSubMenuItems.Append(
                        //    "href='" + smi.SubFormName + "' runat='server'><i class='" + smi.CssClass + "'>");
                        //sbSubMenuItems.Append("" + smi.SubFormText + " </a></li>");


                        sbSubMenuItems.Append(
                            "<li><a id='" + SettheMenuName(smi.SubFormName) + "' href='" + smi.SubFormName + "'><i class='" + smi.CssClass + "'></i>" + smi.SubFormText + "</a></li>");
                    }
                    sbSubMenuItems.Append("</ul>");
                    sbSubMenuItems.Append("</li>");
                }
                else
                {
                    sbSubMenuItems.Append("<li><a href=\"" + ResolveUrl("~/" + mi.MasterFormName) + "\"><i class=" + mi.CssClass + "></i>" + mi.MasterFormText + "</a></li>");
                }

            }
        }

        //sbSubMenuItems.Append("</ul>");
        //sbMainMenuItems.Append("</ul>");

        try
        {
            //((HtmlGenericControl)Master.Master.FindControl("navigation")).InnerHtml = sbSubMenuItems.ToString().Trim();
            //((HtmlGenericControl)((ContentPlaceHolder)Master.Master.FindControl("navigation")).FindControl("dSubMenu")).InnerHtml = sbSubMenuItems.ToString().Trim();
            System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl("navigation");
            //li.InnerHtml = sbSubMenuItems.ToString().Trim();  //Need to un-comment when send to client
        }
        catch
        {
        }
    }

    protected void Getmainandsubform(string subformurl, out string mainform, out string subform)
    {
        mainform = string.Empty;
        subform = string.Empty;
        DataSet dsMenus = new DataSet();
        if (UserSession.MenuDetails != null)
        {
            if (UserSession.MenuDetails.Tables.Count > 0 && UserSession.MenuDetails.Tables[0].Rows.Count > 0)
            {
                dsMenus = UserSession.MenuDetails;
            }
            else
            {
                dsMenus = objMenu.GetMenuItems(UserSession.RoleID, Convert.ToInt32(ConfigurationManager.AppSettings.Get("toolid")), UserSession.CompanyIDCurrent, UserSession.LanguageID);
                UserSession.MenuDetails = dsMenus;
            }
        }
        else
        {
            dsMenus = objMenu.GetMenuItems(UserSession.RoleID, Convert.ToInt32(ConfigurationManager.AppSettings.Get("toolid")), UserSession.CompanyIDCurrent, UserSession.LanguageID);
            UserSession.MenuDetails = dsMenus;
        }

        if (dsMenus.Tables[0].Rows.Count > 0)
        {
            DataRow[] drmenu = dsMenus.Tables[0].Select("SubFormsPageName='" + subformurl + "'");
            if (drmenu.Length > 0)
            {
                mainform = drmenu[0]["MainFormsPageText"].ToString();
                subform = drmenu[0]["SubFormsPageText"].ToString();
            }
        }

    }

    private string SettheMainMenuName(string menuname)
    {
        string[] menunames = menuname.Split('.');
        string finalString = string.Empty;
        if (menunames.Length > 0)
        {
            finalString = "Mli" + menunames[0].ToLower();
        }
        else
        {
            finalString = "Mli" + menuname.ToLower();
        }

        return finalString;
    }

    private string SettheMenuName(string menuname)
    {
        string[] menunames = menuname.Split('.');
        string finalString = string.Empty;
        if (menunames.Length > 0)
        {
            finalString = "li" + menunames[0].ToLower();
        }
        else
        {
            finalString = "li" + menuname.ToLower();
        }

        return finalString;
    }



    protected override void OnPreLoad(EventArgs e)
    {
    }

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected override void OnError(EventArgs e)
    {
        try
        {
            //Log the error using log4net
            //ErrorHandling.OnError();
            //Response.Redirect("~/error.aspx", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    #region Page Render Timing

    // Page render performance fields.
    private DateTime _startTime = DateTime.Now;
    private TimeSpan _renderTime;

    /// <summary>
    /// Sets and gets the page render starting time. This property 
    /// represents the Template Design Pattern.
    /// </summary>
    public DateTime StartTime
    {
        set { _startTime = value; }
        get { return _startTime; }
    }

    /// <summary>
    /// Gets page render time. This property is virtual therefore getting the 
    /// page performance is overridable by derived pages. This property 
    /// represents the Template Design Pattern.
    /// </summary>
    public virtual string PageRenderTime
    {
        get
        {
            _renderTime = DateTime.Now - _startTime;
            return _renderTime.Seconds + "." + _renderTime.Milliseconds + " seconds";
        }
    }

    #endregion
}
