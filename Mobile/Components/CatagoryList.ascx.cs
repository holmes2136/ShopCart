using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Domain.Products;
using Vevo.Domain;
using Vevo.Domain.Stores;
using Vevo.WebUI;

public partial class Mobile_Components_CatagoryList : System.Web.UI.UserControl
{
    private static int ItemPerPage = 10;

    private string CurrentCategoryName
    {
        get
        {
            if (Request.QueryString["CategoryName"] == null)
                return String.Empty;
            else
                return Request.QueryString["CategoryName"].Split( ',' )[0];
        }
    }

    private string CurrentCategoryID
    {
        get
        {
            string id = Request.QueryString["CategoryID"];
            if (id != null)
            {
                return id;
            }
            else
            {
                return DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() );
            }
        }
    }

    private IList<Category> GetCategoryList( int itemsPerPage, out int totalItems )
    {
        if (!String.IsNullOrEmpty( CurrentCategoryName ))
        {
            return DataAccessContext.CategoryRepository.GetByParentUrlName(
                StoreContext.Culture,
                CurrentCategoryName,
                "SortOrder",
                BoolFilter.ShowTrue,
                (uxMobilePagingControl.CurrentPage - 1) * itemsPerPage,
                (uxMobilePagingControl.CurrentPage * itemsPerPage) - 1,
                out totalItems );
        }
        else
        {
            return DataAccessContext.CategoryRepository.GetByParentID(
                StoreContext.Culture,
                CurrentCategoryID,
                "SortOrder",
                BoolFilter.ShowTrue,
                (uxMobilePagingControl.CurrentPage - 1) * itemsPerPage,
                (uxMobilePagingControl.CurrentPage * itemsPerPage) - 1,
                out totalItems );
        }
    }

    private void PopulateCategoryControls()
    {

        int totalItems;

        uxMobileList.DataSource = GetCategoryList( ItemPerPage, out totalItems );
        uxMobileList.DataBind();

        uxMobilePagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / ItemPerPage );
    }

    private ScriptManager GetScriptManager()
    {
        return (ScriptManager) Page.Master.FindControl( "uxScriptManager" );
    }

    private void AddHistoryPoint()
    {
        GetScriptManager().AddHistoryPoint( "page", uxMobilePagingControl.CurrentPage.ToString() );
    }

    protected string GetCatUrl( object categoryID, object urlName )
    {
        return Vevo.UrlManager.GetMobileCategoryUrl( categoryID, urlName );
    }

    protected void uxMobilePagingControl_BubbleEvent( object sender, EventArgs e )
    {
        AddHistoryPoint();
        Refresh();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxMobilePagingControl.BubbleEvent += new EventHandler( uxMobilePagingControl_BubbleEvent );
        GetScriptManager().Navigate += new EventHandler<HistoryEventArgs>( ScriptManager_Navigate );
    }

    protected void ScriptManager_Navigate( object sender, HistoryEventArgs e )
    {
        string args;

        int totalItems;
        GetCategoryList( ItemPerPage, out totalItems );

        uxMobilePagingControl.NumberOfPages = (int) System.Math.Ceiling( (double) totalItems / ItemPerPage );

        if (!string.IsNullOrEmpty( e.State["page"] ))
        {
            args = e.State["page"];
            uxMobilePagingControl.CurrentPage = int.Parse( args );
        }
        else
        {
            uxMobilePagingControl.CurrentPage = 1;
        }

        Refresh();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        Refresh();
    }

    protected void uxMobileList_OnItemDataBound( object sender, DataListItemEventArgs e )
    {
        int totalItems;
        if (e.Item.ItemIndex == GetCategoryList( ItemPerPage, out totalItems ).Count - 1)
        {
            e.Item.BorderStyle = BorderStyle.None;
            e.Item.CssClass = "MobileMenu MobileMenuBottom";
        }
        if (e.Item.ItemIndex == 0)
        {
            e.Item.CssClass = "MobileMenu MobileMenuTop";
        }
    }

    public void Refresh()
    {
        PopulateCategoryControls();
    }
}
