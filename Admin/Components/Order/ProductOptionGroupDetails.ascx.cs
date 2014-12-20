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
using Vevo.Domain.Products;
using System.Collections.Generic;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using Vevo.Domain.Orders;

public partial class Admin_Components_Order_ProductOptionGroupDetails : AdminAdvancedBaseUserControl,
    Vevo.WebUI.Products.IOptionGroupDetailsControl
{
    private OptionItemValueCollection _selectedValue = new OptionItemValueCollection();
    private class OptionGroupComparer : IComparer<OptionGroup>
    {
        private int GetOptionGroupSortOrder( OptionGroup optionGroup )
        {
            switch (optionGroup.Type)
            {
                case OptionGroup.OptionGroupType.DropDown:
                    return 1;
                case OptionGroup.OptionGroupType.Radio:
                    return 2;
                case OptionGroup.OptionGroupType.Text:
                    return 3;
                case OptionGroup.OptionGroupType.InputList:
                    return 4;
                case OptionGroup.OptionGroupType.Upload:
                    return 5;
                case OptionGroup.OptionGroupType.UploadRequired:
                    return 6;
                default:
                    return 0;
            }
        }

        public int Compare( OptionGroup x, OptionGroup y )
        {
            int xOrder = GetOptionGroupSortOrder( x );
            int yOrder = GetOptionGroupSortOrder( y );

            return xOrder.CompareTo( yOrder );
        }
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        DataBinding += new EventHandler( Components_OptionGroupDetails_DataBinding );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }


    private void Components_OptionGroupDetails_DataBinding( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private void SetGroupName()
    {
        foreach (DataListItem item in uxOptionDataList.Items)
        {
            Admin_Components_Order_ProductOptionItemDetails uxOptionItemDetails =
                (Admin_Components_Order_ProductOptionItemDetails) item.FindControl( "uxOptionItemDetails" );
            uxOptionItemDetails.SetValidGroup( _groupName );
        }
    }

    public void PopulateControls()
    {
        PopulateControls( new OptionItemValueCollection() );
    }


    public void PopulateControls( OptionItemValueCollection selectedValue )
    {
        _selectedValue = selectedValue;
        uxOptionDataList.DataSource = GetOptionGroups();
        uxOptionDataList.DataBind();
    }

    private List<OptionGroup> ConvertToOptionGroups( Product product )
    {
        List<OptionGroup> list = new List<OptionGroup>();

        foreach (ProductOptionGroup productOptionGroup in product.ProductOptionGroups)
            list.Add( productOptionGroup.OptionGroup );

        return list;
    }

    private IList<OptionGroup> GetOptionGroups()
    {
        Product product = DataAccessContext.ProductRepository.GetOne(
            DataAccessContext.CultureRepository.GetOne( CultureID ), ProductID, StoreID );
        List<OptionGroup> optionGroups = ConvertToOptionGroups( product );

        optionGroups.Sort( new OptionGroupComparer() );

        return optionGroups;
    }


    protected void uxOptionDataList_ItemDataBound( object sender, DataListItemEventArgs e )
    {
        Admin_Components_Order_ProductOptionItemDetails details =
            (Admin_Components_Order_ProductOptionItemDetails) e.Item.FindControl( "uxOptionItemDetails" );
        details.CultureID = CultureID;
        details.Culture = DataAccessContext.CultureRepository.GetOne( CultureID );
        details.ProductID = ProductID;
        details.StoreID = StoreID;
        details.CurrencyCode = CurrencyCode;
        details.SelectedItemValue = _selectedValue;
        details.SetValidGroup( _groupName );
        details.PopulateControls();
    }


    public string ProductID
    {
        get
        {
            if (ViewState["ProductID"] == null)
                return "0";
            else
                return (string) ViewState["ProductID"];
        }
        set
        {
            ViewState["ProductID"] = value;
        }
    }

    public string CultureID
    {
        get
        {
            if (ViewState["CultureID"] == null)
                return CultureUtilities.StoreCultureID;
            else
                return (string) ViewState["CultureID"];
        }
        set
        {
            ViewState["CultureID"] = value;
        }
    }

    public string StoreID
    {
        get
        {
            if (ViewState["StoreID"] == null)
                return Store.RegularStoreID;
            else
                return (string) ViewState["StoreID"];
        }
        set
        {
            ViewState["StoreID"] = value;
        }
    }

    public string CurrencyCode
    {
        get
        {
            if (ViewState["CurrencyCode"] == null)
                return DataAccessContext.CurrencyRepository.GetOne(
                    DataAccessContext.Configurations.GetValueNoThrow( "DefaultDisplayCurrencyCode",
                    DataAccessContext.StoreRepository.GetOne( StoreID ) ) ).CurrencyCode;
            else
                return (string) ViewState["CurrencyCode"];
        }
        set
        {
            ViewState["CurrencyCode"] = value;
        }
    }


    public OptionItemValueCollection GetSelectedOptions()
    {
        // loop for each option in datalist
        // get selected option for each option
        //string[] result = new string[uxOptionDataList.Items.Count];
        OptionItemValueCollection optionSelected = new OptionItemValueCollection();

        for (int i = 0; i < uxOptionDataList.Items.Count; i++)
        {
            Admin_Components_Order_ProductOptionItemDetails details =
                (Admin_Components_Order_ProductOptionItemDetails) uxOptionDataList.Items[i].FindControl( "uxOptionItemDetails" );


            foreach (OptionItemValue item in details.GetSelectedItem())
            {
                optionSelected.Add( item );
            }
        }

        return optionSelected;
    }

    public void SetEnableControl( bool enable )
    {
        uxOptionDataList.Enabled = enable;
    }

    public bool IsShowStock
    {
        get
        {
            bool result = true;
            foreach (DataListItem item in uxOptionDataList.Items)
            {
                Admin_Components_Order_ProductOptionItemDetails optionItemDetails =
                        (Admin_Components_Order_ProductOptionItemDetails) item.FindControl( "uxOptionItemDetails" );
                if (optionItemDetails.IsOptionUseStock())
                    result = false;
            }
            return result;
        }
    }

    public bool IsValidInput
    {
        get
        {
            bool result = true;
            foreach (DataListItem item in uxOptionDataList.Items)
            {
                Admin_Components_Order_ProductOptionItemDetails optionItemDetails =
                    (Admin_Components_Order_ProductOptionItemDetails) item.FindControl( "uxOptionItemDetails" );
                if (!optionItemDetails.IsValidInput())
                    result = false;
            }
            return result;
        }
    }

    private string _groupName;

    public string ValidGroup
    {
        set
        {
            _groupName = value;
        }
    }
}
