using System;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Discounts;
using Vevo.Domain.Payments;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Domain.Users;
using Vevo.Shared.Utilities;

public partial class Components_CouponDetails : AdminAdvancedBaseUserControl
{
    #region Private

    private const int DefaultCouponLength = 6;

    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string _couponID = String.Empty;

    private string CurrentID
    {
        get
        {
            if (ViewState["CouponID"] == null)
                return MainContext.QueryString["CouponID"];
            else
                return (string) ViewState["CouponID"];
        }
        set
        {
            ViewState["CouponID"] = value;
        }
    }

    private void ClearInputFields()
    {
        uxCouponIDText.Text = "";
        uxDiscountTypeDrop.SelectedValue = Coupon.DiscountTypeEnum.Price.ToString();
        uxExpirationTypeDrop.SelectedValue = Coupon.ExpirationTypeEnum.Date.ToString();
        uxDiscountAmountText.Text = "";
        uxPercentageText.Text = "";
        uxMinimumQuantityText.Text = "";
        uxPromotionQuantityText.Text = "";
        uxRepeatDiscountCheckBox.Checked = false;
        uxRegisterDateCalendarPopup.Reset();
        uxRegisterDateCalendarPopup.SelectedDate = DateTime.Now;
        uxExpirationQuantityText.Text = "";
        uxCurrentQuantityText.Text = "";
        uxMerchantNotesText.Text = "";
        uxMinimumSubtotalText.Text = "";
        uxAllProductRadio.Checked = true;
        uxEligibleProductRadio.Checked = false;
        uxCouponCondition.ClearInput();

        ShowControlsExpirationDate();
        ShowControlsAmount();
    }

    private void PopulateControls()
    {
        if (!String.IsNullOrEmpty( CurrentID ))
        {
            Coupon coupon = DataAccessContext.CouponRepository.GetOne( CurrentID );
            uxCouponIDText.Text = CurrentID;
            uxDiscountTypeDrop.SelectedValue = coupon.DiscountType.ToString();
            uxDiscountAmountText.Text = coupon.DiscountAmount.ToString( "n2" );
            uxPercentageText.Text = coupon.Percentage.ToString();
            uxMinimumQuantityText.Text = coupon.MinimumQuantity.ToString();
            uxPromotionQuantityText.Text = coupon.PromotionQuantity.ToString();
            uxRepeatDiscountCheckBox.Checked = coupon.RepeatDiscount;
            uxExpirationTypeDrop.SelectedValue = coupon.ExpirationType.ToString();
            uxRegisterDateCalendarPopup.SelectedDate = coupon.ExpirationDate;
            uxExpirationQuantityText.Text = coupon.ExpirationQuantity.ToString();
            uxCurrentQuantityText.Text = coupon.CurrentQuantity.ToString();
            uxMerchantNotesText.Text = coupon.MerchantNotes;

            uxCouponCondition.Refresh( coupon );
            uxMinimumSubtotalText.Text = String.Format( "{0}", coupon.MinimumSubtotal );

            SetControlsDiscount();
            SetControlsExpiration();

            if (coupon.ProductCostType == Coupon.ProductCostTypeEnum.CouponEligibleProducts)
                uxEligibleProductRadio.Checked = true;
            else
                uxAllProductRadio.Checked = true;

            uxFreeShippingTypeDrop.SelectedValue = coupon.FreeShippingType.ToString();
        }
    }

    private bool ValidProductIDs( string[] productIDs )
    {
        if (productIDs == null)
            return true;

        for (int i = 0; i < productIDs.Length; i++)
        {
            Product product = DataAccessContext.ProductRepository.GetOne( 
                Culture.Null, productIDs[i], new StoreRetriever().GetCurrentStoreID() );
            if (product.IsNull)
                return false;
        }

        return true;
    }

    private bool IsProductIDExist()
    {
        if (!String.IsNullOrEmpty( uxCouponCondition.ProductIDText ))
        {
            return ValidProductIDs( uxCouponCondition.ProductIDText.Trim().Split( ',' ) );
        }
        else
        {
            return true;
        }
    }

