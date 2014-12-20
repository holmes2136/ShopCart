using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Vevo;
using System.Collections.Generic;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain;
using Vevo.WebUI.Ajax;
using Vevo.Domain.Shipping;
using Vevo.Shared.Utilities;
using System.Text.RegularExpressions;
using Vevo.Base.Domain;

public partial class Admin_MainControls_ShippingZoneItemList : AdminAdvancedBaseUserControl
{
    private bool _emptyRow = false;

    private bool IsContainingOnlyEmptyRow
    {
        get { return _emptyRow; }
        set { _emptyRow = value; }
    }

    private string ZoneGroupID
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["ZoneGroupID"] ))
                return MainContext.QueryString["ZoneGroupID"];
            else
                return String.Empty;
        }
    }

    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.ShippingZoneGroupRepository.GetShippingZoneItemTableSchemas();

        uxSearchFilter.SetUpSchema( list, "ZoneItemID" );
    }

    private void SetFooterRowFocus()
    {
        Control countryList = uxGrid.FooterRow.FindControl( "uxCountryList" );
        AjaxUtilities.GetScriptManager( this ).SetFocus( countryList );
    }

    private void CreateDummyRow( IList<ShippingZoneItem> list )
    {
        ShippingZoneItem item = new ShippingZoneItem();
        item.ZoneItemID = "-1";
        item.ZoneGroupID = "";
        list.Add( item );
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

        IList<ShippingZoneItem> list = DataAccessContext.ShippingZoneGroupRepository.SearchShippingZoneItems(
            ZoneGroupID,
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            (uxPagingControl.CurrentPage - 1) * uxPagingControl.ItemsPerPages,
            (uxPagingControl.CurrentPage * uxPagingControl.ItemsPerPages) - 1,
            out totalItems );


        if (list == null || list.Count == 0)
        {
            IsContainingOnlyEmptyRow = true;
            list = new List<ShippingZoneItem>();
            CreateDummyRow( list );
        }
        else
        {
            IsContainingOnlyEmptyRow = false;
        }

        uxGrid.DataSource = list;
        uxGrid.DataBind();
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        RefreshDeleteButton();

        ((AdminAdvanced_Components_CountryList) uxGrid.FooterRow.FindControl( "uxCountryList" )).Refresh();

        ResetCountryState();
    }

    private void RefreshDeleteButton()
    {
        if (IsContainingOnlyEmptyRow)
            uxDeleteButton.Visible = false;
        else
            uxDeleteButton.Visible = true;
    }


    private void ResetCountryState()
    {
        if (uxGrid.ShowFooter)
        {
            ((AdminAdvanced_Components_StateList) uxGrid.FooterRow.FindControl( "uxStateList" )).CountryCode = "";
            ((AdminAdvanced_Components_CountryList) uxGrid.FooterRow.FindControl( "uxCountryList" )).CurrentSelected = "";
            ((AdminAdvanced_Components_StateList) uxGrid.FooterRow.FindControl( "uxStateList" )).CurrentSelected = "";
        }
    }

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


    private ShippingZoneGroup RemoveItem( ShippingZoneGroup group, string itemID )
    {
        for (int i = 0; i < group.ZoneItem.Count; i++)
        {
            if (group.ZoneItem[i].ZoneItemID == itemID)
            {
                group.ZoneItem.RemoveAt( i );
                return group;
            }
        }
        return group;
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        bool deleted = false;

        foreach (GridViewRow row in uxGrid.Rows)
        {
            CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
            if (deleteCheck != null && deleteCheck.Checked)
            {
                string id = uxGrid.DataKeys[row.RowIndex]["ZoneItemID"].ToString();
                ShippingZoneGroup zoneGroup = DataAccessContext.ShippingZoneGroupRepository.GetOne( ZoneGroupID );
                zoneGroup = RemoveItem( zoneGroup, id );
                DataAccessContext.ShippingZoneGroupRepository.Save( zoneGroup );
                deleted = true;
            }
        }
        uxGrid.EditIndex = -1;

        if (deleted)
        {
            uxMessage.DisplayMessage( Resources.ShippingZoneMessages.DeleteSuccess );
        }

        RefreshGrid();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (String.IsNullOrEmpty( ZoneGroupID ))
            MainContext.RedirectMainControl( "ShippingZoneGroupList.ascx" );

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

        AdminAdvanced_Components_CountryList myCountry = ((AdminAdvanced_Components_CountryList) uxGrid.FooterRow.FindControl( "uxCountryList" ));
        myCountry.CurrentSelected = "";
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

    private ShippingZoneItem SetupShippingZoneItemDetails()
    {
        ShippingZoneItem item = new ShippingZoneItem();
        item.CountryCode = ((AdminAdvanced_Components_CountryList) uxGrid.FooterRow.FindControl( "uxCountryList" )).CurrentSelected;
        item.StateCode = ((AdminAdvanced_Components_StateList) uxGrid.FooterRow.FindControl( "uxStateList" )).CurrentSelected;
        item.ZipCode = ((TextBox) uxGrid.FooterRow.FindControl( "uxZipCodeText" )).Text.Trim();
        item.ZoneGroupID = ZoneGroupID;

        return item;
    }



    private bool IsExistItem( ShippingZoneItem item )
    {
        ShippingZoneGroup zoneGroup = DataAccessContext.ShippingZoneGroupRepository.GetOne( ZoneGroupID );
        foreach (ShippingZoneItem zoneItem in zoneGroup.ZoneItem)
        {
            if (zoneItem.CountryCode == item.CountryCode && zoneItem.StateCode == item.StateCode && zoneItem.ZipCode == item.ZipCode)
            {
                if (zoneItem.ZoneItemID != item.ZoneItemID)
                    return true;
            }
        }

        return false;
    }

    private bool IsZipcodeFormatCorrect( string zipCode )
    {
        if (!String.IsNullOrEmpty( zipCode ))
        {
            Regex r = new Regex( "^[^*]+\\*{0,1}$" );
            return r.IsMatch( zipCode );
        }
        else
            return true;
    }


    protected void uxGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        try
        {
            if (e.CommandName == "Add")
            {
                ShippingZoneGroup zoneGroup = DataAccessContext.ShippingZoneGroupRepository.GetOne( ZoneGroupID );
                ShippingZoneItem item = SetupShippingZoneItemDetails();

                if (IsZipcodeFormatCorrect( item.ZipCode ))
                {
                    if (String.IsNullOrEmpty( item.CountryCode ))
                    {
                        uxMessage.DisplayError( Resources.ShippingZoneMessages.AddErrorSelectCountry );
                        return;
                    }

                    if (!IsExistItem( item ))
                    {
                        zoneGroup.ZoneItem.Add( item );
                        DataAccessContext.ShippingZoneGroupRepository.Save( zoneGroup );

                        uxMessage.DisplayMessage( Resources.ShippingZoneMessages.AddZoneItemSuccess );
                        RefreshGrid();
                    }
                    else
                        uxMessage.DisplayError( Resources.ShippingZoneMessages.AddErrorDuplicatedItem );

                    ((TextBox) uxGrid.FooterRow.FindControl( "uxZipCodeText" )).Text = "";
                }
                else
                {
                    uxMessage.DisplayError( Resources.ShippingZoneMessages.ZipCodeFormatIncorrect );
                }

            }
            if (e.CommandName == "Edit")
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
            string id = ((Label) uxGrid.Rows[e.RowIndex].FindControl( "uxZoneItemIDLabel" )).Text.Trim();
            string countryCode = ((AdminAdvanced_Components_CountryList) uxGrid.Rows[e.RowIndex].FindControl( "uxCountryList" )).CurrentSelected;
            string stateCode = ((AdminAdvanced_Components_StateList) uxGrid.Rows[e.RowIndex].FindControl( "uxStateList" )).CurrentSelected;
            string zipCode = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxZipCodeEditText" )).Text.Trim();

            if (!String.IsNullOrEmpty( id ))
            {
                if (IsZipcodeFormatCorrect( zipCode ))
                {
                    ShippingZoneItem currentItem = new ShippingZoneItem();
                    ShippingZoneGroup zoneGroup = DataAccessContext.ShippingZoneGroupRepository.GetOne( ZoneGroupID );

                    foreach (ShippingZoneItem item in zoneGroup.ZoneItem)
                    {
                        if (item.ZoneItemID == id)
                        {
                            currentItem.ZoneItemID = id;
                            currentItem.CountryCode = countryCode;
                            currentItem.StateCode = stateCode;
                            currentItem.ZipCode = zipCode;

                            if (!IsExistItem( currentItem ))
                            {
                                item.CountryCode = countryCode;
                                item.StateCode = stateCode;
                                item.ZipCode = zipCode;
                                DataAccessContext.ShippingZoneGroupRepository.Save( zoneGroup );
                                uxMessage.DisplayMessage( Resources.ShippingZoneMessages.UpdateZoneItemSuccess );
                            }
                            else
                                uxMessage.DisplayError( Resources.ShippingZoneMessages.AddErrorDuplicatedItem );
                        }
                    }
                }
                else
                {
                    uxMessage.DisplayError( Resources.ShippingZoneMessages.ZipCodeFormatIncorrect );
                }

            }

            // End editing
            uxGrid.EditIndex = -1;
            RefreshGrid();
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

    protected void uxState_RefreshHandler( object sender, EventArgs e )
    {
        int index = uxGrid.EditIndex;
        AdminAdvanced_Components_StateList stateList = (AdminAdvanced_Components_StateList) uxGrid.Rows[index].FindControl( "uxStateList" );
        AdminAdvanced_Components_CountryList countryList = (AdminAdvanced_Components_CountryList) uxGrid.Rows[index].FindControl( "uxCountryList" );
        stateList.CountryCode = countryList.CurrentSelected;
        stateList.Refresh();
    }

    protected void uxStateFooter_RefreshHandler( object sender, EventArgs e )
    {
        AdminAdvanced_Components_StateList stateList = (AdminAdvanced_Components_StateList) uxGrid.FooterRow.FindControl( "uxStateList" );
        AdminAdvanced_Components_CountryList countryList = (AdminAdvanced_Components_CountryList) uxGrid.FooterRow.FindControl( "uxCountryList" );
        stateList.CountryCode = countryList.CurrentSelected;
        stateList.Refresh();
    }
}
