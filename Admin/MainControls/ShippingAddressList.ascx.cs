using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Users;
using Vevo.Base.Domain;

public partial class Admin_MainControls_ShippingAddressList : AdminAdvancedBaseUserControl
{
    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "ShippingAddressID" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private void RefreshGrid()
    {
        int totalItems;

        uxGrid.DataSource = DataAccessContext.CustomerRepository.SearchShippingAddress(
            CustomerID,
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );

        uxPagingControl.NumberOfPages = ( int ) Math.Ceiling( ( double ) totalItems / uxPagingControl.ItemsPerPages );
        uxGrid.DataBind();
    }

    private void PopulateControls()
    {
        RefreshGrid();
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.CustomerRepository.GetShippingAddressTableSchemas();

        uxSearchFilter.SetUpSchema( list, "CustomerID" );
    }

    private void DeleteItem( string id )
    {
        Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );

        ShippingAddress shippingAddress = new ShippingAddress();
        int deleteIndex = -1;

        for (int i = 0; i < customer.ShippingAddresses.Count; i++)
        {
            if (customer.ShippingAddresses[i].ShippingAddressID == id)
            {
                deleteIndex = i;
            }
        }

        customer.ShippingAddresses.RemoveAt( deleteIndex );
        customer = DataAccessContext.CustomerRepository.Save( customer );
    }

    protected void uxGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (String.IsNullOrEmpty( CustomerID ))
            MainContext.RedirectMainControl( "CustomerList.ascx" );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchChange );
        uxPagingControl.BubbleEvent += new EventHandler( uxPageChange );

        if (!MainContext.IsPostBack)
            SetUpSearchFilter();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxSearchChange( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    protected void uxPageChange( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "ShippingAddressAdd.ascx", String.Format( "CustomerID={0}", CustomerID ) );
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            ArrayList deleteIDs = new ArrayList();
            foreach (GridViewRow row in uxGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string id = uxGrid.DataKeys[row.RowIndex]["ShippingAddressID"].ToString().Trim();
                    deleteIDs.Add( id );
                }
            }

            Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );

            if (deleteIDs.Count >= customer.ShippingAddresses.Count)
            {
                uxMessage.DisplayError( Resources.CustomerMessages.DeleteShippingAddressError );
            }
            else
            {
                foreach (string id in deleteIDs)
                {
                    DeleteItem( id );
                    deleted = true;
                }

                uxGrid.EditIndex = -1;

                if (deleted)
                {
                    uxMessage.DisplayMessage( Resources.CustomerMessages.DeleteSuccess );
                }
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

    public string CustomerID
    {
        get
        {
            if (string.IsNullOrEmpty( MainContext.QueryString["CustomerID"] ))
                return "0";
            else
                return MainContext.QueryString["CustomerID"];
        }
    }
}
