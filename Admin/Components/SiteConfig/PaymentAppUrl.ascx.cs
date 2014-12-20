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
using Vevo.WebAppLib;
using Vevo.Domain;

public partial class AdminAdvanced_Components_SiteConfig_PaymentAppUrl : UserControl, IConfigUserControl
{

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    public string ValidationGroup
    {
        get
        {
            if (ViewState["ValidationGroup"] == null)
                return string.Empty;
            else
                return (string) ViewState["ValidationGroup"];
        }
        set
        {
            ViewState["ValidationGroup"] = value;
        }
    }

    public void PopulateControls()
    {
        uxPaymentAppUrlText.Text = DataAccessContext.Configurations.GetValue( "PaymentAppUrl" );
    }

    #region IConfigUserControl Members    

    public void Populate( Vevo.Domain.Configurations.Configuration config )
    {
        PopulateControls();
    }

    public void Update()
    {
        string url = uxPaymentAppUrlText.Text;

        if (url.Substring( 0, "http://".Length ).ToLower().Contains( "http://" ))
        {
            url = url.Substring( "http://".Length );
        }
        else if (url.Substring( 0, "https://".Length ).ToLower().Contains( "https://" ))
        {
            url = url.Substring( "https://".Length );
        }

        if (url.Substring( url.Length - 1, 1 ).ToLower().Contains( "/" ))
        {
            url = url.Substring( 0, url.Length - 1 );
        }

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PaymentAppUrl"],
            url );
    }

    #endregion
}
