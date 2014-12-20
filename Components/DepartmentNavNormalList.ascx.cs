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
using Vevo.Data;
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using Vevo.WebUI.Products;

public partial class Components_DepartmentNavNormalList : BaseCategoryMenuNavListUserControl
{

    private IList<Department> CreateDataSource()
    {
        string rootID = DataAccessContext.Configurations.GetValue( "RootDepartment", new StoreRetriever().GetStore() );
        IList<Department> originalList = DataAccessContext.DepartmentRepository.GetByParentIDAndRootID(
            StoreContext.Culture, rootID, rootID, "SortOrder", BoolFilter.ShowTrue );

        //// The code below does not work right now. Need to look at Clone() later why it does not work.
        //IList<Department> list =
        //    ListUtilities<Department>.CopyListDeep( originalList );

        IList<Department> list = new List<Department>();
        foreach (Department department in originalList)
        {
            Department item = new Department( StoreContext.Culture );
            item.DepartmentID = department.DepartmentID;
            item.Name = department.Name;
            item.UrlName = department.UrlName;
            item.ParentDepartmentID = department.ParentDepartmentID;
            item.DepartmentListLayoutPath = department.DepartmentListLayoutPath;
            list.Add( item );
        }

        foreach (Department department in list)
        {
            if (!DataAccessContext.DepartmentRepository.IsLeaf( department.DepartmentID ))
                department.Name = department.Name + "...";
        }
        return list;
    }

    private void PopulateControls()
    {

        if (DataAccessContext.Configurations.GetBoolValue( "DepartmentListModuleDisplay" ))
        {
            uxList.DataSource = CreateDataSource();
            uxList.DataBind();
        }
        else
        {
            this.Visible = false;
        }
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    protected string GetNavName( object department )
    {
        return ((Department) department).Name;
    }

    public void Refresh()
    {
        PopulateControls();
    }

}
