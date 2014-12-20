using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI;

public partial class Components_CurrencyControl : Vevo.WebUI.International.BaseLanguageUserControl
{
    private string _currencyName;

    private bool IsStoreCurrencyInsideList( IList<Currency> currencyList )
    {
        for (int i = 0; i < currencyList.Count; i++)
        {
            if (currencyList[i].CurrencyCode == CurrencyUtilities.StoreCurrencyCode)
            {
                _currencyName = currencyList[i].Name;
                return true;
            }
        }
        return false;
    }

    private void PopulateControls()
    {
        IList<Currency> list = DataAccessContext.CurrencyRepository.GetByEnabled( BoolFilter.ShowTrue );

        uxCurrencyDataList.Items.Clear();
        MenuItem topItem = new MenuItem();

        if (IsStoreCurrencyInsideList( list ))
            topItem.Text = _currencyName;
        else
            topItem.Text = list[0].Name;

        topItem.Value = CurrencyUtilities.StoreCurrencyCode;
        foreach (Currency currency in list)
        {
            MenuItem childrenItem = NewItem( currency );
            topItem.ChildItems.Add( childrenItem );
        }

        uxCurrencyDataList.Items.Add( topItem );
    }

    private MenuItem NewItem( Currency currency )
    {
        MenuItem newItem = new MenuItem();
        newItem.Text = currency.Name;
        newItem.Value = currency.CurrencyCode;

        return newItem;
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "CurrencyModuleDisplay" ))
        {
            this.Visible = false;
        }
        else
        {
            PopulateControls();
        }
    }

    protected void uxCurrencyDataList_OnMenuItemClick( object sender, EventArgs e )
    {
        CurrencyUtilities.StoreCurrencyCode = uxCurrencyDataList.SelectedValue;
        GetStorefrontEvents().OnStoreCurrencyChanged(
            this,
            new CurrencyEventArgs( uxCurrencyDataList.SelectedValue ) );
    }
}
