using System;
using Vevo.WebAppLib;

public partial class AffiliateCommissionSearch : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private bool AreAllInputsEmpty()
    {
        return (uxStartOrderID.Text.Trim() == "" &&
            uxEndOrderID.Text.Trim() == "" &&
            uxStartAmount.Text.Trim() == "" &&
            uxEndAmount.Text.Trim() == "" &&
            uxStartCommission.Text.Trim() == "" &&
            uxEndCommission.Text.Trim() == "" &&
            uxStartDateCalendarPopup.SelectedDateText.Trim() == "" &&
            uxEndDateCalendarPopup.SelectedDateText.Trim() == "" &&
            uxStatus.SelectedValue == "");
    }

    private bool AreCalendarsValid()
    {
        if (!String.IsNullOrEmpty( uxStartDateCalendarPopup.SelectedDateText.Trim() ) &&
            !uxStartDateCalendarPopup.IsValid)
            return false;

        if (!String.IsNullOrEmpty( uxEndDateCalendarPopup.SelectedDateText.Trim() ) &&
            !uxEndDateCalendarPopup.IsValid)
            return false;

        return true;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        WebUtilities.TieButton( this.Page, uxStartOrderID, uxSearchImageButton );
        WebUtilities.TieButton( this.Page, uxEndOrderID, uxSearchImageButton );
        WebUtilities.TieButton( this.Page, uxStartAmount, uxSearchImageButton );
        WebUtilities.TieButton( this.Page, uxEndAmount, uxSearchImageButton );
        WebUtilities.TieButton( this.Page, uxStartCommission, uxSearchImageButton );
        WebUtilities.TieButton( this.Page, uxEndCommission, uxSearchImageButton );
        uxStartDateCalendarPopup.BindCalendarButton( this.Page, uxSearchImageButton );
        uxEndDateCalendarPopup.BindCalendarButton( this.Page, uxSearchImageButton );
    }

    protected void uxSearchImageButton_Click( object sender, EventArgs e )
    {
        if (AreAllInputsEmpty())
        {
            uxMessage.DisplayError( "[$EmptyInput]" );
        }
        else if (!AreCalendarsValid())
        {
            uxMessage.DisplayError( "[$DateInvalid]" );
        }
        else
        {
            Response.Redirect(
                "AffiliateCommission.aspx?StartOrderID=" + uxStartOrderID.Text.Trim() +
                "&EndOrderID=" + uxEndOrderID.Text.Trim() +
                "&StartAmount=" + uxStartAmount.Text.Trim() +
                "&EndAmount=" + uxEndAmount.Text.Trim() +
                "&StartCommission=" + uxStartCommission.Text.Trim() +
                "&EndCommission=" + uxEndCommission.Text.Trim() +
                "&StartOrderDate=" + uxStartDateCalendarPopup.SelectedDateText +
                "&EndOrderDate=" + uxEndDateCalendarPopup.SelectedDateText +
                "&PaymentStatus=" + uxStatus.SelectedValue
                );
        }
    }
}
