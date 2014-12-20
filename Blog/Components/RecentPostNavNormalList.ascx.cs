using System;
using System.Collections.Generic;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Blogs;
using Vevo.Domain.Stores;
using Vevo.WebUI.International;

public partial class Blog_Components_RecentPostNavNormalList : BaseLanguageUserControl
{
    #region Private
    private string _displayRecentPostListItem = "5";
    private int _countRecentPostItem = 0;
    private IList<Blog> CreateDataSource()
    {
        string storeID = new StoreRetriever().GetCurrentStoreID();
        IList<Blog> list = DataAccessContext.BlogRepository.GetRecentPost( storeID, _displayRecentPostListItem );

        return list;
    }

    private void PopulateControls()
    {
        uxList.DataSource = CreateDataSource();
        uxList.DataBind();

        _countRecentPostItem = uxList.Items.Count;
    }
    #endregion

    #region Protected
    protected void Page_Load( object sender, EventArgs e )
    {
        if ( !IsPostBack )
        {
            PopulateControls();
        }
    }

    protected string GetTextName( object item )
    {
        Blog blog = ( Blog ) item;
        string title = blog.BlogTitle;

        if ( title.Length > 25 )
        {
            title = title.Remove( 25 ) + "...";
        }

        return title;
    }

    protected string GetNavURL( object item )
    {
        Blog blog = ( Blog ) item;

        return UrlManager.GetBlogUrl( blog.BlogID, blog.UrlName );
    }

    #endregion

    #region Public
    public void Refresh()
    {
        PopulateControls();
    }

    public bool HasRecentPostItem()
    {
        if (_countRecentPostItem == 0)
            return false;
        else
            return true;
    }
    #endregion
}
