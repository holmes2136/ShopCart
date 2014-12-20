using System;
using System.IO;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.WebUI;
using Vevo.WebUI.Ajax;
using Vevo.WebUI.International;

public partial class NewsDetails : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private DynamicPageElement element;
    public string CurrentID
    {
        get
        {
            int id = 0;
            string newsInput = Request.QueryString[ "NewsID" ];
            if ( newsInput != null && int.TryParse( newsInput, out id ) )
            {
                if ( id > 0 )
                    return newsInput;
                else
                    return "0";
            }
            else
                return DataAccessContext.NewsRepository.GetNewsIDFromUrlName( newsInput );
        }
    }
    protected bool CheckImageExist( string imageFile )
    {
        string imageName = imageFile.ToString();
        bool imageExist = false;
        if ( !String.IsNullOrEmpty( imageName ) )
        {
            imageExist = File.Exists( Server.MapPath( imageName ) );
        }
        return imageExist;
    }
    private void PopulateControls()
    {

        string title;
        string metaDescription;
        string metakeyword;
        string imageFile;
        News news = DataAccessContext.NewsRepository.GetOne( StoreContext.Culture, CurrentID );
        if (news.IsEnabled == false)
            Response.Redirect( "~/Error404.aspx",true );
        imageFile = news.ImageFile;
        if ( ( imageFile != "" ) && ( CheckImageExist( imageFile ) ) )
            uxCatalogImage.ImageUrl = imageFile;
        else
            uxCatalogImage.Visible = false;

        uxTopicLabel.Text = news.Topic;
        uxNewsDate.Text = string.Format( "{0:ddd, MMM d, yyyy}", ( DateTime ) news.NewsDate );
        uxDescriptionLiteral.Text = news.Description;

        title = news.Topic;
        if ( news.MetaDescription != "" && news.IsEnabled )
            metaDescription = news.MetaDescription;
        else
            metaDescription = NamedConfig.SiteDescription;
        if ( news.MetaKeyword != "" && news.IsEnabled )
            metakeyword = news.MetaKeyword;
        else
            metakeyword = NamedConfig.SiteKeyword;
        element.SetUpTitleAndMetaTags( title, metaDescription, metakeyword );

        SetUpBreadcrumb( news.NewsID, news.Topic, news.MetaDescription, news.URLName );
    }

    private void RegisterStoreEvents()
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( NewsDetails_StoreCultureChanged );
    }

    private void NewsDetails_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        News news = DataAccessContext.NewsRepository.GetOne( StoreContext.Culture, CurrentID );
        string newURL = UrlManager.GetNewsUrl( CurrentID, news.URLName );

        Response.Redirect( newURL );
    }
    private void SetUpBreadcrumb( string newsID, string topic, string shortDescription, string urlName )
    {
        uxCatalogBreadcrumb.SetupNewsSitemap( newsID, topic, shortDescription, urlName );
        uxCatalogBreadcrumb.Refresh();
    }
    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterStoreEvents();
        element = new DynamicPageElement( this );
        AjaxUtilities.ScrollToTop( uxGoToTopLink );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if ( !IsPostBack )
        {
            if ((CurrentID == "0") || (String.IsNullOrEmpty( CurrentID )))
                Response.Redirect( "~/Error404.aspx",true );
            PopulateControls();
            uxCatalogBreadcrumb.Refresh();
        }
    }

}
