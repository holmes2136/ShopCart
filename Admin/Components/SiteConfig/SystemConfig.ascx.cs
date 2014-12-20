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
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Configurations;
using Vevo.WebAppLib;
using Vevo;
using Vevo.Shared.DataAccess;

public partial class AdminAdvanced_Components_SiteConfig_SystemConfig : AdminAdvancedBaseUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
            ThubLogEnabledTR.Visible= false;
    }

    public void PopulateControls()
    {
        uxMoreAjaxErrorMessageDrop.SelectedValue = 
            DataAccessContext.Configurations.GetBoolValue( "ShowDetailAjaxErrorMessage" ).ToString();
        uxThubLogEnabledDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "ThubLogEnabled" );
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["ShowDetailAjaxErrorMessage"],
            uxMoreAjaxErrorMessageDrop.SelectedValue );
        
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["ThubLogEnabled"],
            uxThubLogEnabledDrop.SelectedValue );
    }
}
