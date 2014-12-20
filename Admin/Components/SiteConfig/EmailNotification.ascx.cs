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
using Vevo.Domain.Configurations;
using Vevo.WebAppLib;

public partial class AdminAdvanced_Components_SiteConfig_EmailNotification : System.Web.UI.UserControl
{
    public void PopulateControls()
    {
        uxNewRegistrationDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "NotifyNewRegistration" );
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["NotifyNewRegistration"],
            uxNewRegistrationDrop.SelectedValue );
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }
}
