using System;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.WebUI.BaseControls;
using System.Web.Security;

public partial class Components_CheckoutIndicator : BaseUserControl
{
    #region Private

    private bool IsEnabled
    {
        get
        {
            return StepID > 0 && DataAccessContext.Configurations.GetValue( "CheckoutMode", StoreContext.CurrentStore ).Equals( "Normal" );
        }
    }

    private void PopulateControls()
    {
        if (!Roles.IsUserInRole( "Customers" ))
        {
            uxAddressLink.NavigateUrl += "?skiplogin=true";
            uxShippingLink.NavigateUrl += "?skiplogin=true";
            uxPaymentLink.NavigateUrl += "?skiplogin=true";
            uxSummaryLink.NavigateUrl += "?skiplogin=true";
        }
        uxCurrentPageLabel.Text = Title;
        switch (StepID)
        {
            case 1:
                {
                    uxCheckoutIndicatorTable.Rows[1].Cells[0].CssClass = "LoginOn";
                    uxCheckoutIndicatorTable.Rows[1].Cells[1].CssClass = "AddressOff";
                    uxCheckoutIndicatorTable.Rows[1].Cells[2].CssClass = "ShippingOff";
                    uxCheckoutIndicatorTable.Rows[1].Cells[3].CssClass = "PaymentOff";
                    uxCheckoutIndicatorTable.Rows[1].Cells[4].CssClass = "SummaryOff";

                    uxLoginLink.Enabled = false;
                    uxAddressLink.Enabled = false;
                    uxShippingLink.Enabled = false;
                    uxPaymentLink.Enabled = false;
                    uxSummaryLink.Enabled = false;
                }
                break;
            case 2:
                {
                    uxCheckoutIndicatorTable.Rows[1].Cells[0].CssClass = "LoginPassed";
                    uxCheckoutIndicatorTable.Rows[1].Cells[1].CssClass = "AddressOn";
                    uxCheckoutIndicatorTable.Rows[1].Cells[2].CssClass = "ShippingOff";
                    uxCheckoutIndicatorTable.Rows[1].Cells[3].CssClass = "PaymentOff";
                    uxCheckoutIndicatorTable.Rows[1].Cells[4].CssClass = "SummaryOff";

                    uxLoginLink.Enabled = true;
                    uxAddressLink.Enabled = false;
                    uxShippingLink.Enabled = false;
                    uxPaymentLink.Enabled = false;
                    uxSummaryLink.Enabled = false;
                }
                break;
            case 3:
                {
                    uxCheckoutIndicatorTable.Rows[1].Cells[0].CssClass = "LoginPassed";
                    uxCheckoutIndicatorTable.Rows[1].Cells[1].CssClass = "AddressPassed";
                    uxCheckoutIndicatorTable.Rows[1].Cells[2].CssClass = "ShippingOn";
                    uxCheckoutIndicatorTable.Rows[1].Cells[3].CssClass = "PaymentOff";
                    uxCheckoutIndicatorTable.Rows[1].Cells[4].CssClass = "SummaryOff";

                    uxLoginLink.Enabled = true;
                    uxAddressLink.Enabled = true;
                    uxShippingLink.Enabled = false;
                    uxPaymentLink.Enabled = false;
                    uxSummaryLink.Enabled = false;
                }
                break;
            case 4:
                {
                    uxCheckoutIndicatorTable.Rows[1].Cells[0].CssClass = "LoginPassed";
                    uxCheckoutIndicatorTable.Rows[1].Cells[1].CssClass = "AddressPassed";
                    uxCheckoutIndicatorTable.Rows[1].Cells[2].CssClass = "ShippingPassed";
                    uxCheckoutIndicatorTable.Rows[1].Cells[3].CssClass = "PaymentOn";
                    uxCheckoutIndicatorTable.Rows[1].Cells[4].CssClass = "SummaryOff";

                    uxLoginLink.Enabled = true;
                    uxAddressLink.Enabled = true;
                    uxShippingLink.Enabled = true;
                    uxPaymentLink.Enabled = false;
                    uxSummaryLink.Enabled = false;
                }
                break;
            case 5:
                {
                    uxCheckoutIndicatorTable.Rows[1].Cells[0].CssClass = "LoginPassed";
                    uxCheckoutIndicatorTable.Rows[1].Cells[1].CssClass = "AddressPassed";
                    uxCheckoutIndicatorTable.Rows[1].Cells[2].CssClass = "ShippingPassed";
                    uxCheckoutIndicatorTable.Rows[1].Cells[3].CssClass = "PaymentPassed";
                    uxCheckoutIndicatorTable.Rows[1].Cells[4].CssClass = "SummaryOn";

                    uxLoginLink.Enabled = true;
                    uxAddressLink.Enabled = true;
                    uxShippingLink.Enabled = true;
                    uxPaymentLink.Enabled = true;
                    uxSummaryLink.Enabled = false;
                }
                break;
            default:
                {
                    uxCheckoutIndicatorTable.Rows[1].Cells[0].CssClass = "LoginOff";
                    uxCheckoutIndicatorTable.Rows[1].Cells[1].CssClass = "AddressOff";
                    uxCheckoutIndicatorTable.Rows[1].Cells[2].CssClass = "ShippingOff";
                    uxCheckoutIndicatorTable.Rows[1].Cells[3].CssClass = "PaymentOff";
                    uxCheckoutIndicatorTable.Rows[1].Cells[4].CssClass = "SummaryOff";
                }
                break;
        }
    }

    #endregion

    #region Public

    public int StepID
    {
        get
        {
            if (ViewState["CheckoutIndicator"] == null)
                ViewState["CheckoutIndicator"] = 0;

            return Convert.ToInt32( ViewState["CheckoutIndicator"] );
        }
        set
        {
            ViewState["CheckoutIndicator"] = value;
        }
    }
    public string Title
    {
        get
        {
            if (ViewState["Title"] == null)
                return "Checkout Process";
            return ViewState["Title"].ToString();
        }
        set
        {
            ViewState["Title"] = value;
        }
    }
    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        if (IsEnabled)
        {
            PopulateControls();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (IsEnabled)
            uxCheckoutIndicatorPanel.Visible = true;
        else
            uxCheckoutIndicatorPanel.Visible = false;
    }

    #endregion
}