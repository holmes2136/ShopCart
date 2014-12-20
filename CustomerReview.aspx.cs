using System;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
public partial class CustomerReview : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string ProductID
    {
        get
        {
            if (Request.QueryString["ProductID"] != null)
                return Request.QueryString["ProductID"].ToString();
            else if (Request.QueryString["ProductName"] != null)
                return DataAccessContext.ProductRepository.GetProductIDFromUrlName(
                    Request.QueryString["ProductName"] );
            else
                return String.Empty;
        }
    }

    private string ProductName
    {
        get
        {
            if (Request.QueryString["ProductName"] != null)
                return Request.QueryString["ProductName"].ToString();
            else if (Request.QueryString["ProductID"] != null)
                return DataAccessContext.ProductRepository.GetOne(
                    StoreContext.Culture,
                    Request.QueryString["ProductID"], new StoreRetriever().GetCurrentStoreID() ).Name;
            return String.Empty;
        }
    }

    private void CheckConfigDisplay()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "RatingRequireLogin" ) &&
            DataAccessContext.Configurations.GetBoolValue( "ReviewRequireLogin" ) &&
            Membership.GetUser() == null
            )
        {
            if (DataAccessContext.Configurations.GetBoolValue( "UseSimpleCatalogUrl" ))
                Response.Redirect( "UserLogin.aspx?ReturnUrl=CustomerReview.aspx?ProductName=" + ProductName );
            else
                Response.Redirect( "UserLogin.aspx?ReturnUrl=CustomerReview.aspx?ProductID=" + ProductID );
        }
        else
        {
            if (!DataAccessContext.Configurations.GetBoolValue( "CustomerRating" ))
            {
                uxRatingDiv.Visible = false;
                uxRatingLoginPanel.Visible = false;
            }
            else if (DataAccessContext.Configurations.GetBoolValue( "RatingRequireLogin" ) &&
                !Page.User.Identity.IsAuthenticated)
            {
                uxRatingDiv.Visible = false;
                uxRatingLoginPanel.Visible = true;
            }
            else
            {
                uxRatingDiv.Visible = true;
                uxRatingLoginPanel.Visible = false;
            }


            if (!DataAccessContext.Configurations.GetBoolValue( "CustomerReview" ))
            {
                uxReviewDiv.Visible = false;
                uxReviewLogingLink.Visible = false;
            }
            else if (DataAccessContext.Configurations.GetBoolValue( "ReviewRequireLogin" ) &&
                !Page.User.Identity.IsAuthenticated)
            {
                uxReviewDiv.Visible = false;
                uxReviewLogingLink.Visible = true;
            }
            else
            {
                uxReviewDiv.Visible = true;
                uxReviewLogingLink.Visible = false;
            }
        }
    }

    private string GetEmptyStar( int i, int amount )
    {
        string empty = String.Empty;
        for (int a = 0; a < amount - i; a++)
        {
            empty += "<img src='Images/Design/Icon/Star_Blank.gif' />";
        }
        return empty;
    }

    private void InitialProductRatingList()
    {
        int amountRatingValue = Convert.ToInt32( DataAccessContext.Configurations.GetValue( "StarRatingAmount" ) );

        DataTable table = new DataTable();
        table.Columns.Add( new DataColumn( "Text", System.Type.GetType( "System.String" ) ) );
        table.Columns.Add( new DataColumn( "Value", System.Type.GetType( "System.String" ) ) );

        string stars = String.Empty;
        for (int i = 1; i < amountRatingValue + 1; i++)
        {
            string themeName = DataAccessContext.Configurations.GetValue( "StoreTheme" );
            stars += "<img src=" + String.Format( "Themes/{0}/Images/Design/Icon/Star_Full.gif", themeName ) + " />";
            
            DataRow row = table.NewRow();
            row["Text"] = stars + GetEmptyStar( i, amountRatingValue );
            row["Value"] = i.ToString();

            table.Rows.Add( row );
        }

        table.AcceptChanges();

        uxProductRatingList.DataTextField = "Text";
        uxProductRatingList.DataValueField = "Value";
        uxProductRatingList.DataSource = table;
        uxProductRatingList.DataBind();
    }
    private void InitialProductDetail()
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, new StoreRetriever().GetCurrentStoreID() );
        uxNameLink.Text = product.Name;
        uxNameLink.NavigateUrl = UrlManager.GetProductUrl( product.ProductID, product.UrlName );
        ProductImage details = product.GetPrimaryProductImage();
        uxCatalogImage.ImageUrl = "~/" + details.RegularImage;

        uxSkuLabel.Text = product.Sku;
        uxShortDescriptionLabel.Text = product.ShortDescription;
        uxRatingCustomer.ProductID = product.ProductID;

        decimal retailPrice = product.GetProductPrice( new StoreRetriever().GetCurrentStoreID() ).RetailPrice;
        uxRetailPriceDiv.Visible = IsRetailPriceEnabled( product.IsFixedPrice, product.IsCustomPrice, retailPrice, product.IsCallForPrice );
        uxRetailPriceLabel.Text = StoreContext.Currency.FormatPrice( retailPrice );
        uxPriceValueLabel.Text = StoreContext.Currency.FormatPrice( product.GetDisplayedPrice( StoreContext.WholesaleStatus ) );

        if (!IsAuthorizedToViewPrice( product ))
        {
            uxRetailPriceDiv.Visible = false;
            uxPriceDiv.Visible = false;
        }

    }
    private bool IsRatingControlsVisible()
    {
        return uxRatingDiv.Visible;
    }

    private bool IsReviewControlsVisible()
    {
        return uxReviewDiv.Visible;
    }

    protected bool IsAuthorizedToViewPrice( Product product )
    {
        bool show = true;

        if (ConvertUtilities.ToBoolean( product.IsCallForPrice ))
            return false;

        if (DataAccessContext.Configurations.GetBoolValue( "PriceRequireLogin" ) && !Page.User.Identity.IsAuthenticated)
        {
            show = false;
        }

        return show;
    }

    private string GetCurrentCustomerID()
    {
        if (Page.User.Identity.IsAuthenticated &&
            Roles.IsUserInRole( "Customers" ))
        {
            return DataAccessContext.CustomerRepository.GetIDFromUserName( Membership.GetUser().UserName );
        }
        else
        {
            return "0";
        }
    }

    private double GetSelectedProductRating()
    {
        double customerRating;

        if (uxProductRatingList.SelectedValue == "")
        {
            customerRating = 0;
        }
        else
        {
            customerRating = Convert.ToDouble( uxProductRatingList.SelectedValue.ToString() );
        }

        customerRating /= Convert.ToDouble( DataAccessContext.Configurations.GetValue( "StarRatingAmount" ) );

        return customerRating;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            uxProductRatingList.ClearSelection();
            InitialProductRatingList();
            InitialProductDetail();
        }
        if (DataAccessContext.Configurations.GetBoolValue( "UseSimpleCatalogUrl" ))
        {
            uxRatingLoginLink.NavigateUrl = "UserLogin.aspx?ReturnUrl=CustomerReview.aspx?ProductName=" + ProductName;
            uxReviewLogingLink.NavigateUrl = "UserLogin.aspx?ReturnUrl=CustomerReview.aspx?ProductName=" + ProductName;
        }
        else
        {
            uxRatingLoginLink.NavigateUrl = "UserLogin.aspx?ReturnUrl=CustomerReview.aspx?ProductName=" + ProductID;
            uxReviewLogingLink.NavigateUrl = "UserLogin.aspx?ReturnUrl=CustomerReview.aspx?ProductName=" + ProductID;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        CheckConfigDisplay();
    }

    protected void uxSubmit_Click( object sender, EventArgs e )
    {
        string reviewID = CustomerReviewAccess.Create(
            ProductID,
            StoreContext.Culture.CultureID,
            GetCurrentCustomerID(),
            GetSelectedProductRating(),
            true,
            uxSubjectText.Text.Trim(),
            uxBodyText.Text
            );

        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, new StoreRetriever().GetCurrentStoreID() );
        string url = UrlManager.GetProductUrl( ProductID, product.UrlName );
        Response.Redirect( url );
    }
    protected bool IsRetailPriceEnabled( object isFixedPrice, object isCustomPrice, decimal retailPrice, object isCallForPrice )
    {
        if (DataAccessContext.Configurations.GetBoolValue( "PriceRequireLogin" ) && !Page.User.Identity.IsAuthenticated)
            return false;

        if (ConvertUtilities.ToBoolean( isCallForPrice ))
            return false;

        if (ConvertUtilities.ToBoolean( isCustomPrice ))
            return false;

        if (CatalogUtilities.IsRetailMode &&
            ConvertUtilities.ToBoolean( isFixedPrice ) &&
            ConvertUtilities.ToDecimal( retailPrice ) > 0
            )
            return true;
        else
            return false;
    }
}
