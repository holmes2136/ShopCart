using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using Vevo.Deluxe.Domain.BundlePromotion;

public partial class Components_ProductKitCheckboxItem : Vevo.WebUI.International.BaseLanguageUserControl
{
    #region Private
    private ProductKitGroup _productKitGroup = ProductKitGroup.Null;

    private ProductKitGroup ProductKitGroup
    {
        get
        {
            if (_productKitGroup.IsNull)
            {
                return DataAccessContext.ProductKitGroupRepository.GetOne( StoreContext.Culture, uxKitGroupIDHidden.Value );
            }
            else
            {
                return _productKitGroup;
            }
        }
        set
        {
            _productKitGroup = value;
        }
    }

    private bool isCorrectQty()
    {
        foreach (DataListItem rows in uxInputDataList.Items)
        {
            bool check = ((CheckBox) rows.FindControl( "uxKitItemCheck" )).Checked;
            if (check)
            {
                string qty = ((TextBox) rows.FindControl( "uxQuantityText" )).Text.Trim();
                double Num;
                bool isNum = double.TryParse( qty, out Num );
                if ((Num <= 0) || (!isNum))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool isCheckedItem()
    {
        foreach (DataListItem rows in uxInputDataList.Items)
        {
            bool check = ((CheckBox) rows.FindControl( "uxKitItemCheck" )).Checked;
            if (check)
            {
                return true;
            }
        }
        return false;
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        uxMessageDiv.Visible = false;
    }

    protected void uxInputDataList_ItemDataBound( Object Sender, DataListItemEventArgs e )
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            Components_Common_TooltippedText uxProductKitItemToolTip = (Components_Common_TooltippedText) e.Item.FindControl( "uxTooltippedText" );
            uxProductKitItemToolTip.MainText = ProductKitGroup.Name;
            uxProductKitItemToolTip.TooltipText = ProductKitGroup.Description;

            Label uxRequireLabel = (Label) e.Item.FindControl( "uxRequireLabel" );

            if (ProductKitGroup.IsRequired)
                uxRequireLabel.Visible = true;
            else
                uxRequireLabel.Visible = false;
        }
    }

    protected bool ShowHideQuantity( object isUserDefined )
    {
        bool isDefined = (bool) isUserDefined;
        return !isDefined;
    }

    #endregion

    #region Public Methods

    public void SetupInputList( ProductKitGroup group, Culture culture, string storeID,bool isDynamicPrice )
    {
        ProductKitGroup = group;
        uxKitGroupIDHidden.Value = group.ProductKitGroupID;
        IList<ProductKitItem> itemsList = group.ProductKitItems;

        List<ProductKitItemDisplay> list = new List<ProductKitItemDisplay>();
        foreach (ProductKitItem item in itemsList)
        {
            list.Add( new ProductKitItemDisplay( item, culture, storeID, isDynamicPrice ) );
        }
        uxInputDataList.DataSource = list;
        uxInputDataList.DataBind();

    }

    public bool ValidateInput()
    {
        bool result = true;
        uxMessageLabel.Text = "";
        if (ProductKitGroup.IsNull)
        {
            uxMessageDiv.Visible = true;
            uxMessageLabel.Text = GetLanguageText("ProductKitGroupInvalid");
            result = false;
        }
        else
        {
            if (ProductKitGroup.IsRequired)
            {
                if (!isCheckedItem())
                {
                    uxMessageDiv.Visible = true;
                    uxMessageLabel.Text = GetLanguageText("ProductKitRequire");
                    result = false;
                }

                if (!isCorrectQty())
                {
                    uxMessageDiv.Visible = true;
                    uxMessageLabel.Text = GetLanguageText("ProductKitInputInvalid");
                    result = false;
                }
            }
            else
            {
                if (!isCorrectQty())
                {
                    uxMessageDiv.Visible = true;
                    uxMessageLabel.Text = GetLanguageText("ProductKitInputInvalid");
                    result = false;
                }
            }
        }
        return result;
    }

    public bool IsValidStock( int mainProductQuantity )
    {
        if ((!ProductKitGroup.IsRequired) && (!isCheckedItem()))
            return true;

        ArrayList selectedList = new ArrayList();
        CreateProductKitItems( selectedList );
        foreach (ProductKitItemValue item in selectedList)
        {
            int purchaseQuantity = item.Quantity * mainProductQuantity;
            if (purchaseQuantity <= 0) continue;
            Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, item.ProductID, new StoreRetriever().GetCurrentStoreID() );
            int currentStock = product.GetPurchasableStock(
                 StoreContext.ShoppingCart.GetNumberOfItems( item.ProductID ) + PromotionSelectedItem.FindSubProductAmountInPromotion( StoreContext.ShoppingCart, item.ProductID ) );
            if (product.IsOutOfStock(
                purchaseQuantity,
                StoreContext.ShoppingCart.GetNumberOfItems( item.ProductID ) + PromotionSelectedItem.FindSubProductAmountInPromotion( StoreContext.ShoppingCart, item.ProductID ) ))
            {
                uxMessageDiv.Visible = true;
                uxMessageLabel.Text = String.Format( GetLanguageText("CheckStockMessage") );
                return false;
            }
        }
        return true;
    }

    public void CreateProductKitItems( ArrayList selectedList )
    {
        IList<ProductKitItem> itemsList = ProductKitGroup.ProductKitItems;
        for (int i = 0; i < uxInputDataList.Items.Count; i++)
        {
            DataListItem rows = uxInputDataList.Items[i];
            bool check = ((CheckBox) rows.FindControl( "uxKitItemCheck" )).Checked;
            if (check)
            {
                int qty;
                bool isNum = int.TryParse( ((TextBox) rows.FindControl( "uxQuantityText" )).Text, out qty );
                string name = ((Label) rows.FindControl( "uxInputLabel" )).Text;
                ProductKitItemValue value = new ProductKitItemValue( itemsList[i], ProductKitGroup.ProductKitGroupType.Checkbox, name, qty );
                selectedList.Add( value );

            }
        }
    }

    #endregion
}
