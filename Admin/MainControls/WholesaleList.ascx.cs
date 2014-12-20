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
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain;

public partial class AdminAdvanced_MainControls_WholesaleList : AdminAdvancedBaseListControl
{
    private const int ColumnCustomerID = 1;
    private const int ColumnUserName = 2;


    private void HilightDisabledRow()
    {
        foreach (GridViewRow row in uxGridCustomer.Rows)
        {
            CheckBox uxCheck = (CheckBox) row.FindControl( "uxIsEnabledCheck" );
            if (!uxCheck.Checked)
            {
                foreach (TableCell cell in row.Cells)
                {
                    cell.Style.Add( "color", "#ff0000" );
                }
            }
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.CustomerRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list );
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }

        if (uxGridCustomer.Rows.Count > 0)
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

    private void uxGrid_ResetPageHandler( object sender, EventArgs e )
    {
        uxPagingControl.CurrentPage = 1;
        RefreshGrid();
    }

    private void uxGrid_RefreshHandler( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.CustomerItemsPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxGridCustomer, "CustomerID" );

        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

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

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "WholeSaleAdd.ascx" );
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxGridCustomer.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string id = row.Cells[ColumnCustomerID].Text.Trim();

                    DataAccessContext.CustomerRepository.Delete( id );
                    DataAccessContextDeluxe.CustomerSubscriptionRepository.Delete( id );
                    Membership.DeleteUser( row.Cells[ColumnUserName].Text.Trim() );
                    deleted = true;
                }
            }

            if (deleted)
            {
                uxMessage.DisplayMessage( Resources.CustomerMessages.DeleteSuccess );
                uxStatusHidden.Value = "Deleted";
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if (uxGridCustomer.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }
    }

    protected override void RefreshGrid()
    {
        int totalItems;

        uxGridCustomer.DataSource = DataAccessContext.CustomerRepository.SearchCustomer(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            BoolFilter.ShowTrue,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGridCustomer.DataBind();

        HilightDisabledRow();
    }
}