    private bool ValidCategoryIDs( string[] categoryIDs )
    {
        if (categoryIDs == null)
            return true;

        for (int i = 0; i < categoryIDs.Length; i++)
        {
            Category category = DataAccessContext.CategoryRepository.GetOne( Culture.Null, categoryIDs[i] );
            if (category.IsNull)
                return false;
        }

        return true;
    }

    private bool IsCategoryIDExist()
    {
        if (!String.IsNullOrEmpty( uxCouponCondition.CategoryText ))
        {
            return ValidCategoryIDs( uxCouponCondition.CategoryText.Trim().Split( ',' ) );
        }
        else
        {
            return true;
        }
    }

    private bool IsCustomerExist()
    {
        if (!String.IsNullOrEmpty( uxCouponCondition.CustomerNameText ))
        {
            return Customer.ValidCustomerUserNames( uxCouponCondition.CustomerNameText.Trim().Split( ',' ) );
        }
        else
        {
            return true;
        }
    }

    private bool IsCustomerOnceOnlyExist()
    {
        if (!String.IsNullOrEmpty( uxCouponCondition.CustomerNameOnceOnlyText ))
        {
            return Customer.ValidCustomerUserNames( uxCouponCondition.CustomerNameOnceOnlyText.Trim().Split( ',' ) );
        }
        else
        {
            return true;
        }
    }

    private Coupon.FreeShippingTypeEnum GetFreeShippingType()
    {
        return (Coupon.FreeShippingTypeEnum) Enum.Parse( typeof( Coupon.FreeShippingTypeEnum ), uxFreeShippingTypeDrop.SelectedValue );
    }

    private Coupon.DiscountTypeEnum GetDiscountType()
    {
        return (Coupon.DiscountTypeEnum) Enum.Parse( typeof( Coupon.DiscountTypeEnum ), uxDiscountTypeDrop.SelectedValue );
    }

    private Coupon.ExpirationTypeEnum GetExpirationType()
    {
        return (Coupon.ExpirationTypeEnum) Enum.Parse( typeof( Coupon.ExpirationTypeEnum ), uxExpirationTypeDrop.SelectedValue );
    }

    private DateTime GetExpirationDate()
    {
        if (string.IsNullOrEmpty( uxRegisterDateCalendarPopup.SelectedDateText ))
            return DateTime.Today;
        else
            return uxRegisterDateCalendarPopup.SelectedDate;
    }

    private Coupon.ProductCostTypeEnum GetProductCostType()
    {
        if (uxEligibleProductRadio.Checked)
            return Coupon.ProductCostTypeEnum.CouponEligibleProducts;
        else
            return Coupon.ProductCostTypeEnum.AnyProduct;
    }

    private Coupon SetupCoupon( Coupon coupon )
    {
        _couponID = uxCouponIDText.Text;

        coupon.CouponID = uxCouponIDText.Text.Trim();
        coupon.DiscountType = GetDiscountType();
        coupon.DiscountAmount = ConvertUtilities.ToDecimal( uxDiscountAmountText.Text );
        coupon.Percentage = ConvertUtilities.ToDouble( uxPercentageText.Text );
        coupon.MinimumQuantity = ConvertUtilities.ToInt32( uxMinimumQuantityText.Text );
        coupon.PromotionQuantity = ConvertUtilities.ToInt32( uxPromotionQuantityText.Text );
        coupon.RepeatDiscount = uxRepeatDiscountCheckBox.Checked;
        coupon.ExpirationType = GetExpirationType();
        coupon.ExpirationDate = GetExpirationDate();
        coupon.ExpirationQuantity = ConvertUtilities.ToInt32( uxExpirationQuantityText.Text );
        coupon.CurrentQuantity = ConvertUtilities.ToInt32( uxCurrentQuantityText.Text );
        coupon.MerchantNotes = uxMerchantNotesText.Text;
        coupon.ProductFilter = uxCouponCondition.ProductFilter;
        coupon.ProductIDs = uxCouponCondition.ProductIDText;
        coupon.CategoryIDs = uxCouponCondition.CategoryText;
        coupon.CustomerFilter = uxCouponCondition.CustomerFilter;
        coupon.CustomerUserName = uxCouponCondition.CustomerNameText;
        coupon.GenerateCouponCustomerByUserNamesOnceOnly( uxCouponCondition.CustomerNameOnceOnlyText );
        coupon.MinimumSubtotal = ConvertUtilities.ToDecimal( uxMinimumSubtotalText.Text );
        coupon.ProductCostType = GetProductCostType();
        coupon.FreeShippingType = GetFreeShippingType();
        return coupon;
    }

