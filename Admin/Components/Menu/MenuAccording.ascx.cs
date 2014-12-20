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
using Vevo.DataAccessLib;
using Vevo.WebAppLib;
using Vevo.WebUI.ServerControls;

public partial class AdminAdvanced_Components_Menu_MenuAccording : AdminAdvancedBaseMenu
{
    private AdvancedLinkButton MenuButtonLink( string menuID, string menuText, string menuClass, string menuDescription, string menuUrl )
    {
        AdvancedLinkButton newMenu = new AdvancedLinkButton();
        newMenu.ID = menuID;
        if (MenuModeUse == MenuMode.Text)
            newMenu.Text = menuText;
        else
        {
            newMenu.Text = "&nbsp;";
            newMenu.CssClass = menuClass + " MenuBackGroundPosition";
        }
        newMenu.StatusBarText = menuText;

        newMenu.ToolTip = menuDescription;

        newMenu.CssClassBegin = "SubSideMenuLeft";
        newMenu.CssClassEnd = "SubSideMenuRight";
        if (!String.IsNullOrEmpty( menuUrl ))
        {
            newMenu.PageName = menuUrl;
            newMenu.PageQueryString = "";// menuUrl;
        }
        newMenu.Click += new EventHandler( ChangePage_Click );
        newMenu.Attributes.Add( "onclick", "resetScrollCordinate();" );
        return newMenu;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (MenuModeUse == MenuMode.Text)
            uxMenuTitleLinkButton.Text = String.Format( "{0}", MenuDetails["MenuName"] );
        else
        {
            uxMenuTitleLinkButton.Text = "&nbsp;";
            uxMenuTitleLinkButton.CssClass = String.Format( "{0}", MenuDetails["MenuClass"] );
            uxMenuTitleLinkButton.Width = new Unit( 153 );
        }

        if (SubMenuDetails.Length == 0)
        {
            uxMenuTitleLinkButton.StatusBarText = String.Format( "{0}", MenuDetails["MenuName"] );
        }

        if (!String.IsNullOrEmpty( String.Format( "{0}", MenuDetails["PageName"] ) ))
        {
            if (PermissionMainMenu)
            {
                if (SubMenuDetails.Length == 0)
                {
                    uxMenuTitleLinkButton.PageName = String.Format( "{0}", MenuDetails["PageName"] );
                    uxMenuTitleLinkButton.PageQueryString = "";
                    if (SubMenuDetails.Length == 0)
                        uxMenuTitleLinkButton.Click += new EventHandler( ChangePage_Click );
                }
            }
        }

        if (SubMenuDetails.Length > 0)
        {
            for (int i = 0; i < SubMenuDetails.Length; i++)
            {
                AdvancedLinkButton x = MenuButtonLink( String.Format( "uxSubMenu{0}_{1}", MenuDetails["MenuKey"], i ),
                    String.Format( "{0}", SubMenuDetails[i]["MenuName"] ),
                    String.Format( "{0}", SubMenuDetails[i]["MenuClass"] ),
                    String.Format( "{0}", SubMenuDetails[i]["MenuDescription"] ),
                    String.Format( "{0}", SubMenuDetails[i]["PageName"] ) );
                uxMenuPanel.Controls.Add( x );

                UpdatePanel myUpdatePanel = (UpdatePanel) WebUtilities.FindControlRecursive( this.Page, "uxContentUpdatePanel" );
                AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
                trigger.ControlID = x.UniqueID;
                myUpdatePanel.Triggers.Add( trigger );
            }

            if (PermissionMainMenu)
            {
                uxMenuTitleLinkButton.CustomAddOnClickScript = String.Format( "ShowAccording('{0}','{1}','{2}');return false;", uxShowImage.ClientID, uxHideImage.ClientID, uxMenuPanel.ClientID );
                uxMenuTitleLinkButton.DisablePostBack = true;
            }
            else
            {
                uxMenuTitleLinkButton.Attributes.Add( "onclick", String.Format( "showHideImage('{0}','{1}','{2}');", uxShowImage.ClientID, uxHideImage.ClientID, uxMenuPanel.ClientID ) );
            }

            uxShowImage.Attributes.Add( "onclick", String.Format( "showHideImage('{0}','{1}','{2}');", uxShowImage.ClientID, uxHideImage.ClientID, uxMenuPanel.ClientID ) );
            uxHideImage.Attributes.Add( "onclick", String.Format( "showHideImage('{0}','{1}','{2}');", uxShowImage.ClientID, uxHideImage.ClientID, uxMenuPanel.ClientID ) );

            uxMenuPanel.Style["display"] = "none";
            uxShowImage.Style["display"] = "none";
        }
        else
        {
            uxMenuPanel.Style["font-size"] = "0px";
            uxShowImage.Visible = false;
            uxHideImage.Visible = false;
        }

        UpdatePanel myTitleUpdatePanel = (UpdatePanel) WebUtilities.FindControlRecursive( this.Page, "uxContentUpdatePanel" );
        AsyncPostBackTrigger titleTrigger = new AsyncPostBackTrigger();
        titleTrigger.ControlID = uxMenuTitleLinkButton.UniqueID;
        myTitleUpdatePanel.Triggers.Add( titleTrigger );
    }
}
