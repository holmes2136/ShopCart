using System;
using Vevo;
using Vevo.WebUI.BaseControls;
using Vevo.Deluxe.WebUI.Marketing;

public partial class Themes_ResponsiveGreen_Base : BaseMasterPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if ( KeyUtilities.IsTrialLicense() )
            uxTrialWarningPlaceHolder.Visible = true;
        else
            uxTrialWarningPlaceHolder.Visible = false;

        AffiliateHelper.SetAffiliateCookie( AffiliateCode );
    }
}
