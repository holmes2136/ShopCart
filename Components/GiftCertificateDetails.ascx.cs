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
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.Domain.Stores;

public partial class Components_GiftCertificateDetails : Vevo.WebUI.International.BaseLanguageUserControl
{
    private bool IsElectronicOnlyGiftCertificate( Product product )
    {
        if (product.IsGiftCertificate)
        {
            GiftCertificateProduct giftProduct = (GiftCertificateProduct) product;
            return giftProduct.IsElectronic;
        }
        else
        {
            return false;
        }
    }

    private void SetupGiftDetail()
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID,new StoreRetriever().GetCurrentStoreID() );
        if (!product.IsGiftCertificate
            || IsElectronicOnlyGiftCertificate( product )
            || CatalogUtilities.IsCatalogMode())
        {
            uxNeedPhysicalGCP.Visible = false;
            uxRecipientTR.Visible = false;
            uxPersonalNoteTR.Visible = false;
        }
        else
        {
            uxGiftCertificateComponentsPanel.Visible = true;
            uxNeedPhysicalGCP.Visible = true;
            if (uxNeedPhysicalGCCheck.Checked)
            {
                uxRecipientTR.Visible = true;
                uxPersonalNoteTR.Visible = true;
            }
            else
            {
                uxRecipientTR.Visible = false;
                uxPersonalNoteTR.Visible = false;
            }
        }

        if (product.IsFixedPrice)
        {
            uxGiftAmountTR.Visible = false;
            if (!uxNeedPhysicalGCP.Visible && !uxRecipientTR.Visible && !uxPersonalNoteTR.Visible && !uxGiftAmountTR.Visible)
                uxGiftCertificateComponentsPanel.Visible = false;
        }
        else
        {
            uxGiftAmountTR.Visible = true;
            uxGiftCertificateComponentsPanel.Visible = true;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        SetupGiftDetail();
    }

    public string ProductID
    {
        get
        {
            if (ViewState["ProductID"] == null)
                return "0";
            else
                return (string) ViewState["ProductID"];
        }
        set
        {
            ViewState["ProductID"] = value;
        }
    }

    public bool IsGiftCertificate
    {
        get
        {
            return uxGiftCheck.Checked;
        }
        set
        {
            uxGiftCheck.Checked = value;
        }
    }

    public CartItemGiftDetails GetCartItemGiftDetails()
    {
        return new CartItemGiftDetails(
            uxRecipientText.Text,
            uxPersonalNoteText.Text,
            uxNeedPhysicalGCCheck.Checked,
            ConvertUtilities.ToDecimal( uxGiftAmountText.Text ) );
    }
}
