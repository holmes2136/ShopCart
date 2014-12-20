using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;

public partial class AdminAdvanced_Components_Common_Upload : AdminAdvancedBaseUserControl
{
    private const int DefaultDestionationTextBoxWidth = 210;


    private string CheckFileType()
    {
        switch (CheckType)
        {
            case UploadFileType.Image:
                return "*.jpg;*.jpeg;*.gif;*.png;*.bmp;*.tiff";
            case UploadFileType.Csv:
                return "*.csv";
            default:
                return "*.*";
        }
    }

    private string CheckFileTypeDescription()
    {
        switch (CheckType)
        {
            case UploadFileType.Image:
                return "Images Files";
            case UploadFileType.Csv:
                return "Csv Files";
            default:
                return "Any Files";
        }
    }

    private string ButtonUploadText()
    {
        if (ShowText)
        {
            if (String.IsNullOrEmpty( ButtonText ))
            {
                switch (CheckType)
                {
                    case UploadFileType.Image:
                        return "Select Images";
                    default:
                        return "Select Files";
                }
            }
            else
                return ButtonText;
        }
        else
            return String.Empty;
    }

    private void RegisterStartupScript()
    {
        String uploadFilePath = UrlPath.StorefrontUrl;

        StringBuilder sb = new StringBuilder();
        string scriptName = this.ClientID;
        sb.AppendLine( String.Format( "var upload{0};", scriptName ) );
        sb.Append( String.Format( "    upload{0} =", scriptName ) );
        sb.AppendLine( " new SWFUpload({" );
        sb.AppendLine( "// Backend Settings" );

        sb.Append( "upload_url: " );
        sb.Append( String.Format( "'{0}Components/Upload/{1}'", uploadFilePath, UploadFilePageName ) );
        sb.AppendLine( ",	// Relative to the SWF file" );

        sb.AppendLine( "post_params : {" );
        sb.AppendLine( String.Format( "'{0}' : '{1}',", "ASPSESSID", Session.SessionID ) );
        sb.AppendLine( String.Format( "'{0}' : '{1}'", "UploadControl", this.ClientID ) );
        sb.AppendLine( "}," );

        sb.AppendLine( "" );
        sb.AppendLine( "" );

        sb.AppendLine( "// File Upload Settings" );
        sb.AppendLine( String.Format( "file_size_limit : '{0}',", MaxFileSize ) );
        sb.AppendLine( String.Format( "file_types : '{0}',", CheckFileType() ) );
        sb.AppendLine( String.Format( "file_types_description : '{0}',", CheckFileTypeDescription() ) );
        sb.AppendLine( "file_upload_limit : '0'," );  // Zero means unlimited
        sb.AppendLine( "file_queue_limit : '1'," ); // Zero means unlimited

        sb.AppendLine( "" );
        sb.AppendLine( "" );

        //sb.AppendLine( "//  Event Handler Settings - these functions as defined in Handlers.js" );
        //sb.AppendLine( "//  The handlers are not part of SWFUpload but are part of my website and control how" );
        //sb.AppendLine( "//  my website reacts to the SWFUpload events." );
        sb.AppendLine( "file_dialog_start_handler : fileDialogStart," );
        sb.AppendLine( String.Format( "file_queued_handler : fileQueued{0},", this.ClientID ) );
        sb.AppendLine( "file_queue_error_handler : fileQueueError," );
        sb.AppendLine( String.Format( "file_dialog_complete_handler : fileDialogComplete{0},", this.ClientID ) );
        sb.AppendLine( String.Format( "upload_start_handler : uploadStart{0},", this.ClientID ) );
        sb.AppendLine( "upload_progress_handler : uploadProgress," );
        sb.AppendLine( "upload_error_handler : uploadError," );
        sb.AppendLine( String.Format( "upload_success_handler : uploadSuccess{0},", this.ClientID ) );
        sb.AppendLine( String.Format( "upload_complete_handler : uploadComplete{0},", this.ClientID ) );

        sb.AppendLine( "" );
        sb.AppendLine( "" );

        sb.AppendLine( "// Button settings" );
        sb.AppendLine( String.Format( "button_image_url : '{0}Components/Upload/images/{1}',", uploadFilePath, ButtonImage ) ); // Relative to the SWF file
        sb.AppendLine( String.Format( "button_placeholder_id : '{0}',", uxButtonPlaceHolderLabel.ClientID ) );
        sb.AppendLine( String.Format( "button_width: {0},", ButtonWidth.Value ) );
        sb.AppendLine( String.Format( "button_height: {0},", ButtonHeight.Value ) );
        sb.AppendLine( String.Format( @"button_text : ""<span class='button'>{0}<span class='buttonSmall'>({1} Max)</span></span>"",", ButtonUploadText(), MaxFileSize ) );
        sb.AppendLine( "button_text_style : '.button { font-family: Verdana, Arial, sans-serif; font-size: 12pt; font-weight: bold; color: #ffffff; text-align: center; width:150px; } .buttonSmall { font-size: 10pt; display:none; }'," );
        sb.AppendLine( "button_text_top_padding: 1," );
        sb.AppendLine( "button_text_left_padding: 5," );
        sb.AppendLine( "" );
        sb.AppendLine( "" );
        sb.AppendLine( "// Flash Settings" );
        sb.AppendLine( String.Format( "flash_url : '{0}Components/Upload/swfupload/swfupload.swf',	// Relative to this file", uploadFilePath ) );
        sb.Append( String.Format( "swfupload_element_id : 'flashUI{0}',", scriptName ) );
        sb.AppendLine( "// Setting from graceful degradation plugin" );
        sb.Append( String.Format( "degraded_element_id : 'degradedUI{0}',", scriptName ) );
        sb.AppendLine( "// Setting from graceful degradation plugin" );
        sb.AppendLine( "" );
        sb.AppendLine( "" );
        sb.AppendLine( "custom_settings : {" );
        sb.AppendLine( String.Format( "progressTarget : '{0}'", uxFileProgressPanel.ClientID ) );
        //sb.AppendLine( String.Format( "cancelButtonId : '{0}'", uxCancelButton.ClientID ) );
        sb.AppendLine( "}," );
        sb.AppendLine( "" );
        sb.AppendLine( "" );
        //sb.AppendLine( "// Debug Settings" );
        sb.AppendLine( "debug: false" );
        sb.AppendLine( "});" );

        if (!this.Page.ClientScript.IsStartupScriptRegistered( String.Format( "CreatUpload{0}", this.ClientID ) ))
            ScriptManager.RegisterStartupScript( this, typeof( Page ), String.Format( "CreatUpload{0}", this.ClientID ), sb.ToString(), true );
    }

