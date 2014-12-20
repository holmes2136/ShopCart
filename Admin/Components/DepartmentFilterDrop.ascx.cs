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

public partial class AdminAdvanced_Components_DepartmentFilterDrop :
    AdminAdvancedBaseUserControl, IDepartmentFilterDrop
{
    private void RefreshDropDownList()
    {
        uxRootDepartmentDrop.Items.Clear();
        InsertRootFirstLineIfNecessary();

        IList<Department> departmentList = departmentList = DataAccessContext.DepartmentRepository.GetByParentID(
            DataAccessContext.CultureRepository.GetOne( CultureID ), "0", "DepartmentID", BoolFilter.ShowAll );

        for (int i = 0; i < departmentList.Count; i++)
        {
            uxRootDepartmentDrop.Items.Add(
                 new ListItem( departmentList[i].Name + " (" + departmentList[i].DepartmentID + ")", departmentList[i].DepartmentID ) );

        }

        PopulateDepartmentDropList();

        if (!IsDisplayRootDepartmentDrop)
            uxRootDepartmentFilterPanel.Visible = false;

        if (IsDisplayRootDepartmentDrop && String.IsNullOrEmpty( uxRootDepartmentDrop.SelectedValue ))
            uxDepartmentDrop.Enabled = false;
        else
            uxDepartmentDrop.Enabled = true;

    }

    private void PopulateDepartmentDropList()
    {
        uxDepartmentDrop.Items.Clear();
        InsertFirstLineIfNecessary();

        IList<Department> departmentList = DataAccessContext.DepartmentRepository.GetByRootIDLeafOnly( DataAccessContext.CultureRepository.GetOne( CultureID ), SelectedRootValue, "ParentDepartmentID", BoolFilter.ShowAll );

        string currentParentID = "";
        string tmpFullPath = "";
        for (int i = 0; i < departmentList.Count; i++)
        {
            if (currentParentID != departmentList[i].ParentDepartmentID)
            {
                tmpFullPath = departmentList[i].CreateFullDepartmentPathParentOnly();
                currentParentID = departmentList[i].ParentDepartmentID;
            }
            uxDepartmentDrop.Items.Add(
                new ListItem( tmpFullPath + departmentList[i].Name + " (" + departmentList[i].DepartmentID + ")", departmentList[i].DepartmentID ) );

        }
    }

    private void InsertFirstLineIfNecessary()
    {
        if (FirstLineEnable)
        {
            uxDepartmentDrop.Items.Add(
                new ListItem( Resources.SearchFilterMessages.FilterShowAll, "" ) );
        }
    }

    private void InsertRootFirstLineIfNecessary()
    {
        if (RootFirstLineEnable)
        {
            uxRootDepartmentDrop.Items.Add(
              new ListItem( Resources.SearchFilterMessages.FilterShowAll, "" ) );
        }
    }

    private void LoadDefaultFromQuery()
    {
        if (!String.IsNullOrEmpty( MainContext.QueryString["DepartmentID"] ))
            SelectedValue = MainContext.QueryString["DepartmentID"];
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

    protected void uxDepartmentDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        OnBubbleEvent( e );
    }

    protected void uxRootDepartmentDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        PopulateDepartmentDropList();
        if (String.IsNullOrEmpty( uxRootDepartmentDrop.SelectedValue ))
            uxDepartmentDrop.Enabled = false;
        else
            uxDepartmentDrop.Enabled = true;

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
            return uxDepartmentDrop.AutoPostBack;
        }
        set
        {
            uxDepartmentDrop.AutoPostBack = value;
        }
    }

    public string SelectedValue
    {
        get
        {
            return uxDepartmentDrop.SelectedValue;
        }
        set
        {
            uxDepartmentDrop.SelectedValue = value;
        }
    }

    public string SelectedRootValue
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
                return uxRootDepartmentDrop.SelectedValue;
            else
                return DataAccessContext.Configurations.GetValue(
                    "RootDepartment", DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID ) );
        }
        set
        {
            uxRootDepartmentDrop.SelectedValue = value;
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

    public bool IsDisplayRootDepartmentDrop
    {
        get
        {
            if (ViewState["IsDisplayRootDepartmentDrop"] == null || !KeyUtilities.IsMultistoreLicense())
                return false;
            else
                return (bool) ViewState["IsDisplayRootDepartmentDrop"];
        }
        set
        {
            ViewState["IsDisplayRootDepartmentDrop"] = value;
            RefreshDropDownList();
        }
    }

    public void UpdateBrowseQuery( UrlQuery urlQuery )
    {
        if (!String.IsNullOrEmpty( SelectedValue ))
            urlQuery.AddQuery( "DepartmentID", SelectedValue );
        else
            urlQuery.RemoveQuery( "DepartmentID" );

        if (!String.IsNullOrEmpty( SelectedRootValue ))
            urlQuery.AddQuery( "RootDepartmentID", SelectedRootValue );
        else
            urlQuery.RemoveQuery( "RootDepartmentID" );

    }
}
