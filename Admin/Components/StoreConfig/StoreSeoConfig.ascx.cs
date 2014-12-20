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
using Vevo.Domain.Stores;
using Vevo.Domain.Configurations;

public partial class Admin_Components_StoreConfig_StoreSeoConfig : AdminAdvancedBaseUserControl
{
    private Store CurrentStore
    {
        get
        {
            return DataAccessContext.StoreRepository.GetOne( StoreID );
        }
    }

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

    private string CultureID
    {
        get
        {
            return AdminConfig.CurrentContentCultureID;
        }
    }

    private Culture Culture
    {
        get { return DataAccessContext.CultureRepository.GetOne( CultureID ); }
    }

    public void PopulateControls()
    {
        uxDefaultProductPageTitleText.Text = DataAccessContext.Configurations.GetValue( CultureID, "DefaultProductPageTitle", CurrentStore );
        uxDefaultProductMetaKeywordText.Text = DataAccessContext.Configurations.GetValue( CultureID, "DefaultProductMetaKeyword", CurrentStore );
        uxDefaultProductMetaDescriptionText.Text = DataAccessContext.Configurations.GetValue( CultureID, "DefaultProductMetaDescription", CurrentStore );

        uxDefaultCategoryPageTitleText.Text = DataAccessContext.Configurations.GetValue( CultureID, "DefaultCategoryPageTitle", CurrentStore );
        uxDefaultCategoryMetaKeywordText.Text = DataAccessContext.Configurations.GetValue( CultureID, "DefaultCategoryMetaKeyword", CurrentStore );
        uxDefaultCategoryMetaDescriptionText.Text = DataAccessContext.Configurations.GetValue( CultureID, "DefaultCategoryMetaDescription", CurrentStore );

        uxDefaultDepartmentPageTitleText.Text = DataAccessContext.Configurations.GetValue( CultureID, "DefaultDepartmentPageTitle", CurrentStore );
        uxDefaultDepartmentMetaKeywordText.Text = DataAccessContext.Configurations.GetValue( CultureID, "DefaultDepartmentMetaKeyword", CurrentStore );
        uxDefaultDepartmentMetaDescriptionText.Text = DataAccessContext.Configurations.GetValue( CultureID, "DefaultDepartmentMetaDescription", CurrentStore );
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            Culture,
            DataAccessContext.Configurations["DefaultProductPageTitle"],
            uxDefaultProductPageTitleText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           Culture,
           DataAccessContext.Configurations["DefaultProductMetaKeyword"],
           uxDefaultProductMetaKeywordText.Text,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            Culture,
            DataAccessContext.Configurations["DefaultProductMetaDescription"],
            uxDefaultProductMetaDescriptionText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            Culture,
            DataAccessContext.Configurations["DefaultCategoryPageTitle"],
            uxDefaultCategoryPageTitleText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           Culture,
           DataAccessContext.Configurations["DefaultCategoryMetaKeyword"],
           uxDefaultCategoryMetaKeywordText.Text,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            Culture,
            DataAccessContext.Configurations["DefaultCategoryMetaDescription"],
            uxDefaultCategoryMetaDescriptionText.Text,
            CurrentStore );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            Culture,
            DataAccessContext.Configurations["DefaultDepartmentPageTitle"],
            uxDefaultDepartmentPageTitleText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           Culture,
           DataAccessContext.Configurations["DefaultDepartmentMetaKeyword"],
           uxDefaultDepartmentMetaKeywordText.Text,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            Culture,
            DataAccessContext.Configurations["DefaultDepartmentMetaDescription"],
            uxDefaultDepartmentMetaDescriptionText.Text,
            CurrentStore );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }


}
