using System;
using System.Web;
using eBay.Service.Core.Sdk;
using Vevo;
using Vevo.Domain;
using Vevo.Deluxe.Domain.EBay;
using Vevo.Domain.Products;
using Vevo.eBay;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;
using Vevo.WebUI;
using Vevo.Deluxe.Domain;

public partial class AdminAdvanced_Components_EBayListingProcess : AdminAdvancedBaseUserControl
{
    private enum ListingState
    {
        SelectTemplate = 1,
        CheckCondition = 2,
        ShowFee = 3,
        Listing = 4
    }

    private ListingState CurrentState
    {
        get
        {
            if (ViewState["CurrentState"] == null ||
                ViewState["CurrentState"].ToString() == String.Empty)
            {
                return ListingState.SelectTemplate;
            }
            else
            {
                return (ListingState) ViewState["CurrentState"];
            }
        }
        set
        {
            ViewState["CurrentState"] = value;
        }
    }

    private void SwitchPanelByState( ListingState state )
    {
        CurrentState = state;
        switch (CurrentState)
        {
            case ListingState.SelectTemplate:
                uxListingTemplatePanel.Visible = true;
                uxCheckConditionPanel.Visible = false;
                uxShowFeePanel.Visible = false;
                break;
            case ListingState.CheckCondition:
                uxListingTemplatePanel.Visible = false;
                uxCheckConditionPanel.Visible = true;
                uxShowFeePanel.Visible = false;
                break;
            case ListingState.ShowFee:
                uxListingTemplatePanel.Visible = false;
                uxCheckConditionPanel.Visible = false;
                uxShowFeePanel.Visible = true;
                break;
            default:
                break;
        }

        PopulateByState();
    }

