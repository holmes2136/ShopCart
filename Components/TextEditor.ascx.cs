using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;

public partial class Components_TextEditor : System.Web.UI.UserControl
{
    private string TextInside
    {
        get
        {
            switch ((EditorList) Enum.Parse( 
                typeof( EditorList ), 
                DataAccessContext.Configurations.GetValue( "HtmlEditor" ), true ))
            {
                case EditorList.Html:
                    return Server.HtmlDecode(uxAjaxTextEditor.Content).ToString();
                case EditorList.PlainText:
                    return uxTextBox.Text;
                default:
                    return uxTextBox.Text;
            }
        }
        set
        {
            switch ((EditorList) Enum.Parse( typeof( EditorList ), DataAccessContext.Configurations.GetValue( "HtmlEditor" ), true ))
            {
                case EditorList.Html:
                    uxAjaxTextEditor.Content = value;
                    break;
                case EditorList.PlainText:
                    uxTextBox.Text = value;
                    break;
                default:
                    uxTextBox.Text = value;
                    break;
            }
        }
    }

    private void HideEditor( EditorList editorName )
    {
        switch (editorName)
        {
            case EditorList.Html:
                uxAjaxTextEditor.Visible = false;
                break;
            case EditorList.PlainText:
                uxTextBox.Visible = false;
                break;
        }
    }

    private void UseEditor( string currentEditorName )
    {
        if (String.IsNullOrEmpty( currentEditorName ))
        {
            uxAjaxTextEditor.Visible = false;
        }
        else
        {
            foreach (string item in Enum.GetNames( typeof( EditorList ) ))
            {
                if (String.Compare( item, currentEditorName, true ) != 0)
                {
                    HideEditor( (EditorList) Enum.Parse( typeof( EditorList ), item, true ) );
                }
            }
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxTextEditorPanel.CssClass = PanelClass;
        uxAjaxTextEditor.Width = new Unit( Width.Value - 6 );
        uxAjaxTextEditor.Height = new Unit( Height.Value - 6 );
        uxTextBox.Width = new Unit( Width.Value - 6 );
        uxTextBox.Height = new Unit( Height.Value - 6 );

        uxTextBox.CssClass = TextClass;

        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
        {
            UseEditor( DataAccessContext.Configurations.GetValue( "HtmlEditor" ) );
        }
        else
        {
            UseEditor( "Textbox" );
        }
    }

    public String Text
    {
        get
        {
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {
                return TextInside;
            }
            else
                return uxTextBox.Text;

        }
        set
        {
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {
                TextInside = value;
            }
            else
                uxTextBox.Text = value;
        }
    }

    public String PanelClass
    {
        get
        {
            if (ViewState["PanelClass"] == null)
                return String.Empty;
            else
                return (String) ViewState["PanelClass"];
        }
        set
        {
            ViewState["PanelClass"] = value;
        }
    }

    public String TextClass
    {
        get
        {
            if (ViewState["TextClass"] == null)
                return String.Empty;
            else
                return (String) ViewState["TextClass"];
        }
        set
        {
            ViewState["TextClass"] = value;
        }
    }

    public Unit Width
    {
        get
        {
            if (ViewState["Width"] == null)
                return new Unit( 550 );
            else
                return (Unit) ViewState["Width"];
        }
        set
        {
            ViewState["Width"] = value;
        }
    }

    public Unit Height
    {
        get
        {
            if (ViewState["Height"] == null)
                return new Unit( 300 );
            else
                return (Unit) ViewState["Height"];
        }
        set
        {
            ViewState["Height"] = value;
        }
    }

    public string ToolbarSet
    {
        get
        {
            if (ViewState["ToolbarSet"] == null)
                return "AdminAdvanced";
            else
                return (String) ViewState["ToolbarSet"];
        }
        set { ViewState["ToolbarSet"] = value; }
    }
}
