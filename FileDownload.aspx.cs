using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.WebAppLib;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.Domain.Orders;
using Vevo.Domain.Stores;

public partial class FileDownload : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string ProductID
    {
        get
        {
            if (String.IsNullOrEmpty( Request.QueryString["ProductID"] ))
                return "0";
            else
                return Request.QueryString["ProductID"];
        }
    }

    private string Key
    {
        get
        {
            if (String.IsNullOrEmpty( Request.QueryString["Key"] ))
                return "0";
            else
                return Request.QueryString["Key"];
        }
    }

    private string OrderItemID
    {
        get
        {
            if (String.IsNullOrEmpty( Request.QueryString["OrderItemID"] ))
                return "0";
            else
                return Request.QueryString["OrderItemID"];
        }
    }

    private void UpdateDownloadCount()
    {
        if (OrderItemID == "0")
            return;

        OrderItem orderItem = DataAccessContext.OrderItemRepository.GetOne( OrderItemID );
        orderItem.DownloadCount += 1;
        DataAccessContext.OrderItemRepository.Save( orderItem );

    }
    protected void Page_PreRender( object sender, EventArgs e )
    {
        string errorMessage;
        DownloadKey downloadKey = new DownloadKey( ProductID, SystemConst.AnonymousUser, OrderItemID );

        bool isAnonymousCheckout = (
            downloadKey.VerifyKey( Key, out errorMessage ) == DownloadKey.VerifyStatus.OK);

        if (!isAnonymousCheckout)
        {
            downloadKey = new DownloadKey( ProductID, SystemConst.UnknownUser, OrderItemID );
            isAnonymousCheckout = (downloadKey.VerifyKey( Key, out errorMessage ) == DownloadKey.VerifyStatus.OK);
        }

        if (!Roles.IsUserInRole( "Customers" ) && !isAnonymousCheckout)
        {
            // Response.Redirect( "FileDownloadManager.aspx?ProductID=" + ProductID + "&Key=" + Key );
            Response.Redirect(
                "FileDownloadManager.aspx?ProductID=" + HttpContext.Current.Server.UrlEncode( ProductID ) +
                "&OrderItemID=" + HttpContext.Current.Server.UrlEncode( OrderItemID ) +
                "&Key=" + HttpContext.Current.Server.UrlEncode( Key ) );
        }

        if (!isAnonymousCheckout)
            downloadKey = new DownloadKey( ProductID, Membership.GetUser().UserName, OrderItemID );

        if (isAnonymousCheckout ||
            downloadKey.VerifyKey( Key, out errorMessage ) == DownloadKey.VerifyStatus.OK)
        {
            Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, new StoreRetriever().GetCurrentStoreID() );

            // Identify the file to download including its path.
            string filepath = HttpContext.Current.Server.MapPath( "~/" + product.DownloadPath );

            // Identify the file name.
            string filename = Path.GetFileName( filepath );

            // Open the file.
            Stream iStream = new FileStream( filepath, FileMode.Open,
                        FileAccess.Read, FileShare.Read );

            Exception ex;
            WebUtilities.CreateExportFile( System.Web.HttpContext.Current.ApplicationInstance, iStream, filename, out ex );
            if (ex !=null )
                uxMessageLabel.Text = "<strong>Error:</strong> " + ex.GetType().ToString();
        }
        else
        {
            uxMessageLabel.Text = "<strong>Error:</strong> [$Cannot Download]";
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }
}
