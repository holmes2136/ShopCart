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
using Vevo.Domain.Shipping;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Shared.Utilities;
using Vevo.WebUI.Ajax;
using Vevo.Base.Domain;

public partial class AdminAdvanced_MainControls_ShippingOrderTotalRate : AdminAdvancedBaseUserControl
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
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "ToOrderTotal" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private void RefreshGrid()
    {
        int totalItems = 0;
        IList<ShippingOrderTotalRate> shippingOrderTotalRateList = 
            DataAccessContext.ShippingOrderTotalRateRepository.SearchShippingOrderTotalRateItem(
                ShippingID,
                GridHelper.GetFullSortText(),
                uxSearchFilter.SearchFilterObj, uxPagingControl.StartIndex,
                uxPagingControl.EndIndex,
                out totalItems );

        if (shippingOrderTotalRateList == null || shippingOrderTotalRateList.Count == 0)
        {
            shippingOrderTotalRateList = new List<ShippingOrderTotalRate>();
            CreateDummyRow( shippingOrderTotalRateList );
        }

        uxGrid.DataSource = shippingOrderTotalRateList;
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

    private void CreateDummyRow( IList<ShippingOrderTotalRate> list )
    {
        ShippingOrderTotalRate shippingOrderTotalRate = new ShippingOrderTotalRate();
        shippingOrderTotalRate.ShippingOrderTotalRateID = "-1";
        shippingOrderTotalRate.ShippingID = ShippingID;
        shippingOrderTotalRate.ToOrderTotal = 0;
        shippingOrderTotalRate.OrderTotalRate = 0;
        list.Add( shippingOrderTotalRate );
    }

    private bool IsContainingOnlyEmptyRow()
    {
        if (uxGrid.Rows.Count == 1 &&
            ConvertUtilities.ToInt32( uxGrid.DataKeys[0]["ShippingOrderTotalRateID"] ) == -1)
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
        Control textBox = uxGrid.FooterRow.FindControl( "uxToOrderTotalText" );
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
        ((TextBox) row.FindControl( "uxToOrderTotalText" )).Text = "";
        ((TextBox) row.FindControl( "uxShippingOrderTotalRate" )).Text = "";
    }

    private ShippingOrderTotalRate GetDetailsFromGrid( GridViewRow row, ShippingOrderTotalRate shippingOrderTotalRate )
    {
        shippingOrderTotalRate.ShippingID = ShippingID;
        string toOrderTotalText = ((TextBox) row.FindControl( "uxToOrderTotalText" )).Text;
        shippingOrderTotalRate.ToOrderTotal = ConvertUtilities.ToDecimal ( toOrderTotalText );

        string shippingOrderTotalRateText = ((TextBox) row.FindControl( "uxShippingOrderTotalRate" )).Text;
        shippingOrderTotalRate.OrderTotalRate = ConvertUtilities.ToDecimal( shippingOrderTotalRateText );

        return shippingOrderTotalRate;
    }

    private bool IsCorrectFormatForShippingByOrderTotal( GridViewRow row )
    {
        string toOrderTotalText = ((TextBox) row.FindControl( "uxToOrderTotalText" )).Text;
        Decimal ToOrderTotal = ConvertUtilities.ToDecimal( toOrderTotalText );
        if (ToOrderTotal < 0) return false;

        string shippingOrderTotalRateText = ((TextBox) row.FindControl( "uxShippingOrderTotalRate" )).Text;
        Decimal OrderTotalRate = ConvertUtilities.ToDecimal( shippingOrderTotalRateText );
        if (OrderTotalRate < 0) return false;

        return true;
    }

    private bool IsExisted( object addToOrderTotal )
    {
        bool isExisted = false;
        IList<ShippingOrderTotalRate> shippingOrderTotalRateList =
            DataAccessContext.ShippingOrderTotalRateRepository.GetAllByShippingID( ShippingID, "ToOrderTotal" );

        for (int i = 0; i < shippingOrderTotalRateList.Count; i++)
        {
            if (shippingOrderTotalRateList[i].ToOrderTotal == ConvertUtilities.ToDecimal( addToOrderTotal ))
                isExisted = true;
        }
        return isExisted;
    }

    protected void uxGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        if (e.CommandName == "Add")
        {
            GridViewRow rowAdd = uxGrid.FooterRow;

            if (IsCorrectFormatForShippingByOrderTotal( rowAdd ) == false)
            {
                uxStatusHidden.Value = "Error";
                uxMessage.DisplayError( Resources.ShippingOrderTotalRateMessage.TofewError );
                return;
            }

            ShippingOrderTotalRate shippingOrderTotalRate = new ShippingOrderTotalRate();
            shippingOrderTotalRate = GetDetailsFromGrid( rowAdd, shippingOrderTotalRate );

            if (!IsExisted( shippingOrderTotalRate.ToOrderTotal ))
            {
                if (shippingOrderTotalRate.ToOrderTotal <= SystemConst.UnlimitedNumberDecimal)
                {
                    DataAccessContext.ShippingOrderTotalRateRepository.Save( shippingOrderTotalRate );
                    ClearData( rowAdd );
                    RefreshGrid();

                    uxStatusHidden.Value = "Added";
                    uxMessage.DisplayMessage( Resources.ShippingOrderTotalRateMessage.ItemAddSuccess );
                }
                else
                    uxMessage.DisplayError( Resources.ShippingOrderTotalRateMessage.TomuchItemError );
            }
            else
            {
                uxStatusHidden.Value = "Error";
                uxMessage.DisplayError( Resources.ShippingOrderTotalRateMessage.ToOrderTotalError );
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
                    string shippingOrderTotalRateID =
                        uxGrid.DataKeys[row.RowIndex]["ShippingOrderTotalRateID"].ToString();
                    DataAccessContext.ShippingOrderTotalRateRepository.Delete( shippingOrderTotalRateID );
                    deleted = true;
                }
            }
            uxGrid.EditIndex = -1;

            if (deleted)
                uxMessage.DisplayMessage( Resources.ShippingOrderTotalRateMessage.ItemDeleteSuccess );

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
            if (IsCorrectFormatForShippingByOrderTotal( rowGrid ) == false)
            {
                uxStatusHidden.Value = "Error";
                uxMessage.DisplayError( Resources.ShippingOrderTotalRateMessage.TofewError );
                return;
            }

            string shippingOrderTotalRateID = uxGrid.DataKeys[e.RowIndex]["ShippingOrderTotalRateID"].ToString();

            ShippingOrderTotalRate shippingOrderTotalRate =
                DataAccessContext.ShippingOrderTotalRateRepository.GetOne( shippingOrderTotalRateID );
            shippingOrderTotalRate = GetDetailsFromGrid( rowGrid, shippingOrderTotalRate );

            DataAccessContext.ShippingOrderTotalRateRepository.Save( shippingOrderTotalRate );

            uxGrid.EditIndex = -1;
            RefreshGrid();

            uxStatusHidden.Value = "Updated";

            uxMessage.DisplayMessage( Resources.ShippingOrderTotalRateMessage.ItemUpdateSuccess );
        }
        finally
        {
            e.Cancel = true;
        }

    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.ShippingOrderTotalRateRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list, "ShippingOrderTotalRateID", "ShippingID", "ToOrderTotal" );
    }

    public string ShowFromOrderTotal( object toOrderTotal )
    {
        decimal fromOrderTotal = 0;

        IList<ShippingOrderTotalRate> shippingOrderTotalRateList =
            DataAccessContext.ShippingOrderTotalRateRepository.GetAllByShippingID( ShippingID, "ToOrderTotal" );

        for (int i = 0; i < shippingOrderTotalRateList.Count; i++)
        {
            if (shippingOrderTotalRateList[i].ToOrderTotal == ConvertUtilities.ToDecimal( toOrderTotal ))
                break;
            else
                fromOrderTotal = shippingOrderTotalRateList[i].ToOrderTotal;
        }

        if (fromOrderTotal == 0)
            return String.Format( "{0:f2}", fromOrderTotal );
        else
            return String.Format( "> {0:f2}", fromOrderTotal );
    }

    public bool CheckVisibleFromToOrderTotal( string toOrderTotal )
    {
        if (toOrderTotal == SystemConst.UnlimitedNumberDecimal.ToString())
            return false;
        else
            return true;
    }

    public string LastToOrderTotal( string toOrderTotal )
    {
        if (toOrderTotal == SystemConst.UnlimitedNumberDecimal.ToString())
            return "Above";
        else
            return toOrderTotal;
    }

    protected void uxGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
        RefreshGrid();
    }
}
