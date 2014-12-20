using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Base.Domain;

public partial class Admin_MainControls_ProductKitGroupList : AdminAdvancedBaseListControl
{
    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.ProductKitGroupRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list );
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }

        if (uxProductKitGroupGrid.Rows.Count > 0)
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
            uxPagingControl.ItemsPerPages = AdminConfig.ProductKitGroupItemsPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxProductKitGroupGrid, "ProductKitGroupID" );

        RegisterLanguageControl( uxLanguageControl );
        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxProductKitGroupGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string id = row.Cells[1].Text.Trim();
                    DataAccessContext.ProductKitGroupRepository.Delete( id );
                    deleted = true;
                }
            }

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

        if (uxProductKitGroupGrid.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }

    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "ProductKitGroupAdd.ascx", "" );
    }

    protected void uxCancelButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "ProductList.ascx" );
    }

    protected void uxProductKitItemSortingButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "ProductKitItemSorting.ascx" );
    }

    protected override void RefreshGrid()
    {
        int totalItems;

        uxProductKitGroupGrid.DataSource = DataAccessContext.ProductKitGroupRepository.SearchProductKitGroup(
            uxLanguageControl.CurrentCulture,
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems
        );
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
        uxProductKitGroupGrid.DataBind();
    }
}
