using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Blogs;
using Vevo.WebUI;
using Vevo.WebUI.BaseControls;

public partial class Themes_ResponsiveGreen_Components_NewsEvent : BaseLayoutControl
{
    private string _blogNewsAndAnnouncementCategoryID = "1";

    private string RemoveHTML( string strHTML )
    {
        return Regex.Replace( strHTML, "<(.|\n)*?>", "" );
    }

    private void PopulateNewsEvent()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "BlogEnabled" ) &&
            DataAccessContext.BlogCategoryRepository.GetOne( StoreContext.Culture, _blogNewsAndAnnouncementCategoryID ).IsEnabled)
        {
            int howManyItem = 0;
            IList<Blog> blogList = DataAccessContext.BlogRepository.GetBlogListByCategoryID(
                "CreateDate DESC",
                _blogNewsAndAnnouncementCategoryID,
                0,
                2,
                out howManyItem,
                StoreContext.CurrentStore.StoreID,
                BoolFilter.ShowTrue );

            if (howManyItem == 0)
            {
                uxNewsEventDiv.Visible = false;
                return;
            }

            uxNewsRepeater.DataSource = blogList;
            uxNewsRepeater.DataBind();
        }
        else
        {
            uxNewsEventDiv.Visible = false;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            if (!DataAccessContext.Configurations.GetBoolValue( "NewsModuleDisplay" ))
            {
                uxNewsEventDiv.Visible = false;
            }
            else
            {
                PopulateNewsEvent();
            }
        }
    }

    protected string GetBlogUrl( object blogID, object urlName )
    {
        return UrlManager.GetBlogUrl( blogID, urlName );
    }

    protected string LimitDisplayCharactor( object description, short characterLimit )
    {
        string descriptionWithOutHTML = RemoveHTML( description.ToString() );

        int maxLength = descriptionWithOutHTML.Length;
        if (maxLength > characterLimit)
            maxLength = characterLimit;
        else
            return descriptionWithOutHTML;
        string tempString = descriptionWithOutHTML.Substring( 0, maxLength ).Trim();
        int trimOffset = tempString.LastIndexOf( " " );

        string result = tempString;
        if (trimOffset > 0)
        {
            result = tempString.Substring( 0, trimOffset );
        }
        result += "...";

        return result;
    }

    protected string GetBlogImage( object blogImage )
    {
        string imagePath = blogImage.ToString();

        if (String.IsNullOrEmpty( imagePath ))
            imagePath = "~/Images/News/news-blank.jpg";

        return imagePath;
    }
}
