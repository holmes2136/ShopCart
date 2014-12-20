using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using Vevo.WebUI.Products;

public partial class Components_DepartmentNavTreeList : BaseCategoryMenuNavListUserControl
{
    int nodeDepth = 0;

    private string CurrentDepartmentID
    {
        get
        {
            string id = Request.QueryString["DepartmentID"];
            string urlName = Request.QueryString["DepartmentName"];

            if (!String.IsNullOrEmpty( id ))
            {
                return id;
            }
            else if (!String.IsNullOrEmpty( urlName ))
            {
                Department department = DataAccessContext.DepartmentRepository.GetOneByUrlName(
                    StoreContext.Culture, urlName );
                return department.DepartmentID;
            }
            else
            {
                return "0";
            }
        }
    }

    private string GetVisibleLeafID()
    {
        Department department = DataAccessContext.DepartmentRepository.GetOne(
            StoreContext.Culture, CurrentDepartmentID );
        IList<IBaseCategory> breadcrumb = (IList<IBaseCategory>) department.GetBreadcrumb();
        if (breadcrumb.Count < MaxNode)
        {
            return CurrentDepartmentID;
        }
        else
        {
            return ((Department) breadcrumb[MaxNode - 1]).DepartmentID;
        }
    }

    private TreeNode FindSelectedDepartmentNode( string departmentID, TreeNodeCollection nodes )
    {
        foreach (TreeNode node in nodes)
        {
            if (node.Value == departmentID)
                return node;

            TreeNode foundNode = FindSelectedDepartmentNode( departmentID, node.ChildNodes );
            if (foundNode != null)
                return foundNode;
        }

        return null;
    }

    private void ExpandNode( TreeNode node )
    {
        node.Select();
        node.Expand();

        TreeNode parent = node.Parent;
        while (parent != null)
        {
            parent.Expand();
            parent = parent.Parent;
        }
    }

    private void PopulateControls()
    {
        string rootID = DataAccessContext.Configurations.GetValue( "RootDepartment", new StoreRetriever().GetStore() );
        IList<Department> departmentList = DataAccessContext.DepartmentRepository.GetByParentIDAndRootID(
           StoreContext.Culture, rootID, rootID, "SortOrder", BoolFilter.ShowTrue );

        uxDepartmentNavListTreeView.Nodes.Clear();
        uxDepartmentNavListTreeView.MaxDataBindDepth = DataAccessContext.Configurations.GetIntValue( "DepartmentMenuLevel" );

        foreach (Department department in departmentList)
        {
            uxDepartmentNavListTreeView.Nodes.Add( NewNode( department ) );
        }

        if (CurrentDepartmentID != "0")
        {
            TreeNode node = FindSelectedDepartmentNode( GetVisibleLeafID(), uxDepartmentNavListTreeView.Nodes );
            if (node != null)
                ExpandNode( node );
        }
    }

    private void PopulateCategories( TreeNode node, string parentDepartmentID )
    {
        IList<Department> list = DataAccessContext.DepartmentRepository.GetByParentID(
            StoreContext.Culture,
            parentDepartmentID,
            "SortOrder",
            BoolFilter.ShowTrue );

        foreach (Department department in list)
        {
            node.ChildNodes.Add( NewNode( department ) );
        }
    }


    private TreeNode NewNode( Department department )
    {
        TreeNode newNode = new TreeNode();
        newNode.Text = department.Name;
        newNode.Value = department.DepartmentID.ToString();

        if (DataAccessContext.DepartmentRepository.IsDepartmentIDNotLeaf( department.DepartmentID ))
        {
            newNode.SelectAction = TreeNodeSelectAction.Expand;
            newNode.NavigateUrl = UrlManager.GetDepartmentUrl( department.DepartmentID, department.UrlName );
            newNode.Collapse();
            nodeDepth++;
            if (nodeDepth < MaxNode) //Max Tree depth
                PopulateCategories( newNode, department.DepartmentID );
            nodeDepth--;
        }
        else
        {
            newNode.SelectAction = TreeNodeSelectAction.Select;
            newNode.NavigateUrl = UrlManager.GetDepartmentUrl( department.DepartmentID, department.UrlName );
            newNode.PopulateOnDemand = true;
        }
        return newNode;
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
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
