using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Vevo;
using Vevo.Domain.Marketing;
using Vevo.Deluxe.Domain.BundlePromotion;

public partial class Admin_Components_PromotionGroupImage : AdminAdvancedBaseUserControl
{
    private const string _pathUpload = "Images/PromotionGroup/";

    private void ShowPromotionImage()
    {
        if (uxPromotionImageText.Text != "")
        {
            uxPromotionImage.Visible = true;
            uxPromotionImage.ImageUrl = "~/" + uxPromotionImageText.Text;
        }
        else
            uxPromotionImage.Visible = false;
    }

    public void PopulateControls( PromotionGroup promotionGroup )
    {
        uxPromotionImageText.Text = promotionGroup.ImageFile;
    }

    public PromotionGroup SetupImage( PromotionGroup promotionGroup )
    {
        promotionGroup.ImageFile = uxPromotionImageText.Text;
        return promotionGroup;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxPromotionImageUpload.PathDestination = _pathUpload;
        uxPromotionImageUpload.ReturnTextControlClientID = uxPromotionImageText.ClientID;
        if (!IsAdminModifiable())
        {
            uxPrimaryUploadLinkButton.Visible = false;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        ShowPromotionImage();
    }

    protected void uxPrimaryUploadLinkButton_Click( object sender, EventArgs e )
    {
        uxPromotionImageUpload.ShowControl = true;
        uxPrimaryUploadLinkButton.Visible = false;
    }

    public void ClearInputField()
    {
        uxPromotionImageText.Text = "";
        ShowPromotionImage();
    }
}
