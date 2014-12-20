using System;
using System.Web.Security;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Blogs;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;

public partial class Admin_Components_BlogDetails : AdminAdvancedBaseUserControl
{
    private const string _pathUploadBlog = "Images/Blog/";

    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string CurrentID
    {
        get
        {
            if (string.IsNullOrEmpty( MainContext.QueryString["BlogID"] ))
                return "0";
            else
                return MainContext.QueryString["BlogID"];
        }
    }

    private void ClearInputField()
    {
        uxBlogTitleText.Text = "";
        uxShortContentText.Text = "";
        uxBlogContentText.Text = "";
        uxBlogMetaTitleText.Text = "";
        uxBlogMetaKeywordText.Text = "";
        uxBlogMetaDescriptionText.Text = "";
        uxBlogTagsText.Text = "";
        uxImageText.Text = "";
        uxImageBlogUpload.ShowControl = false;
        uxIsEnabledCheck.Checked = true;
        uxMultiStoreList.SetupDropDownList( CurrentID, false );
        uxBlogCategoryList.SetupDropDownList( CurrentID, false );
        uxCreateDateCalendarPopup.SelectedDate = DateTime.Now;
        uxBlogUrlLink = null;
    }

    private void InitStoreList()
    {

        if (!MainContext.IsPostBack)
        {
            uxMultiStoreList.SetupDropDownList( CurrentID, false );
        }
        else
        {
            uxMultiStoreList.SetupDropDownList( CurrentID, true );
        }

    }

    private void InitBlogCategoryList()
    {

        if (!MainContext.IsPostBack)
        {
            uxBlogCategoryList.SetupDropDownList( CurrentID, false );
        }
        else
        {
            uxBlogCategoryList.SetupDropDownList( CurrentID, true );
        }

    }

    private void PopulateControls()
    {
        if (CurrentID != null &&
            int.Parse( CurrentID ) >= 0)
        {
            Blog blog = DataAccessContext.BlogRepository.GetOne( CurrentID );
            uxBlogUrlLink.NavigateUrl = UrlManager.GetBlogUrl( blog.BlogID, blog.UrlName );
            uxBlogUrlLink.Text = UrlPath.StorefrontUrl + blog.UrlName + "-blog.aspx";
            uxImageText.Text = blog.ImageFile;
            uxBlogTitleText.Text = blog.BlogTitle;
            uxShortContentText.Text = blog.ShortContent;
            uxBlogContentText.Text = blog.BlogContent;
            uxBlogMetaTitleText.Text = blog.MetaTitle;
            uxBlogMetaKeywordText.Text = blog.MetaKeyword;
            uxBlogMetaDescriptionText.Text = blog.MetaDescription;
            uxBlogTagsText.Text = blog.Tags;
            uxIsEnabledCheck.Checked = blog.IsEnabled;
            uxCreateDateCalendarPopup.SelectedDate = blog.CreateDate;
            InitStoreList();
            InitBlogCategoryList();
        }
        else
        {
            ClearInputField();
        }
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private DateTime GetNewsDate()
    {
        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
        {
            return uxCreateDateCalendarPopup.SelectedDate;
        }
        else
        {
            return ConvertUtilities.ToDateTime( uxCreateDateText.Text );
        }
    }

    private string UpdateTag( string tagTextBox )
    {
        string[] tagsList = tagTextBox.Split( ',' );
        string newTag = String.Empty;

        foreach ( string tag in tagsList )
        {
            if ( !tag.Contains( "(" ) && !tag.Contains( "<" ) )
            {
                newTag += tag + ",";
            }
        }

        newTag = newTag.TrimEnd( ',' );

        return newTag;
    }

    private Blog SetUpBlog( Blog blog )
    {
        blog.BlogTitle = uxBlogTitleText.Text;
        blog.ShortContent = uxShortContentText.Text;
        blog.BlogContent = uxBlogContentText.Text;
        blog.MetaTitle = uxBlogMetaTitleText.Text;
        blog.MetaKeyword = uxBlogMetaKeywordText.Text;
        blog.MetaDescription = uxBlogMetaDescriptionText.Text;
        blog.Tags = UpdateTag( uxBlogTagsText.Text.Trim() );
        blog.IsEnabled = uxIsEnabledCheck.Checked;
        blog.CreateDate = uxCreateDateCalendarPopup.SelectedDate;
        blog.ImageFile = uxImageText.Text;
        if (KeyUtilities.IsMultistoreLicense())
        {
            blog.StoreIDs = uxMultiStoreList.ConvertToStoreIDs();
        }
        else
        {
            blog.StoreIDs.Clear();
            blog.StoreIDs.Add( new StoreRetriever().GetCurrentStoreID() );
        }
        blog.BlogCategoryIDs = uxBlogCategoryList.ConvertToBlogCategoryIDs();

        return blog;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        InitStoreList();
        InitBlogCategoryList();
        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
        {
            uxCreateDateText.Visible = false;
        }
        else
        {
            uxCreateDateText.Visible = true;
        }
        if (!MainContext.IsPostBack)
        {
            uxImageBlogUpload.PathDestination = _pathUploadBlog;
            uxImageBlogUpload.ReturnTextControlClientID = uxImageText.ClientID;
        }

        if (!KeyUtilities.IsMultistoreLicense())
            uxStorePanel.Visible = false;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (IsEditMode())
        {
            if (!MainContext.IsPostBack)
            {
                PopulateControls();
                if (IsAdminModifiable())
                {
                    uxUpdateButton.Visible = true;
                }
                else
                {
                    uxUpdateButton.Visible = false;
                }

                uxAddButton.Visible = false;
            }
        }
        else
        {
            if (IsAdminModifiable())
            {
                uxCreateDateLabel.Visible = true;
                uxCreateDateCalendarPopup.Visible = true;
                uxAddButton.Visible = true;
                uxUpdateButton.Visible = false;

                if (!MainContext.IsPostBack)
                    uxCreateDateCalendarPopup.SelectedDate = DateTime.Now;
            }
            else
            {
                MainContext.RedirectMainControl( "BlogList.ascx", "" );
            }
        }
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                if (uxMultiStoreList.ConvertToStoreIDs().Length > 0 || !KeyUtilities.IsMultistoreLicense())
                {
                    Blog blog = new Blog();
                    blog.Publisher = Membership.GetUser().UserName;
                    blog = SetUpBlog( blog );
                    blog = DataAccessContext.BlogRepository.Save( blog );
                 
                   
                    uxMessage.DisplayMessage( Resources.BlogMessage.AddSuccess );

                    ClearInputField();
                }
                else
                {
                    uxMessage.DisplayError( Resources.BlogMessage.ErrorStoreEmpty );
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                if (uxMultiStoreList.ConvertToStoreIDs().Length > 0 || !KeyUtilities.IsMultistoreLicense())
                {
                    Blog blog = DataAccessContext.BlogRepository.GetOne( CurrentID );
                    blog = SetUpBlog( blog );
                    blog = DataAccessContext.BlogRepository.Save( blog );

                    uxMessage.DisplayMessage( Resources.BlogMessage.UpdateSuccess );
                    PopulateControls();
                }
                else
                {
                    uxMessage.DisplayError( Resources.BlogMessage.ErrorStoreEmpty );
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }
    protected void uxImageBlogLinkButton_Click(object sender, EventArgs e)
    {
        uxImageBlogUpload.ShowControl = true;
    }

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }
}
