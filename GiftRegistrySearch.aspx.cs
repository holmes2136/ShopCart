using System;
using Vevo;
using Vevo.WebAppLib;

public partial class GiftRegistrySearch : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    private bool AreAllInputsEmpty()
    {
        return (uxEventName.Text.Trim() == "" &&
            uxFirstName.Text.Trim() == "" &&
            uxLastName.Text.Trim() == "" &&
            uxStartDateCalendarPopup.SelectedDateText.Trim() == "" &&
            uxEndDateCalendarPopup.SelectedDateText.Trim() == "");

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
        WebUtilities.TieButton( this.Page, uxFirstName, uxFindImageButton );
        WebUtilities.TieButton( this.Page, uxLastName, uxFindImageButton );
        WebUtilities.TieButton( this.Page, uxEventName, uxFindImageButton );
        uxStartDateCalendarPopup.BindCalendarButton( this.Page, uxFindImageButton );
        uxEndDateCalendarPopup.BindCalendarButton( this.Page, uxFindImageButton );               
    }

    protected void uxFindImageButton_Click( object sender, EventArgs e )
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
                "GiftRegistryResult.aspx?FirstName=" + uxFirstName.Text.Trim() +
                "&LastName=" + uxLastName.Text.Trim() +
                "&EventName=" + uxEventName.Text.Trim() +
                "&StartEventDate=" + uxStartDateCalendarPopup.SelectedDateText +
                "&EndEventDate=" + uxEndDateCalendarPopup.SelectedDateText );
        }
    }

}
