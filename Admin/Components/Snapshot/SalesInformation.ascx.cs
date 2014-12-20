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
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Shared.Utilities;

public partial class AdminAdvanced_Components_Snapshot_SalesInformation : AdminAdvancedBaseUserControl
{
    private DateTime? GetStartDate()
    {
        switch (uxSalesFilterDropDown.SelectedValue)
        {
            case "Year":
                return DateTimeUtilities.GetFirstDayOfTheYear( DateTime.Now );

            case "Month":
                return DateTimeUtilities.GetFirstDayOfTheMonth( DateTime.Now );

            case "LastMonth":
                return DateTimeUtilities.GetFirstDayOfTheMonth( DateTime.Now.AddMonths( -1 ) );

            case "Week":
                return DateTimeUtilities.GetFirstDayOfTheWeek( DateTime.Now );

            case "LastWeek":
                return DateTimeUtilities.GetFirstDayOfTheWeek( DateTime.Now.AddDays( -7 ) );

            default:
                return null;
        }
    }

    private DateTime? GetEndDate()
    {
        switch (uxSalesFilterDropDown.SelectedValue)
        {
            case "LastMonth":
                return DateTimeUtilities.GetLastDayOfTheMonth( DateTime.Now.AddMonths( -1 ) );

            case "LastWeek":
                return DateTimeUtilities.GetLastDayOfTheWeek( DateTime.Now.AddDays( -7 ) );

            default:
                return DateTime.Now;
        }
    }

    private void PopulateDate()
    {
        uxDateLabel.Text = "Period: ";

        switch (uxSalesFilterDropDown.SelectedValue)
        {
            case "All":
                uxDateLabel.Text += "All paid transactions";
                break;

            default:
                uxDateLabel.Text += String.Format(
                    "{0} to {1}",
                    GetStartDate().Value.ToShortDateString(),
                    GetEndDate().Value.ToShortDateString() );
                break;
        }
    }

    private void PopulateControl()
    {
        PopulateDate();

        IList<Order> list = DataAccessContext.OrderRepository.GetPaidOrdersByDateTime( GetStartDate(), GetEndDate() );

        uxSaleAmountLabel.Text = String.Format( "Number of Transactions: <strong>{0}</strong>", list.Count );
        decimal subtotal = 0;
        for (int i = 0; i < list.Count;i++ )
        {
            subtotal += list[i].Total;
        }
        uxSaleTotalLabel.Text = String.Format(
            "Sales Amount: <strong>{0}</strong>",
            AdminUtilities.FormatPrice( subtotal ) );
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            PopulateControl();
    }

    protected void uxSalesFilterDropDown_SelectedIndexChanged( object sender, EventArgs e )
    {
        PopulateControl();
    }

}
