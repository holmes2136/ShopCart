using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO;
using GCheckout.MerchantCalculation;
using GCheckout.Util;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Payments.Google;
using Vevo.WebAppLib;

public partial class GoogleCallBack : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private void GoogleCallBack_PreInit( object sender, EventArgs e )
    {
        Page.Theme = String.Empty;
    }

    public GoogleCallBack()
    {
        PreInit += new EventHandler( GoogleCallBack_PreInit );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        try
        {
            // Extract the XML from the request.
            Stream RequestStream = Request.InputStream;
            StreamReader RequestStreamReader = new StreamReader( RequestStream );
            string RequestXml = RequestStreamReader.ReadToEnd();

            RequestStream.Close();

            //Log.Debug( "Request XML: " + RequestXml );

            // Process the incoming XML.
            CallbackProcessor p = new CallbackProcessor( new VevoCallBackRules() );
            byte[] ResponseXML = p.Process( RequestXml );

            Log.Debug( "Response XML: " + EncodeHelper.Utf8BytesToString( ResponseXML ) );

            string responseOld = EncodeHelper.Utf8BytesToString( ResponseXML );
            if (DataAccessContext.Configurations.GetValue( "PaymentCurrency" ) != "USD")
                responseOld = responseOld.Replace( "currency=\"USD\"",
                    "currency=\"" + DataAccessContext.Configurations.GetValue( "PaymentCurrency" ) + "\"" );

            byte[] result = EncodeHelper.StringToUtf8Bytes( responseOld );

            Response.BinaryWrite( result );
            //Response.BinaryWrite( ResponseXML );
        }
        catch (Exception ex)
        {
            Log.Debug( ex.ToString() );
            throw;
        }
    }

    public override string StyleSheetTheme
    {
        get
        {
            return String.Empty;
        }
    }
}
