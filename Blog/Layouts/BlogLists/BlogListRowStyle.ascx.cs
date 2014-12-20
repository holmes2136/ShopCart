using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Base.Domain;
using Vevo.Domain;
using Vevo.Domain.Blogs;
using Vevo.WebUI;
using Vevo.WebUI.Products;

public partial class Layouts_BlogLists_BlogListRowStyle : BaseProductListControl
{
    private const string _sortBy = "CreateDate DESC, BlogID DESC";
    private string _blogItemPerPage = DataAccessContext.Configurations.GetValue( "BlogListItemsPerPage", StoreContext.CurrentStore );
    private bool _topPagingClick = true;

    private string ItemPerPage
    {
        get
        {
            if (ViewState["ItemPerPage"] == null)
                ViewState["ItemPerPage"] = uxItemsPerPageControl.DefaultValue;

            return (string) ViewState["ItemPerPage"];
        }
        set
        {
            ViewState["ItemPerPage"] = value;
        }
    }

    private string CurrentBlogListName
    {
        get
        {
            if (String.IsNullOrEmpty( Request.QueryString["BlogListName"] ))
            {
                return String.Empty;
            }
            else
            {
                return Request.QueryString["BlogListName"];
            }
        }
    }

    private string CurrentSearch
    {
        get
        {
            if (Request.QueryString["Search"] == null)
                return String.Empty;
            else
                return Request.QueryString["Search"];
        }
    }

    private bool IsBlogSearch
    {
        get
        {
            if (Request.QueryString["Search"] == null)
                return false;
            else
                return true;
        }
    }

    private string CurrentCategory
    {
        get
        {
            if (Request.QueryString["CategoryName"] == null)
                return String.Empty;
            else
                return Request.QueryString["CategoryName"].ToString();
        }
    }

    private IList<Blog> GetBlogList( int itemsPerPage, string sortBy, out int totalItem )
    {
        int currentPage = 1;

        if (_topPagingClick)
            currentPage = uxPagingControl.CurrentPage;
        else
            currentPage = uxPagingControlBottom.CurrentPage;

        if (!String.IsNullOrEmpty( CurrentBlogListName ))
        {
            string[] date = CurrentBlogListName.Split( '_' );
            return DataAccessContext.BlogRepository.GetBlogListByDate(
                date[0],
                date[1],
                (currentPage - 1) * itemsPerPage,
                (currentPage * itemsPerPage) - 1,
                out totalItem,
                StoreContext.CurrentStore.StoreID );
        }
        else if (IsBlogSearch)
        {
            if (!String.IsNullOrEmpty( CurrentSearch ))
            {
                return DataAccessContext.BlogRepository.QuickSearch(
                    CurrentSearch,
                    new string[] { "BlogTitle", "ShortContent", "BlogContent", "Tags" },
                    (currentPage - 1) * itemsPerPage,
                    (currentPage * itemsPerPage) - 1,
                    out totalItem,
                    StoreContext.CurrentStore.StoreID );
            }
            else
            {
                totalItem = 0;
                return null;
            }
        }
        else if (!String.IsNullOrEmpty( CurrentCategory ))
        {
            string categoryID = DataAccessContext.BlogCategoryRepository.GetBlogCategoryIDFromBlogCategoryURL( CurrentCategory );
            return DataAccessContext.BlogRepository.GetBlogListByCategoryID(
                sortBy,
                categoryID,
                (currentPage - 1) * itemsPerPage,
                (currentPage * itemsPerPage) - 1,
                out totalItem,
                StoreContext.CurrentStore.StoreID,
                BoolFilter.ShowTrue );
        }
        else
        {
            return DataAccessContext.BlogRepository.SearchBlog(
                sortBy,
                new SearchFilter(),
                (currentPage - 1) * itemsPerPage,
                (currentPage * itemsPerPage) - 1,
                out totalItem,
                StoreContext.CurrentStore.StoreID,
                false,
                BoolFilter.ShowTrue );
        }
    }

    private void PopulateProductControls()
    {
        if (this.Visible)
            Refresh();
    }

