using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Base.Domain;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebAppLib;
using Vevo.WebUI.Ajax;

public partial class AdminAdvanced_MainControls_RootCategoryList : AdminAdvancedBaseListControl
{
    #region Private

    private bool _emptyRow = false;

    private bool IsContainingOnlyEmptyRow
    {
        get { return _emptyRow; }
        set { _emptyRow = value; }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.CategoryRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list, "CategoryID", "Description", "ImageFile",
            "ParentCategoryID", "UrlName", "IsEnabled", "Other1", "Other2", "Other3", "Other4", "Other5",
            "MetaKeyword", "MetaDescription" );
    }

    private void SetFooterRowFocus()
    {
        Control textBox = uxGrid.FooterRow.FindControl( "uxNameText" );
        AjaxUtilities.GetScriptManager( this ).SetFocus( textBox );
    }

    private void CreateDummyRow( IList<Category> list )
    {
        Category category = new Category( uxLanguageControl.CurrentCulture );
        category.CategoryID = "-1";

        list.Add( category );
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
    }

    private void RefreshDeleteButton()
    {
        if (IsContainingOnlyEmptyRow)
            uxDeleteButton.Visible = false;
        else
            uxDeleteButton.Visible = true;
    }

    private string[] GetCheckedIDs()
    {
        ArrayList items = new ArrayList();

        foreach (GridViewRow row in uxGrid.Rows)
        {
            CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
            if (deleteCheck.Checked)
            {
                string id = uxGrid.DataKeys[row.RowIndex]["CategoryID"].ToString().Trim();
                items.Add( id );
            }
        }

        string[] result = new string[items.Count];
        items.CopyTo( result );
        return result;
    }

    private bool ContainsCategory( string[] idArray, out string containingCategoryID )
    {
        foreach (string id in idArray)
        {
            IList<Category> categoryList = DataAccessContext.CategoryRepository.GetByRootID( 
                Culture.Null, id, "CategoryID", BoolFilter.ShowAll );
            if (categoryList.Count > 0)
            {
                containingCategoryID = id;
                return true;
            }
        }

        containingCategoryID = "";
        return false;
    }

    private void DeleteItems( string[] checkedIDs )
    {
        try
        {
            bool deleted = false;
            foreach (string id in checkedIDs)
            {
                DataAccessContext.CategoryRepository.Delete( id );
                deleted = true;
            }
            uxGrid.EditIndex = -1;

            if (deleted)
                uxMessage.DisplayMessage( Resources.CategoryMessages.DeleteRootSuccess );
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

        RegisterGridView( uxGrid, "CategoryID" );
        RegisterSearchFilter( uxSearchFilter );
        RegisterLanguageControl( uxLanguageControl );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
    }

    #endregion


    #region Protected

    protected override void RefreshGrid()
    {
        IList<Category> list = DataAccessContext.CategoryRepository.SearchRootCategory(
            uxLanguageControl.CurrentCulture,
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj );


        if (list == null || list.Count == 0)
        {
            IsContainingOnlyEmptyRow = true;
            list = new List<Category>();
            CreateDummyRow( list );
        }
        else
        {
            IsContainingOnlyEmptyRow = false;
        }

        uxGrid.DataSource = list;
        uxGrid.DataBind();

        RefreshDeleteButton();

        if (uxGrid.ShowFooter)
        {
            Control nameText = uxGrid.FooterRow.FindControl( "uxNameText" );
            Control addButton = uxGrid.FooterRow.FindControl( "uxAddlinkButton" );

            WebUtilities.TieButton( this.Page, nameText, addButton );
        }
    }

    protected void uxGrid_DataBound( object sender, EventArgs e )
    {
        if (IsContainingOnlyEmptyRow)
        {
            uxGrid.Rows[0].Visible = false;
        }
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        string[] checkedIDs = GetCheckedIDs();

        string containingID;

        IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "StoreID" );

        foreach (Store store in storeList)
        {
            foreach (string id in checkedIDs)
            {
                Category category = DataAccessContext.CategoryRepository.GetOne( uxLanguageControl.CurrentCulture, id );
                if (DataAccessContext.Configurations.GetValue( "RootCategory", store ) == id)
                {
                    uxMessage.DisplayError( Resources.CategoryMessages.DeleteRootErrorSelectedStore, category.Name, id );
                    return;
                }
            }
        }


        if (ContainsCategory( checkedIDs, out containingID ))
        {
            Category category = DataAccessContext.CategoryRepository.GetOne( uxLanguageControl.CurrentCulture, containingID );
            uxMessage.DisplayError(
                Resources.CategoryMessages.DeleteRootErrorContainingCategories,
                category.Name, containingID );
        }
        else
        {
            DeleteItems( checkedIDs );
        }
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
        uxGrid.EditIndex = -1;
        uxGrid.ShowFooter = true;
        uxGrid.ShowHeader = true;
        RefreshGrid();

        uxAddButton.Visible = false;

        SetFooterRowFocus();
    }

    protected void uxGrid_RowEditing( object sender, GridViewEditEventArgs e )
    {
        uxGrid.EditIndex = e.NewEditIndex;
        RefreshGrid();
    }

    protected void uxGrid_CancelingEdit( object sender, GridViewCancelEditEventArgs e )
    {
        uxGrid.EditIndex = -1;
        RefreshGrid();
    }

    protected void uxGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        if (e.CommandName == "Add")
        {
            try
            {
                string name = ((TextBox) uxGrid.FooterRow.FindControl( "uxNameText" )).Text;

                Category category = new Category( uxLanguageControl.CurrentCulture );
                category.Name = name;
                DataAccessContext.CategoryRepository.Save( category );

                ((TextBox) uxGrid.FooterRow.FindControl( "uxNameText" )).Text = "";

                uxMessage.DisplayMessage( Resources.CategoryMessages.AddRootSuccess );
            }
            catch (Exception)
            {
                string message = Resources.CategoryMessages.AddError;
                throw new VevoException( message );
            }
            finally
            {
            }
            RefreshGrid();
        }

        if (e.CommandName == "Edit")
        {
            uxGrid.ShowFooter = false;
            uxAddButton.Visible = true;
        }
    }

    protected void uxGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            string categoryID = ((HiddenField) uxGrid.Rows[e.RowIndex].FindControl( "uxCategoryIDHidden" )).Value;
            string name = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxNameText" )).Text;

            if (!String.IsNullOrEmpty( categoryID ))
            {
                Category category = new Category( uxLanguageControl.CurrentCulture );
                category.CategoryID = categoryID;
                category.Name = name;
                DataAccessContext.CategoryRepository.Save( category );
            }

            //End editing
            uxGrid.EditIndex = -1;
            RefreshGrid();

            uxMessage.DisplayMessage( Resources.CategoryMessages.UpdateRootSuccess );
        }
        catch (Exception)
        {
            string message = Resources.CategoryMessages.UpdateError;
            throw new ApplicationException( message );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;
        }
    }

    #endregion
}
