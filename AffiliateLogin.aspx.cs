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
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.WebAppLib;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.Marketing;

public partial class AffiliateLogin : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        WebUtilities.TieLoginControlImageButton( this.Page, uxLogin );
        uxContentLayout.ContentName = "Affiliate";
        uxContentLayout.ContentID = DataAccessContext.ContentRepository.GetContentIDFromUrlName( "Affiliate" );
    }

    protected void uxLogin_LoggedIn( object sender, EventArgs e )
    {
        string returnUrl = Request.QueryString["ReturnUrl"].ToString();

        if (string.IsNullOrEmpty( returnUrl ))
            returnUrl = "AffiliateDashboard.aspx";

        Response.Redirect( returnUrl );
    }

    protected void uxLogin_Error( object sender, EventArgs e )
    {
        if (Membership.GetUser( uxLogin.UserName ) != null)
        {
            if (Membership.GetUser( uxLogin.UserName ).IsLockedOut == true)
                Membership.GetUser( uxLogin.UserName ).UnlockUser();
        }
    }

    protected void uxLogin_LoggingIn( object sender, LoginCancelEventArgs e )
    {
        if (Roles.IsUserInRole( uxLogin.UserName, "Affiliates" ))
        {
            string affiliateCode = DataAccessContextDeluxe.AffiliateRepository.GetCodeFromUserName( uxLogin.UserName );
            Affiliate affiliate = DataAccessContextDeluxe.AffiliateRepository.GetOne( affiliateCode );

            if (affiliate.IsEnabled)
            {
                uxMessage.DisplayError( "[$LoginFailureMessage]" );
            }
            else
            {
                uxMessage.DisplayError( "[$LoginDisabledMessage]" );
                e.Cancel = true;
            }
        }
        else
        {
            uxMessage.DisplayError( "[$NotAffiliateAccount]" );
            e.Cancel = true;
        }
    }
}
