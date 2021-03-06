﻿using System;
using System.Web.Security;
using System.Web.UI;
using Vevo.Deluxe.WebUI.Marketing;
using Vevo.Domain;
using Vevo.WebUI.BaseControls;

public partial class Themes_ResponsiveGreen_Search : BaseMasterPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if ( DataAccessContext.Configurations.GetBoolValue( "RestrictAccessToShop" ) )
        {
            if ( !Page.User.Identity.IsAuthenticated )
            {
                MainDivCenter.Style.Add( "margin", "auto" );
                MainDivCenter.Style.Add( "float", "none" );

                if ( IsRestrictedAccessPage() )
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
            }
        }

        if ( DataAccessContext.Configurations.GetBoolValue( "PriceRequireLogin" ) )
        {
            if ( !Page.User.Identity.IsAuthenticated )
            {
                uxPriceRequireLoginPanel.Visible = true;
            }
        }

        AffiliateHelper.SetAffiliateCookie( AffiliateCode );
    }
}
