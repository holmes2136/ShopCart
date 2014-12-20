using System;
using Vevo.Domain;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;

public partial class Components_LikeButton : System.Web.UI.UserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if (!ConvertUtilities.ToBoolean( DataAccessContext.Configurations.GetValue( "FBLikeButton" ) ))
        {
            uxFBLikeButtonFrame.Visible = false;
        }
        else
        {
            uxFBLikeButtonFrame.Attributes["src"] = GetLikeButtonCode();
            uxGplusButton.InnerHtml = GetGooglePlusCode();
        }
    }

    protected string GetLikeButtonCode()
    {
        return "https://www.facebook.com/plugins/like.php?href=" + GetProductURL() + "&send=false&layout=button_count&width=450&show_faces=false&font=arial&colorscheme=light&action=like&locale=en_US";
    }
    protected string GetProductURL()
    {
        String productURL = UrlPath.GetDisplayedUrl().Replace(":", "%3A");
        productURL = productURL.Replace("/", "%2F");
        return productURL;
    }
    protected string GetGooglePlusCode()
    {
        return "<div class=\"g-plusone\" data-size=\"medium\" data-href=\"" +  GetProductURL() + "\"></div>";
    }
}
