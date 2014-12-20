using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.DataAccess;
using Vevo.WebUI;
using Vevo.WebUI.Products;
using Vevo.Domain.Stores;

public partial class Components_DepartmentNavMenuList : BaseCategoryMenuNavListUserControl
{


    private void PopulateControls()
    {
        string rootID = DataAccessContext.Configurations.GetValue( "RootDepartment", new StoreRetriever().GetStore() );
        IList<Department> departmentList = DataAccessContext.DepartmentRepository.GetByParentIDAndRootID(
           StoreContext.Culture, rootID, rootID, "SortOrder", BoolFilter.ShowTrue );

        uxDepartmentNavListMenu.Items.Clear();
        uxDepartmentNavListMenu.MaximumDynamicDisplayLevels = DataAccessContext.Configurations.GetIntValue( "DepartmentMenuLevel" );

        DepartmentNavMenuBuilder menuBuilder =
            new DepartmentNavMenuBuilder( StoreContext.Culture, 
            UrlManager.GetDepartmentUrl, 
            MaxNode );
        foreach (Department department in departmentList)
        {
            uxDepartmentNavListMenu.Items.Add( menuBuilder.CreateMenuItemTree( department ) );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    public void Refresh()
    {
        PopulateControls();
    }
}
