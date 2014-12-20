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
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class Components_ContentMenuItemCheckList : BaseLanguageUserControl
{
    #region Private

    private IList<ContentMenuItem> _listContentMenuItem;

    private IList<ContentMenuItem> ListContentMenuItem
    {
        get
        {
            return _listContentMenuItem;
        }
        set
        {
            _listContentMenuItem = value;
        }
    }

    private void Components_ContentMenuItemCheckList_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        SetCheckList();
    }

    private string[] GetChildrenContentMenuItemIDs( string id )
    {
        ContentMenuItem item = DataAccessContext.ContentMenuItemRepository.GetOne(
            StoreContext.Culture, id );

        IList<ContentMenuItem> list = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
            StoreContext.Culture, item.ReferringMenuID, "SortOrder", BoolFilter.ShowTrue, BoolFilter.ShowTrue );
        string[] contentmenuitems = new string[list.Count];

        for (int i = 0; i < contentmenuitems.Length; i++)
            contentmenuitems[i] = list[i].ContentMenuItemID;
        return contentmenuitems;
    }

    private void GetLeafOfID( NameValueCollection leafList, string id )
    {
        string[] children = GetChildrenContentMenuItemIDs( id );
        if (children.Length == 0)
        {
            if (id != "0")
            {
                ContentMenuItem details = DataAccessContext.ContentMenuItemRepository.GetOne( StoreContext.Culture, id );
                leafList[id] = id + ":" + details.Name;

            }
        }
        else
        {
            foreach (string child in children)
            {
                ContentMenuItem details = DataAccessContext.ContentMenuItemRepository.GetOne( StoreContext.Culture, child );
                leafList[child] = details + ":" + details.Name;
                GetLeafOfID( leafList, child );
            }
        }
    }

    private string[] GetLeafContentMenuItemIDs( string contentMenuItemID )
    {
        NameValueCollection leafList = new NameValueCollection();

        GetLeafOfID( leafList, contentMenuItemID );

        string[] result = new string[leafList.Count];
        leafList.CopyTo( result, 0 );

        return result;
    }

    private string[] SpiltString( string str )
    {
        return str.Split( ':' );
    }

    private void SetCheckList()
    {
        uxContentMenuItemCheckList.Items.Clear();
        ContentMenuItem currentitem = DataAccessContext.ContentMenuItemRepository.GetOne(
            StoreContext.Culture, CurrentID );
        IList<ContentMenuItem> list = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
            StoreContext.Culture, currentitem.ReferringMenuID, "SortOrder", BoolFilter.ShowTrue, BoolFilter.ShowTrue );
        //IList<ContentMenuItem> list = DataAccessContext.ContentMenuItemRepository.GetAllSku( StoreContext.Culture, BoolFilter.ShowTrue );
        if (list.Count > 0)
        {
            foreach (ContentMenuItem item in list)
            {
                string[] leafContentMenuItemIDs = GetLeafContentMenuItemIDs( item.ContentMenuItemID );

                foreach (string leafContentMenuItemID in leafContentMenuItemIDs)
                {
                    string[] result = SpiltString( leafContentMenuItemID );
                    uxContentMenuItemCheckList.Items.Add( new ListItem( result[1], result[0] ) );
                }
            }
        }
        else
        {
            string[] leafContentMenuItemIDs = GetLeafContentMenuItemIDs( currentitem.ContentMenuItemID );

            foreach (string leafContentMenuItemID in leafContentMenuItemIDs)
            {
                string[] result = SpiltString( leafContentMenuItemID );
                uxContentMenuItemCheckList.Items.Add( new ListItem( result[1], result[0] ) );
            }
        }

        uxContentMenuItemCheckList.DataBind();
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCultureChanged +=
         new StorefrontEvents.CultureEventHandler( Components_ContentMenuItemCheckList_StoreCultureChanged );

        if (!IsPostBack)
            SetCheckList();
    }

    #endregion

    #region Public

    public string[] CheckedContentMenuItemID()
    {
        string[] contentmenuitems = new string[uxContentMenuItemCheckList.Items.Count];
        int i = 0;
        foreach (ListItem item in uxContentMenuItemCheckList.Items)
        {
            if (item.Selected)
            {
                contentmenuitems[i++] = item.Value;
            }
        }
        string[] result = new string[i];
        for (int j = 0; j < i; j++)
        {
            if (contentmenuitems[j] != "" && contentmenuitems[j] != null)
                result[j] = contentmenuitems[j];
            else
                break;
        }
        return result;
    }

    public string CurrentID
    {
        get
        {
            return ViewState["ContentMenuItemID"].ToString();
        }
        set
        {
            ViewState["ContentMenuItemID"] = value;
        }
    }

    #endregion
}
