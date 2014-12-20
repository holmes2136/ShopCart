using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.WebUI;
using Vevo.Domain;
using Vevo.Domain.Products;
using System.Collections;
using Vevo.Domain.Stores;
using Vevo.Domain.Orders;

public partial class Components_ProductKitItemDetails : Vevo.WebUI.International.BaseLanguageUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {

    }

    public void PopulateControls()
    {
        ProductKitGroup productKitGroup = DataAccessContext.ProductKitGroupRepository.GetOne( Culture, ProductKitGroupID );
        IList<ProductKitItem> productKitItemLists = DataAccessContext.ProductKitGroupRepository.GetProductKitItems( ProductKitGroupID, Culture );

        IList<ProductKitItemDisplay> productKitItemDisplayList = new List<ProductKitItemDisplay>();

        switch (productKitGroup.Type)
        {
            case ProductKitGroup.ProductKitGroupType.Radio:
                RadioTR.Visible = true;
                uxProductKitRadioItem.SetupRadio( productKitGroup, Culture, new StoreRetriever().GetCurrentStoreID(), IsDynamicPrice );
                break;
            case ProductKitGroup.ProductKitGroupType.DropDown:
                DropdownTR.Visible = true;
                uxProductKitDropDownItem.SetupInputList( productKitGroup, Culture, new StoreRetriever().GetCurrentStoreID(), IsDynamicPrice );
                break;
            case ProductKitGroup.ProductKitGroupType.Checkbox:
                CheckboxTR.Visible = true;
                uxProductKitCheckboxItem.SetupInputList( productKitGroup, Culture, new StoreRetriever().GetCurrentStoreID(), IsDynamicPrice );
                break;
        }

    }

    public bool IsDynamicPrice
    {
        get
        {
            if (ViewState["IsDynamicPrice"] == null)
                return false;
            else
                return (bool) ViewState["IsDynamicPrice"];
        }
        set
        {
            ViewState["IsDynamicPrice"] = value;
        }
    }

    public string ProductKitGroupID
    {
        get
        {
            if (ViewState["ProductKitGroupID"] == null)
                return "0";
            else
                return (string) ViewState["ProductKitGroupID"];
        }
        set
        {
            ViewState["ProductKitGroupID"] = value;
        }
    }

    public Culture Culture
    {
        get
        {
            if (ViewState["Culture"] == null)
                return StoreContext.Culture;
            else
                return (Culture) ViewState["Culture"];
        }
        set
        {
            ViewState["Culture"] = value;
        }
    }

    public bool IsValidInput(int mainQuantity)
    {
        if (RadioTR.Visible)
        {
            if (!uxProductKitRadioItem.ValidateInput()) 
                return false;
            if (!uxProductKitRadioItem.IsValidStock( mainQuantity ))
                return false;
        }

        if (DropdownTR.Visible)
        {
            if (!uxProductKitDropDownItem.ValidateInput()) 
                return false;
            if (!uxProductKitDropDownItem.IsValidStock( mainQuantity ))
                return false;
        }

        if (CheckboxTR.Visible)
        {
            if (!uxProductKitCheckboxItem.ValidateInput()) 
                return false;
            if (!uxProductKitCheckboxItem.IsValidStock( mainQuantity ))
                return false;
        }

        return true;
    }

    public ProductKitItemValue[] GetSelectedItem()
    {
        ArrayList selectedList = new ArrayList();

        if (RadioTR.Visible)
        {
            uxProductKitRadioItem.CreateProductKitItems( selectedList );
        }

        if (DropdownTR.Visible)
        {
            uxProductKitDropDownItem.CreateProductKitItems( selectedList );
        }

        if (CheckboxTR.Visible)
        {
            uxProductKitCheckboxItem.CreateProductKitItems( selectedList );
        }

        ProductKitItemValue[] result = new ProductKitItemValue[selectedList.Count];
        selectedList.CopyTo( result );

        return result;
    }
}
