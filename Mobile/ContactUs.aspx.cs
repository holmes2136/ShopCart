using System;
using System.Web.UI;
using Vevo.Domain;
using Vevo.WebAppLib;

public partial class Mobile_ContactUs : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    protected void Page_Load( object sender, EventArgs e )
    {
    }

    private void SendContactUsEmail()
    {
        string body = String.Format(
            "From Contact Us page.\n" +
            "Sender name: {0}\n" +
            "Email address: {1}\n" +
            "---------------------------------\n\n" +
            "{2}",
            uxNameText.Text,
            uxEmailText.Text,
            uxCommentText.Text );

        try
        {

            WebUtilities.SendMail(
                DataAccessContext.Configurations.GetValue( "CompanyEmail" ),
                DataAccessContext.Configurations.GetValue( "CompanyEmail" ),
                uxSubjectText.Text,
                body );

            Response.Redirect( "~/mobile/ContactUsFinished.aspx" );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxSubmitButton_Click( object sender, EventArgs e )
    {
        if (Page.IsValid)
        {
            SendContactUsEmail();
        }
    }
}
