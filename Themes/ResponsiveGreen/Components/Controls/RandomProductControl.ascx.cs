using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.WebUI.Products;

public partial class Themes_ResponsiveGreen_Components_Controls_RandomProductControl :BaseProductListItemUserControl
{
    private string RemoveHTML(string strHTML)
    {
        return Regex.Replace(strHTML, "<(.|\n)*?>", "");
    }
    private void uxAddtoWishListButton_BubbleEvent( object sender, EventArgs e )
    {
        ProcessAddToWishListButton( uxAddtoWishListButton, uxAddtoWishListButton.ProductID );
    }

    private void uxAddtoCompareListButton_BubbleEvent( object sender, EventArgs e )
    {
        uxAddtoCompareListButton.AddItemToCompareListCart( uxAddtoCompareListButton.ProductID );
        Response.Redirect( "ComparisonList.aspx" );
    }




    protected void Page_Load( object sender, EventArgs e )
    {
        uxAddtoWishListButton.BubbleEvent += new EventHandler( uxAddtoWishListButton_BubbleEvent );
        uxAddtoCompareListButton.BubbleEvent += new EventHandler( uxAddtoCompareListButton_BubbleEvent );
        AddToCartNotificationParent = uxAddToCartNotification;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        
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

}