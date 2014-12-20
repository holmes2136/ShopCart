using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Deluxe.Domain.EBay;

public partial class Admin_Components_EBayInternationalShippingDrop : AdminAdvancedBaseUserControl
{
    private string[] UsShippingText = {
      "None",
      "UPS Worldwide Express Plus",
      "UPS Worldwide Express",
      "UPS Worldwide Expedited",
      "UPS Worldwide Saver",
      "UPS Standard To Canada",
      "USPS First Class Mail International",
      "USPS Priority Mail International",
      "USPS Priority Mail International Flat Rate Envelope",
      "USPS Priority Mail International Small Flat Rate Box",
      "USPS Priority Mail International Medium Flat Rate Box",
      "USPS Priority Mail International Large Flat Rate Box",
      "USPS Priority Mail International Padded Flat Rate Envelope",
      "USPS Priority Mail International Legal Flat Rate Envelope",
      "USPS Express Mail International",
      "USPS Express Mail International Flat Rate Envelope",
      "USPS Express Mail International Legal Flat Rate Envelope"};

    private string[] UsShippingValue = {
      "None",
      "UPSWorldWideExpressPlus",
      "UPSWorldWideExpress",
      "UPSWorldWideExpedited",
      "UPSWorldWideSaver",
      "UPSStandardToCanada",
      "USPSFirstClassMailInternational",
      "USPSPriorityMailInternational",
      "USPSPriorityMailInternationalFlatRateEnvelope",
      "USPSPriorityMailInternationalSmallFlatRateBox",
      "USPSPriorityMailInternationalFlatRateBox",
      "USPSPriorityMailInternationalLargeFlatRateBox",
      "USPSPriorityMailInternationalPaddedFlatRateEnvelope",
      "USPSPriorityMailInternationalLegalFlatRateEnvelope",
      "USPSExpressMailInternational",
      "USPSExpressMailInternationalFlatRateEnvelope",
      "USPSExpressMailInternationalLegalFlatRateEnvelope"};

    private string[] UkShippingText = {
     "None",
     "Parcelforce Euro 48",
     "Parcelforce International Datapost",
     "Parcelforce International Scheduled",
     "Parcelforce Ireland 24",
     "Royal Mail Airmail",
     "Royal Mail Airsure",
     "Royal Mail HM Forces Mail",
     "Royal Mail International Signed-for",
     "Royal Mail Surface Mail"     
};

    private string[] UkShippingValue = {
     "None",     
     "UK_ParcelForceEuro48International",
     "UK_ParcelForceInternationalDatapost",
     "UK_ParcelForceInternationalScheduled",
     "UK_ParcelForceIreland24International",
     "UK_RoyalMailAirmailInternational",
     "UK_RoyalMailAirsureInternational",     
     "UK_RoyalMailHMForcesMailInternational",
     "UK_RoyalMailInternationalSignedFor",     
     "UK_RoyalMailSurfaceMailInternational"    
};

    private string[] DeShippingText = {
    "None",    
    "Deutsche Post Brief (International)",
    "Deutsche Post Brief (Land) (International)",
    "Deutsche Post Brief (Luft) (International)",    
    "DHL Packchen (International)",    
    "DHL Paket International",
    "Expressversand (International)",
    "Hermes Paket International",
    "iloxx Transport (International)"
};

    private string[] DeShippingValue = {
    "None",
    "DE_DeutschePostBriefInternational",
    "DE_DeutschePostBriefLandInternational",
    "DE_DeutschePostBriefLuftInternational",
    "DE_DHLPackchenInternational",
    "DE_DHLPaketInternational",
    "DE_ExpressInternational",
    "DE_HermesPaketInternational",
    "DE_IloxxTransportInternational"
};
    private string[] GetShippingText( string listSite )
    {

        switch (listSite)
        {
            case "UK":
                return UkShippingText;
            case "Germany":
                return DeShippingText;
            default:
                return UsShippingText;
        }
    }

    private string[] GetShippingValue( string listSite )
    {
        switch (listSite)
        {
            case "UK":
                return UkShippingValue;
            case "Germany":
                return DeShippingValue;
            default:
                return UsShippingValue;
        }
    }

    private void SetShippingDrop( string[] shippingText, string[] shippingValue )
    {
        uxEBayInternationalShippingDrop.Enabled = true;
        uxEBayInternationalShippingDrop.Items.Clear();
        for (int i = 0; i < shippingText.Length; i++)
        {
            uxEBayInternationalShippingDrop.Items.Add( new ListItem( shippingText[i], shippingValue[i] ) );
        }
    }

    public string SelectedValue
    {
        get
        {
            return uxEBayInternationalShippingDrop.SelectedValue;
        }
        set
        {
            uxEBayInternationalShippingDrop.SelectedValue = value;
        }
    }

    public void PopulateControls( string listSite, bool displayNone )
    {
        SetShippingDrop( GetShippingText( listSite ), GetShippingValue( listSite ) );
        if (!displayNone)
        {
            uxEBayInternationalShippingDrop.Items.Remove( "None" );
        }
        uxEBayInternationalShippingDrop.SelectedIndex = 0;
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void uxEBayInternationalShippingDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        // Send event to parent controls
        OnBubbleEvent( e );
    }
}
