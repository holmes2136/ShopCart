using System;
using System.Collections.Generic;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.Products;

public partial class Admin_Components_Products_ProductSubscription : AdminAdvancedBaseUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {

    }

    public string CurrentID
    {
        get
        {
            if (String.IsNullOrEmpty( MainContext.QueryString["ProductID"] ))
                return "0";
            else
                return MainContext.QueryString["ProductID"];
        }
    }

    public void HideProductSubscription()
    {
        uxIsSubscriptionProduct.SelectedValue = "False";
        uxSubscriptionProductInfoTR.Visible = false;
    }

    public void ShowProductSubscription()
    {
        uxIsSubscriptionProduct.SelectedValue = "True";
        uxSubscriptionProductInfoTR.Visible = true;
    }

    public void EnableProductScuscriptionControl()
    {
        uxIsSubscriptionProduct.Enabled = true;  
    }

    public void DisableProductScuscriptionControl()
    {
        uxIsSubscriptionProduct.Enabled = false;
    }

    public void PopulateControls( Product product )
    {
        ProductSubscription subscriptionItem = new ProductSubscription( product.ProductID );

        if (subscriptionItem.IsSubscriptionProduct())
        {
            ShowProductSubscription();

        }
        else
        {
            HideProductSubscription();
        }

        if (subscriptionItem.IsSubscriptionProduct())
        {
            uxIsSubscriptionProduct.SelectedValue = "True";
            uxSubscriptionRangeText.Text = subscriptionItem.ProductSubscriptions[0].Range.ToString();
            uxSubscriptionLevel.SelectedValue = subscriptionItem.ProductSubscriptions[0].SubscriptionLevelID;
        }
        else
        {
            uxIsSubscriptionProduct.SelectedValue = "False";
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {

    }

    public void ClearInputFields()
    {
        HideProductSubscription();
        uxSubscriptionLevel.SelectedIndex = 0;
        uxSubscriptionRangeText.Text = String.Empty;

    }

    public ProductSubscription Setup( Product product )
    {
        ProductSubscription subscriptionItem = new ProductSubscription( product.ProductID );

        if (ConvertUtilities.ToBoolean( uxIsSubscriptionProduct.SelectedValue ))
        {
            subscriptionItem.ProductSubscriptions = new List<ProductSubscription>();

            ProductSubscription subscription = new ProductSubscription();
            subscription.SubscriptionLevelID = uxSubscriptionLevel.SelectedValue;
            subscription.Range = ConvertUtilities.ToInt32( uxSubscriptionRangeText.Text );
            subscription.ProductID = product.ProductID;

            subscriptionItem.ProductSubscriptions.Add( subscription );

        }
        else
        {
            subscriptionItem.ProductSubscriptions = new List<ProductSubscription>();
        }

        return subscriptionItem;

    }

    public void InitDropDown()
    {
        uxSubscriptionLevel.DataSource = DataAccessContextDeluxe.SubscriptionLevelRepository.GetAll( "SubscriptionLevelID" );
        uxSubscriptionLevel.DataBind();
    }

    public bool IsProductSubscription
    {
        get { return ConvertUtilities.ToBoolean( uxIsSubscriptionProduct.SelectedValue ); }
    }
}