    private void BlogList_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        Refresh();
    }

    private void BlogList_StoreCurrencyChanged( object sender, CurrencyEventArgs e )
    {
        Refresh();
    }

    private void RegisterStoreEvents()
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( BlogList_StoreCultureChanged );
        GetStorefrontEvents().StoreCurrencyChanged +=
            new StorefrontEvents.CurrencyEventHandler( BlogList_StoreCurrencyChanged );
    }

    private ScriptManager GetScriptManager()
    {
        return (ScriptManager) Page.Master.FindControl( "uxScriptManager" );
    }

    private void AddHistoryPoint()
    {
        GetScriptManager().AddHistoryPoint( "page", uxPagingControl.CurrentPage.ToString() );
        GetScriptManager().AddHistoryPoint( "itemPerPage", uxItemsPerPageControl.SelectedValue );
    }

    private void AddHistoryPointBottom()
    {
        GetScriptManager().AddHistoryPoint( "page", uxPagingControlBottom.CurrentPage.ToString() );
        GetScriptManager().AddHistoryPoint( "itemPerPage", uxItemsPerPageControlBottom.SelectedValue );

    }

    protected void uxItemsPerPageControl_BubbleEvent( object sender, EventArgs e )
    {
        ItemPerPage = uxItemsPerPageControl.SelectedValue;
        AddHistoryPoint();
        Refresh();
    }

    protected void uxItemsPerPageControlBottom_BubbleEvent( object sender, EventArgs e )
    {
        ItemPerPage = uxItemsPerPageControlBottom.SelectedValue;
        AddHistoryPointBottom();
        Refresh();
    }

    protected void uxPagingControl_BubbleEvent( object sender, EventArgs e )
    {
        _topPagingClick = true;
        uxPagingControlBottom.CurrentPage = uxPagingControl.CurrentPage;

        AddHistoryPoint();
        Refresh();
    }

    protected void uxPagingControlBottom_BubbleEvent( object sender, EventArgs e )
    {
        _topPagingClick = false;
        uxPagingControl.CurrentPage = uxPagingControlBottom.CurrentPage;

        AddHistoryPoint();
        Refresh();
    }

    protected void ScriptManager_Navigate( object sender, HistoryEventArgs e )
    {
        string args;

        if (!string.IsNullOrEmpty( e.State["itemPerPage"] ))
        {
            ItemPerPage = e.State["itemPerPage"].ToString();

        }
        else
        {
            ItemPerPage = uxItemsPerPageControl.DefaultValue;

        }

        int totalItems = 50;
        int selectedValue;
        selectedValue = Convert.ToInt32( uxItemsPerPageControl.SelectedValue );

        uxPagingControl.NumberOfPages = (int) System.Math.Ceiling( (double) totalItems / selectedValue );
        uxPagingControlBottom.NumberOfPages = (int) System.Math.Ceiling( (double) totalItems / selectedValue );

        if (!string.IsNullOrEmpty( e.State["page"] ))
        {
            args = e.State["page"];
            uxPagingControl.CurrentPage = int.Parse( args );
            uxPagingControlBottom.CurrentPage = int.Parse( args );
        }
        else
        {
            uxPagingControl.CurrentPage = 1;
            uxPagingControlBottom.CurrentPage = 1;
        }

        Refresh();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterStoreEvents();

        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        uxItemsPerPageControl.BubbleEvent += new EventHandler( uxItemsPerPageControl_BubbleEvent );
        uxPagingControlBottom.BubbleEvent += new EventHandler( uxPagingControlBottom_BubbleEvent );
        uxItemsPerPageControlBottom.BubbleEvent += new EventHandler( uxItemsPerPageControlBottom_BubbleEvent );
        GetScriptManager().Navigate += new EventHandler<HistoryEventArgs>( ScriptManager_Navigate );

        uxList.RepeatColumns = 1;
        uxPageControlTR.Visible = true;
        uxPageControlTRBottom.Visible = true;

        if (!IsPostBack)
        {
            uxItemsPerPageControl.SelectValue( _blogItemPerPage.ToString() );
            uxItemsPerPageControlBottom.SelectValue( _blogItemPerPage.ToString() );
            uxBlogListNoDataDiv.Visible = false;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (this.Visible)
        {
            if (ViewState["UserDefinedParameters"] != null)
            {
                PopulateProductControls();
            }
            else
            {
                uxItemsPerPageControl.SelectValue( ItemPerPage );
                uxItemsPerPageControlBottom.SelectValue( ItemPerPage );
            }

            PopulateProductControls();
        }
    }

    protected void uxList_ItemDataBound( object sender, DataListItemEventArgs e )
    {
    }

    public void PopulateText( int totalItems )
    {
        if (totalItems > 0)
        {
            uxBlogListNoDataDiv.Visible = false;
            uxMessageLabel.Text = "";
        }
        else
        {
            uxBlogListNoDataDiv.Visible = true;
            uxPageControlTR.Visible = false;
            uxPageControlTRBottom.Visible = false;
            uxMessageLabel.Text = "<div style='text-align: center;'>[$BlogListNoResults]</div>";
        }
    }

    public void Refresh()
    {
        uxItemsPerPageControl.SelectValue( ItemPerPage );
        uxItemsPerPageControlBottom.SelectValue( ItemPerPage );

        int totalItems = 0;
        int itemPerPage = Convert.ToInt32( uxItemsPerPageControl.SelectedValue );

        uxList.DataSource = GetBlogList( itemPerPage, _sortBy, out totalItems );
        uxList.DataBind();

        uxPagingControl.NumberOfPages = (int) System.Math.Ceiling( (double) totalItems / itemPerPage );
        uxPagingControlBottom.NumberOfPages = (int) System.Math.Ceiling( (double) totalItems / itemPerPage );
        PopulateText( totalItems );
    }
}
