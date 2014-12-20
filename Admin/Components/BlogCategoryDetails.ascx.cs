using System;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Blogs;

public partial class Admin_Components_BlogCategoryDetails : AdminAdvancedBaseUserControl
{
    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string CurrentID
    {
        get
        {
            return MainContext.QueryString["BlogCategoryID"];
        }
    }

    private void ClearInputFields()
    {
        uxNameText.Text = "";
        uxDescriptionText.Text = "";
        uxMetaKeywordText.Text = "";
        uxMetaDescriptionText.Text = "";
        uxIsEnabledCheck.Checked = true;
    }

    private void PopulateControls()
    {
        if (CurrentID != null &&
             int.Parse( CurrentID ) >= 0)
        {
            BlogCategory blogCategory = DataAccessContext.BlogCategoryRepository.GetOne(
                uxLanguageControl.CurrentCulture, CurrentID );
            uxNameText.Text = blogCategory.Name;
            uxDescriptionText.Text = blogCategory.Description;
            uxMetaKeywordText.Text = blogCategory.MetaKeyword;
            uxMetaDescriptionText.Text = blogCategory.MetaDescription;
            uxIsEnabledCheck.Checked = blogCategory.IsEnabled;
        }
    }

    private BlogCategory SetUpBlogCategory( BlogCategory blogcategory )
    {
        blogcategory.Name = uxNameText.Text;
        blogcategory.Description = uxDescriptionText.Text;
        blogcategory.IsEnabled = uxIsEnabledCheck.Checked;
        blogcategory.MetaKeyword = uxMetaKeywordText.Text;
        blogcategory.MetaDescription = uxMetaDescriptionText.Text;

        return blogcategory;
    }

    private void AddBlogCategory()
    {
        try
        {
            if (Page.IsValid)
            {
                BlogCategory blogcategory = new BlogCategory( uxLanguageControl.CurrentCulture );
                blogcategory = SetUpBlogCategory( blogcategory );
                blogcategory = DataAccessContext.BlogCategoryRepository.Save( blogcategory );

                uxMessage.DisplayMessage( Resources.BlogMessage.AddSuccess );
                ClearInputFields();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        uxStatusHidden.Value = "Added";

        AdminUtilities.ClearSiteMapCache();
    }

    private void UpdateBlogCategory()
    {
        try
        {
            if (Page.IsValid)
            {
                BlogCategory blogcategory = DataAccessContext.BlogCategoryRepository.GetOne(
                    uxLanguageControl.CurrentCulture, CurrentID );
                blogcategory = SetUpBlogCategory( blogcategory );
                blogcategory = DataAccessContext.BlogCategoryRepository.Save( blogcategory );

                uxMessage.DisplayMessage( Resources.CategoryMessages.UpdateSuccess );
                PopulateControls();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        uxStatusHidden.Value = "Updated";

        AdminUtilities.ClearSiteMapCache();
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private void RegisterScript()
    {
        string script = "<script type=\"text/javascript\">" +
            "function ismaxlength(obj){" +
            "var mlength= 255;" +
            "if (obj.value.length > mlength)" +
            "obj.value=obj.value.substring(0,mlength);" +
            "}" +
            "</script>";

        ScriptManager.RegisterStartupScript( this, this.GetType(), "CheckLength", script, false );

        uxDescriptionText.Attributes.Add( "onkeyup", "return ismaxlength(this)" );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (IsEditMode())
        {
            if (!MainContext.IsPostBack)
            {
                PopulateControls();
            }

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
        else
        {
            if (IsAdminModifiable())
            {
                uxAddButton.Visible = true;
                uxUpdateButton.Visible = false;
            }
            else
            {
                MainContext.RedirectMainControl( "BlogCategoryList.ascx" );
            }
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterScript();
        uxLanguageControl.BubbleEvent += new EventHandler( Details_RefreshHandler );
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        AddBlogCategory();
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        UpdateBlogCategory();
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }
}