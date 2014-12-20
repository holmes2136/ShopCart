using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebUI.Ajax;
using Vevo.WebUI.Products;

public partial class Layouts_ManufacturerLists_ManufacturerListDefault : BaseManufacturerListControl
{
    private string CurrentManufacturerName
    {
        get
        {
            if ( Request.QueryString[ "ManufacturerName" ] == null )
                return String.Empty;
            else
                return Request.QueryString[ "ManufacturerName" ];
        }
    }

    private string CurrentManufacturerID
    {
        get
        {
            string id = Request.QueryString[ "ManufacturerID" ];
            if ( id != null )
            {
                return id;
            }
            else
            {
                return "0";
            }
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

    private void AddHistoryPoint()
    {
        GetScriptManager().AddHistoryPoint( "DeptPage", uxPagingControl.CurrentPage.ToString() );
        GetScriptManager().AddHistoryPoint( "DeptItemPerPage", uxItemsPerPageControl.SelectedValue );
    }

    private ScriptManager GetScriptManager()
    {
        return AjaxUtilities.GetScriptManager( this );
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

    private IList<Manufacturer> GetManufacturerList( int itemsPerPage, out int totalItems )
    {
        return DataAccessContext.ManufacturerRepository.GetAll(
            StoreContext.Culture,
            "ManufacturerID",
            BoolFilter.ShowTrue,
            ( uxPagingControl.CurrentPage - 1 ) * itemsPerPage,
            ( uxPagingControl.CurrentPage * itemsPerPage ) - 1,
            out totalItems );
    }

    private void PopulateManufacturerControls()
    {
        uxItemsPerPageControl.SelectValue( ItemPerPage );
        int totalItems;
        int selectedValue = ConvertUtilities.ToInt32( uxItemsPerPageControl.SelectedValue );
        uxList.DataSource = GetManufacturerList( selectedValue, out totalItems );
        uxList.DataBind();

        uxPagingControl.NumberOfPages = ( int ) Math.Ceiling( ( double ) totalItems / selectedValue );
    }

    private void ManufacturerList_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        Refresh();
    }

    private void RegisterStoreEvents()
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( ManufacturerList_StoreCultureChanged );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        uxItemsPerPageControl.BubbleEvent += new EventHandler( uxItemsPerPageControl_BubbleEvent );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterStoreEvents();
        AjaxUtilities.ScrollToTop( uxGoToTopLink );

        uxList.RepeatColumns = 2;
        uxList.RepeatDirection = RepeatDirection.Horizontal;

        Refresh();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        Refresh();
    }

    public void Refresh()
    {
        PopulateManufacturerControls();
    }
}