using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Deluxe.Domain.EBay;
using Vevo.eBay;
using Vevo.Shared.Utilities;
using Vevo.Deluxe.Domain;

public partial class Admin_Components_EBayTemplateDetails : AdminAdvancedBaseUserControl
{
    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string EBayTemplateID
    {
        get
        {
            return MainContext.QueryString["EBayTemplateID"];
        }
    }

    private void uxState_RefreshHandler( object sender, EventArgs e )
    {
        uxStateList.CountryCode = uxCountryList.CurrentSelected;
        uxStateList.Refresh();
    }

    private void uxEBayDomesticShippingMethod2_RefreshHandler( object sender, EventArgs e )
    {
        if (uxEBayDomesticShippingMethod2.SelectedValue != "None")
        {
            if (uxEBayDomesticShippingTypeDrop.SelectedValue == "Flat")
            {
                uxEBayDomesticShippingCostItemPanel2.Visible = true;
            }
            else
            {
                uxEBayDomesticShippingCostItemPanel2.Visible = false;
            }
        }
        else
        {
            uxEBayDomesticShippingCostItemPanel2.Visible = false;
        }
    }

    private void uxEBayDomesticShippingMethod3_RefreshHandler( object sender, EventArgs e )
    {
        if (uxEBayDomesticShippingMethod3.SelectedValue != "None")
        {
            if (uxEBayDomesticShippingTypeDrop.SelectedValue == "Flat")
            {
                uxEBayDomesticShippingCostItemPanel3.Visible = true;
            }
            else
            {
                uxEBayDomesticShippingCostItemPanel3.Visible = false;
            }
        }
        else
        {
            uxEBayDomesticShippingCostItemPanel3.Visible = false;
        }
    }

    private void uxEBayInternationalShippingMethod2_RefreshHandler( object sender, EventArgs e )
    {
        if (uxEBayInternationalShippingMethod2.SelectedValue != "None")
        {
            if (uxEBayInternationalShippingTypeDrop.SelectedValue == "Flat")
            {
                uxEBayInternationalShippingCostItemPanel2.Visible = true;
            }
            else
            {
                uxEBayInternationalShippingCostItemPanel2.Visible = false;
            }
        }
        else
        {
            uxEBayInternationalShippingCostItemPanel2.Visible = false;
        }
    }

    private void uxEBayInternationalShippingMethod3_RefreshHandler( object sender, EventArgs e )
    {
        if (uxEBayInternationalShippingMethod3.SelectedValue != "None")
        {
            if (uxEBayInternationalShippingTypeDrop.SelectedValue == "Flat")
            {
                uxEBayInternationalShippingCostItemPanel3.Visible = true;
            }
            else
            {
                uxEBayInternationalShippingCostItemPanel3.Visible = false;
            }
        }
        else
        {
            uxEBayInternationalShippingCostItemPanel3.Visible = false;
        }
    }

