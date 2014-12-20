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
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Shipping;
using Vevo.Shared.Utilities;
using Vevo.WebUI.Ajax;
using Vevo.Base.Domain;

public partial class AdminAdvanced_MainControls_ShippingWeightRate : AdminAdvancedBaseUserControl
{
    private string ShippingID
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["ShippingID"] ))
                return MainContext.QueryString["ShippingID"];
            else
                return "0";
        }
    }

    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "ToWeight" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private void RefreshGrid()
    {
        int totalItems = 0;
        IList<ShippingWeightRate> shippingWeightRateList = DataAccessContext.ShippingWeightRateRepository.SearchShippingWeightRateItem(
            ShippingID,
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj, uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );

        if (shippingWeightRateList == null || shippingWeightRateList.Count == 0)
        {
            shippingWeightRateList = new List<ShippingWeightRate>();
            CreateDummyRow( shippingWeightRateList );
        }

        uxGrid.DataSource = shippingWeightRateList;
        uxGrid.DataBind();
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
    }

    private void PopulateControls()
    {
        if (ShippingID == "0")
            MainContext.RedirectMainControl( "ShippingList.ascx" );

        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }
    }

    private void ApplyPermissions()
    {
        if (!IsAdminModifiable())
        {
            uxAddButton.Visible = false;
            DeleteVisible( false );
        }
        else
            DeleteVisible( true );
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

    private void CreateDummyRow( IList<ShippingWeightRate> list )
    {
        ShippingWeightRate shippingWeightRate = new ShippingWeightRate();
        shippingWeightRate.ShippingWeightRateID = "-1";
        shippingWeightRate.ShippingID = ShippingID;
        shippingWeightRate.ToWeight = 0;
        shippingWeightRate.WeightRate = 0;
        list.Add( shippingWeightRate );
    }

    private bool IsContainingOnlyEmptyRow()
    {
        if (uxGrid.Rows.Count == 1 &&
            ConvertUtilities.ToInt32( uxGrid.DataKeys[0]["ShippingWeightRateID"] ) == -1)
            return true;
        else
            return false;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxSearchFilter.BubbleEvent += new EventHandler( uxGrid_ResetHandler );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_RefreshHandler );

        if (!MainContext.IsPostBack)
        {
            SetUpSearchFilter();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
        ApplyPermissions();
    }

    private void uxGrid_ResetHandler( object sender, EventArgs e )
    {
        uxPagingControl.CurrentPage = 1;
        RefreshGrid();
    }

    private void uxPagingControl_RefreshHandler( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    private void SetFooterRowFocus()
    {
        Control textBox = uxGrid.FooterRow.FindControl( "uxToWeightText" );
        AjaxUtilities.GetScriptManager( this ).SetFocus( textBox );
    }

    protected void uxEditLinkButton_PreRender( object sender, EventArgs e )
    {
        if (!IsAdminModifiable())
        {
            LinkButton linkButton = (LinkButton) sender;
            linkButton.Visible = false;
        }
    }

    protected void uxGrid_DataBound( object sender, EventArgs e )
    {
        if (IsContainingOnlyEmptyRow())
        {
            uxGrid.Rows[0].Visible = false;
        }
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        uxGrid.EditIndex = -1;
        uxGrid.ShowFooter = true;
        RefreshGrid();

        uxAddButton.Visible = false;

        SetFooterRowFocus();

        uxStatusHidden.Value = "FooterShown";
    }

    private void ClearData( GridViewRow row )
    {
        ((TextBox) row.FindControl( "uxToWeightText" )).Text = "";
        ((TextBox) row.FindControl( "uxShippingWeightRate" )).Text = "";
    }

    private ShippingWeightRate GetDetailsFromGrid( GridViewRow row, ShippingWeightRate shippingWeightRate )
    {
        shippingWeightRate.ShippingID = ShippingID;
        string toWeightText = ((TextBox) row.FindControl( "uxToWeightText" )).Text;
        shippingWeightRate.ToWeight = ConvertUtilities.ToDouble( toWeightText );

        string shippingWeightRateText = ((TextBox) row.FindControl( "uxShippingWeightRate" )).Text;
        shippingWeightRate.WeightRate = ConvertUtilities.ToDecimal( shippingWeightRateText );

        return shippingWeightRate;
    }

    private bool IsCorrectFormatForShippingByWeight( GridViewRow row )
    {
        string toOrderTotalText = ((TextBox) row.FindControl( "uxToWeightText" )).Text;
        Decimal ToOrderTotal = ConvertUtilities.ToDecimal( toOrderTotalText );
        if (ToOrderTotal < 0) return false;

        string shippingOrderTotalRateText = ((TextBox) row.FindControl( "uxShippingWeightRate" )).Text;
        Decimal OrderTotalRate = ConvertUtilities.ToDecimal( shippingOrderTotalRateText );
        if (OrderTotalRate < 0) return false;

        return true;
    }

    private bool IsExisted( object addToWeight )
    {
        bool isExisted = false;
        IList<ShippingWeightRate> shippingWeightRateList =
            DataAccessContext.ShippingWeightRateRepository.GetAllByShippingID( ShippingID, "ToWeight" );

        for (int i = 0; i < shippingWeightRateList.Count; i++)
        {
            if (shippingWeightRateList[i].ToWeight == ConvertUtilities.ToDouble( addToWeight ))
                isExisted = true;
        }
        return isExisted;
    }

    protected void uxGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        if (e.CommandName == "Add")
        {
            GridViewRow rowAdd = uxGrid.FooterRow;

            if (IsCorrectFormatForShippingByWeight( rowAdd ) == false)
            {
                uxStatusHidden.Value = "Error";
                uxMessage.DisplayError( Resources.ShippingWeightRateMessage.TofewError );
                return;
            }

            ShippingWeightRate shippingWeightRate = new ShippingWeightRate();
            shippingWeightRate = GetDetailsFromGrid( rowAdd, shippingWeightRate );

            if (!IsExisted( shippingWeightRate.ToWeight ))
            {
                if (shippingWeightRate.ToWeight <= SystemConst.UnlimitedNumber)
                {
                    DataAccessContext.ShippingWeightRateRepository.Save( shippingWeightRate );
                    ClearData( rowAdd );
                    RefreshGrid();

                    uxStatusHidden.Value = "Added";
                    uxMessage.DisplayMessage( Resources.ShippingWeightRateMessage.ItemAddSuccess );
                }
                else
                    uxMessage.DisplayError( Resources.ShippingWeightRateMessage.TomuchItemError );
            }
            else
            {
                uxStatusHidden.Value = "Error";
                uxMessage.DisplayError( Resources.ShippingWeightRateMessage.ToWeightError );
            }
        }
        else if (e.CommandName == "Edit")
        {
            uxGrid.ShowFooter = false;
            uxAddButton.Visible = true;
        }
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

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck != null &&
                    deleteCheck.Checked)
                {
                    string shippingWeightRateID =
                        uxGrid.DataKeys[row.RowIndex]["ShippingWeightRateID"].ToString();
                    DataAccessContext.ShippingWeightRateRepository.Delete( shippingWeightRateID );
                    deleted = true;
                }
            }
            uxGrid.EditIndex = -1;

            if (deleted)
                uxMessage.DisplayMessage( Resources.ShippingWeightRateMessage.ItemDeleteSuccess );

            uxStatusHidden.Value = "Deleted";
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if (uxGrid.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }
    }

    protected void uxGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            GridViewRow rowGrid = uxGrid.Rows[e.RowIndex];

            if (IsCorrectFormatForShippingByWeight( rowGrid ) == false)
            {
                uxStatusHidden.Value = "Error";
                uxMessage.DisplayError( Resources.ShippingWeightRateMessage.TofewError );
                return;
            }

            string shippingWeightRateID = uxGrid.DataKeys[e.RowIndex]["ShippingWeightRateID"].ToString();

            ShippingWeightRate shippingWeightRate = DataAccessContext.ShippingWeightRateRepository.GetOne( shippingWeightRateID );
            shippingWeightRate = GetDetailsFromGrid( rowGrid, shippingWeightRate );

            DataAccessContext.ShippingWeightRateRepository.Save( shippingWeightRate );

            uxGrid.EditIndex = -1;
            RefreshGrid();

            uxStatusHidden.Value = "Updated";

            uxMessage.DisplayMessage( Resources.ShippingWeightRateMessage.ItemUpdateSuccess );
        }
        finally
        {
            e.Cancel = true;
        }

    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.ShippingWeightRateRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list, "ShippingWeightRateID", "ShippingID", "ToWeight" );
    }

    public string ShowFromWeight( object toWeight )
    {
        double fromWeight = 0;

        IList<ShippingWeightRate> shippingWeightRateList =
            DataAccessContext.ShippingWeightRateRepository.GetAllByShippingID( ShippingID, "ToWeight" );

        for (int i = 0; i < shippingWeightRateList.Count; i++)
        {
            if (shippingWeightRateList[i].ToWeight == ConvertUtilities.ToDouble( toWeight ))
                break;
            else
                fromWeight = shippingWeightRateList[i].ToWeight;
        }

        if (fromWeight == 0)
            return String.Format( "{0}", fromWeight );
        else
            return String.Format( "> {0}", fromWeight );
    }

    public bool CheckVisibleFromToWeight( string toWeight )
    {
        if (toWeight == SystemConst.UnlimitedNumber.ToString())
            return false;
        else
            return true;
    }

    public string LastToWeight( string toWeight )
    {
        if (toWeight == SystemConst.UnlimitedNumber.ToString())
            return "Above";
        else
            return toWeight;
    }

    protected void uxGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
        RefreshGrid();
    }

}
