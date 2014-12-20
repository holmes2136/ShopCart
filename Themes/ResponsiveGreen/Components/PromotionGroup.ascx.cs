using System;
using System.Data;
using System.Web.UI;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.WebUI.Base;
using Vevo.Domain;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Themes_ResponsiveGreen_Components_PromotionGroup : BaseDeluxeLanguageUserControl
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
            
            RegisterScript();
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


    // Setup view more button

    private void RegisterScript()
    {
        String csname = "ShowViewButton";
        ClientScriptManager cs = Page.ClientScript;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("function ShowViewButton(panel,buttonlink) {");
        sb.AppendLine("    document.getElementById(panel).style.display = 'block';");
        sb.AppendLine("    document.getElementById(buttonlink).style.display = 'block';");
        sb.AppendLine(" } ");


        if (!cs.IsClientScriptBlockRegistered(this.GetType(), csname))
        {
            cs.RegisterClientScriptBlock(this.GetType(), csname, sb.ToString(), true);
        }
    }
    protected void uxList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Panel btnOverImage = (Panel)e.Item.FindControl("uxImagePanel");
        Panel btnPanel = (Panel)e.Item.FindControl("uxQuickViewButtonPanel");

        HyperLink viewLink = (HyperLink)e.Item.FindControl("uxPromotionDetailLink");
        btnOverImage.Attributes.Add("onmouseover", "ShowViewButton(" +
            "'" + btnPanel.ClientID + "'," +
            "'" + viewLink.ClientID + "');");
        btnOverImage.Attributes.Add("onmouseout", "document.getElementById('" + btnPanel.ClientID + "').style.display='none'");
        
    }
}

