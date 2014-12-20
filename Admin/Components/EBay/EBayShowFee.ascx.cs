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
using Vevo;
using System.Collections.Generic;
using Vevo.eBay;
using Vevo.Deluxe.Domain.EBay;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.Shared.WebUI;
using Vevo.Domain.Products;
using Vevo.Deluxe.Domain;

public partial class AdminAdvanced_Components_EBay_EBayShowFee : AdminAdvancedBaseUserControl
{
    private Product GetProductByProductID( string productID )
    {
        return DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, productID, StoreContext.CurrentStore.StoreID );
    }
    private EBayTemplate GetEBayTemplateByID( string templateID )
    {
        return DataAccessContextDeluxe.EBayTemplateRepository.GetOne( templateID );
    }

    private EBayCategory GetEBayCategoryByTemplate( EBayTemplate template )
    {
        EBayAccess access = new EBayAccess();
        EBayCategory primaryCategory = access.GetCategoriesDetailsByID( template.PrimaryeBayCategoryID, template.EBayListSite );
        EBayCategory eBayCategory = primaryCategory;
        if (!template.SecondaryeBayCategoryID.Equals( "0" ))
        {
            EBayCategory secondaryCategory = access.GetCategoriesDetailsByID( template.SecondaryeBayCategoryID, template.EBayListSite );
            eBayCategory.SecondaryCategoryID = secondaryCategory.PimaryCategoryID;
            eBayCategory.SecondaryCategoryName = secondaryCategory.PimaryCategoryName;
        }
        return eBayCategory;
    }

    private void ShowListingFee( string[] productIDList, EBayTemplate template, EBayCategory category, Boolean isScheduleDate, DateTime scheduleDateTime )
    {
        EBayAccess access = new EBayAccess( UrlPath.StorefrontUrl );
        IList<EBayFee> feeList = new List<EBayFee>();
        int productCount = productIDList.Length - 1;
        string totalFee = string.Empty;
        string firstProductID = productIDList[0];

        DataTable dt = new DataTable();
        dt.Columns.Add( "feeName" );
        dt.Columns.Add( "feeCost" );

        if (!String.IsNullOrEmpty( firstProductID ))
        {
            feeList = access.VerifyAddItem( GetProductByProductID( firstProductID ), template, category, isScheduleDate, scheduleDateTime );
            foreach (EBayFee fee in feeList)
            {
                if (fee.Name.Equals( "ListingFee" ))    //Total fee for listing the item
                {
                    Currency currency = DataAccessContext.CurrencyRepository.GetOne( fee.Currency.ToString() );
                    totalFee = currency.FormatPrice( (fee.Value * productCount).ToString() );
                    DataRow rowCostPerItem = dt.NewRow();
                    rowCostPerItem["feeName"] = "Per-Item cost";
                    rowCostPerItem["feeCost"] = currency.FormatPrice( fee.Value.ToString() );
                    dt.Rows.Add( rowCostPerItem );
                    break;
                }
            }
            foreach (EBayFee fee in feeList)
            {
                if (fee.Value != 0 && !fee.Name.Equals( "ListingFee" ))
                {
                    Currency currency = DataAccessContext.CurrencyRepository.GetOne( fee.Currency.ToString() );
                    DataRow dr = dt.NewRow();
                    dr["feeName"] = "- " + fee.Name;
                    dr["feeCost"] = currency.FormatPrice( fee.Value.ToString() );
                    dt.Rows.Add( dr );
                }
            }

            DataRow rowTotal = dt.NewRow();
            rowTotal["feeName"] = "Total fees for " + productCount.ToString() + " item(s)";
            rowTotal["feeCost"] = totalFee;
            dt.Rows.Add( rowTotal );
        }

        uxFeeDetailGrid.DataSource = dt;
        uxFeeDetailGrid.DataBind();
        uxFeeDetailGrid.Rows[0].CssClass = "CssAdminContentTop";
        uxFeeDetailGrid.Rows[uxFeeDetailGrid.Rows.Count - 1].CssClass = "CssAdminContentTop";
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    public void PopulateControls( string templateID, string[] productIDList, Boolean isScheduleDate, DateTime scheduleDateTime )
    {
        EBayTemplate template = GetEBayTemplateByID( templateID );
        ShowListingFee( productIDList, template, GetEBayCategoryByTemplate( template ), isScheduleDate, scheduleDateTime );
    }
}
