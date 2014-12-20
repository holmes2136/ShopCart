using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;

public partial class AdminAdvanced_MainControls_ProductImageList : AdminAdvancedBaseUserControl
{
    public string ProductID
    {
        get
        {
            if (string.IsNullOrEmpty( MainContext.QueryString["ProductID"] ))
                return "0";
            else
                return MainContext.QueryString["ProductID"];
        }
    }

    private void PopulateControls()
    {
        uxReviewLink.PageQueryString = String.Format( "ProductID={0}", ProductID );
        uxProductDetailLink.PageQueryString = String.Format( "ProductID={0}", ProductID );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxOKImageButton_Click( object sender, EventArgs e )
    {
        uxProductImage.Update();
    }
}
