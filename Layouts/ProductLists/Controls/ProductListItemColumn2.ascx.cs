using System;
using System.Text.RegularExpressions;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI.Ajax;
using Vevo.WebUI.Products;

public partial class Components_ProductListItemColumn2 : BaseProductListItemUserControl
{
    #region Private

    private void uxAddtoWishListButton_BubbleEvent(object sender, EventArgs e)
    {
        ProcessAddToWishListButton(uxAddtoWishListButton, uxAddtoWishListButton.ProductID);
    }

    private void uxAddtoCompareListButton_BubbleEvent(object sender, EventArgs e)
    {
        uxAddtoCompareListButton.AddItemToCompareListCart(uxAddtoCompareListButton.ProductID);
        Response.Redirect("ComparisonList.aspx");
    }

    private string RemoveHTML(string strHTML)
    {
        return Regex.Replace(strHTML, "<(.|\n)*?>", "");
    }

    #endregion


    #region Protected

    protected void Page_Load(object sender, EventArgs e)
    {
        uxAddtoWishListButton.BubbleEvent += new EventHandler(uxAddtoWishListButton_BubbleEvent);
        uxAddtoCompareListButton.BubbleEvent += new EventHandler(uxAddtoCompareListButton_BubbleEvent);
        OptionGroupDetailsParent = uxOptionGroupPanel;
        AddToCartNotificationParent = uxAddToCartNotification;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!DataAccessContext.Configurations.GetBoolValue("TellAFriendEnabled"))
            uxTellFriendLinkButton.Visible = false;
    }

    protected string LimitDisplayCharactor(object description, short characterLimit)
    {
        string shortContent = RemoveHTML(description.ToString());

        if (shortContent.Length > characterLimit)
        {
            string tempString = shortContent.Substring(0, characterLimit).Trim();

            int trimOffset = tempString.LastIndexOf(" ");

            if (trimOffset > 0)
            {
                shortContent = tempString.Substring(0, trimOffset);
            }

            shortContent += " ...";
        }

        return shortContent;
    }

    #endregion

}
