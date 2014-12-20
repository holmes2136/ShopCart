using System;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Discounts;

public partial class AdminAdvanced_Components_CouponCondition : AdminAdvancedBaseUserControl
{
    private bool VerifyProductIDs( out string errorMessage )
    {
        errorMessage = String.Empty;

        if (uxAllProductRadio.Checked)
        {
        }
        else if (uxProductIDRadio.Checked)
        {
            if (String.IsNullOrEmpty( uxProductIDText.Text ))
            {
                errorMessage = "Product IDs cannot be blank.";
                return false;
            }
        }
        else
        {
            if (String.IsNullOrEmpty( uxCategoryText.Text ))
            {
                errorMessage = "Category IDs cannot be blank.";
                return false;
            }
        }

        return true;
    }

    private bool VerifyCustomerNames( out string errorMessage )
    {
        errorMessage = String.Empty;

        if (!uxAllCustomerRadio.Checked && uxCustomerIDRadio.Checked && 
            String.IsNullOrEmpty( uxCustomerNameText.Text ))
        {
            errorMessage = "Customer username cannot be blank";
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool VerifyCustomerNamesOnceOnly( out string errorMessage )
    {
        errorMessage = String.Empty;

        if (!uxAllCustomerRadio.Checked && uxCustomerOnceOnly.Checked &&
            String.IsNullOrEmpty( uxCustomerOnceOnlyText.Text ))
        {
            errorMessage = "Customer username cannot be blank";
            return false;
        }
        else
        {
            return true;
        }
    }


    protected void Page_Load( object sender, EventArgs e )
    {
    }

    public string CouponID
    {
        get
        {
            if (ViewState["CouponID"] == null)
                return "0";
            return ViewState["CouponID"].ToString();
        }
        set
        {
            ViewState["CouponID"] = value;
        }
    }

    public string ProductIDText
    {
        get
        {
            if (uxProductIDRadio.Checked)
                return uxProductIDText.Text;
            else
                return String.Empty;
        }
    }

    public string CategoryText
    {
        get
        {
            if (uxCategoryIDRadio.Checked)
                return uxCategoryText.Text;
            else
                return String.Empty;
        }
    }

    public string CustomerNameText
    {
        get
        {
            if (uxCustomerIDRadio.Checked)
                return uxCustomerNameText.Text;
            else
                return String.Empty;
        }
    }

    public string CustomerNameOnceOnlyText
    {
        get
        {
            if (uxCustomerOnceOnly.Checked)
                return uxCustomerOnceOnlyText.Text;
            else
                return String.Empty;
        }
    }

    public Coupon.ProductFilterEnum ProductFilter
    {
        get
        {
            if (uxAllProductRadio.Checked)
                return Coupon.ProductFilterEnum.All;
            else if (uxProductIDRadio.Checked)
                return Coupon.ProductFilterEnum.ByProductIDs;
            else
                return Coupon.ProductFilterEnum.ByCategoryIDs;
        }
    }

    public Coupon.CustomerFilterEnum CustomerFilter
    {
        get
        {
            if (uxAllCustomerRadio.Checked)
                return Coupon.CustomerFilterEnum.All;
            else
                if (uxCustomerIDRadio.Checked)
                    return Coupon.CustomerFilterEnum.ByUserNames;
                else
                    return Coupon.CustomerFilterEnum.ByUserNamesOnceOnly;
        }
    }

    public void Refresh( Coupon coupon )
    {
        if (coupon.ProductFilter == Coupon.ProductFilterEnum.All)
            uxAllCustomerRadio.Checked = true;
        else if (coupon.ProductFilter == Coupon.ProductFilterEnum.ByProductIDs)
            uxProductIDRadio.Checked = true;
        else
            uxCategoryIDRadio.Checked = true;

        if (coupon.CustomerFilter == Coupon.CustomerFilterEnum.All)
            uxAllCustomerRadio.Checked = true;
        else if (coupon.CustomerFilter == Coupon.CustomerFilterEnum.ByUserNames)
            uxCustomerIDRadio.Checked = true;
        else
            uxCustomerOnceOnly.Checked = true;

        uxProductIDText.Text = coupon.ProductIDs;
        uxCategoryText.Text = coupon.CategoryIDs;
        uxCustomerNameText.Text = coupon.CustomerUserName;
        uxCustomerOnceOnlyText.Text = coupon.GetUserNamesOnlyOnce();
    }

    public bool Verify( out string errorMessage )
    {
        errorMessage = String.Empty;

        return VerifyProductIDs( out errorMessage ) && VerifyCustomerNames( out errorMessage ) && VerifyCustomerNamesOnceOnly(out errorMessage);
    }


    public void ClearInput()
    {
        uxAllProductRadio.Checked = true;
        uxProductIDRadio.Checked = false;
        uxCategoryIDRadio.Checked = false;

        uxProductIDText.Text = "";
        uxCategoryText.Text = "";

        uxAllCustomerRadio.Checked = true;
        uxCustomerIDRadio.Checked = false;
        uxCustomerOnceOnly.Checked = false;

        uxCustomerNameText.Text = "";
        uxCustomerOnceOnlyText.Text = "";
    }
}
