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
using Vevo.WebAppLib;
using Vevo.Shared.DataAccess;

public partial class Components_FindGiftRegistry : Vevo.WebUI.International.BaseLanguageUserControl
{
    private bool IsGiftRegistryDisplay()
    {
        return DataAccessContext.Configurations.GetBoolValue( "GiftRegistryModuleDisplay" )
            && KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName );
    }
    protected void Page_Load( object sender, EventArgs e )
    {
        this.Visible = IsGiftRegistryDisplay();
    }
}