    private void RegisterScriptBlock()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine( String.Format( "var pathDestination{0} = document.getElementById('{0}');", uxPathDestinationText.ClientID ) );
        if (!String.IsNullOrEmpty( ReturnTextControlClientID ))
            sb.AppendLine( String.Format( "var returnTextControl{0} = document.getElementById('{0}');", ReturnTextControlClientID ) );
        sb.Append( String.Format( "function fileQueued{0}", this.ClientID ) );
        sb.AppendLine( "(file) {" );
        sb.AppendLine( "    try {" );
        // You might include code here that prevents the form from being submitted while the upload is in
        // progress.  Then you'll want to put code in the Queue Complete handler to "unblock" the form
        sb.AppendLine( String.Format( "        if (IsEmpty(pathDestination{0}))", uxPathDestinationText.ClientID ) );
        sb.AppendLine( "        {" );
        sb.AppendLine( String.Format( " cancelQueue(upload{0});", this.ClientID ) );
        sb.AppendLine( "            uploadImageMessage = \"Path destination cannot be empty\";" );
        sb.AppendLine( "        }" );
        sb.AppendLine( "        else" );
        sb.AppendLine( "        {" );
        sb.AppendLine( "            var progress = new FileProgress(file, this.customSettings.progressTarget);" );
        sb.AppendLine( "            progress.setStatus(\"Pending...\");" );
        sb.AppendLine( "            progress.toggleCancel(true, this);" );
        sb.AppendLine( "        }" );
        sb.AppendLine( "    } catch (ex) {" );
        sb.AppendLine( "    this.debug(ex);" );
        sb.AppendLine( "    }" );
        sb.AppendLine( "}" );

        sb.Append( String.Format( "function fileDialogComplete{0}", this.ClientID ) );
        sb.AppendLine( "(numFilesSelected, numFilesQueued) {" );
        sb.AppendLine( "    try {" );
        //sb.AppendLine( "        if (this.getStats().files_queued > 0) {" );
        //sb.AppendLine( "        }" );
        //sb.AppendLine( "        /* I want auto start and I can do that here */" );
        sb.AppendLine( String.Format( "document.getElementById('{0}').style.display='block';", uxRowProgressPanel.ClientID ) );
        sb.AppendLine( "        this.startUpload();" );
        sb.AppendLine( "    } catch (ex)  {" );
        sb.AppendLine( "        this.debug(ex);" );
        sb.AppendLine( "    }" );
        sb.AppendLine( "}" );

