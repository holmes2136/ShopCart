using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;

public partial class Components_Upload_Upload : System.Web.UI.UserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        String uploadFilePath = UrlPath.StorefrontUrl;

        StringBuilder sb = new StringBuilder();

        sb.AppendLine( String.Format( "var upload{0};", this.ClientID ) );
        sb.Append( String.Format( "    upload{0} =", this.ClientID ) );
        sb.AppendLine( " new SWFUpload({" );
        sb.AppendLine( "// Backend Settings" );

        sb.Append( "upload_url: " );
        sb.Append( String.Format( "'{0}Components/Upload/{1}'", uploadFilePath, UploadFilePageName ) );
        sb.AppendLine( ",	// Relative to the SWF file" );

        sb.AppendLine( "post_params : {" );
        sb.AppendLine( String.Format( "'{0}' : '{1}',", "ASPSESSID", Session.SessionID ) );
        sb.AppendLine( String.Format( "'{0}' : '{1}',", "UploadDirectory", UploadDirectory ) );
        sb.AppendLine( String.Format( "'{0}' : '{1}'", "ProductID", ProductID ) );
        sb.AppendLine( "}," );

        sb.AppendLine( "" );
        sb.AppendLine( "" );

        sb.AppendLine( "// File Upload Settings" );
        sb.AppendLine( "file_size_limit : '50 MB'," );
        sb.AppendLine( "file_types : '*.jpg;*.gif;*.png;*.bmp'," );
        sb.AppendLine( "file_types_description : 'Images Files'," );
        sb.AppendLine( "file_upload_limit : '0',  // Zero means unlimited" );
        sb.AppendLine( "file_queue_limit : '1', // Zero means unlimited" );

        sb.AppendLine( "" );
        sb.AppendLine( "" );

        sb.AppendLine( "// Event Handler Settings - these functions as defined in Handlers.js" );
        sb.AppendLine( "//  The handlers are not part of SWFUpload but are part of my website and control how" );
        sb.AppendLine( "//  my website reacts to the SWFUpload events." );
        sb.AppendLine( "file_dialog_start_handler : fileDialogStart," );
        sb.AppendLine( String.Format( "file_queued_handler : fileQueued{0},", this.ClientID ) );
        sb.AppendLine( "file_queue_error_handler : fileQueueError," );
        sb.AppendLine( "file_dialog_complete_handler : fileDialogComplete," );
        sb.AppendLine( "upload_start_handler : uploadStart," );
        sb.AppendLine( "upload_progress_handler : uploadProgress," );
        sb.AppendLine( String.Format( "upload_error_handler : uploadError{0},", this.ClientID ) );
        sb.AppendLine( "upload_success_handler : uploadSuccess," );
        sb.AppendLine( String.Format( "upload_complete_handler : uploadComplete{0},", this.ClientID ) );

        sb.AppendLine( "" );
        sb.AppendLine( "" );

        sb.AppendLine( "// Button settings" );
        sb.AppendLine( String.Format( "button_image_url : '{0}Components/Upload/images/SelectImages.png', // Relative to the SWF file", uploadFilePath ) );
        sb.AppendLine( String.Format( "button_placeholder_id : '{0}',", uxButtonPlaceHolderLabel.ClientID ) );
        sb.AppendLine( "button_width: 103," );
        sb.AppendLine( "button_height: 22," );
        sb.AppendLine( @"button_text : ""<span class='button'><span class='buttonSmall'></span></span>""," );
        sb.AppendLine( "button_text_style : '.button { font-family: Verdana, Arial, sans-serif; font-size: 12pt; font-weight: bold; color: #ffffff; text-align: center; width:150px; } .buttonSmall { font-size: 10pt; display:none; }'," );
        sb.AppendLine( "button_text_top_padding: 0," );
        sb.AppendLine( "button_text_left_padding: 5," );
        sb.AppendLine( "" );
        sb.AppendLine( "" );
        sb.AppendLine( "// Flash Settings" );
        sb.AppendLine( String.Format( "flash_url : '{0}Components/Upload/swfupload/swfupload.swf',	// Relative to this file", uploadFilePath ) );
        sb.Append( String.Format( "swfupload_element_id : 'flashUI{0}',", this.ClientID ) );
        sb.AppendLine( "// Setting from graceful degradation plugin" );
        sb.Append( String.Format( "degraded_element_id : 'degradedUI{0}',", this.ClientID ) );
        sb.AppendLine( "// Setting from graceful degradation plugin" );
        sb.AppendLine( "" );
        sb.AppendLine( "" );
        sb.AppendLine( "custom_settings : {" );
        sb.AppendLine( String.Format( "progressTarget : '{0}',", uxFileProgressPanel.ClientID ) );
        sb.AppendLine( String.Format( "cancelButtonId : '{0}'", uxCancelButton.ClientID ) );
        sb.AppendLine( "}," );
        sb.AppendLine( "" );
        sb.AppendLine( "" );
        sb.AppendLine( "// Debug Settings" );
        sb.AppendLine( "debug: false" );
        sb.AppendLine( "});" );

        ScriptManager.RegisterStartupScript( this, typeof( Page ), this.ClientID, sb.ToString(), true );
        uxCancelButton.Attributes.Add( "onclick", String.Format( "cancelQueue(upload{0});", this.ClientID ) );
        uxCancelButton.Style["display"] = "none";
        //uxCancelButton.Attributes.Add( "disabled", "disabled" );
        uxCancelButton.Enabled = false;
        RegisterScriptBlock();
    }

    private void RegisterScriptBlock()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine( String.Format( "var table = document.getElementById('{0}');", GridViewControlID ) );
        sb.AppendLine( "var uploadImageMessage = '';" );
        sb.AppendLine( "var uploadFilesArray = new Array();" );
        sb.AppendLine( "var fileCount = 0;" );
        sb.AppendLine( "var fileStatus = true;" );
        sb.Append( String.Format( "function fileQueued{0}", this.ClientID ) );
        sb.AppendLine( "(file) {" );
        sb.AppendLine( "    try {" );
        // You might include code here that prevents the form from being submitted while the upload is in
        // progress.  Then you'll want to put code in the Queue Complete handler to "unblock" the form
        sb.AppendLine( "    fileStatus = true;" );
        sb.AppendLine( "    if (table && table.rows.length > 0) {" );
        sb.AppendLine( "        for (r = 0; r < table.rows.length; r++) {" );
        sb.AppendLine( "            innerTables = table.rows[r].getElementsByTagName('table');" );
        sb.AppendLine( "            if (innerTables.length == 1)" );
        sb.AppendLine( "            {" );
        sb.AppendLine( "                imageFileNames = innerTables[0].rows[0].cells[1].getElementsByTagName('span');" );
        sb.AppendLine( "                if ( imageFileNames[0].innerHTML.toLowerCase() == file.name.toLowerCase() )" );
        sb.AppendLine( "                {" );
        sb.AppendLine( String.Format( "upload{0}.stopUpload();", this.ClientID ) );
        sb.AppendLine( String.Format( "upload{0}.cancelUpload();", this.ClientID ) );
        sb.AppendLine( String.Format( "uploadImageMessage = 'This file name already upload in list';" ) );
        sb.AppendLine( "                return;" );
        sb.AppendLine( "                }" );
        sb.AppendLine( "            }" );
        sb.AppendLine( "        }" );//End For
        sb.AppendLine( "    }" );//Emd If Table null
        sb.AppendLine( "   if (CheckInArray(file.name)) {" );
        sb.AppendLine( String.Format( "upload{0}.stopUpload();", this.ClientID ) );
        sb.AppendLine( String.Format( "upload{0}.cancelUpload();", this.ClientID ) );
        sb.AppendLine( String.Format( "uploadImageMessage = 'This file name already upload in list';" ) );
        sb.AppendLine( String.Format( "return;" ) );
        sb.AppendLine( "    }" );
        sb.AppendLine( "    else" );
        sb.AppendLine( "    {" );
        sb.AppendLine( "        var progress = new FileProgress(file, this.customSettings.progressTarget);" );
        sb.AppendLine( "        progress.setStatus(\"Pending...\");" );
        sb.AppendLine( "        progress.toggleCancel(true, this);" );
        sb.AppendLine( "   }" );
        sb.AppendLine( "} catch (ex) {" );
        //sb.AppendLine( "    alert('file Queued has error.');" );
        sb.AppendLine( "    this.debug(ex);" );
        sb.AppendLine( "    }" );
        sb.AppendLine( "}" );

        sb.AppendLine( "function CheckInArray( fileName )" );
        sb.AppendLine( "{" );
        sb.AppendLine( "    if ( uploadFilesArray.length > 0 )" );
        sb.AppendLine( "    {" );
        sb.AppendLine( "        for (x in uploadFilesArray)" );
        sb.AppendLine( "        {" );
        sb.AppendLine( "            if (uploadFilesArray[x].toLowerCase() == fileName.toLowerCase())" );
        sb.AppendLine( "                return true;" );
        sb.AppendLine( "        }" );
        sb.AppendLine( "    }" );
        sb.AppendLine( "    uploadFilesArray.push( fileName );" );
        sb.AppendLine( "    return false;" );
        sb.AppendLine( "}" );

        sb.AppendLine( "function SetErrorFileList(fileName, errorValue)" );
        sb.AppendLine( "{" );
        sb.AppendLine( String.Format( "document.getElementById('{0}').className = 'validator1';", MessageControlID ) );
        sb.AppendLine( String.Format( "document.getElementById('{0}').value = 'Error : Cannot upload ' + fileName + ' ' + errorValue + '<br/>';", uxHiddenFileList.ClientID ) );
        sb.AppendLine( String.Format( "document.getElementById('{0}').innerHTML = 'Error : Cannot upload ' + fileName + ' ' + errorValue + '<br/>';", MessageControlID ) );
        sb.AppendLine( "    fileCount++;" );
        sb.AppendLine( "}" );

        sb.AppendLine( String.Format( "function uploadComplete{0}(file)", this.ClientID ) );
        sb.AppendLine( "{" );
        sb.AppendLine( "    try {" );
        //        /*  I want the next upload to continue automatically so I'll call startUpload here */
        sb.AppendLine( "        if (this.getStats().files_queued === 0) {" );
        sb.AppendLine( "            document.getElementById(this.customSettings.cancelButtonId).disabled = true;" );
        sb.AppendLine( "            document.getElementById(this.customSettings.cancelButtonId).style.display = 'none';" );
        sb.AppendLine( "            this.getMovieElement().style.width = '0px';" );
        sb.AppendLine( "            if (fileStatus)" );
        sb.AppendLine( String.Format( " document.getElementById('{0}').value = '';", uxHiddenFileList.ClientID ) );
        sb.AppendLine( "            setTimeout( \"PostBack()\" , 2000 );" );
        sb.AppendLine( "        } else {" );
        sb.AppendLine( "            this.startUpload();" );
        sb.AppendLine( "        }" );
        sb.AppendLine( "    } catch (ex) {" );
        sb.AppendLine( "        alert('upload Complete has error.');" );
        sb.AppendLine( "        this.debug(ex);" );
        sb.AppendLine( "    }" );
        sb.AppendLine( "}" );

        sb.AppendLine( String.Format( "function uploadError{0}(file, errorCode, message)", this.ClientID ) );
        sb.AppendLine( "{" );
        sb.AppendLine( "    try {" );
        sb.AppendLine( "        fileStatus = false;" );
        sb.AppendLine( "        var progress = new FileProgress(file, this.customSettings.progressTarget);" );
        sb.AppendLine( "        progress.setError();" );
        sb.AppendLine( "        progress.toggleCancel(false);" );
        //sb.AppendLine( "        alert( \"ErrorCode : \" + errorCode + \" Message : \" + message + \" FileName : \" + file.name);" );
        sb.AppendLine( "        switch (errorCode) {" );
        sb.AppendLine( "        case SWFUpload.UPLOAD_ERROR.HTTP_ERROR:" );
        sb.AppendLine( "            if (message == \"600\")" );
        sb.AppendLine( "            {" );
        sb.AppendLine( "                progress.setStatus(\"Destination directory not exists\");" );
        sb.AppendLine( "            }" );
        sb.AppendLine( "            else if (message == \"601\")" );
        sb.AppendLine( "            {" );
        sb.AppendLine( "                progress.setStatus(\"This image has already been used by another product.\");" );
        sb.AppendLine( "                SetErrorFileList( file.name, 'This image has already been used by another product.' );" );
        sb.AppendLine( "            }" );
        sb.AppendLine( "            else" );
        sb.AppendLine( "            {" );
        sb.AppendLine( "                progress.setStatus(\"Upload Error: \" + message);" );
        sb.AppendLine( "            }" );
        sb.AppendLine( "            this.debug(\"Error Code: HTTP Error, File name: \" + file.name + \", Message: \" + message);" );
        sb.AppendLine( "            break;" );
        sb.AppendLine( "        case SWFUpload.UPLOAD_ERROR.MISSING_UPLOAD_URL:" );
        sb.AppendLine( "            progress.setStatus(\"Configuration Error\");" );
        sb.AppendLine( "            this.debug(\"Error Code: No backend file, File name: \" + file.name + \", Message: \" + message);" );
        sb.AppendLine( "            break;" );
        sb.AppendLine( "        case SWFUpload.UPLOAD_ERROR.UPLOAD_FAILED:" );
        sb.AppendLine( "            progress.setStatus(\"Upload Failed.\");" );
        sb.AppendLine( "            this.debug(\"Error Code: Upload Failed, File name: \" + file.name + \", File size: \" + file.size + \", Message: \" + message);" );
        sb.AppendLine( "            break;" );
        sb.AppendLine( "        case SWFUpload.UPLOAD_ERROR.IO_ERROR:" );
        sb.AppendLine( "            progress.setStatus(\"Server (IO) Error\");" );
        sb.AppendLine( "            this.debug(\"Error Code: IO Error, File name: \" + file.name + \", Message: \" + message);" );
        sb.AppendLine( "            break;" );
        sb.AppendLine( "        case SWFUpload.UPLOAD_ERROR.SECURITY_ERROR:" );
        sb.AppendLine( "            progress.setStatus(\"Security Error\");" );
        sb.AppendLine( "            this.debug(\"Error Code: Security Error, File name: \" + file.name + \", Message: \" + message);" );
        sb.AppendLine( "            break;" );
        sb.AppendLine( "        case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:" );
        sb.AppendLine( "            progress.setStatus(\"Upload limit exceeded.\");" );
        sb.AppendLine( "            this.debug(\"Error Code: Upload Limit Exceeded, File name: \" + file.name + \", File size: \" + file.size + \", Message: \" + message);" );
        sb.AppendLine( "            break;" );
        sb.AppendLine( "        case SWFUpload.UPLOAD_ERROR.SPECIFIED_FILE_ID_NOT_FOUND:" );
        sb.AppendLine( "            progress.setStatus(\"File not found.\");" );
        sb.AppendLine( "            this.debug(\"Error Code: The file was not found, File name: \" + file.name + \", File size: \" + file.size + \", Message: \" + message);" );
        sb.AppendLine( "            break;" );
        sb.AppendLine( "        case SWFUpload.UPLOAD_ERROR.FILE_VALIDATION_FAILED:" );
        sb.AppendLine( "            progress.setStatus(\"Failed Validation.  Upload skipped.\");" );
        sb.AppendLine( "            this.debug(\"Error Code: Upload Failed, File name: \" + file.name + \", File size: \" + file.size + \", Message: \" + message);" );
        sb.AppendLine( "            break;" );
        sb.AppendLine( "        case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:" );
        sb.AppendLine( "            if (uploadImageMessage && uploadImageMessage != '')" );
        sb.AppendLine( "            {" );
        sb.AppendLine( "                progress.setStatus( uploadImageMessage );" );
        sb.AppendLine( "                SetErrorFileList( file.name, uploadImageMessage );" );
        sb.AppendLine( "                uploadImageMessage = '';" );
        sb.AppendLine( "            }" );
        sb.AppendLine( "            else" );
        sb.AppendLine( "            {" );
        sb.AppendLine( "                progress.setStatus(\"Cancelled\");" );
        sb.AppendLine( "            }" );
        sb.AppendLine( "            break;" );
        sb.AppendLine( "        case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:" );
        sb.AppendLine( "            progress.setStatus(\"Stopped\");" );
        sb.AppendLine( "            break;" );
        sb.AppendLine( "        default:" );
        sb.AppendLine( "            progress.setStatus(\"Unhandled Error: \" + errorCode);" );
        sb.AppendLine( "            this.debug(\"Error Code: \" + errorCode + \", File name: \" + file.name + \", File size: \" + file.size + \", Message: \" + message);" );
        sb.AppendLine( "            break;" );
        sb.AppendLine( "        }" );
        sb.AppendLine( "    } catch (ex) {" );
        sb.AppendLine( "        alert('upload Error has error.');" );
        sb.AppendLine( "        this.debug(ex);" );
        sb.AppendLine( "    }" );
        sb.AppendLine( "}" );

        if (!Page.ClientScript.IsClientScriptBlockRegistered( String.Format( "UploadScriptBlock{0}", this.ClientID ) ))
            ScriptManager.RegisterClientScriptBlock( this, typeof( Page ), String.Format( "UploadScriptBlock{0}", this.ClientID ), sb.ToString(), true );
    }

    public String ErrorFileList
    {
        get
        {
            return uxHiddenFileList.Value;
        }
    }

    public String UploadDirectory
    {
        get
        {
            if (ViewState["UploadDirectory"] == null)
                return String.Empty;
            else
                return (String) ViewState["UploadDirectory"];
        }
        set
        {
            ViewState["UploadDirectory"] = value;
        }
    }

    public String ProductID
    {
        get
        {
            if (ViewState["ProductID"] == null)
                return String.Empty;
            else
                return (String) ViewState["ProductID"];
        }
        set
        {
            ViewState["ProductID"] = value;
        }
    }

    public String UploadFilePageName
    {
        get
        {
            if (ViewState["UploadFilePageName"] == null)
                return "Upload.aspx";
            else
                return (String) ViewState["UploadFilePageName"];
        }
        set
        {
            ViewState["UploadFilePageName"] = value;
        }
    }

    public String GridViewControlID
    {
        get
        {
            if (ViewState["GridViewControlID"] == null)
                return String.Empty;
            else
                return (String) ViewState["GridViewControlID"];
        }
        set
        {
            ViewState["GridViewControlID"] = value;
        }
    }

    public String MessageControlID
    {
        get
        {
            if (ViewState["MessageControlID"] == null)
                return String.Empty;
            else
                return (String) ViewState["MessageControlID"];
        }
        set
        {
            ViewState["MessageControlID"] = value;
        }
    }
}
