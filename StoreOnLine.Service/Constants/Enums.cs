namespace StoreOnLine.Service.Constants
{
    public sealed class Enums
    {
        #region config.xml

        public static readonly string ConnectionStrings = "StoreOnLineContext";

        #endregion

        #region Table

        public static readonly string TableLocalizeProperty = "LocalizeProperties";

        #endregion

        #region roles

        public static readonly string Administrators = "Administrators";
        public static readonly string Contentadministrators = "Content Administrators";
        public static readonly string Allusers = "All Users";
        public static readonly string ViewRoles = "All Users";
        public static readonly string PrintRoles = "All Users";
        public static readonly string EditRoles = "Administrators; Content Administrators";
        public static readonly string CreateRoles = "Administrators; Content Administrators";

        #endregion

        public static readonly string ErrorDeleteEntity = "There was a problem while delete {0}";

        #region DbType

        public static readonly string DbTypeInt = "Int";
        public static readonly string DbTypeBool = "Bool";
        public static readonly string DbTypeDouble = "Double";
        public static readonly string DbTypeString = "String";

        #endregion

        #region javascritp
        //javasctript
        public static readonly string JsjQuery = "/Scripts/jquery-1.5.1.min.js";
        public static readonly string JsjQueryValidateUnobtrusive = "/Scripts/jquery.validate.unobtrusive.min.js";
        public static readonly string JsjQueryValidate = "/Scripts/jquery.validate.min.js";
        public static readonly string JsUi = "/Scripts/jquery-ui-1.8.23.custom.min.js";
        public static readonly string JseDialog = "/Scripts/eDialog/edialog.js";
        public static readonly string JsEditable = "/Scripts/updateinline/jquery.jeditable.mini.js";
        public static readonly string JsaJaxUnobtrusive = "/Scripts/jquery.unobtrusive-ajax.min.js";
        public static readonly string JsTip = "/Scripts/atooltip/jquery.atooltip.js";
        public static readonly string JssTip = "/Scripts/atooltip/atooltip.css";
        public static readonly string JssUi = "/Content/themes/base/jquery.ui.all.css";
        public static readonly string JssOverideContent = "/Themes/override_content.css";
        public static readonly string JsTiny = "/Scripts/tiny_mce/tiny_mce.js";
        public static readonly string JsTinyInitial = "/Scripts/tiny_mce/tiny_mce_init.js";
        public static readonly string JsaJaxBusy = "/Scripts/site/ajaxbusy.js";
        #endregion

        #region Sessions

        public static readonly string EmployerOnLine = "EmployerOnLine";

        #endregion

        #region Roles

        public static readonly string RolEnSuperAdmin = "SuperAdmin";
                
        #endregion
    }
}