    private void PopulateByState()
    {
        uxNext.Visible = true;
        uxListing.Visible = false;

        try
        {
            if (CurrentState == ListingState.SelectTemplate)
            {
                uxListingTemplate.PopulateControls();
            }
            else if (CurrentState == ListingState.CheckCondition)
            {
                uxHiddenSelectedTemplateID.Value = uxListingTemplate.GetSelectedEBayTemplateID();

                if (!uxHiddenSelectedTemplateID.Value.Equals( "-1" ))
                {
                    Boolean isFixDateValid = false;
                    Boolean isScheduleDate = false;
                    uxHiddenScheduleDateTime.Value = uxListingTemplate.GetListingSchedule( out isFixDateValid, out isScheduleDate ).ToString();
                    uxHiddenIsSchedule.Value = isScheduleDate.ToString();

                    if (isFixDateValid)
                    {
                        if (!uxCheckCondition.PopulateControls( uxHiddenSelectedTemplateID.Value, ProductIDList ))
                        {
                            uxNext.Visible = false;
                        }
                    }
                    else
                    {
                        CurrentState = ListingState.SelectTemplate;
                        uxMessage.DisplayError( "Error : Please select schedule date." );
                    }
                }
                else
                {
                    CurrentState = ListingState.SelectTemplate;
                    uxMessage.DisplayError( "Error : Please select listing template." );
                }

            }
            else if (CurrentState == ListingState.ShowFee)
            {
                uxShowFee.PopulateControls(
                    uxHiddenSelectedTemplateID.Value,
                    ProductIDList,
                    ConvertUtilities.ToBoolean( uxHiddenIsSchedule.Value ),
                    ConvertUtilities.ToDateTime( uxHiddenScheduleDateTime.Value ) );

                uxNext.Visible = false;
                uxListing.Visible = true;
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayError( "Error : " + ex.Message );
        }
    }

    private string[] ProductIDList
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["ProductID"].ToString() ))
            {
                return MainContext.QueryString["ProductID"].ToString().Split( ',' );
            }
            else
            {
                return null;
            }
        }
    }

    private Product GetProductByProductID( string productID )
    {
        return DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, productID, StoreContext.CurrentStore.StoreID );
    }

    private EBayItem GetEBayItemDetailByID( string eBayItemID, EBayTemplate template )
    {
        EBayAccess access = new EBayAccess();
        return access.GetItemDetail( eBayItemID, access.GetApiContext( template.EBayListSite ) );
    }

    private EBayTemplate GetEBayTemplateByID( string templateID )
    {
        return DataAccessContextDeluxe.EBayTemplateRepository.GetOne( templateID );
    }

    private EBayCategory GetEBayCategoryByTemplate( EBayTemplate template )
    {
        EBayAccess access = new EBayAccess();
        EBayCategory primaryCategory = access.GetCategoriesDetailsByID( template.PrimaryeBayCategoryID, template.EBayListSite );
        EBayCategory eBayCategory = primaryCategory;
        if (!template.SecondaryeBayCategoryID.Equals( "0" ))
        {
            EBayCategory secondaryCategory = access.GetCategoriesDetailsByID( template.SecondaryeBayCategoryID, template.EBayListSite );
            eBayCategory.SecondaryCategoryID = secondaryCategory.PimaryCategoryID;
            eBayCategory.SecondaryCategoryName = secondaryCategory.PimaryCategoryName;
        }
        return eBayCategory;
    }

    private string ListProductToEBay(
        Product product,
        EBayTemplate template,
        EBayCategory category,
        Boolean isScheduleDate,
        DateTime scheduleDateTime )
    {
        string eBayItemID = string.Empty;

        EBayAccess access = new EBayAccess( UrlPath.StorefrontUrl );
        eBayItemID = access.AddItem( product, template, category, isScheduleDate, scheduleDateTime );

        if (access.HasError)
        {
            string errorMessage = string.Empty;
            foreach (EBayErrorType error in access.ErrorTypeList)
            {
                errorMessage += HttpUtility.HtmlEncode( error.ErrorLongMessage ) + " (" + error.ErrorCode + ")<br/>";
            }
            uxMessage.DisplayError( errorMessage );
        }

        return eBayItemID;
    }

    private void SaveListing( string eBayItemID, EBayTemplate template )
    {
        EBayAccess acc = new EBayAccess();
        EBayItem item = GetEBayItemDetailByID( eBayItemID, template );
        ApiContext apiContext = acc.GetApiContext( template.EBayListSite );
        EBayList eBayList = new EBayList();

        if (item.Type.Equals( "Chinese" ))
        {
            eBayList.BuyItNowPrice = ConvertUtilities.ToDecimal( item.Bidding_BuyItNowPrice );
        }
        else
        {
            eBayList.BuyItNowPrice = ConvertUtilities.ToDecimal( item.BuyItNowPrice );
        }

        eBayList.BidPrice = (decimal) item.Bidding_CurrentPrice;

        eBayList.ItemName = item.Title;
        eBayList.ItemNumber = item.ID;
        eBayList.LastStatus = (EBayList.EBayListStatus) Enum.Parse( typeof( EBayList.EBayListStatus ), item.ListingStatus, true );
        eBayList.ListDate = item.StartTime;
        eBayList.ListType = (EBayList.EBayListType) Enum.Parse( typeof( EBayList.EBayListType ), item.Type, true );
        eBayList.QtyLeft = item.QuantityLeft;
        eBayList.LastUpdate = DateTime.Now;
        eBayList.ViewUrl = item.ViewUrl;
        eBayList.Currency = item.Currency;

        eBayList = DataAccessContextDeluxe.EBayListRepository.Save( eBayList );
    }

    private void CreateList()
    {
        EBayTemplate template = GetEBayTemplateByID( uxHiddenSelectedTemplateID.Value );

        foreach (string productID in ProductIDList)
        {
            if (!string.IsNullOrEmpty( productID ))
            {
                string eBayItemID = ListProductToEBay(
                    GetProductByProductID( productID ),
                    template, GetEBayCategoryByTemplate( template ),
                    ConvertUtilities.ToBoolean( uxHiddenIsSchedule.Value ),
                    ConvertUtilities.ToDateTime( uxHiddenScheduleDateTime.Value ) );

                if (String.IsNullOrEmpty( eBayItemID ))
                    return;

                SaveListing( eBayItemID, template );
            }
        }
        MainContext.RedirectMainControl( "EBayListing.ascx" );
    }

    private void PopulateControls()
    {
        uxListingTemplate.PopulateControls();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (CurrentState == ListingState.SelectTemplate)
            SwitchPanelByState( ListingState.SelectTemplate );
    }

    protected void uxNext_Click( object sender, EventArgs e )
    {
        switch (CurrentState)
        {
            case ListingState.SelectTemplate:
                SwitchPanelByState( ListingState.CheckCondition );
                break;
            case ListingState.CheckCondition:
                SwitchPanelByState( ListingState.ShowFee );
                break;
            case ListingState.ShowFee:
                SwitchPanelByState( ListingState.Listing );
                break;
            default:
                break;
        }
    }

    protected void uxBack_Click( object sender, EventArgs e )
    {
        switch (CurrentState)
        {
            case ListingState.SelectTemplate:
                MainContext.RedirectMainControl( "ProductList.ascx" );
                break;
            case ListingState.CheckCondition:
                SwitchPanelByState( ListingState.SelectTemplate );
                break;
            case ListingState.ShowFee:
                SwitchPanelByState( ListingState.CheckCondition );
                break;
            default:
                break;
        }
    }

    protected void uxListing_Click( object sender, EventArgs e )
    {
        CreateList();
    }
}
