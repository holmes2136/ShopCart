using System;
using System.Collections;
using System.Collections.Generic;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class ContentMenuItem : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    #region Private
    private string ContentMenuItemID
    {
        get
        {
            if (!String.IsNullOrEmpty( Request.QueryString["ContentMenuItemID"] ))
                return Request.QueryString["ContentMenuItemID"];
            else
            {
                return DataAccessContext.ContentMenuItemRepository.GetContentMenuItemIDFromUrlName( ContentMenuItemName );
            }
        }
    }

    private string ContentMenuItemName
    {
        get
        {
            if (Request.QueryString["ContentMenuItemName"] == null)
                return String.Empty;
            else
                return Request.QueryString["ContentMenuItemName"];
        }
    }

    private void PopulateGrid()
    {
        Vevo.Domain.Contents.ContentMenuItem currentItem = DataAccessContext.ContentMenuItemRepository.GetOne(
            StoreContext.Culture, ContentMenuItemID );

        IList<Vevo.Domain.Contents.ContentMenuItem> list = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
            StoreContext.Culture, currentItem.ReferringMenuID, "SortOrder", BoolFilter.ShowTrue );
        if (list.Count > 0)
        {
            uxContentListGrid.DataSource = list;
            uxContentListGrid.DataBind();
        }
        else
        {
            uxLabel.Text = Resources.ContentMenuItemMessages.ContentMenuEmpty;
            uxLabel.Visible = true;
        }
    }

    private void PopulateTitle()
    {
        Vevo.Domain.Contents.ContentMenuItem currentItem = DataAccessContext.ContentMenuItemRepository.GetOne(
           StoreContext.Culture, ContentMenuItemID );
        DynamicPageElement element = new DynamicPageElement( this );
        switch (currentItem.Name)
        {
            case "Top":
                uxDefaultTitle.Text = "[$TopDefaultTitle]";
                element.SetUpTitleAndMetaTags( "[$TopTitle]", currentItem.Description, NamedConfig.SiteKeyword );
                break;
            case "Left":
                uxDefaultTitle.Text = "[$LeftDefaultTitle]";
                element.SetUpTitleAndMetaTags( "[$LeftTitle]", currentItem.Description, NamedConfig.SiteKeyword );
                break;
            case "Right":
                uxDefaultTitle.Text = "[$RightDefaultTitle]";
                element.SetUpTitleAndMetaTags( "[$RightTitle]", currentItem.Description, NamedConfig.SiteKeyword );
                break;
            default:
                uxDefaultTitle.Text = currentItem.Name;
                element.SetUpTitleAndMetaTags( "[$Title]", currentItem.Description, NamedConfig.SiteKeyword );
                break;
        }
    }

    private void ContentMenuItem_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        Culture culture = DataAccessContext.CultureRepository.GetOne( "1" );
        Vevo.Domain.Contents.ContentMenuItem contentMenuItem = DataAccessContext.ContentMenuItemRepository.GetOne(
            culture, ContentMenuItemID );

        string newURL = UrlManager.GetContentMenuUrl( ContentMenuItemID, contentMenuItem.UrlName );

        Response.Redirect( newURL );
    }
    #endregion

    #region Protected
    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( ContentMenuItem_StoreCultureChanged );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateTitle();
        PopulateGrid();
    }

    protected string GetImage( string id )
    {
        Vevo.Domain.Contents.ContentMenuItem item = DataAccessContext.ContentMenuItemRepository.GetOne(
            StoreContext.Culture, id );
        if (item.ReferringMenuID == "0")
            return "SmallContent";
        else
            return "SmallCategory";
    }

    protected string GetLink( string id )
    {
        Vevo.Domain.Contents.ContentMenuItem item = DataAccessContext.ContentMenuItemRepository.GetOne(
            StoreContext.Culture, id );
        if (item.ReferringMenuID != "0")
            return UrlManager.GetContentMenuUrl( item.ContentMenuItemID, item.UrlName );
        else
        {
            Vevo.Domain.Contents.Content contentitem = DataAccessContext.ContentRepository.GetOne(
                StoreContext.Culture, item.ContentID );
            return UrlManager.GetContentUrl( contentitem.ContentID, contentitem.UrlName );
        }
    }
    #endregion
}
