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
public partial class AdminAdvanced_MainControls_ShippingSelectMethod : AdminAdvancedBaseUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void uxNextButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "ShippingAdd.ascx", String.Format( "ShippingOption={0}", uxShippingOptionRadioButtonList.SelectedValue ) );
    }
}
