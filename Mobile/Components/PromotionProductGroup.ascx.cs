using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.WebUI.International;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.Domain;

public partial class Mobile_Components_PromotionProductGroup : BaseLanguageUserControl
{
    private IList<PromotionProduct> promoProductList;

    private void UpdateProductItem( object sender )
    {
        Mobile_Components_PromotionProductItem selectedItem = (Mobile_Components_PromotionProductItem) sender;
        uxSelectedProductHidden.Value = selectedItem.ProductID + ":" + selectedItem.GetProductOption;

        foreach (DataListItem item in uxList.Items)
        {
            if (item.FindControl( "uxProductItem" ) != null)
            {
                Mobile_Components_PromotionProductItem productItem = (Mobile_Components_PromotionProductItem) item.FindControl( "uxProductItem" );

                if (!selectedItem.ProductID.Equals( productItem.ProductID ))
                {
                    productItem.CheckedRadio = false;
                }
            }
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

    public string GetSelectedOption
    {
        get
        {
            return uxSelectedProductHidden.Value;
        }
    }

    public bool IsSelectedProduct
    {
        get
        {
            bool isSuccess = true;
            uxErrorMessagePanel.Visible = false;

            if (String.IsNullOrEmpty( uxSelectedProductHidden.Value ))
            {
                uxErrorMessagePanel.Visible = true;
                uxMessageSelect.Visible = true;
                uxMessageOption.Visible = false;
                isSuccess = false;
            }

            if (uxSelectedProductHidden.Value.Contains( "Error" ))
            {
                uxErrorMessagePanel.Visible = true;
                uxMessageSelect.Visible = false;
                uxMessageOption.Visible = true;
                isSuccess = false;
            }

            return isSuccess;
        }
    }

    public void Reload()
    {
        foreach (DataListItem item in uxList.Items)
        {
            if (item.FindControl( "uxProductItem" ) != null)
            {
                Mobile_Components_PromotionProductItem productItem = (Mobile_Components_PromotionProductItem) item.FindControl( "uxProductItem" );
                productItem.Reload();
            }
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            uxErrorMessagePanel.Visible = false;
            promoProductList = DataAccessContextDeluxe.PromotionProductRepository.GetProductList( PromotionSubGroupID, "SortOrder" );

            IList<PromotionProduct> newList = new List<PromotionProduct>();
            foreach (PromotionProduct product in promoProductList)
            {
                newList.Add( product );
            }

            uxList.DataSource = newList;
            uxList.DataBind();
        }
    }
    protected void uxList_OnItemDataBound( object sender, DataListItemEventArgs e )
    {
        if (e.Item.ItemIndex == DataAccessContextDeluxe.PromotionProductRepository.GetProductList( PromotionSubGroupID, "SortOrder" ).Count - 1)
        {
            Mobile_Components_PromotionProductItem productItem = (Mobile_Components_PromotionProductItem) e.Item.FindControl( "uxProductItem" );
            productItem.SetNoBorderBottomCss();
        }
    }


    protected void uxProductItem_ProductCheckedChanged( object sender, EventArgs e )
    {
        UpdateProductItem( sender );
    }

    protected void uxProductItem_ProductOptionChanged( object sender, EventArgs e )
    {
        UpdateProductItem( sender );
    }
}
