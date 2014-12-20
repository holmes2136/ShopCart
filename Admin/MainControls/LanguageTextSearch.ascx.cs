using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.Shared.DataAccess;
using Vevo.WebAppLib;

public partial class AdminAdvanced_MainControls_LanguageTextSearch : AdminAdvancedBaseListControl
{
    private const int MaximumPathLengthInLine = 25;

    private string TextSearch
    {
        get
        {
            if (ViewState["TextSearch"] != null)
                return ViewState["TextSearch"].ToString();
            else if (MainContext.QueryString["Keyword"] != null)
                return MainContext.QueryString["Keyword"];
            else
                return uxSearchText.Text;
        }
        set { ViewState["TextSearch"] = value; }
    }

    private string SearchFieldName
    {
        get
        {
            if (MainContext.QueryString["Field"] != null)
                return MainContext.QueryString["Field"];
            else if (ViewState["SearchFieldName"] != null)
                return ViewState["SearchFieldName"].ToString();
            else
                return uxSearchFieldsDrop.SelectedValue;
        }
        set { ViewState["SearchFieldName"] = value; }
    }

    private string CultureIDSearch
    {
        get
        {
            if (MainContext.QueryString["CultureID"] != null)
                return MainContext.QueryString["CultureID"];
            else if (ViewState["CultureIDSearch"] != null)
                return ViewState["CultureIDSearch"].ToString();
            else
                return uxSearchCultureIDDrop.SelectedValue;
        }
        set { ViewState["CultureIDSearch"] = value; }
    }

    private void PopulateControls()
    {
        if (IsAdminModifiable())
        {
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {
                uxDeleteButton.Attributes.Add(
                    "onclick",
                    "if (confirm('Are you sure to delete language keyword(s)?')) {} else {return false}" );
            }
        }
        else
        {
            uxDeleteButton.Visible = false;
        }
    }

    private void LoadSearchTextFromQuery()
    {
        if (MainContext.QueryString["Keyword"] != null)
        {
            uxSearchText.Text = MainContext.QueryString["Keyword"];
        }
    }

    private void LoadSearchFieldDropFromQuery()
    {
        if (MainContext.QueryString["Field"] != null)
        {
            uxSearchFieldsDrop.SelectedValue = MainContext.QueryString["Field"];
        }
    }

    private void LoadSearchCultureIDDropFromQuery()
    {
        if (MainContext.QueryString["CultureID"] != null)
        {
            uxSearchCultureIDDrop.SelectedValue = MainContext.QueryString["CultureID"];
        }
    }

    private void AdminAdvanced_MainControls_LanguageTextSearch_BrowseHistoryAdding( object sender, BrowseHistoryAddEventArgs e )
    {
        e.BrowseHistoryQuery.AddQuery( "Keyword", uxSearchText.Text );
        e.BrowseHistoryQuery.AddQuery( "Field", uxSearchFieldsDrop.SelectedValue );
        e.BrowseHistoryQuery.AddQuery( "CultureID", uxSearchCultureIDDrop.SelectedValue );
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.CustomerItemsPerPage;

            LoadSearchTextFromQuery();
            LoadSearchFieldDropFromQuery();
            // For LoadSearchCultureIDDropFromQuery(), since it is a dynamic control, we need to wait until control is populated
            // by DataSource first before setting the default value.
        }

        RegisterGridView( uxSearchGrid, "Path" );

