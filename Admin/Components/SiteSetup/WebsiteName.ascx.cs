using System;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;

public partial class AdminAdvanced_Components_SiteSetup_WebsiteName : AdminAdvancedBaseUserControl
{
    private Culture Culture
    {
        get { return DataAccessContext.CultureRepository.GetOne( CultureID ); }
    }

    private string StoreID
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

    protected void uxEnableSiteGreetingTextDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        uxSiteGreetingTextDiv.Visible = ConvertUtilities.ToBoolean( uxEnableSiteGreetingTextDrop.SelectedValue );
    }

    protected void uxEnableAccountGreetingTextDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        uxAccountGreetingTextDiv.Visible = ConvertUtilities.ToBoolean( uxEnableAccountGreetingTextDrop.SelectedValue );
    }

    public string CultureID
    {
        get
        {
            if (ViewState["CultureID"] == null)
                return String.Empty;
            else
                return (String) ViewState["CultureID"];
        }
        set { ViewState["CultureID"] = value; }
    }

    public void PopulateControl()
    {
        uxWebsiteTitleText.Text = DataAccessContext.Configurations.GetValue( CultureID, "Title", CurrentStore );
        uxSiteNameText.Text = DataAccessContext.Configurations.GetValue( CultureID, "SiteName", CurrentStore );
        uxSiteDescriptionText.Text = DataAccessContext.Configurations.GetValue( CultureID, "SiteDescription", CurrentStore );
        uxSiteKeywordText.Text = DataAccessContext.Configurations.GetValue( CultureID, "Sitekeyword", CurrentStore );
        uxEnableSiteGreetingTextDrop.SelectedValue = DataAccessContext.Configurations.GetValue( CultureID, "EnableSiteGreetingText", CurrentStore );
        uxSiteGreetingTextbox.Text = DataAccessContext.Configurations.GetValue( CultureID, "SiteGreetingText", CurrentStore );
        uxEnableAccountGreetingTextDrop.SelectedValue = DataAccessContext.Configurations.GetValue( CultureID, "EnableAccountGreetingText", CurrentStore );
        uxAccountGreetingTextbox.Text = DataAccessContext.Configurations.GetValue( CultureID, "AccountGreetingText", CurrentStore );
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            Culture,
            DataAccessContext.Configurations["SiteName"],
            uxSiteNameText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           Culture,
           DataAccessContext.Configurations["Title"],
           uxWebsiteTitleText.Text,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            Culture,
            DataAccessContext.Configurations["SiteDescription"],
            uxSiteDescriptionText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            Culture,
            DataAccessContext.Configurations["SiteKeyword"],
            uxSiteKeywordText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           Culture,
           DataAccessContext.Configurations["EnableSiteGreetingText"],
           uxEnableSiteGreetingTextDrop.SelectedValue,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           Culture,
           DataAccessContext.Configurations["SiteGreetingText"],
           uxSiteGreetingTextbox.Text,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           Culture,
           DataAccessContext.Configurations["EnableAccountGreetingText"],
           uxEnableAccountGreetingTextDrop.SelectedValue,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           Culture,
           DataAccessContext.Configurations["AccountGreetingText"],
           uxAccountGreetingTextbox.Text,
           CurrentStore );
    }
}
