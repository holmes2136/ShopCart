using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.WebAppLib;
using Vevo.WebUI.Products;
using Vevo.Shared.SystemServices;

public partial class Components_OptionUploadRequireItem : Vevo.WebUI.International.BaseLanguageUserControl
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

    private bool CheckFileUploaded(out string errorMessage)
    {
        errorMessage = String.Empty;
        bool isUploaded = false;
        if (!String.IsNullOrEmpty( UploadedFile ))
        {
            string filename = TempUploadedFile.GetTempFileLocalPath();
            if (!String.IsNullOrEmpty( filename ))
                if (File.Exists(  filename  ))
                    isUploaded = true;
        }

        if (!isUploaded)
            errorMessage = GetLanguageText( "OptionUploadRequired" );

        return isUploaded;
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        uxMessageDiv.Visible = false;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (String.IsNullOrEmpty(UploadedFile))
            uxUploadedFilename.Text = String.Empty;
        else
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
                uxUploadRQMessage.Text = " " + GetLanguageText("OptionUploadMaxLimit");
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
        if (!OptionUploadWebControlService.ValidateInput( out errorMessage ) || !CheckFileUploaded( out errorMessage ))
        {
            uxMessageDiv.Visible = true;
            uxUploadRQMessage.Text = " " + errorMessage;
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
