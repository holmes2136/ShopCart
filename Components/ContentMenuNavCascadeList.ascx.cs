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
using Vevo.Data;
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.WebUI;
using Vevo.WebAppLib;

public partial class Components_ContentMenuNavCascadeList
    : Vevo.WebUI.Contents.BaseContentMenuNavListUserControl
{
    private string _rootID = "0";
    private string _rootMenuName = "";
    private const int _maxNode = 5;


    private string CurrentContentMenuItemID
    {
        get
        {
            string id = Request.QueryString["ContentMenuID"];
            if (String.IsNullOrEmpty( id ))
                return "0";
            else
                return id;
        }
    }

    private string CurrentContentMenuItemName
    {
        get
        {
            if (Request.QueryString["ContentMenuItemName"] == null)
                return String.Empty;
            else
                return Request.QueryString["ContentMenuItemName"];
        }
    }

    private void PopulateControls()
    {
        uxContentMenuNavList.Items.Clear();
        uxContentMenuNavListTop.Items.Clear();
        uxContentMenuNavListTop.MaximumDynamicDisplayLevels = _maxNode;
        uxContentMenuNavList.MaximumDynamicDisplayLevels = _maxNode;

        IList<ContentMenuItem> list = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
           StoreContext.Culture, _rootID, "SortOrder", BoolFilter.ShowTrue );

        if (_rootID == DataAccessContext.Configurations.GetValue( "TopContentMenu" ))
        {
            uxContentMenuNavList.Visible = false;
            uxContentMenuNavListTop.Visible = true;

            ContentMenuItem rootItem = DataAccessContext.ContentMenuItemRepository.GetOne(
                StoreContext.Culture, ContentMenuItem.RootMenuItemID );

            MenuItem rootMenu = new MenuItem();
            rootMenu.Text = ContentMenuItem.RootMenuItemName;
            rootMenu.NavigateUrl = UrlManager.GetContentMenuUrl( rootItem.ContentMenuItemID,
                rootItem.UrlName );

            foreach (ContentMenuItem contentMenuItem in list)
            {
                rootMenu.ChildItems.Add( CreateMenuItemWithChildren( 0, contentMenuItem ) );
            }
            uxContentMenuNavListTop.Items.Add( rootMenu );
            uxContentMenuNavListTop.Orientation = Orientation.Horizontal;

            foreach (MenuItem mi in uxContentMenuNavListTop.Items)
            {
                SetSelectedContentMenuItem( mi, CurrentContentMenuItemID, CurrentContentMenuItemName );
            }
        }
        else
        {
            uxContentMenuNavListTop.Visible = false;
            foreach (ContentMenuItem contentMenuItem in list)
            {
                uxContentMenuNavList.Items.Add( CreateMenuItemWithChildren( 0, contentMenuItem ) );
            }
            uxContentMenuNavList.Orientation = Orientation.Vertical;
            foreach (MenuItem mi in uxContentMenuNavList.Items)
            {
                SetSelectedContentMenuItem( mi, CurrentContentMenuItemID, CurrentContentMenuItemName );
            }
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControls();
        }
    }



    public string RootID
    {
        get
        {
            return _rootID;
        }
        set
        {
            _rootID = value;
        }
    }

    public string RootMenuName
    {
        get
        {
            return _rootMenuName;
        }
        set
        {
            _rootMenuName = value;
        }
    }

    public void Refresh()
    {
        PopulateControls();
    }
}
