using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Base.Domain;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Blogs;

public partial class Admin_MainControls_BlogCategoryList : AdminAdvancedBaseListControl
{
    #region Private

    private bool _emptyRow = false;
    private string _sortPage = "BlogCategorySorting.ascx";

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.BlogCategoryRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list, "ImageFile" );
    }

    private void CreateDummyRow( IList<BlogCategory> list )
    {
        BlogCategory blogCategory = new BlogCategory( uxLanguageControl.CurrentCulture );
        blogCategory.BlogCategoryID = "-1";
        list.Add( blogCategory );
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

    private bool IsContainingOnlyEmptyRow
    {
        get { return _emptyRow; }
        set { _emptyRow = value; }
    }

    private void RefreshDeleteButton()
    {
        if (IsContainingOnlyEmptyRow)
            uxDeleteButton.Visible = false;
        else
            uxDeleteButton.Visible = true;
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

    private string[] GetCheckedIDs()
    {
        ArrayList items = new ArrayList();

        foreach (GridViewRow row in uxGrid.Rows)
        {
            CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
            if (deleteCheck.Checked)
            {
                string id = uxGrid.DataKeys[row.RowIndex]["BlogCategoryID"].ToString().Trim();
                items.Add( id );
            }
        }

        string[] result = new string[items.Count];
        items.CopyTo( result );
        return result;
    }

    private void DeleteItems( string[] checkedIDs )
    {
        try
        {
            bool deleted = false;
            foreach (string id in checkedIDs)
            {
                DataAccessContext.BlogCategoryRepository.Delete( id );
                deleted = true;
            }
            uxGrid.EditIndex = -1;

            if (deleted)
                uxMessage.DisplayMessage( Resources.BlogMessage.DeleteSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
            SetUpSearchFilter();

        RegisterGridView( uxGrid, "BlogCategoryID" );
        RegisterLanguageControl( uxLanguageControl );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
    }

    #endregion

    #region Protected

    protected void uxGrid_DataBound( object sender, EventArgs e )
    {
        if (IsContainingOnlyEmptyRow)
        {
            uxGrid.Rows[0].Visible = false;
        }
    }

    protected override void RefreshGrid()
    {
        int totalItems;
        IList<BlogCategory> list = DataAccessContext.BlogCategoryRepository.SearchBlogCategory(
            uxLanguageControl.CurrentCulture,
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            BoolFilter.ShowAll,
            out totalItems );

        uxGrid.DataSource = list;
        uxGrid.DataBind();

        RefreshDeleteButton();
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        string[] checkedIDs = GetCheckedIDs();
        DeleteItems( checkedIDs );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "BlogCategoryAdd.ascx" );
    }

    protected void uxSortButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( _sortPage );
    }

    #endregion

}