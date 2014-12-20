using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Base.Domain;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;

public partial class AdminAdvanced_MainControls_CategoryList : AdminAdvancedBaseListControl
{
    private class CategoryDisplayItem
    {
        private string _categorID;
        private string _name;
        private string _description;
        private string _imageFile;
        private string _parentCategoryID;
        private int _level;
        private bool _isEnabled;


        public string CategoryID { get { return _categorID; } set { _categorID = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public string ImageFile { get { return _imageFile; } set { _imageFile = value; } }
        public string ParentCategoryID { get { return _parentCategoryID; } set { _parentCategoryID = value; } }
        public Int32 Level { get { return _level; } set { _level = value; } }
        public bool IsEnabled { get { return _isEnabled; } set { _isEnabled = value; } }
    }

    private const int CategoryIDColumn = 1;
    private const int CategoryIndentInPixels = 20;
    private const int MaxCategoryDescriptionLength = 100;
    private string _sortPage = "CategorySorting.ascx";

    private string RootCategoryID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
            {
                if (MainContext.QueryString["RootID"] != null)
                    return MainContext.QueryString["RootID"];
                else
                    return DataAccessContext.Configurations.GetValue(
                        "RootCategory", DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID ) );
            }
            else
            {
                return DataAccessContext.Configurations.GetValue(
                        "RootCategory", DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID ) );
            }
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.CategoryRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list, "UrlName" );
    }

    private IList<CategoryDisplayItem> _categorySource = new List<CategoryDisplayItem>();

    private void BuildData( string parentID, int level )
    {
        IList<Category> categoryList = DataAccessContext.CategoryRepository.GetByParentIDAndRootID( uxLanguageControl.CurrentCulture, parentID, RootCategoryID, "CategoryID", BoolFilter.ShowAll );
        string description;

        for (int i = 0; i < categoryList.Count; i++)
        {
            if (categoryList[i].Description.Length > MaxCategoryDescriptionLength)
                description = categoryList[i].Description.Substring( 0, MaxCategoryDescriptionLength ) + "...";
            else
                description = categoryList[i].Description;
            BuildRow( categoryList[i],
                description,
                level
                );
            BuildData( categoryList[i].CategoryID, level + 1 );
        }
    }

    private void BuildRow( Category category, string description, int level )
    {
        CategoryDisplayItem temp = new CategoryDisplayItem();
        temp.CategoryID = category.CategoryID;
        temp.Name = category.Name;
        temp.Description = description;
        temp.ImageFile = category.ImageFile;
        temp.ParentCategoryID = category.ParentCategoryID;
        temp.Level = level;
        temp.IsEnabled = category.IsEnabled;

        _categorySource.Add( temp );
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }

        if (uxGrid.Rows.Count > 0)
        {
            DeleteVisible( true );
        }
        else
        {
            DeleteVisible( false );
        }

        if (!IsAdminModifiable())
        {
            uxAddButton.Visible = false;
            DeleteVisible( false );
        }

        uxSortButton.Visible = IsAdminViewable( _sortPage );
    }

    private void DeleteVisible( bool value )
    {
        uxDeleteButton.Visible = value;
        if (value)
        {
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {
                uxDeleteConfirmButton.TargetControlID = "uxDeleteButton";
                uxConfirmModalPopup.TargetControlID = "uxDeleteButton";
            }
            else
            {
                uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
                uxConfirmModalPopup.TargetControlID = "uxDummyButton";
            }
        }
        else
        {
            uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
            uxConfirmModalPopup.TargetControlID = "uxDummyButton";
        }
    }

    private string[] GetCheckedIDs()
    {
        ArrayList items = new ArrayList();

        foreach (GridViewRow row in uxGrid.Rows)
        {
            CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
            if (deleteCheck.Checked)
            {
                string id = ((HiddenField) row.Cells[0].FindControl( "uxCategoryIDHidden" )).Value.Trim();
                items.Add( id );
            }
        }

        string[] result = new string[items.Count];
        items.CopyTo( result );
        return result;
    }

    private bool ContainsProducts( string[] idArray, out string containingCategoryID )
    {
        foreach (string id in idArray)
        {
            int productCount = DataAccessContext.ProductRepository.GetProductCountByCategoryID( id, BoolFilter.ShowAll );

            if (productCount > 0)
            {
                containingCategoryID = id;
                return true;
            }
        }

        containingCategoryID = "";
        return false;
    }

    private bool ContainsCategories( string[] idArray, out string containingCategoryID )
    {
        foreach (string id in idArray)
        {
            Category category = DataAccessContext.CategoryRepository.GetOne( uxLanguageControl.CurrentCulture, id );
            if (!category.IsLeafCategory())
            {
                containingCategoryID = category.CategoryID;
                return true;
            }
        }

        containingCategoryID = "";
        return false;
    }

    private void DeleteItems()
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string id = ((HiddenField) row.Cells[0].FindControl( "uxCategoryIDHidden" )).Value.Trim();
                    string parentId = ((HiddenField) row.Cells[0].FindControl( "uxCategoryParentIDHidden" )).Value.Trim();
                    DataAccessContext.CategoryRepository.Delete( id );

                    IList<Category> categoryList = DataAccessContext.CategoryRepository.GetByParentID( uxLanguageControl.CurrentCulture,
                        parentId, "SortOrder", BoolFilter.ShowTrue );

                    string[] result = new string[categoryList.Count];
                    for (int i = 0; i < categoryList.Count; i++)
                    {
                        result[i] = categoryList[i].CategoryID;
                    }
                    DataAccessContext.CategoryRepository.UpdateSortOrder( result );
                    HttpContext.Current.Session[SystemConst.CategoryTreeViewLeftKey] = null;
                    deleted = true;
                }
            }
            if (deleted)
            {
                uxMessage.DisplayMessage( Resources.CategoryMessages.DeleteSuccess );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();
    }


    private void uxGrid_ClearSearchtHandler( object sender, EventArgs e )
    {
        // Do not keep search filter while switching language
        uxSearchFilter.ClearFilter();
        RefreshGrid();
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            SetUpSearchFilter();
        }
        RegisterGridView( uxGrid, "CategoryID" );

        RegisterLanguageControl( uxLanguageControl );
        RegisterSearchFilter( uxSearchFilter );

        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();

        if (KeyUtilities.IsMultistoreLicense())
            uxBreadcrumbLabel.Text = DataAccessContext.CategoryRepository.GetOne( uxLanguageControl.CurrentCulture, RootCategoryID ).Name
                + " (" + RootCategoryID + ")";
    }

    protected string PageQueryString( string categoryID )
    {
        return String.Format( "RootID={0}&CategoryID={1}", RootCategoryID, categoryID );
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "CategoryAdd.ascx", String.Format( "RootID={0}", RootCategoryID ) );
    }

    protected string GetName( string name, string id, string level )
    {
        string showname = name + " (" + id + ")";
        if (level == "0" && uxSearchFilter.SearchFilterObj.FieldName == "")
            return "<b>" + showname + "</b>";
        else
            return showname;
    }

    protected string GetSpaceStyle( object level )
    {
        if (uxSearchFilter.SearchFilterObj.FieldName == "")
            return "float: left; width: " + (CategoryIndentInPixels * ConvertUtilities.ToInt32( level )) + "px;";
        else
            return "float: left;";
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        string[] checkedIDs = GetCheckedIDs();

        string containingID;
        if (ContainsProducts( checkedIDs, out containingID ))
        {
            Category category = DataAccessContext.CategoryRepository.GetOne( uxLanguageControl.CurrentCulture, containingID );
            uxMessage.DisplayError(
                Resources.CategoryMessages.DeleteErrorContainingProducts,
                category.Name, containingID );
        }
        else if (ContainsCategories( checkedIDs, out containingID ))
        {
            Category category = DataAccessContext.CategoryRepository.GetOne( uxLanguageControl.CurrentCulture, containingID );
            uxMessage.DisplayError(
                Resources.CategoryMessages.DeleteErrorContainingCategories,
                category.Name, containingID );
        }
        else
        {
            DeleteItems();
        }

        uxStatusHidden.Value = "Deleted";
    }

    protected void uxSortButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( _sortPage );
    }

    protected override void RefreshGrid()
    {
        if (uxSearchFilter.SearchFilterObj.FieldName == "")
        {
            //_allCategories = DataAccessContext.CategoryRepository.GetAll( uxLanguageControl.CurrentCulture, "SortOrder" );
            BuildData( RootCategoryID, 0 );
        }
        else
        {
            IList<Category> categoryList = DataAccessContext.CategoryRepository.SearchCategoryByRootID(
                uxLanguageControl.CurrentCulture,
                GridHelper.GetFullSortText(),
                uxSearchFilter.SearchFilterObj,
                RootCategoryID );

            foreach (Category category in categoryList)
            {
                string description = category.Description;

                BuildRow( category, description, 0 );
            }

        }

        uxGrid.DataSource = _categorySource;
        uxGrid.DataBind();
    }
}
