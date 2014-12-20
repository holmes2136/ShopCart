using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using Vevo;
using Vevo.eBay;
using Vevo.Domain;
using Vevo.Deluxe.Domain.EBay;
using Vevo.WebUI;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;
using Vevo.Deluxe.Domain;

public partial class AdminAdvanced_Components_EBay_EBayCheckCondition : AdminAdvancedBaseUserControl
{
    private Product GetProductByProductID( string productID )
    {
        return DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, productID, StoreContext.CurrentStore.StoreID );
    }

    private void ShowMessageByCondition( EBayCategoryFeature feature, EBayCategory category )
    {
        /*
        if (feature.IsProductVariationEnabled)
        {
            lcIsProductVariationEnabled.Text = "Product with option can be listed in this template.";
        }
        else
        {
            lcIsProductVariationEnabled.Text = "Product with option can not be listed in this template.";
        }*/
        lcIsProductVariationEnabled.Text = "Product with option can not be listed in this template.";

        /*
        if (feature.IsPayPalRequired)
        {
            lcIsPayPalRequired.Text = "PayPal is required for payment gateway.";
        }
        else
        {
            lcIsPayPalRequired.Text = "PayPal is not required for payment gateway.";
        }*/
        lcIsPayPalRequired.Text = "PayPal is required for payment gateway.";

        if (feature.IsReturnPolicyEnabled)
        {
            lcIsReturnPolycyEnabled.Text = "Return Policy is required for listing.";
        }
        else
        {
            lcIsReturnPolycyEnabled.Text = "Return Policy can be set in this template.";
        }

        lcMinimunReservePrice.Text = "A reserve price can be set for listing. (minimun price is $" + feature.MinimumReservePrice.ToString() + ")";

        if (category.IsLSD)
        {
            lcIsLotSizeEnabled.Text = "Lot Size can be set in this template.";
        }
        else
        {
            lcIsLotSizeEnabled.Text = "Lot Size can not be set in this template.";
        }
    }

    private Boolean CheckCondition( string templateID, string[] productIDList )
    {
        Boolean conditionPass = true;

        EBayAccess access = new EBayAccess( UrlPath.StorefrontUrl );
        EBayTemplate eBayTemplate = DataAccessContextDeluxe.EBayTemplateRepository.GetOne( templateID );
        EBayCategory category = access.GetCategoriesDetailsByID( eBayTemplate.PrimaryeBayCategoryID, eBayTemplate.EBayListSite );
        EBayCategoryFeature feature = access.GetCategoryFeatureDetail( category.PimaryCategoryID, category.CategoryLevel, eBayTemplate.EBayListSite );

        ShowMessageByCondition( feature, category );

        if (feature.IsReturnPolicyEnabled)
        {
            if (!eBayTemplate.IsAcceptReturn)
            {
                lcMessage.Text = "Return Policy is required for listing. Please check your listing template.";
                return false;
            }
        }

        foreach (string productID in productIDList)
        {
            if (productID.Equals( "" ))
                break;

            Product product = GetProductByProductID( productID );

            double productPrice = 0.0;
            foreach (ProductPrice price in product.ProductPrices)
            {
                if (price.StoreID.Equals( "0" ))    //use defalut price
                {
                    productPrice = (double) price.Price;
                    break;
                }
            }

            if (eBayTemplate.DomesticShippingType == "Calculate" || eBayTemplate.InternationalShippingType == "Calculate")
            {
                if (product.Weight <= 0)
                {
                    lcMessage.Text = "Cannot list to eBay because some product does not have weight.";
                    return false;
                }
            }

            if (String.IsNullOrEmpty( product.ShortDescription ))
            {
                lcMessage.Text = "Cannot list to eBay because some product does not have short description.";
                return false;
            }

            if (product.IsCustomPrice)
            {
                lcMessage.Text = "Cannot list to eBay because some product(s) enabled custom price.";
                return false;
            }

            if (product.IsCallForPrice)
            {
                lcMessage.Text = "Cannot list to eBay because some product(s) enabled call for price.";
                return false;
            }

            IList<ProductOptionGroup> optionGroupList = product.ProductOptionGroups;
            if (optionGroupList.Count > 0)
            {
                lcMessage.Text = "Cannot list to eBay because some product(s) has option.";
                return false;
            }

            if (product.IsRecurring)
            {
                lcMessage.Text = "Cannot list to eBay because some product(s) enabled recurring option.";
                return false;
            }

            double eBayTemplateReservePrice = 0.0;
            double eBayTemplateStartingPrice = 0.0;
            double eBayTemplateBuyItNowPrice = 0.0;

            if (eBayTemplate.SellingMethod.Equals( "Online Auction" ))
            {
                if (eBayTemplate.UseReservePrice)
                {
                    switch (eBayTemplate.ReservePriceType)
                    {
                        case "ProductPrice":
                            eBayTemplateReservePrice = productPrice;
                            break;
                        case "PricePlusAmount":
                            eBayTemplateReservePrice = productPrice + ConvertUtilities.ToDouble( eBayTemplate.ReservePriceValue );
                            break;
                        case "PricePlusPercentage":
                            eBayTemplateReservePrice = productPrice + (productPrice * (ConvertUtilities.ToDouble( eBayTemplate.ReservePriceValue ) / 100));
                            break;
                        case "CustomPrice":
                            eBayTemplateReservePrice = ConvertUtilities.ToDouble( eBayTemplate.ReservePriceValue );
                            break;
                        default:
                            eBayTemplateReservePrice = productPrice;
                            break;
                    }
                }

                if (eBayTemplate.UseBuyItNowPrice)
                {
                    switch (eBayTemplate.BuyItNowPriceType)
                    {
                        case "ProductPrice":
                            eBayTemplateBuyItNowPrice = productPrice;
                            break;
                        case "PricePlusAmount":
                            eBayTemplateBuyItNowPrice = productPrice + ConvertUtilities.ToDouble( eBayTemplate.BuyItNowPriceValue );
                            break;
                        case "PricePlusPercentage":
                            eBayTemplateBuyItNowPrice = productPrice + (productPrice * (ConvertUtilities.ToDouble( eBayTemplate.BuyItNowPriceValue ) / 100));
                            break;
                        case "CustomPrice":
                            eBayTemplateBuyItNowPrice = ConvertUtilities.ToDouble( eBayTemplate.BuyItNowPriceValue );
                            break;
                        default:
                            eBayTemplateBuyItNowPrice = productPrice;
                            break;
                    }
                }

                switch (eBayTemplate.StartingPriceType)
                {
                    case "ProductPrice":
                        eBayTemplateStartingPrice = productPrice;
                        break;
                    case "PricePlusAmount":
                        eBayTemplateStartingPrice = productPrice + ConvertUtilities.ToDouble( eBayTemplate.StartingPriceValue );
                        break;
                    case "PricePlusPercentage":
                        eBayTemplateStartingPrice = productPrice + (productPrice * (ConvertUtilities.ToDouble( eBayTemplate.StartingPriceValue ) / 100));
                        break;
                    case "CustomPrice":
                        eBayTemplateStartingPrice = ConvertUtilities.ToDouble( eBayTemplate.StartingPriceValue );
                        break;
                    default:
                        eBayTemplateStartingPrice = productPrice;
                        break;
                }

                if (eBayTemplate.UseReservePrice)
                {
                    if (eBayTemplateReservePrice < feature.MinimumReservePrice)
                    {
                        lcMessage.Text = "Reserve Price in template is less than eBay minimun reserve price. Please check your template.";
                        return false;
                    }

                    if (eBayTemplateReservePrice <= eBayTemplateStartingPrice)
                    {
                        lcMessage.Text = "The starting price must be less than the reserve price. Please check your template.";
                        return false;
                    }
                }

                if (eBayTemplate.UseBuyItNowPrice)
                {
                    double eBayTemplateStartingPricePlusTenPercent = eBayTemplateStartingPrice + (eBayTemplateStartingPrice * 0.1);
                    if (eBayTemplateBuyItNowPrice <= eBayTemplateStartingPricePlusTenPercent)
                    {
                        lcMessage.Text = "Buy It Now price should be at least 10% more than your starting price. Please check your template.";
                        return false;
                    }
                }
            }
        }

        access.VerifyAddItem( GetProductByProductID( productIDList[0] ), eBayTemplate, category, false, DateTime.Now );
        if (access.HasError)
        {
            foreach (EBayErrorType error in access.ErrorTypeList)
            {
                lcMessage.Text += HttpUtility.HtmlEncode( error.ErrorLongMessage ) + " (" + error.ErrorCode + ")<br/>";
            }

            lcMessage.ForeColor = System.Drawing.Color.Red;
            return false;
        }
        return conditionPass;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        lcMessage.Text = "";
    }

    public Boolean PopulateControls( string templateID, string[] productIDList )
    {
        Boolean result = false;
        try
        {
            result = CheckCondition( templateID, productIDList );
        }
        catch
        {
            throw;
        }

        return result;
    }
}
