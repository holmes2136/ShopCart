using System;
using System.Web.Security;
using System.Web.UI;
using Vevo;
using Vevo.Deluxe.WebUI.Marketing;
using Vevo.Domain;

public partial class Themes_ResponsiveGreen_Front : Vevo.WebUI.BaseControls.BaseMasterPage
{
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
                uxLeft.Visible = false;
                uxRight.Visible = false;
                MainDivCenter.Style.Add( "margin", "auto" );
                MainDivCenter.Style.Add( "float", "none" );

                if (IsRestrictedAccessPage())
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
            }
            else
            {
                uxLeft.Visible = true;
                uxRight.Visible = true;
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
}