        sb.Append( String.Format( "function uploadStart{0}", this.ClientID ) );
        sb.AppendLine( "(file) {" );
        sb.AppendLine( "try {" );
        sb.AppendLine( String.Format( "        this.addFileParam(file.id, \"PathDestination\", pathDestination{0}.value );", uxPathDestinationText.ClientID ) );
        sb.AppendLine( "        var progress = new FileProgress(file, this.customSettings.progressTarget);" );
        sb.AppendLine( "        progress.setStatus(\"Uploading...\");" );
        sb.AppendLine( "        progress.toggleCancel(true, this);" );
        sb.AppendLine( "    }" );
        sb.AppendLine( "    catch (ex) {" );
        sb.AppendLine( "    }" );
        sb.AppendLine( "    return true;" );
        sb.AppendLine( "}" );


        sb.Append( String.Format( "function uploadSuccess{0}", this.ClientID ) );
        sb.AppendLine( "(file, serverData) {" );
        sb.AppendLine( "    try {" );
        sb.AppendLine( "        var progress = new FileProgress(file, this.customSettings.progressTarget);" );
        sb.AppendLine( "        progress.setComplete();" );
        sb.AppendLine( "        progress.setStatus(\"Complete.\");" );
        sb.AppendLine( "        progress.toggleCancel(false);" );
        if (!String.IsNullOrEmpty( ReturnTextControlClientID ))
        {
            sb.AppendLine( String.Format( "mypath = pathDestination{0}.value + file.name;", uxPathDestinationText.ClientID ) );
            sb.AppendLine( String.Format( "returnTextControl{0}.value = mypath;", ReturnTextControlClientID ) );
        }
        sb.AppendLine( "    } catch (ex) {" );
        sb.AppendLine( "        this.debug(ex);" );
        sb.AppendLine( "    }" );
        sb.AppendLine( "}" );

        sb.Append( String.Format( "function uploadComplete{0}", this.ClientID ) );
        sb.AppendLine( "(file) {" );
        sb.AppendLine( "    try {" );
        /*  I want the next upload to continue automatically so I'll call startUpload here */
        sb.AppendLine( "        if (this.getStats().files_queued === 0) {" );
        sb.AppendLine( "        } else {" );
        sb.AppendLine( "            this.startUpload();" );
        sb.AppendLine( "        }" );
        if (AutoUpdateContentPanel)
            sb.AppendLine( "        __doPostBack('uxContentUpdatePanel', '');" );// For AsyncPostBack If you Want;
        sb.Append( "        setTimeout(" );
        sb.Append( String.Format( " \"hideRowProgress{0}()\"", this.ClientID ) );
        sb.AppendLine( ", 3000);" );
        sb.AppendLine( "    } catch (ex) {" );
        sb.AppendLine( "        this.debug(ex);" );
        sb.AppendLine( "    }" );
        sb.AppendLine( "}" );

        sb.AppendLine( String.Format( "function hideRowProgress{0}()", this.ClientID ) );
        sb.AppendLine( "{" );
        sb.AppendLine( String.Format( "document.getElementById('{0}').style.display='none';", uxRowProgressPanel.ClientID ) );
        sb.AppendLine( "}" );

        if (!Page.ClientScript.IsClientScriptBlockRegistered( String.Format( "UploadScriptBlock{0}", this.ClientID ) ))
            ScriptManager.RegisterClientScriptBlock( this, typeof( Page ), String.Format( "UploadScriptBlock{0}", this.ClientID ), sb.ToString(), true );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxPathDestinationText.Text = PathDestination;
        uxRowDestinationLabel.CssClass = LeftLabelClass;
        uxFileProgressPanelLabel.CssClass = LeftLabelClass;
        uxPathDestinationText.Width = DestionationTextBoxWidth;
        lcDestination.Text = DestionationTextLabel;

        if (!ShowDestinationLabel)
            uxRowDestinationLabel.Visible = false;
        if (!ShowDestinationTextBox)
            uxPathDestinationText.Style["display"] = "none";
        if (ShowControl && !String.IsNullOrEmpty( CssClass ))
        {
            uxUploadPanel.CssClass = CssClass;
        }

