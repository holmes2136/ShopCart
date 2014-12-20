using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Shared.Utilities;
using Vevo.WebUI.Users;

public partial class AdminAdvanced_Components_ExportDataList : AdminAdvancedBaseUserControl
{
    #region Private

    private const string _exportCustomerPage = "ExportCustomerList.ascx";
    private const string _exportOrderPage = "ExportOrderList.ascx";
    private const int _exportCustomerTabIndex = 0;
    private const int _exportOrderTabIndex = 1;

    private void SetExportViewPermission()
    {
        AdminHelper admin = AdminHelper.LoadByUserName( Page.User.Identity.Name );
        if (!admin.CanViewPage( _exportCustomerPage ))
            uxTabContainer.Tabs[_exportCustomerTabIndex].Enabled = false;
        if (!admin.CanViewPage( _exportOrderPage ))
            uxTabContainer.Tabs[_exportOrderTabIndex].Enabled = false;
    }

    private void SetExportModifyPermission()
    {
        AdminHelper admin = AdminHelper.LoadByUserName( Page.User.Identity.Name );
        if (!admin.CanModifyPage( _exportCustomerPage ))
            uxExportCustomer.HideCustomerGenerateButton();

        if (!admin.CanModifyPage( _exportOrderPage ))
        {
            uxExportOrder.HideOrderGenerateButton();
        }
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            SetExportViewPermission();
            SetExportModifyPermission();
        }
    }

    #endregion

    #region Public Methods

    public void SetTabIndex( int tabIndex )
    {
        uxTabContainer.ActiveTabIndex = tabIndex;
    }

    #endregion
}
