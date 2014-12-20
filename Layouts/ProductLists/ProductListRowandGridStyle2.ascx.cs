using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebUI.Ajax;
using Vevo.WebUI.Products;

public partial class Layouts_ProductLists_ProductListRowandGridStyle2 : BaseProductListControl
{
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

    private string ViewType
    {
        get
        {
            if ( ViewState[ "ViewType" ] == null )
                ViewState[ "ViewType" ] = "Grid";

            return ( string ) ViewState[ "ViewType" ];
        }
        set
        {
            ViewState[ "ViewType" ] = value;
        }
    }

    private int NoOfCategoryColumn
    {
        get
        {
            return DataAccessContext.Configurations.GetIntValue( uxProductListViewType.ProductColumnConfig );
        }
    }

    private IList<Product> GetProductList( int itemsPerPage, string sortBy, out int totalItems )
    {
        return DataRetriever(
            StoreContext.Culture,
            sortBy,
            ( uxPagingControl.CurrentPage - 1 ) * itemsPerPage,
            ( uxPagingControl.CurrentPage * itemsPerPage ) - 1,
            UserDefinedParameters,
            out totalItems );
    }

    private void PopulateProductControls()
    {
        if ( this.Visible )
            Refresh();
    }

    private void ProductList_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        Refresh();
    }

    private void ProductList_StoreCurrencyChanged( object sender, CurrencyEventArgs e )
    {
        Refresh();
    }

    private void RegisterStoreEvents()
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( ProductList_StoreCultureChanged );
        GetStorefrontEvents().StoreCurrencyChanged +=
            new StorefrontEvents.CurrencyEventHandler( ProductList_StoreCurrencyChanged );
    }

    private ScriptManager GetScriptManager()
    {
        return AjaxUtilities.GetScriptManager( this );
    }

    private void AddHistoryPoint()
    {
        GetScriptManager().AddHistoryPoint( "page", uxPagingControl.CurrentPage.ToString() );
        GetScriptManager().AddHistoryPoint( "sortField", SortField );
        GetScriptManager().AddHistoryPoint( "sortType", SortType );
        GetScriptManager().AddHistoryPoint( "itemPerPage", uxItemsPerPageControl.SelectedValue );
        GetScriptManager().AddHistoryPoint( "viewType", uxProductListViewType.SelectedView );
    }

    private string CurrentCategoryName
    {
        get
        {
            if (Request.QueryString["CategoryName"] == null)
                return String.Empty;
            else
                return Request.QueryString["CategoryName"];
        }
    }

    private string GetCategoryDescription()
    {
        return DataAccessContext.CategoryRepository.GetOne(
                StoreContext.Culture,
                CurrentCategoryName).Description;
    }

    private void PopulateCategoryControls()
    {
        uxCategoryDescriptionText.Text = GetCategoryDescription();
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

    protected void uxItemsPerPageControl_BubbleEvent( object sender, EventArgs e )
    {
        ItemPerPage = uxItemsPerPageControl.SelectedValue;
        uxPagingControl.CurrentPage = 1;
        AddHistoryPoint();
        Refresh();
    }

    protected void uxPagingControl_BubbleEvent( object sender, EventArgs e )
    {
        AddHistoryPoint();
        Refresh();
    }

    protected void uxProductListViewType_BubbleEvent( object sender, EventArgs e )
    {
        ViewType = uxProductListViewType.SelectedView;
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

    protected void ScriptManager_Navigate( object sender, HistoryEventArgs e )
    {
        if ( IsSearchResult )
            return;

        string args;

        if ( !string.IsNullOrEmpty( e.State[ "sortField" ] ) )
        {
            SortField = e.State[ "sortField" ].ToString();
        }
        else
        {
            SortField = uxSortField.Items[ uxSortField.SelectedIndex ].Value.ToString();
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

        if ( !string.IsNullOrEmpty( e.State[ "viewType" ] ) )
        {
            ViewType = e.State[ "viewType" ].ToString();
        }
        else
        {
            ViewType = "Grid";
        }

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
        PopulateCategoryControls();
        if ( IsSearchResult && !IsPostBack )
        {
            uxSortField.SelectedIndex = 1;
            uxSortField.Items[ 0 ].Enabled = false;
        }

        RegisterStoreEvents();

        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        uxItemsPerPageControl.BubbleEvent += new EventHandler( uxItemsPerPageControl_BubbleEvent );
        GetScriptManager().Navigate += new EventHandler<HistoryEventArgs>( ScriptManager_Navigate );
        uxProductListViewType.BubbleEvent += new EventHandler( uxProductListViewType_BubbleEvent );
        AjaxUtilities.ScrollToTop( uxGoToTopLink );

        uxPageControlTR.Visible = true;

        uxList.Visible = true;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if ( this.Visible )
        {
            if ( !IsPostBack )
            {
                if ( ViewState[ "UserDefinedParameters" ] != null )
                {
                    PopulateProductControls();
                }
                else
                {
                    DisplaySortType();
                    uxSortField.SelectedValue = SortField;
                    uxItemsPerPageControl.SelectValue( ItemPerPage );
                    DisplayViewType();
                }
            }
        }

        CatalogUtilities.ProductItemsPerPage = ItemPerPage;
        CatalogUtilities.CatalogSortField = SortField;
        CatalogUtilities.ProductListView = ViewType;
    }

    protected void uxList_ItemDataBound( object sender, DataListItemEventArgs e )
    {
        Components_ProductListItem listItem = ( Components_ProductListItem ) e.Item.FindControl( "uxItem" );
        if ( listItem != null )
            listItem.Refresh();
    }

    protected void DisplayViewType()
    {
        if ( uxProductListViewType.SelectedView == "Grid" )
        {
            uxDataListPanel.Visible = false;
            uxGridViewPanel.Visible = true;
            uxTableViewPanel.Visible = false;
        }
        else if ( uxProductListViewType.SelectedView == "List" )
        {
            uxDataListPanel.Visible = true;
            uxGridViewPanel.Visible = false;
            uxTableViewPanel.Visible = false;
        }
        else
        {
            uxDataListPanel.Visible = false;
            uxGridViewPanel.Visible = false;
            uxTableViewPanel.Visible = true;
        }
    }

    public void PopulateText( int totalItems, int totalItemsPerPage )
    {
        if ( totalItems > 0 )
        {
            uxMessageLabel.Text = "";
            int itemPerPage = ConvertUtilities.ToInt32( ItemPerPage );
            if ( itemPerPage > totalItems )
                itemPerPage = totalItems;

            uxItemCounLabel.Text = String.Format( "{0} {1} {2}", totalItemsPerPage.ToString(), GetLanguageText( "items" ), totalItems.ToString() );
        }
        else
        {
            uxMessageDiv.Visible = true;
            uxMessageLabel.Text = "<div style='text-align: center;'>" + GetLanguageText( "ProductListNoResults" ) + "</div>";
            uxItemCounLabel.Text = String.Empty;
        }
    }

    public void Refresh()
    {
        DisplaySortType();
        uxSortField.SelectedValue = SortField;
        uxItemsPerPageControl.SelectValue( ItemPerPage );
        uxProductListViewType.SelectedView = ViewType;
        uxProductListViewType.SetViewTypeText( ViewType );
        DisplayViewType();

        int totalItems;
        int selectedValue;
        int totalItemsPerPage;
        selectedValue = Convert.ToInt32( uxItemsPerPageControl.SelectedValue );
        if ( ViewType == "List" )
        {
            uxList.DataSource = GetProductList( selectedValue, SortField + " " + SortType, out totalItems);
            uxList.DataBind();
            totalItemsPerPage = uxList.Items.Count;
        }
        else if ( ViewType == "Grid" )
        {
            uxList2.DataSource = GetProductList( selectedValue, SortField + " " + SortType, out totalItems );
            uxList2.DataBind();
            totalItemsPerPage = uxList2.Items.Count;
        }
        else
        {
            uxTableList.DataSource = GetProductList( selectedValue, SortField + " " + SortType, out totalItems );
            uxTableList.DataBind();
            totalItemsPerPage = uxTableList.Items.Count;

            if ( totalItemsPerPage == 0 )
                uxTableViewPanel.Visible = false;
        }

        uxPagingControl.NumberOfPages = ( int ) System.Math.Ceiling( ( double ) totalItems / selectedValue );
        PopulateText( totalItems, totalItemsPerPage );
    }
}
