using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Products;
using Vevo.WebUI.Ajax;
using Vevo.Base.Domain;


public partial class Admin_MainControls_SpecificationItemList : AdminAdvancedBaseListControl
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
        IList<TableSchemaItem> list = DataAccessContext.SpecificationItemRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list );
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
            uxPagingControl.Visible = true;
        }
        else
        {
            DeleteVisible( false );
            uxPagingControl.Visible = false;
        }

        if (!IsAdminModifiable())
        {
            uxAddButton.Visible = false;
            DeleteVisible( false );
        }
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

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.StoreItemPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxGrid, "SpecificationItemID" );

        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );
        RegisterLanguageControl( uxLanguageControl );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
    }

    private void RefreshDeleteButton()
    {
        if (IsContainingOnlyEmptyRow)
            uxDeleteButton.Visible = false;
        else
            uxDeleteButton.Visible = true;
    }

    private void CreateDummyRow( IList<SpecificationItem> list )
    {
        SpecificationItem specificationItem = new SpecificationItem( uxLanguageControl.CurrentCulture );
        specificationItem.SpecificationItemID = "-1";
        specificationItem.Name = string.Empty;

        list.Add( specificationItem );
    }

    private void SetFooterRowFocus()
    {
        Control textBox = uxGrid.FooterRow.FindControl( "uxNameText" );
        AjaxUtilities.GetScriptManager( this ).SetFocus( textBox );
    }

    #endregion

    #region Protected

    protected string PageQueryString( string specItemID )
    {
        return String.Format( "SpecificationItemID={0}&SpecificationGroupID={1}", Eval( "SpecificationItemID" ), SpecificationGroupID );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected override void RefreshGrid()
    {
        int totalItems = 0;
        IList<SpecificationItem> list = DataAccessContext.SpecificationItemRepository.SearchSpecificationItem(
            uxLanguageControl.CurrentCulture,
            SpecificationGroupID,
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );

        if (list == null || list.Count == 0)
        {
            IsContainingOnlyEmptyRow = true;
            list = new List<SpecificationItem>();
            CreateDummyRow( list );
        }

        uxGrid.DataSource = list;
        uxGrid.DataBind();

        RefreshDeleteButton();

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string id = uxGrid.DataKeys[row.RowIndex]["SpecificationItemID"].ToString().Trim();
                    DataAccessContext.SpecificationItemRepository.Delete( id );
                    deleted = true;
                }
            }
            uxGrid.EditIndex = -1;
            if (deleted)
            {
                uxMessage.DisplayMessage( Resources.ProductSpecificationMessages.DeleteSuccess );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if (uxGrid.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }
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

    private SpecificationItem SetupSpecificationItemDetails()
    {
        SpecificationItem item = new SpecificationItem( uxLanguageControl.CurrentCulture );
        item.Name = ((TextBox) uxGrid.FooterRow.FindControl( "uxNameText" )).Text.Trim();
        item.DisplayName = ((TextBox) uxGrid.FooterRow.FindControl( "uxDisplayNameText" )).Text.Trim();
        item.Description = ((TextBox) uxGrid.FooterRow.FindControl( "uxDescriptionText" )).Text.Trim();
        item.Type = (SpecificationItemControlType) Enum.Parse( typeof( SpecificationItemControlType ), ((DropDownList) uxGrid.FooterRow.FindControl( "uxTypeDrop" )).SelectedValue );
        item.UseInFacetedSearch = ((CheckBox) uxGrid.FooterRow.FindControl( "uxUseInFacetedSearchCheck" )).Checked;
        item.SpecificationGroupID = SpecificationGroupID;
        return item;
    }

    private bool IsSpecificationItemNameExist( string name )
    {
        return DataAccessContext.SpecificationItemRepository.IsSpecificationItemNameExist(name, SpecificationGroupID);
    }

    protected void uxGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        try
        {
            if (e.CommandName == "Add")
            {
                SpecificationItem item = SetupSpecificationItemDetails();
                if (IsSpecificationItemNameExist(item.Name))
                {
                    uxMessage.DisplayError( Resources.ProductSpecificationMessages.DuplicateNameError );
                    return;
                }

                DataAccessContext.SpecificationItemRepository.Save( item );
                uxMessage.DisplayMessage( Resources.ProductSpecificationMessages.ItemAddSuccess );
                RefreshGrid();
            }
            else if (e.CommandName == "Edit")
            {
                uxGrid.ShowFooter = false;
                uxAddButton.Visible = true;
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }


    }

    protected void uxGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            string id = ((Label) uxGrid.Rows[e.RowIndex].FindControl( "uxSpecificationItemIDLabel" )).Text.Trim();
            string name = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxNameText" )).Text.Trim();
            string displayName = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxDisplayNameText" )).Text.Trim();
            string description = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxDescriptionText" )).Text.Trim();
            SpecificationItemControlType controlType = (SpecificationItemControlType) Enum.Parse( typeof( SpecificationItemControlType ), ((Label) uxGrid.Rows[e.RowIndex].FindControl( "uxTypeText" )).Text );
            bool useInFacetedSearch = ((CheckBox) uxGrid.Rows[e.RowIndex].FindControl( "uxUseInFacetedSearchCheck" )).Checked;

            if (!String.IsNullOrEmpty( id ))
            {
                SpecificationItem item = DataAccessContext.SpecificationItemRepository.GetOne( uxLanguageControl.CurrentCulture, id );
                if (!item.Name.Equals( name ) && IsSpecificationItemNameExist( name ))
                {
                    uxMessage.DisplayError( Resources.ProductSpecificationMessages.DuplicateNameError );
                    return;
                }
                item.Name = name;
                item.DisplayName = displayName;
                item.Description = description;
                item.Type = controlType;
                item.UseInFacetedSearch = useInFacetedSearch;

                DataAccessContext.SpecificationItemRepository.Save( item );
                uxGrid.EditIndex = -1;
                uxMessage.DisplayMessage( Resources.ProductSpecificationMessages.ItemUpdateSuccess );

                RefreshGrid();               
            }   
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;
        }
    }

    protected void uxGrid_DataBound( object sender, EventArgs e )
    {
        if (IsContainingOnlyEmptyRow)
        {
            uxGrid.Rows[0].Visible = false;
        }
    }

    protected bool IsTypeTextbox( object specificationItemID )
    {
        SpecificationItem item = DataAccessContext.SpecificationItemRepository.GetOne( uxLanguageControl.CurrentCulture, specificationItemID.ToString() );
        if (item.Type.ToString().Equals( "Textbox" ))
            return true;
        else
            return false;
    }

    #endregion

    #region Public
    public string SpecificationGroupID
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["SpecificationGroupID"] ))
                return MainContext.QueryString["SpecificationGroupID"];
            else
                return "0";
        }
    }
    #endregion
}