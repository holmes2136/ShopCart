using System;
using System.Text;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Stores;
using Vevo.WebAppLib;

public partial class ContactUs : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        SetStoreMap();
    }

    private void SetStoreMap()
    {
        Store currentStore = DataAccessContext.StoreRepository.GetOne( DataAccessContext.StoreRetriever.GetCurrentStoreID() );
        if (DataAccessContext.Configurations.GetValue( "EnableMap", currentStore ).Equals( "False" ))
        {
            ContactMapDiv.Visible = false;
        }
        else
        {
            ScriptManager.RegisterClientScriptInclude( Page, typeof( Page ), "GoogleMap", "http://maps.googleapis.com/maps/api/js?sensor=false" );
            RegisterJavaScript( currentStore );
        }

    }




    private void RegisterJavaScript( Store currentStore )
    {
        ContactMapDiv.Attributes.Add( "Onload", "initialize()" );
        ContactMapDiv.Attributes.Add( "OnResize", "initialize()" );
        String csname = "ContactMap";
        ClientScriptManager cs = Page.ClientScript;
        String latitude = DataAccessContext.Configurations.GetValue( "Latitude", currentStore );
        String longitude = DataAccessContext.Configurations.GetValue( "Longitude", currentStore );
        String companyName = DataAccessContext.Configurations.GetValue(CultureUtilities.StoreCultureID, "CompanyName" );

        StringBuilder sb = new StringBuilder();
        sb.AppendLine( "function initialize() {" );
        sb.AppendLine( "     var map_canvas = document.getElementById('" + ContactMapDiv.ClientID + "');" );
        sb.AppendLine( "     var map_options = {" );
        sb.AppendLine( "         center: new google.maps.LatLng(" + latitude + ", " + longitude + ")," );
        sb.AppendLine( "         zoom: 15," );
        sb.AppendLine( "         mapTypeId: google.maps.MapTypeId.ROADMAP" );
        sb.AppendLine( "     }" );
        sb.AppendLine( "     var map = new google.maps.Map(map_canvas, map_options)" );
        sb.AppendLine( "     var marker = new google.maps.Marker({" );
        sb.AppendLine( "         position: new google.maps.LatLng(" + latitude + ", " + longitude + ")," );
        sb.AppendLine( "         map: map," );
        sb.AppendLine( "         title: '"+companyName+"'" );
        sb.AppendLine( "     });" );
        sb.AppendLine( "}" );
        sb.AppendLine( "google.maps.event.addDomListener(window, 'load', initialize);" );

        if (!cs.IsClientScriptBlockRegistered( this.GetType(), csname ))
        {
            cs.RegisterClientScriptBlock( this.GetType(), csname, sb.ToString(), true );
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        uxCompanyLabel.Text = DataAccessContext.Configurations.GetValue(
            CultureUtilities.StoreCultureID, "CompanyName" );
        uxAddressLabel.Text = DataAccessContext.Configurations.GetValue(
            CultureUtilities.StoreCultureID, "CompanyAddress" );
        uxCityLabel.Text = DataAccessContext.Configurations.GetValue(
            CultureUtilities.StoreCultureID, "CompanyCity" );
        uxStateLabel.Text = DataAccessContext.Configurations.GetValue(
            CultureUtilities.StoreCultureID, "CompanyState" );
        uxZipLabel.Text = DataAccessContext.Configurations.GetValue( "CompanyZip" );
        uxCountryLabel.Text = DataAccessContext.Configurations.GetValue(
            CultureUtilities.StoreCultureID, "CompanyCountry" );
        uxPhoneLabel.Text = DataAccessContext.Configurations.GetValue( "CompanyPhone" );
        uxFaxLabel.Text = DataAccessContext.Configurations.GetValue( "CompanyFax" );
        uxEmailLabel.Text = DataAccessContext.Configurations.GetValue( "CompanyEmail" );
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
            Response.Redirect( "~/ContactUsFinished.aspx" );
        }
        catch (Exception)
        {
            uxErrorMessage.DisplayErrorNoNewLine( "[$SentErrorMessage]" );
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
