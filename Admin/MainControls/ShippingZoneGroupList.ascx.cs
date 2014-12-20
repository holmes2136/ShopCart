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
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Shipping;
using Vevo.WebUI.Ajax;
using Vevo.WebAppLib;
using Vevo.Base.Domain;

public partial class Admin_MainControls_ShippingZoneGroupList : AdminAdvancedBaseUserControl
{
    #region Private

    private bool _emptyRow = false;

    private bool IsContainingOnlyEmptyRow
    {
        get { return _emptyRow; }
        set { _emptyRow = value; }
    }

    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "ZoneGroupID" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.ShippingZoneGroupRepository.GetTableSchemas();
        uxSearchFilter.SetUpSchema( list );
    }

    private void SetFooterRowFocus()
    {
        Control textBox = uxGrid.FooterRow.FindControl( "uxNameText" );
        AjaxUtilities.GetScriptManager( this ).SetFocus( textBox );
    }

    private void CreateDummyRow( IList<ShippingZoneGroup> list )
    {
        ShippingZoneGroup zoneGroup = new ShippingZoneGroup();
        zoneGroup.ZoneGroupID = "-1";
        list.Add( zoneGroup );
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

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }

        if (uxGrid.Rows.Count > 0)
        {
            DeleteVisible( true );
            uxPagingControl.Visible = true;
        }
        else
        {
            DeleteVisible( false );
            uxPagingControl.Visible = false;
        }

        if (!IsAdminModifiable())
        {
            uxAddButton.Visible = false;
            DeleteVisible( false );
        }
    }

    private void RefreshGrid()
    {
        int totalItems;

        IList<ShippingZoneGroup> list = DataAccessContext.ShippingZoneGroupRepository.SearchShippingZoneGroups(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            (uxPagingControl.CurrentPage - 1) * uxPagingControl.ItemsPerPages,
            (uxPagingControl.CurrentPage * uxPagingControl.ItemsPerPages) - 1,
            out totalItems );

        if (list == null || list.Count == 0)
        {
            IsContainingOnlyEmptyRow = true;
            list = new List<ShippingZoneGroup>();
            CreateDummyRow( list );
        }
        else
        {
            IsContainingOnlyEmptyRow = false;
        }

        uxGrid.DataSource = list;
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
        uxGrid.DataBind();

        RefreshDeleteButton();

        if (uxGrid.ShowFooter)
        {
            Control nameText = uxGrid.FooterRow.FindControl( "uxNameText" );
            Control addButton = uxGrid.FooterRow.FindControl( "uxAddLinkButton" );

            WebUtilities.TieButton( this.Page, nameText, addButton );
        }
    }

    private void RefreshDeleteButton()
    {
        if (IsContainingOnlyEmptyRow)
            uxDeleteButton.Visible = false;
        else
            uxDeleteButton.Visible = true;
    }

    private string[] GetCheckedIDs()
    {
        ArrayList items = new ArrayList();

        foreach (GridViewRow row in uxGrid.Rows)
        {
            CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
            if (deleteCheck.Checked)
            {
                string id = uxGrid.DataKeys[row.RowIndex]["ZoneGroupID"].ToString().Trim();
                items.Add( id );
            }
        }

        string[] result = new string[items.Count];
        items.CopyTo( result );
        return result;
    }

    private bool ContainsShippingOptions( string[] idArray, out string containingID )
    {
        foreach (string id in idArray)
        {
            IList<ShippingOption> shippingList = DataAccessContext.ShippingOptionRepository.GetAllByShippingZoneGroupID( Culture.Null, id );

            if (shippingList.Count > 0)
            {
                containingID = id;
                return true;
            }
        }

        containingID = "";
        return false;
    }

    private void DeleteItems( string[] checkedIDs )
    {
        try
        {
            bool deleted = false;
            foreach (string id in checkedIDs)
            {
                DataAccessContext.ShippingZoneGroupRepository.Delete( id );
                deleted = true;
            }
            uxGrid.EditIndex = -1;

            if (deleted)
                uxMessage.DisplayMessage( Resources.ShippingZoneMessages.DeleteSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();
    }

    #endregion

    #region Protected

    protected void uxScarchChange( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    protected void uxChangePage( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    protected void uxGrid_DataBound( object sender, EventArgs e )
    {
        if (IsContainingOnlyEmptyRow)
        {
            uxGrid.Rows[0].Visible = false;
        }
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        string[] checkedIDs = GetCheckedIDs();

        string containingID;
        if (ContainsShippingOptions( checkedIDs, out containingID ))
        {
            ShippingZoneGroup zoneGroup = DataAccessContext.ShippingZoneGroupRepository.GetOne( containingID );
            uxMessage.DisplayError(
                Resources.ShippingZoneMessages.DeleteErrorContainingShippingOptions,
                zoneGroup.ZoneName, containingID );
        }
        else
        {
            DeleteItems( checkedIDs );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxSearchFilter.BubbleEvent += new EventHandler( uxScarchChange );
        uxPagingControl.BubbleEvent += new EventHandler( uxChangePage );

        if (!MainContext.IsPostBack)
            SetUpSearchFilter();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        uxGrid.EditIndex = -1;
        uxGrid.ShowFooter = true;
        uxGrid.ShowHeader = true;
        RefreshGrid();

        uxAddButton.Visible = false;

        SetFooterRowFocus();
    }

    protected void uxGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
        RefreshGrid();
    }

    protected void uxGrid_RowEditing( object sender, GridViewEditEventArgs e )
    {
        uxGrid.EditIndex = e.NewEditIndex;
        RefreshGrid();
    }

    protected void uxGrid_CancelingEdit( object sender, GridViewCancelEditEventArgs e )
    {
        uxGrid.EditIndex = -1;
        RefreshGrid();
    }

    protected void uxGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        try
        {
            if (e.CommandName == "Add")
            {

                string name = ((TextBox) uxGrid.FooterRow.FindControl( "uxNameText" )).Text;

                ShippingZoneGroup zoneGroup = new ShippingZoneGroup();
                zoneGroup.ZoneName = name;

                DataAccessContext.ShippingZoneGroupRepository.Save( zoneGroup );

                ((TextBox) uxGrid.FooterRow.FindControl( "uxNameText" )).Text = "";

                uxMessage.DisplayMessage( Resources.ShippingZoneMessages.AddSuccess );

                RefreshGrid();

            }

            else if (e.CommandName == "Edit")
            {
                uxGrid.ShowFooter = false;
                uxAddButton.Visible = true;
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

    }

    protected void uxGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            string zoneGroupID = ((Label) uxGrid.Rows[e.RowIndex].FindControl( "uxZoneGroupIDLabel" )).Text;
            string name = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxNameText" )).Text;

            if (!String.IsNullOrEmpty( zoneGroupID ))
            {
                ShippingZoneGroup zoneGroup = new ShippingZoneGroup();
                zoneGroup.ZoneName = name;
                zoneGroup.ZoneGroupID = zoneGroupID;
                DataAccessContext.ShippingZoneGroupRepository.Save( zoneGroup );
            }

            // End editing
            uxGrid.EditIndex = -1;
            RefreshGrid();

            uxMessage.DisplayMessage( Resources.ShippingZoneMessages.UpdateSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;
        }
    }

    #endregion
}