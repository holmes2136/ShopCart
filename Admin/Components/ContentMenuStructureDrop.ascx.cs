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
using Vevo.Domain;
using Vevo.Domain.Contents;


public partial class AdminAdvanced_Components_ContentMenuStructureDrop : AdminAdvancedBaseUserControl
{
    #region Private

    private IList<ContentMenuItem> GetMenuList( IList<ContentMenuItem> allMenuList, string parentID, Culture culture )
    {
        IList<ContentMenuItem> list = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
            culture, parentID, "SortOrder", BoolFilter.ShowAll );
        foreach (ContentMenuItem item in list)
        {
            if (item.ReferringMenuID != "0")
            {
                allMenuList.Add( item );
                allMenuList = GetMenuList( allMenuList, item.ReferringMenuID, culture );
            }
        }
        return allMenuList;
    }

    private void InitDropDownList()
    {
        uxDrop.Items.Clear();
        uxDrop.Items.Add( new ListItem( "Root", RootContentMenuID, true ) );
        Culture culture = DataAccessContext.CultureRepository.GetOne( CultureID );
        ContentMenuItem currentItem = DataAccessContext.ContentMenuItemRepository.GetOne(
            culture ,ContentMenuItemIDToExclude );
        IList<ContentMenuItem> allMenuList = new List<ContentMenuItem>();
        allMenuList = GetMenuList( allMenuList, RootContentMenuID, culture );
        string id = currentItem.ReferringMenuID;
        foreach (ContentMenuItem item in allMenuList)
        {
            if (!IsContentMenuItemChildOf( item, id ) && !IsContentMenuItemChildOf( item, currentItem.ReferringMenuID ))
                uxDrop.Items.Add( new ListItem( item.GetFullContentPath( culture ), item.ReferringMenuID ) );
            else
                id = item.ReferringMenuID;
        }
        uxDrop.Items.Remove( uxDrop.Items.FindByValue(
            currentItem.ReferringMenuID ) );

    }

    private bool IsContentMenuItemChildOf( ContentMenuItem item, string id )
    {
        if (item.ContentMenuID == id)
        {
            return true;
        }
        else
            return false;
    }
    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        InitDropDownList();
        if (SelectedValueItem == "0" || SelectedValueItem == String.Empty)
            SelectedValueItem = RootContentMenuID;
        uxDrop.SelectedValue = SelectedValueItem;
    }

    protected void uxDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        SelectedValueItem = uxDrop.SelectedValue;
        
        // Send event to parent controls
        OnBubbleEvent( e );
    }
    #endregion


    #region Public Properties

    public string RootContentMenuID
    {
        get
        {
            if (ViewState["RootContentMenuID"] == null)
                return "0";
            else
                return ViewState["RootContentMenuID"].ToString();
        }
        set
        {
            ViewState["RootContentMenuID"] = value;
        }
    }

    public string ContentMenuItemIDToExclude
    {
        get
        {
            if (ViewState["ContentMenuItemIDToExclude"] == null)
                return "0";
            else
                return ViewState["ContentMenuItemIDToExclude"].ToString();
        }
        set
        {
            ViewState["ContentMenuItemIDToExclude"] = value;
        }
    }

    public string CultureID
    {
        get
        {
            if (ViewState["CultureID"] == null)
                return "0";
            else
                return ViewState["CultureID"].ToString();
        }
        set
        {
            ViewState["CultureID"] = value;
        }
    }

    public string SelectedValueItem
    {
        get
        {
            if (ViewState["SelectedValueItem"] == null)
                return String.Empty;
            else
                return ViewState["SelectedValueItem"].ToString();
        }
        set
        {
            ViewState["SelectedValueItem"] = value;
        }
    }

    public Unit Width
    {
        get { return uxDrop.Width; }
        set { uxDrop.Width = value; }
    }

    public string CssClass
    {
        get { return uxDrop.CssClass; }
        set { uxDrop.CssClass = value; }
    }

    #endregion

}
