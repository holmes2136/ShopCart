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
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;

public partial class Components_Common_TooltippedText : System.Web.UI.UserControl
{
    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        uxProductItemParentDiv.Attributes.Add(
            "onmouseover",
            String.Format( "ShowToolTip('{0}','{1}', event)",
                uxProductItemParentDiv.ClientID, uxProductItemDiv.ClientID ) );

        uxProductItemParentDiv.Attributes.Add(
            "onmouseout",
            String.Format( "HideToolTip('{0}', event)", uxProductItemDiv.ClientID ) );
    }

    protected string GetMainText()
    {
        return WebUtilities.ReplaceNewLine( MainText );
    }

    protected string GetTooltipText()
    {
        return WebUtilities.ReplaceNewLine( TooltipText );
    }

    #endregion


    #region Public Methods

    public string MainText
    {
        get { return ConvertUtilities.ToString( ViewState["MainText"] ); }
        set { ViewState["MainText"] = value; }
    }

    public string TooltipText
    {
        get { return ConvertUtilities.ToString( ViewState["TooltipText"] ); }
        set { ViewState["TooltipText"] = value; }
    }

    #endregion

}
