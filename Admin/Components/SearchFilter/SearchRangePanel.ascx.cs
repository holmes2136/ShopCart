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
using Vevo.Domain.DataInterfaces;
using Vevo.WebAppLib;
using Vevo.Base.Domain;

public partial class AdminAdvanced_Components_SearchFilter_SearchRangePanel : AdminAdvancedBaseUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {

    }

    public string GetLowerTextClientID()
    {
        return uxLowerText.ClientID;
    }

    public string GetUpperTextClientID()
    {
        return uxUpperText.ClientID;
    }

    public string GetLowerValue()
    {
        return uxLowerText.Text;
    }

    public string GetUpperValue()
    {
        return uxUpperText.Text;
    }

    public void RestoreControls( SearchFilter searchFilterObj )
    {
        uxLowerText.Text = searchFilterObj.Value1;
        uxUpperText.Text = searchFilterObj.Value2;
    }

    public void TieTextBoxesWithButtons( Control button )
    {
        WebUtilities.TieButton( this.Page, uxLowerText, button );
        WebUtilities.TieButton( this.Page, uxUpperText, button );
    }
}
