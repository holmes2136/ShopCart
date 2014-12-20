using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Shared.DataAccess;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebUI;

public partial class Components_CategoryCheckList : Vevo.WebUI.International.BaseLanguageUserControl
{
    //private string _categoryID;
    private bool _isSelectedAll = false;
    private IList<Category> _tableCategory;

    private IList<Category> TableCategory
    {
        get
        {
            return _tableCategory;
        }
        set
        {
            _tableCategory = value;
        }
    }

    public string CurrentID
    {
        get
        {
            //return _categoryID;
            return ViewState["CategoryID"].ToString();
        }
        set
        {
            //_categoryID = value;
            ViewState["CategoryID"] = value;
        }
    }

    public bool IsSeletedAll
    {
        get
        {
            return _isSelectedAll;
        }
        set
        {
            _isSelectedAll = value;
        }
    }

    private IList<Category> GetChildrenOf( string categoryID )
    {
        IList<Category> categoryList = new List<Category>();
        foreach (Category category in TableCategory)
        {
            if (category.ParentCategoryID == categoryID)
                categoryList.Add( category );
        }

        return categoryList;
    }

    private string[] GetChildrenCategoryIDs( string id )
    {
        IList<Category> categoryList = GetChildrenOf( id );

        string[] categoies = new string[categoryList.Count];

        for (int i = 0; i < categoies.Length; i++)
            categoies[i] = categoryList[i].CategoryID;
        return categoies;
    }

    private void GetLeafOfID( NameValueCollection leafList, string id )
    {
        string[] children = GetChildrenCategoryIDs( id );
        if (children.Length == 0)
        {
            if (id != "0")
            {
                Category category = DataAccessContext.CategoryRepository.GetOne( StoreContext.Culture, id );

                leafList[id] = id + ":" + category.Name;
            }
        }
        else
        {
            foreach (string child in children)
            {
                Category category = DataAccessContext.CategoryRepository.GetOne( StoreContext.Culture, child );
                leafList[child] = child + ":" + category.Name;
                GetLeafOfID( leafList, child );
            }
        }
    }

    private string[] GetLeafCategoryIDs( string categoryID )
    {
        NameValueCollection leafList = new NameValueCollection();

        GetLeafOfID( leafList, categoryID );

        string[] result = new string[leafList.Count];
        leafList.CopyTo( result, 0 );

        return result;
    }

    private string[] SpiltString( string str )
    {
        return str.Split( ':' );
    }

    private void SetCheckList()
    {
        uxCategoryCheckList.Items.Clear();

        TableCategory = DataAccessContext.CategoryRepository.GetAll( StoreContext.Culture, "SortOrder", BoolFilter.ShowTrue );

        string[] leafCategoryIDs = GetLeafCategoryIDs( CurrentID );

        if (leafCategoryIDs.Length == 1 && SpiltString( leafCategoryIDs[0] )[0].Equals( CurrentID ))
        {
            CategoryCheckListPanel.Visible = false;
        }
        else
        {
            foreach (string leafCategoryID in leafCategoryIDs)
            {
                string[] result = SpiltString( leafCategoryID );
                uxCategoryCheckList.Items.Add( new ListItem( result[1], result[0] ) );
            }
            uxCategoryCheckList.DataBind();
        }
    }

    public string[] CheckedCategoryID()
    {
        string[] categories = new string[uxCategoryCheckList.Items.Count];
        int i = 0;
        foreach (ListItem item in uxCategoryCheckList.Items)
        {
            if (item.Selected)
            {
                categories[i++] = item.Value;
            }
        }
        string[] result = new string[i];
        for (int j = 0; j < i; j++)
        {
            if (categories[j] != "" && categories[j] != null)
                result[j] = categories[j];
            else
                break;
        }
        return result;
    }

    public void refresh()
    {
        if (ViewState["CategoryID"] != null)
            SetCheckList();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
            SetCheckList();
    }

}
