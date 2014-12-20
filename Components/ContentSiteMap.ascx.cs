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
using System.Collections.Generic;
using Vevo;
using Vevo.Domain.Contents;
using Vevo.WebUI.International;
using Vevo.WebUI;
using Vevo.Domain.Stores;
public partial class Components_ContentSiteMap : Vevo.WebUI.International.BaseLanguageUserControl
{

    IList<ContentMenuItem> _contentMenuList;
    ArrayList shownContentList;
    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( Components_ContentSiteMap_StoreCultureChanged );
        PopulateContentMenu();
    }

    private void Components_ContentSiteMap_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        PopulateContentMenu();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }

    private void PopulateContentMenu()
    {
        GetContentMenu();

        uxContentMenuDataList.DataSource = CreateLeafContentMenu( _contentMenuList );
        uxContentMenuDataList.DataBind();
    }

    private void GetContentMenu()
    {
        shownContentList = new ArrayList();
        StoreRetriever storeRetriever = new StoreRetriever();
        _contentMenuList = DataAccessContext.ContentMenuItemRepository.GetByStoreID(
            StoreContext.Culture, storeRetriever.GetCurrentStoreID(), "ContentMenuItemID", BoolFilter.ShowTrue );
    }

    private IList<ContentMenuItem> CreateLeafContentMenu( IList<ContentMenuItem> list )
    {
        IList<ContentMenuItem> _leafContentMenuList = new List<ContentMenuItem>();
        foreach (ContentMenuItem item in list)
        {
            if (!item.LinksToContent())
            {
                IList<ContentMenuItem> item_list = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
                    StoreContext.Culture, item.ReferringMenuID, "SortOrder", BoolFilter.ShowTrue, BoolFilter.ShowTrue );

                if (ContainContent( item_list ))
                    _leafContentMenuList.Add( item );
            }
            else if (item.LinksToContent() && ContentMenu.IsRootMenuID( item.ContentMenuID ))
            {
                ContentMenuItem parentItem = DataAccessContext.ContentMenuItemRepository.GetOne(
                  StoreContext.Culture, item.ContentMenuID );

                Vevo.Domain.Contents.Content content = DataAccessContext.ContentRepository.GetOne(
                    StoreContext.Culture, item.ContentID );
                if (content.IsShowInSiteMap)
                {
                    if (content.IsEnabled)
                    {
                        _leafContentMenuList.Add( item );
                        shownContentList.Add( item.ContentID );
                    }
                }

            }
        }

        return _leafContentMenuList;
    }

    protected void uxContentMenuDataList_ItemDataBound( object sender, DataListItemEventArgs e )
    {
        GenerateBreadcrumb( e.Item );
    }

    private void GenerateBreadcrumb( DataListItem item )
    {
        Panel panel = (Panel) item.FindControl( "uxBreadcrumbPanel" );
        string name = DataBinder.Eval( item.DataItem, "Name" ).ToString();
        string contentMenuItemID = DataBinder.Eval( item.DataItem, "ContentMenuItemID" ).ToString();
        string urlName = DataBinder.Eval( item.DataItem, "UrlName" ).ToString();
        string parentContentMenuID = DataBinder.Eval( item.DataItem, "ContentMenuID" ).ToString();

        HyperLink link = new HyperLink();
        if (contentMenuItemID != "0")
        {
            ContentMenuItem currntItem = DataAccessContext.ContentMenuItemRepository.GetOne(
               StoreContext.Culture, contentMenuItemID );
            IList<ContentMenuItem> parentList = new List<ContentMenuItem>();

            if (currntItem.MenuPosition == ContentMenuItem.MenuPositionType.Root)
            {
                parentList.Add( currntItem );
                link = new HyperLink();
                link.Text = parentList[0].Name;
                //link.Text = "Information";
                link.CssClass = "SiteMapParent";
                panel.Controls.Add( link );

                if (!currntItem.LinksToContent())
                {
                    link.NavigateUrl = UrlManager.GetContentMenuUrl( contentMenuItemID, urlName );
                }
                else
                {
                    Vevo.Domain.Contents.Content content = DataAccessContext.ContentRepository.GetOne(
                        StoreContext.Culture, currntItem.ContentID.ToString() );
                    link.NavigateUrl = UrlManager.GetContentUrl( content.ContentID, content.UrlName );
                }
            }
            else
            {
                parentList = currntItem.GetParentMenuItemList( StoreContext.Culture );
                link = new HyperLink();
                link.Text = parentList[0].Name;
                link.CssClass = "SiteMapParent";


                if (!currntItem.LinksToContent())
                {
                    link.NavigateUrl = UrlManager.GetContentMenuUrl( contentMenuItemID, urlName );
                }
                else
                {
                    Vevo.Domain.Contents.Content content = DataAccessContext.ContentRepository.GetOne(
                        StoreContext.Culture, currntItem.ContentID.ToString() );
                    link.NavigateUrl = UrlManager.GetContentUrl( content.ContentID, content.UrlName );
                }

                panel.Controls.Add( link );

                for (int i = 1; i < parentList.Count; i++)
                {
                    Label label = new Label();
                    label.Text = " >> ";
                    label.CssClass = "SiteMapSeparate";
                    panel.Controls.Add( label );

                    link = new HyperLink();
                    link.Text = parentList[i].Name;
                    link.CssClass = "SiteMapParent";
                    link.NavigateUrl = UrlManager.GetContentMenuUrl(
                        parentList[i].ContentMenuItemID, parentList[i].UrlName );

                    panel.Controls.Add( link );
                }

            }
        }
        else
        {
            link = new HyperLink();
            link.Text = name;
            link.CssClass = "SiteMapParent";
            panel.Controls.Add( link );
        }
        GenerateContent( item, contentMenuItemID );
    }

    private void GenerateContent( DataListItem item, string contentMenuItemID )
    {
        Repeater listcontent = (Repeater) item.FindControl( "uxContentItemRepeater" );
        IList<Vevo.Domain.Contents.Content> contentList = new List<Vevo.Domain.Contents.Content>();

        if (contentMenuItemID != "0")
        {
            ContentMenuItem contentMenuItem = DataAccessContext.ContentMenuItemRepository.GetOne(
                StoreContext.Culture, contentMenuItemID );
            IList<ContentMenuItem> list = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
                StoreContext.Culture, contentMenuItem.ReferringMenuID, "SortOrder", BoolFilter.ShowTrue, BoolFilter.ShowTrue );


            foreach (ContentMenuItem contentmenuitem in list)
            {
                if (contentmenuitem.ReferringMenuID == "0")
                {
                    Vevo.Domain.Contents.Content content = DataAccessContext.ContentRepository.GetOne(
                        StoreContext.Culture, contentmenuitem.ContentID );
                    if (content.IsShowInSiteMap)
                    {
                        contentList.Add( content );
                        shownContentList.Add( content.ContentID );
                    }
                }
            }
        }

        listcontent.DataSource = contentList;
        listcontent.DataBind();
    }

    private bool ContainContent( IList<ContentMenuItem> list )
    {
        foreach (ContentMenuItem item in list)
        {
            if (item.ContentID != "0")
                return true;
        }
        return false;
    }

    private ContentMenuItem GetReferringContentMenuItem( string id )
    {
        IList<ContentMenuItem> list = DataAccessContext.ContentMenuItemRepository.GetAll(
            StoreContext.Culture, BoolFilter.ShowTrue );

        foreach (ContentMenuItem item in list)
        {
            if (item.ReferringMenuID == id)
                return item;
        }
        return null;
    }

    public bool HaveData()
    {
        if (uxContentMenuDataList.Items.Count == 0)
            return false;
        else
            return true;
    }
}
