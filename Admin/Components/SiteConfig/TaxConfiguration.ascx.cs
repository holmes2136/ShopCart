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
using Vevo.WebAppLib;
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Configurations;
using Vevo.Domain.Tax;
using Vevo.Shared.Utilities;

public partial class AdminAdvanced_Components_SiteConfig_TaxConfiguration : AdminAdvancedBaseUserControl
{
    private void InitTaxClassDrop()
    {
        uxShippingTaxClassDrop.DataSource = DataAccessContext.TaxClassRepository.GetAll( "TaxClassID" );
        uxShippingTaxClassDrop.DataTextField = "TaxClassName";
        uxShippingTaxClassDrop.DataValueField = "TaxClassID";
        uxShippingTaxClassDrop.DataBind();
        uxShippingTaxClassDrop.Items.Insert( 0, new ListItem( "None", "0" ) );
    }

    private void PopulateTaxPercentage()
    {
        uxTaxPercentageIncludedInPriceTR.Visible = ConvertUtilities.ToBoolean( uxTaxIncludedInPriceDrop.SelectedValue );
        uxTaxPercentageIncludedInPriceText.Text = DataAccessContext.Configurations.GetValue( "TaxPercentageIncludedInPrice" );
    }

    private void PopulateShippingTaxClass()
    {
        uxShippingTaxClassTR.Visible = ConvertUtilities.ToBoolean( uxTaxableShippingCostDrop.SelectedValue );
        uxShippingTaxClassDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "ShippingTaxClass" );
    }

    public string ValidationGroup
    {
        get
        {
            if (ViewState["ValidationGroup"] == null)
                return String.Empty;
            else
                return (String) ViewState["ValidationGroup"];
        }
        set { ViewState["ValidationGroup"] = value; }
    }

    public void PopulateControls()
    {
        uxTaxableWholesalerDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "IsTaxableWholesaler" );
        uxTaxableShippingCostDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "IsTaxableShippingCost" );
        uxTaxIncludedInPriceDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "TaxIncludedInPrice" );
        uxTaxPriceAfterDiscountDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "TaxPriceAfterDiscount" );

        PopulateTaxPercentage();
        PopulateShippingTaxClass();
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["IsTaxableWholesaler"],
            uxTaxableWholesalerDrop.SelectedValue );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["IsTaxableShippingCost"],
            uxTaxableShippingCostDrop.SelectedValue );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["TaxIncludedInPrice"],
            uxTaxIncludedInPriceDrop.SelectedValue );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["ShippingTaxClass"],
            uxShippingTaxClassDrop.SelectedValue );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["TaxPriceAfterDiscount"],
            uxTaxPriceAfterDiscountDrop.SelectedValue );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["TaxPercentageIncludedInPrice"],
            uxTaxPercentageIncludedInPriceText.Text.Trim() );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            InitTaxClassDrop();
        }
    }
    protected void uxTaxIncludedInPriceDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        PopulateTaxPercentage();
    }
    protected void uxTaxableShippingCostDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        PopulateShippingTaxClass();
    }
}
