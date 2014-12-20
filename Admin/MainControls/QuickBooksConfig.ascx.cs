using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Stores;
using Vevo.WebAppLib;
using Vevo.Shared.DataAccess;

public partial class AdminAdvanced_MainControls_QuickBooksConfig : AdminAdvancedBaseUserControl
{
    #region private

    private void PopulateControls()
    {
        if (!IsAdminModifiable())
        {
            uxUpdateButton.Visible = false;
        }

            uxAccountNameText.Text = DataAccessContext.Configurations.GetValue( "QBIncomeAccountName" ).ToString();
            uxTaxItemText.Text = DataAccessContext.Configurations.GetValue( "QBTaxItem" ).ToString();
            uxDiscountItemText.Text = DataAccessContext.Configurations.GetValue( "QBDiscountItem" ).ToString();
            uxShippingItemText.Text = DataAccessContext.Configurations.GetValue( "QBShippingItem" ).ToString();
            uxCustomerNameDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "QBCustomerNameFormat" ).ToString();
            uxAdminAccountText.Text = DataAccessContext.Configurations.GetValue( "QBVevoAdminUser" ).ToString();
            uxStoreDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "QBSelectStoreExport" ).ToString();
            uxEnableCustomerModifyDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "QBEnableCustomerModify" );
            uxEnableProductModifyDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "QBEnableProductModify" );
    }

    private void Update()
    {
        try
        {
            DataAccessContext.ConfigurationRepository.UpdateValue(
                DataAccessContext.Configurations["QBIncomeAccountName"],
                uxAccountNameText.Text.Trim() );
            DataAccessContext.ConfigurationRepository.UpdateValue(
                DataAccessContext.Configurations["QBTaxItem"],
                uxTaxItemText.Text.Trim() );
            DataAccessContext.ConfigurationRepository.UpdateValue(
                DataAccessContext.Configurations["QBDiscountItem"],
                uxDiscountItemText.Text.Trim() );
            DataAccessContext.ConfigurationRepository.UpdateValue(
                DataAccessContext.Configurations["QBShippingItem"],
                uxShippingItemText.Text.Trim() );
            DataAccessContext.ConfigurationRepository.UpdateValue(
                DataAccessContext.Configurations["QBCustomerNameFormat"],
                uxCustomerNameDrop.SelectedValue );
            DataAccessContext.ConfigurationRepository.UpdateValue(
                DataAccessContext.Configurations["QBVevoAdminUser"],
                uxAdminAccountText.Text.Trim() );
            DataAccessContext.ConfigurationRepository.UpdateValue(
                DataAccessContext.Configurations["QBSelectStoreExport"],
                uxStoreDrop.SelectedValue );
            DataAccessContext.ConfigurationRepository.UpdateValue(
                DataAccessContext.Configurations["QBEnableCustomerModify"], "False" );
            //uxEnableCustomerModifyDrop.SelectedValue );
            DataAccessContext.ConfigurationRepository.UpdateValue(
                DataAccessContext.Configurations["QBEnableProductModify"], "False" );
            //uxEnableProductModifyDrop.SelectedValue );

            SystemConfig.Load();
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    private void OpenLog()
    {
        MainContext.RedirectMainControl( "QuickBooksLog.ascx" );
    }

    private void PopulateStoreFilterDropDown()
    {
        IList<Store> stores = DataAccessContext.StoreRepository.GetAll( "StoreID" );

        uxStoreDrop.Items.Clear();
        foreach (Store store in stores)
        {
            uxStoreDrop.Items.Add( new ListItem( store.StoreName, store.StoreID ) );
        }
    }

    #endregion

    #region protected

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
            MainContext.RedirectMainControl( "Default.ascx", String.Empty );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateStoreFilterDropDown();
            PopulateControls();
        }

        if (!KeyUtilities.IsMultistoreLicense())
            uxStoreTR.Visible = false;
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        Update();
        uxMessage.DisplayMessage( Resources.SetupMessages.UpdateSuccess );
    }

    protected void uxViewLogButton_Click( object sender, EventArgs e )
    {
        OpenLog();
    }

    #endregion
}
