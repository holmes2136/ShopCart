using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Shared.DataAccess;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;
using Vevo.WebUI.DataAccessCache;

public partial class AdminAdvanced_Components_Menu_AdminTopMenu : AdminAdvancedBaseListControl
{
    private DataTable _fullMenuTable = null;

    private string[] excludeMenus = { "QuickBooks", "EBayListing", "EBayTemplate" , "PromotionGroup" };
    private string[] excludeParentMenus = { "AffiliateMain" };

    private bool IsDisabledMenu( string MenuKey, string[] excludeMenus )
    {
        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
        {
            foreach (string key in excludeMenus)
            {
                if (MenuKey == key)
                    return true;
            }
        }
        return false;
    }

    private DataTable FullMenuTable
    {
        get
        {
            if (_fullMenuTable == null)
                _fullMenuTable = AdminMenuAdvancedAccessCache.Instance.GetAll(
                AdminConfig.CurrentCultureID,
                "SortOrder",
                FlagFilter.ShowTrue );
            return _fullMenuTable;
        }
    }

    private MenuItem GetMenuItem( string menuKey, bool viewMode )
    {
        MenuItem item = new MenuItem();
        DataRow[] dataRows = FullMenuTable.Select( "MenuKey = '" + menuKey + "'", "SortOrder" );
        if (dataRows.Length > 0)
        {
            item.Text = dataRows[0]["MenuName"].ToString();

            UrlPath url = new UrlPath( Request.Url.AbsoluteUri );
            Regex regex = new Regex( ".ascx", RegexOptions.IgnoreCase );
            string pageName = String.Format( "#{0}", regex.Replace( dataRows[0]["PageName"].ToString(), "," ) );
            if (viewMode)
                item.NavigateUrl = String.Format( "~/{0}/default.aspx{1}", url.ExtractFirstApplicationSubfolder(), pageName );
        }

        return item;
    }

    private MenuItem createHelpChildItem( string menuName, string navigateUrl )
    {
        MenuItem item = new MenuItem();

        item.Text = menuName;
        item.NavigateUrl = navigateUrl;
        item.Target = "_blank";

        return item;
    }

    private void PopulateControls()
    {
        uxContentMenuNavListTop.Controls.Clear();

        DataTable menuTable = AdminMenuAdvancedAccessCache.Instance.GetAllByAdminID(
            AdminConfig.CurrentCultureID,
            DataAccessContext.AdminRepository.GetIDFromUserName( Page.User.Identity.Name ),
            "SortOrder",
            FlagFilter.ShowAll );

        DataRow[] parentRows = menuTable.Select( "ParentMenuKey = '' OR ParentMenuKey is null", "SortOrder" );

        if (parentRows.Length > 0)
        {
            uxContentMenuNavListTop.Items.Add( GetMenuItem( "DefaultMain", true ) );
            for (int i = 0; i < parentRows.Length; i++)
            {
                if (!IsDisabledMenu( parentRows[i]["MenuKey"].ToString(), excludeParentMenus ))
                {
                    bool parentViewMode = ConvertUtilities.ToBoolean( parentRows[i]["ViewMode"] );
                    MenuItem parentItem = GetMenuItem( parentRows[i]["MenuKey"].ToString(), parentViewMode );

                    DataRow[] childRows = menuTable.Select( "ParentMenuKey = '" + parentRows[i]["MenuKey"].ToString() + "' ", "SortOrder" );
                    if (childRows.Length > 0)
                    {
                        for (int j = 0; j < childRows.Length; j++)
                        {
                            bool viewPermission = ConvertUtilities.ToBoolean( childRows[j]["ViewMode"] );
                            if (viewPermission && !IsDisabledMenu( childRows[j]["MenuKey"].ToString(), excludeMenus ))
                            {

                                MenuItem childrenItem = GetMenuItem( childRows[j]["MenuKey"].ToString(), viewPermission );
                                if (childRows[j]["MenuKey"].ToString().CompareTo("News") != 0)
                                    parentItem.ChildItems.Add( childrenItem );
                            }
                        }
                    }

                    if (parentItem.ChildItems.Count == 0 && !parentViewMode)
                        continue;

                    uxContentMenuNavListTop.Items.Add( parentItem );
                    uxContentMenuNavListTop.Orientation = Orientation.Horizontal;
                }
            }

        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (Request.UserAgent.IndexOf( "AppleWebKit" ) > 0)
        {
            Request.Browser.Adapters.Clear();
        }

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    protected override void RefreshGrid()
    {
        throw new Exception( "The method or operation is not implemented." );
    }
}
