using System;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.WebUI.International;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.Domain;

public partial class Components_PromotionProductItem : BaseLanguageUserControl
{
    private Product product;
    private PromotionProduct promoProduct;
    public event EventHandler ProductCheckedChanged;

    private string StoreID
    {
        get
        {
            return StoreContext.CurrentStore.StoreID;
        }
    }

    public string ProductID
    {
        get
        {
            if (ViewState["ProductID"] == null)
                return "0";
            else
                return (string) ViewState["ProductID"];
        }
        set
        {
            ViewState["ProductID"] = value;
        }
    }

    public string PromotionSubGroupID
    {
        get
        {
            if (ViewState["PromotionSubGroupID"] == null)
                return "0";
            else
                return (string) ViewState["PromotionSubGroupID"];
        }
        set
        {
            ViewState["PromotionSubGroupID"] = value;
        }
    }

    public bool CheckedRadio
    {
        get
        {
            return uxProductSelect.Checked;
        }
        set
        {
            uxProductSelect.Checked = value;
        }
    }

    public string ProductOptionIDs
    {
        get
        {
            if (!String.IsNullOrEmpty( uxOptionHidden.Value ))
                return uxOptionHidden.Value;
            else
                return uxOptionItemHiddenText.Text;
        }
        set
        {
            uxOptionHidden.Value = value;
        }
    }

    public void Reload()
    {
        PopulateControl();
    }

    private void PopulateControl()
    {
        product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, StoreID );
        promoProduct = DataAccessContextDeluxe.PromotionProductRepository.GetOne( PromotionSubGroupID, ProductID );

        ProductImage image = product.GetPrimaryProductImage();
        uxProductImage.ImageUrl = "~/" + image.ThumbnailImage;

        uxNameText.Text = product.Name;
        uxNameLink.NavigateUrl = Vevo.UrlManager.GetProductUrl( product.ProductID, product.UrlName );
        uxImageLink.NavigateUrl = Vevo.UrlManager.GetProductUrl( product.ProductID, product.UrlName );
        uxQuantityText.Text = promoProduct.Quantity.ToString();
        uxPriceText.Text = StoreContext.Currency.FormatPrice( product.GetProductPrice( StoreID ).Price );

        if (IsAuthorizedToViewPrice())
            uxPriceText.Visible = true;
        else
            uxPriceText.Visible = false;

        if (String.IsNullOrEmpty( promoProduct.OptionItemID ))
        {
            uxFixOptionPanel.Visible = false;
            uxPopupPanel.Visible = false;
        }
        else if (promoProduct.OptionItemID.Equals( "0" ))
        {
            uxFixOptionPanel.Visible = false;
            uxPopupPanel.Visible = true;
            uxOptionGroupDetails.ProductID = ProductID;
        }
        else
        {
            uxFixOptionPanel.Visible = true;
            uxPopupPanel.Visible = false;

            string displayName = "";
            foreach (string optionItemID in promoProduct.OptionItemID.Split( ',' ))
            {
                OptionItem optionItem = DataAccessContext.OptionItemRepository.GetOne( StoreContext.Culture, optionItemID );
                OptionGroup optionGroup = DataAccessContext.OptionGroupRepository.GetOne( StoreContext.Culture, optionItem.OptionGroupID );
                displayName += optionGroup.DisplayText + ": " + optionItem.Name + "<br />";
            }

            uxFixOption.Text = displayName;
            uxOptionHidden.Value = promoProduct.OptionItemID;
            uxOptionItemHiddenText.Text = uxOptionHidden.Value;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControl();
            ProductOptionIDs = "";
            uxOptionGroupDetails.ProductID = ProductID;
            uxOptionGroupDetails.ValidGroup = ProductID;
            uxOkButton.ValidationGroup = ProductID;
        }
    }

    protected void uxProductSelect_CheckedChanged( object sender, EventArgs e )
    {
        if (!uxOptionGroupDetails.IsValidInput)
        {
            ProductOptionIDs = "Error";
        }

        if (CheckedRadio)
        {
            if (ProductCheckedChanged != null)
                ProductCheckedChanged( this, EventArgs.Empty );
        }
    }

    protected void uxOkButton_Click( object sender, EventArgs e )
    {
        if (uxOptionGroupDetails.IsValidInput)
        {
            OptionItemValueCollection selectedOptions = uxOptionGroupDetails.GetSelectedOptions();

            foreach (OptionItemValue item in selectedOptions)
            {
                ProductOptionIDs = item.OptionItemID + ",";
            }

            ProductOptionIDs = ProductOptionIDs.TrimEnd( ',' );
        }
        else
        {
            ProductOptionIDs = "Error";
        }

        if (CheckedRadio)
        {
            if (ProductCheckedChanged != null)
                ProductCheckedChanged( this, EventArgs.Empty );
        }
    }

    protected static bool IsCatalogMode()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "IsCatalogMode" ))
            return true;
        else
            return false;
    }

    protected bool IsAuthorizedToViewPrice()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "PriceRequireLogin" ) && !Page.User.Identity.IsAuthenticated)
            return false;

        return true;
    }
}
