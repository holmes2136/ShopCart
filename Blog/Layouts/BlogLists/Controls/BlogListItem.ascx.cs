using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Blogs;
using Vevo.WebUI;
using Vevo.WebUI.Blog;
using System.Text.RegularExpressions;
using Vevo.Shared.WebUI;

public partial class Layouts_BlogLists_Controls_BlogListItem : BaseBlogDetailsControl
{
    #region Private
    private string RemoveHTML(string strHTML)
    {
        return Regex.Replace(strHTML, "<(.|\n)*?>", "");
    }
    
    private void GetTagsList()
    {
        string tagListID = uxTagsHidden.Value;
        string[] tagsList = tagListID.Split(',');
        foreach (string tag in tagsList)
        {
            HyperLink tagsLink = new HyperLink();
            tagsLink.ID = tag;
            tagsLink.CssClass = "BlogDetailsDefaultTagsLink";
            tagsLink.Text = Server.HtmlEncode(tag);
            tagsLink.NavigateUrl = "~/Blog/SearchResult.aspx?Search=" + Server.UrlEncode(tag.Trim());
            uxTagsLinkPanel.Controls.Add(tagsLink);
            if (tagsList[tagsList.Length - 1] != tag)
            {
                Label uxSeparater = new Label();
                uxSeparater.ID = "uxSeparater";
                uxSeparater.Text = " , ";
                uxTagsLinkPanel.Controls.Add(uxSeparater);
            }
        }
        uxTagsListPanel.Visible = true;
    }

    private void GetCatagoryList()
    {
        string[] categoryIDList = uxCategoryIDsHidden.Value.Split(',');

        foreach (string id in categoryIDList)
        {
            BlogCategory blogCategory = DataAccessContext.BlogCategoryRepository.GetOne(StoreContext.Culture, id);
            HyperLink categoryLink = new HyperLink();
            categoryLink.ID = "uxCategoryLink" + blogCategory.BlogCategoryID;
            categoryLink.CssClass = "BlogDetailsDefaultTagsLink";
            categoryLink.Text = blogCategory.Name;
            categoryLink.NavigateUrl = UrlManager.GetBlogCategoryUrl(blogCategory.UrlName);
            uxCategoryLinkPanel.Controls.Add(categoryLink);

            if (categoryIDList[categoryIDList.Length - 1] != id)
            {
                Label uxSeparater = new Label();
                uxSeparater.ID = "uxCategorySeparater" + blogCategory.BlogCategoryID;
                uxSeparater.Text = " , ";
                uxCategoryLinkPanel.Controls.Add(uxSeparater);
            }
        }
        uxCategoryListPanel.Visible = true;
    }

    private void PopulateControls()
    {
        if (!IsPostBack)
        {
            if (String.IsNullOrEmpty(uxTagsHidden.Value) && String.IsNullOrEmpty(uxCategoryIDsHidden.Value))
            {
                uxTagsCategoryDiv.Visible = false;
            }
            else
            {
                uxTagsListPanel.Visible = false;
                uxCategoryListPanel.Visible = false;

                if (!String.IsNullOrEmpty(uxTagsHidden.Value))
                    GetTagsList();
                if (!String.IsNullOrEmpty(uxCategoryIDsHidden.Value))
                    GetCatagoryList();
            }

            if (String.IsNullOrEmpty(uxBlogImage.ImageUrl))
            {
                uxBlogImage.Visible = false;
            }
            else
            {
                uxBlogImage.Visible = true;
            }
        }
    }
    #endregion

    #region Protected
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    protected string GetCatagoryIDList(object categoryID)
    {
        IList<string> categoryListID = (IList<string>)categoryID;
        string categoryIDs = string.Empty;
        for (int i = 0; i < categoryListID.Count; i++)
        {
            categoryIDs += categoryListID[i];
            if (i == categoryListID.Count - 1)
                break;
            categoryIDs += ",";
        }

        return categoryIDs;
    }

    protected string CreateGooglePlusUrl(object blogID, object urlName)
    {
        string getBlogUrl = UrlManager.GetBlogUrl(blogID, urlName);
        int indexRealUrlName = getBlogUrl.LastIndexOf("/");
        int indexUrl = UrlPath.GetDisplayedUrl().LastIndexOf("/");
        getBlogUrl = UrlPath.GetDisplayedUrl().Substring(0, indexUrl) + "/" + getBlogUrl.Substring(indexRealUrlName + 1);

        return getBlogUrl;
    }

    protected string CreateFacebookLikeUrl( object blogID, object urlName )
    {
        string getBlogUrl = UrlManager.GetBlogUrl( blogID, urlName );
        int indexRealUrlName = getBlogUrl.LastIndexOf( "/" );
        int indexUrl = UrlPath.GetDisplayedUrl().LastIndexOf( "/" );
        getBlogUrl = UrlPath.GetDisplayedUrl().Substring( 0, indexUrl ) + "/" + getBlogUrl.Substring( indexRealUrlName + 1 );
		return "https://www.facebook.com/plugins/like.php?href= " + getBlogUrl + "&layout=button_count&width=100&show_faces=false&action=like&colorscheme=light&font=arial&locale=en_US";    
	}

    protected string LimitDisplayCharactor(object description, short characterLimit)
    {
        string shortContent = RemoveHTML(description.ToString());

        if (shortContent.Length > characterLimit)
        {
            string tempString = shortContent.Substring(0, characterLimit).Trim();

            int trimOffset = tempString.LastIndexOf(" ");

            if (trimOffset > 0)
            {
                shortContent = tempString.Substring(0, trimOffset);
            }

            shortContent += " [...]";
        }

        return shortContent;
    }
    #endregion
}
