using System;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class ContentPage : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    #region Private
    private string ContentID
    {
        get
        {
            if (!String.IsNullOrEmpty( Request.QueryString["ContentID"] ))
                return Request.QueryString["ContentID"];
            else
                return DataAccessContext.ContentRepository.GetContentIDFromUrlName( ContentName );
        }
    }

    private string ContentName
    {
        get
        {
            if (Request.QueryString["ContentName"] == null)
                return String.Empty;
            else
                return Request.QueryString["ContentName"];
        }
    }

    private void PopulateTitleAndMeta( DynamicPageElement element )
    {
        string title;
        string metaDescription;
        string metakeyword;

        Vevo.Domain.Contents.Content content = DataAccessContext.ContentRepository.GetOne( StoreContext.Culture, ContentID );

        title = content.MetaTitle;

        if (content.MetaDescription != "" && content.IsEnabled)
            metaDescription = content.MetaDescription;
        else
            metaDescription = NamedConfig.SiteDescription;

        if (content.MetaKeyword != "" && content.IsEnabled)
            metakeyword = content.MetaKeyword;
        else
            metakeyword = NamedConfig.SiteKeyword;

        element.SetUpTitleAndMetaTags( title, metaDescription, metakeyword );
    }

    private void Content_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        Vevo.Domain.Contents.Content content = DataAccessContext.ContentRepository.GetOne(
                    StoreContext.Culture, ContentID );

        string newURL = UrlManager.GetContentUrl( ContentID, content.UrlName );

        Response.Redirect( newURL );
    }
    #endregion

    #region Protected
    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( Content_StoreCultureChanged );

        uxContentLayout.ContentName = ContentName;
        uxContentLayout.ContentID = ContentID;
        DynamicPageElement element = new DynamicPageElement( this );
        PopulateTitleAndMeta( element );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }

    #endregion
}
