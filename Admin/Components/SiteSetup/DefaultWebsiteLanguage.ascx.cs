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
using Vevo.WebAppLib;
using Vevo.Domain.Stores;

public partial class AdminAdvanced_Components_SiteSetup_DefaultWebsiteLanguage : AdminAdvancedBaseUserControl
{
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

    private void PopulateLanguage()
    {
        uxDefaultLanguageDrop.DataSource = DataAccessContext.CultureRepository.GetAll();
        uxDefaultLanguageDrop.DataBind();

        uxLanguageKeywordBaseCultureDrop.DataSource = DataAccessContext.CultureRepository.GetAll();
        uxLanguageKeywordBaseCultureDrop.DataBind();
    }

    public void PopulateControls()
    {
        uxDefaultLanguageDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "DefaultWebsiteLanguage", CurrentStore );
        uxLanguageKeywordBaseCultureDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "LanguageKeywordBaseCulture", CurrentStore );
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["DefaultWebsiteLanguage"],
            uxDefaultLanguageDrop.SelectedValue, CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["LanguageKeywordBaseCulture"],
            uxLanguageKeywordBaseCultureDrop.SelectedValue, CurrentStore );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            PopulateLanguage();
    }
}
