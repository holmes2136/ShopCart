using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Vevo;
using Vevo.Domain.Orders;
using Vevo.WebUI.Products;
using AjaxControlToolkit;
using Vevo.Domain;
using Vevo.Shared.SystemServices;

public partial class Admin_Components_Order_ProductOptionUploadRequireItem : AdminAdvancedBaseUserControl
{

    #region Private

    private OptionUploadWebControlService _optionUploadWebControlService;

    private OptionUploadWebControlService OptionUploadWebControlService
    {
        get
        {
            if (_optionUploadWebControlService == null)
            {
                if ((UploadedFile == null) || (UploadedFile == string.Empty))
                _optionUploadWebControlService = new OptionUploadWebControlService(
                    true, uxUploadRQFile, Server.MapPath( "~/" ) );
                else
                    _optionUploadWebControlService = new OptionUploadWebControlService(
                        true, UploadedFile, Server.MapPath( "~/" ) );
            }

            return _optionUploadWebControlService;
        }
    }

    private string UploadedFile
    {
        get
        {
            return (string) ViewState[this.UniqueID + "_UploadedFile"];
        }
        set
        {
            ViewState[this.UniqueID + "_UploadedFile"] = value;
        }
    }
    private TempUploadedFile TempUploadedFile
    {
        get
        {
            return (TempUploadedFile) ViewState[this.UniqueID + "_TempUploadedFile"];
        }
        set
        {
            ViewState[this.UniqueID + "_TempUploadedFile"] = value;
        }
    }
    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {

    }
	
	protected void Page_PreRender( object sender, EventArgs e )
    {
        uxUploadedFilename.Text = " - " + UploadedFile;
    }

    protected void uxUploadButton_Click( object sender, EventArgs e )
    {
        if (uxUploadRQFile.HasFile)
        {
            if (uxUploadRQFile.PostedFile.ContentLength
                        > DataAccessContext.Configurations.GetIntValue( "UploadSize" ) * SystemConst.UploadSizeConfigFactor)
            {
                uxMessageDiv.Visible = true;
                uxUploadRQMessage.Text = "Upload size is exceeded.";
                return;
            }

            TempUploadedFile = new TempUploadedFile(
               new FileManager(),
               SystemConst.OptionFileTempPath,
               SystemConst.OptionFileUpload,
                uxUploadRQFile.FileName );

            uxUploadRQFile.PostedFile.SaveAs( TempUploadedFile.GetTempFileLocalPath() );
            UploadedFile = uxUploadRQFile.FileName;
            uxMessageDiv.Visible = false;
            uxUploadedFilename.Text = " - " + UploadedFile;
        }

        Control parent = this.Parent;
        Control control = null;
        while (parent.GetType() != typeof( Page ))
        {
            control = parent.FindControl( "uxAddItemButtonModalPopup" );
            if (control != null)
                break;
            parent = parent.Parent;
        }
        if (control != null)
        {
            ModalPopupExtender popup = (ModalPopupExtender) control;
            popup.Show();
        }
    }

    #endregion


    #region Public Properties

    public string OptionLabel
    {
        get
        {
            return uxUploadRQLabel.Text;
        }
        set
        {
            uxUploadRQLabel.Text = value;
        }
    }

    #endregion


    #region Public Methods

    public bool ValidateInput()
    {
        string errorMessage;
        if (!OptionUploadWebControlService.ValidateInput( out errorMessage ))
        {
            uxUploadRQMessage.Text = "<br/>" + errorMessage;
            return false;
        }
        else
        {
            return true;
        }
    }

    public void CreateOption( ArrayList selectedList, string optionItemID, bool useStock )
    {
        _optionUploadWebControlService = new OptionUploadWebControlService( false, UploadedFile, Server.MapPath( "~/" ) );
        OptionItemValue optionItemValue = OptionUploadWebControlService.CreateOptionItemValue(
            optionItemID, useStock, TempUploadedFile );

        if ((optionItemValue != null) && (UploadedFile != null))
            selectedList.Add( optionItemValue );
    }

    #endregion
}
