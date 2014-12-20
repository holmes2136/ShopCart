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
using Vevo;
using Vevo.Domain.Products;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Shared.Utilities;
using Vevo.Domain.Stores;

public partial class Admin_Components_Order_ProductGiftCertificateDetails : AdminAdvancedBaseUserControl
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
        Product product = DataAccessContext.ProductRepository.GetOne(
           DataAccessContext.CultureRepository.GetOne( CultureID ), ProductID, StoreID );
        if (!product.IsGiftCertificate || IsElectronicOnlyGiftCertificate( product ))
        {
            uxNeedPhysicalGCP.Visible = false;
            uxRecipientTR.Visible = false;
            uxPersonalNoteTR.Visible = false;
        }
        else
        {
            uxGiftCertificateComponentsPanel.Visible = true;
            uxNeedPhysicalGCP.Visible = true;
            uxRecipientTR.Visible = true;
            uxPersonalNoteTR.Visible = true;
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
    }

    public void PopulateControls()
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

    public string CultureID
    {
        get
        {
            if (ViewState["CultureID"] == null)
                return CultureUtilities.StoreCultureID;
            else
                return (string) ViewState["CultureID"];
        }
        set
        {
            ViewState["CultureID"] = value;
        }
    }

    public string StoreID
    {
        get
        {
            if (ViewState["StoreID"] == null)
                return Store.RegularStoreID;
            else
                return (string) ViewState["StoreID"];
        }
        set
        {
            ViewState["StoreID"] = value;
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

    private bool IsNeedPhysicalGift()
    {
        return !String.IsNullOrEmpty( uxRecipientText.Text );
    }
    public CartItemGiftDetails GetCartItemGiftDetails()
    {
        return new CartItemGiftDetails(
            uxRecipientText.Text,
            uxPersonalNoteText.Text,
            IsNeedPhysicalGift(),
            ConvertUtilities.ToDecimal( uxGiftAmountText.Text ) );
    }
}
