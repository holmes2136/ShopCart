using System;
using System.Collections;
using System.Collections.Generic;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.Shared.Utilities;
using Vevo.WebUI;

public partial class Components_ContentNavList : Vevo.WebUI.Contents.BaseContentMenuNavListUserControl
{
    private string _rootID = "0";
    private string _menuType = "default";
    private string _position = "right";

    private void SetPositionParameter()
    {
        switch (Position)
        {
            case "top":
                _rootID = DataAccessContext.Configurations.GetValue( "TopContentMenu" );
                _menuType = DataAccessContext.Configurations.GetValue( "TopContentMenuType" );

                break;

            case "left":                
                _rootID = DataAccessContext.Configurations.GetValue( "LeftContentMenu" );
                _menuType = DataAccessContext.Configurations.GetValue( "LeftContentMenuType" );
                break;

            case "right":                
                _rootID = DataAccessContext.Configurations.GetValue( "RightContentMenu" );
                _menuType = DataAccessContext.Configurations.GetValue( "RightContentMenuType" );
                break;
        }
    }

    private void PopulateControls()
    {
        SetPositionParameter();
        //_rootID = DataAccessContext.Configurations.GetValue( _rootMenu );

        IList<ContentMenuItem> originalList = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
            StoreContext.Culture, _rootID, "SortOrder", BoolFilter.ShowTrue );
        IList<ContentMenuItem> list = ListUtilities<ContentMenuItem>.CopyListDeep( originalList );
        if (_menuType == "default")
        {
            foreach (ContentMenuItem item in list)
            {
                if (!item.LinksToContent())
                    item.Name = item.Name + "...";
            }

            uxList.Visible = true;
            uxList.DataSource = list;
            uxList.DataBind();
        }

    }


    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    protected string GetNavName( object item )
    {
        return ((ContentMenuItem) item).Name;
    }

    protected string GetNavUrl( object item )
    {

        ContentMenuItem currentItem = DataAccessContext.ContentMenuItemRepository.GetOne(
            StoreContext.Culture, ((ContentMenuItem) item).ContentMenuItemID );

        if (!currentItem.LinksToContent())
            return UrlManager.GetContentMenuUrl( currentItem.ContentMenuItemID, currentItem.UrlName );
        else
        {
            Vevo.Domain.Contents.Content content = DataAccessContext.ContentRepository.GetOne(
                StoreContext.Culture, ((ContentMenuItem) item).ContentID.ToString() );
            return UrlManager.GetContentUrl( content.ContentID, content.UrlName );
        }

    }

    public void Refresh()
    {
        PopulateControls();
    }

    public string Position
    {
        get
        {
            return _position;
        }
        set
        {
            _position = value;
        }
    }
}