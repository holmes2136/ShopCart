using System;
using System.Web.Security;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.WebUI;

public partial class Components_HeaderMenu : Vevo.WebUI.BaseControls.BaseLayoutControl
{
    private bool IsUserRestrictAccessToShop
    {
        get
        {
            if (DataAccessContext.Configurations.GetBoolValue( "RestrictAccessToShop" ) && !Page.User.Identity.IsAuthenticated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private void PopulateMenu()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "CategoryDynamicDropDownDisplay" ) && !IsUserRestrictAccessToShop)
        {
            uxCatelogMenu.Visible = false;
            uxCategoryDynamicDropDownMenu.Visible = true;
        }
        else
        {
            uxCatelogMenu.Visible = true;
            uxCatelogDropDownMenu.Visible = false;
        }

        if (DataAccessContext.Configurations.GetBoolValue( "DepartmentHeaderMenuDisplay" ))
        {
            if (DataAccessContext.Configurations.GetBoolValue( "DepartmentDynamicDropDownDisplay" ) && !IsUserRestrictAccessToShop)
            {
                uxDepartmentMenu.Visible = false;
                uxDepartmentDynamicDropDownMenu.Visible = true;
            }
            else
            {
                uxDepartmentMenu.Visible = true;
                uxDepartmentDropDownMenu.Visible = false;
            }
        }
        else
        {
            uxDepartmentMenu.Visible = false;
            uxDepartmentDropDownMenu.Visible = false;
        }

        if (DataAccessContext.Configurations.GetBoolValue( "ManufacturerHeaderMenuDisplay" ))
        {
            if (DataAccessContext.Configurations.GetBoolValue( "ManufacturerDynamicDropDownDisplay" ) && !IsUserRestrictAccessToShop)
            {
                uxManufacturerMenu.Visible = false;
                uxManufacturerDynamicDropDownMenu.Visible = true;
            }
            else
            {
                uxManufacturerMenu.Visible = true;
                uxManufacturerDropDownMenu.Visible = false;
            }
        }
        else
        {
            uxManufacturerMenu.Visible = false;
            uxManufacturerDropDownMenu.Visible = false;
        }

        if (Roles.IsUserInRole( "Affiliates" ))
            uxMyAccountLink.NavigateUrl = "~/affiliatedashboard.aspx";
        else
            uxMyAccountLink.NavigateUrl = "~/accountdashboard.aspx";

        uxContentMenu.Visible = IsTopContentMenuDisplay();
        uxBlogMenu.Visible = DataAccessContext.Configurations.GetBoolValue( "BlogEnabled" );
    }

    private bool IsTopContentMenuDisplay()
    {
        string id = DataAccessContext.Configurations.GetValue( "TopContentMenu" );

        ContentMenu contentMenu = DataAccessContext.ContentMenuRepository.GetOne( id );

        return contentMenu.IsEnabled;

    }
    private string GetNavigateUrl( String name )
    {
        Content content = DataAccessContext.ContentRepository.GetOne( StoreContext.Culture,
           DataAccessContext.ContentRepository.GetIDByName( StoreContext.Culture, name ) );
        if (!content.IsNull)
            return UrlManager.GetContentUrl( content.ContentID, content.UrlName );
        else
            return UrlManager.GetContentUrl( "0", name );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        PopulateMenu();
        uxContentMenuNavList.Position = "top";
        uxCategoryDynamicDropDownMenu.RootMenuName = "[$Products]";
        uxDepartmentDynamicDropDownMenu.RootMenuName = "[$Department]";
        uxManufacturerDynamicDropDownMenu.RootMenuName = "[$Manufacturer]";
    }
}
