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
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;

public partial class AdminAdvanced_Components_SiteConfig_Wholesale : UserControl, IConfigUserControl
{
    public string ValidationGroup
    {
        get
        {
            if (ViewState["ValidationGroup"] == null)
                return string.Empty;
            else
                return (string) ViewState["ValidationGroup"];
        }
        set
        {
            ViewState["ValidationGroup"] = value;
        }
    }

    public void PopulateControls()
    {
        uxWholesaleModeDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "WholesaleMode" ).ToString();
        uxNumberOfWholesaleDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "WholesaleLevel" ).ToString();
        uxDiscountLevel1Text.Text = DataAccessContext.Configurations.GetValue( "DiscountLevel1" ).ToString();
        uxDiscountLevel2Text.Text = DataAccessContext.Configurations.GetValue( "DiscountLevel2" ).ToString();
        uxDiscountLevel3Text.Text = DataAccessContext.Configurations.GetValue( "DiscountLevel3" ).ToString();
        UpdateWholesaleControls();
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["WholesaleMode"],
            uxWholesaleModeDrop.SelectedValue );

        switch (int.Parse( uxWholesaleModeDrop.SelectedValue ))
        {
            case 1:
                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["WholesaleLevel"],
                    uxNumberOfWholesaleDrop.SelectedValue );

                DataAccessContext.CustomerRepository.UpdateWholesalesLevel( int.Parse( uxNumberOfWholesaleDrop.SelectedValue ) );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["DiscountLevel1"],
                    "" );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["DiscountLevel2"],
                    "" );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["DiscountLevel3"],
                    "" );

                ClearDiscountLevels();
                break;
            case 2:
                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["WholesaleLevel"],
                    uxNumberOfWholesaleDrop.SelectedValue );

                DataAccessContext.CustomerRepository.UpdateWholesalesLevel( int.Parse( uxNumberOfWholesaleDrop.SelectedValue ) );

                if (int.Parse( uxNumberOfWholesaleDrop.SelectedValue ) >= 1)
                    DataAccessContext.ConfigurationRepository.UpdateValue(
                        DataAccessContext.Configurations["DiscountLevel1"],
                        uxDiscountLevel1Text.Text );
                else
                {
                    DataAccessContext.ConfigurationRepository.UpdateValue(
                        DataAccessContext.Configurations["DiscountLevel1"],
                        "" );
                    uxDiscountLevel1Text.Text = string.Empty;
                }

                if (int.Parse( uxNumberOfWholesaleDrop.SelectedValue ) >= 2)
                    DataAccessContext.ConfigurationRepository.UpdateValue(
                        DataAccessContext.Configurations["DiscountLevel2"],
                        uxDiscountLevel2Text.Text );
                else
                {
                    DataAccessContext.ConfigurationRepository.UpdateValue(
                        DataAccessContext.Configurations["DiscountLevel2"],
                        "" );
                    uxDiscountLevel2Text.Text = string.Empty;
                }

                if (int.Parse( uxNumberOfWholesaleDrop.SelectedValue ) >= 3)
                    DataAccessContext.ConfigurationRepository.UpdateValue(
                        DataAccessContext.Configurations["DiscountLevel3"],
                        uxDiscountLevel3Text.Text );
                else
                {
                    DataAccessContext.ConfigurationRepository.UpdateValue(
                        DataAccessContext.Configurations["DiscountLevel3"],
                        "" );
                    uxDiscountLevel3Text.Text = string.Empty;
                }
                break;
            default:
                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["WholesaleLevel"],
                    "0" );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["DiscountLevel1"],
                    "" );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["DiscountLevel2"],
                    "" );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["DiscountLevel3"],
                    "" );

                uxNumberOfWholesaleDrop.ClearSelection();
                ClearDiscountLevels();

                break;
        }
    }

    private void ClearDiscountLevels()
    {
        uxDiscountLevel1Text.Text = String.Empty;
        uxDiscountLevel2Text.Text = String.Empty;
        uxDiscountLevel3Text.Text = String.Empty;
    }

    private void UpdateWholesaleControls()
    {
        switch (uxWholesaleModeDrop.SelectedValue)
        {
            case "0":
                uxNumberOfWholesaleLevelTR.Visible = false;
                uxWholesaleWorningTR.Visible = false;
                uxDiscountLevel1TR.Visible = false;
                uxDiscountLevel2TR.Visible = false;
                uxDiscountLevel3TR.Visible = false;
                break;

            case "1":
                uxNumberOfWholesaleLevelTR.Visible = true;
                uxWholesaleWorningTR.Visible = true;
                uxDiscountLevel1TR.Visible = false;
                uxDiscountLevel2TR.Visible = false;
                uxDiscountLevel3TR.Visible = false;
                break;

            case "2":
                uxNumberOfWholesaleLevelTR.Visible = true;
                uxWholesaleWorningTR.Visible = true;
                int levels = ConvertUtilities.ToInt32( uxNumberOfWholesaleDrop.SelectedValue );
                if (levels >= 1)
                {
                    uxDiscountLevel1TR.Visible = true;
                }
                else
                {
                    uxDiscountLevel1TR.Visible = false;
                }

                if (levels >= 2)
                {
                    uxDiscountLevel2TR.Visible = true;
                }
                else
                {
                    uxDiscountLevel2TR.Visible = false;
                }

                if (levels >= 3)
                {
                    uxDiscountLevel3TR.Visible = true;
                }
                else
                {
                    uxDiscountLevel3TR.Visible = false;
                }
                break;
        }

        if (ConvertUtilities.ToInt32( uxNumberOfWholesaleDrop.SelectedValue )
            < DataAccessContext.Configurations.GetIntValue( "WholesaleLevel" ))
            uxWholesaleLevelWarningLabel.Visible = true;
        else
            uxWholesaleLevelWarningLabel.Visible = false;
        uxStatusHidden.Value = "Refresh";
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        //  PopulateControls();
    }

    protected void uxWholesaleModeDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        UpdateWholesaleControls();
    }

    protected void uxNumberOfWholesaleDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        UpdateWholesaleControls();
    }

    #region IConfigUserControl Members

    public void Populate( Vevo.Domain.Configurations.Configuration config )
    {
        // throw new Exception( "The method or operation is not implemented." );
        PopulateControls();
    }

    #endregion
}
