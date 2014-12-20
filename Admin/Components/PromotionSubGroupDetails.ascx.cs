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
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.Domain;

public partial class Admin_Components_PromotionSubGroupDetails : AdminAdvancedBaseUserControl
{
    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string CurrentID
    {
        get
        {
            if (IsEditMode())
                return MainContext.QueryString["PromotionSubGroupID"];
            else
                return "0";
        }
    }

    private void PopulateControls()
    {
        if (CurrentID != null && int.Parse( CurrentID ) >= 0)
        {
            PromotionSubGroup promotionSubGroup = DataAccessContextDeluxe.PromotionSubGroupRepository.GetOne( CurrentID );

            uxPromotionalProductList.PromotionSubGroupID = CurrentID;
        }

        uxPromotionProductSelectedList.CurrentCulture = uxLanguageControl.CurrentCulture;
        uxPromotionalProductList.CurrentCulture = uxLanguageControl.CurrentCulture;
    }

    private void Button_RefreshHandler( object sender, EventArgs e )
    {
        uxPromotionSubGroupTab.ActiveTabIndex = 1;
    }

    private void Grid_RefreshHandler( object sender, EventArgs e )
    {
        uxPromotionSubGroupTab.ActiveTabIndex = 0;
        uxPromotionProductSelectedList.UpdateLanguage();
        uxPromotionProductSelectedList.AddSuccessMessage();
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
        uxPromotionProductSelectedList.UpdateLanguage();
        uxPromotionalProductList.UpdateCategoryLanguage();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxLanguageControl.BubbleEvent += new EventHandler( Details_RefreshHandler );
        uxPromotionalProductList.BubbleEvent += new EventHandler( Grid_RefreshHandler );
        uxPromotionProductSelectedList.BubbleEvent += new EventHandler( Button_RefreshHandler );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            if (IsEditMode())
            {
                PopulateControls();
                //uxAddButton.Visible = false;

                if (IsAdminModifiable())
                {
                    //uxUpdateButton.Visible = true;
                }
                else
                {
                    //uxUpdateButton.Visible = false;
                }
            }
            else
            {
                if (IsAdminModifiable())
                {
                    //uxAddButton.Visible = true;
                    //uxUpdateButton.Visible = false;
                }
                else
                {
                    MainContext.RedirectMainControl( "PromotionSubGroupList.ascx" );
                }
            }
        }
    }

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }

    //protected void uxPromotionSubGroupTab_OnActiveTabChanged( object sender, EventArgs e )
    //{
    //    PopulateControls();
    //    uxPromotionProductSelectedList.UpdateLanguage();
    //}
}
