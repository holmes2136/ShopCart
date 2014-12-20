using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;

public partial class Admin_MainControls_ExportProductList : AdminAdvancedBaseUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            uxExportDataList.SetTabIndex( 2 );
    }
}
