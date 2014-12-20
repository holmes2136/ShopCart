using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Blogs;
using Vevo.WebUI;
using Vevo.WebUI.Blog;
using Vevo.Shared.WebUI;

public partial class Layouts_BlogDetails_BlogDetailsDefault : BaseBlogDetailsControl
{
    #region Private
    private void SetTagsLink()
    {
        string[] tagsList = CurrentBlog.Tags.Split(',');

        foreach (string tags in tagsList)
        {
            HyperLink tagsLink = new HyperLink();
            tagsLink.ID = tags;
            tagsLink.CssClass = "BlogDetailsDefaultTagsLink";
            tagsLink.Text = Server.HtmlEncode(tags);
            tagsLink.NavigateUrl = "~/Blog/SearchResult.aspx?Search=" + Server.UrlEncode(tags.Trim());
            uxTagsLinkPanel.Controls.Add(tagsLink);
            if (tagsList[tagsList.Length - 1] != tags)
            {
                Label uxSeparater = new Label();
                uxSeparater.ID = "uxSeparater";
                uxSeparater.Text = " , ";
                uxTagsLinkPanel.Controls.Add(uxSeparater);
            }
        }
        uxTagsPanel.Visible = true;
    }

    private void SetCatagoryLink()
    {
        IList<string> categoryListID = CurrentBlog.BlogCategoryIDs;
        foreach (string id in categoryListID)
        {
            BlogCategory blogCategory = DataAccessContext.BlogCategoryRepository.GetOne(StoreContext.Culture, id);
            HyperLink categoryLink = new HyperLink();
            categoryLink.ID = "uxCategoryLink" + blogCategory.BlogCategoryID;
            categoryLink.CssClass = "BlogDetailsDefaultTagsLink";
            categoryLink.Text = blogCategory.Name;
            categoryLink.NavigateUrl = UrlManager.GetBlogCategoryUrl(blogCategory.UrlName);
            uxCategoryLinkPanel.Controls.Add(categoryLink);

            if (categoryListID[categoryListID.Count - 1] != id)
            {
                Label uxSeparater = new Label();
                uxSeparater.ID = "uxSeparater";
                uxSeparater.Text = " , ";
                uxCategoryLinkPanel.Controls.Add(uxSeparater);
            }
        }
        uxCategoryPanel.Visible = true;
    }
    #endregion

    #region Protected
    protected void Page_Load(object sender, EventArgs e)
    {

        if (String.IsNullOrEmpty(CurrentBlog.Tags) && CurrentBlog.BlogCategoryIDs.Count == 0)
        {
            uxTagsCategoryDiv.Visible = false;
        }
        else
        {
            uxTagsPanel.Visible = false;
            uxCategoryPanel.Visible = false;

            if (!String.IsNullOrEmpty(CurrentBlog.Tags))
                SetTagsLink();
            if (CurrentBlog.BlogCategoryIDs.Count != 0)
                SetCatagoryLink();
        }

    }
    protected bool IsImageVisible(object ImageFile)
    {
        return !String.IsNullOrEmpty((string)ImageFile);
    }

    protected string CreateGooglePlusUrl(object blogID, object urlName)
    {
        string getBlogUrl = UrlManager.GetBlogUrl(blogID, urlName);
        int indexRealUrlName = getBlogUrl.LastIndexOf("/");
        int indexUrl = UrlPath.GetDisplayedUrl().LastIndexOf("/");
        getBlogUrl = UrlPath.GetDisplayedUrl().Substring(0, indexUrl) + "/" + getBlogUrl.Substring(indexRealUrlName + 1);

        return getBlogUrl;
    }

    protected string CreateFacebookLikeUrl(object blogID, object urlName)
    {
        string getBlogUrl = UrlManager.GetBlogUrl(blogID, urlName);
        int indexRealUrlName = getBlogUrl.LastIndexOf("/");
        int indexUrl = UrlPath.GetDisplayedUrl().LastIndexOf("/");
        getBlogUrl = UrlPath.GetDisplayedUrl().Substring(0, indexUrl) + "/" + getBlogUrl.Substring(indexRealUrlName + 1);
        return "https://www.facebook.com/plugins/like.php?href= " + getBlogUrl + "&layout=button_count&width=100&show_faces=false&action=like&colorscheme=light&font=arial&locale=en_US";
    }
    #endregion
}
