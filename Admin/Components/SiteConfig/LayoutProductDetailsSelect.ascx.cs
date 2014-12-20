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

public partial class AdminAdvanced_Components_SiteConfig_LayoutProductDetailsSelect : AdminAdvancedBaseUserControl, IConfigUserControl
{

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    public void PopulateControls()
    {
        uxCommonSelect.PopulateFilesControls( "DefaultProductDetailsLayout", SystemConst.LayoutProductDetailsPath );
    }

    public void PopulateControls( Store store )
    {
        uxCommonSelect.PopulateFilesControls( "DefaultProductDetailsLayout", SystemConst.LayoutProductDetailsPath, store );
    }

    public void Update()
    {
        uxCommonSelect.Update( "DefaultProductDetailsLayout" );
    }

    public void Update( Store store )
    {
        uxCommonSelect.Update( "DefaultProductDetailsLayout", store );
    }

    #region IConfigUserControl Members

    public void Populate( Vevo.Domain.Configurations.Configuration config )
    {
        // throw new Exception( "The method or operation is not implemented." );
        PopulateControls();
    }

    #endregion
}
