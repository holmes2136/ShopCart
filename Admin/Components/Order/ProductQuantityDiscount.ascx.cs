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
using Vevo.Domain.Discounts;
using Vevo.Domain;
using Vevo.Shared.Utilities;

public partial class Admin_Components_Order_ProductQuantityDiscount : AdminAdvancedBaseUserControl
{
    #region Private


    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }

    #endregion


    #region Public Properties

    public void PopulateControls()
    {
        DiscountGroup discountGroup = DataAccessContext.DiscountGroupRepository.GetOne( DiscountGroupID );

        if (discountGroup.DiscountRules.Count > 0)
        {
            uxQuantityDiscountView.DataSource = discountGroup.DiscountRules;
            uxQuantityDiscountView.DataBind();
            uxQuantityDiscountPanel.Visible = true;
        }
        else
        {
            uxQuantityDiscountPanel.Visible = false;
        }
    }

    public string DiscountGroupID
    {
        get
        {
            if (ViewState["DiscountGroupID"] == null)
                return "0";
            return ViewState["DiscountGroupID"].ToString();
        }
        set
        {
            ViewState["DiscountGroupID"] = value;
        }
    }

    #endregion


    #region Public Methods

    public string ShowFromItem( object toItem )
    {
        int fromItems = 0;
        if (DiscountGroupID != "0")
        {
            DiscountGroup discountGroup = DataAccessContext.DiscountGroupRepository.GetOne( DiscountGroupID );

            for (int i = 0; i < discountGroup.DiscountRules.Count; i++)
            {
                if (discountGroup.DiscountRules[i].ToItems == ConvertUtilities.ToInt32( toItem ))
                    return String.Format( "{0}", ++fromItems );
                else
                    fromItems = discountGroup.DiscountRules[i].ToItems;
            }
        }
        return String.Format( "{0}", ++fromItems );
    }

    public string LastToItems( string toItems )
    {
        if (toItems == DiscountRule.MaxNumberOfItems.ToString())
            return "Above";
        else
            return toItems;
    }

    #endregion
}