        RegisterPagingControl( uxPagingControl );

        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );

        BrowseHistoryAdding += new BrowseHistoryAddEventHandler( AdminAdvanced_MainControls_LanguageTextSearch_BrowseHistoryAdding );
    }

    private bool IsLoadedFromBrowserHistory()
    {
        return !String.IsNullOrEmpty( MainContext.QueryString["Keyword"] );
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpGridSupportControls();

        WebUtilities.TieButton( Page, uxSearchText, uxSearchButton );

        if (!MainContext.IsPostBack)
        {
            uxAdminContent.ShowBorderContent = false;
            PopulateControls();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack &&
            IsLoadedFromBrowserHistory())
        {
            RefreshGrid();
        }
    }

    protected void uxSearchButton_Click( object sender, EventArgs e )
    {
        TextSearch = uxSearchText.Text;
        SearchFieldName = uxSearchFieldsDrop.SelectedValue;
        CultureIDSearch = uxSearchCultureIDDrop.SelectedValue;

        uxPagingControl.CurrentPage = 1;
        RefreshGrid();

        AddBrowseHistory();
    }

    protected void uxCultureDrop_PreRender( object sender, EventArgs e )
    {
        DropDownList cultureDrop = (DropDownList) sender;
        cultureDrop.SelectedValue = uxSearchGrid.DataKeys[uxSearchGrid.EditIndex]["CultureID"].ToString();
    }

    protected void uxEditLinkButton_PreRender( object sender, EventArgs e )
    {
        if (!IsAdminModifiable())
        {
            LinkButton linkButton = (LinkButton) sender;
            linkButton.Visible = false;
        }
    }

    protected void uxSearchGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            string pageID = ((HiddenField) uxSearchGrid.Rows[e.RowIndex].FindControl( "uxPageIDHidden" )).Value;
            string oldCultureID = ((HiddenField) uxSearchGrid.Rows[e.RowIndex].FindControl( "uxOldCultureIDHidden" )).Value;
            string oldKeyName = ((HiddenField) uxSearchGrid.Rows[e.RowIndex].FindControl( "uxOldKeyNameHidden" )).Value;
            string newCultureID = ((DropDownList) uxSearchGrid.Rows[e.RowIndex].FindControl( "uxCultureDrop" )).SelectedValue;
            string newKeyName = ((TextBox) uxSearchGrid.Rows[e.RowIndex].FindControl( "uxKeyNameText" )).Text.Trim();
            string newTextData = ((TextBox) uxSearchGrid.Rows[e.RowIndex].FindControl( "uxTextDataText" )).Text.Trim();

            LanguageTextAccess.Update(
                pageID,
                oldCultureID,
                oldKeyName,
                newCultureID,
                newKeyName,
                newTextData );

            // End editing
            uxSearchGrid.EditIndex = -1;

            uxMessage.DisplayMessage( Resources.LanguagePageMessages.KeywordUpdateSuccess );
            RefreshGrid();
        }
        catch (Exception ex)
        {
            string message;
            if (ex.InnerException is DuplicatedPrimaryKeyException)
                message = Resources.LanguagePageMessages.KeywordUpdateErrorDuplicated;
            else
                message = Resources.LanguagePageMessages.KeywordUpdateError;
            throw new ApplicationException( message );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;
        }

        AdminUtilities.ClearLanguageCache();
    }

    protected void uxSearchGrid_RowEditing( object sender, GridViewEditEventArgs e )
    {
        uxSearchGrid.EditIndex = e.NewEditIndex;
        RefreshGrid();
    }

    protected void uxSearchGrid_RowCancelingEdit( object sender, GridViewCancelEditEventArgs e )
    {
        uxSearchGrid.EditIndex = -1;
        RefreshGrid();
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        bool deleted = false;
        foreach (GridViewRow row in uxSearchGrid.Rows)
        {
            CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
            if (deleteCheck != null &&
                deleteCheck.Checked)
            {
                string pageID = ((HiddenField) row.FindControl( "uxPageIDHidden" )).Value;
                string cultureID = ((HiddenField) row.FindControl( "uxOldCultureIDHidden" )).Value;
                string keyName = ((HiddenField) row.FindControl( "uxOldKeyNameHidden" )).Value;

                LanguageTextAccess.Delete( pageID, cultureID, keyName );
                deleted = true;
            }
        }
        uxSearchGrid.EditIndex = -1;

        if (deleted)
            uxMessage.DisplayMessage( Resources.LanguagePageMessages.KeywordDeleteSuccess );

        RefreshGrid();
        AdminUtilities.ClearLanguageCache();
    }

    protected void uxSearchCultureIDDrop_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            DropDownList drop = (DropDownList) sender;
            drop.Items.Insert( 0, new ListItem( "(All)", "" ) );

            LoadSearchCultureIDDropFromQuery();
        }
    }

    protected string GetResultPath( object objPath )
    {
        string path = objPath.ToString();
        if (path.Length > MaximumPathLengthInLine)
        {
            if (path.Contains( "/" ))
            {
                int position = path.LastIndexOf( '/' );
                if (position != 0)
                    path = path.Insert( position + 1, "<br />" );
            }
            return path;
        }
        else
            return path;
    }

    protected override void uxPagingControl_BubbleEvent( object sender, EventArgs e )
    {
        uxSearchGrid.EditIndex = -1;

        base.uxPagingControl_BubbleEvent( sender, e );
    }

    protected override void RefreshGrid()
    {
        uxAdminContent.ShowBorderContent = true;
        int totalItems;

        uxSearchGrid.DataSource = LanguageTextAccess.Search(
            CultureIDSearch,
            GridHelper.GetFullSortText(),
            SearchFieldName,
            TextSearch,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxSearchGrid.DataBind();
    }

}
