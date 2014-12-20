using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain;
using Vevo.Domain.Blogs;
using Vevo.Base.Domain;

public partial class Admin_MainControls_BlogCommentList : AdminAdvancedBaseListControl
{
    private const int ColumnBlogCommentID = 1;

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.BlogCommentRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list, "BlogID");
    }

    private void PopulateControls()
    {
        RefreshGrid();

        if (uxGridBlogComment.Rows.Count > 0)
        {
            uxPagingControl.Visible = true;
            DeleteVisible( true );
        }
        else
        {
            uxPagingControl.Visible = false;
            DeleteVisible( false );
        }

        if (!IsAdminModifiable())
        {
            DeleteVisible( false );
        }
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.BlogCommentItemsPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxGridBlogComment, "BlogCommentID" );
  
        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }

    protected string BlogCommentDateMessage( string createDate )
    {
        return String.Format( "{0}", Convert.ToDateTime( createDate ).ToShortDateString() );
    }

    private void DeleteVisible( bool value )
    {
        uxDeleteButton.Visible = value;
        if (value)
        {
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {
                uxDeleteConfirmButton.TargetControlID = "uxDeleteButton";
                uxConfirmModalPopup.TargetControlID = "uxDeleteButton";
            }
            else
            {
                uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
                uxConfirmModalPopup.TargetControlID = "uxDummyButton";
            }
        }
        else
        {
            uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
            uxConfirmModalPopup.TargetControlID = "uxDummyButton";
        }
    }

    protected string CurrentBlogID
    {
        get
        {
            return MainContext.QueryString["BlogID"];
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxGridBlogComment.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string id = ((Label)row.FindControl( "uxBlogCommentID" )).Text;
                    DataAccessContext.BlogCommentRepository.Delete( id );

                    deleted = true;
                }
            }

            if (deleted)
                uxMessage.DisplayMessage( Resources.BlogCommentMessages.DeleteSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if (uxGridBlogComment.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }

        uxStatusHidden.Value = "Deleted";
    }

    protected override void RefreshGrid()
    {
        int totalItems;

        uxGridBlogComment.DataSource = DataAccessContext.BlogCommentRepository.SearchBlogComment(
            CurrentBlogID,
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems,
            BoolFilter.ShowAll );

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGridBlogComment.DataBind();
    }

    protected void uxGrid_RowEditing( object sender, GridViewEditEventArgs e )
    {
        uxGridBlogComment.EditIndex = e.NewEditIndex;
        RefreshGrid();
    }

    protected void uxGrid_CancelingEdit( object sender, GridViewCancelEditEventArgs e )
    {
        uxGridBlogComment.EditIndex = -1;
        RefreshGrid();
    }

    protected void uxGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            string id = ((Label) uxGridBlogComment.Rows[e.RowIndex].FindControl( "uxBlogCommentID" )).Text;
            string comment = ((TextBox) uxGridBlogComment.Rows[e.RowIndex].FindControl( "uxCommentText" )).Text;
            bool enabled = ((CheckBox) uxGridBlogComment.Rows[e.RowIndex].FindControl( "uxIsEnabledCheck" )).Checked;
            if (!String.IsNullOrEmpty( id ))
            {
                BlogComment blogComment = DataAccessContext.BlogCommentRepository.GetOne( id );
                blogComment.Comment = comment;
                blogComment.Enabled = enabled;
                blogComment = DataAccessContext.BlogCommentRepository.Save( blogComment );
                uxMessage.DisplayMessage( Resources.BlogCommentMessages.UpdateSuccess );
                uxGridBlogComment.EditIndex = -1;
                RefreshGrid();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;
        }
    }
}
