using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Base.Domain;

public partial class AdminAdvanced_MainControls_CouponList : AdminAdvancedBaseListControl
{

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.CouponRepository.GetTableSchema();

        NameValueCollection renameList = new NameValueCollection();
        renameList.Add( "CurrentQuantity", "Usage" );

        uxSearchFilter.SetUpSchema( list, renameList, "UrlName" );
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

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.CouponItemsPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxGrid, "CouponID" );

        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
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
                    string id = row.Cells[1].Text.Trim();
                    DataAccessContext.CouponRepository.Delete( id );
                    deleted = true;
                }
            }

            if (deleted)
            {
                uxMessage.DisplayMessage( Resources.CouponMessages.DeleteSuccess );
            }
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

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "CouponAdd.ascx", "" );
    }

    protected bool IsAmount( string discountType )
    {
        if (discountType == "Price" || discountType == "BuyXDiscountYPrice")
            return true;
        else
            return false;
    }

    protected bool IsExpirationDate( string expirationType )
    {
        if (expirationType == "Date")
            return true;
        else
            return false;
    }

    protected string ExpirationMessage( string expirationType, string expirationDate, string expirationQuantity, string type )
    {
        if (expirationType == "Date" && type == "Date")
            return Convert.ToDateTime( expirationDate ).ToShortDateString();
        else if (expirationType == "Quantity" && type == "Quantity")
            return expirationQuantity;
        else if (expirationType == "Both")
        {
            if (type == "Date")
                return String.Format( "{0}", Convert.ToDateTime( expirationDate ).ToShortDateString() );
            else if (type == "Quantity")
                return String.Format( "{0}", expirationQuantity );
        }
        return String.Empty;
    }

    protected string GetDiscountTypeText(object discountType)
    {
        String discountTypeText = discountType.ToString();

        if (discountTypeText == "BuyXDiscountYPrice")
            discountTypeText = "Buy X Discount Y Price";
        else if (discountTypeText == "BuyXDiscountYPercentage")
            discountTypeText = "Buy X Discount Y Percentage";
        return discountTypeText;
    }

    protected string GetAmountText( object discountType, object amount, object percentage )
    {
        if (IsAmount( discountType.ToString() ))
            return String.Format( "{0:n2}", amount );
        else
            return percentage + "%";
    }

    protected override void RefreshGrid()
    {
        int totalItems;

        uxGrid.DataSource = DataAccessContext.CouponRepository.SearchCoupon(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems
            );

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGrid.DataBind();
    }
}
