using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Users;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Domain.Stores;

public partial class Mobile_UserLogin : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private void SetUpRegisterLink( HyperLink uxRegisterLink )
    {
        uxRegisterLink.NavigateUrl = "Register.aspx";

        if (Request.QueryString["ReturnUrl"] != null)
        {
            string returnUrl = Request.QueryString["ReturnUrl"].ToString();

            if (returnUrl.ToLower().Contains( "checkout.aspx" ))
            {
                uxRegisterLink.NavigateUrl += "?ReturnUrl=Shipping.aspx&Checkout=1";
            }
            else
                uxRegisterLink.NavigateUrl += "?ReturnUrl=" + Server.UrlEncode( returnUrl );
        }

    }

    protected void Page_Load( object sender, EventArgs e )
    {
        WebUtilities.TieLoginControlImageButton( this.Page, uxLogin );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }

    protected void uxLogin_LoggedIn( object sender, EventArgs e )
    {
        if (Request.QueryString["ReturnUrl"] != null)
        {
            string returnUrl = Request.QueryString["ReturnUrl"].ToString();

            if (returnUrl.ToLower().Contains( "product.aspx" ))
            {
                string newUrl = returnUrl.Replace( "Product.aspx?ProductName=", "" ) + "-details.aspx";
                Response.Redirect( newUrl );
            }
            if (returnUrl.ToLower().Contains( "passwordrecoveryfinished.aspx" ))
            {
                Response.Redirect( "Default.aspx" );
            }

            Response.Redirect( returnUrl );
        }
        else
        {
            Response.Redirect( "Default.aspx" );
        }
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
        string customerID = DataAccessContext.CustomerRepository.GetIDFromUserName( uxLogin.UserName );
        Customer customer = DataAccessContext.CustomerRepository.GetOne( customerID );
        if (!customer.IsNull &&
            Roles.IsUserInRole(uxLogin.UserName, "Customers") && customer.StoreIDs.Contains(new StoreRetriever().GetStore().StoreID))
        {
            if (customer.IsEnabled)
            {
                uxMessage.DisplayError( "[$LoginFailureMessage]" );
            }
            else
            {
                uxMessage.DisplayError( "[$LoginFailureMessage]" );
                e.Cancel = true;
            }
        }
        else
        {
            uxMessage.DisplayError( "[$NotCustomerAccount]" );
            e.Cancel = true;
        }
    }


    protected void uxRegisterLink_Load( object sender, EventArgs e )
    {
        SetUpRegisterLink( (HyperLink) sender );
    }

    protected void uxSkiploginButton_Click( object sender, EventArgs e )
    {
        if (VerifyAnonymousGiftCertificate())
            Response.Redirect( "~/Checkout.aspx?skiplogin=true" );
        else
        {
            Label uxMessage = (Label) uxLogin.FindControl( "uxSkiploginMessageLabel" );
            uxMessage.Visible = true;
        }
    }

    private bool VerifyAnonymousGiftCertificate()
    {
        foreach (ICartItem item in StoreContext.ShoppingCart.GetCartItems())
        {
            if (item.IsGiftCertificate)
                return false;
        }
        return true;

    }

    protected void SetUpAnonymousPanel()
    {
        Panel uxSkipLoginPanel = (Panel) uxLogin.FindControl( "uxSkipLoginPanel" );
        if (Request.QueryString["ReturnUrl"] != null)
        {
            string returnUrl = Request.QueryString["ReturnUrl"].ToString();
            if (returnUrl.ToLower().Contains( "checkout.aspx" ))
            {
                uxSkipLoginPanel.Visible = DataAccessContext.Configurations.GetBoolValueNoThrow( "AnonymousCheckoutAllowed" );
            }
            else
                uxSkipLoginPanel.Visible = false;
        }

    }
}
