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
using Vevo.Domain;
using Vevo.Domain.Discounts;
using Vevo.Domain.Products;
using Vevo.WebUI;

public partial class CouponPage : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    #region Private
    private string _couponID = "";
    private string CouponID
    {
        get
        {
            if (!String.IsNullOrEmpty(Request.QueryString["CouponID"]))
                _couponID = Request.QueryString["CouponID"];

            return _couponID;
        }
        set
        {
            _couponID = value;
        }
    }

    private void DisplayExpiredError(Coupon.ExpirationStatusEnum expirationStatus)
    {
        switch (expirationStatus)
        {
            case Coupon.ExpirationStatusEnum.ExpiredByDate:
                uxMessage.DisplayError("[$Expire]");
                break;
            case Coupon.ExpirationStatusEnum.ExpiredByQuantity:
                uxMessage.DisplayError("[$Limit]");
                break;
            default:
                uxMessage.DisplayError("[$NotValid]");
                break;
        }
        StoreContext.CheckoutDetails.SetCouponID("");
    }

    private void DisplayCouponDetails()
    {
        uxMessage.DisplayMessage("[$Accept]");

        Coupon coupon = DataAccessContext.CouponRepository.GetOne(CouponID);
        uxCouponMessageDisplay.DisplayCouponDetails(coupon, "all_product");
    }

    private bool IsDiscountableProduct(Product product)
    {
        return product.DiscountGroupID != "0";
    }

    private bool ValidateCustomer(Coupon coupon, out string errorMessage)
    {
        errorMessage = String.Empty;
        if (coupon.CustomerFilter == Coupon.CustomerFilterEnum.ByUserNames)
        {
            if (StoreContext.Customer.IsNull)
            {
                errorMessage = "[$ThistCouponIsOnlyValidForUser]";
                //This coupon is specifie for user please log in before use this coupon.
                return false;
            }

            if (!coupon.IsCustomerDiscountable(StoreContext.Customer))
            {
                errorMessage = "[$NotValidForYourUser]";
                //This coupon is not valid for your log-in account.
                return false;
            }
        }

        return true;
    }

    private bool VerifyCoupon(string couponID)
    {
        bool couponValid = false;
        Coupon coupon = DataAccessContext.CouponRepository.GetOne(couponID);
        uxCouponLiteral.Text = "[$CouponCodeLabel]\"" + couponID + "\"";
        
        // Verify if coupon expired
        if (coupon.ExpirationStatus != Coupon.ExpirationStatusEnum.NotExpired)
        {
            if (coupon.IsNull)
                DisplayExpiredError(Coupon.ExpirationStatusEnum.UnknownType);
            else
                DisplayExpiredError(coupon.ExpirationStatus);
            return couponValid;
        }

        string errorMessage;
        if (!ValidateCustomer(coupon, out errorMessage))
        {
            uxMessage.DisplayError(errorMessage);
            return couponValid;
        }

        couponValid = true;
        return couponValid;
    }

    #endregion


    #region Protected

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        uxCouponMessageDisplay.HideAll();

        if (String.IsNullOrEmpty(CouponID))
        {
            uxNoCouponDiv.Visible = true;
            uxMessage.DisplayError("[$NoCoupon]");
        }
        else
        {
            bool couponValid = VerifyCoupon(CouponID);
            if (couponValid)
            {
                DisplayCouponDetails();
                StoreContext.CheckoutDetails.SetCouponID(CouponID);
            }
        }
    }

    protected void uxCouponButton_Click(object sender, EventArgs e)
    {
        _couponID = uxCouponIDText.Text;
        bool couponValid = VerifyCoupon(CouponID);
        if (couponValid)
        {
            uxCouponLiteral.Visible = true;
            DisplayCouponDetails();
            StoreContext.CheckoutDetails.SetCouponID(CouponID);
        }
        else
            uxCouponLiteral.Visible = false;

        uxCouponIDText.Text = "";

    }
    #endregion

}
