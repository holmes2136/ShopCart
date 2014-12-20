using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.WebUI;
using Vevo.WebUI.Ajax;
using Vevo.WebUI.Products;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.Domain;

public partial class Layouts_PromotionLists_PromotionListDefault : BaseProductListControl
{
    private string _promotionItemPerPage = DataAccessContext.Configurations.GetIntValue( "BundlePromotionDisplay", StoreContext.CurrentStore ).ToString();

    private string SortField
    {
        get
        {
            if ( ViewState[ "SortField" ] == null )
            {
                if ( !IsSearchResult )
                    ViewState[ "SortField" ] = uxSortField.Items[ 0 ].Value.ToString();
                else
                    ViewState[ "SortField" ] = uxSortField.Items[ 1 ].Value.ToString();
            }

            return ( string ) ViewState[ "SortField" ];
        }

        set
        {
            ViewState[ "SortField" ] = value;
        }
    }

    private string SortType
    {
        get
        {
            if ( ViewState[ "SortType" ] == null )
                ViewState[ "SortType" ] = "ASC";

            return ( string ) ViewState[ "SortType" ];
        }
        set
        {
            ViewState[ "SortType" ] = value;
        }
    }

    private string ItemPerPage
    {
        get
        {
            if ( ViewState[ "ItemPerPage" ] == null )
                ViewState[ "ItemPerPage" ] = uxItemsPerPageControl.DefaultValue;

            return ( string ) ViewState[ "ItemPerPage" ];
        }
        set
        {
            ViewState[ "ItemPerPage" ] = value;
        }
    }

    private IList<PromotionGroup> GetPromotionGroupList( int itemsPerPage, string sortBy, out int totalItem )
    {
        return DataAccessContextDeluxe.PromotionGroupRepository.GetPromotionGroupList(
            StoreContext.Culture,
            sortBy,
            StoreContext.CurrentStore.StoreID,
            BoolFilter.ShowTrue,
            ( uxPagingControl.CurrentPage - 1 ) * itemsPerPage,
            ( uxPagingControl.CurrentPage * itemsPerPage ) - 1,
            out totalItem );
    }

    private void PopulatePromotionControls()
    {
        DisplaySortType();
        uxSortField.SelectedValue = SortField;
        uxItemsPerPageControl.SelectValue( ItemPerPage );

        int totalItems = 0;
        int itemPerPage = Convert.ToInt32( uxItemsPerPageControl.SelectedValue );
        uxList.DataSource = GetPromotionGroupList( itemPerPage, SortField + " " + SortType, out totalItems );
        uxList.DataBind();

        uxPagingControl.NumberOfPages = ( int ) System.Math.Ceiling( ( double ) totalItems / itemPerPage );
        PopulateText( totalItems );
    }

