using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Shared.Utilities;

public partial class Components_Upload : AdminAdvancedBaseUserControl
{
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

    public string PathFullName
    {
        get
        {
            if (ViewState["PathFullName"] != null)
                return (string) ViewState["PathFullName"];
            else
                return String.Empty;
        }
        set
        {
            ViewState["PathFullName"] = value;
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

    public string PanelClass
    {
        get
        {
            if (ViewState["CssClass"] != null)
                return (string) ViewState["CssClass"];
            else
                return String.Empty;
        }
        set
        {
            ViewState["CssClass"] = value;
        }
    }

    public string LabelClass
    {
        get
        {
            if (ViewState["LabelClass"] != null)
                return (string) ViewState["LabelClass"];
            else
                return String.Empty;
        }
        set
        {
            ViewState["LabelClass"] = value;
        }
    }

    public string RowClass
    {
        get
        {
            if (ViewState["RowClass"] != null)
                return (string) ViewState["RowClass"];
            else
                return String.Empty;
        }
        set
        {
            ViewState["RowClass"] = value;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        //UpdatePanel updatePanel = (UpdatePanel) Page.Master.FindControl( "uxContentUpdatePanel" );
        //PostBackTrigger trigger = new PostBackTrigger();
        //trigger.ControlID = uxUploadButton.UniqueID;
        //updatePanel.Triggers.Add( trigger );

        //if (!IsPostBack)
        //    uxDestinationText.Text = PathDestination;
        UpdatePanel updatePanel = (UpdatePanel) Page.FindControl( "uxContentUpdatePanel" );
        PostBackTrigger trigger = new PostBackTrigger();
        trigger.ControlID = uxUploadButton.UniqueID;
        updatePanel.Triggers.Add( trigger );

        if (!MainContext.IsPostBack) //if (!IsPostBack)
            uxDestinationText.Text = PathDestination;

        if (!String.IsNullOrEmpty( PanelClass ))
        {
            uxFileUploadPanel.CssClass = PanelClass;
        }
        else
        {
            uxFileUploadPanel.CssClass = "b14 pdt1 pdb5";
        }
        

        if (!String.IsNullOrEmpty( LabelClass ))
        {
            uxRowUploadLabel.CssClass = LabelClass;
            uxRowDestinationLabel.CssClass = LabelClass;
            uxMessageErrorLabel.CssClass = LabelClass;
        }

        if (!String.IsNullOrEmpty( RowClass ))
        {
            uxRowUploadFilePanel.CssClass = RowClass;
            uxRowDestinationPanel.CssClass = RowClass;
            uxRowMessageErrorPanel.CssClass = RowClass;
        }
    }

    private void UploadFile( FileUpload fileUpload )
    {
        string fileFullName = fileUpload.PostedFile.FileName;
        string fileNameInfo = Path.GetFileName( fileFullName );
        string tempPath = Server.MapPath( "../" + uxDestinationText.Text );
        if (fileNameInfo != "" && (FileUtilities.CheckFileType( fileNameInfo, CheckType )))
        {
            PathDestination = uxDestinationText.Text;
            fileUpload.PostedFile.SaveAs( tempPath + fileNameInfo );
            PathFullName = uxDestinationText.Text + fileNameInfo;
            uxRowMessageErrorPanel.Visible = true;
            uxErrorLabel.Text = "Upload Successfully!";
        }
        else
        {
            uxErrorLabel.Text = "Can not upload file!";
            uxRowMessageErrorPanel.Visible = true;
        }
    }

    protected void uxUploadButton_Click( object sender, EventArgs e )
    {
        UploadFile( uxImageFileUpload );
    }


}
