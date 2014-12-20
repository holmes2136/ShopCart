using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Shared.DataAccess;
using Vevo.Shared.WebUI;
using Vevo.Deluxe.Domain;

public partial class AdminAdvanced_Components_ContentDetails : AdminAdvancedBaseUserControl
{
    #region Private
    private string ContentID
    {
        get
        {
            return MainContext.QueryString["ContentID"];
        }

    }
    private enum Mode { Add, Edit };
    private Mode _mode = Mode.Add;

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private void PopulateControls()
    {

        if (ContentID != null)
        {
            Vevo.Domain.Contents.Content content = DataAccessContext.ContentRepository.GetOne( uxLanguageControl.CurrentCulture, ContentID );

            if (!content.IsNull)
            {

                uxContentUrlLink.NavigateUrl = UrlManager.GetContentUrl( ContentID, content.UrlName );
                uxContentUrlLink.Text = UrlPath.StorefrontUrl + content.UrlName + "-content.aspx";

                uxContentNameText.Text = content.ContentName;
                uxContentTitleText.Text = content.Title;
                uxLongDescriptionText.Text = content.Body;
                uxContentMetaTitleText.Text = content.MetaTitle;
                uxContentMetaKeywordText.Text = content.MetaKeyword;
                uxContentMetaDescriptionText.Text = content.MetaDescription;
                uxContentEnabledCheck.Checked = content.IsEnabled;
                uxContentShowInSiteMapCheck.Checked = content.IsShowInSiteMap;
                uxSubscriptionLevel.SelectedValue = content.SubscriptionLevelID;
                uxOther1Text.Text = content.Other1;
                uxOther2Text.Text = content.Other2;
                uxOther3Text.Text = content.Other3;

            }
            else
            {
                ClearInputFields();
            }

        }

    }

    private void ClearInputFields()
    {
        uxContentNameText.Text = "";
        uxContentTitleText.Text = "";
        uxContentUrlLink = null;
        uxLongDescriptionText.Text = "";
        uxContentMetaTitleText.Text = "";
        uxContentMetaKeywordText.Text = "";
        uxContentMetaDescriptionText.Text = "";
        uxContentEnabledCheck.Checked = true;
        uxContentShowInSiteMapCheck.Checked = true;
        uxContentCustomUrlText.Text = "";
        uxOther1Text.Text = "";
        uxOther2Text.Text = "";
        uxOther3Text.Text = "";
        uxSubscriptionLevel.SelectedValue = "0";
    }

    private Vevo.Domain.Contents.Content SetUpContent( Vevo.Domain.Contents.Content content )
    {
        content.ContentName = uxContentNameText.Text.Trim();
        content.Title = uxContentTitleText.Text.Trim();
        content.Body = uxLongDescriptionText.Text;
        content.MetaTitle = uxContentMetaTitleText.Text.Trim();
        content.MetaKeyword = uxContentMetaKeywordText.Text.Trim();
        content.MetaDescription = uxContentMetaDescriptionText.Text.Trim();
        content.IsEnabled = uxContentEnabledCheck.Checked;
        content.IsShowInSiteMap = uxContentShowInSiteMapCheck.Checked;
        content.Other1 = uxOther1Text.Text;
        content.Other2 = uxOther2Text.Text;
        content.Other3 = uxOther3Text.Text;
        if (uxContentCustomUrlText.Text != "")
            content.UrlName = uxContentCustomUrlText.Text.Trim();
        else
            content.UrlName = uxContentTitleText.Text.Trim();

        content.SubscriptionLevelID = uxSubscriptionLevel.SelectedValue;

        return content;
    }

    private void PopulateSubscriptionLevelControl()
    {
        uxSubscriptionLevel.Items.Clear();
        uxSubscriptionLevel.DataSource = DataAccessContextDeluxe.SubscriptionLevelRepository.GetAll( "SubscriptionLevelID" ); ;
        uxSubscriptionLevel.DataBind();
        uxSubscriptionLevel.Items.Insert( 0, new ListItem( "None", "0" ) );
    }

    #endregion

    #region Protected
    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
        {
            ucContentSubscriptionTR.Visible = false;
        }

        if (IsEditMode())
        {
            if (!MainContext.IsPostBack)
            {
                PopulateControls();
            }

            if (IsAdminModifiable())
            {
                uxEditButton.Visible = true;
            }
            else
            {
                uxEditButton.Visible = false;
            }

            uxAddButton.Visible = false;

        }
        else
        {
            if (IsAdminModifiable())
            {
                uxAddButton.Visible = true;
                uxEditButton.Visible = false;
            }
            else
            {
                MainContext.RedirectMainControl( "ContentList.ascx", "" );
            }
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxLanguageControl.BubbleEvent += new EventHandler( Details_RefreshHandler );

        if (!MainContext.IsPostBack)
        {
            uxContentEnabledCheck.Checked = true;
            uxContentShowInSiteMapCheck.Checked = true;
            PopulateSubscriptionLevelControl();
        }
    }

    protected void uxEditButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                if (ContentID != "0")
                {
                    Vevo.Domain.Contents.Content content = DataAccessContext.ContentRepository.GetOne( uxLanguageControl.CurrentCulture, ContentID );

                    content = SetUpContent( content );

                    content = DataAccessContext.ContentRepository.Save( content );

                    uxMessage.DisplayMessage( Resources.ContentMessages.UpdateSuccess );

                    PopulateControls();

                }
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                Vevo.Domain.Contents.Content content = new Vevo.Domain.Contents.Content( uxLanguageControl.CurrentCulture );
                content = SetUpContent( content );
                content = DataAccessContext.ContentRepository.Save( content );

                uxMessage.DisplayMessage( Resources.ContentMessages.AddSuccess );
                ClearInputFields();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    #endregion

    #region Public
    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }
    #endregion
}
