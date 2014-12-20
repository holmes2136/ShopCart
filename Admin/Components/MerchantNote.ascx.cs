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


public partial class AdminAdvanced_Components_MerchantNote : AdminAdvancedBaseUserControl
{
    private string CurrentOrderID
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["OrderID"] ))
                return MainContext.QueryString["OrderID"];
            else
                return "0";
        }
    }


    private void ClearInput()
    {
        uxNameText.Text = String.Empty;
        uxNoteText.Text = String.Empty;
    }

    private void PopulateControls()
    {
        uxOrderIDLabel.Text = CurrentOrderID;
        ClearInput();
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateControls();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsAdminModifiable())
            uxSaveNoteButton.Visible = false;
    }

    protected void uxSaveNoteButton_Click( object sender, EventArgs e )
    {
        try
        {
            OrderNoteAccess.Create( CurrentOrderID, uxNameText.Text, uxNoteText.Text );

            MessageControl.DisplayMessage( Resources.OrdersMessages.NoteAddSuccess );
        }
        catch (Exception ex)
        {
            MessageControl.DisplayException( ex );
        }

        ClearInput();
    }

}