    private void PopulateControls()
    {
        if (EBayTemplateID != null && int.Parse( EBayTemplateID ) >= 0)
        {
            EBayTemplate eBayTemplate = DataAccessContextDeluxe.EBayTemplateRepository.GetOne( EBayTemplateID );

            uxEBayTemplateNameText.Text = eBayTemplate.EBayTemplateName;
            uxEBayTemplateListSiteDrop.SelectedValue = eBayTemplate.EBayListSite;
            if (uxEBayTemplateListSiteDrop.SelectedValue != "US")
            {
                uxEBayDomesticShippingTypeDrop.Items.Remove( "Calculate" );
                uxEBayInternationalShippingTypeDrop.Items.Remove( "Calculate" );
            }
            else
            {
                uxEBayDomesticShippingTypeDrop.Items.Add( "Calculate" );
                uxEBayInternationalShippingTypeDrop.Items.Add( "Calculate" );
            }
            uxEBayDomesticShippingMethod1.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, false );
            uxEBayDomesticShippingMethod2.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, true );
            uxEBayDomesticShippingMethod3.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, true );
            uxEBayInternationalShippingMethod1.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, false );
            uxEBayInternationalShippingMethod2.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, true );
            uxEBayInternationalShippingMethod3.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, true );
            uxIsPrivateListCheck.Checked = eBayTemplate.IsPrivateListing;
            uxPrimaryEBayCategoryIDHidden.Value = eBayTemplate.PrimaryeBayCategoryID;
            uxPrimaryEBayCategoryNameLabel.Text = eBayTemplate.PrimaryeBayCategoryBreadCrumb; ;
            EBayAccess eBayAccess = new EBayAccess();
            EBayCategory eBayCategory = eBayAccess.GetCategoriesDetailsByID( uxPrimaryEBayCategoryIDHidden.Value, eBayTemplate.EBayListSite );
            EBayCategoryFeature eBayCategoryFeature = eBayAccess.GetCategoryFeatureDetail( eBayCategory.PimaryCategoryID, eBayCategory.CategoryLevel, eBayTemplate.EBayListSite );
            if (eBayCategoryFeature.IsProductConditionEnabled)
            {
                uxProductConditionDev.Visible = true;
                PopulateProductConditionDrop();
                uxProductConditionDrop.SelectedValue = eBayTemplate.ProductConditionID;
            }
            uxSecondaryEBayCategoryIDHidden.Value = eBayTemplate.SecondaryeBayCategoryID;
            uxSecondaryEBayCategoryNameLabel.Text = eBayTemplate.SecondaryeBayCategoryBreadCrumb;
            //productcondition
            uxQuantityText.Text = eBayTemplate.Quantity.ToString();
            uxIsDisplayImageCheck.Checked = eBayTemplate.IsShowImage;
            uxCountryList.CurrentSelected = eBayTemplate.ProductCountry;
            uxStateList.CountryCode = uxCountryList.CurrentSelected;
            uxStateList.Refresh();
            uxStateList.CurrentSelected = eBayTemplate.ProductState;
            uxEBayZipText.Text = eBayTemplate.ProductZip;
            uxEBaySellingMethodRadioList.SelectedValue = eBayTemplate.SellingMethod;

            if (uxEBaySellingMethodRadioList.SelectedValue == "Online Auction")
            {
                uxEBayIsUseReservePriceCheck.Checked = eBayTemplate.UseReservePrice;
                if (uxEBayIsUseReservePriceCheck.Checked)
                {
                    uxEBayReservePriceSettingPanel.Visible = true;
                    uxEBayReservePriceTypeRadioList.SelectedValue = eBayTemplate.ReservePriceType;
                    if (uxEBayReservePriceTypeRadioList.SelectedValue != "ProductPrice")
                    {
                        uxReservePriceValueDiv.Visible = true;
                        uxEBayReservePriceValueText.Text = eBayTemplate.ReservePriceValue.ToString();
                    }
                }
                uxEBayStartingPriceTypeRadioList.SelectedValue = eBayTemplate.StartingPriceType;
                if (uxEBayStartingPriceTypeRadioList.SelectedValue != "ProductPrice")
                {
                    uxStartingPriceValueDiv.Visible = true;
                    uxEBayStartingPriceValueText.Text = eBayTemplate.StartingPriceValue.ToString();
                }
                uxEBayIsUseBuyItNowPriceCheck.Checked = eBayTemplate.UseBuyItNowPrice;
                if (uxEBayIsUseBuyItNowPriceCheck.Checked)
                {
                    uxEBayBuyItNowPriceSettingPanel.Visible = true;
                    uxEBayBuyItNowPriceTypeRadioList.SelectedValue = eBayTemplate.BuyItNowPriceType;
                    if (uxEBayBuyItNowPriceTypeRadioList.SelectedValue != "ProductPrice")
                    {
                        uxEBayBuyItNowPriceValueDiv.Visible = true;
                        uxEBayBuyItNowPriceValueText.Text = eBayTemplate.BuyItNowPriceValue.ToString();
                    }
                }
                uxQuantityText.Enabled = false;
                uxQuantityText.Text = "1";
            }
            else
            {
                uxEBaySellingMethodSetting.Text = "Fixed Price Setting";
                uxEBayReservePricePanel.Visible = false;
                uxEBayStartingPricePanel.Visible = false;
                uxEBayIsUseBuyItNowPriceCheck.Checked = true;
                uxEBayIsUseBuyItNowPriceCheck.Visible = false;
                uxEBayIsUseBuyItNowPriceLabel.Visible = false;
                uxEBayBuyItNowPriceSettingPanel.Visible = true;
                uxEBayBuyItNowPriceTypeRadioList.SelectedValue = eBayTemplate.BuyItNowPriceType;
                if (uxEBayBuyItNowPriceTypeRadioList.SelectedValue != "ProductPrice")
                {
                    uxEBayBuyItNowPriceValueDiv.Visible = true;
                    uxEBayBuyItNowPriceValueText.Text = eBayTemplate.BuyItNowPriceValue.ToString();
                }

                uxQuantityText.Enabled = true;

            }
            uxEBayListingDurationDrop.SelectedValue = eBayTemplate.ListingDuration;

            string[] payments = eBayTemplate.PaymentMethod.Split( ',' );
            uxEBayPaymentMethodCheckList.ClearSelection();
            for (int i = 0; i < uxEBayPaymentMethodCheckList.Items.Count; i++)
            {
                foreach (string payment in payments)
                {
                    if (payment == uxEBayPaymentMethodCheckList.Items[i].Value)
                    {
                        uxEBayPaymentMethodCheckList.Items[i].Selected = true;
                        break;
                    }
                }
            }

            uxEBayPayPalEmailAddressText.Text = eBayTemplate.PayPalEmailAddress;

            uxEBayDomesticShippingMethod1.SelectedValue = eBayTemplate.DomesticShippingMethod1;
            uxEBayDomesticShippingMethod2.SelectedValue = eBayTemplate.DomesticShippingMethod2;
            uxEBayDomesticShippingMethod3.SelectedValue = eBayTemplate.DomesticShippingMethod3;

            uxEBayDomesticShippingTypeDrop.SelectedValue = eBayTemplate.DomesticShippingType;
            if (uxEBayDomesticShippingTypeDrop.SelectedValue == "Flat")
            {
                uxEBayDomesticShippingFirstItemText1.Text = eBayTemplate.DomesticShippingFirstItem1.ToString();
                uxEBayDomesticShippingNextItemText1.Text = eBayTemplate.DomesticShippingNextItem1.ToString();
                uxEBayDomesticShippingFirstItemText2.Text = eBayTemplate.DomesticShippingFirstItem2.ToString();
                uxEBayDomesticShippingNextItemText2.Text = eBayTemplate.DomesticShippingNextItem2.ToString();
                uxEBayDomesticShippingFirstItemText3.Text = eBayTemplate.DomesticShippingFirstItem3.ToString();
                uxEBayDomesticShippingNextItemText3.Text = eBayTemplate.DomesticShippingNextItem3.ToString();
                uxEBayDomesticPackageTypeDiv.Visible = false;
                uxEBayDomesticHandlingCostDiv.Visible = false;

                if (uxEBayDomesticShippingMethod2.SelectedValue != "None")
                {
                    uxEBayDomesticShippingCostItemPanel2.Visible = true;
                }
                else
                {
                    uxEBayDomesticShippingCostItemPanel2.Visible = false;
                }

                if (uxEBayDomesticShippingMethod3.SelectedValue != "None")
                {
                    uxEBayDomesticShippingCostItemPanel3.Visible = true;
                }
                else
                {
                    uxEBayDomesticShippingCostItemPanel3.Visible = false;
                }
            }
            else
            {
                uxEBayDomesticShippingCostItemPanel1.Visible = false;
                uxEBayDomesticShippingCostItemPanel2.Visible = false;
                uxEBayDomesticShippingCostItemPanel3.Visible = false;
                uxEBayDomesticPackageTypeDiv.Visible = true;
                uxEBayDomesticHandlingCostDiv.Visible = true;
                uxEBayDomesticHandlingCostText.Text = eBayTemplate.DomesticShippingHandlingCost.ToString();
                uxEBayDomesticPackageTypeDrop.SelectedValue = eBayTemplate.DomesticShippingPackage;
            }

            uxEBayDomesticIsGetItFastCheck.Checked = eBayTemplate.DomesticShippingShowGetItFast;
            uxIsFreeDomesticShippingCheck.Checked = eBayTemplate.IsFreeDomesticShippingCost;
            uxEBayIsInterantionalShippingEnableCheck.Checked = eBayTemplate.IsInternationalShippingEnabled;
            if (uxEBayIsInterantionalShippingEnableCheck.Checked)
            {
                uxInternationalShippingDetailsPanel.Visible = true;

                uxEBayInternationalShippingTypeDrop.SelectedValue = eBayTemplate.InternationalShippingType;

                uxEBayInternationalShipTo1.SelectedValue = eBayTemplate.InternationalShippingShipTo1;
                uxEBayInternationalShipTo2.SelectedValue = eBayTemplate.InternationalShippingShipTo2;
                uxEBayInternationalShipTo3.SelectedValue = eBayTemplate.InternationalShippingShipTo3;
                uxEBayInternationalShippingMethod1.SelectedValue = eBayTemplate.InternationalShippingMethod1;
                uxEBayInternationalShippingMethod2.SelectedValue = eBayTemplate.InternationalShippingMethod2;
                uxEBayInternationalShippingMethod3.SelectedValue = eBayTemplate.InternationalShippingMethod3;

                if (uxEBayInternationalShippingTypeDrop.SelectedValue == "Flat")
                {
                    uxEBayInternationalShippingFirstItemText1.Text = eBayTemplate.InternationalShippingFirstItem1.ToString();
                    uxEBayInternationalShippingNextItemText1.Text = eBayTemplate.InternationalShippingNextItem1.ToString();
                    uxEBayInternationalShippingFirstItemText2.Text = eBayTemplate.InternationalShippingFirstItem2.ToString();
                    uxEBayInternationalShippingNextItemText2.Text = eBayTemplate.InternationalShippingNextItem2.ToString();
                    uxEBayInternationalShippingFirstItemText3.Text = eBayTemplate.InternationalShippingFirstItem3.ToString();
                    uxEBayInternationalShippingNextItemText3.Text = eBayTemplate.InternationalShippingNextItem3.ToString();
                    uxEBayInternationalPackageTypeDiv.Visible = false;
                    uxEBayInternationalHandlingCostDiv.Visible = false;

                    if (uxEBayInternationalShippingMethod2.SelectedValue != "None")
                    {
                        uxEBayInternationalShippingCostItemPanel2.Visible = true;
                    }
                    else
                    {
                        uxEBayInternationalShippingCostItemPanel2.Visible = false;
                    }

                    if (uxEBayInternationalShippingMethod3.SelectedValue != "None")
                    {
                        uxEBayInternationalShippingCostItemPanel3.Visible = true;
                    }
                    else
                    {
                        uxEBayInternationalShippingCostItemPanel3.Visible = false;
                    }

                }
                else
                {
                    uxEBayInternationalShippingCostItemPanel1.Visible = false;
                    uxEBayInternationalShippingCostItemPanel2.Visible = false;
                    uxEBayInternationalShippingCostItemPanel3.Visible = false;
                    uxEBayInternationalPackageTypeDiv.Visible = true;
                    uxEBayInternationalHandlingCostDiv.Visible = true;
                    uxEBayInternationalHandlingCostText.Text = eBayTemplate.InternationalShippingHandlingCost.ToString();
                    uxEBayInternationalPackageTypeDrop.SelectedValue = eBayTemplate.InternationalShippingPackage;
                }
            }
            uxEBayHandlingTimeDrop.SelectedValue = eBayTemplate.HandlingTime;
            uxEBaySalesTaxDrop.SelectedValue = eBayTemplate.SalesTaxType;
            if (uxEBaySalesTaxDrop.SelectedValue == "Charge")
            {
                uxEBaySalesTaxDiv.Visible = true;
                uxEBaySalesTaxStateList.CurrentSelected = eBayTemplate.SalesTaxState;
                uxEBaySalesTaxValueText.Text = eBayTemplate.SalesTaxValue.ToString();
                uxEBayIsTaxableShippingCheck.Checked = eBayTemplate.IsTaxableShippingCost;
            }
            uxEBayCheckoutIstructionText.Text = eBayTemplate.CheckoutInstruction;
            uxEBayIsAcceptReturnCheck.Checked = eBayTemplate.IsAcceptReturn;
            if (uxEBayIsAcceptReturnCheck.Checked)
            {
                uxAcceptReturnDetailsPanel.Visible = true;
                uxEBayReturnOfferDrop.SelectedValue = eBayTemplate.ReturnOffered;
                uxEBayReturnPeriodDrop.SelectedValue = eBayTemplate.ReturnPeriod;
                uxEBayReturnShippingPaidDrop.SelectedValue = eBayTemplate.ReturnShippingPaidBy;
                uxEBayAddionalPolicyText.Text = eBayTemplate.ReturnPolicyAdditionInfo;
            }
            uxEBayCounterStyleRadioList.SelectedValue = eBayTemplate.CounterStyle;
            uxGalleryOptionRadioList.SelectedValue = eBayTemplate.GalleryType;
            if (uxGalleryOptionRadioList.SelectedValue == "FeaturedGallery")
            {
                uxGalleryDurationDiv.Visible = true;
                uxGalleryDurationDrop.SelectedValue = eBayTemplate.GalleryDuration;
            }
            if (eBayTemplate.IsBoldListingTitle)
                uxEBayListringFeatureCheckList.Items[0].Selected = true;
            else
                uxEBayListringFeatureCheckList.Items[0].Selected = false;
            uxLotSizeText.Text = eBayTemplate.LotSize;
        }
    }

    private void PopulateProductConditionDrop()
    {
        uxProductConditionDrop.Items.Clear();
        EBayAccess eBayAccess = new EBayAccess();
        EBayCategory eBayCategory = eBayAccess.GetCategoriesDetailsByID( uxPrimaryEBayCategoryIDHidden.Value, uxEBayTemplateListSiteDrop.SelectedValue );
        EBayCategoryFeature eBayCategoryFeature = eBayAccess.GetCategoryFeatureDetail( eBayCategory.PimaryCategoryID, eBayCategory.CategoryLevel, uxEBayTemplateListSiteDrop.SelectedValue );
        if (eBayCategoryFeature.IsProductConditionEnabled)
        {
            IList<EBayProductCondition> eBayProductConditionList = eBayCategoryFeature.ProductCondition;
            ListItem item = new ListItem();
            foreach (EBayProductCondition eBayProductCondition in eBayProductConditionList)
            {
                item = new ListItem();
                item.Text = eBayProductCondition.ConditionName;
                item.Value = eBayProductCondition.ConditionID;
                uxProductConditionDrop.Items.Add( item );
            }

        }
        else
        {
            uxProductConditionDev.Visible = false;
        }
    }

    private void ClearInputFields()
    {
        uxEBayTemplateNameText.Text = "";
        uxEBayTemplateListSiteDrop.SelectedIndex = 0;
        if (uxEBayDomesticShippingTypeDrop.Items.Count < 2)
        {
            uxEBayDomesticShippingTypeDrop.Items.Add( "Calculate" );
        }

        if (uxEBayInternationalShippingTypeDrop.Items.Count < 2)
        {
            uxEBayInternationalShippingTypeDrop.Items.Add( "Calculate" );
        }

        uxIsPrivateListCheck.Checked = false;
        uxPrimaryEBayCategoryIDHidden.Value = "";
        uxSecondaryEBayCategoryIDHidden.Value = "";
        uxPrimaryEBayCategoryNameLabel.Text = "";
        uxSecondaryEBayCategoryNameLabel.Text = "";
        uxProductConditionDrop.Items.Clear();
        uxProductConditionDev.Visible = false;
        uxQuantityText.Text = "1";
        uxQuantityText.Enabled = false;
        uxIsDisplayImageCheck.Checked = true;
        uxStateList.CurrentSelected = "";
        uxCountryList.CurrentSelected = "";
        uxEBayZipText.Text = "";
        uxEBaySellingMethodRadioList.SelectedValue = "Online Auction";
        uxEBayIsUseReservePriceCheck.Checked = false;
        uxEBayReservePriceTypeRadioList.SelectedIndex = 0;
        uxEBayReservePriceValueText.Text = "";
        uxEBayStartingPriceTypeRadioList.SelectedIndex = 0;
        uxEBayStartingPriceValueText.Text = "";
        uxEBayIsUseBuyItNowPriceCheck.Checked = false;
        uxEBayBuyItNowPriceTypeRadioList.SelectedIndex = 0;
        uxEBayBuyItNowPriceValueText.Text = "";
        uxEBayListingDurationDrop.SelectedValue = "7";
        uxEBayPaymentMethodCheckList.ClearSelection();
        uxEBayPaymentMethodCheckList.Items[5].Selected = true;
        uxEBayPayPalEmailAddressText.Text = "";

        uxEBayDomesticPackageTypeDrop.SelectedIndex = 0;
        uxEBayDomesticShippingTypeDrop.SelectedIndex = 0;
        uxEBayDomesticShippingFirstItemText1.Text = "";
        uxEBayDomesticShippingFirstItemText2.Text = "";
        uxEBayDomesticShippingFirstItemText3.Text = "";
        uxEBayDomesticShippingNextItemText1.Text = "";
        uxEBayDomesticShippingNextItemText2.Text = "";
        uxEBayDomesticShippingNextItemText3.Text = "";
        uxEBayDomesticShippingCostItemPanel1.Visible = true;
        uxEBayDomesticShippingCostItemPanel2.Visible = false;
        uxEBayDomesticShippingCostItemPanel3.Visible = false;
        uxEBayDomesticPackageTypeDiv.Visible = false;
        uxEBayDomesticHandlingCostDiv.Visible = false;
        uxEBayDomesticShippingMethod1.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, false );
        uxEBayDomesticShippingMethod2.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, true );
        uxEBayDomesticShippingMethod3.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, true );
        uxEBayDomesticIsGetItFastCheck.Checked = false;
        uxIsFreeDomesticShippingCheck.Checked = false;
        uxEBayDomesticHandlingCostText.Text = "";
        uxEBayIsInterantionalShippingEnableCheck.Checked = false;
        uxEBayInternationalShippingTypeDrop.SelectedIndex = 0;
        uxEBayInternationalShippingFirstItemText1.Text = "";
        uxEBayInternationalShippingFirstItemText2.Text = "";
        uxEBayInternationalShippingFirstItemText3.Text = "";
        uxEBayInternationalShippingNextItemText1.Text = "";
        uxEBayInternationalShippingNextItemText2.Text = "";
        uxEBayInternationalShippingNextItemText3.Text = "";
        uxEBayInternationalShippingCostItemPanel1.Visible = true;
        uxEBayInternationalShippingCostItemPanel2.Visible = false;
        uxEBayInternationalShippingCostItemPanel3.Visible = false;
        uxEBayInternationalPackageTypeDiv.Visible = false;
        uxEBayInternationalHandlingCostDiv.Visible = false;
        uxEBayInternationalPackageTypeDrop.SelectedIndex = 0;
        uxEBayInternationalShipTo1.PopulateControls( false );
        uxEBayInternationalShipTo2.PopulateControls( true );
        uxEBayInternationalShipTo3.PopulateControls( true );
        uxEBayInternationalShippingMethod1.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, false );
        uxEBayInternationalShippingMethod2.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, true );
        uxEBayInternationalShippingMethod3.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, true );
        uxEBayInternationalHandlingCostText.Text = "";
        uxEBayHandlingTimeDrop.SelectedIndex = 0;
        uxEBaySalesTaxDrop.SelectedIndex = 0;
        uxEBaySalesTaxDiv.Visible = false;
        uxEBaySalesTaxStateList.CurrentSelected = "";
        uxEBaySalesTaxValueText.Text = "0";
        uxEBayIsTaxableShippingCheck.Checked = false;
        uxEBayCheckoutIstructionText.Text = "";
        uxEBayIsAcceptReturnCheck.Checked = false;
        uxEBayReturnOfferDrop.SelectedIndex = 0;
        uxEBayReturnPeriodDrop.SelectedIndex = 0;
        uxEBayReturnShippingPaidDrop.SelectedIndex = 0;
        uxEBayAddionalPolicyText.Text = "";
        uxEBayCounterStyleRadioList.SelectedIndex = 2;
        uxGalleryOptionRadioList.SelectedIndex = 0;
        uxGalleryDurationDrop.SelectedIndex = 0;
        uxEBayListringFeatureCheckList.Items[0].Selected = false;
        uxLotSizeText.Text = "";
    }

    private void Update()
    {
        try
        {
            if (Page.IsValid && IsEBayPrimaryCategoryIsValid())
            {
                EBayTemplate eBayTemplate = DataAccessContextDeluxe.EBayTemplateRepository.GetOne( EBayTemplateID );
                eBayTemplate = SetUpTemplate( eBayTemplate );
                eBayTemplate = DataAccessContextDeluxe.EBayTemplateRepository.Save( eBayTemplate );

                PopulateControls();
                uxMessage.DisplayMessage( "Update Complete" );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    private EBayTemplate SetUpTemplate( EBayTemplate eBayTemplate )
    {
        eBayTemplate.EBayTemplateName = uxEBayTemplateNameText.Text;
        eBayTemplate.EBayListSite = uxEBayTemplateListSiteDrop.SelectedValue;
        eBayTemplate.EBayTemplateDate = DateTime.Now;
        eBayTemplate.IsPrivateListing = uxIsPrivateListCheck.Checked;
        eBayTemplate.PrimaryeBayCategoryID = uxPrimaryEBayCategoryIDHidden.Value;
        eBayTemplate.PrimaryeBayCategoryBreadCrumb = uxPrimaryEBayCategoryNameLabel.Text;
        if (uxProductConditionDev.Visible)
        {
            eBayTemplate.ProductConditionID = uxProductConditionDrop.SelectedValue;
            eBayTemplate.ProductConditionName = uxProductConditionDrop.SelectedItem.Text;
        }
        eBayTemplate.SecondaryeBayCategoryID = uxSecondaryEBayCategoryIDHidden.Value;
        eBayTemplate.SecondaryeBayCategoryBreadCrumb = uxSecondaryEBayCategoryNameLabel.Text;
        eBayTemplate.IsShowImage = uxIsDisplayImageCheck.Checked;
        eBayTemplate.ProductCountry = uxCountryList.CurrentSelected;
        eBayTemplate.ProductState = uxStateList.CurrentSelected;
        eBayTemplate.ProductZip = uxEBayZipText.Text;
        eBayTemplate.SellingMethod = uxEBaySellingMethodRadioList.SelectedValue;
        if (uxEBaySellingMethodRadioList.SelectedValue == "Online Auction")
        {
            bool isUseReservePrice = uxEBayIsUseReservePriceCheck.Checked;
            bool isUseBuyItNowPrice = uxEBayIsUseBuyItNowPriceCheck.Checked;

            eBayTemplate.UseReservePrice = isUseReservePrice;
            if (isUseReservePrice)
            {
                eBayTemplate.ReservePriceType = uxEBayReservePriceTypeRadioList.SelectedValue;
                eBayTemplate.ReservePriceValue = ConvertUtilities.ToDouble( uxEBayReservePriceValueText.Text );
            }

            eBayTemplate.StartingPriceType = uxEBayStartingPriceTypeRadioList.SelectedValue;
            eBayTemplate.StartingPriceValue = ConvertUtilities.ToDouble( uxEBayStartingPriceValueText.Text );

            eBayTemplate.UseBuyItNowPrice = isUseBuyItNowPrice;
            if (isUseBuyItNowPrice)
            {
                eBayTemplate.BuyItNowPriceType = uxEBayBuyItNowPriceTypeRadioList.SelectedValue;
                eBayTemplate.BuyItNowPriceValue = ConvertUtilities.ToDouble( uxEBayBuyItNowPriceValueText.Text );
            }

            uxQuantityText.Enabled = false;
            uxQuantityText.Text = "1";
        }
        else
        {
            eBayTemplate.UseReservePrice = false;
            eBayTemplate.UseBuyItNowPrice = true;
            eBayTemplate.BuyItNowPriceType = uxEBayBuyItNowPriceTypeRadioList.SelectedValue;
            eBayTemplate.BuyItNowPriceValue = ConvertUtilities.ToDouble( uxEBayBuyItNowPriceValueText.Text );
            uxQuantityText.Enabled = true;
        }
        eBayTemplate.Quantity = ConvertUtilities.ToInt32( uxQuantityText.Text );
        eBayTemplate.ListingDuration = uxEBayListingDurationDrop.SelectedValue;

        string payment = String.Empty;
        for (int i = 0; i < uxEBayPaymentMethodCheckList.Items.Count; i++)
        {
            if (uxEBayPaymentMethodCheckList.Items[i].Selected)
            {
                if (String.IsNullOrEmpty( payment ))
                {
                    payment += uxEBayPaymentMethodCheckList.Items[i].Value;
                }
                else
                {
                    payment += "," + uxEBayPaymentMethodCheckList.Items[i].Value;
                }
            }
        }

        eBayTemplate.PaymentMethod = payment;
        eBayTemplate.PayPalEmailAddress = uxEBayPayPalEmailAddressText.Text;
        eBayTemplate.DomesticShippingType = uxEBayDomesticShippingTypeDrop.SelectedValue;

        if (uxEBayDomesticShippingTypeDrop.SelectedValue == "Flat")
        {
            eBayTemplate.DomesticShippingFirstItem1 = ConvertUtilities.ToDouble( uxEBayDomesticShippingFirstItemText1.Text );
            eBayTemplate.DomesticShippingNextItem1 = ConvertUtilities.ToDouble( uxEBayDomesticShippingNextItemText1.Text );
            if (uxEBayDomesticShippingCostItemPanel2.Visible)
            {
                eBayTemplate.DomesticShippingFirstItem2 = ConvertUtilities.ToDouble( uxEBayDomesticShippingFirstItemText2.Text );
                eBayTemplate.DomesticShippingNextItem2 = ConvertUtilities.ToDouble( uxEBayDomesticShippingNextItemText2.Text );
            }
            if (uxEBayDomesticShippingCostItemPanel3.Visible)
            {
                eBayTemplate.DomesticShippingFirstItem3 = ConvertUtilities.ToDouble( uxEBayDomesticShippingFirstItemText3.Text );
                eBayTemplate.DomesticShippingNextItem3 = ConvertUtilities.ToDouble( uxEBayDomesticShippingNextItemText3.Text );
            }
        }
        else
        {
            eBayTemplate.DomesticShippingPackage = uxEBayDomesticPackageTypeDrop.SelectedValue;
            eBayTemplate.DomesticShippingHandlingCost = ConvertUtilities.ToDouble( uxEBayDomesticHandlingCostText.Text );
        }

        eBayTemplate.DomesticShippingMethod1 = uxEBayDomesticShippingMethod1.SelectedValue;
        eBayTemplate.DomesticShippingMethod2 = uxEBayDomesticShippingMethod2.SelectedValue;
        eBayTemplate.DomesticShippingMethod3 = uxEBayDomesticShippingMethod3.SelectedValue;
        eBayTemplate.DomesticShippingShowGetItFast = uxEBayDomesticIsGetItFastCheck.Checked;
        eBayTemplate.IsFreeDomesticShippingCost = uxIsFreeDomesticShippingCheck.Checked;
        eBayTemplate.IsInternationalShippingEnabled = uxEBayIsInterantionalShippingEnableCheck.Checked;
        if (uxEBayIsInterantionalShippingEnableCheck.Checked)
        {

            eBayTemplate.InternationalShippingType = uxEBayInternationalShippingTypeDrop.SelectedValue;
            if (uxEBayInternationalShippingTypeDrop.SelectedValue == "Flat")
            {
                eBayTemplate.InternationalShippingFirstItem1 = ConvertUtilities.ToDouble( uxEBayInternationalShippingFirstItemText1.Text );
                eBayTemplate.InternationalShippingNextItem1 = ConvertUtilities.ToDouble( uxEBayInternationalShippingNextItemText1.Text );
                if (uxEBayInternationalShippingCostItemPanel2.Visible)
                {
                    eBayTemplate.InternationalShippingFirstItem2 = ConvertUtilities.ToDouble( uxEBayInternationalShippingFirstItemText2.Text );
                    eBayTemplate.InternationalShippingNextItem2 = ConvertUtilities.ToDouble( uxEBayInternationalShippingNextItemText2.Text );
                }
                if (uxEBayInternationalShippingCostItemPanel3.Visible)
                {
                    eBayTemplate.InternationalShippingFirstItem3 = ConvertUtilities.ToDouble( uxEBayInternationalShippingFirstItemText3.Text );
                    eBayTemplate.InternationalShippingNextItem3 = ConvertUtilities.ToDouble( uxEBayInternationalShippingNextItemText3.Text );
                }
            }
            else
            {
                eBayTemplate.InternationalShippingPackage = uxEBayInternationalPackageTypeDrop.SelectedValue;
                eBayTemplate.InternationalShippingHandlingCost = ConvertUtilities.ToDouble( uxEBayInternationalHandlingCostText.Text );
            }
            eBayTemplate.InternationalShippingShipTo1 = uxEBayInternationalShipTo1.SelectedValue;
            eBayTemplate.InternationalShippingShipTo2 = uxEBayInternationalShipTo2.SelectedValue;
            eBayTemplate.InternationalShippingShipTo3 = uxEBayInternationalShipTo3.SelectedValue;
            eBayTemplate.InternationalShippingMethod1 = uxEBayInternationalShippingMethod1.SelectedValue;
            eBayTemplate.InternationalShippingMethod2 = uxEBayInternationalShippingMethod2.SelectedValue;
            eBayTemplate.InternationalShippingMethod3 = uxEBayInternationalShippingMethod3.SelectedValue;

        }
        eBayTemplate.HandlingTime = uxEBayHandlingTimeDrop.SelectedValue;
        eBayTemplate.SalesTaxType = uxEBaySalesTaxDrop.SelectedValue;
        if (uxEBaySalesTaxDrop.SelectedValue == "Charge")
        {
            eBayTemplate.SalesTaxState = uxEBaySalesTaxStateList.CurrentSelected;
            eBayTemplate.SalesTaxValue = ConvertUtilities.ToDouble( uxEBaySalesTaxValueText.Text );
            eBayTemplate.IsTaxableShippingCost = uxEBayIsTaxableShippingCheck.Checked;
        }
        eBayTemplate.CheckoutInstruction = uxEBayCheckoutIstructionText.Text;
        eBayTemplate.IsAcceptReturn = uxEBayIsAcceptReturnCheck.Checked;
        eBayTemplate.ReturnOffered = uxEBayReturnOfferDrop.SelectedValue;
        eBayTemplate.ReturnPeriod = uxEBayReturnPeriodDrop.SelectedValue;
        eBayTemplate.ReturnShippingPaidBy = uxEBayReturnShippingPaidDrop.SelectedValue;
        eBayTemplate.ReturnPolicyAdditionInfo = uxEBayAddionalPolicyText.Text;
        eBayTemplate.CounterStyle = uxEBayCounterStyleRadioList.SelectedValue;
        eBayTemplate.GalleryType = uxGalleryOptionRadioList.SelectedValue;
        eBayTemplate.GalleryDuration = uxGalleryDurationDrop.SelectedValue;
        if (uxEBayListringFeatureCheckList.SelectedValue == "BoldTitle")
            eBayTemplate.IsBoldListingTitle = true;
        else
            eBayTemplate.IsBoldListingTitle = false;

        eBayTemplate.LotSize = uxLotSizeText.Text;


        return eBayTemplate;
    }

    protected void uxEBaySalesTaxDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxEBaySalesTaxDrop.SelectedValue == "Charge")
        {
            uxEBaySalesTaxDiv.Visible = true;
            uxEBaySalesTaxStateList.Refresh();
        }
        else
        {
            uxEBaySalesTaxDiv.Visible = false;
        }
    }

    protected void uxEBaySellingMethodRadioList_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxEBaySellingMethodRadioList.SelectedValue == "Online Auction")
        {
            uxEBaySellingMethodSetting.Text = "Online Auction Setting";
            uxEBayReservePricePanel.Visible = true;
            uxEBayStartingPricePanel.Visible = true;
            uxEBayIsUseBuyItNowPriceCheck.Checked = false;
            uxEBayIsUseBuyItNowPriceCheck.Visible = true;
            uxEBayIsUseBuyItNowPriceLabel.Visible = true;
            uxEBayBuyItNowPriceSettingPanel.Visible = false;
            uxQuantityText.Enabled = false;
            uxQuantityText.Text = "1";
        }
        else
        {
            uxEBaySellingMethodSetting.Text = "Fixed Price Setting";
            uxEBayReservePricePanel.Visible = false;
            uxEBayStartingPricePanel.Visible = false;
            uxEBayIsUseBuyItNowPriceCheck.Checked = true;
            uxEBayIsUseBuyItNowPriceCheck.Visible = false;
            uxEBayIsUseBuyItNowPriceLabel.Visible = false;
            uxEBayBuyItNowPriceSettingPanel.Visible = true;
            uxQuantityText.Enabled = true;
        }
    }

    protected void uxEBayDomesticShippingTypeDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxEBayDomesticShippingTypeDrop.SelectedValue == "Flat")
        {
            uxEBayDomesticShippingCostItemPanel1.Visible = true;
            uxEBayDomesticShippingCostItemPanel2.Visible = true;
            uxEBayDomesticShippingCostItemPanel3.Visible = true;
            uxEBayDomesticPackageTypeDiv.Visible = false;
            uxEBayDomesticHandlingCostDiv.Visible = false;
            if (uxEBayDomesticShippingMethod2.SelectedValue != "None")
            {
                uxEBayDomesticShippingCostItemPanel2.Visible = true;
            }
            else
            {
                uxEBayDomesticShippingCostItemPanel2.Visible = false;
            }

            if (uxEBayDomesticShippingMethod3.SelectedValue != "None")
            {
                uxEBayDomesticShippingCostItemPanel3.Visible = true;
            }
            else
            {
                uxEBayDomesticShippingCostItemPanel3.Visible = false;
            }
        }
        else
        {
            uxEBayDomesticShippingCostItemPanel1.Visible = false;
            uxEBayDomesticShippingCostItemPanel2.Visible = false;
            uxEBayDomesticShippingCostItemPanel3.Visible = false;
            uxEBayDomesticPackageTypeDiv.Visible = true;
            uxEBayDomesticHandlingCostDiv.Visible = true;
        }
    }

    protected void uxEBayInternationalShippingTypeDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxEBayInternationalShippingTypeDrop.SelectedValue == "Flat")
        {
            uxEBayInternationalShippingCostItemPanel1.Visible = true;
            uxEBayInternationalShippingCostItemPanel2.Visible = true;
            uxEBayInternationalShippingCostItemPanel3.Visible = true;
            uxEBayInternationalPackageTypeDiv.Visible = false;

            if (uxEBayInternationalShippingMethod2.SelectedValue != "None")
            {
                uxEBayInternationalShippingCostItemPanel2.Visible = true;
            }
            else
            {
                uxEBayInternationalShippingCostItemPanel2.Visible = false;
            }

            if (uxEBayInternationalShippingMethod3.SelectedValue != "None")
            {
                uxEBayInternationalShippingCostItemPanel3.Visible = true;
            }
            else
            {
                uxEBayInternationalShippingCostItemPanel3.Visible = false;
            }

            uxEBayInternationalHandlingCostDiv.Visible = false;
        }
        else
        {
            uxEBayInternationalShippingCostItemPanel1.Visible = false;
            uxEBayInternationalShippingCostItemPanel2.Visible = false;
            uxEBayInternationalShippingCostItemPanel3.Visible = false;
            uxEBayInternationalPackageTypeDiv.Visible = true;
            uxEBayInternationalHandlingCostDiv.Visible = true;
        }
    }

    protected void uxEBayReservePriceTypeRadioList_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxEBayReservePriceTypeRadioList.SelectedValue == "ProductPrice")
        {
            uxReservePriceValueDiv.Visible = false;
        }
        else
        {
            uxReservePriceValueDiv.Visible = true;
        }
    }

    protected void uxEBayStartingPriceTypeRadioList_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxEBayStartingPriceTypeRadioList.SelectedValue == "ProductPrice")
        {
            uxStartingPriceValueDiv.Visible = false;
        }
        else
        {
            uxStartingPriceValueDiv.Visible = true;
        }
    }

    protected void uxEBayBuyItNowPriceTypeRadioList_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxEBayBuyItNowPriceTypeRadioList.SelectedValue == "ProductPrice")
        {
            uxEBayBuyItNowPriceValueDiv.Visible = false;
        }
        else
        {
            uxEBayBuyItNowPriceValueDiv.Visible = true;
        }
    }

    protected void uxEBayIsUseReservePriceCheck_CheckedChanged( object sender, EventArgs e )
    {
        if (uxEBayIsUseReservePriceCheck.Checked)
            uxEBayReservePriceSettingPanel.Visible = true;
        else
            uxEBayReservePriceSettingPanel.Visible = false;
    }

    protected void uxEBayIsUseBuyItNowPriceCheck_CheckedChanged( object sender, EventArgs e )
    {
        if (uxEBayIsUseBuyItNowPriceCheck.Checked)
            uxEBayBuyItNowPriceSettingPanel.Visible = true;
        else
            uxEBayBuyItNowPriceSettingPanel.Visible = false;
    }

    protected void uxEBayIsInterantionalShippingEnableCheck_CheckedChanged( object sender, EventArgs e )
    {
        if (uxEBayIsInterantionalShippingEnableCheck.Checked)
        {
            uxInternationalShippingDetailsPanel.Visible = true;
        }
        else
        {
            uxInternationalShippingDetailsPanel.Visible = false;
        }
    }

    protected void uxEBayIsAcceptReturnCheck_CheckedChanged( object sender, EventArgs e )
    {
        if (uxEBayIsAcceptReturnCheck.Checked)
        {
            uxAcceptReturnDetailsPanel.Visible = true;
        }
        else
        {
            uxAcceptReturnDetailsPanel.Visible = false;
        }
    }

    protected void uxGalleryOptionRadioList_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxGalleryOptionRadioList.SelectedValue == "Featured")
            uxGalleryDurationDiv.Visible = true;
        else
            uxGalleryDurationDiv.Visible = false;
    }

    protected void uxChangedPrimaryCategoryLink_Click( object sender, EventArgs e )
    {
        try
        {
            uxPrimaryCategorySelect.ListSite = uxEBayTemplateListSiteDrop.SelectedValue;
            uxPrimaryCategorySelect.PopulateControl();
            uxPrimaryCategoryOkButton.Visible = true;
            uxPrimaryCategoryCancelButton.Visible = true;
            uxPrimaryCategoryMessage.Visible = false;
            uxProductConditionDev.Visible = false;
        }
        catch
        {
            uxPrimaryCategorySelect.PanelVisibility = false;
            uxMessage.DisplayError( "eBay API is not setting up properly." +
                "<br/>Please verify eBay setting in menu \"Setting > Configuration\"" );
        }
    }

    protected void uxChangedSecondaryCategoryLink_Click( object sender, EventArgs e )
    {
        try
        {
            uxSecondaryCategorySelect.ListSite = uxEBayTemplateListSiteDrop.SelectedValue;
            uxSecondaryCategorySelect.PopulateControl();
            uxSecondaryCategoryOkButton.Visible = true;
            uxSecondaryCategoryCancelButton.Visible = true;
            uxSecondaryCategoryMessage.Visible = false;
        }
        catch
        {
            uxSecondaryCategorySelect.PanelVisibility = false;
            uxMessage.DisplayError( "eBay API is not setting up properly." +
                "<br/>Please verify eBay setting in menu \"Setting > Configuration\"" );
        }
    }

    protected void uxRemoveSecondaryCategoryLink_Click( object sender, EventArgs e )
    {
        uxSecondaryEBayCategoryIDHidden.Value = "";
        uxSecondaryEBayCategoryNameLabel.Text = "";
        uxRemoveSecondaryCategoryLink.Visible = false;
    }

    protected void uxPrimaryCategoryCancelButton_Click( object sender, EventArgs e )
    {
        uxPrimaryCategorySelect.ClosePanel();
        uxPrimaryCategoryOkButton.Visible = false;
        uxPrimaryCategoryCancelButton.Visible = false;
        uxPrimaryCategoryMessage.Visible = false;
        if (!String.IsNullOrEmpty( uxPrimaryEBayCategoryNameLabel.Text ))
        {
            uxProductConditionDev.Visible = true;
        }
        else
        {
            uxProductConditionDev.Visible = false;
        }
    }

    protected void uxSecondaryCategoryCancelButton_Click( object sender, EventArgs e )
    {
        uxSecondaryCategorySelect.ClosePanel();
        uxSecondaryCategoryOkButton.Visible = false;
        uxSecondaryCategoryCancelButton.Visible = false;
        uxSecondaryCategoryMessage.Visible = false;
    }

    protected void uxSecondaryCategoryOkButton_Click( object sender, EventArgs e )
    {
        try
        {
            uxSecondaryEBayCategoryNameLabel.Text = uxSecondaryCategorySelect.GetSelectedCategoryList();
            uxSecondaryEBayCategoryIDHidden.Value = uxSecondaryCategorySelect.GetSelectedCategoryID();
            uxSecondaryCategorySelect.ClosePanel();
            uxSecondaryCategoryOkButton.Visible = false;
            uxSecondaryCategoryCancelButton.Visible = false;
            uxSecondaryCategoryMessage.Visible = false;

            if (uxSecondaryEBayCategoryIDHidden.Value.Equals( "0" ) || uxSecondaryEBayCategoryNameLabel.Text.Equals( "" ))
                uxRemoveSecondaryCategoryLink.Visible = false;
            else
                uxRemoveSecondaryCategoryLink.Visible = true;
        }
        catch
        {
            uxSecondaryCategoryMessage.Visible = true;
            uxSecondaryCategoryMessage.DisplayError( "This Category is not leaf category, cannot selected it" );
        }
    }

    protected void uxPrimaryCategoryOkButton_Click( object sender, EventArgs e )
    {
        try
        {
            uxPrimaryEBayCategoryNameLabel.Text = uxPrimaryCategorySelect.GetSelectedCategoryList();
            uxPrimaryEBayCategoryIDHidden.Value = uxPrimaryCategorySelect.GetSelectedCategoryID();
            uxPrimaryCategorySelect.ClosePanel();
            uxPrimaryCategoryOkButton.Visible = false;
            uxPrimaryCategoryCancelButton.Visible = false;
            uxPrimaryCategoryMessage.Visible = false;
            if (!String.IsNullOrEmpty( uxPrimaryEBayCategoryNameLabel.Text ))
            {
                uxProductConditionDev.Visible = true;
                PopulateProductConditionDrop();
            }
            else
            {
                uxProductConditionDev.Visible = false;
            }
        }
        catch
        {
            uxPrimaryCategoryMessage.Visible = true;
            uxPrimaryCategoryMessage.DisplayError( "This Category is not leaf category, cannot selected it" );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            uxEBayDomesticShippingMethod1.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, false );
            uxEBayDomesticShippingMethod2.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, true );
            uxEBayDomesticShippingMethod3.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, true );
            uxEBayInternationalShipTo1.PopulateControls( false );
            uxEBayInternationalShipTo2.PopulateControls( true );
            uxEBayInternationalShipTo3.PopulateControls( true );
            uxEBayInternationalShippingMethod1.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, false );
            uxEBayInternationalShippingMethod2.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, true );
            uxEBayInternationalShippingMethod3.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, true );
            uxEBayDomesticShippingCostItemPanel2.Visible = false;
            uxEBayDomesticShippingCostItemPanel3.Visible = false;
            uxEBayInternationalShippingCostItemPanel2.Visible = false;
            uxEBayInternationalShippingCostItemPanel3.Visible = false;
        }
        uxCountryList.BubbleEvent += new EventHandler( uxState_RefreshHandler );
        uxEBayDomesticShippingMethod2.BubbleEvent += new EventHandler( uxEBayDomesticShippingMethod2_RefreshHandler );
        uxEBayDomesticShippingMethod3.BubbleEvent += new EventHandler( uxEBayDomesticShippingMethod3_RefreshHandler );
        uxEBayInternationalShippingMethod2.BubbleEvent += new EventHandler( uxEBayInternationalShippingMethod2_RefreshHandler );
        uxEBayInternationalShippingMethod3.BubbleEvent += new EventHandler( uxEBayInternationalShippingMethod3_RefreshHandler );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            if (IsEditMode())
            {
                PopulateControls();

                if (IsAdminModifiable())
                {
                    uxAddButton.Visible = false;
                    uxUpdateButton.Visible = true;
                }
                else
                {
                    uxAddButton.Visible = false;
                    uxUpdateButton.Visible = false;
                }
            }
            else
            {
                if (IsAdminModifiable())
                {
                    uxAddButton.Visible = true;
                    uxUpdateButton.Visible = false;
                }
                else
                {
                    MainContext.RedirectMainControl( "EBayTemplateList.aspx" );
                }
            }

            if (uxSecondaryEBayCategoryIDHidden.Value.Equals( "0" ) || uxSecondaryEBayCategoryNameLabel.Text.Equals( "" ))
                uxRemoveSecondaryCategoryLink.Visible = false;
            else
                uxRemoveSecondaryCategoryLink.Visible = true;
        }
    }

    private bool IsEBayPrimaryCategoryIsValid()
    {
        if (uxPrimaryEBayCategoryIDHidden.Value != "0" && !String.IsNullOrEmpty( uxPrimaryEBayCategoryIDHidden.Value ))
        {
            uxMessage.Clear();
            return true;
        }
        else
        {
            uxMessage.DisplayError( "Please Select EBay Primary Category" );
            return false;
        }
    }



    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        try
        {
            //string templateID = DataAccessContextDeluxe.EBayTemplateRepository.get
            if (Page.IsValid && IsEBayPrimaryCategoryIsValid())
            {
                EBayTemplate eBayTemplate = new EBayTemplate();
                eBayTemplate = SetUpTemplate( eBayTemplate );
                eBayTemplate = DataAccessContextDeluxe.EBayTemplateRepository.Save( eBayTemplate );
                string newTemplateID = eBayTemplate.EBayTemplateID;

                uxMessage.DisplayMessage( "Add Success" );

                ClearInputFields();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        Update();
    }

    protected void uxEBayTemplateListSiteDrop_SelectedIndexchanged( object sender, EventArgs e )
    {
        uxEBayDomesticShippingMethod1.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, false );
        uxEBayDomesticShippingMethod2.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, true );
        uxEBayDomesticShippingMethod3.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, true );

        uxEBayInternationalShippingMethod1.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, false );
        uxEBayInternationalShippingMethod2.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, true );
        uxEBayInternationalShippingMethod3.PopulateControls( uxEBayTemplateListSiteDrop.SelectedValue, true );
        if (uxEBayTemplateListSiteDrop.SelectedValue != "US")
        {
            uxEBayDomesticShippingTypeDrop.Items.Remove( "Calculate" );
            uxEBayInternationalShippingTypeDrop.Items.Remove( "Calculate" );
        }
        else
        {
            uxEBayDomesticShippingTypeDrop.Items.Add( "Calculate" );
            uxEBayInternationalShippingTypeDrop.Items.Add( "Calculate" );
        }
    }

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }

}