using System;
using System.Web.Security;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Deluxe.WebUI.Marketing;

public partial class Themes_ResponsiveGreen_TwoColumnRight : Vevo.WebUI.BaseControls.BaseMasterPage
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
                MainDivCenter.Style.Add( "margin", "auto" );
                MainDivCenter.Style.Add( "float", "none" );

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
}
