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
using Vevo.Domain;
using Vevo.DataAccessLib;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;

public partial class AdminAdvanced_Components_SiteConfig_Seo : AdminAdvancedBaseUserControl
{
    private void RefreshUrlCultureNameDrop()
    {
        uxUrlCultureNameTR.Visible = ConvertUtilities.ToBoolean( uxUseSimpleCatalogUrlDrop.SelectedValue );
    }

    private void PopulateLanguage()
    {
        uxUrlCultureNameDrop.DataSource = DataAccessContext.CultureRepository.GetAll();
        uxUrlCultureNameDrop.DataBind();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            PopulateLanguage();
    }

    protected void uxUseSimpleCatalogUrlDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        RefreshUrlCultureNameDrop();
    }


    public void PopulateControls()
    {
        uxUseSimpleCatalogUrlDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue( "UseSimpleCatalogUrl" ).ToString();
        uxUrlCultureNameDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "UrlCultureName" );

        RefreshUrlCultureNameDrop();
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["UseSimpleCatalogUrl"],
            uxUseSimpleCatalogUrlDrop.SelectedValue );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["UrlCultureName"],
            uxUrlCultureNameDrop.SelectedValue );
    }

}