    private void PromotionGroupList_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        Refresh();
    }

    private void PromotionGroupList_StoreCurrencyChanged( object sender, CurrencyEventArgs e )
    {
        Refresh();
    }

    private void RegisterStoreEvents()
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( PromotionGroupList_StoreCultureChanged );
        GetStorefrontEvents().StoreCurrencyChanged +=
            new StorefrontEvents.CurrencyEventHandler( PromotionGroupList_StoreCurrencyChanged );
    }

    private ScriptManager GetScriptManager()
    {
        return ( ScriptManager ) Page.Master.FindControl( "uxScriptManager" );
    }

    private void AddHistoryPoint()
    {
        GetScriptManager().AddHistoryPoint( "page", uxPagingControl.CurrentPage.ToString() );
        GetScriptManager().AddHistoryPoint( "sortField", SortField );
        GetScriptManager().AddHistoryPoint( "sortType", SortType );
        GetScriptManager().AddHistoryPoint( "itemPerPage", uxItemsPerPageControl.SelectedValue );
    }

    protected void uxSortUpLink_Click( object sender, EventArgs e )
    {
        SortType = "DESC";
        AddHistoryPoint();
        Refresh();
    }

    protected void uxSortDownLink_Click( object sender, EventArgs e )
    {
        SortType = "ASC";
        AddHistoryPoint();
        Refresh();
    }

    protected void uxFieldSortDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        SortField = uxSortField.SelectedValue;
        uxSortValueHidden.Value = uxSortField.SelectedValue;
        AddHistoryPoint();
        Refresh();
    }

    protected void DisplaySortType()
    {
        if ( SortType == "ASC" )
        {
            uxSortUpLink.Visible = true;
            uxSortDownLink.Visible = false;
        }
        else
        {
            uxSortUpLink.Visible = false;
            uxSortDownLink.Visible = true;
        }
    }

    protected void uxPagingControl_BubbleEvent( object sender, EventArgs e )
    {
        AddHistoryPoint();
        Refresh();
    }

    protected void uxItemsPerPageControl_BubbleEvent( object sender, EventArgs e )
    {
        ItemPerPage = uxItemsPerPageControl.SelectedValue;
        uxPagingControl.CurrentPage = 1;
        AddHistoryPoint();
        Refresh();
    }

    protected void ScriptManager_Navigate( object sender, HistoryEventArgs e )
    {
        string args;

        if ( !string.IsNullOrEmpty( e.State[ "sortField" ] ) )
        {
            SortField = e.State[ "sortField" ].ToString();
        }
        else
        {
            SortField = uxSortField.Items[ 0 ].Value.ToString();
        }

        if ( !string.IsNullOrEmpty( e.State[ "sortType" ] ) )
        {
            SortType = e.State[ "sortType" ].ToString();
        }
        else
        {
            SortType = "ASC";
        }

        if ( !string.IsNullOrEmpty( e.State[ "itemPerPage" ] ) )
        {
            ItemPerPage = e.State[ "itemPerPage" ].ToString();
        }
        else
        {
            ItemPerPage = uxItemsPerPageControl.DefaultValue;
        }

        int totalItems = 50;
        int selectedValue;
        selectedValue = Convert.ToInt32( uxItemsPerPageControl.SelectedValue );

        uxPagingControl.NumberOfPages = ( int ) System.Math.Ceiling( ( double ) totalItems / selectedValue );

        if ( !string.IsNullOrEmpty( e.State[ "page" ] ) )
        {
            args = e.State[ "page" ];
            uxPagingControl.CurrentPage = int.Parse( args );
        }
        else
        {
            uxPagingControl.CurrentPage = 1;
        }

        Refresh();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if ( IsSearchResult )
        {
            if ( !IsPostBack )
            {
                uxSortField.SelectedIndex = 1;
            }
            uxSortField.Items[ 0 ].Enabled = false;
        }

        RegisterStoreEvents();

        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        uxItemsPerPageControl.BubbleEvent += new EventHandler( uxItemsPerPageControl_BubbleEvent );
        GetScriptManager().Navigate += new EventHandler<HistoryEventArgs>( ScriptManager_Navigate );
        AjaxUtilities.ScrollToTop( uxGoToTopLink );

            uxList.RepeatColumns = DataAccessContext.Configurations.GetIntValue( "BundlePromotionColumn" );
            uxPageControlTR.Visible = true;

        uxList.RepeatDirection = RepeatDirection.Horizontal;
        uxList.Visible = true;

        if ( !IsPostBack )
        {
            ItemPerPage = _promotionItemPerPage;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        Refresh();
        _promotionItemPerPage = ItemPerPage;
    }

    public void PopulateText( int totalItems )
    {
        if ( totalItems > 0 )
            uxMessageLabel.Text = "";
        else
        {
            uxMessageDiv.Visible = true;
            uxMessageLabel.Text = "<div style='text-align: center;'>[$PromotionListNoResults]</div>";
        }
    }

    public void Refresh()
    {
        PopulatePromotionControls();
    }
}
