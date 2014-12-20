using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Stores;

public partial class AdminAdvanced_Components_StoreConfig_StoreBlogConfig : AdminAdvancedBaseUserControl
{
    private Store CurrentStore
    {
        get
        {
            return DataAccessContext.StoreRepository.GetOne( StoreID );
        }
    }

    private Culture Culture
    {
        get
        {
            return DataAccessContext.CultureRepository.GetOne( CultureID );
        }
    }

    private void SetUpFacebookCommentDetailsVisibility()
    {
        if (uxFacebookCommentEnabledDrop.SelectedValue == "True")
            uxFbCommentDetailsPanel.Visible = true;
        else
            uxFbCommentDetailsPanel.Visible = false;
    }

    private void SetUpBlogCommentDetailsVisibility()
    {
        if (uxEnableBlogCommentDrop.SelectedValue == "True")
            uxBlogCommentPanel.Visible = true;
        else
            uxBlogCommentPanel.Visible = false;
    }

    public string CultureID
    {
        get
        {
            return AdminConfig.CurrentContentCultureID;
        }
    }

    public string StoreID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
            {
                return MainContext.QueryString["StoreID"];
            }
            else
            {
                return Store.RegularStoreID;
            }
        }
    }

    public void PopulateControls()
    {
        uxEnableBlogDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue( "BlogEnabled", CurrentStore ).ToString();
        uxEnableBlogCommentDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue( "BlogCommentEnabled", CurrentStore ).ToString();
        uxBlogListItemsPerPageTextbox.Text = DataAccessContext.Configurations.GetValue( "BlogListItemsPerPage", CurrentStore );
        uxBlogCommentsSortOrderDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "BlogCommentsSortOrder", CurrentStore );
        uxEnableDisplayArchivesDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue( "DisplayArchivesEnabled", CurrentStore ).ToString();
        uxDisplayBlogCountDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue( "BlogCountInCategoryBox", CurrentStore ).ToString();
        uxBlogPageTitleTextbox.Text = DataAccessContext.Configurations.GetValue( CultureID, "BlogPageTitle", CurrentStore );
        uxBlogThemeSelect.PopulateControls( CurrentStore );
        uxBlogListSelect.PopulateControls( CurrentStore );
        uxBlogDetailsSelect.PopulateControls( CurrentStore );

        uxFacebookCommentEnabledDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue( "FacebookCommentEnabled", CurrentStore ).ToString();
        uxFacebookCommentAPIKeyTextbox.Text = DataAccessContext.Configurations.GetValue( "FacebookCommentAPIKey", CurrentStore );
        uxFacebookCommentNumberOfPostsTextbox.Text = DataAccessContext.Configurations.GetValue( "FacebookCommentNumberOfPosts", CurrentStore );
        uxBlogCommentPerPageText.Text = DataAccessContext.Configurations.GetValue( "BlogCommentsPerPage", CurrentStore );
        SetUpFacebookCommentDetailsVisibility();
        SetUpBlogCommentDetailsVisibility();
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["BlogEnabled"],
            uxEnableBlogDrop.SelectedValue,
            CurrentStore );
        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["BlogCommentEnabled"],
           uxEnableBlogCommentDrop.SelectedValue,
           CurrentStore );
        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["DisplayArchivesEnabled"],
           uxEnableDisplayArchivesDrop.SelectedValue,
           CurrentStore );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["BlogCountInCategoryBox"],
            uxDisplayBlogCountDrop.SelectedValue,
            CurrentStore );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["BlogListItemsPerPage"],
            uxBlogListItemsPerPageTextbox.Text,
            CurrentStore );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            Culture,
            DataAccessContext.Configurations["BlogPageTitle"],
            uxBlogPageTitleTextbox.Text,
            CurrentStore );

        uxEnableBlogDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue( "BlogEnabled", CurrentStore ).ToString();
        uxEnableBlogCommentDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue( "BlogCommentEnabled", CurrentStore ).ToString();
        uxEnableDisplayArchivesDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue( "DisplayArchivesEnabled", CurrentStore ).ToString();
        uxDisplayBlogCountDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue( "BlogCountInCategoryBox", CurrentStore ).ToString();
        uxBlogListItemsPerPageTextbox.Text = DataAccessContext.Configurations.GetValue( "BlogListItemsPerPage", CurrentStore );
        uxBlogPageTitleTextbox.Text = DataAccessContext.Configurations.GetValue( CultureID, "BlogPageTitle", CurrentStore );
        uxBlogThemeSelect.Update( CurrentStore );
        uxBlogListSelect.Update( CurrentStore );
        uxBlogDetailsSelect.Update( CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["FacebookCommentEnabled"],
            uxFacebookCommentEnabledDrop.SelectedValue,
            CurrentStore );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["FacebookCommentAPIKey"],
            uxFacebookCommentAPIKeyTextbox.Text,
            CurrentStore );
        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["FacebookCommentNumberOfPosts"],
           uxFacebookCommentNumberOfPostsTextbox.Text,
           CurrentStore );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["BlogCommentsPerPage"],
            uxBlogCommentPerPageText.Text,
            CurrentStore );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["BlogCommentsSortOrder"],
            uxBlogCommentsSortOrderDrop.SelectedValue,
            CurrentStore );

        PopulateControls();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (MainContext.IsPostBack)
        {
            uxBlogThemeSelect.PopulateControls();
            uxBlogListSelect.PopulateControls();
            uxBlogDetailsSelect.PopulateControls();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            PopulateControls();

        if (uxEnableBlogDrop.SelectedValue == "True")
            uxBlogDetailsPanel.Visible = true;
        else
            uxBlogDetailsPanel.Visible = false;

        SetUpFacebookCommentDetailsVisibility();
    }


    protected void uxFacebookCommentEnabledDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        SetUpFacebookCommentDetailsVisibility();
    }

    protected void uxEnableBlogCommentDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        SetUpBlogCommentDetailsVisibility();
    }
}
