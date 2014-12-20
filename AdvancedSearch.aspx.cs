using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.Products;

public partial class AdvancedSearch : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    #region Private
    private const string _hide = "[$Hide]";
    private const string _show = "[$Show]";

    private IList<Category> _tableCategory;
    private IList<Department> _tableDepartment;
    private string _keywordText = "[$Keyword]";

    private bool IsSearched
    {
        get
        {
            if (Request.QueryString["IsSearched"] == null)
                return false;
            else
                return Convert.ToBoolean( Request.QueryString["IsSearched"] );
        }
    }

    private string CategoryIDs
    {
        get
        {
            if (Request.QueryString["CategoryIDs"] == null)
                return String.Empty;
            else
                return Request.QueryString["CategoryIDs"];
        }
    }

    private string DepartmentIDs
    {
        get
        {
            if (Request.QueryString["DepartmentIDs"] == null)
                return String.Empty;
            else
                return Request.QueryString["DepartmentIDs"];
        }
    }


    private string ManufacturerID
    {
        get
        {
            if (Request.QueryString["ManufacturerID"] == null)
                return String.Empty;
            else
                return Request.QueryString["ManufacturerID"];
        }
    }

    private string Keyword
    {
        get
        {
            if (Request.QueryString["Keyword"] == null)
                return String.Empty;
            else
                return Request.QueryString["Keyword"];
        }
    }

    private string SearchType
    {
        get
        {
            if (Request.QueryString["Type"] == null)
                return String.Empty;
            else
                return Request.QueryString["Type"];
        }
    }

    private string Price1
    {
        get
        {
            if (Request.QueryString["Price1"] == null)
                return String.Empty;
            else
                return Request.QueryString["Price1"];
        }
    }

    private string Price2
    {
        get
        {
            if (Request.QueryString["Price2"] == null)
                return String.Empty;
            else
                return Request.QueryString["Price2"];
        }
    }

    private string ProductSearchType
    {
        get
        {
            if (Request.QueryString["SearchType"] == null)
                return String.Empty;
            else
                return Request.QueryString["SearchType"];
        }
    }

    private bool IsNewSearch
    {
        get
        {
            if (Request.QueryString["IsNewSearch"] == null)
                return false;
            else
                return Convert.ToBoolean( Request.QueryString["IsNewSearch"] );
        }
    }

    private IList<Vevo.Domain.Contents.ContentMenuItem> _listContentMenuItem;

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

    private IList<Department> TableDepartment
    {
        get
        {
            return _tableDepartment;
        }
        set
        {
            _tableDepartment = value;
        }
    }

    private int CurrentPage
    {
        get
        {
            int result;
            string page = Request.QueryString["Page"];
            if (String.IsNullOrEmpty( page ) ||
                !int.TryParse( page, out result ))
                return 1;
            else
                return result;
        }
    }

    private IList<Vevo.Domain.Contents.ContentMenuItem> ListContentMenuItem
    {
        get
        {
            return _listContentMenuItem;
        }
        set
        {
            _listContentMenuItem = value;
        }
    }

    private bool IsUseEnhanceAdvancedSearch
    {
        get
        {
            string searchMode = DataAccessContext.Configurations.GetValue( "AdvancedSearchMode", new StoreRetriever().GetStore() );
            if (searchMode == "Enhance")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private void RefreshGrid()
    {
        IList<Category> categoryList = DataAccessContext.CategoryRepository.GetByParentID(
           StoreContext.Culture,
           DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() ),
           "SortOrder",
           BoolFilter.ShowTrue );

        IList<Department> departmentList = DataAccessContext.DepartmentRepository.GetByParentID(
            StoreContext.Culture,
            DataAccessContext.Configurations.GetValue( "RootDepartment", new StoreRetriever().GetStore() ),
           "SortOrder",
           BoolFilter.ShowTrue );


        uxCategoryList.DataSource = categoryList;
        uxCategoryList.DataBind();

        uxDepartmentsList.DataSource = departmentList;
        uxDepartmentsList.DataBind();

        if (DataAccessContext.ContentMenuRepository.GetOne(
            DataAccessContext.Configurations.GetValue( "TopContentMenu" ) ).IsEnabled)
        {
            uxTopContentMenuItemList.DataSource = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
                StoreContext.Culture, DataAccessContext.Configurations.GetValue( "TopContentMenu" ),
                "SortOrder", BoolFilter.ShowTrue );
            uxTopContentMenuItemList.DataBind();
        }

        if (DataAccessContext.ContentMenuRepository.GetOne(
            DataAccessContext.Configurations.GetValue( "LeftContentMenu" ) ).IsEnabled)
        {
            uxLeftContentMenuItemList.DataSource = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
                StoreContext.Culture, DataAccessContext.Configurations.GetValue( "LeftContentMenu" ),
                "SortOrder", BoolFilter.ShowTrue );
            uxLeftContentMenuItemList.DataBind();
        }

        if (DataAccessContext.ContentMenuRepository.GetOne(
            DataAccessContext.Configurations.GetValue( "RightContentMenu" ) ).IsEnabled)
        {
            uxRightContentMenuItemList.DataSource = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID
                ( StoreContext.Culture, DataAccessContext.Configurations.GetValue( "RightContentMenu" ),
                "SortOrder", BoolFilter.ShowTrue );
            uxRightContentMenuItemList.DataBind();
        }

        if (uxRightContentMenuItemList.Items.Count == 0 && uxLeftContentMenuItemList.Items.Count == 0 && uxTopContentMenuItemList.Items.Count == 0)
            uxContentCheckPanel.Visible = false;
    }

    private string[] GetChildrenCategoryIDs( string id )
    {
        IList<Category> childList = new List<Category>();
        foreach (Category category in TableCategory)
        {
            if (category.ParentCategoryID == id)
                childList.Add( category );
        }

        string[] categoies = new string[childList.Count];

        for (int i = 0; i < categoies.Length; i++)
            categoies[i] = childList[i].CategoryID;
        return categoies;
    }

    private string[] GetChildrenDepartmentIDs( string id )
    {
        IList<Department> childList = new List<Department>();
        foreach (Department department in TableDepartment)
        {
            if (department.ParentDepartmentID == id)
                childList.Add( department );
        }

        string[] departments = new string[childList.Count];

        for (int i = 0; i < departments.Length; i++)
            departments[i] = childList[i].DepartmentID;
        return departments;
    }

    private string[] GetChildrenContentMenuItemIDs( string id )
    {
        Vevo.Domain.Contents.ContentMenuItem item = DataAccessContext.ContentMenuItemRepository.GetOne(
            StoreContext.Culture, id );

        IList<Vevo.Domain.Contents.ContentMenuItem> list = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
            StoreContext.Culture, item.ReferringMenuID, "SortOrder", BoolFilter.ShowTrue );
        string[] contentmenuitems = new string[list.Count];

        for (int i = 0; i < contentmenuitems.Length; i++)
            contentmenuitems[i] = list[i].ContentMenuItemID;
        return contentmenuitems;
    }

    private void GetLeafOfID( NameValueCollection leafList, string id )
    {
        string[] children = GetChildrenCategoryIDs( id );
        if (children.Length == 0)
        {
            leafList[id] = id;
        }
        else
        {
            foreach (string child in children)
            {
                GetLeafOfID( leafList, child );
            }
        }
    }

    private void GetLeafDepartmentsID( NameValueCollection leafList, string id )
    {
        string[] children = GetChildrenDepartmentIDs( id );
        if (children.Length == 0)
        {
            leafList[id] = id;
        }
        else
        {
            foreach (string child in children)
            {
                GetLeafDepartmentsID( leafList, child );
            }
        }
    }

    private void GetLeafOfContentMenuItemID( NameValueCollection leafList, string id )
    {
        string[] children = GetChildrenContentMenuItemIDs( id );
        if (children.Length == 0)
        {
            leafList[id] = id;
        }
        else
        {
            foreach (string child in children)
            {
                GetLeafOfContentMenuItemID( leafList, child );
            }
        }
    }

    private void GetLeafCategoryIDs( NameValueCollection leafList, string[] categoryIDs )
    {
        foreach (string id in categoryIDs)
        {
            GetLeafOfID( leafList, id );
        }
    }

    private void GetLeafDepartmentIDs( NameValueCollection leafList, string[] departmentIDs )
    {
        foreach (string id in departmentIDs)
        {
            GetLeafDepartmentsID( leafList, id );
        }
    }

    private void GetLeafContentMenuItemIDs( NameValueCollection leafList, string[] checkedContentMenuItemIDs )
    {
        foreach (string id in checkedContentMenuItemIDs)
        {
            GetLeafOfContentMenuItemID( leafList, id );
        }
    }

    private string[] GetCategoryIDCheckInGrid( DataListItem item )
    {
        CheckBox categoryCheck = (CheckBox) item.FindControl( "uxCategoryCheck" );
        Components_CategoryCheckList categoryCheckList =
                (Components_CategoryCheckList) item.FindControl( "uxCategoryCheckList" );
        HiddenField id = (HiddenField) item.FindControl( "uxCategoryIDHidden" );
        string[] checkedCategoryIDs;
        if (!categoryCheck.Checked)
        {
            checkedCategoryIDs = categoryCheckList.CheckedCategoryID();
        }
        else
        {
            checkedCategoryIDs = new string[1];
            checkedCategoryIDs[0] = id.Value;
        }
        return checkedCategoryIDs;
    }

    private string[] GetDepartmentIDCheckInGrid( DataListItem item )
    {
        CheckBox departmentCheck = (CheckBox) item.FindControl( "uxDepartmentCheck" );
        Components_DepartmentCheckList departmentCheckList =
                (Components_DepartmentCheckList) item.FindControl( "uxDepartmentCheckList" );
        HiddenField id = (HiddenField) item.FindControl( "uxDepartmentIDHidden" );
        string[] checkedDepartmentIDs;
        if (!departmentCheck.Checked)
        {
            checkedDepartmentIDs = departmentCheckList.CheckedDepartmentID();
        }
        else
        {
            checkedDepartmentIDs = new string[1];
            checkedDepartmentIDs[0] = id.Value;
        }
        return checkedDepartmentIDs;
    }

    private string[] GetContentIDCheckInGrid( DataListItem item, string checkBoxName, string checklistName, string hiddenfieldName )
    {
        CheckBox contentMenuItemCheck = (CheckBox) item.FindControl( checkBoxName );
        Components_ContentMenuItemCheckList contentMenuItemCheckList =
                (Components_ContentMenuItemCheckList) item.FindControl( checklistName );
        HiddenField id = (HiddenField) item.FindControl( hiddenfieldName );
        string[] checkedMenuItemContentIDs;
        if (!contentMenuItemCheck.Checked)
        {
            checkedMenuItemContentIDs = contentMenuItemCheckList.CheckedContentMenuItemID();
        }
        else
        {
            checkedMenuItemContentIDs = new string[1];
            checkedMenuItemContentIDs[0] = id.Value;
        }
        return checkedMenuItemContentIDs;
    }

    private void PopulateControls()
    {
        TableCategory = DataAccessContext.CategoryRepository.GetAll( StoreContext.Culture, "CategoryID" );
        TableDepartment = DataAccessContext.DepartmentRepository.GetAll( StoreContext.Culture, "DepartmentID" );
        ListContentMenuItem = DataAccessContext.ContentMenuItemRepository.GetAll(
          StoreContext.Culture, BoolFilter.ShowTrue );
        ResultSearch();
    }

    private void ResultSearch()
    {
        string categoryIDs = "";
        string departmentIDs = "";
        string contentMenuItemIDs = "";
        string manufacturerID = "";

        string keyword = HttpUtility.UrlEncode( uxKeywordText.Text );

        if (uxKeywordText.Text == uxStartKeywordTextHidden.Value)
        {
            keyword = "";
        }

        if (!IsUseEnhanceAdvancedSearch)
        {
            NameValueCollection catleafList = new NameValueCollection();
            NameValueCollection deptleafList = new NameValueCollection();
            NameValueCollection leafContentMenuItemList = new NameValueCollection();

            foreach (DataListItem item in uxCategoryList.Items)
            {
                string[] checkedCategoryIDs = GetCategoryIDCheckInGrid( item );
                GetLeafCategoryIDs( catleafList, checkedCategoryIDs );
            }

            foreach (DataListItem item in uxDepartmentsList.Items)
            {
                string[] checkedDepartmentIDs = GetDepartmentIDCheckInGrid( item );
                GetLeafDepartmentIDs( deptleafList, checkedDepartmentIDs );
            }

            string[] arCatLeafList = new string[catleafList.Count];
            catleafList.CopyTo( arCatLeafList, 0 );
            string[] arDeptLeafList = new string[deptleafList.Count];
            deptleafList.CopyTo( arDeptLeafList, 0 );

            foreach (string leaf in arCatLeafList)
                categoryIDs += leaf + "+";

            foreach (string leaf in arDeptLeafList)
                departmentIDs += leaf + "+";

            foreach (DataListItem item in uxTopContentMenuItemList.Items)
            {
                string[] checkedContentTopMenuItemIDs = GetContentIDCheckInGrid(
                    item, "uxTopContentMenuItemCheck", "uxTopContentMenuItemCheckList", "uxTopContentMenuItemIDHidden" );
                GetLeafContentMenuItemIDs( leafContentMenuItemList, checkedContentTopMenuItemIDs );
            }

            foreach (DataListItem item in uxLeftContentMenuItemList.Items)
            {
                string[] checkedContentLeftMenuItemIDs = GetContentIDCheckInGrid(
                    item, "uxLeftContentMenuItemCheck", "uxLeftContentMenuItemCheckList", "uxLeftContentMenuItemIDHidden" );

                GetLeafContentMenuItemIDs( leafContentMenuItemList, checkedContentLeftMenuItemIDs );
            }

            foreach (DataListItem item in uxRightContentMenuItemList.Items)
            {
                string[] checkedContentRightMenuItemIDs = GetContentIDCheckInGrid(
                   item, "uxRightContentMenuItemCheck", "uxRightContentMenuItemCheckList", "uxRightContentMenuItemIDHidden" );
                GetLeafContentMenuItemIDs( leafContentMenuItemList, checkedContentRightMenuItemIDs );
            }
            string[] arLeafContentMenuItemList = new string[leafContentMenuItemList.Count];
            leafContentMenuItemList.CopyTo( arLeafContentMenuItemList, 0 );

            foreach (string contentMenuItemleaf in arLeafContentMenuItemList)
                contentMenuItemIDs += contentMenuItemleaf + "+";

            Response.Redirect( "AdvancedSearchResult.aspx?" +
            "Type=" + uxSearchDrop.SelectedValue +
            "&CategoryIDs=" + categoryIDs +
            "&Keyword=" + keyword +
            "&Price1=" + uxPrice1Text.Text +
            "&Price2=" + uxPrice2Text.Text +
            "&ContentMenuItemIDs=" + contentMenuItemIDs +
            "&DepartmentIDs=" + departmentIDs +
            "&ManufacturerID=" + manufacturerID +
            "&SearchType=" + CheckedSearchType() +
            "&IsNewSearch=" + IsUseEnhanceAdvancedSearch.ToString() );
        }
        else
        {
            categoryIDs = uxCategoryDrop.SelectedValue;
            if (uxNewSearchPanel2.Visible)
            {
                if (DataAccessContext.Configurations.GetBoolValue( "EnableManufacturer" ))
                {
                    manufacturerID = uxManufacturerDrop.SelectedValue;
                }
                if (DataAccessContext.Configurations.GetBoolValue( "DepartmentListModuleDisplay" ))
                {
                    departmentIDs = uxDepartmentDrop.SelectedValue;
                }
            }

            Response.Redirect( "AdvancedSearch.aspx?" +
            "Type=" + uxSearchDrop.SelectedValue +
            "&CategoryIDs=" + categoryIDs +
            "&Keyword=" + keyword +
            "&Price1=" + uxPrice1Text.Text +
            "&Price2=" + uxPrice2Text.Text +
            "&DepartmentIDs=" + departmentIDs +
            "&ManufacturerID=" + manufacturerID +
            "&SearchType=" + CheckedSearchType() +
            "&IsNewSearch=" + IsUseEnhanceAdvancedSearch.ToString() +
            "&IsSearched=true" );
        }
    }

    private void RegisterSubmitButton()
    {
        WebUtilities.TieButton( this.Page, uxKeywordText, uxSearchImageButton );
    }

    private void AdvancedSearch_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        if (!IsUseEnhanceAdvancedSearch)
        {
            RefreshGrid();
        }
        else
        {
            PopulateCategory();
            PopulateDepartment();
            PopulateManufacturer();
        }
    }

    private void RegisterStoreEvents()
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( AdvancedSearch_StoreCultureChanged );
    }

    private string GetSearchInListDisplayText( string searchField )
    {
        switch (searchField)
        {
            case "ShortDescription":
                return "Short Description";

            case "LongDescription":
                return "Full Description";

            case "ImageSecondary":
                return "Image Secondary";

            case "RelatedProducts":
                return "Related Products";

            case "ManufacturerPartNumber":
                return "Manufacturer Part Number";

            case "MetaKeyword":
                return "Meta Keyword";

            case "MetaDescription":
                return "Meta Description";

            default:
                return searchField;
        }
    }

    private void SetSearchInList()
    {
        uxSearchTypeCheckList.Items.Clear();
        string totalFieldSearch = DataAccessContext.Configurations.GetValueNoThrow( "ProductSearchBy" );
        string[] searchFieldList = SplitColumn( totalFieldSearch );
        foreach (string searchField in searchFieldList)
        {
            if (!String.IsNullOrEmpty( searchField ))
            {
                string displayText = GetSearchInListDisplayText( searchField );
                ListItem item = new ListItem( displayText, searchField );
                uxSearchTypeCheckList.Items.Add( item );
            }
        }

        uxSearchTypeCheckList.DataBind();
    }

    private void SelectAllSearchType()
    {
        foreach (ListItem item in uxSearchTypeCheckList.Items)
        {
            item.Selected = true;
        }
    }

    private string CheckedSearchType()
    {
        string searchType = String.Empty;

        foreach (ListItem item in uxSearchTypeCheckList.Items)
        {
            if (item.Selected)
            {
                if (!String.IsNullOrEmpty( searchType ))
                {
                    searchType += ",";
                }
                searchType += item.Value;
            }
        }
        return searchType;
    }

    private string[] SplitColumn( string str )
    {
        char[] delimiter = new char[] { ',', ':', ';' };
        string[] result = str.Split( delimiter );
        return result;
    }

    private void PopulateCategory()
    {
        uxCategoryDrop.Items.Add( new ListItem( "All Category", "" ) );
        IList<Category> categoryList = DataAccessContext.CategoryRepository.GetAllLeafCategory(
            StoreContext.Culture, "ParentCategoryID" );

        string currentParentID = "";
        string tmpFullPath = "";
        for (int i = 0; i < categoryList.Count; i++)
        {
            if (currentParentID != categoryList[i].ParentCategoryID)
            {
                tmpFullPath = categoryList[i].CreateFullCategoryPathParentOnly();
                currentParentID = categoryList[i].ParentCategoryID;
            }
            uxCategoryDrop.Items.Add(
            new ListItem( tmpFullPath + categoryList[i].Name,
                    categoryList[i].CategoryID ) );
        }
    }

    private void PopulateDepartment()
    {
        uxDepartmentDrop.Items.Add( new ListItem( "-- Select --", "" ) );
        IList<Department> departmentList = DataAccessContext.DepartmentRepository.GetAllLeafDepartment(
            StoreContext.Culture, "ParentDepartmentID" );

        string currentParentID = "";
        string tmpFullPath = "";
        for (int i = 0; i < departmentList.Count; i++)
        {
            if (currentParentID != departmentList[i].ParentDepartmentID)
            {
                tmpFullPath = departmentList[i].CreateFullDepartmentPathParentOnly();
                currentParentID = departmentList[i].ParentDepartmentID;
            }
            uxDepartmentDrop.Items.Add(
                new ListItem( tmpFullPath + departmentList[i].Name, departmentList[i].DepartmentID ) );
        }
    }

    private void PopulateManufacturer()
    {
        uxManufacturerDrop.Items.Add( new ListItem( "-- Select --", "" ) );
        IList<Vevo.Domain.Products.Manufacturer> manufacuterList = DataAccessContext.ManufacturerRepository.GetAll( StoreContext.Culture, BoolFilter.ShowTrue, "Name" );

        for (int i = 0; i < manufacuterList.Count; i++)
        {
            uxManufacturerDrop.Items.Add(
                new ListItem( manufacuterList[i].Name, manufacuterList[i].ManufacturerID ) );
        }
    }

    private void RegisterScript()
    {
        uxKeywordText.Text = _keywordText;
        uxKeywordText.Attributes.Add( "onblur", "javascript:if(this.value=='') this.value='" + _keywordText + "';" );
        uxKeywordText.Attributes.Add( "onfocus", "javascript:if(this.value=='" + _keywordText + "') this.value='';" );
    }

    private void InitSearchView()
    {
        if (IsUseEnhanceAdvancedSearch)
        {
            uxOldAdvancedSearchDiv.Visible = false;
            SetSearchInList();
            PopulateCategory();

            if (!DataAccessContext.Configurations.GetBoolValue( "EnableManufacturer" ) &&
                 !DataAccessContext.Configurations.GetBoolValue( "DepartmentListModuleDisplay" ))
            {
                uxNewSearchPanel2.Visible = false;
            }
            else
            {
                if (DataAccessContext.Configurations.GetBoolValue( "DepartmentListModuleDisplay" ))
                {
                    uxDepartmentDiv.Visible = true;
                    PopulateDepartment();
                }

                if (DataAccessContext.Configurations.GetBoolValue( "EnableManufacturer" ))
                {
                    uxManufacturerDiv.Visible = true;
                    if (!uxDepartmentDiv.Visible)
                    {
                        uxManufactureLabel.Attributes.Add( "class", "AdvancedSearchDepartmentLabel" );
                    }
                    PopulateManufacturer();
                }

            }

            SetDefaultSearchValue();
        }
        else
        {
            uxOldAdvancedSearchDiv.Visible = true;
            uxNewSearchPanel1.Visible = false;
            uxNewSearchPanel2.Visible = false;
            uxResetImageButton.Visible = false;
            RefreshGrid();
        }
    }

    private void SetDefaultSearchValue()
    {
        uxKeywordText.Text = String.Empty;
        uxSearchDrop.SelectedIndex = 0;
        uxCategoryDrop.SelectedIndex = 0;
        uxDepartmentDrop.SelectedIndex = 0;
        uxManufacturerDrop.SelectedIndex = 0;
        SelectAllSearchType();
        uxPrice1Text.Text = String.Empty;
        uxPrice2Text.Text = String.Empty;
        RegisterScript();
        
    }

    private void PopulateProductControl()
    {
        BaseProductListControl productListControl = new BaseProductListControl();
        productListControl = LoadControl( String.Format(
            "{0}{1}",
            SystemConst.LayoutProductListPath,
            DataAccessContext.Configurations.GetValue( "DefaultProductListLayout" ) ) ) as BaseProductListControl;
        string[] productSearchType = DataAccessContext.Configurations.GetValueList( "ProductSearchBy" );
        if (!String.IsNullOrEmpty( ProductSearchType ))
        {
            productSearchType = SplitColumn( ProductSearchType );
        }
        productListControl.ID = "uxProductList";
        productListControl.DataRetriever = new DataAccessCallbacks.ProductListRetriever( GetSearchResult );
        productListControl.IsSearchResult = true;
        productListControl.UserDefinedParameters = new object[] { 
            CategoryIDs, 
            DepartmentIDs,
            ManufacturerID,
            Keyword, 
            Price1, 
            Price2,
            productSearchType,
            SearchType,
            IsNewSearch};
        uxCatalogControlPanel.Controls.Add( productListControl );
    }

    private IList<Product> GetSearchResult(
        Culture culture,
        string sortBy,
        int startIndex,
        int endIndex,
        object[] userDefined,
        out int howManyItems )
    {
        if (!String.IsNullOrEmpty( userDefined[0].ToString() )
            || !String.IsNullOrEmpty( userDefined[1].ToString() )
            || !String.IsNullOrEmpty( userDefined[2].ToString() )
            || !String.IsNullOrEmpty( userDefined[3].ToString() )
            || !String.IsNullOrEmpty( userDefined[4].ToString() )
            || !String.IsNullOrEmpty( userDefined[5].ToString() ))
        {
            if ((bool) userDefined[8])
            {
                return DataAccessContext.ProductRepository.AdvancedSearch(
                        culture,
                        (string) userDefined[0],
                        (string) userDefined[1],
                        (string) userDefined[2],
                        sortBy,
                        (string) userDefined[3],
                        (string) userDefined[4],
                        (string) userDefined[5],
                        (string[]) userDefined[6],
                        startIndex,
                        endIndex,
                        out howManyItems,
                        new StoreRetriever().GetCurrentStoreID(),
                        DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() ),
                        DataAccessContext.Configurations.GetValue( "RootDepartment", new StoreRetriever().GetStore() ),
                        (string) userDefined[7]
                        );
            }
            else
            {
                return DataAccessContext.ProductRepository.AdvancedSearch(
                    culture,
                    (string) userDefined[0],
                    sortBy,
                    (string) userDefined[3],
                    (string) userDefined[4],
                    (string) userDefined[5],
                    (string[]) userDefined[6],
                    startIndex,
                    endIndex,
                    out howManyItems,
                    new StoreRetriever().GetCurrentStoreID(),
                    DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() ),
                    (string) userDefined[7]
                    );
            }
        }
        else
        {
            howManyItems = 0;
            return null;
        }
    }

    #endregion

    #region Protected
    protected void Page_Load( object sender, EventArgs e )
    {
        //check if department is disable
        if (!DataAccessContext.Configurations.GetBoolValue( "DepartmentListModuleDisplay", new StoreRetriever().GetStore() )
            && !DataAccessContext.Configurations.GetBoolValue( "DepartmentHeaderMenuDisplay", new StoreRetriever().GetStore() ))
        {
            uxDepartmentCheckPanel.Visible = false;
        }

        RegisterSubmitButton();
        if (!IsPostBack)
        {
            InitSearchView();
            RegisterScript();
            uxStartKeywordTextHidden.Value = _keywordText;
        }

        RegisterStoreEvents();
        if (IsSearched)
        {
            uxEnhancedSearchResultPanel.Visible = true;
            uxDefaultDeptTitleResult.Text = "[$HeadProduct] for \"" + Keyword + "\"";
            PopulateProductControl();
        }
    }

    protected void HideAdvancedSearchLinkButton_OnClick( object sender, EventArgs e )
    {
        if (IsUseEnhanceAdvancedSearch)
        {
            uxEnhancedSearchPanel.Visible = !uxEnhancedSearchPanel.Visible;
        }
        if (uxEnhancedSearchPanel.Visible)
        {
            uxHideAdvancedSearchLinkButton.Text = _hide;
            uxHideAdvancedSearchLinkButton.CssClass = "HideAdvancedSearchLinkButton";
        }
        else
        {
            uxHideAdvancedSearchLinkButton.Text = _show;
            uxHideAdvancedSearchLinkButton.CssClass = "ShowAdvancedSearchLinkButton";
        }
    }

    protected void uxSearchImageButton_Click( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxResetImageButton_Click( object sender, EventArgs e )
    {
        SetDefaultSearchValue();
    }

    protected void uxCategoryList_ItemDataBound( object sender, DataListItemEventArgs e )
    {
        Components_CategoryCheckList catList = (Components_CategoryCheckList) e.Item.FindControl( "uxCategoryCheckList" );
        if (catList != null)
            catList.refresh();
    }

    protected void uxDepartmentsList_ItemDataBound( object sender, DataListItemEventArgs e )
    {
        Components_DepartmentCheckList deptList = (Components_DepartmentCheckList) e.Item.FindControl( "uxDepartmentCheckList" );
        if (deptList != null)
            deptList.refresh();
    }
    #endregion

}
