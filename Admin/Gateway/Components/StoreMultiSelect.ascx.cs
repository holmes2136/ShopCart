using System;
using System.Collections.Generic;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Stores;
using Vevo.Domain.Payments;
using System.Web.UI.WebControls;
using System.ComponentModel;

public partial class Admin_Gateway_Components_StoreMultiSelect : AdminAdvancedBaseUserControl
{
    private string _paymentName = string.Empty;

    private CheckBox GetUseOptionCheck( GridViewRow row )
    {
        return (CheckBox) row.FindControl( "uxEnableStoreCheck" );
    }

    protected void PopulateStoreList()
    {
        IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "StoreID" );
        uxStoreGrid.DataSource = storeList;
        uxStoreGrid.DataBind();
        
        if (!KeyUtilities.IsMultistoreLicense())
            uxStoreListDiv.Visible = false;

        IList<PaymentOptionStore> poStoreList = DataAccessContext.PaymentOptionRepository.GetAllPaymentOptionStoreByName( PaymentName );
        if (poStoreList == null) return;
        foreach (PaymentOptionStore poStore in poStoreList)
        {
            for (int i = 0; i < uxStoreGrid.Rows.Count; i++)
            {
                string rowStoreID = uxStoreGrid.DataKeys[i].Value.ToString();
                GridViewRow row = uxStoreGrid.Rows[i];
                CheckBox checkbox = GetUseOptionCheck( row );
                if (poStore.StoreID == rowStoreID)
                {
                    checkbox.Checked = poStore.IsEnabled;
                }
            }
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateStoreList();
        }
    }

    public void UpdateStoreList()
    {
        DataAccessContext.PaymentOptionRepository.DeletePaymentOptionStoreByName( PaymentName );
        for (int i = 0; i < uxStoreGrid.Rows.Count; i++)
        {
            PaymentOptionStore poStore = new PaymentOptionStore();
            poStore.Name = PaymentName;
            poStore.StoreID = uxStoreGrid.DataKeys[i].Value.ToString();
            poStore.IsEnabled = GetUseOptionCheck( uxStoreGrid.Rows[i] ).Checked;
            DataAccessContext.PaymentOptionRepository.CreatePaymentOptionStore( poStore );
        }
    }

    public string PaymentName
    {
        get { return _paymentName; }
        set { _paymentName = value; }
    }
}