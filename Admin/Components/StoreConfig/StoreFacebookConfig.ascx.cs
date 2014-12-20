using System;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Stores;

public partial class Admin_Components_StoreConfig_StoreFacebookConfig : AdminAdvancedBaseUserControl
{
    public string StoreID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
            {
                return MainContext.QueryString["StoreID"];
            }
            else
            {
                return Store.RegularStoreID;
            }
        }
    }

    public void PopulateControls()
    {
        uxWidgetLikeBoxConfig.PopulateControls( CurrentStore );
        
        uxEnableFacebookLoginDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "FacebookLoginEnabled", CurrentStore ).ToString();
        uxFacebookAPIKeyTextbox.Text =
            DataAccessContext.Configurations.GetValue( "FacebookAPIKey", CurrentStore ).ToString();
        uxFacebookAppSecretTextbox.Text =
            DataAccessContext.Configurations.GetValue( "FacebookAppSecret", CurrentStore ).ToString();
        uxFacebookAccessTokenTextbox.Text =
            DataAccessContext.Configurations.GetValue( "FacebookAccessToken", CurrentStore ).ToString();
    }

    public void Update()
    {
        uxWidgetLikeBoxConfig.Update( CurrentStore );
        DataAccessContext.ConfigurationRepository.UpdateValue(
              DataAccessContext.Configurations["FacebookLoginEnabled"],
              uxEnableFacebookLoginDrop.SelectedValue,
              CurrentStore );
        DataAccessContext.ConfigurationRepository.UpdateValue(
              DataAccessContext.Configurations["FacebookAPIKey"],
              uxFacebookAPIKeyTextbox.Text,
              CurrentStore );
        DataAccessContext.ConfigurationRepository.UpdateValue(
              DataAccessContext.Configurations["FacebookAppSecret"],
              uxFacebookAppSecretTextbox.Text,
              CurrentStore );
        DataAccessContext.ConfigurationRepository.UpdateValue(
              DataAccessContext.Configurations["FacebookAccessToken"],
              uxFacebookAccessTokenTextbox.Text,
              CurrentStore );

        PopulateControls();
    }

    private Store CurrentStore
    {
        get
        {
            return DataAccessContext.StoreRepository.GetOne( StoreID );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            PopulateControls();

        if (uxEnableFacebookLoginDrop.SelectedValue == "True")
            uxFacebookParameter.Visible = true;
        else
            uxFacebookParameter.Visible = false;
    }
}
