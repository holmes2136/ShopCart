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
using Vevo.Domain.Marketing;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.DataInterfaces;
using Vevo.Deluxe.Domain.Marketing;
using Vevo.Shared.DataAccess;

public partial class AdminAdvanced_MainControls_AffiliatePaymentList : AdminAdvancedBaseUserControl
{
    private const int ColumnAffiliatePaymentID = 1;

    private string AffiliateCode
    {
        get
        {
            return MainContext.QueryString["AffiliateCode"];
        }
    }

    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "AffiliatePaymentID" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContextDeluxe.AffiliatePaymentRepository.GetTableSchema();

        uxSearchFilter.SetUpSchema( list, "AffiliateCode" );
    }

    private void RefreshGrid()
    {
        int totalItems;

        uxGrid.DataSource = DataAccessContextDeluxe.AffiliatePaymentRepository.SearchAffiliatePayment(
            AffiliateCode,
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            (uxPagingControl.CurrentPage - 1) * uxPagingControl.ItemsPerPages,
            (uxPagingControl.CurrentPage * uxPagingControl.ItemsPerPages) - 1,
            out totalItems );
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGrid.DataBind();
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

    private void ApplyPermissions()
    {
        if (!IsAdminModifiable())
        {
            DeleteVisible( false );
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

        Affiliate affiliate = DataAccessContextDeluxe.AffiliateRepository.GetOne( AffiliateCode );
        uxAffiliateNameLabel.Text = affiliate.ContactAddress.FirstName + " " + affiliate.ContactAddress.LastName;
        uxAffiliateUserNameLabel.Text = affiliate.UserName;
        GetCommissionListLink();
    }

    private void GetCommissionListLink()
    {
        uxCommissionListLink.PageName = "AffiliateCommissionList.ascx";
        uxCommissionListLink.PageQueryString = "AffiliateCode=" + AffiliateCode;
    }

    private void uxGrid_RefreshHandler( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
            MainContext.RedirectMainControl( "Default.ascx", String.Empty );

        if (String.IsNullOrEmpty( AffiliateCode ))
            MainContext.RedirectMainControl( "AffiliateList.ascx" );

        uxSearchFilter.BubbleEvent += new EventHandler( uxGrid_RefreshHandler );
        uxPagingControl.BubbleEvent += new EventHandler( uxGrid_RefreshHandler );

        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.AffiliatePaymentPerPage;
            SetUpSearchFilter();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
        ApplyPermissions();
    }

    protected void uxGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
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
                if (deleteCheck.Checked)
                {
                    string id = row.Cells[ColumnAffiliatePaymentID].Text.Trim();
                    DataAccessContextDeluxe.AffiliatePaymentRepository.Delete( id );
                    deleted = true;
                }
            }

            if (deleted)
            {
                uxMessage.DisplayMessage( Resources.AffiliatePaymentMessages.DeleteSuccess );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();
    }

    protected string GetAllOrderID( object affiliatePaymentID )
    {
        return DataAccessContextDeluxe.AffiliateOrderRepository.GetOrderIDByAffiliatePaymentID( affiliatePaymentID.ToString() );
    }

    protected string ShowDate( object paiddate )
    {
        return ((DateTime) paiddate).ToShortDateString();
    }
}
