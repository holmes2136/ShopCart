using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.WebAppLib;


namespace Vevo
{

    /// <summary>
    /// Summary description for AdminAdvancedBaseListControl
    /// </summary>
    public abstract class AdminAdvancedBaseListControl : AdminAdvancedBaseUserControl
    {
        // Delegates
        public delegate void BrowseHistoryAddEventHandler( object sender, BrowseHistoryAddEventArgs e );


        private UrlQuery _browseHistoryQuery;

        private GridView _grid;
        private string _defaultSortExpression;

        private IPagingControl _pagingControl;
        private ISearchFilter _searchFilter;
        private ILanguageControl _languageControl;
        private ICategoryFilterDrop _categoryFilterDrop;
        private IStoreFilterDrop _storeFilterDrop;
        private ICurrencyControl _currencyFilterDrop;

        private List<IAdvancedPostbackControl> _postbackControls = new List<IAdvancedPostbackControl>();


        private void OnBrowseHistoryAdding( BrowseHistoryAddEventArgs e )
        {
            if (BrowseHistoryAdding != null)
            {
                BrowseHistoryAdding( this, e );
            }
        }


        protected event BrowseHistoryAddEventHandler BrowseHistoryAdding;

        protected abstract void RefreshGrid();


        protected GridViewHelper GridHelper
        {
            get
            {
                if (ViewState["GridHelper"] == null && _grid != null)
                {
                    GridViewHelper helper = new GridViewHelper( _grid, _defaultSortExpression );

                    if (MainContext.QueryString["Sort"] != null)
                    {
                        helper.SetFullSortText( MainContext.QueryString["Sort"] );
                    }

                    ViewState["GridHelper"] = helper;
                }

                return (GridViewHelper) ViewState["GridHelper"];
            }
        }


        protected void uxGrid_Sorting( object sender, GridViewSortEventArgs e )
        {
            GridHelper.SelectSorting( e.SortExpression );
            RefreshGrid();

            AddBrowseHistory();
        }

        protected virtual void uxLanguageControl_BubbleEvent( object sender, EventArgs e )
        {
            if (_categoryFilterDrop != null)
                _categoryFilterDrop.CultureID = _languageControl.CurrentCultureID;

            // Do not keep search filter while switching language
            if (_pagingControl != null)
                _pagingControl.CurrentPage = 1;

            if (_searchFilter != null)
                _searchFilter.ClearFilter();

            RefreshGrid();

            AddBrowseHistory();
        }

        protected virtual void uxSearchFilter_BubbleEvent( object sender, EventArgs e )
        {
            if (_pagingControl != null)
                _pagingControl.CurrentPage = 1;

            RefreshGrid();

            AddBrowseHistory();
        }

        protected virtual void uxPagingControl_BubbleEvent( object sender, EventArgs e )
        {
            RefreshGrid();

            AddBrowseHistory();
        }

        protected virtual void uxCategoryFilterDrop_BubbleEvent( object sender, EventArgs e )
        {
            if (_pagingControl != null)
                _pagingControl.CurrentPage = 1;

            RefreshGrid();

            AddBrowseHistory();
        }


        protected virtual void uxStoreFilterDrop_BubbleEvent( object sender, EventArgs e )
        {
            if (_pagingControl != null)
                _pagingControl.CurrentPage = 1;
            
            RefreshGrid();
            AddBrowseHistory();
        }

        protected virtual void uxCurrencyControl_BubbleEvent( object sender, EventArgs e )
        {
            if (_pagingControl != null)
                _pagingControl.CurrentPage = 1;

            RefreshGrid();
            AddBrowseHistory();
        }

        protected void AddBrowseHistory()
        {
            // First, begin with current MainContext query string and apply the query string from 
            // other controls (e.g. page number, search).
            _browseHistoryQuery.AddQuery( MainContext.QueryString );

            OnBrowseHistoryAdding( new BrowseHistoryAddEventArgs( _browseHistoryQuery ) );

            foreach (IAdvancedPostbackControl control in _postbackControls)
            {
                control.UpdateBrowseQuery( _browseHistoryQuery );
            }

            if (GridHelper != null)
                _browseHistoryQuery.AddQuery( "Sort", GridHelper.GetFullSortText() );

            MainContext.AddBrowseHistory( MainContext.LastControl, _browseHistoryQuery.RawQueryString );
        }

        protected void RegisterGridView( GridView grid, string defaultSortExpression )
        {
            _grid = grid;
            _defaultSortExpression = defaultSortExpression;
        }

        protected void RegisterLanguageControl( ILanguageControl languageControl )
        {
            _languageControl = languageControl;
            _postbackControls.Add( languageControl );
        }

        protected void RegisterSearchFilter( ISearchFilter searchFilter )
        {
            _searchFilter = searchFilter;
            _postbackControls.Add( searchFilter );
        }

        protected void RegisterPagingControl( IPagingControl pagingContorl )
        {
            _pagingControl = pagingContorl;
            _postbackControls.Add( pagingContorl );
        }

        protected void RegisterCategoryFilterDrop( ICategoryFilterDrop categoryFilterDrop )
        {
            _categoryFilterDrop = categoryFilterDrop;
            _postbackControls.Add( categoryFilterDrop );
        }

        protected void RegisterStoreFilterDrop( IStoreFilterDrop storeFilterDrop )
        {
            _storeFilterDrop = storeFilterDrop;
            _postbackControls.Add( storeFilterDrop );
        }

        protected void RegisterCurrencyFilterDrop( ICurrencyControl currencyFilterDrop )
        {
            _currencyFilterDrop = currencyFilterDrop;
            _postbackControls.Add( currencyFilterDrop );
        }

        protected void RegisterCustomControl( IAdvancedPostbackControl control )
        {
            _postbackControls.Add( control );
        }


        public AdminAdvancedBaseListControl()
        {
            _browseHistoryQuery = new UrlQuery();
        }

    }
}
