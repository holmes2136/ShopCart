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
using Vevo.Domain;
using Vevo.Domain.Configurations;
using Vevo.Domain.Stores;
using Vevo.WebAppLib;

public partial class AdminAdvanced_Components_SiteConfig_LayoutDepartmentListSelect : AdminAdvancedBaseUserControl, IConfigUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {

    }

    public void PopulateControls()
    {
        uxCommonSelect.PopulateFilesControls( "DefaultDepartmentListLayout", SystemConst.LayoutDepartmentListPath );
    }

    public void PopulateControls( Store store )
    {
        uxCommonSelect.PopulateFilesControls( "DefaultDepartmentListLayout", SystemConst.LayoutDepartmentListPath, store );
    }

    public void Update()
    {
        uxCommonSelect.Update( "DefaultDepartmentListLayout" );
    }

    public void Update( Store store )
    {
        uxCommonSelect.Update( "DefaultDepartmentListLayout", store );
    }

    #region IConfigUserControl Members

    public void Populate( Vevo.Domain.Configurations.Configuration config )
    {
        PopulateControls();
    }

    #endregion
}
