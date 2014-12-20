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
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;

public partial class AdminAdvanced_Components_OrderItemDetails : AdminAdvancedBaseUserControl
{
    #region Private

    private enum Mode { Add, Edit };
    private Mode _mode = Mode.Add;

    private string _orderID;


    private string CurrentOrderID
    {
        get
        {
            if (_orderID == null)
            {
                if (IsEditMode())
                {
                    _orderID = GetCurrentOrderIDByOrderItemID( CurrentOrderItemID );
                }
                else
                {
                    _orderID = MainContext.QueryString["OrderID"];
                }
            }

            return _orderID;
        }
    }

    private string CurrentOrderItemID
    {
        get
        {
            return MainContext.QueryString["OrderItemID"];
        }
    }


    //private string GetOrderEditLink()
    //{
    //    return Page.ResolveUrl( "OrdersEdit.aspx?OrderID=" + CurrentOrderID );
    //}

    private void ClearInputField()
    {
        uxQuantityText.Text = "";
        uxNameText.Text = "";
        uxSkuText.Text = "";
        uxUnitPriceText.Text = "";
        uxDownloadCountText.Text = "";
    }

    private void PopulateControl()
    {
        ClearInputField();

        OrderItem orderItem = DataAccessContext.OrderItemRepository.GetOne( CurrentOrderItemID );
        uxQuantityText.Text = orderItem.Quantity.ToString();
        uxNameText.Text = orderItem.Name;
        uxSkuText.Text = orderItem.Sku;
        uxUnitPriceText.Text = orderItem.UnitPrice.ToString( "f2" );
        uxDownloadCountText.Text = orderItem.DownloadCount.ToString();
        IsItemDownloadable( orderItem.ProductID );
    }

    private void IsItemDownloadable( string ProductID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, new StoreRetriever().GetCurrentStoreID() );
        bool IsDownloadable = product.IsDownloadable;
        if (!IsDownloadable)
        {
            DownloadCountPanel.Visible = false;
        }
    }

    private string GetCurrentOrderIDByOrderItemID( string OrderItemID )
    {
        OrderItem orderItem = DataAccessContext.OrderItemRepository.GetOne( CurrentOrderItemID );
        return orderItem.OrderID;
    }

    private OrderItem LoadOrderItemDataFromGui( OrderItem orderItem )
    {
        orderItem.OrderID = CurrentOrderID;
        orderItem.Quantity = ConvertUtilities.ToInt32( uxQuantityText.Text );
        orderItem.Name = uxNameText.Text;
        orderItem.Sku = uxSkuText.Text;
        orderItem.UnitPrice = ConvertUtilities.ToDecimal( uxUnitPriceText.Text );
        orderItem.DownloadCount = ConvertUtilities.ToInt32( uxDownloadCountText.Text );
        return orderItem;
    }

    protected void checkDownloadCount( object source, ServerValidateEventArgs args )
    {
        int count = ConvertUtilities.ToInt32( uxDownloadCountText.Text );
        bool IsVisible = DownloadCountPanel.Visible;
        if (IsVisible && count < 0)
        {
            args.IsValid = false;
        }
    }

    private void CreateNewOrderItem()
    {
        OrderItem orderItem = LoadOrderItemDataFromGui( new OrderItem() );
        DataAccessContext.OrderItemRepository.Save( orderItem );
    }

    private void UpdateCurrentOrderItem()
    {
        OrderItem orderItem = DataAccessContext.OrderItemRepository.GetOne( CurrentOrderItemID );
        orderItem = LoadOrderItemDataFromGui( orderItem );
        DataAccessContext.OrderItemRepository.Save( orderItem );
    }

    private void RefreshOrderPricing()
    {
        OrderEditService orderEditService = new OrderEditService();
        orderEditService.RefreshOrderAmount( CurrentOrderID );
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            if (IsEditMode())
            {
                PopulateControl();
            }

            uxOrderIDdisplayLable.Text = CurrentOrderID;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (IsEditMode())
        {
            if (!MainContext.IsPostBack)
            {
                if (IsAdminModifiable())
                {
                    uxUpdateButton.Visible = true;
                }
                else
                {
                    uxUpdateButton.Visible = false;
                }

                uxAddButton.Visible = false;
            }
        }
        else
        {
            if (IsAdminModifiable())
            {
                uxAddButton.Visible = true;
                uxUpdateButton.Visible = false;
            }
            else
            {
                MainContext.RedirectMainControl( "OrderList.ascx" );
            }
        }

    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                CreateNewOrderItem();

                RefreshOrderPricing();

                ClearInputField();

                MainContext.RedirectMainControl( "OrdersEdit.ascx", String.Format( "OrderID={0}", CurrentOrderID ) );

                //uxMessage.DisplayMessage( Resources.OrderItemMessage.AddSuccess );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateCurrentOrderItem();

                RefreshOrderPricing();

                MainContext.RedirectMainControl( "OrdersEdit.ascx", String.Format( "OrderID={0}", CurrentOrderID ) );

                //uxMessage.DisplayMessage( Resources.OrderItemMessage.UpdateSuccess );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxOrderLink_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "OrdersEdit.ascx", String.Format( "OrderID={0}", CurrentOrderID ) );
    }
    #endregion


    #region Public Methods

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }

    #endregion

}
