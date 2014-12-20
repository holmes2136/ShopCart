using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Deluxe.Domain.EBay;
using Vevo.eBay;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain;
using Vevo.Shared.DataAccess;

public partial class Admin_MainControls_EBayListing : AdminAdvancedBaseListControl
{
    #region Private
    private const int ListIDColumnIndex = 1;
    private const int ListItemNumberColumnIndex = 3;
    private const int ListTypeColumnIndex = 6;

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContextDeluxe.EBayListRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list, "LastUpdate" );
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
            RefreshListingCurrentPage();
            PopulateReasonDropdown();
        }

        if (uxGirdEBayList.Rows.Count > 0)
        {
            DeleteVisible( true );
            uxPagingControl.Visible = true;
        }
        else
        {
            DeleteVisible( false );
            uxPagingControl.Visible = false;
        }

        if (!IsAdminModifiable())
        {
            DeleteVisible( false );
            uxEndListingButton.Visible = false;
        }
    }

    private void PopulateReasonDropdown()
    {
        uxDropDownReason.Items.Clear();
        uxDropDownReason.Items.Add( new ListItem( "Incorrect Price", "Incorrect" ) );
        uxDropDownReason.Items.Add( new ListItem( "Lost or Broken Price", "LostOrBroken" ) );
        uxDropDownReason.Items.Add( new ListItem( "Unavailable for sale", "NotAvailable" ) );
        uxDropDownReason.Items.Add( new ListItem( "Contained the error", "OtherListingError" ) );
        uxDropDownReason.Items.Add( new ListItem( "The listing has qualifying bids", "SellToHighBidder" ) );
    }

    private void DeleteVisible( bool value )
    {
        uxDeleteButton.Visible = value;
        uxEndListingButton.Visible = value;
        if (value)
        {
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {
                uxDeleteConfirmButton.TargetControlID = "uxDeleteButton";
                uxConfirmModalPopup.TargetControlID = "uxDeleteButton";
                uxEndListingConfirmButton.TargetControlID = "uxEndListingButton";
                uxEndListingConfirmModalPopup.TargetControlID = "uxEndListingButton";
            }
            else
            {
                uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
                uxConfirmModalPopup.TargetControlID = "uxDummyButton";
                uxEndListingConfirmButton.TargetControlID = "uxDummyButton_2";
                uxEndListingConfirmModalPopup.TargetControlID = "uxDummyButton_2";
            }
        }
        else
        {
            uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
            uxConfirmModalPopup.TargetControlID = "uxDummyButton";
            uxEndListingConfirmButton.TargetControlID = "uxDummyButton_2";
            uxEndListingConfirmModalPopup.TargetControlID = "uxDummyButton_2";
        }
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.ProductItemsPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxGirdEBayList, "ListID" );

        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }

    private void RefreshListingInTable( string listID )
    {
        EBayList list = DataAccessContextDeluxe.EBayListRepository.GetOne( listID );
        if (list.LastStatus != EBayList.EBayListStatus.Completed &&
            list.LastStatus != EBayList.EBayListStatus.Ended)
        {
            EBayAccess ebayAccess = new EBayAccess();
            list = ebayAccess.GetListUpdateFromItem( list );
            DataAccessContextDeluxe.EBayListRepository.Save( list );
        }
    }

    private void RefreshListingCurrentPage()
    {
        foreach (GridViewRow row in uxGirdEBayList.Rows)
        {
            string listID = row.Cells[ListIDColumnIndex].Text;
            RefreshListingInTable( listID );
        }
        RefreshGrid();
    }

    private EBayItem GetEBayItemDetailByID( string eBayItemID )
    {
        EBayAccess acc = new EBayAccess();
        return acc.GetItemDetail( eBayItemID, acc.GetApiContext() );
    }

    #endregion
    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
            MainContext.RedirectMainControl( "Default.ascx", String.Empty );
        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxGirdEBayList.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string id = row.Cells[ListIDColumnIndex].Text.Trim();
                    DataAccessContextDeluxe.EBayListRepository.Delete( id );
                    deleted = true;
                }
            }

            uxStatusHidden.Value = "Deleted";

            if (deleted)
            {
                uxMessage.DisplayMessage( Resources.EBayMessages.DeleteSuccess );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if (uxGirdEBayList.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }
    }

    protected void uxEndListingButton_Click( object sender, EventArgs e )
    {
        EBayAccess eBayAccess = new EBayAccess();
        string failedItems = String.Empty;
        int successCount = 0;

        foreach (GridViewRow row in uxGirdEBayList.Rows)
        {
            CheckBox endCheck = (CheckBox) row.FindControl( "uxCheck" );
            if (endCheck.Checked)
            {
                HyperLink hyperlink = (HyperLink) row.FindControl( "uxHyperlinkItemNumber" );
                string listNumber = hyperlink.Text;
                EBayList.EBayListEndListingReason endType = (EBayList.EBayListEndListingReason) Enum.Parse( typeof( EBayList.EBayListEndListingReason ), uxDropDownReason.SelectedValue, true );
                bool result = eBayAccess.EndListing( listNumber, endType );
                if (result == false)
                {
                    failedItems += listNumber + ",";
                }
                else
                {
                    successCount += 1;
                }
            }
        }

        RefreshListingCurrentPage();

        if (failedItems == String.Empty)
        {
            uxMessage.DisplayMessage( Resources.EBayMessages.EndListingSuccess + " " + successCount.ToString() + " item(s)." );
        }
        else
        {
            uxMessage.DisplayError( Resources.EBayMessages.EndListingError + " " + failedItems.Substring( 0, failedItems.Length - 1 ) );
        }
    }

    protected void uxRefreshButton_Click( object sender, EventArgs e )
    {
        RefreshListingCurrentPage();
    }

    protected void uxGridEBayList_RowDataBound( object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string productID = e.Row.Cells[ListIDColumnIndex].Text.Trim();

            if (e.Row.RowIndex > -1)
            {
                if ((e.Row.RowIndex % 2) == 0)
                {
                    // Even
                    e.Row.Attributes.Add( "onmouseover", "this.className='DefaultGridRowStyleHover'" );
                    e.Row.Attributes.Add( "onmouseout", "this.className='DefaultGridRowStyle'" );
                }
                else
                {
                    // Odd
                    e.Row.Attributes.Add( "onmouseover", "this.className='DefaultGridRowStyleHover'" );
                    e.Row.Attributes.Add( "onmouseout", "this.className='DefaultGridAlternatingRowStyle'" );
                }
            }
        }

    }

    protected string GetViewURL( object itemNumber )
    {
        GetEBayItemDetailByID( (string) itemNumber );
        IList<EBayList> list = DataAccessContextDeluxe.EBayListRepository.GetEBayListByItemNumber( (string) itemNumber );
        return list[0].ViewUrl;
    }

    protected string GetBidAmount( object listID )
    {
        EBayList list = DataAccessContextDeluxe.EBayListRepository.GetOne( (string) listID );
        return list.BidAmount.ToString();
    }

    protected decimal GetBidPrice( object listID )
    {
        EBayList list = DataAccessContextDeluxe.EBayListRepository.GetOne( (string) listID );
        return list.BidPrice;
    }

    protected decimal GetBuyItNowPrice( object listID )
    {
        EBayList list = DataAccessContextDeluxe.EBayListRepository.GetOne( (string) listID );
        return list.BuyItNowPrice;
    }

    protected string GetCurrencySymbol( object listID )
    {
        EBayList list = DataAccessContextDeluxe.EBayListRepository.GetOne( (string) listID );
        Currency currency = DataAccessContext.CurrencyRepository.GetOne( list.Currency );
        return currency.CurrencySymbol;
    }

    protected override void RefreshGrid()
    {
        int totalItems;

        uxGirdEBayList.DataSource = DataAccessContextDeluxe.EBayListRepository.SearchEbayList(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGirdEBayList.DataBind();
    }
}
