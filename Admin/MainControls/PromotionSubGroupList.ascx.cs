using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Marketing;
using Vevo.Shared.Utilities;
using Vevo.WebUI.Ajax;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.Domain;
using Vevo.Shared.DataAccess;

public partial class Admin_MainControls_PromotionSubGroupList : AdminAdvancedBaseListControl
{
    private const int ColumnPromotionSubGroupID = 1;
    private const int ColumnPromotionSubGroupName = 2;

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContextDeluxe.PromotionSubGroupRepository.GetTableSchemas();

        uxSearchFilter.SetUpSchema( list );
    }

    private void PopulateControl()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }

        if (uxPromotionSubGroupGrid.Rows.Count > 0)
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
            uxPagingControl.ItemsPerPages = AdminConfig.PromotionSubGroupItemsPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxPromotionSubGroupGrid, "PromotionSubGroupID" );

        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }

    private bool IsContainingOnlyEmptyRow()
    {
        if (uxPromotionSubGroupGrid.Rows.Count == 1 &&
            ConvertUtilities.ToInt32( uxPromotionSubGroupGrid.DataKeys[0]["PromotionSubGroupID"] ) == -1)
            return true;
        else
            return false;
    }

    private PromotionSubGroup GetPromotionSubGroupFromGrid( PromotionSubGroup promotionSubGroup, GridViewRow row )
    {
        string uxNameText = ((TextBox) row.FindControl( "uxNameText" )).Text;

        promotionSubGroup.Name = uxNameText;

        return promotionSubGroup;
    }

    private void ClearData( GridViewRow row )
    {
        ((TextBox) row.FindControl( "uxNameText" )).Text = "";
    }

    private void SetFooterRowFocus()
    {
        Control textBox = uxPromotionSubGroupGrid.FooterRow.FindControl( "uxNameText" );
        AjaxUtilities.GetScriptManager( textBox ).SetFocus( textBox );
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

    protected void uxEditLinkButton_PreRender( object sender, EventArgs e )
    {
        if (!IsAdminModifiable())
        {
            LinkButton linkButton = (LinkButton) sender;
            linkButton.Visible = false;
        }
    }

    protected void uxPromotionSubGroupGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        if (e.CommandName == "Add")
        {
            GridViewRow rowAdd = uxPromotionSubGroupGrid.FooterRow;
            //DiscountGroup discountGroup = new DiscountGroup();
            PromotionSubGroup promotionSubGroup = new PromotionSubGroup();

            promotionSubGroup = GetPromotionSubGroupFromGrid( promotionSubGroup, rowAdd );
            if (String.IsNullOrEmpty( promotionSubGroup.Name ))
            {
                uxMessage.DisplayError( Resources.PromotionSubGroupMessage.ItemAddErrorEmpty );
                return;
            }

            DataAccessContextDeluxe.PromotionSubGroupRepository.Save( promotionSubGroup );

            ClearData( rowAdd );

            RefreshGrid();

            uxMessage.DisplayMessage( Resources.PromotionSubGroupMessage.ItemAddSuccess );

            uxStatusHidden.Value = "Added";
        }
    }

    protected void uxPromotionSubGroupGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            GridViewRow rowGrid = uxPromotionSubGroupGrid.Rows[e.RowIndex];

            string promotionSubGroupID = uxPromotionSubGroupGrid.DataKeys[e.RowIndex]["PromotionSubGroupID"].ToString();

            PromotionSubGroup promotionSubGroup = DataAccessContextDeluxe.PromotionSubGroupRepository.GetOne( promotionSubGroupID );
            promotionSubGroup = GetPromotionSubGroupFromGrid( promotionSubGroup, rowGrid );

            if (String.IsNullOrEmpty( promotionSubGroup.Name ))
            {
                uxMessage.DisplayError( Resources.PromotionSubGroupMessage.ItemUpdateErrorEmpty );
                return;
            }

            DataAccessContextDeluxe.PromotionSubGroupRepository.Save( promotionSubGroup );

            uxPromotionSubGroupGrid.EditIndex = -1;
            RefreshGrid();

            uxMessage.DisplayMessage( Resources.PromotionSubGroupMessage.ItemUpdateSuccess );

            uxStatusHidden.Value = "Updated";
        }
        finally
        {
            e.Cancel = true;
        }
    }

    protected void uxPromotionSubGroupGrid_DataBound( object sender, EventArgs e )
    {
        if (IsContainingOnlyEmptyRow())
        {
            uxPromotionSubGroupGrid.Rows[0].Visible = false;
        }
    }

    protected void uxPromotionSubGroupGrid_RowEditing( object sender, GridViewEditEventArgs e )
    {
        uxPromotionSubGroupGrid.EditIndex = e.NewEditIndex;
        RefreshGrid();
    }

    protected void uxPromotionSubGroupGrid_CancelingEdit( object sender, GridViewCancelEditEventArgs e )
    {
        uxPromotionSubGroupGrid.EditIndex = -1;
        RefreshGrid();
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        uxPromotionSubGroupGrid.EditIndex = -1;
        uxPromotionSubGroupGrid.ShowFooter = true;
        RefreshGrid();
        uxAddButton.Visible = false;
        SetFooterRowFocus();

    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxPromotionSubGroupGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck != null && deleteCheck.Checked)
                {
                    string id = uxPromotionSubGroupGrid.DataKeys[row.RowIndex]["PromotionSubGroupID"].ToString();

                    DataAccessContextDeluxe.PromotionSubGroupRepository.Delete( id );
                    deleted = true;
                }
            }

            uxPromotionSubGroupGrid.EditIndex = -1;

            if (deleted)
            {
                uxMessage.DisplayMessage( Resources.PromotionSubGroupMessage.DeleteSuccess );
                uxStatusHidden.Value = "Deleted";
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if (uxPromotionSubGroupGrid.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }
    }

    private void CreateDummyRow( IList<PromotionSubGroup> list )
    {
        PromotionSubGroup promotionSubGroup = new PromotionSubGroup();
        promotionSubGroup.PromotionSubGroupID = "-1";
        promotionSubGroup.Name = "";
        list.Add( promotionSubGroup );
    }

    protected override void RefreshGrid()
    {
        int totalItems;

        IList<PromotionSubGroup> promotionSubGroupList = DataAccessContextDeluxe.PromotionSubGroupRepository.SearchPromotionSubGroup(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );

        if (promotionSubGroupList == null || promotionSubGroupList.Count == 0)
        {
            promotionSubGroupList = new List<PromotionSubGroup>();
            CreateDummyRow( promotionSubGroupList );
        }
        uxPromotionSubGroupGrid.DataSource = promotionSubGroupList;
        uxPromotionSubGroupGrid.DataBind();
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
    }

    protected void uxPromotionProductSorting_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "PromotionProductSorting.ascx" );
    }
}
