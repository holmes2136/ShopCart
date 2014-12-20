using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;

public partial class Components_ProductKitGroupDetails : Vevo.WebUI.International.BaseLanguageUserControl
{
    private string _groupName;

    #region Private
    private void Components_ProductKitGroupDetails_DataBinding( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private List<ProductKitGroup> ConvertToProductKitGroups( Product product )
    {
        List<ProductKitGroup> list = new List<ProductKitGroup>();
        foreach (ProductKit productKit in product.ProductKits)
        {
            ProductKitGroup kitGroup = DataAccessContext.ProductKitGroupRepository.GetOne( StoreContext.Culture, productKit.ProductKitGroupID );
            list.Add( kitGroup );
        }

        return list;
    }

    private IList<ProductKitGroup> GeProductKitGroups()
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, new StoreRetriever().GetCurrentStoreID() );
        List<ProductKitGroup> kitGroups = ConvertToProductKitGroups( product );

        return kitGroups;
    }

    #endregion

    #region Protected
    protected void Page_Load( object sender, EventArgs e )
    {
        DataBinding += new EventHandler( Components_ProductKitGroupDetails_DataBinding );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControls();
        }

        if (!(uxOptionDataList.Items.Count > 0))
            uxTitlePanel.Visible = false;
    }

    protected void uxProductKitDataList_ItemDataBound( object sender, DataListItemEventArgs e )
    {
        Components_ProductKitItemDetails details =
            (Components_ProductKitItemDetails) e.Item.FindControl( "uxProductKitItemDetails" );
        details.IsDynamicPrice = IsDynamicPrice;
        details.Culture = Culture;
        details.PopulateControls();
    }

    #endregion

    #region Public
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

    public bool IsDynamicPrice
    {
        get
        {
            if (ViewState["IsDynamicPrice"] == null)
                return DataAccessContext.ProductRepository.GetOne( Culture, ProductID, new StoreRetriever().GetCurrentStoreID() ).IsDynamicProductKitPrice;
            else
                return (bool) ViewState["IsDynamicPrice"];
        }
        set
        {
            ViewState["IsDynamicPrice"] = value;
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

    public string ValidGroup
    {
        set
        {
            _groupName = value;
        }
    }

    public void PopulateControls()
    {
        uxOptionDataList.DataSource = GeProductKitGroups();
        uxOptionDataList.DataBind();
    }

    public ProductKitItemValueCollection GetSelectedProductKitItems()
    {
        // loop for each option in datalist
        // get selected option for each option
        //string[] result = new string[uxOptionDataList.Items.Count];
        ProductKitItemValueCollection itemSelected = new ProductKitItemValueCollection();

        for (int i = 0; i < uxOptionDataList.Items.Count; i++)
        {
            Components_ProductKitItemDetails details =
                (Components_ProductKitItemDetails) uxOptionDataList.Items[i].FindControl( "uxProductKitItemDetails" );

            foreach (ProductKitItemValue item in details.GetSelectedItem())
            {
                itemSelected.Add( item );
            }
        }

        return itemSelected;
    }

    public bool IsValidInput(int mainQuantity)
    {
        bool result = true;
        foreach (DataListItem item in uxOptionDataList.Items)
        {
            Components_ProductKitItemDetails optionItemDetails =
                (Components_ProductKitItemDetails) item.FindControl( "uxProductKitItemDetails" );
            if (!optionItemDetails.IsValidInput( mainQuantity ))
                result = false;
        }
        return result;
    }


    #endregion
}
