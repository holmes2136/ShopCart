using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Discounts;
using Vevo.Shared.Utilities;
using Vevo.WebUI.Ajax;
using Vevo.Base.Domain;

public partial class AdminAdvanced_MainControls_QuantityDiscountList : AdminAdvancedBaseListControl
{
    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.DiscountGroupRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list );
    }

    private string CurrentDiscountType
    {
        get
        {
            if (ViewState["CurrentDiscountType"] == null)
                return "Percentage";
            else
                return ViewState["CurrentDiscountType"].ToString();
        }
        set
        {
            ViewState["CurrentDiscountType"] = value;
        }
    }

    private string CurrentProductOptionDiscount
    {
        get
        {
            if (ViewState["CurrentProductOptionDiscount"] == null)
                return "False";
            else
                return ViewState["CurrentProductOptionDiscount"].ToString();
        }
        set
        {
            ViewState["CurrentProductOptionDiscount"] = value;
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

    private void SetCurrentDiscountType()
    {
        if (uxDiscountGroupGrid.EditIndex != -1)
        {
            Label discount = (Label) (uxDiscountGroupGrid.Rows[uxDiscountGroupGrid.EditIndex].Cells[3].FindControl( "uxDiscountTypeLabel" ));
            CurrentDiscountType = discount.Text;
            Label productOptionDiscount = (Label) (uxDiscountGroupGrid.Rows[uxDiscountGroupGrid.EditIndex].Cells[4].FindControl( "uxProductOptionDiscountLabel" ));
            CurrentProductOptionDiscount = productOptionDiscount.Text;
        }
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }
    }

    private void ApplyPermissions()
    {
        if (!IsAdminModifiable())
        {
            uxAddButton.Visible = false;
            DeleteVisible( false );
        }
        else
        {
            if (uxDiscountGroupGrid.Rows.Count > 0)
                DeleteVisible( true );
            else
                DeleteVisible( false );
        }
    }

    private bool IsContainingOnlyEmptyRow()
    {
        if (uxDiscountGroupGrid.Rows.Count == 1 &&
            ConvertUtilities.ToInt32( uxDiscountGroupGrid.DataKeys[0]["DiscountGroupID"] ) == -1)
            return true;
        else
            return false;
    }

    private void SetFooterRowFocus()
    {
        Control textBox = uxDiscountGroupGrid.FooterRow.FindControl( "uxGroupNameText" );
        AjaxUtilities.GetScriptManager( textBox ).SetFocus( textBox );
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.OptionItemsPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxDiscountGroupGrid, "DiscountGroupID" );

        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }

    private void CreateDummyRow( IList<DiscountGroup> list )
    {
        DiscountGroup discountGroup = new DiscountGroup();
        discountGroup.DiscountGroupID = "-1";
        discountGroup.GroupName = "";
        list.Add( discountGroup );
    }

    private void ClearData( GridViewRow row )
    {
        ((TextBox) row.FindControl( "uxGroupNameText" )).Text = "";
    }

    private DiscountGroup.DiscountTypeEnum GetDiscountType( string discountType )
    {
        return (DiscountGroup.DiscountTypeEnum) Enum.Parse( typeof( DiscountGroup.DiscountTypeEnum ), discountType );
    }

    private DiscountGroup AddUnlimitedDiscountRule( DiscountGroup discountGroup )
    {
        DiscountRule discountRule = new DiscountRule();
        discountRule.ToItems = SystemConst.UnlimitedNumber;
        discountRule.Percentage = 0;
        discountRule.Amount = 0;
        discountGroup.DiscountRules.Add( discountRule );
        return discountGroup;
    }

    private DiscountGroup GetDiscountGroupFromGrid( DiscountGroup discountGroup, GridViewRow row )
    {
        string uxGroupNameText = ((TextBox) row.FindControl( "uxGroupNameText" )).Text;
        string uxDiscountType = ((DropDownList) row.FindControl( "uxDiscountTypeDrop" )).SelectedValue;
        string uxProductOptionDiscount = ((DropDownList) row.FindControl( "uxProductOptionDiscount" )).SelectedValue;

        discountGroup.GroupName = uxGroupNameText;
        discountGroup.DiscountType = GetDiscountType( uxDiscountType );
        discountGroup.ProductOptionDiscount = ConvertUtilities.ToBoolean( uxProductOptionDiscount );

        return discountGroup;
    }

    protected void uxDiscountTypeDrop_PreRender( object sender, EventArgs e )
    {
        DropDownList typePrice = (DropDownList) sender;
        if (CurrentDiscountType == "Percentage")
            typePrice.SelectedValue = "Percentage";
        else
            typePrice.SelectedValue = "Price";
    }

    protected void uxProductOptionDiscountLabel_PreRender( object sender, EventArgs e )
    {
        DropDownList productOption = (DropDownList) sender;
        if (CurrentProductOptionDiscount == "Yes")
            productOption.SelectedValue = "True";
        else
            productOption.SelectedValue = "False";
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpGridSupportControls();

        if (!MainContext.IsPostBack)
        {
            uxDiscountGroupGrid.ShowFooter = false;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
        ApplyPermissions();
        //SetCurrentDiscountType();
    }

    protected void uxEditLinkButton_PreRender( object sender, EventArgs e )
    {
        if (!IsAdminModifiable())
        {
            LinkButton linkButton = (LinkButton) sender;
            linkButton.Visible = false;
        }
    }

    protected void uxDiscountGroupGrid_DataBound( object sender, EventArgs e )
    {
        if (IsContainingOnlyEmptyRow())
        {
            uxDiscountGroupGrid.Rows[0].Visible = false;
        }
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        uxDiscountGroupGrid.EditIndex = -1;
        uxDiscountGroupGrid.ShowFooter = true;
        RefreshGrid();

        uxAddButton.Visible = false;

        SetFooterRowFocus();
    }

    protected void uxDiscountGroupGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        if (e.CommandName == "Add")
        {
            GridViewRow rowAdd = uxDiscountGroupGrid.FooterRow;
            DiscountGroup discountGroup = new DiscountGroup();
            discountGroup = GetDiscountGroupFromGrid( discountGroup, rowAdd );
            if (String.IsNullOrEmpty( discountGroup.GroupName ))
            {
                uxMessage.DisplayMessage( Resources.DiscountGroupMessage.ItemAddErrorEmpty );
                return;
            }
            discountGroup = AddUnlimitedDiscountRule( discountGroup );

            DataAccessContext.DiscountGroupRepository.Save( discountGroup );

            ClearData( rowAdd );

            RefreshGrid();

            uxMessage.DisplayMessage( Resources.DiscountGroupMessage.ItemAddSuccess );

            uxStatusHidden.Value = "Added";
        }
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            int totalDel = 0;
            bool containsProduct = false;
            foreach (GridViewRow row in uxDiscountGroupGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck != null &&
                    deleteCheck.Checked)
                {
                    totalDel += 1;

                    string discountGroupID =
                        uxDiscountGroupGrid.DataKeys[row.RowIndex]["DiscountGroupID"].ToString();

                    if (!DataAccessContext.CategoryRepository.HasCategoryInDiscountGroup( discountGroupID ) &&
                        !DataAccessContext.ProductRepository.HasProductInDiscountGroup( discountGroupID ))
                    {
                        DataAccessContext.DiscountGroupRepository.Delete( discountGroupID );
                    }
                    else
                    {
                        containsProduct = true;
                    }
                }
            }

            uxDiscountGroupGrid.EditIndex = -1;

            if (totalDel != 0)
            {
                if (!containsProduct)
                {
                    uxMessage.DisplayMessage( Resources.DiscountGroupMessage.ItemDeleteSuccess );
                    uxStatusHidden.Value = "Deleted";
                }
                else
                {
                    uxMessage.DisplayError( Resources.DiscountGroupMessage.DeleteProductReferenceError );
                }
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if (uxDiscountGroupGrid.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }
    }

    protected void uxDiscountGroupGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            GridViewRow rowGrid = uxDiscountGroupGrid.Rows[e.RowIndex];

            string discountGroupID = uxDiscountGroupGrid.DataKeys[e.RowIndex]["DiscountGroupID"].ToString();

            DiscountGroup discountGroup = DataAccessContext.DiscountGroupRepository.GetOne( discountGroupID );
            discountGroup = GetDiscountGroupFromGrid( discountGroup, rowGrid );

            if (String.IsNullOrEmpty( discountGroup.GroupName ))
                throw new VevoException( Resources.DiscountGroupMessage.ItemUpdateErrorEmpty );

            DataAccessContext.DiscountGroupRepository.Save( discountGroup );

            uxDiscountGroupGrid.EditIndex = -1;
            RefreshGrid();

            uxMessage.DisplayMessage( Resources.DiscountGroupMessage.ItemUpdateSuccess );

            uxStatusHidden.Value = "Updated";
        }
        finally
        {
            e.Cancel = true;
        }
    }

    protected override void RefreshGrid()
    {
        int totalItems = 0;
        IList<DiscountGroup> discountGroupList = DataAccessContext.DiscountGroupRepository.SearchDiscountGroup(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );

        if (discountGroupList == null || discountGroupList.Count == 0)
        {
            discountGroupList = new List<DiscountGroup>();
            CreateDummyRow( discountGroupList );
        }

        uxDiscountGroupGrid.DataSource = discountGroupList;
        uxDiscountGroupGrid.DataBind();
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
    }

    protected void uxDiscountGroupGrid_RowEditing( object sender, GridViewEditEventArgs e )
    {
        uxDiscountGroupGrid.EditIndex = e.NewEditIndex;
        SetCurrentDiscountType();
        RefreshGrid();
    }

    protected void uxDiscountGroupGrid_CancelingEdit( object sender, GridViewCancelEditEventArgs e )
    {
        uxDiscountGroupGrid.EditIndex = -1;
        RefreshGrid();
    }

}
