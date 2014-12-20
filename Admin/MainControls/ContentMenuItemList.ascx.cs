using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.Domain.DataInterfaces;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.ServerControls;
using Vevo.Domain.Stores;
using Vevo.Base.Domain;

public partial class AdminAdvanced_MainControls_ContentMenuItemList : AdminAdvancedBaseListControl
{
    #region Private

    private const int ColumnContentMenuItemID = 1;
    private const int ColumnTitle = 2;
    private const int ContentMenuIndentInPixels = 20;
    private IList<ContentMenuItem> _allContentMenuItem, _searchResult;
    private DataTable _contentMenuSource = new DataTable( "ContentMenu" );

    private Store CurrentStore
    {
        get
        {
            return DataAccessContext.StoreRepository.GetOne( StoreID );
        }
    }

    private string StoreID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
            {
                if (MainContext.QueryString["StoreID"] != null)
                    return MainContext.QueryString["StoreID"];
                else
                    return Store.RegularStoreID;
            }
            else
            {
                return Store.RegularStoreID;
            }
        }
    }

    private string ContentMenuID
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["ContentMenuID"] ))
                return MainContext.QueryString["ContentMenuID"];
            else
                return String.Empty;
        }

    }

    private string Position
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["Position"] ))
                return MainContext.QueryString["Position"];
            else
                return String.Empty;
        }
    }

    private void CreateTable()
    {
        _contentMenuSource.Columns.Add( "ContentMenuItemID", typeof( Int32 ) );
        _contentMenuSource.Columns.Add( "Name", typeof( string ) );
        _contentMenuSource.Columns.Add( "Description", typeof( string ) );
        _contentMenuSource.Columns.Add( "ContentMenuID", typeof( string ) );
        _contentMenuSource.Columns.Add( "ContentID", typeof( string ) );
        _contentMenuSource.Columns.Add( "ReferringMenuID", typeof( string ) );
        _contentMenuSource.Columns.Add( "Level", typeof( Int32 ) );
        _contentMenuSource.Columns.Add( "IsEnabled", typeof( Boolean ) );
    }


    private ContentMenuItem GetReferringContentMenuItem( Culture culture, string id )
    {
        IList<ContentMenuItem> list = DataAccessContext.ContentMenuItemRepository.GetAll( culture, BoolFilter.ShowAll );
        foreach (ContentMenuItem item in list)
        {
            if (item.ReferringMenuID == id)
                return item;
        }
        return ContentMenuItem.Null;
    }

    private void SetUpMenuLocationHeader()
    {
        if (KeyUtilities.IsMultistoreLicense())
            uxBreadcrumbLabel.Text = "Store: " + CurrentStore.StoreName + " (" + CurrentStore.StoreID + "), Position: " + Position + " Menu";
        else
            uxBreadcrumbLabel.Text = Position + " Menu";
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.ContentMenuItemRepository.GetTableSchema();
        NameValueCollection renameList = new NameValueCollection();
        renameList.Add( "IsEnabled", "Enabled" );
        uxSearchFilter.SetUpSchema( list, renameList, "ContentMenuID", "StoreID" );
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }

        if (uxGrid.Rows.Count > 0)
        {
            DeleteVisible( true );
        }
        else
        {
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
            SetUpSearchFilter();
        }

        RegisterGridView( uxGrid, "ContentMenuItemID" );

        RegisterLanguageControl( uxLanguageControl );
        RegisterSearchFilter( uxSearchFilter );

        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
    }

    private string[] GetCheckedIDs()
    {
        ArrayList items = new ArrayList();

        foreach (GridViewRow row in uxGrid.Rows)
        {
            CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
            if (deleteCheck.Checked)
            {
                //    string ID = row.Cells[ColumnContentMenuItemID].Text.Trim();
                string ID = ((HiddenField) row.Cells[0].FindControl( "uxContentMenuItemIDHidden" )).Value.Trim();

                items.Add( ID );
            }
        }

        string[] result = new string[items.Count];
        items.CopyTo( result );
        return result;
    }

    private bool ContainsContentMenuItem( string[] idArray, out string containingContentMenuItemID )
    {
        foreach (string ID in idArray)
        {
            ContentMenuItem contentmenuitem = DataAccessContext.ContentMenuItemRepository.GetOne(
                uxLanguageControl.CurrentCulture, ID );
            IList<ContentMenuItem> contentmenuitem_list = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
                uxLanguageControl.CurrentCulture, contentmenuitem.ReferringMenuID, "", BoolFilter.ShowAll );

            if (contentmenuitem_list.Count > 0)
            {
                containingContentMenuItemID = ID;
                return true;
            }
        }
        containingContentMenuItemID = "";
        return false;
    }

    private void UpdateSortOrders( string parentID )
    {
        IList<ContentMenuItem> list = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
                uxLanguageControl.CurrentCulture, parentID, "SortOrder", BoolFilter.ShowTrue );
        string[] result = new string[list.Count];
        for (int i = 0; i < list.Count; i++)
        {
            result[i] = list[i].ContentMenuItemID;
        }
        DataAccessContext.ContentMenuItemRepository.UpdateSortOrder( result );
    }

    private void DeleteItems()
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string contentMenuItemID = ((HiddenField) row.Cells[0].FindControl( "uxContentMenuItemIDHidden" )).Value.Trim();
                    // string parentId = ((HiddenField) row.Cells[0].FindControl( "uxContentMenuIDHidden" )).Value.Trim();
                    ContentMenuItem item = DataAccessContext.ContentMenuItemRepository.GetOne(
                        uxLanguageControl.CurrentCulture, contentMenuItemID );

                    string parentID = item.ContentMenuID;
                    DataAccessContext.ContentMenuRepository.Delete(
                        item.ReferringMenuID );
                    DataAccessContext.ContentMenuItemRepository.Delete( contentMenuItemID );

                    UpdateSortOrders( parentID );
                    deleted = true;
                }
            }

            if (deleted)
            {
                uxMessage.DisplayMessage( Resources.ContentMenuItemMessages.DeleteSuccess );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();


    }

    private void BuildData( string parentID, int level )
    {
        IList<ContentMenuItem> dataRows = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
            uxLanguageControl.CurrentCulture, parentID, "SortOrder", BoolFilter.ShowAll );

        foreach (ContentMenuItem item in dataRows)
        {
            BuildRow( item, level );
            if (item.ReferringMenuID != "0")
                BuildData( item.ReferringMenuID, level + 1 );
        }
    }

    private void BuildRow( ContentMenuItem item, int level )
    {
        DataRow row;
        row = _contentMenuSource.NewRow();
        row["ContentMenuItemID"] = item.ContentMenuItemID;
        row["Name"] = item.Name;
        row["Description"] = item.Description;
        row["ContentMenuID"] = item.ContentMenuID;
        row["ContentID"] = item.ContentID;
        row["ReferringMenuID"] = item.ReferringMenuID;
        row["Level"] = level;
        row["IsEnabled"] = item.IsEnabled;
        _contentMenuSource.Rows.Add( row );
    }

    #endregion


    #region Protected

    protected string PageQueryString( string itemID )
    {
        return String.Format( "ContentMenuItemID={0}&ContentMenuID={1}&Position={2}&StoreID={3}", itemID, GetRoot(), Position, StoreID );
    }

    protected string GetRoot()
    {
        return ContentMenuID;
    }

    protected string GetName( string name, string id, string level )
    {
        string showname = name;
        if (level == "0" && uxSearchFilter.SearchFilterObj.FieldName == "")
            return "<b>" + showname + "</b>";
        else
            return showname;
    }

    protected string ContentMenuItemID
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["ContentMenuItemID"] ))
                return MainContext.QueryString["ContentMenuItemID"];
            else
                return "0";
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpMenuLocationHeader();
        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl(
            "ContentMenuItemAdd.ascx",
            String.Format( "ContentMenuID={0}&Position={1}&StoreID={2}", ContentMenuID, Position, CurrentStore.StoreID ) );
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        string[] checkedIDs = GetCheckedIDs();
        string containingID;


        if (ContainsContentMenuItem( checkedIDs, out containingID ))
        {
            ContentMenuItem contentmenuitem
                = DataAccessContext.ContentMenuItemRepository.GetOne( uxLanguageControl.CurrentCulture, containingID );
            uxMessage.DisplayError(
                Resources.ContentMenuItemMessages.DeleteErrorContainingItem,
                contentmenuitem.Name,
                contentmenuitem.ContentMenuItemID );
        }
        else
        {
            DeleteItems();
        }
    }

    protected override void RefreshGrid()
    {
        CreateTable();

        if (uxSearchFilter.SearchFilterObj.Value1 == "" && uxSearchFilter.SearchFilterObj.Value2 == "")
        {
            _allContentMenuItem = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
                uxLanguageControl.CurrentCulture, ContentMenuID, "SortOrder", BoolFilter.ShowAll );

            BuildData( ContentMenuID, 0 );

        }
        else
        {
            _searchResult = DataAccessContext.ContentMenuItemRepository.SearchContentMenuItem(
                 uxLanguageControl.CurrentCulture,
                 Position.ToLower(),
                 "SortOrder",
                 uxSearchFilter.SearchFilterObj );

            foreach (ContentMenuItem item in _searchResult)
            {
                BuildRow( item, 0 );
            }

        }
        uxGrid.DataSource = _contentMenuSource;
        uxGrid.DataBind();
    }

    protected string GetSpaceStyle( object level )
    {
        if (uxSearchFilter.SearchFilterObj.FieldName == "")
            return "float: left; width: " + (ContentMenuIndentInPixels * ConvertUtilities.ToInt32( level )) + "px;";
        else
            return "float: left;";
    }

    protected string GetImage( string id )
    {
        ContentMenuItem item = DataAccessContext.ContentMenuItemRepository.GetOne(
            AdminUtilities.CurrentCulture, id );
        if (item.ReferringMenuID == "0")
            return "SmallContent";
        else
            return "SmallCategory";
    }

    protected void uxSortButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl(
            "ContentMenuSorting.ascx",
            String.Format( "ContentMenuID={0}&Position={1}&StoreID={2}", ContentMenuID, Position, CurrentStore.StoreID ) );
    }

    #endregion
}
