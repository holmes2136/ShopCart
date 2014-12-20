using System;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;

public partial class AdminAdvanced_Components_SiteSetup_BusinessProfile : AdminAdvancedBaseUserControl
{
    #region Private

    private Culture Culture
    {
        get
        {
            return DataAccessContext.CultureRepository.GetOne( CultureID );
        }
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

    private void SetLatitudeAndLongitudeVisible()
    {
        uxLatitudeTextDiv.Visible = ConvertUtilities.ToBoolean( uxEnableMapDrop.SelectedValue );
        uxLongitudeTextDiv.Visible = ConvertUtilities.ToBoolean( uxEnableMapDrop.SelectedValue );
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        uxRequiredEmailValidator.ValidationGroup = ValidationGroup;
        uxRegularEmailValidator.ValidationGroup = ValidationGroup;
        uxRequiredLatitudeValidator.ValidationGroup = ValidationGroup;
        uxCompareLatitudeValidator.ValidationGroup = ValidationGroup;
        uxRequiredLongitudeValidator.ValidationGroup = ValidationGroup;
        uxCompareLongitudeValidator.ValidationGroup = ValidationGroup;
    }

    protected void uxEnableMapDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        uxLatitudeTextDiv.Visible = ConvertUtilities.ToBoolean( uxEnableMapDrop.SelectedValue );
        uxLongitudeTextDiv.Visible = ConvertUtilities.ToBoolean( uxEnableMapDrop.SelectedValue );
    }

    #endregion


    #region Public Properties

    public string CultureID
    {
        get
        {
            if (ViewState["CultureID"] == null)
                return String.Empty;
            else return (String) ViewState["CultureID"]; ;
        }
        set { ViewState["CultureID"] = value; }
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

    #endregion


    #region Public Methods

    public void PopulateControl()
    {
        uxCompanyNameText.Text = DataAccessContext.Configurations.GetValue( CultureID, "CompanyName", CurrentStore );
        uxCompanyAddressText.Text = DataAccessContext.Configurations.GetValue( CultureID, "CompanyAddress", CurrentStore );
        uxCompanyCityText.Text = DataAccessContext.Configurations.GetValue( CultureID, "CompanyCity", CurrentStore );
        uxCompanyStateText.Text = DataAccessContext.Configurations.GetValue( CultureID, "CompanyState", CurrentStore );
        uxCompanyZipText.Text = DataAccessContext.Configurations.GetValue( "CompanyZip", CurrentStore );
        uxCompanyCountryText.Text = DataAccessContext.Configurations.GetValue( CultureID, "CompanyCountry", CurrentStore );
        uxCompanyPhoneText.Text = DataAccessContext.Configurations.GetValue( "CompanyPhone", CurrentStore );
        uxCompanyFaxText.Text = DataAccessContext.Configurations.GetValue( "CompanyFax", CurrentStore );
        uxCompanyEmailText.Text = DataAccessContext.Configurations.GetValue( "CompanyEmail", CurrentStore );
        uxEnableMapDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "EnableMap", CurrentStore );
        uxLatitudeText.Text = DataAccessContext.Configurations.GetValue( "Latitude", CurrentStore );
        uxLongitudeText.Text = DataAccessContext.Configurations.GetValue( "Longitude", CurrentStore );
        SetLatitudeAndLongitudeVisible();
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            Culture,
            DataAccessContext.Configurations["CompanyName"],
            uxCompanyNameText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            Culture,
            DataAccessContext.Configurations["CompanyAddress"],
            uxCompanyAddressText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            Culture,
            DataAccessContext.Configurations["CompanyCity"],
            uxCompanyCityText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            Culture,
            DataAccessContext.Configurations["CompanyState"],
            uxCompanyStateText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CompanyZip"],
            uxCompanyZipText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            Culture,
            DataAccessContext.Configurations["CompanyCountry"],
            uxCompanyCountryText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CompanyPhone"],
            uxCompanyPhoneText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CompanyFax"],
            uxCompanyFaxText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CompanyEmail"],
            uxCompanyEmailText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["EnableMap"],
            uxEnableMapDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["Latitude"],
            uxLatitudeText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["Longitude"],
            uxLongitudeText.Text,
            CurrentStore );

    }

    #endregion
}
