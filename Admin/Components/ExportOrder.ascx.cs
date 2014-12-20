using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain.Orders;
using System.Collections.Generic;
using Vevo.Domain.Users;
using System.IO;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain;
using Vevo.Shared.DataAccess;
using Vevo.Shared.DataAccess.Tester;
using Vevo.Domain.Stores;
using Vevo.WebAppLib;
using Vevo.Base.Domain;

public partial class AdminAdvanced_Components_ExportOrder : AdminAdvancedBaseUserControl
{
    #region Private

    private void CreateOrderItemExportFile( IList<Order> orderList )
    {
        OrderItemExporter exporter = new OrderItemExporter();
        IList<OrderItem> orderItemList = exporter.GetOrderItemList( orderList );
        if (orderItemList.Count > 0)
        {
            MemoryStream memStream = exporter.ExportToStream( orderItemList );
            Exception ex;
            WebUtilities.CreateExportFile( System.Web.HttpContext.Current.ApplicationInstance, memStream, "ExportOrderItem.csv", out ex );
            if (ex != null)
                uxMessage.DisplayError( Resources.ExportDataMessage.Error + ex.GetType().ToString() );
        }
        else
        {
            uxMessage.DisplayError( Resources.ExportDataMessage.CannotDownload );
        }
    }

    private void CreateOrderExportFile( IList<Order> list )
    {
        if (list.Count > 0)
        {
            OrderExporter exporter = new OrderExporter();
            MemoryStream memStream = exporter.ExportToStream( list );
            Exception ex;
            WebUtilities.CreateExportFile( System.Web.HttpContext.Current.ApplicationInstance, memStream, "ExportOrder.csv" , out ex);
            if (ex != null)
                uxMessage.DisplayError( Resources.ExportDataMessage.Error + ex.GetType().ToString() );
        }
        else
        {
            uxMessage.DisplayError( Resources.ExportDataMessage.CannotDownload );
        }

    }

    private void SetUpSortDrop(
       IList<TableSchemaItem> list )
    {
        uxSortByDrop.Items.Clear();

        for (int i = 0; i < list.Count; i++)
        {
            uxSortByDrop.Items.Add( list[i].ColumnName );
        }
    }


    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.OrderRepository.GetTableSchema();
        uxExportFilter.SetUpSchema( list );
        SetUpSortDrop( list );
    }

    private void TieGenerateButton()
    {
        uxExportFilter.TieGenerateTextBox( uxGenerateOrderButton );
    }

    private IList<Order> CreateOrderList( EventArgs e )
    {
        uxMessage.Clear();

        uxExportFilter.GetSearchFilterObj( e );

        OrderCreateExtraFilter orderExtraFilter = new OrderCreateExtraFilter( uxPaymentDrop.SelectedValue, uxProcessedDrop.SelectedValue );

        IList<Order> list = DataAccessContext.OrderRepository.ExportOrder(
            uxSortByDrop.SelectedValue,
            uxExportFilter.SearchFilterObj,
            orderExtraFilter,
            StoreID );

        return list;
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            SetUpSearchFilter();
        }

        TieGenerateButton();

        ScriptManager uxScriptManager = ScriptManager.GetCurrent( Page );
        uxScriptManager.RegisterPostBackControl( uxGenerateOrderButton );
        uxScriptManager.RegisterPostBackControl( uxGenerateOrderItemButton );

        if (!KeyUtilities.IsMultistoreLicense())
            uxStoreFilterPanel.Visible = false;
    }

    protected void uxGenerateOrderButton_Click( object sender, EventArgs e )
    {
        IList<Order> list = CreateOrderList( e );

        if (list.Count > 0)
        {
            CreateOrderExportFile( list );
        }
        else
        {
            uxMessage.DisplayError( Resources.ExportDataMessage.CannotFindData );
        }
    }

    protected void uxGenerateOrderItemButton_Click( object sender, EventArgs e )
    {
        IList<Order> list = CreateOrderList( e );

        if (list.Count > 0)
        {
            CreateOrderItemExportFile( list );
        }
        else
        {
            uxMessage.DisplayError( Resources.ExportDataMessage.CannotFindData );
        }
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

    #endregion

    #region Public Methods

    public void HideOrderGenerateButton()
    {
        uxGenerateOrderButton.Visible = false;
        uxGenerateOrderItemButton.Visible = false;
    }

    #endregion
}