        RegisterStartupScript();
        RegisterScriptBlock();
    }

    private void SetClassDisplay( bool value )
    {
        if (ShowControl)
            uxUploadPanel.CssClass = CssClass;
        else
            uxUploadPanel.CssClass = "dn";
    }

    public bool ShowText
    {
        get
        {
            if (ViewState["ShowText"] == null)
                return true;
            else
                return (bool) ViewState["ShowText"];
        }
        set
        {
            ViewState["ShowText"] = value;
        }
    }

    public bool ShowControl
    {
        get
        {
            if (ViewState["ShowControl"] == null)
                return true;
            else
                return (bool) ViewState["ShowControl"];
        }
        set
        {
            ViewState["ShowControl"] = value;
            SetClassDisplay( (bool) ViewState["ShowControl"] );
        }
    }

    public String ButtonImage
    {
        get
        {
            if (ViewState["ButtonImage"] == null)
                return "AdvancedButtonNoText_160x20.gif";
            else
                return (String) ViewState["ButtonImage"];
        }
        set
        {
            ViewState["ButtonImage"] = value;
        }
    }

    public Unit ButtonWidth
    {
        get
        {
            if (ViewState["ButtonWidth"] == null)
                return new Unit( 160 );
            else
                return (Unit) ViewState["ButtonWidth"];
        }
        set
        {
            ViewState["ButtonWidth"] = value;
        }
    }

    public Unit ButtonHeight
    {
        get
        {
            if (ViewState["ButtonHeight"] == null)
                return new Unit( 22 );
            else
                return (Unit) ViewState["ButtonHeight"];
        }
        set
        {
            ViewState["ButtonHeight"] = value;
        }
    }

    public String ButtonText
    {
        get
        {
            if (ViewState["ButtonText"] == null)
                return String.Empty;
            else
                return (String) ViewState["ButtonText"];
        }
        set
        {
            ViewState["ButtonText"] = value;
        }
    }

    public bool ShowDestinationLabel
    {
        get
        {
            if (ViewState["ShowDestinationLabel"] == null)
                return true;
            else
                return (bool) ViewState["ShowDestinationLabel"];
        }
        set
        {
            ViewState["ShowDestinationLabel"] = value;
        }
    }

    public bool ShowDestinationTextBox
    {
        get
        {
            if (ViewState["ShowDestinationTextBox"] == null)
                return true;
            else
                return (bool) ViewState["ShowDestinationTextBox"];
        }
        set
        {
            ViewState["ShowDestinationTextBox"] = value;
        }
    }

    public Unit DestionationTextBoxWidth
    {
        get
        {
            if (ViewState["DestionationTextBoxWidth"] == null)
                return new Unit( DefaultDestionationTextBoxWidth );
            else
                return (Unit) ViewState["DestionationTextBoxWidth"];
        }
        set
        {
            ViewState["DestionationTextBoxWidth"] = value;
        }
    }

    public String DestionationTextLabel
    {
        get
        {
            if (ViewState["DestionationTextLabel"] == null)
                return Resources.Upload.DestinationLabel;
            else
                return (String) ViewState["DestionationTextLabel"];
        }
        set
        {
            ViewState["DestionationTextLabel"] = value;
        }
    }

    public String CssClass
    {
        get
        {
            if (ViewState["CssClass"] == null)
                return String.Empty;
            else
                return (String) ViewState["CssClass"];
        }
        set
        {
            ViewState["CssClass"] = value;
        }
    }

    public string MaxFileSize
    {
        get
        {
            if (ViewState["MaxFileSize"] == null)
                return "2 MB";
            else
                return (String) ViewState["MaxFileSize"];
        }
        set
        {
            ViewState["MaxFileSize"] = value;
        }
    }

    public string PathDestination
    {
        get
        {
            if (ViewState["PathDestination"] != null)
                return (string) ViewState["PathDestination"];
            else
                return String.Empty;
        }
        set
        {
            ViewState["PathDestination"] = value;
        }
    }

    public String UploadFilePageName
    {
        get
        {
            if (ViewState["UploadFilePageName"] == null)
                return "UploadNormal.aspx";
            else
                return (String) ViewState["UploadFilePageName"];
        }
        set
        {
            ViewState["UploadFilePageName"] = value;
        }
    }

    public String ReturnTextControlClientID
    {
        get
        {
            if (ViewState["ReturnTextControlClientID"] == null)
                return String.Empty;
            else
                return (String) ViewState["ReturnTextControlClientID"];
        }
        set
        {
            ViewState["ReturnTextControlClientID"] = value;
        }
    }

    public bool AutoUpdateContentPanel
    {
        get
        {
            if (ViewState["AutoUpdateContentPanel"] == null)
                return false;
            else
                return (bool) ViewState["AutoUpdateContentPanel"];
        }
        set
        {
            ViewState["AutoUpdateContentPanel"] = value;
        }
    }

    public String LeftLabelClass
    {
        get
        {
            if (ViewState["LeftLabelClass"] == null)
                return String.Empty;
            else
                return (String) ViewState["LeftLabelClass"];
        }
        set
        {
            ViewState["LeftLabelClass"] = value;
        }
    }

    public UploadFileType CheckType
    {
        get
        {
            if (ViewState["CheckType"] != null)
                return (UploadFileType) ViewState["CheckType"];
            else
                return UploadFileType.Any;
        }
        set
        {
            ViewState["CheckType"] = value;
        }
    }
}
