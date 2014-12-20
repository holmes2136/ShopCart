using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using Vevo.Domain.Products;
using Vevo.Domain;
using Vevo.WebUI;

public partial class Components_ProductSpecificationDetails : Vevo.WebUI.International.BaseLanguageUserControl
{
    private void PopulateControls()
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, StoreContext.CurrentStore.StoreID );

        foreach (ProductSpecification item in product.ProductSpecifications)
        {
            SpecificationItem specificationItem = DataAccessContext.SpecificationItemRepository.GetOne(
                StoreContext.Culture, item.SpecificationItemID );

            Label label = new Label();
            label.ID = "SpecificationNameLabel" + item.SpecificationItemID;
            label.Text = "  " + specificationItem.DisplayName + " : ";
            label.CssClass = "SpecItemName";
            uxSpecificationItemTR.Controls.Add( label );

            label = new Label();
            label.ID = "SpecificationValueLabel" + item.SpecificationItemID;
            label.Text = "  " + item.Value;
            label.CssClass = "SpecItemValue";
            uxSpecificationItemTR.Controls.Add( label );

            Panel panel = new Panel();
            panel.CssClass = "Clear";
            uxSpecificationItemTR.Controls.Add( panel );

        }

    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    public string ProductID
    {
        get
        {
            if (ViewState["ProductID"] == null)
                ViewState["ProductID"] = "0";
            return ViewState["ProductID"].ToString();
        }
        set
        {
            ViewState["ProductID"] = value;
        }
    }
}
