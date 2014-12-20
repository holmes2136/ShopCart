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

public partial class AdminAdvanced_Components_QuantityDiscount : AdminAdvancedBaseUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        InitDiscountDrop();
    }

    private void InitDiscountDrop()
    {
        if (!MainContext.IsPostBack)
        {
            uxQuantityDiscountDrop.DataSource = DataAccessContext.DiscountGroupRepository.GetAll();
            uxQuantityDiscountDrop.DataTextField = "GroupName";
            uxQuantityDiscountDrop.DataValueField = "DiscountGroupID";
            uxQuantityDiscountDrop.DataBind();
            uxQuantityDiscountDrop.Items.Insert( 0, new ListItem( "None", "0" ) );
            uxQuantityDiscountDrop.SelectedValue = "0";
        }
    }

    public void Clear()
    {
        uxQuantityDiscountDrop.SelectedIndex = 0;
    }

    public void Refresh( string discountGroupID )
    {
        uxQuantityDiscountDrop.SelectedValue = discountGroupID;
    }

    public string DiscountGounpID
    {
        get
        {
            return uxQuantityDiscountDrop.SelectedValue;
        }
    }

    public void IsQuantityDiscountEnabled( bool isEnabled )
    {
        if (!isEnabled)
        {
            uxQuantityDiscountDrop.SelectedIndex = 0;
            uxQuantityDiscountDrop.Enabled = false;
        }
        else
        {
            uxQuantityDiscountDrop.Enabled = true;
        }
    }
}
