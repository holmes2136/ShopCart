using System;
using System.Collections;
using System.Collections.Generic;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.WebUI.Orders;

public partial class Mobile_Components_ProductReOrder : Vevo.WebUI.Products.BaseProductUserControl
{
    private string CurrentOrderID
    {
        get
        {
            if (!String.IsNullOrEmpty( Request.QueryString["OrderID"] ))
                return Request.QueryString["OrderID"];
            else
                return "0";
        }
    }

    private bool IsOptionGroupExist(
        Product product,
        IList<ProductOptionGroup> productOptionGroupList,
        string productOptionGroupName,
        out string optionGroupName,
        out OptionGroup optionGroup )
    {
        foreach (ProductOptionGroup productOptionGroup in productOptionGroupList)
        {
            string[] optNameList = productOptionGroupName.Split( ':' );
            string optName = string.Empty;

            for (int i = 0; i < optNameList.Length; i++)
            {
                if (i != 0)
                {
                    optName += ":";
                }

                optName += optNameList[i];
                string trimOptName = optName;

                if (trimOptName.Trim().Equals( productOptionGroup.OptionGroup.Name ))
                {
                    optionGroupName = trimOptName.Trim();
                    optionGroup = productOptionGroup.OptionGroup;
                    return true;
                }
            }

        }

        optionGroupName = string.Empty;
        optionGroup = OptionGroup.Null;

        return false;
    }

