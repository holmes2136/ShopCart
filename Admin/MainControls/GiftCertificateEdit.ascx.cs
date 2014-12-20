using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Payments;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebUI;

public partial class AdminAdvanced_MainControls_GiftCertificateEdit : AdminAdvancedBaseUserControl
{
    private string GiftCertificateCode
    {
        get
        {
            return MainContext.QueryString["GiftCertificateCode"];
        }
    }

    private void PopulateControls()
    {
        GiftCertificate giftCertificate = DataAccessContext.GiftCertificateRepository.GetOne( GiftCertificateCode );
        if (!giftCertificate.IsNull)
        {
            uxNameLabel.Text = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, giftCertificate.ProductID, new StoreRetriever().GetCurrentStoreID() ).Name;
            uxGiftCertificateCodeLabel.Text = giftCertificate.GiftCertificateCode;
            uxGiftValueText.Text = string.Format( "{0:f2}", giftCertificate.GiftValue );
            uxRemainValueText.Text = string.Format( "{0:f2}", giftCertificate.RemainValue );
            uxRecipientText.Text = giftCertificate.Recipient;
            uxPersonalNote.Text = giftCertificate.PersonalNote;
            uxIsExpireCheck.Checked = giftCertificate.IsExpirable;
            uxDateCalendarPopup.SelectedDate = giftCertificate.ExpireDate;
            uxNeedPhysicalCheck.Checked = giftCertificate.NeedPhysical;
            uxIsActiveCheck.Checked = giftCertificate.IsActive;
        }

        if (!IsAdminModifiable())
        {
            uxUpdateButton.Visible = false;
        }
    }

    private void SetExpirationVisibility( bool visible )
    {
        uxExpireDateTR.Visible = visible;
        uxDateRequiredValidator.Enabled = visible;
    }

    private void Update()
    {
        GiftCertificate giftCertificate = DataAccessContext.GiftCertificateRepository.GetOne( GiftCertificateCode );
        giftCertificate.GiftValue = ConvertUtilities.ToDecimal( uxGiftValueText.Text );
        giftCertificate.RemainValue = ConvertUtilities.ToDecimal( uxRemainValueText.Text );
        giftCertificate.Recipient = uxRecipientText.Text;
        giftCertificate.PersonalNote = uxPersonalNote.Text;
        giftCertificate.IsExpirable = ConvertUtilities.ToBoolean( uxIsExpireCheck.Checked );
        giftCertificate.ExpireDate = uxDateCalendarPopup.SelectedDate;
        giftCertificate.NeedPhysical = ConvertUtilities.ToBoolean( uxNeedPhysicalCheck.Checked );
        giftCertificate.IsActive = ConvertUtilities.ToBoolean( uxIsActiveCheck.Checked );

        DataAccessContext.GiftCertificateRepository.Update( giftCertificate );

    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (String.IsNullOrEmpty( MainContext.QueryString["GiftCertificateCode"] ))
            MainContext.RedirectMainControl( "GiftCertificateList.ascx", "" );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateControls();
        }
        if (uxIsExpireCheck.Checked)
        {
            SetExpirationVisibility( true );
        }
        else
        {
            SetExpirationVisibility( false );
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        if (Page.IsValid)
        {
            Update();
            PopulateControls();
            uxMessage.DisplayMessage( Resources.GiftCertificateMessage.UpdateSuccess );
        }
    }

    protected void uxPrintButton_Load( object sender, EventArgs e )
    {
        string script = String.Format( "window.open( '{0}', '_blank' ); return false;",
            "GiftCertifiCatePrint.aspx?GiftCertificateCode=" + GiftCertificateCode );

        Button printButton = (Button) sender;
        printButton.Attributes.Add( "onclick", script );
    }
}
