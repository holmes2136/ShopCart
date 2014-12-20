using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.Domain.Stores;

public partial class Admin_Components_CurrencyControl : AdminAdvancedBaseUserControl, ICurrencyControl
{
    private void PopulateControls()
    {
        uxDrop.DataSource = DataAccessContext.CurrencyRepository.GetByEnabled( BoolFilter.ShowTrue );
        uxDrop.DataBind();

        LoadDefaultFromQuery();
    }

    private void LoadDefaultFromQuery()
    {
        if (!String.IsNullOrEmpty( MainContext.QueryString["CurrencyCode"] ))
            CurrentCurrencyCode = MainContext.QueryString["CurrencyCode"];
        else
            CurrentCurrencyCode = DataAccessContext.CurrencyRepository.GetOne(
                DataAccessContext.Configurations.GetValueNoThrow( "DefaultDisplayCurrencyCode",
                DataAccessContext.StoreRepository.GetOne( StoreID ) ) ).CurrencyCode;

        uxDrop.SelectedValue = CurrentCurrencyCode;
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        //UpdateLanguageHidden();

        if (!MainContext.IsPostBack)
        {
            PopulateControls();
        }
      
    }


    #region ICurrencyControl Members

    public string CurrentCurrencyCode
    {
        get
        {
            return uxDrop.SelectedValue;
        }
        set
        {
            uxDrop.SelectedValue = value;
        }
    }

    #endregion

    protected void uxDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        CurrentCurrencyCode = uxDrop.SelectedValue;

        // Send event to parent controls
        OnBubbleEvent( e );
    }

    public Currency CurrentCurrency
    {
        get
        {
            return DataAccessContext.CurrencyRepository.GetOne( CurrentCurrencyCode );
        }
    }

    public string StoreID
    {
        get
        {
            if (ViewState["StoreID"] == null)
                return Store.RegularStoreID;
            else
                return (string) ViewState["StoreID"];
        }
        set
        {
            ViewState["StoreID"] = value;
            PopulateControls();
        }
    }


    #region IAdvancedPostbackControl Members

    public void UpdateBrowseQuery( UrlQuery urlQuery )
    {
        if (!String.IsNullOrEmpty( CurrentCurrencyCode ))
            urlQuery.AddQuery( "CurrencyCode", CurrentCurrencyCode );
        else
            urlQuery.RemoveQuery( "CurrencyCode" );
    }

    #endregion


}
