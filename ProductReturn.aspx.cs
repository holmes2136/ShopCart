using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.Domain.Returns;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class ProductReturn : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    #region Private

    private string CurrentOrderID
    {
        get
        {
            if (!String.IsNullOrEmpty( Request.QueryString["OrderID"] ))
                return Request.QueryString["OrderID"];
            else
                return "0";
        }
    }

    private bool IsGridViewValid()
    {
        bool hasCheckBoxSelected = false;

        foreach (GridViewRow row in uxProductReturnGrid.Rows)
        {
            if (row.FindControl( "uxSelectBox" ) != null)
            {
                CheckBox box = (CheckBox) row.FindControl( "uxSelectBox" );

                if (box.Checked)
                {
                    hasCheckBoxSelected = true;
                    TextBox text = (TextBox) row.FindControl( "uxQuantityText" );
                    RangeValidator range = (RangeValidator) row.FindControl( "uxQuantityRange" );

                    if (String.IsNullOrEmpty( text.Text ))
                    {
                        range.IsValid = false;
                    }
                    else
                    {
                        range.Validate();
                    }
                }
            }
        }

        return hasCheckBoxSelected;
    }

    private void PopulateControl()
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );

            if (order != null && Page.User.Identity.Name.ToLower().Equals( order.UserName.ToLower() ))
            {
                uxProductReturnGrid.DataSource = DataAccessContext.OrderItemRepository.GetByOrderID( CurrentOrderID );
                uxProductReturnGrid.DataBind();

                uxReturnDrop.DataSource = DataAccessContext.RmaActionRepository.GetAll( BoolFilter.ShowTrue );
                uxReturnDrop.DataTextField = "Name";
                uxReturnDrop.DataValueField = "RmaActionID";
                uxReturnDrop.DataBind();
                uxReturnDrop.Items.Insert( 0, "[$Select]" );
            }
            else
            {
                Response.Redirect( "~/OrderHistory.aspx" );
            }
        }
        else
        {
            Response.Redirect( "~/Default.aspx" );
        }
    }

    private void SendMailToMerchant( Rma rma )
    {
        string subjectText;
        string bodyText;

        EmailTemplateTextVariable.ReplaceRMARequisitionText( rma, out subjectText, out bodyText );

        WebUtilities.SendHtmlMail(
            NamedConfig.CompanyEmail,
            NamedConfig.CompanyEmail,
            subjectText,
            bodyText );
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControl();
        }
    }

    protected void uxProductReturnButton_Click( object sender, EventArgs e )
    {
        uxGridValidator.IsValid = IsGridViewValid();

        if (Page.IsValid)
        {
            Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );

            foreach (GridViewRow row in uxProductReturnGrid.Rows)
            {
                if (row.FindControl( "uxSelectBox" ) != null)
                {
                    CheckBox box = (CheckBox) row.FindControl( "uxSelectBox" );
                    Label product = (Label) row.FindControl( "uxNameProductLabel" );
                    TextBox quantity = (TextBox) row.FindControl( "uxQuantityText" );

                    if (box.Checked)
                    {
                        Rma rma = new Rma();
                        rma.CustomerID = order.CustomerID;
                        rma.OrderID = order.OrderID;
                        rma.ProductName = product.Text;
                        rma.Quantity = ConvertUtilities.ToInt32( quantity.Text );
                        rma.RequestDate = DateTime.Now;
                        rma.RequestStatus = "New";
                        rma.ReturnReason = uxReasonText.Text;
                        rma.RmaActionID = uxReturnDrop.SelectedValue;
                        rma.RmaNote = uxNoteText.Text;
                        rma.StoreID = order.StoreID;
                        rma = DataAccessContext.RmaRepository.Save( rma );

                        try
                        {
                            SendMailToMerchant( rma );
                        }
                        catch (Exception)
                        {
                            uxErrorMessage.DisplayErrorNoNewLine( "[$SentErrorMessage]" );
                            return;
                        }
                    }
                }
            }

            Response.Redirect( "~/RmaHistory.aspx" );
        }
    }

    protected void uxProductReturnGrid_RowDataBound( object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            OrderItem item = (OrderItem) e.Row.DataItem;

            Product product = DataAccessContext.ProductRepository.GetOne(
                StoreContext.Culture, item.ProductID, StoreContext.CurrentStore.StoreID );
            Order order = DataAccessContext.OrderRepository.GetOne( item.OrderID );

            DateTime currectDate = DateTime.Now.Date;
            DateTime orderDate = order.OrderDate.Date;
            DateTime expireDate = orderDate.Date.AddDays( ConvertUtilities.ToDouble( product.ReturnTime ) );

            if (currectDate > expireDate || item.ProductID.Equals( "0" ) || product.ReturnTime == 0)
            {
                CheckBox box = (CheckBox) e.Row.FindControl( "uxSelectBox" );
                box.Visible = false;

                Label quantity = (Label) e.Row.FindControl( "uxQuantityLabel" );
                quantity.Text = "[$NotReturn]";

                TextBox text = (TextBox) e.Row.FindControl( "uxQuantityText" );
                text.Visible = false;

                RangeValidator range = (RangeValidator) e.Row.FindControl( "uxQuantityRange" );
                range.Visible = false;
                range.Enabled = false;

               e.Row.ForeColor = Color.FromArgb( 160, 160, 160 );
            }
        }
    }

    #endregion
}
