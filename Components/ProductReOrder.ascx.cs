using System;
using System.Collections;
using System.Collections.Generic;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.Products;
using Vevo.Deluxe.Domain.Users;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using Vevo.WebUI.Orders;

public partial class Components_ProductReOrder : Vevo.WebUI.Products.BaseProductUserControl
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
        out ArrayList optionGroupNames,
        out ArrayList optionGroups )
    {
        optionGroupNames = new ArrayList();
        optionGroups = new ArrayList();

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
                    optionGroupNames.Add( trimOptName.Trim() );
                    optionGroups.Add( productOptionGroup.OptionGroup );
                }
            }

        }

        if ((optionGroupNames.Count != 0) && (optionGroups.Count != 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private string IsProductKitGroupValidInProduct( string productID, Culture culture, string productKitGroupName, Product productKitItem )
    {
        IList<ProductKitGroup> groups = DataAccessContext.ProductKitGroupRepository.GetAllByProductID( culture, productID, "" );
        foreach (ProductKitGroup group in groups)
        {
            if (productKitGroupName == group.Name)
            {
                foreach (ProductKitItem item in group.ProductKitItems)
                {
                    if (item.ProductID == productKitItem.ProductID)
                    {
                        return item.ProductKitGroupID;
                    }
                }

            }
        }
        return String.Empty;
    }

    private bool IsProductKitValid( Product product, OrderItem orderItem, Culture culture, string storeID, out ArrayList productKitItemNameArray, out ArrayList productKitItemCountArray, out ArrayList productKitGroupIDArray )
    {
        productKitItemNameArray = new ArrayList();
        productKitItemCountArray = new ArrayList();
        productKitGroupIDArray = new ArrayList();
        IList<ProductKit> productKitList = product.ProductKits;

        string productKitName = orderItem.Name.Substring( product.Name.Length );

        string[] productKitNameList = productKitName.Split( new string[] { "<br>" }, StringSplitOptions.None );

        foreach (string productKit in productKitNameList)
        {
            if ((!productKit.StartsWith( "- " )) || (productKit.Trim() == "Items:"))
                continue;
            if (!String.IsNullOrEmpty( productKit ))
            {
                string[] subName = productKit.Split( new string[] { "x" }, StringSplitOptions.None );
                if (subName.Length > 0)
                {
                    int productCount;
                    if (int.TryParse( subName[subName.Length - 1], out productCount ))
                    {
                        string[] fullName = subName[0].Substring( 2 ).Trim().Split( new string[] { ":" }, StringSplitOptions.None );
                        if (fullName.Length != 2)
                            continue;
                        string groupName = fullName[0];
                        string productName = fullName[1];
                        IList<Product> products = DataAccessContext.ProductRepository.GetProductByName( StoreContext.Culture, StoreContext.CurrentStore.StoreID, productName );
                        if (products.Count != 1)
                            return false;
                        if ((!products[0].IsNull) && (products[0].IsProductAvailable( storeID )))
                        {
                            string groupID = IsProductKitGroupValidInProduct( product.ProductID, culture, groupName, products[0] );
                            if (groupID != String.Empty)
                            {
                                productKitItemNameArray.Add( products[0] );
                                productKitItemCountArray.Add( productCount );
                                productKitGroupIDArray.Add( groupID );
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
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

    private bool IsProductOptionValid( Product product, OrderItem orderItem, out ArrayList optionItemNameArray )
    {
        optionItemNameArray = new ArrayList();

        IList<ProductOptionGroup> productOptionGroupList = product.ProductOptionGroups;
        string mainString = orderItem.Name;
        if ((product.IsProductKit) && (mainString.IndexOf( "Items:" ) >= 0))
        {
            mainString = mainString.Substring( 0, mainString.IndexOf( "Items:" ) );
        }
        string productOptionName = mainString.Substring( product.Name.Length );

        string[] productOptionNameList = productOptionName.Split( new string[] { "<br>" }, StringSplitOptions.None );

        string optionGroupName = String.Empty;
        string optionItemName = String.Empty;

        int optionCount = 0;
        foreach (string productOption in productOptionNameList)
        {
            if (!String.IsNullOrEmpty( productOption ))
            {
                optionCount += 1;
            }
        }
        if (optionCount != productOptionGroupList.Count)
            return false;

        foreach (string productOption in productOptionNameList)
        {
            if (!String.IsNullOrEmpty( productOption ))
            {
                ArrayList optionGroupNames, optionGroups;
                if (IsOptionGroupExist( product, productOptionGroupList, productOption, out optionGroupNames, out optionGroups ))
                {
                    if (!IsOptionItemExist( productOption, optionGroups, optionGroupName, out optionItemName ))
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

    private bool IsOptionItemExist( string mainString, ArrayList optionGroups, string optionName, out string optionItemName )
    {
        foreach (OptionGroup optionGroup in optionGroups)
        {
            IList<OptionItem> optItem = optionGroup.OptionItems;

            string[] splitOptionName = mainString.Split( new string[] { optionName + " : " }, StringSplitOptions.None );
            string[] splitItemName = splitOptionName[1].Split( new string[] { "(cost" }, StringSplitOptions.None );
            string itemName = splitItemName[0].Replace( "<br>", "" ).Trim();

            for (int i = 0; i < optItem.Count; i++)
            {
                if (optItem[i].Name.Equals( itemName ))
                {
                    optionItemName = itemName;
                    return true;
                }
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

    private ProductKitItemValueCollection GenerateProductKitItemValueCollection( Product product, OrderItem orderItem, ArrayList productKit, ArrayList productKitCount, ArrayList productKitGroupID )
    {
        ProductKitItemValueCollection kitCollection = new ProductKitItemValueCollection();
        for (int i = 0; i < productKit.Count; i++)
        {
            Product subProduct = (Product) productKit[i];
            ProductKitItem item = new ProductKitItem();
            item.IsUserDefinedQuantity = true;
            item.ProductKitGroupID = (string) productKitGroupID[i];
            item.ProductID = subProduct.ProductID;
            ProductKitItemValue value = new ProductKitItemValue( item, ProductKitGroup.ProductKitGroupType.Unknown, "",
            (int) productKitCount[i] );
            kitCollection.Add( value );
        }
        return kitCollection;
    }

    private bool ReOrderToCart( Product product, OrderItem orderItem, string optionItemIDs, ProductKitItemValueCollection itemCollection )
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
                itemCollection, 
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
                itemCollection, 
                GetProductQuantity( product, orderItem ), 
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
                itemCollection, 
                GetProductQuantity( product, orderItem ), 
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

    private bool IsProductIsValid( Product product, OrderItem orderItem, out ArrayList optionItemNameArray, out ProductKitItemValueCollection itemCollection )
    {
        //check product enabled
        optionItemNameArray = null;
        itemCollection = ProductKitItemValueCollection.Null;

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

        ArrayList productKitArray = new ArrayList();
        ArrayList productKitCountArray = new ArrayList();
        ArrayList productKitGroupIDArray = new ArrayList();
        if (product.IsProductKit)
        {
            if (!IsProductKitValid( product, orderItem, StoreContext.Culture, new StoreRetriever().GetCurrentStoreID(), out productKitArray, out productKitCountArray, out productKitGroupIDArray ))
            {
                StoreContext.ClearCheckoutSession();
                string message = "<p class=\"ErrorHeader\">[$ProductKit is inactive]</p>";
                ErrorMessage( message );
                return false;
            }

            itemCollection = GenerateProductKitItemValueCollection( product, orderItem, productKitArray, productKitCountArray, productKitGroupIDArray );
        }

        ProductSubscription subscriptionItem = new ProductSubscription( product.ProductID );

        //check customer can subscription this product
        if (subscriptionItem.IsSubscriptionProduct())
        {

            if (CustomerSubscription.IsContainsProductSubscriptionHigherLevel(
                subscriptionItem.ProductSubscriptions[0].SubscriptionLevelID,
                DataAccessContextDeluxe.CustomerSubscriptionRepository.GetCustomerSubscriptionsByCustomerID( StoreContext.Customer.CustomerID ) ))
            {
                StoreContext.ClearCheckoutSession();
                string message = "<p class=\"ErrorHeader\">[$Cannot subscription]</p>";
                ErrorMessage( message );
                return false;
            }
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

            if (productCount > 1)
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
            ProductKitItemValueCollection itemCollection = new ProductKitItemValueCollection();

            if (!IsProductIsValid( product, orderItem, out optionItemNameArray, out itemCollection ))
                return;

            string optionItemIDs = string.Empty;
            for (int i = 0; i < productOptionGroup.Count; i++)
            {
                optionItemIDs += GetOptionItemIDsFromItemName( productOptionGroup[i].OptionGroup, optionItemNameArray );
            }

            if (!ReOrderToCart( product, orderItem, optionItemIDs, itemCollection ))
                return;
        }

        Response.Redirect( "ShoppingCart.aspx" );
    }

    private int GetProductQuantity( Product product, OrderItem orderItem )
    {
        if (product.MinQuantity > orderItem.Quantity)
        {
            return product.MinQuantity;
        }
        else if ((product.MaxQuantity < orderItem.Quantity) && (product.MaxQuantity != 0))
        {
            return product.MaxQuantity;
        }
        else
        {
            return orderItem.Quantity;
        }
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
