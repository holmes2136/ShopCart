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
using Vevo.DataAccessLib;
using Vevo.WebUI.DataAccessCache;
using Vevo.WebUI.ServerControls;

public partial class AdminAdvanced_Components_Common_SiteMap : AdminAdvancedBaseUserControl
{
    int _sitemapCount = 0;

    private AdvancedLinkButton MenuButtonLink(
        string menuID,
        string menuText,
        string menuDescription,
        string menuUrl,
        string menuParameter,
        string cssClassBegin,
        string cssClassEnd,
        string cssClass )
    {
        AdvancedLinkButton newMenu = new AdvancedLinkButton();
        newMenu.ID = menuID;
        newMenu.Text = menuText;
        newMenu.ToolTip = menuDescription;

        newMenu.CssClassBegin = cssClassBegin;
        newMenu.CssClassEnd = cssClassEnd;
        newMenu.CssClass = cssClass;
        if (!String.IsNullOrEmpty( menuUrl ))
        {
            newMenu.PageName = menuUrl;
            newMenu.PageQueryString = menuParameter;
        }
        newMenu.Attributes.Add( "onclick", "resetScrollCordinate();" );
        newMenu.Click += new EventHandler( ChangePage_Click );
        return newMenu;
    }

    private Label Menulabel( string menuId, string menuText, string menuDescription, string cssClass )
    {
        Label newMenu = new Label();
        newMenu.ID = menuId;
        newMenu.Text = menuText;
        newMenu.ToolTip = menuDescription;
        newMenu.CssClass = cssClass;
        return newMenu;
    }

    private DataTable SiteMapData
    {
        get
        {
            DataTable table = AdminMenuPageAccessCache.Instance.GetAll( AdminConfig.CurrentCultureID );
            return table;
        }
    }

    private void AddSeparate()
    {
        uxSiteMapPlaceHolder.Controls.Add(
            Menulabel( 
                string.Format( "separate_{0}", uxSiteMapPlaceHolder.Controls.Count ), 
                " >> ", 
                "",
                "SeperatorStyle" ) );
    }

    private string GetPageName( string navigateUrl )
    {
        DataRow[] datarows = SiteMapData.Select(
            String.Format( "MenuPageName = '{0}'", navigateUrl ), "MenuPagename" );

        if (datarows.Length > 0)
            return String.Format( "{0}", datarows[0]["BreadcrumbName"] );
        else
            return String.Empty; //"No Name";
    }

    private void CheckSiteMap( string navigateUrl )
    {
        string parentMenuPageName = String.Empty;

        DataTable siteMapSource = new DataTable( "SiteMapTable" );
        siteMapSource.Columns.Add( "SiteMapID", typeof( string ) );
        siteMapSource.Columns.Add( "BreadcrumbName", typeof( string ) );
        siteMapSource.Columns.Add( "MenuDescription", typeof( string ) );
        siteMapSource.Columns.Add( "MenuPageName", typeof( string ) );
        siteMapSource.Columns.Add( "UrlParameter", typeof( string ) );

        DataRow[] datarows = SiteMapData.Select(
            String.Format( "MenuPageName = '{0}'", navigateUrl ),
            "MenuPageName" );

        if (datarows.Length > 0)
        {
            parentMenuPageName = string.Format( "{0}", datarows[0]["ParentMenuPageName"] );
            if (!String.IsNullOrEmpty( parentMenuPageName ))
            {
                do
                {
                    DataRow[] subDatarows = SiteMapData.Select( string.Format( "MenuPageName = '{0}'", parentMenuPageName ), "MenuPageName" );
                    if (subDatarows.Length > 0)
                    {
                        parentMenuPageName = string.Format( "{0}", subDatarows[0]["ParentMenuPageName"] );
                        DataRow row;
                        row = siteMapSource.NewRow();
                        row["SiteMapID"] = string.Format( "{0}", _sitemapCount++ );
                        row["BreadcrumbName"] = string.Format( "{0}", subDatarows[0]["BreadcrumbName"] );
                        row["MenuDescription"] = string.Format( "{0}", subDatarows[0]["MenuDescription"] );
                        row["MenuPageName"] = string.Format( "{0}", subDatarows[0]["MenuPageName"] );
                        if (!String.IsNullOrEmpty( String.Format( "{0}", subDatarows[0]["ReliantOn"] ) ))
                            row["UrlParameter"] = MainContext.RawQueryString;

                        siteMapSource.Rows.Add( row );

                    }
                } while (!String.IsNullOrEmpty( parentMenuPageName ));
            }
        }

        if (siteMapSource.Rows.Count > 0)
        {
            datarows = siteMapSource.Select( "", "SiteMapID DESC" );
            foreach (DataRow row in datarows)
            {
                AddSeparate();
                uxSiteMapPlaceHolder.Controls.Add(
                    MenuButtonLink(
                        string.Format( "{0}", row["SiteMapID"] ),
                        string.Format( "{0}", row["BreadcrumbName"] ),
                        string.Format( "{0}", row["MenuDescription"] ),
                        string.Format( "{0}", row["MenuPageName"] ),
                        string.Format( "{0}", row["UrlParameter"] ), "fl", "", "LinkStyle" ) );
            }
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxSiteMapPlaceHolder.Controls.Clear();
       
        if (MainContext.LastControl.ToLower() != "default.ascx")
        {
            uxSiteMapList.Visible = true;
            uxSiteMapPlaceHolder.Controls.Add( MenuButtonLink( "SiteMapHome", "Home", "Home", "Default.ascx", "", "fl", "", "HomeLinkStyle LinkStyle" ) );
            CheckSiteMap( MainContext.LastControl );
            AddSeparate();
            uxSiteMapPlaceHolder.Controls.Add( Menulabel( "SiteMapThisPage", GetPageName( MainContext.LastControl ), "", "DefaultStyle" ) );
        }
    }
}
