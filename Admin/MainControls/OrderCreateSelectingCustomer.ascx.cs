using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Stores;
using Vevo.Base.Domain;

public partial class Admin_MainControls_OrderCreateSelectingCustomer : AdminAdvancedBaseListControl
{
    private const int ColumnCustomerID = 1;
    private const int ColumnUserName = 2;
    
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
            uxPagingControl.Visible = true;
        }
        else
        {
            uxPagingControl.Visible = false;
        }
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
        RegisterStoreFilterDrop( uxStoreFilterDrop );
        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        uxStoreFilterDrop.BubbleEvent += new EventHandler( uxStoreFilterDrop_BubbleEvent );
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
        int totalItems;

        uxGridCustomer.DataSource = DataAccessContext.CustomerRepository.SearchCustomer(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            BoolFilter.ShowAll,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGridCustomer.DataBind();
    }

    protected string GetPageQuery( object customerID )
    {
        return String.Format( "CustomerID={0}&StoreID={1}", customerID.ToString(), StoreID );
    }

    protected string StoreID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
            {
                return uxStoreFilterDrop.SelectedValue;
            }
            else
            {
                return Store.RegularStoreID;
            }
        }
    }
}

