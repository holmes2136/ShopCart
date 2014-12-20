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

public partial class Components_CategoryNavNormalList : BaseCategoryMenuNavListUserControl
{

    private IList<Category> CreateDataSource()
    {
        string rootID = DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() );
        IList<Category> originalList = DataAccessContext.CategoryRepository.GetByParentIDAndRootID(
            StoreContext.Culture, rootID, rootID, "SortOrder", BoolFilter.ShowTrue );

        //// The code below does not work right now. Need to look at Clone() later why it does not work.
        //IList<Category> list =
        //    ListUtilities<Category>.CopyListDeep( originalList );

        IList<Category> list = new List<Category>();
        foreach (Category category in originalList)
        {
            Category item = new Category( StoreContext.Culture );
            item.CategoryID = category.CategoryID;
            item.Name = category.Name;
            item.UrlName = category.UrlName;
            item.ParentCategoryID = category.ParentCategoryID;
            item.CategoryListLayoutPath = category.CategoryListLayoutPath;
            list.Add( item );
            //Category item = (Category) category.Clone();
            //newList.Add( item );
            //item.Name += "...";
        }


        foreach (Category category in list)
        {
            if (!DataAccessContext.CategoryRepository.IsLeaf( category.CategoryID ))
                category.Name = category.Name + "...";
        }
        return list;
    }

    private void PopulateControls()
    {

        if (DataAccessContext.Configurations.GetBoolValue( "CategoryListModuleDisplay" ))
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

    protected string GetNavName( object category )
    {
        return ((Category) category).Name;
    }

    public void Refresh()
    {
        PopulateControls();
    }

}
