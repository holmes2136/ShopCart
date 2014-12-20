using System;
using System.Drawing;
using System.Web.UI.WebControls;
using Vevo.WebAppLib;


public partial class Components_MobileMessage : System.Web.UI.UserControl, Vevo.WebUI.IMessageControl
{
    private const int DefaultNumberOfNewLines = 3;
    private const string NewLineText = "<br/>";


    private string GetNewLineText()
    {
        string result = String.Empty;

        for (int i = 0; i < NumberOfNewLines; i++)
            result += NewLineText;

        return result;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxMessageLabel.Visible = false;
        uxMessageDiv.Visible = false;
    }


    public Color ForeColor
    {
        get
        {
            return uxMessageLabel.ForeColor;
        }
        set
        {
            uxMessageLabel.ForeColor = value;
        }
    }

    public FontUnit FontSize
    {
        get
        {
            return uxMessageLabel.Font.Size;
        }
        set
        {
            uxMessageLabel.Font.Size = value;
        }
    }

    public bool FontBold
    {
        get
        {
            return uxMessageLabel.Font.Bold;
        }
        set
        {
            uxMessageLabel.Font.Bold = value;
        }
    }

    public int NumberOfNewLines
    {
        get
        {
            if (ViewState["NumberOfNewLines"] == null)
                return DefaultNumberOfNewLines;
            else
                return (int) ViewState["NumberOfNewLines"];
        }
        set
        {
            ViewState["NumberOfNewLines"] = value;
        }
    }

    public void DisplayMessage( string text, params object[] parameters )
    {
        DisplayMessageNoNewLIne( text + GetNewLineText(), parameters );
    }

    public void DisplayMessageNoNewLIne( string text, params object[] parameters )
    {
        uxMessageLabel.Text = String.Format( text, parameters );
        uxMessageLabel.ForeColor = Color.Blue;
        uxMessageLabel.Visible = true;

        uxMessageDiv.Attributes.Add( "class", "MobileCommonDisplayMessageDiv" );
        uxMessageDiv.Visible = true;
    }

    public void DisplayError( string text, params object[] parameters )
    {
        DisplayErrorNoNewLine( text + GetNewLineText(), parameters );
    }

    public void DisplayErrorNoNewLine( string text, params object[] parameters )
    {
        uxMessageLabel.Text = String.Format( text, parameters );
        uxMessageLabel.ForeColor = Color.Red;
        uxMessageLabel.Visible = true;

        uxMessageDiv.Attributes.Add( "class", "MobileCommonErrorMessageDiv" );
        uxMessageDiv.Visible = true;
    }

    public void DisplayException( Exception ex )
    {
        DisplayError( "Error: " + WebUtilities.ReplaceNewLine( ex.Message ) );

        WebUtilities.LogError( ex );
    }

    public void ClearMessage()
    {
        uxMessageLabel.Visible = false;
        uxMessageLabel.Text = "";

        uxMessageDiv.Visible = false;
    }

}
