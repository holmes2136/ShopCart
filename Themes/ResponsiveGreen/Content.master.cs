using System;
using System.Web.Security;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.WebUI.BaseControls;
using Vevo.Deluxe.WebUI.Marketing;

public partial class Themes_ResponsiveGreen_Content : BaseMasterPage
{
    private bool IsDisplayRightLayout
    {
        get
        {
            string rightMenuID = DataAccessContext.Configurations.GetValue( "RightContentMenu" );
            ContentMenu rightMenu = DataAccessContext.ContentMenuRepository.GetOne( rightMenuID );
            uxRight.Visible = rightMenu.IsEnabled;
            uxContentRightDiv.Visible = rightMenu.IsEnabled;
            return rightMenu.IsEnabled;
        }
    }

    private bool IsDisplayLeftLayout
    {
        get
        {
            string leftMenuID = DataAccessContext.Configurations.GetValue( "LeftContentMenu" );
            ContentMenu leftMenu = DataAccessContext.ContentMenuRepository.GetOne( leftMenuID );
            uxLeft.Visible = leftMenu.IsEnabled;
            uxContentLeftDiv.Visible = leftMenu.IsEnabled;
            return leftMenu.IsEnabled;
        }
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        if (KeyUtilities.IsTrialLicense())
            uxTrialWarningPlaceHolder.Visible = true;
        else
            uxTrialWarningPlaceHolder.Visible = false;

        if (DataAccessContext.Configurations.GetBoolValue( "RestrictAccessToShop" ))
        {
            if (!Page.User.Identity.IsAuthenticated)
            {
                uxContentLeftDiv.Visible = false;
                uxContentRightDiv.Visible = false;
                uxContentCenterDiv.Style.Add( "margin", "auto" );
                uxContentCenterDiv.Style.Add( "float", "none" );

                if (IsRestrictedAccessPage())
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
            }

        }

        if (DataAccessContext.Configurations.GetBoolValue( "PriceRequireLogin" ))
        {
            if (!Page.User.Identity.IsAuthenticated)
            {
                uxPriceRequireLoginPanel.Visible = true;
            }
        }

        AffiliateHelper.SetAffiliateCookie( AffiliateCode );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsDisplayLeftLayout && !IsDisplayRightLayout)
        {
            uxContentCenterDiv.Attributes.Add( "Class", "" );
        }
        else if (IsDisplayLeftLayout && IsDisplayRightLayout)
        {
            uxContentCenterDiv.Attributes.Add( "Class", "common-center-col leftrightcol columns" );
        }
    }
}
