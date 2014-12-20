using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Blogs;
using Vevo.Domain.Users;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebUI.Blog;
using Vevo.WebUI.International;

public partial class Blog_BlogDetails : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string BlogID
    {
        get
        {
            if (!String.IsNullOrEmpty( Request.QueryString["BlogID"] ))
            {
                string[] blogID = Request.QueryString["BlogID"].Split( ',' );
                return blogID[0];
            }
            else
            {
                if (!String.IsNullOrEmpty( Request.QueryString["BlogName"] ))
                {
                    string[] blogName = Request.QueryString["BlogName"].Split( ',' );
                    return DataAccessContext.BlogRepository.GetBlogIDFromUrlName( blogName[0] );
                }
                else
                    return string.Empty;
            }
        }
    }

    private string BlogTitle
    {
        get
        {
            if (ViewState["BlogTitle"] == null)
                return String.Empty;
            else
                return (String) ViewState["BlogTitle"];
        }
        set
        {
            ViewState["BlogTitle"] = value;
        }
    }

    private string BlogContent
    {
        get
        {
            if (ViewState["BlogContent"] == null)
                return String.Empty;
            else
                return (String) ViewState["BlogContent"];
        }
        set
        {
            ViewState["BlogContent"] = value;
        }
    }

    private void Refresh()
    {
        uxBlogFormView.DataBind();
    }

    private void BlogDetails_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        Refresh();
    }

    private void BlogDetails_StoreCurrencyChanged( object sender, CurrencyEventArgs e )
    {
        Refresh();
    }

    private void RegisterStoreEvents()
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( BlogDetails_StoreCultureChanged );
        GetStorefrontEvents().StoreCurrencyChanged +=
            new StorefrontEvents.CurrencyEventHandler( BlogDetails_StoreCurrencyChanged );
    }

    private void PopulateTitleAndMeta()
    {
        DynamicPageElement element = new DynamicPageElement( this );
        Blog blog = DataAccessContext.BlogRepository.GetOne( BlogID );

        element.SetUpTitleAndMetaTags(
            blog.BlogTitle,
            blog.MetaDescription,
            blog.MetaKeyword );

        if (DataAccessContext.Configurations.GetBoolValue( "FacebookCommentEnabled" ))
        {
            /*
                og:title - The title of the entity.
                og:type - The type of entity. You must select a type from the list of Open Graph types. http://developers.facebook.com/docs/opengraphprotocol/#types
                og:image - The URL to an image that represents the entity. Images must be at least 50 pixels by 50 pixels. Square images work best, but you are allowed to use images up to three times as wide as they are tall.
                og:url - The canonical, permanent URL of the page representing the entity. When you use Open Graph tags, the Like button posts a link to the og:url instead of the URL in the Like button code.
                og:site_name - A human-readable name for your site, e.g., "IMDb".
                fb:admins or fb:app_id - A comma-separated list of either the Facebook IDs of page administrators or a Facebook Platform application ID. At a minimum, include only your own Facebook ID.
                facebook debug tool: http://developers.facebook.com/tools/debug
             */

            HtmlMeta titleTag = new HtmlMeta();
            titleTag.Attributes.Add( "property", "og:title" );
            titleTag.Attributes.Add( "content", blog.BlogTitle );

            HtmlMeta typeTag = new HtmlMeta();
            typeTag.Attributes.Add( "property", "og:type" );
            typeTag.Attributes.Add( "content", "blog" );

            //HtmlMeta imageTag = new HtmlMeta();
            //imageTag.Attributes.Add( "property", "og:image" );
            //imageTag.Attributes.Add( "content", UrlPath.StorefrontUrl + _product.GetPrimaryProductImage().RegularImage );

            HtmlMeta urlTag = new HtmlMeta();
            urlTag.Attributes.Add( "property", "og:url" );
            urlTag.Attributes.Add( "content", Vevo.Shared.WebUI.UrlPath.GetDisplayedUrl() );

            HtmlMeta site_nameTag = new HtmlMeta();
            site_nameTag.Attributes.Add( "property", "og:site_name" );
            site_nameTag.Attributes.Add( "content", DataAccessContext.Configurations.GetValue( StoreContext.Culture.CultureID, "SiteName", StoreContext.CurrentStore ) );

            HtmlMeta app_id = new HtmlMeta();
            app_id.Attributes.Add( "property", "fb:app_id" );
            app_id.Attributes.Add( "content", DataAccessContext.Configurations.GetValue( "FacebookCommentAPIKey" ) );

            Page.Header.Controls.Add( titleTag );
            Page.Header.Controls.Add( typeTag );
            //Page.Header.Controls.Add( imageTag );
            Page.Header.Controls.Add( urlTag );
            Page.Header.Controls.Add( site_nameTag );
            Page.Header.Controls.Add( app_id );
        }
    }

    private void IsBlogEnable()
    {
        Blog blog = DataAccessContext.BlogRepository.GetOne( BlogID );
        string adminID = "0";
        if (Membership.GetUser() != null)
        {
            adminID = DataAccessContext.AdminRepository.GetIDFromUserName( Membership.GetUser().UserName );
        }
        Admin admin = DataAccessContext.AdminRepository.GetOne( adminID );
        if (blog.IsNull || ((!blog.IsEnabled || blog.CreateDate > DateTime.Now) && admin.IsNull))
            Response.Redirect( "./Default.aspx" );
    }

    private void RefreshTitle()
    {
        Blog blog = DataAccessContext.BlogRepository.GetOne( BlogID );
        this.Page.Title = blog.BlogTitle;

        uxTitleLiteral.Text = blog.BlogTitle;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterStoreEvents();

        if (!IsPostBack)
        {
            IsBlogEnable();
        }

        PopulateTitleAndMeta();

        if (DataAccessContext.Configurations.GetBoolValue( "FacebookCommentEnabled" ))
        {
            uxFacebookCommentBoxPanel.Visible = true;
            uxFacebookCommentBox.Attributes.Add( "data-href", GetPageLink() );
        }
        else
        {
            uxFacebookCommentBoxPanel.Visible = false;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        RefreshTitle();
    }

    protected void uxBlogDetailsSource_Selecting( object sender, ObjectDataSourceSelectingEventArgs e )
    {
        e.InputParameters["BlogID"] = BlogID;
    }

    protected void uxBlogFormView_DataBinding( object sender, EventArgs e )
    {
    }

    protected void uxBlogFormView_DataBound( object sender, EventArgs e )
    {
        FormView formView = (FormView) sender;

        // FormView's DataItem property is valid only in DataBound event. 
        // Most of the time it is null.
        if (String.IsNullOrEmpty( BlogTitle ))
        {
            BlogTitle = ConvertUtilities.ToString( DataBinder.Eval( formView.DataItem, "BlogTitle" ) );
            BlogContent = ConvertUtilities.ToString( DataBinder.Eval( formView.DataItem, "BlogContent" ) );
        }
    }

    protected void BlogItemCreate( object sender, EventArgs e )
    {
        FormView formView = (FormView) sender;
        Blog blog = DataAccessContext.BlogRepository.GetOne( BlogID );

        BaseBlogDetailsControl blogDetailsControl = new BaseBlogDetailsControl();

        blogDetailsControl = LoadControl( String.Format( "{0}{1}",
                    SystemConst.LayoutBlogDetailsPath,
                    DataAccessContext.Configurations.GetValue( "DefaultBlogDetailsLayout" ), StoreContext.CurrentStore ) )
                as BaseBlogDetailsControl;

        blogDetailsControl.CurrentBlog = blog;

        formView.Controls.Add( blogDetailsControl );
    }

    protected string GetPageLink()
    {
        return Vevo.Shared.WebUI.UrlPath.GetDisplayedUrl();
    }

    protected string GetNoOfPosts()
    {
        return DataAccessContext.Configurations.GetValue( "FacebookCommentNumberOfPosts" );
    }
}
