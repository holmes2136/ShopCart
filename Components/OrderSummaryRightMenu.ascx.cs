using System;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class Components_OrderSummaryRightMenu : BaseLanguageUserControl
{
    private void PanelVisible( decimal cost, Panel control, Label display )
    {
        if ( cost == 0 )
            control.Visible = false;
        else
        {
            control.Visible = true;
            display.Text = StoreContext.Currency.FormatPrice( cost );

        }
    }

    private decimal ExtractCostFromShippingMethodText( string listItemName )
    {
        if (String.IsNullOrEmpty( listItemName ))
            return 0;
        int index = listItemName.LastIndexOf( "(" ) + 2;
        int length = listItemName.Length - 1;
        string number = listItemName.Substring( index, length - index );
        if (index > 0)
            return decimal.Parse( number.Trim() );
        else
            return 0;
    }

    private string ExtractNameFromListItemText( string listItemName )
    {
        int index = listItemName.LastIndexOf( "(" );
        if (index > 0)
            return listItemName.Substring( 0, index ).Trim();
        else
            return listItemName;
    }

    private void PopulateControls()
    {
        OrderAmount orderAmount = StoreContext.GetOrderAmount();

        uxSubTotalValueLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.Subtotal );
        uxTaxValueLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.Tax );

        decimal shippingCost = 0;

        if (orderAmount.ShippingCost > 0)
        {
            shippingCost = orderAmount.ShippingCost;
            uxTotalValueLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.Total );
        }
        else
        {
            uxTotalValueLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.Total + shippingCost );
        }
        uxShippingValueLabel.Text = StoreContext.Currency.FormatPrice( shippingCost );

        PanelVisible( orderAmount.Discount * -1, uxDiscountPanel, uxDiscountValueLabel );
        PanelVisible( orderAmount.GiftCertificate * -1, uxGiftCertificatePanel, uxGiftCertificateValueLabel );
        
        if( DataAccessContext.Configurations.GetBoolValue( "HandlingFeeEnabled" ))
            PanelVisible( orderAmount.HandlingFee, uxHandlingFeePanel, uxHandlingFeeValueLabel );
        else
            PanelVisible( 0, uxHandlingFeePanel, uxHandlingFeeValueLabel );

        if ( DataAccessContext.Configurations.GetBoolValue( "PointSystemEnabled", StoreContext.CurrentStore ) )
            PanelVisible( orderAmount.PointDiscount * -1, uxPointDiscountPanel, uxPointDiscountValueLabel );
        else
            PanelVisible( 0, uxPointDiscountPanel, uxPointDiscountValueLabel );
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }
}