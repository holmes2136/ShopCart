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

public partial class Admin_MainControls_DepartmentList : AdminAdvancedBaseListControl
{
    private class DepartmentDisplayItem
    {
        private string _departmentID;
        private string _name;
        private string _description;
        private string _imageFile;
        private string _parentDepartmentID;
        private int _level;
        private bool _isEnabled;


        public string DepartmentID { get { return _departmentID; } set { _departmentID = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public string ImageFile { get { return _imageFile; } set { _imageFile = value; } }
        public string ParentDepartmentID { get { return _parentDepartmentID; } set { _parentDepartmentID = value; } }
        public Int32 Level { get { return _level; } set { _level = value; } }
        public bool IsEnabled { get { return _isEnabled; } set { _isEnabled = value; } }
    }

    private const int DepartmentIDColumn = 1;
    private const int DepartmentIndentInPixels = 20;
    private const int MaxDepartmentDescriptionLength = 100;
    private string _sortPage = "DepartmentSorting.ascx";

    private string RootDepartmentID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
            {
                if (MainContext.QueryString["RootID"] != null)
                    return MainContext.QueryString["RootID"];
                else
                    return DataAccessContext.Configurations.GetValue(
                        "RootDepartment", DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID ) );
            }
            else
            {
                return DataAccessContext.Configurations.GetValue(
                        "RootDepartment", DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID ) );
            }
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.DepartmentRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list, "UrlName" );
    }

    private IList<DepartmentDisplayItem> _departmentSource = new List<DepartmentDisplayItem>();

    private void BuildData( string parentID, int level )
    {
        IList<Department> departmentList = DataAccessContext.DepartmentRepository.GetByParentIDAndRootID( uxLanguageControl.CurrentCulture, parentID, RootDepartmentID, "DepartmentID", BoolFilter.ShowAll );
        string description;

        for (int i = 0; i < departmentList.Count; i++)
        {
            if (departmentList[i].Description.Length > MaxDepartmentDescriptionLength)
                description = departmentList[i].Description.Substring( 0, MaxDepartmentDescriptionLength ) + "...";
            else
                description = departmentList[i].Description;
            BuildRow( departmentList[i],
                description,
                level
                );
            BuildData( departmentList[i].DepartmentID, level + 1 );
        }
    }

    private void BuildRow( Department department, string description, int level )
    {
        DepartmentDisplayItem temp = new DepartmentDisplayItem();
        temp.DepartmentID = department.DepartmentID;
        temp.Name = department.Name;
        temp.Description = description;
        temp.ImageFile = department.ImageFile;
        temp.ParentDepartmentID = department.ParentDepartmentID;
        temp.Level = level;
        temp.IsEnabled = department.IsEnabled;

        _departmentSource.Add( temp );
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
                string id = ((HiddenField) row.Cells[0].FindControl( "uxDepartmentIDHidden" )).Value.Trim();
                items.Add( id );
            }
        }

        string[] result = new string[items.Count];
        items.CopyTo( result );
        return result;
    }

    private bool ContainsProducts( string[] idArray, out string containingDepartmentID )
    {
        foreach (string id in idArray)
        {
            IList<Product> productList = DataAccessContext.ProductRepository.GetByDepartmentID(
                uxLanguageControl.CurrentCulture,
                id, null, BoolFilter.ShowAll, new StoreRetriever().GetCurrentStoreID() );

            if (productList.Count > 0)
            {
                containingDepartmentID = id;
                return true;
            }
        }

        containingDepartmentID = "";
        return false;
    }

    private bool ContainsCategories( string[] idArray, out string containingDepartmentID )
    {
        foreach (string id in idArray)
        {
            Department department = DataAccessContext.DepartmentRepository.GetOne( uxLanguageControl.CurrentCulture, id );
            if (!department.IsLeafDepartment())
            {
                containingDepartmentID = department.DepartmentID;
                return true;
            }
        }

        containingDepartmentID = "";
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
                    string id = ((HiddenField) row.Cells[0].FindControl( "uxDepartmentIDHidden" )).Value.Trim();
                    string parentId = ((HiddenField) row.Cells[0].FindControl( "uxDepartmentParentIDHidden" )).Value.Trim();
                    DataAccessContext.DepartmentRepository.Delete( id );

                    IList<Department> departmentList = DataAccessContext.DepartmentRepository.GetByParentID( uxLanguageControl.CurrentCulture,
                        parentId, "SortOrder", BoolFilter.ShowTrue );

                    string[] result = new string[departmentList.Count];
                    for (int i = 0; i < departmentList.Count; i++)
                    {
                        result[i] = departmentList[i].DepartmentID;
                    }
                    DataAccessContext.DepartmentRepository.UpdateSortOrder( result );
                    HttpContext.Current.Session[SystemConst.DepartmentTreeViewLeftKey] = null;
                    deleted = true;
                }
            }
            if (deleted)
            {
                uxMessage.DisplayMessage( Resources.DepartmentMessages.DeleteSuccess );
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
        RegisterGridView( uxGrid, "DepartmentID" );

        RegisterLanguageControl( uxLanguageControl );
        RegisterSearchFilter( uxSearchFilter );

        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpGridSupportControls();

        if (!KeyUtilities.IsMultistoreLicense())
        {
            IList<Department> rootDept = DataAccessContext.DepartmentRepository.GetRootDepartment( uxLanguageControl.CurrentCulture, "DepartmentID", BoolFilter.ShowAll );
            if (rootDept.Count == 0)
            {
                Department dept = new Department( uxLanguageControl.CurrentCulture );
                dept.Name = "RootDepartment";
                DataAccessContext.DepartmentRepository.Save( dept );
            }
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();

        if (KeyUtilities.IsMultistoreLicense())
            uxBreadcrumbLabel.Text = DataAccessContext.DepartmentRepository.GetOne( uxLanguageControl.CurrentCulture, RootDepartmentID ).Name
                + " (" + RootDepartmentID + ")";
    }

    protected string PageQueryString( string departmentID )
    {
        return String.Format( "RootID={0}&DepartmentID={1}", RootDepartmentID, departmentID );
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "DepartmentAdd.ascx", String.Format( "RootID={0}", RootDepartmentID ) );
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
            return "float: left; width: " + (DepartmentIndentInPixels * ConvertUtilities.ToInt32( level )) + "px;";
        else
            return "float: left;";
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        string[] checkedIDs = GetCheckedIDs();

        string containingID;
        if (ContainsProducts( checkedIDs, out containingID ))
        {
            Department department = DataAccessContext.DepartmentRepository.GetOne( uxLanguageControl.CurrentCulture, containingID );
            uxMessage.DisplayError(
                Resources.DepartmentMessages.DeleteErrorContainingProducts,
                department.Name, containingID );
        }
        else if (ContainsCategories( checkedIDs, out containingID ))
        {
            Department department = DataAccessContext.DepartmentRepository.GetOne( uxLanguageControl.CurrentCulture, containingID );
            uxMessage.DisplayError(
                Resources.DepartmentMessages.DeleteErrorContainingCategories,
                department.Name, containingID );
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
            //_allCategories = DataAccessContext.DepartmentRepository.GetAll( uxLanguageControl.CurrentCulture, "SortOrder" );
            BuildData( RootDepartmentID, 0 );
        }
        else
        {
            IList<Department> departmentList = DataAccessContext.DepartmentRepository.SearchDepartmentByRootID(
                uxLanguageControl.CurrentCulture,
                GridHelper.GetFullSortText(),
                uxSearchFilter.SearchFilterObj,
                RootDepartmentID );

            foreach (Department department in departmentList)
            {
                string description = department.Description;

                BuildRow( department, description, 0 );
            }

        }

        uxGrid.DataSource = _departmentSource;
        uxGrid.DataBind();
    }
}
