using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;

public partial class AdminAdvanced_Components_Products_GiftCertificate : AdminAdvancedBaseUserControl
{
    #region Private
    private void SetUpExpireType()
    {
        if (uxFixedDateRadio.Checked)
        {
            uxCalendarTD.Visible = true;
            uxNumberOfDayText.Visible = false;
            uxFixedDateRequiredValidator.Enabled = true;
        }
        else if (uxRollingDateRadio.Checked)
        {
            uxCalendarTD.Visible = false;
            uxNumberOfDayText.Visible = true;
            uxFixedDateRequiredValidator.Enabled = false;
        }
        else  // No expiration
        {
            uxCalendarTD.Visible = false;
            uxNumberOfDayText.Visible = false;
            uxFixedDateRequiredValidator.Enabled = false;
        }
    }

    private DateTime GetGiftFixedExpirationDate()
    {
        if (uxFixedDateCalendarPopup.IsValid)
            return uxFixedDateCalendarPopup.SelectedDate;
        else
            return DateTime.Today;
    }

    private GiftCertificateProduct.ExpireTypeEnum GetExpireType()
    {
        if (uxFixedDateRadio.Checked)
            return GiftCertificateProduct.ExpireTypeEnum.FixedDate;
        else if (uxRollingDateRadio.Checked)
            return GiftCertificateProduct.ExpireTypeEnum.RollingDate;
        else
            return GiftCertificateProduct.ExpireTypeEnum.None;
    }

    #endregion

    #region Protected
    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            uxFixedDateCalendarPopup.SelectedDate = DateTime.Now;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }

    protected void uxIsFixedPriceDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        uxStatusHidden.Value = "Refresh";
    }

    #endregion

    #region Public
    public void PopulateGiftData( GiftCertificateProduct product )
    {
        uxNoExpirationRadio.Checked = false;
        uxFixedDateRadio.Checked = false;
        uxRollingDateRadio.Checked = false;
        if (product.ExpireType == GiftCertificateProduct.ExpireTypeEnum.None)
            uxNoExpirationRadio.Checked = true;
        else if (product.ExpireType == GiftCertificateProduct.ExpireTypeEnum.FixedDate)
            uxFixedDateRadio.Checked = true;
        else if (product.ExpireType == GiftCertificateProduct.ExpireTypeEnum.RollingDate)
            uxRollingDateRadio.Checked = true;

        uxGiftAmountText.Text = String.Format( "{0:f2}", product.GiftAmount );
        uxFixedDateCalendarPopup.SelectedDate = product.FixedExpireDate;
        uxNumberOfDayText.Text = product.NumberOfDays.ToString();
        uxIsElectronicCheck.Checked = product.IsElectronic;
    }

    public void IsGiftCertificateEnabled( bool isEnabled )
    {
        uxIsGiftCertificateDrop.Enabled = isEnabled;
    }

    public void ClearInputFields()
    {
        uxIsGiftCertificateDrop.SelectedValue = "False";
        uxGiftAmountText.Text = "";
        uxNoExpirationRadio.Checked = true;
        uxFixedDateCalendarPopup.Reset();
        uxNumberOfDayText.Text = "";
        uxIsElectronicCheck.Checked = false;
        uxIsFixedPriceDrop.SelectedValue = "False";
    }
    public Product Setup( Culture culture )
    {
        if (ConvertUtilities.ToBoolean( uxIsGiftCertificateDrop.SelectedValue ))
        {
            GiftCertificateProduct giftProduct = new GiftCertificateProduct( culture );
            giftProduct.GiftAmount = ConvertUtilities.ToDecimal( uxGiftAmountText.Text );
            giftProduct.ExpireType = GetExpireType();
            giftProduct.FixedExpireDate = GetGiftFixedExpirationDate();
            giftProduct.NumberOfDays = ConvertUtilities.ToInt32( uxNumberOfDayText.Text );
            giftProduct.IsElectronic = uxIsElectronicCheck.Checked;
            return giftProduct;
        }
        else
        {
            return new Product( culture );
        }
    }

    public Product Update( Product product )
    {
        GiftCertificateProduct giftProduct = (GiftCertificateProduct) product;
        giftProduct.GiftAmount = ConvertUtilities.ToDecimal( uxGiftAmountText.Text );
        giftProduct.ExpireType = GetExpireType();
        giftProduct.FixedExpireDate = GetGiftFixedExpirationDate();
        giftProduct.NumberOfDays = ConvertUtilities.ToInt32( uxNumberOfDayText.Text );
        giftProduct.IsElectronic = uxIsElectronicCheck.Checked;
        return giftProduct;
    }

    public bool IsGiftCertificate { get { return ConvertUtilities.ToBoolean( uxIsGiftCertificateDrop.SelectedValue ); } }
    public bool IsFixedPrice { get { return ConvertUtilities.ToBoolean( uxIsFixedPriceDrop.SelectedValue ); } }

    public string GiftAmount { get { return uxGiftAmountText.Text; } }

    public string CurrentID
    {
        get
        {
            if (string.IsNullOrEmpty( MainContext.QueryString["ProductID"] ))
                return "0";
            else
                return MainContext.QueryString["ProductID"];
        }
    }

    public Culture CurrentCulture
    {
        get
        {
            return (Culture) ViewState["CurrentCulture"];
        }
        set { ViewState["CurrentCulture"] = value; }
    }

    public string CultureID
    {
        get
        {
            if (ViewState["CultureID"] == null)
                return String.Empty;
            else
                return (String) ViewState["CultureID"];
        }
        set { ViewState["CultureID"] = value; }
    }

    public void PopulateControls( Product product )
    {
        uxIsGiftCertificateDrop.SelectedValue = product.IsGiftCertificate.ToString();
        uxIsFixedPriceDrop.SelectedValue = product.IsFixedPrice.ToString();
    }

    public void SetDisplayControl( bool isVisible )
    {
        uxGiftCertificateTR.Visible = isVisible;
        uxIsGiftCertificateTR.Visible = isVisible;
    }

    public void SetGiftCertificateControlsVisibility( bool isEditMode )
    {
        if (isEditMode)
        {
            uxIsGiftCertificateDrop.Enabled = false;
        }
        else
        {
            uxIsGiftCertificateDrop.Enabled = true;
        }

        if (ConvertUtilities.ToBoolean( IsGiftCertificate ))
        {
            uxIsFixedPriceTR.Style.Remove( "Display" );
            if (ConvertUtilities.ToBoolean( uxIsFixedPriceDrop.SelectedValue ))
                uxGiftAmountTR.Visible = true;
            else
                uxGiftAmountTR.Visible = false;
            uxExpirationTypeTR.Style.Remove( "Display" );
            uxIsElectronicTR.Visible = true;
        }
        else
        {
            uxIsFixedPriceTR.Style["Display"] = "None";
            uxGiftAmountTR.Visible = false;
            uxExpirationTypeTR.Style["Display"] = "None";
            uxIsElectronicTR.Visible = false;
        }
        SetUpExpireType();

        uxGiftCertificateStatusHidden.Value = uxIsGiftCertificateDrop.SelectedValue;
    }

    #endregion

}