    private void CreateCoupon()
    {
        try
        {
            if (Page.IsValid)
            {
                if (IsExistGiftCertificate())
                {
                    uxMessage.DisplayError( Resources.CouponMessages.AddDuplicatedGiftError );
                    return;
                }

                string errorMessage;
                if (!uxCouponCondition.Verify( out errorMessage ))
                {
                    uxMessage.DisplayError( errorMessage );
                    return;
                }

                if (!IsProductIDExist())
                {
                    uxMessage.DisplayError( Resources.CouponMessages.InvalidProductID );
                }
                else if (!IsCategoryIDExist())
                {
                    uxMessage.DisplayError( Resources.CouponMessages.InvalidCategoryID );
                }
                else if (!IsCustomerExist() || !IsCustomerOnceOnlyExist())
                {
                    uxMessage.DisplayError( Resources.CouponMessages.InvalidUserName );
                }
                else if (!IsExpiredDateAvailable())
                {
                    uxMessage.DisplayError( Resources.CouponMessages.InvalidExpirationDate );
                }
                else
                {
                    Coupon coupon = new Coupon();
                    coupon = SetupCoupon( coupon );
                    DataAccessContext.CouponRepository.Create( coupon );

                    uxMessage.DisplayMessage( Resources.CouponMessages.AddSuccess + "&nbsp;&quot;Coupon ID : " + _couponID + "&quot;" );
                    ClearInputFields();
                }
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    private bool IsExistGiftCertificate()
    {
        GiftCertificate giftCertificate = DataAccessContext.GiftCertificateRepository.GetOne( uxCouponIDText.Text.Trim() );
        if (giftCertificate.IsNull)
            return false;
        else
            return true;
    }

    private bool IsExpiredDateAvailable()
    {
        if (uxExpirationTypeDrop.SelectedValue.ToString() == "Quantity")
        {
            return true;
        }
        else
        {
            if (uxRegisterDateCalendarPopup.SelectedDateText == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    private void UpdateCoupon()
    {
        try
        {
            if (Page.IsValid)
            {
                if (IsExistGiftCertificate())
                {
                    uxMessage.DisplayError( Resources.CouponMessages.AddDuplicatedGiftError );
                    return;
                }

                string errorMessage;
                if (!uxCouponCondition.Verify( out errorMessage ))
                {
                    uxMessage.DisplayError( errorMessage );
                    return;
                }

                if (!IsProductIDExist())
                {
                    uxMessage.DisplayError( Resources.CouponMessages.InvalidProductID );
                }
                else if (!IsCategoryIDExist())
                {
                    uxMessage.DisplayError( Resources.CouponMessages.InvalidCategoryID );
                }
                else if (!IsCustomerExist())
                {
                    uxMessage.DisplayError( Resources.CouponMessages.InvalidUserName );
                }
                else if (!IsExpiredDateAvailable())
                {
                    uxMessage.DisplayError( Resources.CouponMessages.InvalidExpirationDate );
                }
                else
                {
                    Coupon coupon = DataAccessContext.CouponRepository.GetOne( CurrentID );
                    coupon = SetupCoupon( coupon );
                    DataAccessContext.CouponRepository.Update( coupon, uxCouponIDText.Text.Trim() );

                    PopulateControls();

                    uxMessage.DisplayMessage( Resources.CouponMessages.UpdateSuccess );
                }
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    private void ShowControlsPercentage()
    {
        uxAmountTR.Visible = false;
        uxPercentageTR.Visible = true;
        uxBuyXGetYTR.Visible = false;
        uxRepeatDiscountTR.Visible = false;
        uxFreeShippingTR.Visible = false;
    }

    private void ShowControlsAmount()
    {
        uxAmountTR.Visible = true;
        uxPercentageTR.Visible = false;
        uxBuyXGetYTR.Visible = false;
        uxRepeatDiscountTR.Visible = false;
        uxFreeShippingTR.Visible = false;
    }

    private void ShowControlsBuyXGetYAmount()
    {
        uxAmountTR.Visible = true;
        uxPercentageTR.Visible = false;
        uxBuyXGetYTR.Visible = true;
        uxRepeatDiscountTR.Visible = true;
        uxFreeShippingTR.Visible = false;
    }

    private void ShowControlsBuyXGetYPercentage()
    {
        uxAmountTR.Visible = false;
        uxPercentageTR.Visible = true;
        uxBuyXGetYTR.Visible = true;
        uxRepeatDiscountTR.Visible = true;
        uxFreeShippingTR.Visible = false;
    }

    private void ShowControlsFreeShipping()
    {
        uxAmountTR.Visible = false;
        uxPercentageTR.Visible = false;
        uxBuyXGetYTR.Visible = false;
        uxRepeatDiscountTR.Visible = false;
        uxFreeShippingTR.Visible = true;
    }

    private void SetControlsExpiration()
    {
        switch (GetExpirationType())
        {
            case Coupon.ExpirationTypeEnum.Date:
                ShowControlsExpirationDate();
                break;
            case Coupon.ExpirationTypeEnum.Quantity:
                ShowControlsExpirationQuantity();
                break;
            case Coupon.ExpirationTypeEnum.Both:
                ShowControlExpirationBoth();
                break;
        }
    }

    private void SetControlsDiscount()
    {
        switch (GetDiscountType())
        {
            case Coupon.DiscountTypeEnum.Price:
                ShowControlsAmount();
                break;
            case Coupon.DiscountTypeEnum.Percentage:
                ShowControlsPercentage();
                break;
            case Coupon.DiscountTypeEnum.BuyXDiscountYPercentage:
                ShowControlsBuyXGetYPercentage();
                break;
            case Coupon.DiscountTypeEnum.BuyXDiscountYPrice:
                ShowControlsBuyXGetYAmount();
                break;
            case Coupon.DiscountTypeEnum.FreeShipping:
                ShowControlsFreeShipping();
                break;
        }
    }

    private void ShowControlsExpirationDate()
    {
        uxExpirationDateTR.Visible = true;
        uxExpirationQuantityTR.Visible = false;
    }

    private void ShowControlsExpirationQuantity()
    {
        uxExpirationDateTR.Visible = false;
        uxExpirationQuantityTR.Visible = true;
    }

    private void ShowControlExpirationBoth()
    {
        uxExpirationDateTR.Visible = true;
        uxExpirationQuantityTR.Visible = true;
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (IsEditMode())
        {
            if (!MainContext.IsPostBack)
            {
                PopulateControls();
                uxGenerateIDButton.Visible = false;
                uxCouponIDText.Enabled = false;
            }

            if (IsAdminModifiable())
            {
                uxUpdateButton.Visible = true;
            }
            else
            {
                uxUpdateButton.Visible = false;
                uxGenerateIDButton.Visible = false;
            }

            uxAddButton.Visible = false;
        }
        else
        {
            if (IsAdminModifiable())
            {
                uxAddButton.Visible = true;
                uxUpdateButton.Visible = false;
                if (!MainContext.IsPostBack)
                {
                    ShowControlsAmount();
                    ShowControlsExpirationDate();
                    uxRegisterDateCalendarPopup.SelectedDate = DateTime.Now;
                }
            }
            else
            {
                MainContext.RedirectMainControl( "CouponList.ascx" );
            }
        }

    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        CreateCoupon();
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        UpdateCoupon();
    }

    protected void uxDiscountTypeDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        SetControlsDiscount();
    }

    protected void uxExpirationTypeDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        SetControlsExpiration();
    }

    protected void uxGenerateIDButton_Click( object sender, EventArgs e )
    {
        string id = "";

        int valueOfA = Char.ConvertToUtf32( "A", 0 );
        for (int i = 0; i < DefaultCouponLength; i++)
        {
            int random = RandomUtilities.RandomNumber( 35 );
            if (random < 10)
            {
                id += random.ToString();
            }
            else
            {
                id += Char.ConvertFromUtf32( valueOfA + random - 10 );
            }
        }

        uxCouponIDText.Text = id;
    }

    #endregion


    #region Public Methods

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }

    #endregion

}
