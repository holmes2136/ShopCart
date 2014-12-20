using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Shared.Utilities;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain;
using Vevo.Shared.DataAccess;

public partial class Admin_MainControls_PromotionGroupList : AdminAdvancedBaseListControl
{
    private const int ColumnPromotionID = 1;
    private const int ColumnPromotionName = 2;

    private void SetUpSearchFilter()
    {        
        IList<TableSchemaItem> list = DataAccessContextDeluxe.PromotionGroupRepository.GetPromotionGroupTableSchema();

        uxSearchFilter.SetUpSchema( list );
    }

    private void PopulateControl()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }

        if (uxPromotionGroupGrid.Rows.Count > 0)
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
            uxPagingControl.ItemsPerPages = AdminConfig.PromotionGroupItemsPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxPromotionGroupGrid, "PromotionGroupID" );

        RegisterLanguageControl( uxLanguageControl );
        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
            MainContext.RedirectMainControl( "Default.ascx", String.Empty );

        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControl();
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "PromotionGroupAdd.ascx" );
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxPromotionGroupGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string id = row.Cells[ColumnPromotionID].Text.Trim();

                    DataAccessContextDeluxe.PromotionGroupRepository.Delete( id );                    
                    deleted = true;
                }
            }

            if (deleted)
            {
                uxMessage.DisplayMessage( Resources.PromotionGroupMessage.DeleteSuccess );
                uxStatusHidden.Value = "Deleted";
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if (uxPromotionGroupGrid.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }
    }

    protected override void RefreshGrid()
    {
        int totalItems;

        uxPromotionGroupGrid.DataSource = DataAccessContextDeluxe.PromotionGroupRepository.SearchPromotionGroup(
            uxLanguageControl.CurrentCulture,
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );

        uxPromotionGroupGrid.DataBind();
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
    }

    protected void uxPromotionSubGroupList_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "PromotionSubGroupList.ascx" );
    }

    protected void uxPromotionSubGroupSorting_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "PromotionSubGroupSorting.ascx" );
    }

    protected decimal GetPrice( object price )
    {
        return ConvertUtilities.ToDecimal( price );
    }
}
