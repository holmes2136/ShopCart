using System;
using System.Collections.Generic;
using System.Web.UI;
using Vevo.Domain;
using Vevo.Domain.Blogs;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebUI.Products;
using Vevo.WebUI.International;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;

public partial class Blog_Components_BlogComment : BaseProductListControl
{
    private string CurrentBlogID
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

    private string CustomerID
    {
        get
        {
            return StoreContext.Customer.CustomerID;
        }
    }

    protected void ScriptManager_Navigate( object sender, HistoryEventArgs e )
    {
        string args;


        int totalItems = 50;

        uxPagingControl.NumberOfPages = (int) System.Math.Ceiling( (double) totalItems / 1 );

        if (!string.IsNullOrEmpty( e.State["page"] ))
        {
            args = e.State["page"];
            uxPagingControl.CurrentPage = int.Parse( args );
        }
        else
        {
            uxPagingControl.CurrentPage = 1;
        }

        Refresh();
    }

    private IList<BlogComment> GetBlogCommentList( int itemPerPage, out int totalItems )
    {
        totalItems = 0;
        string sortOrder;
        if (DataAccessContext.Configurations.GetValue( "BlogCommentsSortOrder" ) == "Last to First")
            sortOrder = "CreatedDate DESC";
        else
            sortOrder = "CreatedDate ASC";

        return DataAccessContext.BlogCommentRepository.SearchBlogComment(
            CurrentBlogID,
            sortOrder,
            (uxPagingControl.CurrentPage - 1) * itemPerPage,
            (uxPagingControl.CurrentPage * itemPerPage) - 1,
            out totalItems,
            BoolFilter.ShowTrue );
    }


    private void AddHistoryPoint()
    {
        GetScriptManager().AddHistoryPoint( "page", uxPagingControl.CurrentPage.ToString() );
    }

    private ScriptManager GetScriptManager()
    {
        return (ScriptManager) Page.Master.FindControl( "uxScriptManager" );
    }

    protected void uxPagingControl_BubbleEvent( object sender, EventArgs e )
    {
        AddHistoryPoint();
        Refresh();
    }

    protected string GetUserName( object customerID )
    {
        return DataAccessContext.CustomerRepository.GetOne( customerID.ToString() ).UserName;
    }

    protected string GetShortDate( object createDate )
    {
        return ConvertUtilities.ToDateTime( createDate ).ToShortDateString();
    }

    protected void uxBlogPostButton_OnClick( object sender, EventArgs e )
    {
        string comment = uxBlogPostCommentArea.Text;

        if (!Page.User.Identity.IsAuthenticated)
        {
            Session["BlogCommentTemp"] = comment;
            string returnUrl = "~/Blog/BlogDetails.aspx?BlogID=" + CurrentBlogID; 
            Response.Redirect( "~/UserLogin.aspx?ReturnUrl=" + returnUrl );
        }

        comment = Regex.Replace( comment, @"<(.|\n)*?>", string.Empty );
        comment = comment.Replace( "\n", "<br />" );
        BlogComment blog = new BlogComment();
        blog.CreatedDate = DateTime.Now;
        blog.Enabled = true;
        blog.BlogID = CurrentBlogID;
        blog.UserName = Page.User.Identity.Name;
        blog.Comment = comment;

        DataAccessContext.BlogCommentRepository.Save( blog );
        uxBlogPostCommentArea.Text = "";
        
        if (DataAccessContext.Configurations.GetValue( "BlogCommentsSortOrder" ) == "Last to First")
            uxPagingControl.CurrentPage = 1;
        else
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
        Refresh();

    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxBlogPostButton.Text = GetLanguageText( "SubmitComment" );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        GetScriptManager().Navigate += new EventHandler<HistoryEventArgs>( ScriptManager_Navigate );

        if (!Page.IsPostBack)
        {
            if (DataAccessContext.Configurations.GetBoolValue( "BlogCommentEnabled" ))
            {
                uxBlogCommentDiv.Visible = true;
                Refresh();
            }

            if (Session["BlogCommentTemp"] != null)
            {
                uxBlogPostCommentArea.Text = Session["BlogCommentTemp"].ToString();
                Session.Remove( "BlogCommentTemp" );
            }

            if (DataAccessContext.Configurations.GetValue( "BlogCommentsSortOrder" ) == "Last to First")
                uxPagingControl.CurrentPage = 1;
            else
                uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }

    public void Refresh()
    {
        int totalItems;
        int selectedValue = DataAccessContext.Configurations.GetIntValue( "BlogCommentsPerPage" );
        uxBlogCommentGrid.DataSource = GetBlogCommentList( selectedValue, out totalItems );
        uxBlogCommentGrid.DataBind();
        uxDefaultTitle.Text = totalItems.ToString() + " comments for this topic";
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / selectedValue );
    }
}
