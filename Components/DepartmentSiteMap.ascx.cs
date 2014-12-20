using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.DataAccess;
using Vevo.WebUI;
using Vevo.Domain.Stores;

public partial class Components_DepartmentSiteMap : Vevo.WebUI.International.BaseLanguageUserControl
{
    private IList<Department> _department, _leafDepartment;

    private void GetDepartment()
    {
        _department = DataAccessContext.DepartmentRepository.GetByRootID(
            StoreContext.Culture,
            DataAccessContext.Configurations.GetValue( "RootDepartment", new StoreRetriever().GetStore() ),
            "Name",
            BoolFilter.ShowTrue );
        _leafDepartment = DataAccessContext.DepartmentRepository.GetByRootIDLeafOnly(
            StoreContext.Culture,
            DataAccessContext.Configurations.GetValue( "RootDepartment", new StoreRetriever().GetStore() ),
            "Name",
            BoolFilter.ShowTrue );
    }

    public void PopulateDepartment()
    {
        GetDepartment();
        uxDepartmentDataList.DataSource = _leafDepartment;
        uxDepartmentDataList.DataBind();
    }

    private void GenerateParent( string parentDepartmentID, Panel panel )
    {
        foreach (Department department in _department)
        {
            if (department.DepartmentID == parentDepartmentID)
            {
                string name = department.Name;
                string departmentID = department.DepartmentID;
                string urlName = department.UrlName;
                string newparentDepartmentID = department.ParentDepartmentID;

                GenerateParent( newparentDepartmentID, panel );
                HyperLink link = new HyperLink();
                link.NavigateUrl = UrlManager.GetDepartmentUrl( departmentID, urlName );
                link.Text = name;
                link.CssClass = "SiteMapParent";
                panel.Controls.Add( link );

                Label label = new Label();
                label.Text = " >> ";
                label.CssClass = "SiteMapSeparate";
                panel.Controls.Add( label );
            }
        }
    }

    private void GenerateBreadcrumb( DataListItem item )
    {
        Panel panel = (Panel) item.FindControl( "uxBreadcrumbPanel" );
        string name = DataBinder.Eval( item.DataItem, "Name" ).ToString();
        string departmentID = DataBinder.Eval( item.DataItem, "DepartmentID" ).ToString();
        string urlName = DataBinder.Eval( item.DataItem, "UrlName" ).ToString();
        string parentDepartmentID = DataBinder.Eval( item.DataItem, "ParentDepartmentID" ).ToString();

        GenerateParent( parentDepartmentID, panel );
        HyperLink link = new HyperLink();
        link.NavigateUrl = UrlManager.GetDepartmentUrl( departmentID, urlName );
        link.Text = name;
        link.CssClass = "SiteMapBreadcrumb";

        panel.Controls.Add( link );

        GenerateDepartment( item, departmentID );
    }

    private void GenerateDepartment( DataListItem item, string departmentID )
    {
        Repeater list = (Repeater) item.FindControl( "uxDepartmentItemRepeater" );
        list.DataSource = DataAccessContext.ProductRepository.GetByDepartmentID( StoreContext.Culture, departmentID, "Name", BoolFilter.ShowTrue, new StoreRetriever().GetCurrentStoreID() );
        list.DataBind();
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void uxDepartmentDataList_ItemDataBound( object sender, DataListItemEventArgs e )
    {
        GenerateBreadcrumb( e.Item );
    }
}
