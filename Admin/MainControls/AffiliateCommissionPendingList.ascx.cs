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
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Marketing;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.DataInterfaces;
using Vevo.Deluxe.Domain.Marketing;
using Vevo.Shared.DataAccess;

public partial class AdminAdvanced_MainControls_AffiliateCommissionPendingList : AdminAdvancedBaseUserControl
{
    private const int AffiliateOrderIDIndex = 1;

    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "AffiliateOrderID" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContextDeluxe.AffiliateOrderRepository.GetTableSchema();
        list.Add( new TableSchemaItem( "Username", typeof( string ) ) );

        uxSearchFilter.SetUpSchema( list );
    }

    private void RefreshGrid()
    {
        int totalItems;
        IList<AffiliateOrder> affiliateOrderList = DataAccessContextDeluxe.AffiliateOrderRepository.SearchPendingList(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            (uxPagingControl.CurrentPage - 1) * uxPagingControl.ItemsPerPages,
            (uxPagingControl.CurrentPage * uxPagingControl.ItemsPerPages) - 1,
            out totalItems );

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
        uxGrid.DataSource = affiliateOrderList;
        uxGrid.DataBind();
    }

    private void ApplyPermissions()
    {
        if (!IsAdminModifiable())
        {
            uxProcessedButton.Visible = false;
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
            uxPagingControl.Visible = true;
            uxProcessedButton.Visible = true;
        }
        else
        {
            uxPagingControl.Visible = false;
            uxProcessedButton.Visible = false;
        }
    }

    private void uxGrid_RefreshPageHandler( object sender, EventArgs e )
    {
        uxPagingControl.CurrentPage = 1;
        RefreshGrid();
    }

    private void uxGrid_RefreshHandler( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
            MainContext.RedirectMainControl( "Default.ascx", String.Empty );

        uxSearchFilter.BubbleEvent += new EventHandler( uxGrid_RefreshPageHandler );
        uxPagingControl.BubbleEvent += new EventHandler( uxGrid_RefreshHandler );

        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.AffiliateCommissionPendingPerPage;
            SetUpSearchFilter();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
        ApplyPermissions();
    }

    protected void uxGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
        RefreshGrid();
    }

    protected void uxProcessedButton_Click( object sender, EventArgs e )
    {
        foreach (GridViewRow row in uxGrid.Rows)
        {
            CheckBox check = (CheckBox) row.FindControl( "uxCheck" );
            if (check.Checked)
            {
                string id = ((Label) row.Cells[AffiliateOrderIDIndex].FindControl( "uxAffiliateOrderIDLabel" )).Text.Trim();
                AffiliateOrder affiliateOrder = DataAccessContextDeluxe.AffiliateOrderRepository.GetOne( id );
                affiliateOrder.Pending = false;
                DataAccessContextDeluxe.AffiliateOrderRepository.Save( affiliateOrder );
            }
        }

        RefreshGrid();
    }
}
