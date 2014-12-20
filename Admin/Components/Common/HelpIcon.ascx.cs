using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using System.Text;
using Vevo.Domain.Configurations;
using Vevo.WebUI;

public partial class AdminAdvanced_Components_Common_HelpIcon : AdminAdvancedBaseUserControl
{
    private string _configName = String.Empty;
    private string _helpKeyName = String.Empty;
    private void PopulateControl()
    {
        string tooltipMsg = "";
        if (ConfigName != String.Empty)
        {
            tooltipMsg = DataAccessContext.Configurations[ConfigName].Descriptions[0].Description;

        }
        else if (HelpKeyName != String.Empty)
        {
            tooltipMsg = DataAccessContext.HelpRepository.GetOne( StoreContext.Culture, HelpKeyName ).HelpText;
        }

        if (tooltipMsg != "")
        {
            StringBuilder sb = new StringBuilder();
            string objectName = uxHelpToolTipPanel.ClientID;
            sb.Append( String.Format( "$('#{0}')", objectName ) );
            sb.Append( ".simpletip({ " );
            sb.Append( String.Format( "content: '{0}',", tooltipMsg ) );
            sb.Append( "position:'right'," );
            sb.Append( "offset:[10,30]" );
            sb.Append( "});" );
            uxHelpImage.Attributes.Add( "onload", sb.ToString() );
        }
        else
            uxHelpToolTipPanel.Visible = false;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControl();
    }

    // This is for help text from ConfigurationDescription database table.
    public string ConfigName
    {
        set
        {
            _configName = value;
            _helpKeyName = String.Empty;
        }
        get { return _configName; }
    }

    // This is for help text from Help database table.
    public string HelpKeyName
    {
        set
        {
            _helpKeyName = value;
            _configName = String.Empty;
        }
        get { return _helpKeyName; }
    }
}
