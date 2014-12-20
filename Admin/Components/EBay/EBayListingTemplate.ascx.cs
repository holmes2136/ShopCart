using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using Vevo;
using Vevo.Data;
using Vevo.Domain;
using Vevo.Shared.Utilities;
using Vevo.Deluxe.Domain.EBay;
using Vevo.Deluxe.Domain;

public partial class AdminAdvanced_Components_EBay_EBayListingTemplate : AdminAdvancedBaseUserControl
{
    private void GenerateListingTemplate()
    {
        ListItem dummyItem = new ListItem( "--Select Template--", "-1" );
        uxListingTemplateDrop.Items.Insert( 0, dummyItem );

        IList<EBayTemplate> eBayTemplateList = DataAccessContextDeluxe.EBayTemplateRepository.GetAll( "EBayTemplateID" );

        foreach (EBayTemplate eBayTemplate in eBayTemplateList)
        {
            ListItem item = new ListItem();
            item.Text = eBayTemplate.EBayTemplateName;
            item.Value = eBayTemplate.EBayTemplateID;
            uxListingTemplateDrop.Items.Add( item );
        }
    }

    private void PopulateDropDownTime()
    {
        for (int hour = 0; hour < 24; hour++)
        {
            ListItem hourList = new ListItem( hour.ToString(), hour.ToString() );
            uxHourDrop.Items.Add( hourList );
        }

        for (int min = 0; min < 60; min = min + 15)
        {
            ListItem minList = new ListItem( min.ToString(), min.ToString() );
            uxMinDrop.Items.Add( minList );
        }
    }

    private void PopulateEBayTemplate()
    {
        if (uxListOnRadio.Checked)
        {
            uxCalendarTD.Visible = true;
        }
        else
        {
            uxCalendarTD.Visible = false;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            GenerateListingTemplate();
            PopulateDropDownTime();
        }
    }

    public string GetSelectedEBayTemplateID()
    {
        return uxListingTemplateDrop.SelectedValue;
    }

    public DateTime GetListingSchedule( out Boolean isFixDateValid, out Boolean isFixedDate )
    {
        isFixedDate = false;
        isFixDateValid = false;
        if (uxListOnRadio.Checked)
        {
            if (uxFixedDateCalendarPopup.IsValid)
            {
                isFixedDate = true;
                isFixDateValid = true;
                return new DateTime(
                    uxFixedDateCalendarPopup.SelectedDate.Year,
                    uxFixedDateCalendarPopup.SelectedDate.Month,
                    uxFixedDateCalendarPopup.SelectedDate.Day,
                    ConvertUtilities.ToInt32( uxHourDrop.SelectedValue ),
                    ConvertUtilities.ToInt32( uxMinDrop.SelectedValue ),
                    0 );
            }
            else
            {
                return new DateTime();
            }
        }
        else
        {
            isFixDateValid = true;
            return DateTime.Now;
        }
    }

    public void PopulateControls()
    {
        PopulateEBayTemplate();
    }
}
