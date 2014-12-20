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
using Vevo.Domain.Marketing;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.Marketing;
using Vevo.Shared.DataAccess;

public partial class AdminAdvanced_MainControls_AffiliatePaymentSendMail : AdminAdvancedBaseUserControl
{
    private string AffiliatePaymentID
    {
        get
        {
            return MainContext.QueryString["AffiliatePaymentID"];
        }
    }

    private string AffiliateCode
    {
        get
        {
            return MainContext.QueryString["AffiliateCode"];
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
            MainContext.RedirectMainControl( "Default.ascx", String.Empty );

        if (IsAdminModifiable())
        {
            uxSendButton.Visible = true;
        }
        else
        {
            uxSendButton.Visible = false;
        }

        if (!MainContext.IsPostBack)
        {
            SetEmailBody();
        }
    }

    protected void uxSendButton_Click( object sender, EventArgs e )
    {
        if (uxEmailBodyText.Text == string.Empty)
        {
            uxMessage.DisplayError( Resources.AffiliatePaymentMessages.EmptyContentError );
            return;
        }

        try
        {
            string emailBody = uxEmailBodyText.Text;
            WebUtilities.SendHtmlMail(
                NamedConfig.CompanyEmail, uxToText.Text.Trim(), uxSubjectText.Text, emailBody );
            uxMessage.DisplayMessage( Resources.AffiliatePaymentMessages.SendMailSuccess );

        }
        catch (Exception ex)// in case of an error
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void SetEmailBody()
    {
        uxFromLabel.Text = NamedConfig.CompanyEmail;
        uxToText.Text = DataAccessContextDeluxe.AffiliateRepository.GetOne( AffiliateCode ).Email;

        string subjectMail;
        string bodyMail;

        AffiliatePayment affiliatePayment = DataAccessContextDeluxe.AffiliatePaymentRepository.GetOne(
            AffiliatePaymentID );

        EmailTemplateTextVariable.ReplaceAffiliatePaymentText(
            affiliatePayment.PaidDate.ToShortDateString(),
            StoreContext.Currency.FormatPrice( affiliatePayment.Amount ),
            DataAccessContextDeluxe.AffiliateOrderRepository.GetOrderIDByAffiliatePaymentID( AffiliatePaymentID ),
            out subjectMail,
            out bodyMail );

        uxSubjectText.Text = subjectMail;
        uxEmailBodyText.Text = bodyMail;
    }
}
