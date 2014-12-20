using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Discounts;
using Vevo.Shared.DataAccess;
using Vevo.Shared.Utilities;

namespace Vevo
{
    /// <summary>
    /// Summary description for DatabaseConverterPro
    /// </summary>
    public partial class DatabaseConverter
    {
        private DataTable GetAllDiscountGroup()
        {
            return DataAccess.ExecuteSelect( "SELECT * FROM DiscountGroup" );
        }

        private DataTable GetAllDiscountRule()
        {
            return DataAccess.ExecuteSelect(
                "SELECT * FROM DiscountRule " +
                "ORDER BY DiscountRuleID;" );
        }

        // This function upgrade DiscountGroup and DiscountRule table from version 2.52 to 3.0.
        // In version 2.52, the "MinItems" column in DiscountRule is the starting quantity to use discount rate.
        // In version 3.0, the "ToItems" column is the ending quantity to use discount rate.
        //
        // ----- Version 2.52 -----
        // MinItems     Amount
        //   5             10
        //   10            20
        //
        // Will be converted to
        // 
        // ----- Version 3.0 -----
        // ToItems     Amount
        //   4              0
        //   9             10
        // Above           20

       
        private void CopyDiscount()
        {
            DataTable discountGroupTable = GetAllDiscountGroup();
            DataTable discountRuleTable = GetAllDiscountRule();
            for (int i = 0; i < discountGroupTable.Rows.Count; i++)
            {
                string discountGroupID = discountGroupTable.Rows[i]["DiscountGroupID"].ToString();
                DataRow[] discountrule = discountRuleTable.Select( "DiscountGroupID = " + discountGroupID );

                DiscountGroup discountGroup = DataAccessContext.DiscountGroupRepository.GetOne( discountGroupID );
                discountGroup.DiscountType = (DiscountGroup.DiscountTypeEnum) Enum.Parse(
                    typeof( DiscountGroup.DiscountTypeEnum ), discountrule[0]["DiscountType"].ToString() );
                discountGroup.ProductOptionDiscount = false;

                discountGroup = CopyDiscountRule( discountGroup );
                DataAccessContext.DiscountGroupRepository.Save( discountGroup );
            }
            DataAccess.ExecuteNonQueryNoParameter( "ALTER TABLE DiscountRule DROP COLUMN DiscountType;" );
        }

        //private void CopyDiscountRule( DataRow[] discountrule )
        //{
        //    int firstToItems = ConvertUtilities.ToInt32( discountrule[0]["ToItems"] ) - 1;
        //    int toItems;
        //    DiscountRuleAccess.Create( discountrule[0]["DiscountGroupID"].ToString(), firstToItems.ToString(), 0, 0 );
        //    for (int j = 0; j < discountrule.Length - 1; j++)
        //    {
        //        toItems = ConvertUtilities.ToInt32( discountrule[j + 1]["ToItems"] ) - 1;
        //        DiscountRuleAccess.Update(
        //            discountrule[j]["DiscountRuleID"].ToString(),
        //            discountrule[j]["DiscountGroupID"].ToString(),
        //            toItems.ToString(),
        //            float.Parse( discountrule[j]["Percentage"].ToString() ),
        //            ConvertUtilities.ToDecimal( discountrule[j]["Amount"] )
        //        );
        //    }
        //    int Last = discountrule.Length - 1;
        //    DiscountRuleAccess.Update(
        //        discountrule[Last]["DiscountRuleID"].ToString(),
        //        discountrule[Last]["DiscountGroupID"].ToString(),
        //        SystemConst.UnlimitedNumber.ToString(),
        //        float.Parse( discountrule[Last]["Percentage"].ToString() ),
        //        ConvertUtilities.ToDecimal( discountrule[Last]["Amount"] )
        //    );
        //}

        private DiscountGroup CopyDiscountRule( DiscountGroup discountGroup )
        {
            int firstToItems = discountGroup.DiscountRules[0].ToItems - 1;
            int toItems;
            DiscountRule discountRule = new DiscountRule();
            discountRule.DiscountGroupID = discountGroup.DiscountGroupID;
            discountRule.ToItems = firstToItems;
            discountRule.Amount = 0;
            discountRule.Percentage = 0;
            discountGroup.DiscountRules.Add( discountRule );

            for (int j = 0; j < discountGroup.DiscountRules.Count - 1; j++)
            {
                toItems = discountGroup.DiscountRules[j + 1].ToItems - 1;
                discountGroup.DiscountRules[j].ToItems = toItems;
            }

            int Last = discountGroup.DiscountRules.Count - 1;
            discountGroup.DiscountRules[Last].ToItems = SystemConst.UnlimitedNumber;

            return discountGroup;
        }
    }
}
