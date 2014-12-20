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
using Vevo.Domain.Stores;


public partial class Admin_Components_StoreFilterDrop : AdminAdvancedBaseUserControl, IStoreFilterDrop
{
    private void RefreshDropDownList()
    {
        uxStoreDrop.Items.Clear();
        lcStoreFilter.Visible = DisplayLabel;
        InsertFirstLineIfNecessary();

        IList<Store> list = DataAccessContext.StoreRepository.GetAll( "StoreID" );

        for (int i = 0; i < list.Count; i++)
        {
            string name = list[i].StoreName;
            name += " (" + list[i].StoreID + ")";
            if(IsDisplayUrl)
                name += " : " + list[i].UrlName;

            uxStoreDrop.Items.Add( new ListItem( name, list[i].StoreID ) );
        }
    }

    private void LoadDefaultFromQuery()
    {
        if (!String.IsNullOrEmpty( MainContext.QueryString["StoreID"] ))
            SelectedValue = MainContext.QueryString["StoreID"];
    }

    private void InsertFirstLineIfNecessary()
    {
        if (FirstLineEnable)
        {
            uxStoreDrop.Items.Add(
                new ListItem( Resources.SearchFilterMessages.FilterShowAll, "" ) );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            RefreshDropDownList();
            LoadDefaultFromQuery();
        }

        if (!KeyUtilities.IsMultistoreLicense())
            uxStorePanel.Visible = false;

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
        }
    }

    protected void uxStoreDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        OnBubbleEvent( e );
    }

    public bool AutoPostBack
    {
        get
        {
            return uxStoreDrop.AutoPostBack;
        }
        set
        {
            uxStoreDrop.AutoPostBack = value;
        }
    }

    public string SelectedValue
    {
        get
        {
            return uxStoreDrop.SelectedValue;
        }
        set
        {
            uxStoreDrop.SelectedValue = value;
        }
    }

    public void UpdateBrowseQuery( UrlQuery urlQuery )
    {
        if (!String.IsNullOrEmpty( SelectedValue ))
            urlQuery.AddQuery( "StoreID", SelectedValue );
        else
            urlQuery.RemoveQuery( "StoreID" );
    }

    public bool FirstLineEnable
    {
        get
        {
            if (ViewState["FirstLineEnable"] == null)
                return false;
            else
                return (bool) ViewState["FirstLineEnable"];
        }
        set
        {
            ViewState["FirstLineEnable"] = value;
            RefreshDropDownList();
        }
    }

    public bool DisplayLabel
    {
        get
        {
            if (ViewState["DisplayLabel"] == null)
                return true;
            else
                return (bool) ViewState["DisplayLabel"];
        }
        set
        {
            ViewState["DisplayLabel"] = value;
            RefreshDropDownList();
        }
    }

    public bool IsDisplayUrl
    {
        get
        {
            if (ViewState["IsDisplayUrl"] == null)
                return false;
            else
                return (bool) ViewState["IsDisplayUrl"];
        }
        set
        {
            ViewState["IsDisplayUrl"] = value;
            RefreshDropDownList();
        }
    }
}
