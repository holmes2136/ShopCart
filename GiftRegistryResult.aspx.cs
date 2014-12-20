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
using Vevo.Domain;
using Vevo;
using Vevo.Deluxe.Domain;

public partial class GiftRegistryResult : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    private String FirstName
    {
        get
        {
            return Request.QueryString["FirstName"];
        }
    }

    private string LastName
    {
        get
        {
            return Request.QueryString["LastName"];
        }
    }

    private string EventName
    {
        get
        {
            return Request.QueryString["EventName"];
        }
    }

    private string StartEventDate
    {
        get
        {
            return Request.QueryString["StartEventDate"];
        }
    }

    private string EndEventDate
    {
        get
        {
            return Request.QueryString["EndEventDate"];
        }
    }

    private void PopulateGiftRegistry( out int totalItems )
    {
        int itemsPerPage = 20;
        uxGrid.DataSource = DataAccessContextDeluxe.GiftRegistryRepository.Search(
            FirstName,
            LastName,
            EventName,
            StartEventDate,
            EndEventDate,
            DataAccessContext.StoreRetriever.GetCurrentStoreID(),
            (uxPagingControl.CurrentPage - 1) * itemsPerPage,
            (uxPagingControl.CurrentPage * itemsPerPage) - 1,
            out totalItems );
        uxGrid.DataBind();
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / itemsPerPage );
    }

    private void PopulateText( int totalItems )
    {
        if (totalItems <= 0)
        {
            uxNoResultPanel.Visible = true;
        }
    }

    private void PopulateLink( int totalItems )
    {
        if (totalItems > 0)
            uxBackLink.Visible = false;
        else
            uxBackLink.Visible = true;
    }

    private void PopulateControls()
    {
        int totalItems;
        PopulateGiftRegistry( out totalItems );

        if (totalItems == 0)
            uxResultTable.Visible = false;

        PopulateText( totalItems );
        PopulateLink( totalItems );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    public string FormatDate( object eventDate )
    {
        return string.Format( "{0:dd} {0:MMM} {0:yyyy}", eventDate );
    }
}
