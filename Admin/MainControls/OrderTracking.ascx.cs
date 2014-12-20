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
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Orders;

public partial class AdminAdvanced_MainControls_OrderTracking : AdminAdvancedBaseUserControl
{
    private void ShowTrackingControls()
    {
        uxCustomerTracking.Visible = true;
        uxMerchantNote.Visible = false;
    }

    private void ShowNoteControls()
    {
        uxCustomerTracking.Visible = false;
        uxMerchantNote.Visible = true;
    }

    private void HideInputControls()
    {
        uxCustomerTracking.Visible = false;
        uxMerchantNote.Visible = false;
    }

    private void DisplayUserControls()
    {
        if (uxSelectTaskDrop.SelectedValue == "0")
        {
            HideInputControls();
        }
        else if (uxSelectTaskDrop.SelectedValue == "1")
        {
            ShowTrackingControls();
        }
        else if (uxSelectTaskDrop.SelectedValue == "2")
        {
            ShowNoteControls();
        }
        else
        {
            HideInputControls();
        }
    }


    private void RefreshDataTrackingGrid()
    {
        DataTable table = OrderTrackingAccess.GetByOrderID(
            CurrentOrderID, "TrackingID" );
        uxCustomerTrackingGrid.DataSource = table;
        uxCustomerTrackingGrid.DataBind();
    }

    private void RefreshMerchantNoteGrid()
    {
        DataTable table = OrderNoteAccess.GetByOrderID(
            CurrentOrderID, "NoteID" );

        uxMerchantNoteGrid.DataSource = table;
        uxMerchantNoteGrid.DataBind();
    }

    private void DisplayDetailsIfOrderExists()
    {
        try
        {
            Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
            if (order != null)
            {
                //uxPanel.Visible = true;
                DisplayUserControls();
                RefreshDataTrackingGrid();
                RefreshMerchantNoteGrid();
            }
            else
            {
                uxMessage.DisplayError( Resources.OrdersMessages.TrackingInvalidOrderID );
                //uxPanel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected string CurrentOrderID
    {
        get
        {
            return MainContext.QueryString["OrderID"];
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        DisplayDetailsIfOrderExists();
    }
}
