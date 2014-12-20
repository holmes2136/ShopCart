using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Shared.DataAccess;
using Vevo.WebAppLib;

public partial class AdminAdvanced_Components_Menu_AdminMenu : AdminAdvancedBaseUserControl
{
    //private void AddMenuControl( int menuID, DataRow menuDetails, DataRow[] subMenuDetails, string effects, bool permission, MenuMode menuModeUse )
    //{
    //    AdminAdvancedBaseMenu userControlMenu = LoadControl( "MenuAccording.ascx" ) as AdminAdvancedBaseMenu;

    //    userControlMenu.ID = String.Format( "MenuID_{0:00}", menuID.ToString() );
    //    userControlMenu.MenuDetails = menuDetails;
    //    userControlMenu.SubMenuDetails = subMenuDetails;
    //    userControlMenu.Effects = effects;
    //    userControlMenu.PermissionMainMenu = permission;
    //    userControlMenu.MenuModeUse = menuModeUse;
    //    uxMenuPlaceHolder.Controls.Add( userControlMenu );
    //}

    //private void PopulateMenuItem()
    //{
    //    uxMenuPlaceHolder.Controls.Clear();
    //    DataTable fullTable = AdminMenuAdvancedAccessCache.Instance.GetAll(
    //        AdminConfig.CurrentCultureID, "SortOrder", FlagFilter.ShowTrue );

    //    DataTable table = AdminMenuAdvancedAccessCache.Instance.GetAllByAdminID(
    //        AdminConfig.CurrentCultureID,
    //        DataContext.AdminRepository.GetIDFromUserName( Page.User.Identity.Name ),
    //        "SortOrder",
    //        FlagFilter.ShowTrue );

    //    DataRow[] dataRows = fullTable.Select( "ParentMenuKey = '' OR ParentMenuKey is null", "SortOrder" );

    //    if (dataRows.Length > 0)
    //    {
    //        for (int i = 0; i < dataRows.Length; i++)
    //        {
    //            DataRow[] checkPermission = table.Select( String.Format( "MenuKey = '{0}'", dataRows[i]["MenuKey"] ) );
    //            DataRow[] subDataRows = table.Select( String.Format( "ParentMenuKey = '{0}'", dataRows[i]["MenuKey"] ), "SortOrder" );

    //            if (checkPermission.Length > 0)
    //                AddMenuControl( i, dataRows[i], subDataRows, "appear", true, MenuModeUse );
    //            else
    //            {
    //                if (subDataRows.Length > 0)
    //                    AddMenuControl( i, dataRows[i], subDataRows, "appear", false, MenuModeUse );
    //            }
    //        }

    //        DataRow[] helpDataRows = fullTable.Select( "MenuKey = 'HelpMain'", "SortOrder" );
    //        DataRow[] helpSubDataRows = fullTable.Select( String.Format( "ParentMenuKey = '{0}'", "HelpMain" ), "SortOrder" );

    //        AddMenuControl( dataRows.Length, helpDataRows[0], helpSubDataRows, "appear", true, MenuModeUse );
    //    }
    //}

    protected void Page_Load( object sender, EventArgs e )
    {
        //PopulateMenuItem();

        // If Use Frame code will be like this.
        // ------------------------------------------------------------------------
        // ------------------------------------------------------------------------
        // ------------------------------------------------------------------------
        //uxHideMenuImage.Attributes.Add( "onclick", "Effect.toggle('MenuShow','appear');Effect.toggle('MenuShowA','appear');Effect.toggle('MenuHide','appear');Effect.toggle('MenuHideA','appear');resizeMenu(18,'innerFr');return false;" );
        //uxShowMenuImage.Attributes.Add( "onclick", "Effect.toggle('MenuShow','appear');Effect.toggle('MenuShowA','appear');Effect.toggle('MenuHide','appear');Effect.toggle('MenuHideA','appear');resizeMenu(200,'innerFr');return false;" );
        //uxHideMenuImage.Attributes.Add( "onclick", "hideMenu();resizeMenu(18,'innerFr');return false;" );
        //uxShowMenuImage.Attributes.Add( "onclick", "showMenu();resizeMenu(200,'innerFr');return false;" );
        //uxShowMenuTextImage.Attributes.Add( "onclick", "showMenu();resizeMenu(200,'innerFr');return false;" );
        // ------------------------------------------------------------------------
        // ------------------------------------------------------------------------
        // ------------------------------------------------------------------------

        uxHideMenuImage.Attributes.Add( "onclick", "hideMenu();return false;" );
        uxShowMenuImage.Attributes.Add( "onclick", "showMenu();return false;" );
        uxShowMenuTextImage.Attributes.Add( "onclick", "showMenu();return false;" );
    }

    public MenuMode MenuModeUse
    {
        get
        {
            if (ViewState["MenuMode"] == null)
                return MenuMode.Image;
            else
                return (MenuMode) ViewState["MenuMode"];
        }
        set
        {
            ViewState["MenuMode"] = value;
        }
    }
}
