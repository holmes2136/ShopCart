using System;
using System.Web.Security;
using Vevo.Deluxe.Domain.Contents;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;

public partial class Components_ContentLayout : Vevo.WebUI.International.BaseLanguageUserControl
{
    private string _contentName;
    private string _contentID;

    private void PopulateProductList( string subscriptionID )
    {
        uxProductRepeater.DataSource = DataAccessContext.ProductRepository.GetAllBySubscriptionLevelID(
            StoreContext.Culture, subscriptionID, StoreContext.CurrentStore.StoreID );
        uxProductRepeater.DataBind();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (ContentName == null)
            Response.Redirect( "Default.aspx" );

        Vevo.Domain.Contents.Content content = DataAccessContext.ContentRepository.GetOne( StoreContext.Culture, ContentID );

        if ((content.SubscriptionLevelID != "0") && !Roles.IsUserInRole( "Customers" ) && !Roles.IsUserInRole( "Administrators" ))
        {
            Response.Redirect( "UserLogin.aspx?ReturnUrl=Content.aspx?ContentName=" + content.UrlName );
        }

        if (!content.IsNull && content.IsEnabled)
        {
            string errMsg = String.Empty;
            ContentSubscription subscription = new ContentSubscription();
            subscription.SubscriptionLevelID = content.SubscriptionLevelID;
            if ((Roles.IsUserInRole( "Administrators" )) || (subscription.IsAvailable(StoreContext.Customer.CustomerID, out errMsg )))
            {
                uxTitleLiteral.Visible = true;
                uxTitleLiteral.Text = content.Title;

                uxBodyLiteral.Visible = true;
                uxBodyLiteral.Text = content.Body;

                uxProductSubscriptionListPanel.Visible = false;
            }
            else
            {
                uxTitleLiteral.Visible = true;
                uxTitleLiteral.Text = content.Title;

                uxBodyLiteral.Visible = true;

                string displayMsg = errMsg + "<br><br>"
                    + "The content allows level " + subscription.ContentSubscriptionLevel.Level;

                if (!subscription.ContentSubscriptionLevel.IsHigherLevel())
                    displayMsg += " or higher only";

                displayMsg += ". You will be granted to access content level " + subscription.ContentSubscriptionLevel.Level + " by purchase: <br>";
                uxBodyLiteral.Text = displayMsg;

                PopulateProductList( content.SubscriptionLevelID );

                uxProductSubscriptionListPanel.Visible = true;
            }
        }
        else
        {
            uxTitleLiteral.Visible = false;
            uxBodyLiteral.Text = Resources.ContentMessages.ContentNotAvailable;
        }
    }

    public string GetNavName( string productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, productID, StoreContext.CurrentStore.StoreID );
        return product.Name;
    }

    public string ContentName
    {
        get { return _contentName; }
        set { _contentName = value; }
    }

    public string ContentID
    {
        get { return _contentID; }
        set { _contentID = value; }
    }
}
