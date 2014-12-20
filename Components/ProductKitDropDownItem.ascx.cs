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

public partial class Components_ProductKitDropDownItem : Vevo.WebUI.International.BaseLanguageUserControl
{
    private ProductKitGroup _productKitGroup = ProductKitGroup.Null;

    #region Private

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

    private ProductKitItem GetSelectedProductKitItem()
    {
        if (uxKitItemDrop.SelectedIndex > 0)
        {
            IList<ProductKitItem> itemsList = ProductKitGroup.ProductKitItems;
            return itemsList[uxKitItemDrop.SelectedIndex - 1];
        }
        else
        {
            return ProductKitItem.Null;
        }
    }

    private bool isCorrectQty()
    {
        double Num;
        bool isNum = double.TryParse( uxQuantityText.Text, out Num );
        if ((Num <= 0) || (!isNum))
        {
            return false;
        }
        return true;
    }

    private void PopulateControls()
    {
        ProductKitItem kitItem = GetSelectedProductKitItem();
        if (kitItem.IsDefault)
        {
            Quantity = kitItem.Quantity.ToString();
        }
        else
        {
            Quantity = "";
        }

        SetDisplayIsUserDefinedQTY( kitItem );
    }

    private void SetDisplayIsUserDefinedQTY( ProductKitItem kitItem )
    {
        if ((kitItem.IsUserDefinedQuantity))
        {
            uxQuantityText.Visible = true;
            uxQuantityLabel.Visible = false;
        }
        else
        {
            uxQuantityText.Visible = false;
            uxQuantityLabel.Visible = true;
        }
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        uxMessageDiv.Visible = false;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!Page.IsPostBack)
            PopulateControls();
    }

    protected void uxKitItemDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        ProductKitItem kitItem = GetSelectedProductKitItem();
        if (!kitItem.IsNull)
        {
            SetDisplayIsUserDefinedQTY( kitItem );
            Quantity = kitItem.Quantity.ToString();
        }
        else
        {
            uxQuantityText.Visible = false;
            uxQuantityLabel.Visible = true;
            Quantity = "";
        }
    }

    #endregion


    #region Public Methods

    public string Quantity
    {
        get
        {
            ProductKitItem kitItem = GetSelectedProductKitItem();
            if ((kitItem.IsUserDefinedQuantity))
            {
                return uxQuantityText.Text;
            }
            else
            {
                return uxQuantityLabel.Text;
            }
        }

        set
        {
            uxQuantityLabel.Text = value;
            uxQuantityText.Text = value;
        }
    }

    public void SetupInputList( ProductKitGroup group, Culture culture, string storeID, bool isDynamicPrice )
    {
        ProductKitGroup = group;
        uxTooltippedText.MainText = ProductKitGroup.Name;
        uxTooltippedText.TooltipText = ProductKitGroup.Description;

        if (ProductKitGroup.IsRequired)
            uxRequireLabel.Visible = true;
        else
            uxRequireLabel.Visible = false;

        uxKitGroupIDHidden.Value = group.ProductKitGroupID;
        int selectIndex = 0;
        int curIndex = 0;
        IList<ProductKitItem> itemsList = group.ProductKitItems;

        List<ProductKitItemDisplay> list = new List<ProductKitItemDisplay>();

        foreach (ProductKitItem item in itemsList)
        {
            curIndex += 1;
            if (item.IsDefault) selectIndex = curIndex;
            list.Add( new ProductKitItemDisplay( item, culture, storeID, isDynamicPrice ) );
        }
        uxKitItemDrop.DataSource = list;
        uxKitItemDrop.DataBind();
        uxKitItemDrop.Items.Insert( 0, new ListItem( "--- Please select ---", String.Empty ) );

        uxKitItemDrop.SelectedIndex = selectIndex;
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
                if (uxKitItemDrop.SelectedValue == "")
                {
                    uxMessageDiv.Visible = true;
                    uxMessageLabel.Text = GetLanguageText("ProductKitRequire");
                    result = false;
                }
                else if (!isCorrectQty())
                {
                    uxMessageDiv.Visible = true;
                    uxMessageLabel.Text = GetLanguageText("ProductKitInputInvalid");
                    result = false;
                }
            }
            else
            {
                if ((uxKitItemDrop.SelectedValue != "") && (!isCorrectQty()))
                {
                    uxMessageDiv.Visible = true;
                    uxMessageLabel.Text = GetLanguageText( "ProductKitRequire" );
                    result = false;
                }
            }
        }
        return result;
    }

    public bool IsValidStock( int mainProductQuantity )
    {
        if ((!ProductKitGroup.IsRequired) && (uxKitItemDrop.SelectedValue == ""))
            return true;
        int productQty;
        bool isNum = int.TryParse( Quantity, out productQty );
        if (isNum == false)
        {
            uxMessageLabel.Text = GetLanguageText("ProductKitInputInvalid");
            return false;
        }
        int purchaseQuantity = productQty * mainProductQuantity;
        if (purchaseQuantity <= 0) return true;
        ProductKitItem kitItem = GetSelectedProductKitItem();
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, kitItem.ProductID, new StoreRetriever().GetCurrentStoreID() );
        int currentStock = product.GetPurchasableStock(
             StoreContext.ShoppingCart.GetNumberOfItems( kitItem.ProductID ) + PromotionSelectedItem.FindSubProductAmountInPromotion( StoreContext.ShoppingCart, kitItem.ProductID ) );
        if (product.IsOutOfStock(
            purchaseQuantity,
            StoreContext.ShoppingCart.GetNumberOfItems( kitItem.ProductID ) + PromotionSelectedItem.FindSubProductAmountInPromotion( StoreContext.ShoppingCart, kitItem.ProductID ) ))
        {
            uxMessageDiv.Visible = true;
            uxMessageLabel.Text = String.Format( GetLanguageText("CheckStockMessage") );
            return false;
        }
        return true;
    }

    public void CreateProductKitItems( ArrayList selectedList )
    {
        ProductKitItem kitItem = GetSelectedProductKitItem();
        if (!kitItem.IsNull)
        {
            int qty;
            bool isNum = int.TryParse( uxQuantityText.Text, out qty );
            ProductKitItemValue value = new ProductKitItemValue( kitItem, ProductKitGroup.ProductKitGroupType.DropDown, uxKitItemDrop.SelectedItem.Text, qty );
            selectedList.Add( value );
        }
    }

    #endregion
}
