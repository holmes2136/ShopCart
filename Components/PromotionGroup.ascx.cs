using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebUI.International;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.WebUI.Base;

public partial class Components_PromotionGroup : BaseDeluxeLanguageUserControl
{
    private void PopulateControls()
    {
        DataTable promotionGroupList =
            DataAccessContextDeluxe.PromotionGroupRepository.GetRandomPromotionGroupList(
            StoreContext.Culture,
            DataAccessContext.Configurations.GetIntValue("BundlePromotionShow"),
            StoreContext.CurrentStore.StoreID,
            BoolFilter.ShowTrue);

        if (promotionGroupList.Rows.Count > 0)
        {
            uxList.DataSource = promotionGroupList;
            uxList.DataBind();
        }
        else
        {
            uxList.Visible = false;
        }
    }

    private void PromotionGroup_StoreCultureChanged(object sender, CultureEventArgs e)
    {
        PopulateControls();
    }

    private void PromotionGroup_StoreCurrencyChanged(object sender, CurrencyEventArgs e)
    {
        PopulateControls();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler(PromotionGroup_StoreCultureChanged);
        GetStorefrontEvents().StoreCurrencyChanged +=
            new StorefrontEvents.CurrencyEventHandler(PromotionGroup_StoreCurrencyChanged);
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!DataAccessContext.Configurations.GetBoolValue("EnableBundlePromo"))
        {
            this.Visible = false;
        }
        else
        {
            if (!IsPostBack)
                PopulateControls();
        }
    }


    protected string CreateShortDescritpion(object shortDesc)
    {
        string shortDescription = ConvertUtilities.ToString(shortDesc);

        if (shortDescription.Length > 120)
        {
            string subDescription = shortDescription.Substring(0, 119);
            return subDescription;
        }

        return shortDescription;
    }

    protected bool IsShowSeeMore(object shortDesc)
    {
        string shortDescription = ConvertUtilities.ToString(shortDesc);

        return shortDescription.Length > 119;
    }

    protected static bool IsCatalogMode()
    {
        if (DataAccessContext.Configurations.GetBoolValue("IsCatalogMode"))
            return true;
        else
            return false;
    }

    protected bool IsAuthorizedToViewPrice()
    {
        if (DataAccessContext.Configurations.GetBoolValue("PriceRequireLogin") && !Page.User.Identity.IsAuthenticated)
            return false;

        return true;
    }

    protected bool IsTellFriendVisible()
    {
        return DataAccessContext.Configurations.GetBoolValue("TellAFriendEnabled");
    }

    public string GetImageUrl(object promotionGroupID)
    {
        string imageUrl = "";
        PromotionGroup promotionGroup = DataAccessContextDeluxe.PromotionGroupRepository.GetOne(
            StoreContext.Culture,
            StoreContext.CurrentStore.StoreID,
            ConvertUtilities.ToString(promotionGroupID),
            BoolFilter.ShowAll);
        if (String.IsNullOrEmpty(promotionGroup.ImageFile) || promotionGroup.ImageFile.Equals("~/"))
        {
            imageUrl = "~/" + DataAccessContext.Configurations.GetValue("DefaultImageUrl");
        }
        else
        {
            imageUrl = "~/" + promotionGroup.ImageFile;
        }

        return imageUrl;
    }
}
