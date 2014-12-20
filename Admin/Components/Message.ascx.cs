using System;
using System.Data;
using System.Drawing;
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


public partial class AdminAdvanced_Components_Message : System.Web.UI.UserControl, Vevo.WebUI.IMessageControl
{
    #region Private

    private const string NewLineText = "";
    private void ControlVisible( bool value )
    {
        if (value)
            uxMessagePanel.CssClass = "MessagePanel";
        else
            uxMessagePanel.CssClass = "dn";
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        ControlVisible( false );
    }

    #endregion


    #region Public Properties

    public String MessageControlID
    {
        get
        {
            return uxMessageLabel.ClientID;
        }
    }

    #endregion


    #region Public Methods

    public void DisplayMessage( string text, params object[] parameters )
    {
        DisplayMessageNoNewLIne( text + NewLineText, parameters );
    }

    public void DisplayMessageNoNewLIne( string text, params object[] parameters )
    {
        uxMessageLabel.Text = String.Format( text, parameters );
        uxMessageLabel.CssClass = "MessageSuccess";
        ControlVisible( true );
    }

    public void DisplayError( string text, params object[] parameters )
    {
        DisplayErrorNoNewLine( text + NewLineText, parameters );
    }

    public void DisplayErrorNoNewLine( string text, params object[] parameters )
    {
        uxMessageLabel.Text = String.Format( text, parameters );
        uxMessageLabel.CssClass = "MessageError"; 
        ControlVisible( true );
    }

    public void DisplayException( Exception ex )
    {
        DisplayError( "Error: " + WebUtilities.ReplaceNewLine( ex.Message ) );

        WebUtilities.LogError( ex );
    }

    public void Clear()
    {
        ControlVisible( false );
    }

    #endregion
}
