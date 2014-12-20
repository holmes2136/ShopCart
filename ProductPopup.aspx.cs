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
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.Domain.Stores;

public partial class ProductPopup : Vevo.WebUI.International.BaseLanguagePage
{
    private string ProductID
    {
        get
        {
            if (Request.QueryString["ProductID"] == null)
                return "0";
            else
                return Request.QueryString["ProductID"];
        }
    }

    private void PopulateControl()
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, new StoreRetriever().GetCurrentStoreID() );

        string image = product.GetPrimaryProductImage().RegularImage;
        uxNameLable.Text = product.Name;
        uxNameLable.Font.Bold = true;
        uxImage.ImageUrl = "~/" + image;
        uxSummary.Text = product.ShortDescription;
        uxDescription.Text = product.LongDescription;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        PopulateControl();
        CloseWindow();
    }

    private void CloseWindow()
    {
        uxCloseWindow.Attributes.Add( "onclick", "window.close();" );
    }
}
