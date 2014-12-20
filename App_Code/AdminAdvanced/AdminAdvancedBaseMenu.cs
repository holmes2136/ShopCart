using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Vevo
{
    /// <summary>
    /// Summary description for IAdminAdvancedBaseMenu
    /// </summary>
    public class AdminAdvancedBaseMenu : AdminAdvancedBaseUserControl
    {
        private DataRow _menuDetails;
        private DataRow[] _subMenuDetails;
        private string _effects;
        private bool _permissionMainMenu;
        private MenuMode _menuMode;

        public DataRow MenuDetails { get { return _menuDetails; } set { _menuDetails = value; } }
        public DataRow[] SubMenuDetails { get { return _subMenuDetails; } set { _subMenuDetails = value; } }
        public string Effects { get { return _effects; } set { _effects = value; } }
        public bool PermissionMainMenu { get { return _permissionMainMenu; } set { _permissionMainMenu = value; } }
        public MenuMode MenuModeUse { get { return _menuMode; } set { _menuMode = value; } }
    }
}