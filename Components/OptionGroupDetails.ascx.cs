using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;

public partial class Components_OptionGroupDetails
    : Vevo.WebUI.International.BaseLanguageUserControl,
    Vevo.WebUI.Products.IOptionGroupDetailsControl
{
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
        if (!IsPostBack)
        {
            PopulateControls();
        }

        if (!String.IsNullOrEmpty( _groupName ))
            SetGroupName();

        if (!(uxOptionDataList.Items.Count > 0))
            uxTitlePanel.Visible = false;
    }


    private void Components_OptionGroupDetails_DataBinding( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private void SetGroupName()
    {
        foreach (DataListItem item in uxOptionDataList.Items)
        {
            Components_OptionItemDetails uxOptionItemDetails =
                (Components_OptionItemDetails) item.FindControl( "uxOptionItemDetails" );
            uxOptionItemDetails.SetValidGroup( _groupName );
        }
    }

    public void PopulateControls()
    {
        //DataView dv = SetUpDataSource();
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
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, new StoreRetriever().GetCurrentStoreID() );
        List<OptionGroup> optionGroups = ConvertToOptionGroups( product );

        optionGroups.Sort( new OptionGroupComparer() );

        return optionGroups;
    }


    protected void uxOptionDataList_ItemDataBound( object sender, DataListItemEventArgs e )
    {
        Components_OptionItemDetails details =
            (Components_OptionItemDetails) e.Item.FindControl( "uxOptionItemDetails" );
        details.CultureID = CultureID;
        details.ProductID = ProductID;
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

    public OptionItemValueCollection GetSelectedOptions()
    {
        // loop for each option in datalist
        // get selected option for each option
        //string[] result = new string[uxOptionDataList.Items.Count];
        OptionItemValueCollection optionSelected = new OptionItemValueCollection();

        for (int i = 0; i < uxOptionDataList.Items.Count; i++)
        {
            Components_OptionItemDetails details =
                (Components_OptionItemDetails) uxOptionDataList.Items[i].FindControl( "uxOptionItemDetails" );

            foreach (OptionItemValue item in details.GetSelectedItem())
            {
                optionSelected.Add( item );
            }
        }

        return optionSelected;
    }

    public bool IsShowStock
    {
        get
        {
            bool result = true;
            foreach (DataListItem item in uxOptionDataList.Items)
            {
                Components_OptionItemDetails optionItemDetails =
                        (Components_OptionItemDetails) item.FindControl( "uxOptionItemDetails" );
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
                Components_OptionItemDetails optionItemDetails =
                    (Components_OptionItemDetails) item.FindControl( "uxOptionItemDetails" );
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
