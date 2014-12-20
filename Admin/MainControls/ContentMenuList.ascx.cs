using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.WebAppLib;
using Vevo.Data;
using Vevo.Domain.Stores;

public partial class AdminAdvanced_MainControls_ContentMenu : AdminAdvancedBaseListControl
{
    #region Private

    private Store CurrentStore
    {
        get
        {
            return DataAccessContext.StoreRepository.GetOne( StoreID );
        }
    }

    private string MenuType
    {
        get
        {
            return (string) ViewState["MenuType"];
        }
        set
        {
            ViewState["MenuType"] = value;
        }
    }

    private void SetUpGridSupportControls()
    {
        RegisterGridView( uxTopContentMenuGrid, "ContentMenuID" );
        RegisterStoreFilterDrop( uxStoreFilterDrop );
        uxStoreFilterDrop.BubbleEvent += new EventHandler( uxStoreFilterDrop_BubbleEvent );
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }
    }

    private void PopulateTypeDropDown()
    {
        if (uxTopContentMenuGrid.EditIndex != -1)
        {
            Label menuTypeLabel =
                (Label) (uxTopContentMenuGrid.Rows[uxTopContentMenuGrid.EditIndex].Cells[2].FindControl( "uxTypeLabel" ));

            // If label is not found, this control is already in Edit mode
            if (menuTypeLabel != null)
                MenuType = menuTypeLabel.Text;
        }
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

    #endregion


    #region Protected

    protected string GetName( string contentMenuID )
    {
        Culture culture
            = DataAccessContext.CultureRepository.GetOne( DataAccessContext.CultureRepository.GetIDByName( SystemConst.USCultureName ) );
        ContentMenuItem item = GetReferringContentMenuItem( culture, contentMenuID );
        return item.Name;
    }

    protected string GetPageQueryString( string contentMenuID )
    {
        return String.Format(
            "ContentMenuID={0}&Position={1}&StoreID={2}", contentMenuID, GetName( contentMenuID ), CurrentStore.StoreID );
    }

    protected string GetType( string contentMenuID )
    {
        string type = "";
        if (contentMenuID == DataAccessContext.Configurations.GetValue( "TopContentMenu", CurrentStore ))
            type = DataAccessContext.Configurations.GetValue( "TopContentMenuType", CurrentStore );
        else if (contentMenuID == DataAccessContext.Configurations.GetValue( "LeftContentMenu", CurrentStore ))
            type = DataAccessContext.Configurations.GetValue( "LeftContentMenuType", CurrentStore );
        else if (contentMenuID == DataAccessContext.Configurations.GetValue( "RightContentMenu", CurrentStore ))
            type = DataAccessContext.Configurations.GetValue( "RightContentMenuType", CurrentStore );
        return type;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsMultistoreLicense())
        {
            uxStoreFilterDrop.Visible = false;
        }
        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected override void RefreshGrid()
    {
        IList<ContentMenu> topContentMenu = new List<ContentMenu>();

        topContentMenu.Add( DataAccessContext.ContentMenuRepository.GetOne(
            DataAccessContext.Configurations.GetValue( "TopContentMenu", CurrentStore ) ) );
        topContentMenu.Add( DataAccessContext.ContentMenuRepository.GetOne(
            DataAccessContext.Configurations.GetValue( "LeftContentMenu", CurrentStore ) ) );
        topContentMenu.Add( DataAccessContext.ContentMenuRepository.GetOne(
            DataAccessContext.Configurations.GetValue( "RightContentMenu", CurrentStore ) ) );

        uxTopContentMenuGrid.DataSource = topContentMenu;
        uxTopContentMenuGrid.DataBind();
    }

    protected void uxTopContentMenuGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            GridViewRow rowGrid = uxTopContentMenuGrid.Rows[e.RowIndex];

            string contentmenuID = ((Label) rowGrid.FindControl( "uxContentMenuIDLabel" )).Text;

            ContentMenu contentMenu = DataAccessContext.ContentMenuRepository.GetOne( contentmenuID );
            contentMenu.IsEnabled = ((CheckBox) rowGrid.FindControl( "uxContentEnabledCheck" )).Checked;

            ContentMenuItem contentMenuItem = GetReferringContentMenuItem( DataAccessContext.CultureRepository.GetOne(
            DataAccessContext.CultureRepository.GetIDByName( SystemConst.USCultureName ) ), contentmenuID );
            contentMenuItem.IsEnabled = contentMenu.IsEnabled;

            DataAccessContext.ContentMenuItemRepository.Save( contentMenuItem );

            string menuType = ((DropDownList) rowGrid.FindControl( "uxContentMenuTypeDrop" )).SelectedValue;

            DataAccessContext.ContentMenuRepository.Save( contentMenu );

            Store store = DataAccessContext.StoreRepository.GetOne( uxStoreFilterDrop.SelectedValue );

            if (contentmenuID == DataAccessContext.Configurations.GetValue( "TopContentMenu", CurrentStore ))
                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["TopContentMenuType"], menuType, CurrentStore );
            else if (contentmenuID == DataAccessContext.Configurations.GetValue( "LeftContentMenu", CurrentStore ))
                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["LeftContentMenuType"], menuType, CurrentStore );
            else if (contentmenuID == DataAccessContext.Configurations.GetValue( "RightContentMenu", CurrentStore ))
                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["RightContentMenuType"], menuType, CurrentStore );


            AdminUtilities.LoadSystemConfig();

            uxTopContentMenuGrid.EditIndex = -1;
            RefreshGrid();

            uxMessage.DisplayMessage( Resources.ContentMenuItemMessages.ItemUpdateSuccess );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;
        }
    }

    protected void uxTopContentMenuGrid_RowEditing( object sender, GridViewEditEventArgs e )
    {
        uxTopContentMenuGrid.EditIndex = e.NewEditIndex;
        PopulateTypeDropDown();
        RefreshGrid();
    }

    protected void uxTopContentMenuGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        if (e.CommandName == "Edit")
            PopulateTypeDropDown();
    }

    protected void uxContentMenuTypeDrop_PreRender( object sender, EventArgs e )
    {
        DropDownList typeMenu = (DropDownList) sender;
        typeMenu.SelectedValue = MenuType;
    }

    protected void uxTopContentMenuGrid_CancelingEdit( object sender, GridViewCancelEditEventArgs e )
    {
        uxTopContentMenuGrid.EditIndex = -1;
        RefreshGrid();
    }

    #endregion

    #region Public

    public string StoreID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
            {
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
