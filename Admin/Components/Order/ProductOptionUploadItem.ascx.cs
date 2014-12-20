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

public partial class Admin_Components_Order_ProductOptionUploadItem : AdminAdvancedBaseUserControl
{
    #region Private

    private OptionUploadWebControlService _optionUploadWebControlService;

    private OptionUploadWebControlService OptionUploadWebControlService
    {
        get
        {
            if (_optionUploadWebControlService == null)
                _optionUploadWebControlService = new OptionUploadWebControlService(
                    false, uxUploadFile, Server.MapPath( "~/" ) );

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
        uxMessageDiv.Visible = false;
    }
	protected void Page_PreRender( object sender, EventArgs e )
    {
        uxUploadedFilename.Text = " - " + UploadedFile;
    }

    protected void uxUploadButton_Click( object sender, EventArgs e )
    {
        if (uxUploadFile.HasFile)
        {
            if (uxUploadFile.PostedFile.ContentLength
                        > DataAccessContext.Configurations.GetIntValue( "UploadSize" ) * SystemConst.UploadSizeConfigFactor)
            {
                uxMessageDiv.Visible = true;
                uxUploadMessage.Text = "Upload size is exceeded.";
                return;
            }
            TempUploadedFile = new TempUploadedFile(
               new FileManager(),
               SystemConst.OptionFileTempPath,
               SystemConst.OptionFileUpload,
                uxUploadFile.FileName );
            uxUploadFile.PostedFile.SaveAs( TempUploadedFile.GetTempFileLocalPath() );
            UploadedFile = uxUploadFile.FileName;
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
            return uxUploadLabel.Text;
        }
        set
        {
            uxUploadLabel.Text = value;
        }
    }

    #endregion


    #region Public Methods

    public bool ValidateInput()
    {
        string errorMessage;
        if (!OptionUploadWebControlService.ValidateInput( out errorMessage ))
        {
            uxMessageDiv.Visible = true;
            uxUploadMessage.Text = " " + errorMessage;
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
