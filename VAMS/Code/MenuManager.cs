using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace VAMS.Code
{
    public class MenuManager
    {
        public IList<MenuItem> MenuItems { get; set; }
        public MenuManager(DataSet dsMenuDetails)
        {
            MenuItems = GetMenuItems(dsMenuDetails);
        }

        private IList<MenuItem> GetMenuItems(DataSet dsMenuDetails)
        {
            IList<MenuItem> lstMainMenuItem = new List<MenuItem>();
            try
            {
                string currentFormName = new FileInfo(HttpContext.Current.Request.Url.AbsolutePath.ToString().Trim().ToLower()).Name;

                bool current = false;
                //get all the main forms
                string masterFormName = string.Empty, masterFormText = string.Empty, dummyMasterFormName = string.Empty;
                foreach (DataRow dr in dsMenuDetails.Tables[0].Select("IsVisible = 1"))
                {
                    masterFormName = dr["MainFormsPageName"].ToString().Trim();
                    masterFormText = dr["MainFormsCustomText"].ToString().Trim() != string.Empty ? dr["MainFormsCustomText"].ToString().Trim() : dr["MainFormsPageTextLang"].ToString().Trim() != string.Empty ? dr["MainFormsPageTextLang"].ToString().Trim() : dr["MainFormsPageText"].ToString().Trim();
                    if (dummyMasterFormName.Trim() != masterFormName.Trim())
                    {
                        MenuItem mmi = new MenuItem();
                        mmi.MasterFormName = dr["MainFormsPageName"].ToString().Trim();
                        mmi.MasterFormText = masterFormText.Trim();
                        mmi.CssClass = dr["cssClass"].ToString().Trim();

                        mmi.SubMenuGroup = GetSubMenuGroupItems(dsMenuDetails, currentFormName, mmi.MasterFormName, out current);
                        //mmi.Current = current;
                        mmi.Current = Convert.ToBoolean(dr["IsVisible"].ToString().Trim());
                        lstMainMenuItem.Add(mmi);
                    }
                    dummyMasterFormName = masterFormName.Trim();

                }
            }
            catch { }

            return lstMainMenuItem;
        }

        private IList<SubMenuGroup> GetSubMenuGroupItems(DataSet dsMenuDetails, string currentFormName, string mainFormName, out bool current)
        {
            current = false;
            bool currentDummy = false;
            IList<SubMenuGroup> lstSubMenuGroup = new List<SubMenuGroup>();

            string dummySubGroupName = string.Empty, subGroupName = string.Empty;
            //get all the sub forms groups
            foreach (DataRow dr in dsMenuDetails.Tables[0].Select("IsVisible = 1 and MainFormsPageName = '" + mainFormName.ToLower().Trim() + "'"))
            {
                subGroupName = dr["SubFormsGroupsName"].ToString().Trim();
                if (dummySubGroupName.Trim() != subGroupName.Trim())
                {
                    SubMenuGroup smg = new SubMenuGroup();
                    smg.SubMenuGroupName = dr["SubFormsGroupsName"].ToString().Trim();

                    smg.SubMenuItem = GetSubMenuItems(dsMenuDetails, currentFormName, mainFormName, smg.SubMenuGroupName, out currentDummy);
                    if (currentDummy)
                        current = currentDummy;

                    smg.Current = currentDummy;
                    lstSubMenuGroup.Add(smg);
                }

                dummySubGroupName = subGroupName;
            }

            return lstSubMenuGroup;
        }

        private IList<SubMenuItem> GetSubMenuItems(DataSet dsMenuDetails, string currentFormName, string mainFormName, string groupName, out bool current)
        {
            current = false;

            IList<SubMenuItem> lstSubMenuItems = new List<SubMenuItem>();
            //get all the sub forms
            string subFormName = string.Empty, subFormText = string.Empty, dummySubFormName = string.Empty;
            foreach (DataRow dr in dsMenuDetails.Tables[0].Select("IsVisible = 1 and MainFormsPageName = '" + mainFormName.ToLower().Trim() + "' and SubFormsGroupsName = '" + groupName.ToLower().Trim() + "'"))
            {
                subFormName = dr["SubFormsPageName"].ToString().Trim();
                if (dummySubFormName.Trim() != subFormName.Trim())
                {
                    SubMenuItem smi = new SubMenuItem();
                    smi.SubFormName = dr["SubFormsPageName"].ToString().Trim();
                    smi.SubFormText = dr["SubFormsCustomText"].ToString().Trim() != string.Empty ? dr["SubFormsCustomText"].ToString().Trim() : dr["SubFormsPageTextLang"].ToString().Trim() != string.Empty ? dr["SubFormsPageTextLang"].ToString().Trim() : dr["SubFormsPageText"].ToString().Trim();
                    smi.CssClass = dr["SubFormCssClass"].ToString().Trim();

                    if (!current)
                        current = smi.Current = currentFormName.Trim() == dr["SubFormsPageName"].ToString().ToLower().Trim() ? true : false;

                    lstSubMenuItems.Add(smi);
                }
                dummySubFormName = subFormName.Trim();
            }

            return lstSubMenuItems;
        }

        public class MenuItem
        {
            public string MasterFormName { get; set; }
            public string MasterFormText { get; set; }
            public string CssClass { get; set; }
            public bool Current { get; set; }
            public IList<SubMenuGroup> SubMenuGroup { get; set; }
        }

        public class SubMenuGroup
        {
            public string SubMenuGroupName { get; set; }
            public string CssClass { get; set; }
            public bool Current { get; set; }
            public IList<SubMenuItem> SubMenuItem { get; set; }
        }

        public class SubMenuItem
        {
            public string SubFormName { get; set; }
            public string SubFormText { get; set; }
            public string CssClass { get; set; }
            public string SubFormId { get; set; }
            public bool Current { get; set; }
            public PagePermission PagePermission { get; set; }
        }

        public class PagePermission
        {
            public bool Full { get; set; }
            public bool New { get; set; }
            public bool Edit { get; set; }
            public bool Delete { get; set; }
            public bool View { get; set; }
        }
    }
}