    private bool IsProductOptionValid( Product product, OrderItem orderItem, out ArrayList optionItemNameArray )
    {
        optionItemNameArray = new ArrayList();

        IList<ProductOptionGroup> productOptionGroupList = product.ProductOptionGroups;
        if (productOptionGroupList.Count <= 0)
            return true;

        string productOptionName = orderItem.Name.Substring( product.Name.Length );

        string[] productOptionNameList = productOptionName.Split( new string[] { "<br>" }, StringSplitOptions.None );

        string optionGroupName = String.Empty;
        OptionGroup optionGroup;
        string optionItemName = String.Empty;


        foreach (string productOption in productOptionNameList)
        {
            if (!String.IsNullOrEmpty( productOption ))
            {
                if (IsOptionGroupExist( product, productOptionGroupList, productOption, out optionGroupName, out optionGroup ))
                {
                    if (!IsOptionItemExist( orderItem, optionGroup, optionGroupName, out optionItemName ))
                    {
                        return false;
                    }
                    else
                    {
                        optionItemNameArray.Add( optionItemName );
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool IsOptionItemExist( OrderItem orderItem, OptionGroup optionGroup, string optionName, out string optionItemName )
    {
        IList<OptionItem> optItem = optionGroup.OptionItems;

        string[] splitOptionName = orderItem.Name.Split( new string[] { optionName + " : " }, StringSplitOptions.None );
        string[] splitItemName = splitOptionName[1].Split( new string[] { "(cost" }, StringSplitOptions.None );
        string itemName = splitItemName[0].Trim();

        for (int i = 0; i < optItem.Count; i++)
        {
            if (optItem[i].Name.Equals( itemName ))
            {
                optionItemName = itemName;
                return true;
            }
        }

        optionItemName = string.Empty;
        return false;
    }

    private string GetOptionItemIDsFromItemName(
        OptionGroup optionGroup,
        ArrayList optionItemNameArray )
    {
        IList<OptionItem> optItem = optionGroup.OptionItems;

        string optionItemIDs = string.Empty;

        for (int i = 0; i < optItem.Count; i++)
        {
            foreach (string optionItemName in optionItemNameArray)
            {
                if (optItem[i].Name.Equals( optionItemName ))
                {
                    optionItemIDs += optItem[i].OptionItemID + ",";
                }
            }
        }
        return optionItemIDs;
    }

    private void ErrorMessage( string text )
    {
        uxErrorMessage.Text = text;
    }

    private bool ReOrderToCart( Product product, OrderItem orderItem, string optionItemIDs )
    {
        string errorCurrentStock = string.Empty;
        int currentStock = 0;
        bool isAddToCartSuccess = false;

        OptionItemValueCollection optionCollection = new OptionItemValueCollection( StoreContext.Culture, optionItemIDs, product.ProductID );
        CartItemGiftDetails giftDetails = new CartItemGiftDetails();

        if (product.IsGiftCertificate)
        {
            GiftCertificateProduct giftProduct = (GiftCertificateProduct) product;

            IList<GiftCertificate> giftCertificateList = DataAccessContext.GiftCertificateRepository.GetAllByOrderID( orderItem.OrderID );

            foreach (GiftCertificate giftCertificate in giftCertificateList)
            {
                if (orderItem.OrderItemID == giftCertificate.OrderItemID)
                {
                    giftDetails = new CartItemGiftDetails(
                        giftCertificate.Recipient,
                        giftCertificate.PersonalNote,
                        giftCertificate.NeedPhysical,
                        giftCertificate.GiftValue );
                }
            }

            if (giftProduct.GiftAmount == 0)
            {
                giftProduct.GiftAmount = orderItem.UnitPrice;
            }

            product = (Product) giftProduct;
            CartAddItemService addToCartService = new CartAddItemService(
                StoreContext.Culture, StoreContext.ShoppingCart );
            isAddToCartSuccess = addToCartService.AddToCart( 
                product, 
                optionCollection, 
                ProductKitItemValueCollection.Null, 
                orderItem.Quantity, 
                giftDetails, 
                0, 
                out errorCurrentStock, 
                out currentStock );
        }
        else if (product.IsCustomPrice)
        {
            CartAddItemService addToCartService = new CartAddItemService(
                StoreContext.Culture, StoreContext.ShoppingCart );
            isAddToCartSuccess = addToCartService.AddToCart( 
                product, 
                optionCollection, 
                ProductKitItemValueCollection.Null, 
                orderItem.Quantity, 
                giftDetails, 
                orderItem.UnitPrice, 
                out errorCurrentStock, 
                out currentStock );
        }
        else
        {
            CartAddItemService addToCartService = new CartAddItemService(
                StoreContext.Culture, StoreContext.ShoppingCart );
            isAddToCartSuccess = addToCartService.AddToCart( 
                product, 
                optionCollection, 
                ProductKitItemValueCollection.Null, 
                orderItem.Quantity, 
                giftDetails, 
                0, 
                out errorCurrentStock, 
                out currentStock );
        }

        if (!isAddToCartSuccess)
        {
            StoreContext.ClearCheckoutSession();
            string message = "<p class=\"ErrorHeader\">[$StockError]</p>";

            ErrorMessage( message );
            return false;
        }

        return true;
    }

    private bool IsProductIsValid( Product product, OrderItem orderItem, out ArrayList optionItemNameArray )
    {
        //check product enabled
        optionItemNameArray = null;

        if (!product.IsEnabled)
        {
            StoreContext.ClearCheckoutSession();

            string message = "<p class=\"ErrorHeader\">[$Product is inactive]</p>";
            ErrorMessage( message );
            return false;
        }

        //check is product option valid
        if (!IsProductOptionValid( product, orderItem, out optionItemNameArray ))
        {
            StoreContext.ClearCheckoutSession();

            string message = "<p class=\"ErrorHeader\">[$Product changed]</p>";
            ErrorMessage( message );
            return false;
        }

        //check is category enabled
        if (!product.IsProductAvailable( StoreContext.CurrentStore.StoreID ))
        {
            StoreContext.ClearCheckoutSession();

            string message = "<p class=\"ErrorHeader\">[$Product is inactive]</p>";
            ErrorMessage( message );
            return false;
        }
        return true;
    }

    private void ReorderProcess()
    {
        Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
        IList<OrderItem> orderList = order.OrderItems;

        foreach (OrderItem orderItem in orderList)
        {
            int productCount = DataAccessContext.ProductRepository.GetAllProductCountBySku( orderItem.Sku );
            Product product;

            if (productCount != 1)
            {
                IList<Product> products = DataAccessContext.ProductRepository.GetProductByName( StoreContext.Culture, StoreContext.CurrentStore.StoreID, orderItem.Name );
                if (products.Count == 1)
                    product = products[0];
                else
                    return;
            }
            else
            {
                product = DataAccessContext.ProductRepository.GetOneBySku( StoreContext.Culture, StoreContext.CurrentStore.StoreID, orderItem.Sku );
            }

            IList<ProductOptionGroup> productOptionGroup = product.ProductOptionGroups;
            ArrayList optionItemNameArray = new ArrayList();

            if (!IsProductIsValid( product, orderItem, out optionItemNameArray ))
                return;

            string optionItemIDs = string.Empty;
            for (int i = 0; i < productOptionGroup.Count; i++)
            {
                optionItemIDs += GetOptionItemIDsFromItemName( productOptionGroup[i].OptionGroup, optionItemNameArray );
            }

            if (!ReOrderToCart( product, orderItem, optionItemIDs ))
                return;
        }

        Response.Redirect( "ShoppingCart.aspx" );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (CurrentOrderID.Equals( "0" ))
        {
            string message = "<p class=\"ErrorHeader\">[$Error]</p>";
            ErrorMessage( message );
            return;
        }
        ReorderProcess();
    }
}
