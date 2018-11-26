using System.Web;
using System.Web.Optimization;

namespace VAMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/JQuery").Include("~/Scripts/js/jquery-3.3.1.js"));
            //bundles.Add(new ScriptBundle("~/bundles/JQuery").Include("~/Scripts/query-2.2.4.js"));


            bundles.Add(new ScriptBundle("~/bundles/BootStrapJs").Include("~/Scripts/js/bootstrap.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/BootBox").Include("~/Scripts/js/bootbox.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/MultiSelect").Include("~/Scripts/js/bootstrap-multiselect.js"));
            bundles.Add(new ScriptBundle("~/bundles/CommonJs").Include("~/Scripts/NotificationMessage/Notification.js",
                "~/Scripts/Common/RolePermission.js",
                "~/Scripts/Common/UserProfile.js"));

            bundles.Add(new ScriptBundle("~/bundles/TemplateJs").Include(
               "~/Scripts/js/plugins.js",
               "~/Scripts/js/custom.js",
               "~/Scripts/js/jquery.smoothState.min.js",
               "~/Scripts/js/datetimepicker.min.js",
               "~/Scripts/js/scrollbar.js",
               "~/Scripts/js/select2.js")
                );


            bundles.Add(new ScriptBundle("~/bundles/DataTable").Include(
            "~/Scripts/DataTable/jquery.dataTables.min.js",
            "~/Scripts/DataTable/dataTables.buttons.min.js",
            "~/Scripts/DataTable/buttons.flash.min.js",
            "~/Scripts/DataTable/jszip.min.js",
            "~/Scripts/DataTable/pdfmake.min.js",
            "~/Scripts/DataTable/vfs_fonts.js",
            "~/Scripts/DataTable/buttons.html5.min.js",
            "~/Scripts/DataTable/buttons.print.min.js"
            ));

            bundles.Add(new StyleBundle("~/Content/Bootstrap").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/style.css"
            ));

            bundles.Add(new StyleBundle("~/Content/MultiSelect").Include(
            "~/Content/css/bootstrap-multiselect.css"
            ));

            bundles.Add(new StyleBundle("~/Content/DataTable").Include(
            "~/Content/css/DataTable/buttons.dataTables.min.css",
            "~/Content/css/DataTable/jquery.dataTables.min.css",
            "~/Content/css/DataTable/jquerys.dataTables.min.css"
            ));



        }
    }
}
