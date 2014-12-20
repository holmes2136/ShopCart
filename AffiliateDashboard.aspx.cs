using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Marketing;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.DataInterfaces;
using Vevo.Deluxe.Domain.Marketing;

public partial class AffiliateDashboard : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    private string AffiliateCode
    {
        get
        {
            return DataAccessContextDeluxe.AffiliateRepository.GetCodeFromUserName( HttpContext.Current.User.Identity.Name );
        }
    }

    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "OrderID", GridViewHelper.Direction.DESC );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private String StartOrderID
    {
        get
        {
            if (uxSearchFilter.FieldValue == "OrderID")
                return uxSearchFilter.Value1;
            else
                return Request.QueryString["StartOrderID"];
        }
    }

    private string EndOrderID
    {
        get
        {
            if (uxSearchFilter.FieldValue == "OrderID")
                return uxSearchFilter.Value2;
            else
                return Request.QueryString["EndOrderID"];
        }
    }

    private string StartAmount
    {
        get
        {
            if (uxSearchFilter.FieldValue == "ProductCost")
                return uxSearchFilter.Value1;
            else
                return Request.QueryString["StartAmount"];
        }
    }

    private string EndAmount
    {
        get
        {
            if (uxSearchFilter.FieldValue == "ProductCost")
                return uxSearchFilter.Value2;
            else
                return Request.QueryString["EndAmount"];
        }
    }

    private string StartCommission
    {
        get
        {
            if (uxSearchFilter.FieldValue == "Commission")
                return uxSearchFilter.Value1;
            else
                return Request.QueryString["StartCommission"];
        }
    }

    private string EndCommission
    {
        get
        {
            if (uxSearchFilter.FieldValue == "Commission")
                return uxSearchFilter.Value2;
            else
                return Request.QueryString["EndCommission"];
        }
    }

    private string StartOrderDate
    {
        get
        {
            if (uxSearchFilter.FieldValue == "OrderDate")
                return uxSearchFilter.Value1;
            else
                return Request.QueryString["StartOrderDate"];
        }
    }

    private string EndOrderDate
    {
        get
        {
            if (uxSearchFilter.FieldValue == "OrderDate")
                return uxSearchFilter.Value2;
            else
                return Request.QueryString["EndOrderDate"];
        }
    }

    private string PaymentStatus
    {
        get
        {
            if (uxSearchFilter.FieldValue == "PaymentStatus")
                return uxSearchFilter.Value1;
            else
                return Request.QueryString["PaymentStatus"];
        }
    }

    private bool IsEnabled
    {
        get
        {
            if (ViewState["CurrentIsEnabled"] == null)
                return true;
            else
                return (bool) ViewState["CurrentIsEnabled"];
        }

        set { ViewState["CurrentIsEnabled"] = value; }
    }

    private void RefreshGrid()
    {
        int totalItems = 0;
        IList<AffiliateOrder> affiliateOrderList = new List<AffiliateOrder>();

        affiliateOrderList = DataAccessContextDeluxe.AffiliateOrderRepository.SearchCommission(
                GridHelper.GetFullSortText(),
                AffiliateCode,
                StartOrderID,
                EndOrderID,
                StartAmount,
                EndAmount,
                StartCommission,
                EndCommission,
                StartOrderDate,
                EndOrderDate,
                PaymentStatus,
                0,
                4,
                out totalItems );

        uxGrid.DataSource = affiliateOrderList;
        uxGrid.DataBind();
    }

    private void PopulateControls()
    {
        if (!string.IsNullOrEmpty( AffiliateCode ))
        {
            Affiliate affiliate = DataAccessContextDeluxe.AffiliateRepository.GetOne( AffiliateCode );
            uxFirstNameLabel.Text = affiliate.ContactAddress.FirstName;
            uxLastNameLable.Text = affiliate.ContactAddress.LastName;
            uxUsernameLabel.Text = affiliate.UserName;
            uxCompanyLabel.Text = affiliate.ContactAddress.Company;
            uxAddress1Label.Text = affiliate.ContactAddress.Address1;
            uxAddress2Label.Text = affiliate.ContactAddress.Address2;
            uxCityLabel.Text = affiliate.ContactAddress.City;
            uxStateLabel.Text = affiliate.ContactAddress.State;
            uxCountryLabel.Text = DataAccessContext.CountryRepository.GetOne( affiliate.ContactAddress.Country ).CommonName;
            uxZipLabel.Text = affiliate.ContactAddress.Zip;
            uxPhoneLabel.Text = affiliate.ContactAddress.Phone;
            uxFaxLabel.Text = affiliate.ContactAddress.Fax;
            uxEmailLabel.Text = affiliate.Email;
            uxWebsiteLabel.Text = affiliate.Website;
            uxCommissionRateLabel.Text = affiliate.CommissionRate.ToString();
            IsEnabled = affiliate.IsEnabled;
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContextDeluxe.AffiliateOrderRepository.GetCommissionSchema();
        uxSearchFilter.SetUpSchema( list );
    }

    private string GetTotal()
    {
        return DataAccessContextDeluxe.AffiliateOrderRepository.GetTotalCommission(
            AffiliateCode,
            StartOrderID,
            EndOrderID,
            StartAmount,
            EndAmount,
            StartCommission,
            EndCommission,
            StartOrderDate,
            EndOrderDate,
            PaymentStatus
        );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControls();
            SetUpSearchFilter();
            RefreshGrid();
        }
    }

    protected void uxEditAccountButton_OnClick( object sender, EventArgs e )
    {
        Page.Response.Redirect( "~/affiliatedetails.aspx?editmode=account" );
    }

    protected void uxEditAddressButton_OnClick( object sender, EventArgs e )
    {
        Page.Response.Redirect("~/affiliatedetails.aspx?editmode=address");
    }

    protected void uxSearchButton_OnClick( object sender, EventArgs e )
    {
        Page.Response.Redirect( "~/affiliatecommissionsearch.aspx" );
    }

    protected string GetPaymentStatus( object affiliatePaymentID )
    {
        if (affiliatePaymentID.ToString() == "0")
            return "No";
        else
            return "Yes";
    }

    protected string ShowOnlyDate( object orderDate )
    {
        DateTime date = (DateTime) orderDate;
        return date.ToShortDateString();
    }

    public void SetFooter( Object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            TableCellCollection cells = e.Row.Cells;
            cells[0].CssClass = "AffiliateCommissionFooterFirstColumn";
            cells[1].Text = "Total";
            cells[1].CssClass = "AffiliateCommissionFooterTotalColumn";
            cells[2].Text = AdminUtilities.FormatPrice( Convert.ToDecimal( GetTotal() ) );
            cells[2].CssClass = "AffiliateCommissionFooterTotalAmountColumn";
            cells[3].CssClass = "AffiliateCommissionFooterEndColumn";
        }
    }
}