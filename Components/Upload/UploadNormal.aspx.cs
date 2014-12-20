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

public partial class Components_Upload_UploadNormal : System.Web.UI.Page
{
    public string PathDestination
    {
        get
        {
            return Request.Form["PathDestination"];
        }
    }

    public string UploadControl
    {
        get
        {
            return Request.Form["UploadControl"];
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        System.Drawing.Image original_image = null;
        String fileNameInfo = String.Empty;
        String tempPath = String.Empty;
        bool directoryNotExists = false;
        try
        {
            if (String.IsNullOrEmpty( UploadControl ))
            {
                Response.End();
            }
            // Get the data
            HttpPostedFile image_upload = Request.Files["Filedata"];
            fileNameInfo = Path.GetFileName( image_upload.FileName );
            tempPath = Server.MapPath( "../../" + PathDestination );
            if (Directory.Exists( tempPath ))
            {
                image_upload.SaveAs( tempPath + fileNameInfo );
                Response.StatusCode = 200;
                Response.Write( "Success" );
            }
            else
            {
                directoryNotExists = true;
                Response.End();
            }
        }
        catch
        {
            if (String.IsNullOrEmpty( UploadControl ))
            {
                Response.StatusCode = 601;
                Response.Write( "Can't use this page alone." );
            }
            else
            {
                if (directoryNotExists)
                {
                    Response.StatusCode = 600;
                    Response.Write( "Directory Not Exists" );
                }
                else
                {
                    Response.StatusCode = 500;
                    Response.Write( "An error occured" );
                }

                //if ((ex.Message.Length > 30) && (ex.Message.Substring( 0, 34 ) == "The process cannot access the file") && (ex.Message.Substring( ex.Message.Length - 44 ) == "because it is being used by another process."))
                //{
                //    Response.StatusCode = 602;
                //    Response.Write( ex.Message.Substring( 0, 34 ) + " " + ex.Message.Substring( ex.Message.Length - 44 ) );
                //}

            }
            // If any kind of error occurs return a 500 Internal Server error
            Response.End();
        }
        finally
        {
            // Clean up
            if (original_image != null) original_image.Dispose();
            Response.End();
        }
    }
}
