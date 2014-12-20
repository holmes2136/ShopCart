using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using Vevo.Deluxe.WebUI.Marketing;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Stores;
using Vevo.Domain.Users;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.Facebook;

public partial class UserLogin : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string ReplaceOneTimeNoCase( string original, string oldValue, string newValue )
    {
        int index = original.IndexOf( oldValue, StringComparison.OrdinalIgnoreCase );
        if (index < 0)
            return original;

        string removed = original.Remove( index, oldValue.Length );
        return removed.Insert( index, newValue );
    }


    private void RedirectToAdminLoginIfNecessary()
    {
        string returnURL = Request.QueryString["ReturnUrl"];
        if (returnURL != null)
        {
            UrlPath urlPath = new UrlPath( returnURL );

            if (returnURL.Equals( "/" ))
            {
                return;
            }
            else if (!returnURL.EndsWith( "/" ) && !returnURL.Contains( "." ))
            {
                urlPath = new UrlPath( returnURL + "/" );
            }

            if (returnURL.ToLower().Contains( "blog" ))
            {
                return;
            }
            if (!String.IsNullOrEmpty( urlPath.ExtractFirstApplicationSubfolder() ))
            {
                string newUrl = ReplaceOneTimeNoCase(
                    Request.Url.AbsoluteUri,
                    "/UserLogin.aspx", "/" + urlPath.ExtractFirstApplicationSubfolder() + "/Login.aspx" );

                Session.Remove( "ReturnURL" );
                Response.Redirect( newUrl );
            }
            if (urlPath.IsAffiliateUrl())
            {
                string newUrl = ReplaceOneTimeNoCase(
                    Request.Url.AbsoluteUri, "/UserLogin.aspx", "/AffiliateLogin.aspx" );

                Session.Remove( "ReturnURL" );
                Response.Redirect( newUrl );
            }
        }
    }

    private void SetUpRegisterLink( LinkButton uxRegisterLink, bool isFacebook )
    {
        uxRegisterLink.PostBackUrl = "Register.aspx";

        string returnUrl = String.Empty;
        if (Session["ReturnURL"] != null)
            returnUrl = Session["ReturnURL"].ToString();
        else if (Request.QueryString["ReturnUrl"] != null)
            returnUrl = Request.QueryString["ReturnUrl"].ToString();

        if (returnUrl != String.Empty)
        {
            if (returnUrl.ToLower().Contains( "checkout.aspx" ))
            {
                uxRegisterLink.PostBackUrl += "?ReturnUrl=Shipping.aspx&Checkout=1";
            }
            else
                uxRegisterLink.PostBackUrl += "?ReturnUrl=" + Server.UrlEncode( returnUrl );

            if (isFacebook)
                uxRegisterLink.PostBackUrl += "&FBConnect=true";
        }
        else
        {
            if (isFacebook)
                uxRegisterLink.PostBackUrl += "?FBConnect=true";
        }

    }

    private void LoginFacebookProcess()
    {
        FacebookLogin fbLogin = new FacebookLogin( "UserLogin.aspx" );
        string code = fbLogin.ParseCode( HttpContext.Current.Request.Url.ToString() );
        Components_VevoHyperLink fbLoginButton = (Components_VevoHyperLink) uxLogin.FindControl( "uxFacebookLink" );
        fbLoginButton.NavigateUrl = fbLogin.FacebookLoginURL;
        if ((code != null) && (code != String.Empty))
        {
            string fbUserID = fbLogin.GetFacebookUserID( code );
            if (fbUserID != String.Empty)
            {
                Customer customer = DataAccessContext.CustomerRepository.GetOneByFBUserID( fbUserID );
                if (!customer.IsNull)
                {
                    TextBox uxUsername = (TextBox) uxLogin.FindControl( "UserName" );
                    uxUsername.Text = customer.UserName;
                    FormsAuthentication.SetAuthCookie( customer.UserName, true );
                    AffiliateHelper.UpdateAffiliateReference( uxLogin.UserName );
                    RedirectProcess();
                    if (Session["ReturnURL"] != null)
                    {
                        string returnURL = Session["ReturnURL"].ToString();
                        Session.Remove( "ReturnURL" );
                        Response.Redirect( returnURL );
                    }
                    else
                        Response.Redirect( "Default.aspx" );
                }
                else
                {
                    LinkButton registerLink = (LinkButton) uxLogin.FindControl( "uxRegisterLink" );
                    SetUpRegisterLink( registerLink, true );
                    Response.Redirect( registerLink.PostBackUrl );
                }
            }
            else
            {
                uxMessage.DisplayError( "[$FacebookConnectFailureMessage]" );
            }
        }
        else if (Request.QueryString["ReturnUrl"] != null)
        {
            Session["ReturnURL"] = Request.QueryString["ReturnUrl"].ToString();
        }
    }

    private void RedirectProcess()
    {
        string returnUrl = String.Empty;
        if (Session["ReturnURL"] != null)
            returnUrl = Session["ReturnURL"].ToString();
        else if (Request.QueryString["ReturnUrl"] != null)
            returnUrl = Request.QueryString["ReturnUrl"].ToString();

        if (returnUrl != String.Empty)
        {
            if (returnUrl.ToLower().Contains( "product.aspx" ))
            {
                string newUrl = returnUrl.Replace( "Product.aspx?ProductName=", "" ) + "-details.aspx";
                Session.Remove( "ReturnURL" );
                Response.Redirect( newUrl );
            }

            if (returnUrl.ToLower().Contains( "content.aspx" ))
            {
                string newUrl = returnUrl.Replace( "Content.aspx?ContentName=", "" ) + "-content.aspx";
                Session.Remove( "ReturnURL" );
                Response.Redirect( newUrl );
            }

            if (returnUrl.ToLower().Contains( "passwordrecoveryfinished.aspx" ))
            {
                Session.Remove( "ReturnURL" );
                Response.Redirect( "~/Default.aspx" );
            }
        }
    }

    private bool LoginProcess( Customer customer )
    {
        if (!customer.IsNull && Roles.IsUserInRole( customer.UserName, "Customers" ) && customer.StoreIDs.Contains( new StoreRetriever().GetStore().StoreID ))
        {
            if (customer.IsEnabled)
            {
                uxMessage.DisplayError( "[$LoginFailureMessage]" );
            }
            else
            {
                uxMessage.DisplayError( "[$LoginDisabledMessage]" );

                return false;
            }
        }
        else
        {
            uxMessage.DisplayError( "[$NotCustomerAccount]" );
            return false;
        }
        return true;
    }

    protected void Page_PreInit( object sender, EventArgs e )
    {
        string theme = DataAccessContext.Configurations.GetValue( "StoreTheme", StoreContext.CurrentStore );
        string returnURL = Request.QueryString["ReturnUrl"];

        if (String.IsNullOrEmpty( returnURL ))
            returnURL = String.Empty;

        else
        {
            if (returnURL.ToLower().Contains( "checkout.aspx" ))
            {
                this.Page.MasterPageFile = "~/Themes/" + theme + "/Checkout.master";
            }
        }

    }

    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpAnonymousPanel();
        RedirectToAdminLoginIfNecessary();

        if (FacebookConnect.IsConfigurationReady() && !IsPostBack)
        {
            Components_VevoHyperLink fbLoginButton = (Components_VevoHyperLink) uxLogin.FindControl( "uxFacebookLink" );
            fbLoginButton.Visible = true;
            LoginFacebookProcess();
        }

        WebUtilities.TieLoginControlImageButton( this.Page, uxLogin );

        if (Request.QueryString["ReturnUrl"] != null)
        {
            string returnUrl = Request.QueryString["ReturnUrl"].ToString();
            if (returnUrl.ToLower().Contains( "checkout.aspx" ))
            {
                uxCheckoutIndicator.StepID = 1;
                uxUserLogin.Attributes.Add( "class", "CheckoutLogin" );
            }
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {

    }

    protected void uxLogin_LoggedIn( object sender, EventArgs e )
    {
        AffiliateHelper.UpdateAffiliateReference( uxLogin.UserName );
        RedirectProcess();
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
        if (!LoginProcess( customer ))
            e.Cancel = true;
    }

    protected void uxRegisterLink_Load( object sender, EventArgs e )
    {
        SetUpRegisterLink( (LinkButton) sender, false );
    }

    protected void uxSkiploginButton_Click( object sender, EventArgs e )
    {
        if (VerifyAnonymousGiftCertificate())
        {
            Session.Remove( "ReturnURL" );
            Response.Redirect( "~/Checkout.aspx?skiplogin=true" );
        }
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
                uxTopPage.Visible = false;
                uxCheckoutIndicator.Title = "[$Checkout Title]";
                uxSkipLoginPanel.Visible = DataAccessContext.Configurations.GetBoolValueNoThrow( "AnonymousCheckoutAllowed" );
            }
            else
            {
                uxTitle.Text = "[$My Account]";
                uxSkipLoginPanel.Visible = false;
            }
        }

    }
}
