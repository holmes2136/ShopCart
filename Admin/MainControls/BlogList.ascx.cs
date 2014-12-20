using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Base.Domain;
using Vevo.Domain;
using Vevo.Domain.Stores;
using Vevo.WebUI.ServerControls;

public partial class Admin_MainControls_BlogList : AdminAdvancedBaseListControl
{
    private const int ColumnBlogID = 1;

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.BlogRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list );
    }

    private void PopulateControls()
    {
        RefreshGrid();

        if (uxGridBlog.Rows.Count > 0)
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
            uxAddButton.Visible = false;
            DeleteVisible( false );
        }
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

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.BlogItemPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxGridBlog, "BlogID" );

        RegisterCustomControl( uxStoreFilterDrop );
        uxStoreFilterDrop.BubbleEvent += new EventHandler( uxStoreFilterDrop_BubbleEvent );

        //RegisterLanguageControl( uxLanguageControl );
        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

        //uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }

    protected string BlogDateMessage( string createDate )
    {
        return String.Format( "{0}", Convert.ToDateTime( createDate ).ToShortDateString() );
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
            foreach (GridViewRow row in uxGridBlog.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string id = row.Cells[ColumnBlogID].Text.Trim();

                    DataAccessContext.BlogRepository.Delete( id );

                    deleted = true;
                }
            }

            if (deleted)
                uxMessage.DisplayMessage( Resources.NewsMessage.DeleteSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if (uxGridBlog.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }

        uxStatusHidden.Value = "Deleted";
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "BlogAdd.ascx" );
    }

    protected void ChangePageComment_Click( object sender, EventArgs e )
    {
        AdvancedLinkButton linkButton = (AdvancedLinkButton) sender;
        GridViewRow row = (GridViewRow)linkButton.Parent.Parent;
        string id = row.Cells[ColumnBlogID].Text.Trim();
        MainContext.RedirectMainControl( "BlogCommentList.ascx","BlogID=" + id );
    }

    protected override void RefreshGrid()
    {
        int totalItems;
        
        uxGridBlog.DataSource = DataAccessContext.BlogRepository.SearchBlog(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems,
            StoreID,
            true,
            BoolFilter.ShowAll );        

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGridBlog.DataBind();
    }

    #region Public
    public string StoreID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
            {
                if (MainContext.QueryString["StoreID"] != null)
                    return MainContext.QueryString["StoreID"];
                else
                    return uxStoreFilterDrop.SelectedValue;
            }
            else
            {
                return Store.RegularStoreID;
            }
        }
    }
    #endregion
}
