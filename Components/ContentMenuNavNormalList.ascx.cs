using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Data;
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;

public partial class Components_ContentMenuNavNormalList 
    : Vevo.WebUI.Contents.BaseContentMenuNavListUserControl
{
    private string _rootID = "0";
    private string _rootMenuName = "";

    private void PopulateControls()
    {
        IList<ContentMenuItem> originalList = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
            StoreContext.Culture, RootID, "SortOrder", BoolFilter.ShowTrue );
        IList<ContentMenuItem> list = ListUtilities<ContentMenuItem>.CopyListDeep( originalList );
        foreach (ContentMenuItem item in list)
        {
            if (!item.LinksToContent())
                item.Name = item.Name + "...";
        }

        if (_rootID == DataAccessContext.Configurations.GetValue( "TopContentMenu" ))
        {
            uxList.Visible = false;
            uxContentMenuListTop.Visible = true;
            uxContentMenuListTop.Items.Clear();
            
            if (DataAccessContext.Configurations.GetBoolValue( "RestrictAccessToShop" ) && !Page.User.Identity.IsAuthenticated)
            {
                return;
            }

            Culture culture = DataAccessContext.CultureRepository.GetOne( "1" );
            ContentMenuItem rootItem = DataAccessContext.ContentMenuItemRepository.GetOne(
                culture, ContentMenuItem.RootMenuItemID );

            MenuItem rootMenu = new MenuItem();
            rootMenu.Text = ContentMenuItem.RootMenuItemName;
            rootMenu.NavigateUrl = UrlManager.GetContentMenuUrl( rootItem.ContentMenuItemID,
                rootItem.UrlName );

            foreach (ContentMenuItem contentMenuItem in list)
            {
                rootMenu.ChildItems.Add( CreateMenuItemWithChildren( 0, contentMenuItem ) );
            }
            uxContentMenuListTop.Items.Add( rootMenu );
            uxContentMenuListTop.Orientation = Orientation.Horizontal;
        }
        else
        {
            uxContentMenuListTop.Visible = false;
            uxList.Visible = true;
            uxList.DataSource = list;
            uxList.DataBind();
        }
    }

 
    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    protected string GetNavName( object item )
    {
        return ((ContentMenuItem) item).Name;
    }

    protected string GetNavUrl( object item )
    {

        ContentMenuItem currentItem = DataAccessContext.ContentMenuItemRepository.GetOne(
            StoreContext.Culture, ((ContentMenuItem) item).ContentMenuItemID );

        if (!currentItem.LinksToContent())
            return UrlManager.GetContentMenuUrl( currentItem.ContentMenuItemID, currentItem.UrlName );
        else
        {
            Vevo.Domain.Contents.Content content = DataAccessContext.ContentRepository.GetOne(
                StoreContext.Culture, ((ContentMenuItem) item).ContentID.ToString() );
            return UrlManager.GetContentUrl( content.ContentID, content.UrlName );
        }

    }

    public void Refresh()
    {
        PopulateControls();
    }

    public string RootID
    {
        get
        {
            return _rootID;
        }
        set
        {
            _rootID = value;
        }
    }

    public string RootMenuName
    {
        get
        {
            return _rootMenuName;
        }
        set
        {
            _rootMenuName = value;
        }
    }
}
