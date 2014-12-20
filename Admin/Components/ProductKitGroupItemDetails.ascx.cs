using System;
using Vevo;

public partial class Admin_Components_ProductKitGroupItemDetails : AdminAdvancedBaseUserControl
{
    private string CurrentID
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["ProductKitGroupID"] ))
                return MainContext.QueryString["ProductKitGroupID"];
            else
                return "0";
        }
    }

    private void PopulateControls()
    {
        if (CurrentID != null && int.Parse( CurrentID ) >= 0)
        {
            uxProductKitProductList.ProductKitGroupID = CurrentID;
        }

        uxProductKitGroupItemSeletedList.CurrentCulture = uxLanguageControl.CurrentCulture;
        uxProductKitProductList.CurrentCulture = uxLanguageControl.CurrentCulture;
    }

    private void Button_RefreshHandler( object sender, EventArgs e )
    {
        uxPromotionSubGroupTab.ActiveTabIndex = 1;
    }

    private void Grid_RefreshHandler( object sender, EventArgs e )
    {
        uxPromotionSubGroupTab.ActiveTabIndex = 0;
        uxProductKitGroupItemSeletedList.UpdateLanguage();
        uxProductKitGroupItemSeletedList.AddSuccessMessage();
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
        uxProductKitGroupItemSeletedList.UpdateLanguage();
        uxProductKitProductList.UpdateCategoryLanguage();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxLanguageControl.BubbleEvent += new EventHandler( Details_RefreshHandler );
        uxProductKitProductList.BubbleEvent += new EventHandler( Grid_RefreshHandler );
        uxProductKitGroupItemSeletedList.BubbleEvent += new EventHandler( Button_RefreshHandler );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateControls();
        }
    }
}
