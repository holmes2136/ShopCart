using System;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using Vevo.WebUI.Ajax;
using Vevo.WebUI.International;
using Vevo.WebUI.Orders;

public partial class Components_AddToWishListButton : BaseLanguageUserControl, IAddToWishListControl
{
    private void AddToWishList( string productID, OptionItemValueCollection selectedOptions, string quantity )
    {
        Product product = DataAccessContext.ProductRepository.GetOne(
            StoreContext.Culture,
            productID,new StoreRetriever().GetCurrentStoreID() );

        if (selectedOptions == null || selectedOptions.Count == 0)
        {
            StoreContext.WishList.Cart.AddItem( product, int.Parse( quantity ) );
        }
        else
        {
            OptionItemValueCollection inputLists = selectedOptions.GetOptionItemsByGroupType(
                OptionGroup.OptionGroupType.InputList );
            if (inputLists.Count == 0)
            {
                StoreContext.WishList.Cart.AddItem( product, int.Parse( quantity ), selectedOptions );
            }
            else
            {
                OptionItemValueCollection optionsWithoutInputList =
                    selectedOptions.GetOptionItemsNotInGroupType( OptionGroup.OptionGroupType.InputList );
                //add product is Loop.
                foreach (OptionItemValue optionInput in inputLists)
                {
                    OptionItemValueCollection options = optionsWithoutInputList.Clone();
                    options.Add( optionInput );

                    StoreContext.WishList.Cart.AddItem( product, int.Parse( optionInput.Details ), options );
                }
            }
        }

        DataAccessContext.CartRepository.UpdateWhole( StoreContext.WishList.Cart );
    }

    private void AddToWishList( string productID, OptionItemValueCollection selectedOptions )
    {
        AddToWishList( productID, selectedOptions, "1" );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "WishListEnabled" ))
        {
            uxAddWishListImageButton.Visible = false;
        }
        else
        {
            AjaxUtilities.GetScriptManager( this ).RegisterAsyncPostBackControl( uxAddWishListImageButton );
            
            if (!String.IsNullOrEmpty(CssClass))
            uxAddWishListImageButton.CssClass = CssClass;
            uxAddWishListImageButton.Text = Text;
    	}
    }

    public string ValidationGroup
    {
        set { uxAddWishListImageButton.ValidationGroup = value; }
    }

    public string CssClass
    {
        get
        {
            if (ViewState["CssClass"] == null)
                return "";
            return ViewState["CssClass"].ToString();
        }
        set
        {
            ViewState["CssClass"] = value;
        }
    }

    public string ProductID
    {
        get
        {
            if (ViewState["ProductID"] == null)
                return "";
            return ViewState["ProductID"].ToString();
        }
        set
        {
            ViewState["ProductID"] = value;
        }
    }
    public string Text
    {
        get
        {
            if (ViewState["Text"] == null)
                return "[$BtnAddtoWishlist]";
            return ViewState["Text"].ToString();
        }
        set
        {
            ViewState["Text"] = value;
        }
    }
    public void AddItemToWishListCart( string productID, OptionItemValueCollection selectedOptions, string quantity )
    {
        AddToWishList( productID, selectedOptions, quantity );
    }

    protected void uxAddWishListImageButton_Click( object sender, EventArgs e )
    {
        OnBubbleEvent( e );
    }

    public void AddItemToWishListCart( string productID, OptionItemValueCollection selectedOptions )
    {
        AddToWishList( productID, selectedOptions );
    }
}
