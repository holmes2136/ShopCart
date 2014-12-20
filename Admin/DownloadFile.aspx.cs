using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.WebAppLib;

public partial class AdminAdvanced_DownloadFile : System.Web.UI.Page
{
    private string filePath
    {
        get
        {
            if (Request.QueryString["FilePath"] != null)
                return Server.MapPath( Request.QueryString["FilePath"].ToString() );
            else
                return "";
        }
    }

    private string fileName
    {
        get { return Path.GetFileName( filePath ); }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        Stream iStream = new FileStream( filePath, FileMode.Open,
                            FileAccess.Read, FileShare.Read );
        Exception ex;
        WebUtilities.CreateExportFile( System.Web.HttpContext.Current.ApplicationInstance, iStream, fileName,out ex );
    }
}
