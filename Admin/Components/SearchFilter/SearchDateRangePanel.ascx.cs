using System;
using System.Web.UI;
using Vevo;
using Vevo.Domain.DataInterfaces;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.Base.Domain;

public partial class AdminAdvanced_Components_SearchFilter_SearchDateRangePanel : AdminAdvancedBaseUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {

    }

    public string GetStartDateTextClientID()
    {
        return uxStartDateText.ClientID;
    }

    public string GetEndDateTextClientID()
    {
        return uxEndDateText.ClientID;
    }

    public string GetStartDateText()
    {
        try
        {
            ConvertUtilities.ToDateTime( uxStartDateText.Text );
            return uxStartDateText.Text;
        }
        catch
        {
            return "";
        }
    }

    public string GetEndDateText()
    {
        try
        {
            ConvertUtilities.ToDateTime( uxEndDateText.Text );
            return uxEndDateText.Text;
        }
        catch
        {
            return "";
        }
    }

    public void RestoreControls( SearchFilter searchFilterObj )
    {
        uxStartDateText.Text = ConvertUtilities.ToDateTime( searchFilterObj.Value1 ).ToString( "MMMM d,yyyy" );
        uxEndDateText.Text = ConvertUtilities.ToDateTime( searchFilterObj.Value2 ).ToString( "MMMM d,yyyy" );
    }

    public void TieTextBoxesWithButtons( Control button )
    {
        WebUtilities.TieButton( this.Page, uxStartDateText, button );
        WebUtilities.TieButton( this.Page, uxEndDateText, button );
    }
}
