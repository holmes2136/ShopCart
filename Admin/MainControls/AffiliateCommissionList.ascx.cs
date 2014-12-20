using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.Marketing;
using Vevo.Shared.Utilities;
using Vevo.Shared.DataAccess;

public partial class AdminAdvanced_MainControls_AffiliateCommissionList : AdminAdvancedBaseUserControl
{
    private const int AffiliateOrderIDIndex = 1;

    private string AffiliateCode
    {
        get
        {
            return MainContext.QueryString["AffiliateCode"];
        }
    }

    private string CurrentPending
    {
        get
        {
            if (ViewState["CurrentPending"] == null)
                return "False";
            else
                return ViewState["CurrentPending"].ToString();
        }
        set
        {
            ViewState["CurrentPending"] = value;
        }
    }

    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "AffiliateOrderID" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContextDeluxe.AffiliateOrderRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list, "AffiliateCode" );
    }

    private string CreatePaymentFilter()
    {
        if (uxPaidDrop.SelectedValue == "Paid")
        {
            return "AffiliatePaymentID > 0 ";
        }
        else if (uxPaidDrop.SelectedValue == "UnPaid")
        {
            return "AffiliatePaymentID = 0 ";
        }
        else
        {
            return "1 = 1 ";
        }
    }

    private void RefreshGrid()
    {
        int totalItems;
        IList<AffiliateOrder> affiliateOrderList = DataAccessContextDeluxe.AffiliateOrderRepository.SearchAffiliateOrder(
            AffiliateCode,
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            CreatePaymentFilter(),
            (uxPagingControl.CurrentPage - 1) * uxPagingControl.ItemsPerPages,
            (uxPagingControl.CurrentPage * uxPagingControl.ItemsPerPages) - 1,
            out totalItems );

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
        uxGrid.DataSource = affiliateOrderList;
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
            uxProcessedButton.Visible = false;
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
            uxProcessedButton.Visible = true;
        }
        else
        {
            DeleteVisible( false );
            uxPagingControl.Visible = false;
            uxProcessedButton.Visible = false;
        }

        Affiliate affiliate = DataAccessContextDeluxe.AffiliateRepository.GetOne( AffiliateCode );
        uxAffiliateNameLabel.Text = affiliate.ContactAddress.FirstName + " " + affiliate.ContactAddress.LastName;
        uxAffiliateUserNameLabel.Text = affiliate.UserName;
        GetPaymentListLink();
    }

    private void GetPaymentListLink()
    {
        uxPaymentListLink.PageName = "AffiliatePaymentList.ascx";
        uxPaymentListLink.PageQueryString = "AffiliateCode=" + AffiliateCode;
    }

    private void uxGrid_RefreshPageHandler( object sender, EventArgs e )
    {
        uxPagingControl.CurrentPage = 1;
        RefreshGrid();
    }

    private void uxGrid_RefreshHandler( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    private void SetupCurrentValue()
    {
        if (uxGrid.EditIndex != -1)
        {
            HiddenField pending = (HiddenField) (uxGrid.Rows[uxGrid.EditIndex].Cells[7].FindControl( "uxPendingHidden" ));
            if (pending != null)
                CurrentPending = pending.Value;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
            MainContext.RedirectMainControl( "Default.ascx", String.Empty );

        if (String.IsNullOrEmpty( AffiliateCode ))
            MainContext.RedirectMainControl( "AffiliateList.ascx" );

        uxSearchFilter.BubbleEvent += new EventHandler( uxGrid_RefreshPageHandler );
        uxPagingControl.BubbleEvent += new EventHandler( uxGrid_RefreshHandler );

        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.AffiliateOrderPerPage;
            SetUpSearchFilter();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
        ApplyPermissions();
        SetupCurrentValue();
    }

    protected void uxPaidDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    protected void uxGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
        RefreshGrid();
    }

    protected void uxProcessedButton_Click( object sender, EventArgs e )
    {
        string affiliateOrderID = String.Empty;
        foreach (GridViewRow row in uxGrid.Rows)
        {
            CheckBox check = (CheckBox) row.FindControl( "uxCheck" );
            if (check.Checked)
            {
                string id = ((Label) row.Cells[AffiliateOrderIDIndex].FindControl( "uxAffiliateOrderIDLabel" )).Text.Trim();
                if (String.IsNullOrEmpty( affiliateOrderID ))
                    affiliateOrderID = id;
                else
                    affiliateOrderID += "-" + id;
            }
        }
        if (!String.IsNullOrEmpty( affiliateOrderID ))
            MainContext.RedirectMainControl( "AffiliatePaymentAdd.ascx", "AffiliateOrderID=" + affiliateOrderID +
                "&AffiliateCode=" + AffiliateCode );
        else
            uxMessage.DisplayError( Resources.AffiliateCommissionMessages.SelecteItem );
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
                    string id = ((Label) row.Cells[AffiliateOrderIDIndex].FindControl( "uxAffiliateOrderIDLabel" )).Text.Trim();
                    DataAccessContextDeluxe.AffiliateOrderRepository.Delete( id );
                    deleted = true;
                }
            }
            RefreshGrid();
            if (deleted)
                uxMessage.DisplayMessage( Resources.AffiliateCommissionMessages.DeleteSuccess );

        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();
    }

    protected void uxGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            string affiliateOrderID = ((Label) uxGrid.Rows[e.RowIndex].FindControl( "uxAffiliateOrderIDLabel" )).Text;
            decimal commission = ConvertUtilities.ToDecimal( ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxCommissionText" )).Text );
            bool pending = ConvertUtilities.ToBoolean(
                ((DropDownList) uxGrid.Rows[e.RowIndex].FindControl( "uxPendingDrop" )).SelectedValue );

            if (!String.IsNullOrEmpty( affiliateOrderID ))
            {
                AffiliateOrder affiliateOrder = DataAccessContextDeluxe.AffiliateOrderRepository.GetOne( affiliateOrderID );
                affiliateOrder.Commission = commission;
                affiliateOrder.AffiliatePaymentID = "0";
                affiliateOrder.Pending = pending;
                DataAccessContextDeluxe.AffiliateOrderRepository.Save( affiliateOrder );
            }

            // End editing
            uxGrid.EditIndex = -1;
            RefreshGrid();

            uxMessage.DisplayMessage( Resources.AffiliateCommissionMessages.UpdateSuccess );
        }
        catch (Exception)
        {
            string message = Resources.AffiliateCommissionMessages.UpdateError;
            throw new ApplicationException( message );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;
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

    protected bool CanPayCommission( object paymentStatus, object pending )
    {
        if (ConvertUtilities.ToBoolean( paymentStatus ) ||
            ConvertUtilities.ToBoolean( pending ))
            return false;
        else
            return true;
    }

    protected void uxPendingDrop_PreRender( object sender, EventArgs e )
    {
        DropDownList uxPendingDrop = (DropDownList) sender;
        uxPendingDrop.SelectedValue = CurrentPending;
    }

    protected bool SetEditLinkButtonVisible()
    {
        if (ConvertUtilities.ToBoolean( Eval( "PaymentStatus" ) ) || !IsAdminModifiable())
        {
            return false;
        }
        return true;
    }
}
