using System;
using Vevo;

public partial class Admin_MainControls_ExportDepartmentList : AdminAdvancedBaseUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            uxExportDataList.SetTabIndex( 4 );
    }
}
