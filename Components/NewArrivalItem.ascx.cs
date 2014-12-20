using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.Domain;

public partial class Components_NewArrivalItem : Vevo.WebUI.Products.BaseProductUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if (DataAccessContext.Configurations.GetBoolValue( "PriceRequireLogin" ) && !Page.User.Identity.IsAuthenticated)
        {
            uxRetailPrice.Visible = false;
            uxNewArrivalItemValue.Visible = false;
        }
    }
    protected string GetFormattedPrice(object productID)
    {
        Product product = DataAccessContext.ProductRepository.GetOne(StoreContext.Culture, productID.ToString(), StoreContext.CurrentStore.StoreID);

        decimal price = product.GetDisplayedPrice(StoreContext.WholesaleStatus);
        return StoreContext.Currency.FormatPrice(price);
    }
}
