using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;

public partial class Admin_Components_EBayDomesticShippingDrop : AdminAdvancedBaseUserControl
{
    private string[] UsShippingText = {
      "None",
      "Local PickUp",
      "UPS Ground",
      "UPS 3 Day Select",
      "UPS 2nd Day Air",
      "UPS Next Day Air Saver",
      "UPS Next Day Air",
      "USPS Priority Mail",
      "USPS Priority Mail Flat Rate Envelope",
      "USPS Priority Mail Small Flat Rate Box",
      "USPS Priority Mail Medium Flat Rate Box",
      "USPS Priority Mail Large Flat Rate Box",
      "USPS Priority Mail Padded Flat Rate Envelope",
      "USPS Priority Mail Legal Flat Rate Envelope",
      "USPS First Class Mail",
      "USPS Parcel Post",
      "USPS Media Mail",
      "USPS Express Mail",
      "USPS Express Mail Flat Rate Envelope",
      "USPS Express Mail Legal Flat Rate Envelope"};

    private string[] UsShippingValue = {
      "None",
      "LocalDelivery",
      "UPSGround",
      "UPS3rdDay",
      "UPS2ndDay",
      "UPSNextDay",
      "UPSNextDayAir",
      "USPSPriority",
      "USPSPriorityFlatRateEnvelope",
      "USPSPriorityMailSmallFlatRateBox",
      "USPSPriorityFlatRateBox",
      "USPSPriorityMailLargeFlatRateBox",
      "USPSPriorityMailPaddedFlatRateEnvelope",
      "USPSPriorityMailLegalFlatRateEnvelope",
      "USPSFirstClass",
      "USPSParcel",
      "USPSMedia",
      "USPSExpressMail",
      "USPSExpressFlatRateEnvelope",
      "USPSExpressMailLegalFlatRateEnvelope"};

    private string[] UkShippingText = {
     "None",
     "Collection in Person",
     "Economy Delivery from outside UK",
     "Express Delivery from outside UK",
     "myHermes - door to door service",
     "Parcelforce 24",
     "Parcelforce 48",
     "Royal Mail 1st Class Recorded",
     "Royal Mail 1st Class Standard",
     "Royal Mail 2nd Class Recorded",
     "Royal Mail 2nd Class Standard",
     "Royal Mail Special Delivery 9:00 am", 
     "Royal Mail Special Delivery Next Day",
     "Royal Mail Standard Parcels",
     "Standard Delivery from outside UK"
};

    private string[] UkShippingValue = {
     "None",
     "UK_CollectInPerson",
     "UK_EconomyShippingFromOutside",
     "UK_ExpeditedShippingFromOutside",
     "UK_myHermesDoorToDoorService",
     "UK_Parcelforce24",
     "UK_Parcelforce48",
     "UK_RoyalMailFirstClassRecorded",
     "UK_RoyalMailFirstClassStandard",
     "UK_RoyalMailSecondClassRecorded",
     "UK_RoyalMailSecondClassStandard",
     "UK_RoyalMailSpecialDelivery9am", 
     "UK_RoyalMailSpecialDeliveryNextDay",
     "UK_RoyalMailStandardParcel",
     "UK_StandardShippingFromOutside"
};

    private string[] DeShippingText = {
    "None",
    "Abholung",
    "Deutsche Post Brief",
    "Deutsche Post Buchersendung",
    "DHL Packchen",
    "DHL Paket",
    "eBay Hermes Paket Sperrgut Shop2Shop (Abgabe und Zustellung im Paketshop)",
    "Einschreiben (inkl. aller Gebuehren)",
    "Hermes Paket",
    "Hermes Paket Sperrgut",
    "iloxx Transport",
    "Nachnahme (inkl. aller Gebuehren)",
    "Paketversand",
    "Sonstige (Siehe Artikelbeschreibung)",
    "Sonstige",
    "Sonderversand"
};

    private string[] DeShippingValue = {
    "None",
    "DE_Pickup",
    "DE_DeutschePostBrief",
    "DE_DeutschePostBuchersendung",
    "DE_DHLPackchen",
    "DE_DHLPaket",
    "DE_eBayHermesPaketSperrgutShop2Shop",
    "DE_Einschreiben",
    "DE_HermesPaket",
    "DE_HermesPaketSperrgut",
    "DE_IloxxTransport",
    "DE_Nachname",
    "DE_Paket",
    "DE_Sonstige",
    "DE_SonstigeDomestic",
    "DE_SpecialDelivery",
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
        uxEBayDomesticShippingDrop.Enabled = true;
        uxEBayDomesticShippingDrop.Items.Clear();        
        for (int i = 0; i < shippingText.Length; i++)
        {
            uxEBayDomesticShippingDrop.Items.Add( new ListItem( shippingText[i], shippingValue[i] ) );
        }
    }

    public string SelectedValue
    {
        get
        {
            return uxEBayDomesticShippingDrop.SelectedValue;
        }
        set
        {
            uxEBayDomesticShippingDrop.SelectedValue = value;
        }
    }

    public void PopulateControls( string listSite, bool displayNone )
    {
        SetShippingDrop( GetShippingText( listSite ), GetShippingValue( listSite ) );
        if (!displayNone)
        {
            uxEBayDomesticShippingDrop.Items.Remove( "None" );
        }
        uxEBayDomesticShippingDrop.SelectedIndex = 0;
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }
    protected void uxEBayDomesticShippingDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        // Send event to parent controls
        OnBubbleEvent( e );
    }

}
