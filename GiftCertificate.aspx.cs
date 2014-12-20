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
using Vevo.Domain;
using Vevo.Domain.Payments;
using Vevo.WebAppLib;
using Vevo.WebUI;

public partial class GiftCertificatePage : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string UrlParameter
    {
        get
        {
            if (!String.IsNullOrEmpty( Request.QueryString["Code"] ))
                return Request.QueryString["Code"];
            else
                return "";
        }

    }

    private void PopulateData( GiftCertificate gift )
    {
        uxGiftCertificateCodeLabel.Text = gift.GiftCertificateCode.ToUpper();
        uxRemainValueLabel.Text = StoreContext.Currency.FormatPrice( gift.RemainValue );
        if (gift.IsExpirable)
            uxExpireDateLabel.Text = gift.ExpireDate.ToLongDateString();
        else
            uxExpireDateLabel.Text = "-";

        if (gift.IsActive)
            uxStatusLabel.Text = "Active";
        else
            uxStatusLabel.Text = "Not Active";
    }

    private void PopulateControl()
    {
        if (UrlParameter != "")
        {
            uxGiftCodeDiv.Visible = false;
            string giftCertificateCode = DataAccessContext.GiftCertificateRepository.GetCodeFromUrlParameter( UrlParameter );
            if (IsGiftCodeExist( giftCertificateCode ))
            {
                GiftCertificate gift = DataAccessContext.GiftCertificateRepository.GetOne( giftCertificateCode );

                if (!VerifyUser( gift.OrderItemID ))
                {
                    uxGiftDetailDiv.Visible = false;
                    uxErrorLiteral.Visible = true;
                    uxGiftDetailTitle.Visible = false;
                }
                else
                    PopulateGiftCertificateDetails( gift );
            }
        }
    }

    private bool VerifyUser( string orderItemID )
    {
        string orderID = DataAccessContext.OrderItemRepository.GetOne( orderItemID ).OrderID;
        string userName = DataAccessContext.OrderRepository.GetOne( orderID ).UserName;

        return userName == User.Identity.Name;
    }

    private void PopulateGiftCertificateDetails( GiftCertificate gift )
    {
        string errorMessage;
        if (gift.Verify( out errorMessage ))
        {
            uxGiftDetailDiv.Visible = true;
            PopulateData( gift );
        }
        else
        {
            uxGiftDetailDiv.Visible = false;
            uxMessage.DisplayError( errorMessage );
        }
    }

    private bool IsGiftCodeExist( string giftCode )
    {
        if (String.IsNullOrEmpty( giftCode ))
        {
            uxGiftDetailDiv.Visible = false;
            string errorMessage = String.Empty;
            if (String.IsNullOrEmpty( UrlParameter ))
                errorMessage = "Invalid gift certificate code. Please try again.";
            else
                errorMessage = "Cannot display gift certificate code. Please verify your URL.";

            uxMessage.DisplayError( errorMessage );

            return false;
        }
        else
        {
            return true;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!User.Identity.IsAuthenticated ||
            !Roles.IsUserInRole( User.Identity.Name, "Customers" ))
            Response.Redirect( "~/UserLogin.aspx?ReturnUrl=" + "GiftCertificate.aspx?Code=" + UrlParameter );

        WebUtilities.TieButton( this, uxGiftCertificateCodeText, uxVerifyImageButton );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsPostBack)
            PopulateControl();
    }

    protected void uxVerifyImageButton_Click( object sender, EventArgs e )
    {
        if (uxGiftCertificateCodeText.Text != "")
        {
            GiftCertificate gift = DataAccessContext.GiftCertificateRepository.GetOne( uxGiftCertificateCodeText.Text );
            if (IsGiftCodeExist( gift.GiftCertificateCode ))
                PopulateGiftCertificateDetails( gift );
        }
        else
            uxMessage.DisplayError( "[$NoGiftCertificateCode]" );
    }
}
