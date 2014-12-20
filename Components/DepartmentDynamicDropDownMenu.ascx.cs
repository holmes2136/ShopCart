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
using System.Collections.Generic;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.WebUI.Products;
using Vevo.Domain.Stores;

public partial class Components_DepartmentDynamicDropDownMenu : BaseCategoryMenuNavListUserControl
{
    private string _rootMenuName;

    private void Components_DepartmentDynamicDropDownMenu_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        PopulateControls();
    }

    private void PopulateControls()
    {

        MaxNode = DataAccessContext.Configurations.GetIntValue( "DepartmentDynamicDropDownLevel" );
        uxDepartmentDropDownMenu.Items.Clear();
        uxDepartmentDropDownMenu.MaximumDynamicDisplayLevels = MaxNode;

        MenuItem rootMenu = new MenuItem();
        rootMenu.Text = RootMenuName;
        rootMenu.NavigateUrl = "~/Department.aspx";

        string rootID = DataAccessContext.Configurations.GetValue( "RootDepartment", new StoreRetriever().GetStore() );
        IList<Department> departmentList = DataAccessContext.DepartmentRepository.GetByParentIDAndRootID(
           StoreContext.Culture, rootID, rootID, "SortOrder", BoolFilter.ShowTrue );

        DepartmentNavMenuBuilder menuBuilder =
			new DepartmentNavMenuBuilder( StoreContext.Culture, UrlManager.GetDepartmentUrl, MaxNode );
        foreach (Department department in departmentList)
        {
            rootMenu.ChildItems.Add( menuBuilder.CreateMenuItemTree(department) );
        }
        uxDepartmentDropDownMenu.Items.Add( rootMenu );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler(
            Components_DepartmentDynamicDropDownMenu_StoreCultureChanged );

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    public string RootMenuName
    {
        get { return _rootMenuName; }
        set { _rootMenuName = value; }
    }
}
