using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.Domain.Stores;


public partial class AdminAdvanced_Components_CategoryFilterDrop
    : AdminAdvancedBaseUserControl, ICategoryFilterDrop
{
    private void RefreshDropDownList()
    {
        uxRootCategoryDrop.Items.Clear();
        InsertRootFirstLineIfNecessary();

        IList<Category> categoryList = categoryList = DataAccessContext.CategoryRepository.GetByParentID(
            DataAccessContext.CultureRepository.GetOne( CultureID ), "0", "CategoryID", BoolFilter.ShowAll );

        for (int i = 0; i < categoryList.Count; i++)
        {
            uxRootCategoryDrop.Items.Add(
                 new ListItem( categoryList[i].Name + " (" + categoryList[i].CategoryID + ")", categoryList[i].CategoryID ) );

        }
        uxRootCategoryDrop.SelectedValue = SelectedRootValue;
        PopulateCategoryDropList();

        if (!IsDisplayRootCategoryDrop)
            uxRootCategoryFilterPanel.Visible = false;

        if (IsDisplayRootCategoryDrop && String.IsNullOrEmpty( uxRootCategoryDrop.SelectedValue ))
            uxCategoryDrop.Enabled = false;
        else
            uxCategoryDrop.Enabled = true;

    }

    private void PopulateCategoryDropList()
    {
        uxCategoryDrop.Items.Clear();
        InsertFirstLineIfNecessary();

        IList<Category> categoryList = DataAccessContext.CategoryRepository.GetByRootIDLeafOnly(
            DataAccessContext.CultureRepository.GetOne( CultureID ), SelectedRootValue, "ParentCategoryID", BoolFilter.ShowAll );

        string currentParentID = "";
        string tmpFullPath = "";
        for (int i = 0; i < categoryList.Count; i++)
        {
            if (currentParentID != categoryList[i].ParentCategoryID)
            {
                tmpFullPath = categoryList[i].CreateFullCategoryPathParentOnly();
                currentParentID = categoryList[i].ParentCategoryID;
            }
            uxCategoryDrop.Items.Add(
                new ListItem( tmpFullPath + categoryList[i].Name + " (" + categoryList[i].CategoryID + ")", categoryList[i].CategoryID ) );

        }
    }

    private void InsertFirstLineIfNecessary()
    {
        if (FirstLineEnable)
        {
            uxCategoryDrop.Items.Add(
                new ListItem( Resources.SearchFilterMessages.FilterShowAll, "" ) );
        }
    }

    private void InsertRootFirstLineIfNecessary()
    {
        if (RootFirstLineEnable)
        {
            uxRootCategoryDrop.Items.Add(
              new ListItem( Resources.SearchFilterMessages.FilterShowAll, "" ) );
        }
    }

    private void LoadDefaultFromQuery()
    {
        if (!String.IsNullOrEmpty( MainContext.QueryString["CategoryID"] ))
            SelectedValue = MainContext.QueryString["CategoryID"];
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            RefreshDropDownList();
            LoadDefaultFromQuery();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
        }
    }

    protected void uxCategoryDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        OnBubbleEvent( e );
    }

    protected void uxRootCategoryDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        PopulateCategoryDropList();
        if (String.IsNullOrEmpty( uxRootCategoryDrop.SelectedValue ))
            uxCategoryDrop.Enabled = false;
        else
            uxCategoryDrop.Enabled = true;

        OnBubbleEvent( e );
    }

    public string CultureID
    {
        get
        {
            if (ViewState["CultureID"] == null)
                return AdminConfig.CurrentCultureID;
            else
                return (string) ViewState["CultureID"];
        }
        set
        {
            ViewState["CultureID"] = value;
            RefreshDropDownList();
        }
    }

    public bool AutoPostBack
    {
        get
        {
            return uxCategoryDrop.AutoPostBack;
        }
        set
        {
            uxCategoryDrop.AutoPostBack = value;
        }
    }

    public string SelectedValue
    {
        get
        {
            return uxCategoryDrop.SelectedValue;
        }
        set
        {
            uxCategoryDrop.SelectedValue = value;
        }
    }

    public string SelectedRootValue
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
                return uxRootCategoryDrop.SelectedValue;
            else
                return DataAccessContext.Configurations.GetValue(
                    "RootCategory", DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID ) );
        }
        set
        {
            uxRootCategoryDrop.SelectedValue = value;
        }
    }

    public bool FirstLineEnable
    {
        get
        {
            if (ViewState["FirstLineEnable"] == null)
                return true;
            else
                return (bool) ViewState["FirstLineEnable"];
        }
        set
        {
            ViewState["FirstLineEnable"] = value;
            RefreshDropDownList();
        }
    }

    public bool RootFirstLineEnable
    {
        get
        {
            if (ViewState["RootFirstLineEnable"] == null)
                return false;
            else
                return (bool) ViewState["RootFirstLineEnable"];
        }
        set
        {
            ViewState["RootFirstLineEnable"] = value;
            RefreshDropDownList();
        }
    }

    public bool IsEnableRootCategoryDrop
    {
        get
        {
            if (ViewState["IsEnableRootCategoryDrop"] == null)
                return true;
            else
                return (bool) ViewState["IsEnableRootCategoryDrop"];
        }
        set
        {
            ViewState["IsEnableRootCategoryDrop"] = value;
            SetEnableRootCategoryDrop();
        }
    }

    private void SetEnableRootCategoryDrop()
    {
        uxRootCategoryDrop.Enabled = IsEnableRootCategoryDrop;
    }

    public bool IsDisplayRootCategoryDrop
    {
        get
        {
            if (ViewState["IsDisplayRootCategoryDrop"] == null || !KeyUtilities.IsMultistoreLicense())
                return false;
            else
                return (bool) ViewState["IsDisplayRootCategoryDrop"];
        }
        set
        {
            ViewState["IsDisplayRootCategoryDrop"] = value;
            RefreshDropDownList();
        }
    }

    public void UpdateBrowseQuery( UrlQuery urlQuery )
    {
        if (!String.IsNullOrEmpty( SelectedValue ))
            urlQuery.AddQuery( "CategoryID", SelectedValue );
        else
            urlQuery.RemoveQuery( "CategoryID" );

        if (!String.IsNullOrEmpty( SelectedRootValue ))
            urlQuery.AddQuery( "RootCategoryID", SelectedRootValue );
        else
            urlQuery.RemoveQuery( "RootCategoryID" );

    }

    public void RefreshCategoryDropList(string rootID)
    {
        SelectedRootValue = rootID;
        PopulateCategoryDropList();
    }

}
