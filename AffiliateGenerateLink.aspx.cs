using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.DataAccess;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using Vevo.Deluxe.Domain;

public partial class AffiliateGenerateLink : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    private string AffiliateCode
    {
        get
        {
            return DataAccessContextDeluxe.AffiliateRepository.GetCodeFromUserName( HttpContext.Current.User.Identity.Name );
        }
    }

    private void PopulateCategory()
    {
        if (!IsPostBack)
        {
            uxCategoryDrop.Items.Add( new ListItem( "-- Select --", "0" ) );
            IList<Category> categoryList = DataAccessContext.CategoryRepository.GetAllLeafCategory(
                StoreContext.Culture, "ParentCategoryID" );

            string currentParentID = "";
            string tmpFullPath = "";
            for (int i = 0; i < categoryList.Count; i++)
            {
                if (currentParentID != categoryList[i].ParentCategoryID)
                {
                    tmpFullPath = categoryList[i].CreateFullCategoryPathParentOnly();
                    currentParentID = categoryList[i].ParentCategoryID;
                }
                uxCategoryDrop.Items.Add(
                new ListItem( tmpFullPath + categoryList[i].Name,
                        categoryList[i].CategoryID ) );
            }
        }
    }

    private void PopulateProduct()
    {
        uxProductDrop.Items.Clear();
        if (uxCategoryDrop.SelectedValue != "0")
        {
            uxProductPanel.Visible = true;

            IList<Product> productList = DataAccessContext.ProductRepository.GetByCategoryID(
                StoreContext.Culture,
                uxCategoryDrop.SelectedValue,
                "ProductID", BoolFilter.ShowTrue, true, new StoreRetriever().GetCurrentStoreID() );

            uxProductDrop.DataSource = productList;
            uxProductDrop.DataTextField = "Name";
            uxProductDrop.DataValueField = "ProductID";
            uxProductDrop.DataBind();

            if (productList.Count <= 0)
            {
                uxProductPanel.Visible = false;
                uxProductLinkPanel.Visible = false;
                uxGenerateButton.Visible = false;
            }
            else
            {
                uxProductPanel.Visible = true;
                uxProductLinkPanel.Visible = true;
                uxGenerateButton.Visible = true;
            }
        }
        else
        {
            uxProductPanel.Visible = false;
            uxProductLinkPanel.Visible = false;
            uxGenerateButton.Visible = false;
        }
    }

    private void PopulateWebSiteLink()
    {
        string webSiteLink = UrlPath.StorefrontUrl + "Default.aspx?AffiliateCode=" + AffiliateCode;
        uxWebSiteLinkText.Text = webSiteLink;
    }

    private void GenerateProductLink()
    {
        uxProductLinkPanel.Visible = true;
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, uxProductDrop.SelectedValue, new StoreRetriever().GetCurrentStoreID() );
        uxProductLinkLabel.Text = "[$ProductLinkDetail] " + product.Name;
        string productLink = UrlPath.StorefrontUrl.Substring( 0, UrlPath.StorefrontUrl.Length - 1 ) +
            UrlManager.GetProductUrl( product.ProductID, product.UrlName ).Substring( 1 );

        if (DataAccessContext.Configurations.GetBoolValue( "UseSimpleCatalogUrl" ))
            productLink += "?AffiliateCode=" + AffiliateCode;
        else
            productLink += "&AffiliateCode=" + AffiliateCode;

        uxProductLinkText.Text = productLink;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateCategory();
        PopulateProduct();
        PopulateWebSiteLink();
    }

    protected void uxGenerateButton_Click( object sender, EventArgs e )
    {
        if (uxCategoryDrop.SelectedValue == "0" ||
           uxProductDrop.Items.Count == 0)
            return;

        GenerateProductLink();
    }

    protected void uxCategoryDrop_SelectIndexChanged( object sender, EventArgs e )
    {

        //RefreshGrid();
    }
}
