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
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.WebAppLib;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.DataInterfaces;
using Vevo.Deluxe.Domain.Marketing;
using Vevo.Shared.DataAccess;

public partial class AdminAdvanced_MainControls_AffiliateList : AdminAdvancedBaseUserControl
{
    private const int ColumnUserName = 1;

    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "AffiliateCode" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private void HilightDisabledRow()
    {
        foreach (GridViewRow row in uxGrid.Rows)
        {
            CheckBox uxCheck = (CheckBox) row.FindControl( "uxIsEnabledCheck" );
            if (!uxCheck.Checked)
            {
                foreach (TableCell cell in row.Cells)
                {
                    cell.Style.Add( "color", "#ff0000" );
                }
            }
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContextDeluxe.AffiliateRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list );
    }

    private void RefreshGrid()
    {
        int totalItems;

        uxGrid.DataSource = DataAccessContextDeluxe.AffiliateRepository.SearchAffiliate(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            (uxPagingControl.CurrentPage - 1) * uxPagingControl.ItemsPerPages,
            (uxPagingControl.CurrentPage * uxPagingControl.ItemsPerPages) - 1,
            out totalItems );
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGrid.DataBind();

        HilightDisabledRow();
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
            DeleteVisible( false );
            uxAddButton.Visible = false;
        }
        GetCommissionPendingListLink();
    }

    private void uxGrid_RefreshHandler( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    private void uxGrid_ResetPageHandler( object sender, EventArgs e )
    {
        uxPagingControl.CurrentPage = 1;
        RefreshGrid();
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

    private void GetCommissionPendingListLink()
    {
        uxCommissionPendingListLink.PageName = "AffiliateCommissionPendingList.ascx";
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
            MainContext.RedirectMainControl( "Default.ascx", String.Empty );

        uxSearchFilter.BubbleEvent += new EventHandler( uxGrid_ResetPageHandler );
        uxPagingControl.BubbleEvent += new EventHandler( uxGrid_RefreshHandler );

        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.AffiliatePerPage;
            SetUpSearchFilter();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
        RefreshGrid();
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "AffiliateAdd.ascx", "" );
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string code = ((HiddenField) row.FindControl( "uxAffiliateCodeHidden" )).Value;

                    DataAccessContextDeluxe.AffiliateRepository.Delete( code );
                    Membership.DeleteUser( row.Cells[ColumnUserName].Text.Trim() );
                    deleted = true;
                }
            }

            if (deleted)
            {
                uxMessage.DisplayMessage( Resources.AffiliateMessages.DeleteSuccess );
                //uxStatusHidden.Value = "Deleted";
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();
    }
}
