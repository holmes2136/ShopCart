using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.WebAppLib;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.WebUI;

public partial class AdminAdvanced_GiftCertifiCatePrint : System.Web.UI.Page
{
    private string GiftCertificateCode
    {
        get
        {
            return Request.QueryString["GiftCertificateCode"];
        }
    }

    private void PopulateControls()
    {
        GiftCertificate giftCertificate = DataAccessContext.GiftCertificateRepository.GetOne( GiftCertificateCode );        
        Order orders = DataAccessContext.OrderRepository.GetOne(
            DataAccessContext.OrderItemRepository.GetOne( giftCertificate.OrderItemID ).OrderID );
        string fromuser = orders.Billing.FirstName + " " + orders.Billing.LastName;
        string amount = StoreContext.Currency.FormatPrice( giftCertificate.GiftValue );
        string expireDate;
        if (giftCertificate.IsExpirable)
            expireDate = String.Format( "{0:dd} {0:MMM} {0:yyyy}", giftCertificate.ExpireDate );
        else
            expireDate = "-";

        string subjectMail;
        string bodyMail;

        EmailTemplateTextVariable.ReplaceGiftCertificateLayoutText( 
            giftCertificate, orders, fromuser, amount, expireDate, out subjectMail, out bodyMail );


        uxGiftLiteral.Text = bodyMail;
    }
    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControls();
        }
    }
}
