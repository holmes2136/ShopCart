using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Xml;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using Vevo.WebUI.Products;

public partial class Components_CategoryNavTreeList : BaseCategoryMenuNavListUserControl
{
    int nodeDepth = 0;

    private string CurrentCategoryID
    {
        get
        {
            string id = Request.QueryString["CategoryID"];
            string urlName = Request.QueryString["CategoryName"];

            if (!String.IsNullOrEmpty( id ))
            {
                return id;
            }
            else if (!String.IsNullOrEmpty( urlName ))
            {
                Category category = DataAccessContext.CategoryRepository.GetOneByUrlName(
                    StoreContext.Culture, urlName );
                return category.CategoryID;
            }
            else
            {
                return "0";
            }
        }
    }

    private string GetVisibleLeafID()
    {
        Category category = DataAccessContext.CategoryRepository.GetOne(
            StoreContext.Culture, CurrentCategoryID );
        IList<IBaseCategory> breadcrumb = (IList<IBaseCategory>) category.GetBreadcrumb();
        if (breadcrumb.Count < MaxNode)
        {
            return CurrentCategoryID;
        }
        else
        {
            return ((Category) breadcrumb[MaxNode - 1]).CategoryID;
        }
    }

    private TreeNode FindSelectedCategoryNode( string categoryID, TreeNodeCollection nodes )
    {
        foreach (TreeNode node in nodes)
        {
            if (node.Value == categoryID)
                return node;

            TreeNode foundNode = FindSelectedCategoryNode( categoryID, node.ChildNodes );
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
        string rootID = DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() );
        IList<Category> categoryList = DataAccessContext.CategoryRepository.GetByParentIDAndRootID(
           StoreContext.Culture, rootID, rootID, "SortOrder", BoolFilter.ShowTrue );

        uxCategoryNavListTreeView.Nodes.Clear();
        uxCategoryNavListTreeView.MaxDataBindDepth = DataAccessContext.Configurations.GetIntValue( "CategoryMenuLevel" );

        foreach (Category category in categoryList)
        {
            uxCategoryNavListTreeView.Nodes.Add( NewNode( category ) );
        }

        if (CurrentCategoryID != "0")
        {
            TreeNode node = FindSelectedCategoryNode( GetVisibleLeafID(), uxCategoryNavListTreeView.Nodes );
            if (node != null)
                ExpandNode( node );
        }
    }

    private void PopulateCategories( TreeNode node, string parentCategoryID )
    {
        IList<Category> list = DataAccessContext.CategoryRepository.GetByParentID(
            StoreContext.Culture,
            parentCategoryID,
            "SortOrder",
            BoolFilter.ShowTrue );

        foreach (Category category in list)
        {
            node.ChildNodes.Add( NewNode( category ) );
        }
    }


    private TreeNode NewNode( Category category )
    {
        TreeNode newNode = new TreeNode();
        newNode.Text = category.Name;
        newNode.Value = category.CategoryID.ToString();

        if (DataAccessContext.CategoryRepository.IsCategoryIDNotLeaf( category.CategoryID ))
        {
            newNode.SelectAction = TreeNodeSelectAction.Expand;
            newNode.NavigateUrl = UrlManager.GetCategoryUrl( category.CategoryID, category.UrlName );
            newNode.Collapse();
            nodeDepth++;
            if (nodeDepth < MaxNode) //Max Tree depth
                PopulateCategories( newNode, category.CategoryID );
            nodeDepth--;
        }
        else
        {
            newNode.SelectAction = TreeNodeSelectAction.Select;
            newNode.NavigateUrl = UrlManager.GetCategoryUrl( category.CategoryID, category.UrlName );
            newNode.PopulateOnDemand = true;
        }
        return newNode;
    }

    protected void uxCategoryNavListTreeView_DataBound( object sender, EventArgs e )
    {
        TreeNodeEventArgs ee = (TreeNodeEventArgs) e;
        XmlElement xe = (XmlElement) ee.Node.DataItem;
        ee.Node.Text = xe.Attributes["DisplayThisText"].Value;
        ee.Node.SelectAction = TreeNodeSelectAction.Expand;
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
