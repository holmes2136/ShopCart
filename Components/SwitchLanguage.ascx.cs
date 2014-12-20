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

public partial class Components_SwitchLanguage : Vevo.WebUI.International.BaseLanguageUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if (String.Compare(
            DataAccessContext.Configurations.GetValue( "LanguageMenuDisplayMode" ), "None", true ) == 0)
        {
            this.Visible = false;
        }
    }
}
