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
using Vevo.WebUI;
using Vevo.Domain.Products;
using System.Collections.Generic;
using Vevo.Domain;
using Vevo.Domain.Stores;
using Vevo;
using Vevo.Shared.Utilities;
using System.IO;
using System.Drawing;
using Vevo.Domain.Users;

public partial class ComparisonListPopup : Vevo.WebUI.International.BaseLanguagePage
{

    private IList<Product> GetProductList()
    {
        ArrayList list = StoreContext.ProductIDsCompareList;
        if (UserUtilities.GetCurrentCustomerID() != "0")
        {
            Customer currentUser = DataAccessContext.CustomerRepository.GetOne( UserUtilities.GetCurrentCustomerID() );
            list = currentUser.GetProductIDsCompare();
            if (StoreContext.ProductIDsCompareList.Count > 0)
            {
                ArrayList sessionList = StoreContext.ProductIDsCompareList;
                foreach (string id in sessionList)
                {
                    if (!list.Contains( id ))
                        list.Add( id );
                }
            }


            currentUser.SetProductIDsCompare( list );
            DataAccessContext.CustomerRepository.Save( currentUser );
        }

        int itemShow = DataAccessContext.Configurations.GetIntValue( "CompareProductShow" );
        IList<Product> displayList = new List<Product>();
        displayList.Add( Product.Null );
        int itemCount = 0;

        foreach (string id in list)
        {
            Product product = DataAccessContext.ProductRepository.GetOne(
                StoreContext.Culture, id, new StoreRetriever().GetStore().StoreID );

            if (!product.IsNull && IsExitsInCurrentStore( product ) && itemCount < itemShow)
            {
                displayList.Add( product );
                itemCount++;
            }
            else
            {
                return displayList;
            }
        }


        return displayList;
    }

    private bool IsExitsInCurrentStore( Product product )
    {
        string rootID = DataAccessContext.Configurations.GetValue(
                "RootCategory",
                DataAccessContext.StoreRepository.GetOne( DataAccessContext.StoreRetriever.GetCurrentStoreID() ) );

        return product.IsAvailable( rootID );
    }

    private void PopulateProductControls()
    {
        if (this.Visible)
            Refresh();
    }

    private void RegisterClientScriptLibraries()
    {
        ScriptManager.RegisterClientScriptInclude( Page, typeof( Page ), "ForCallOut", "ClientScripts/Utilities.js" );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterClientScriptLibraries();
        PopulateProductControls();
        uxList.RepeatDirection = RepeatDirection.Horizontal;
        uxList.Visible = true;
    }

    public ArrayList GetSpecificationItemList( IList<Product> productList )
    {
        ArrayList result = new ArrayList();

        foreach (Product product in productList)
        {
            IList<ProductSpecification> items = product.ProductSpecifications;
            foreach (ProductSpecification item in items)
            {
                if (!result.Contains( item.SpecificationItemID ))
                    result.Add( item.SpecificationItemID );
            }
        }

        return result;
    }


    protected void uxList_ItemDataBound( object sender, DataListItemEventArgs e )
    {
        Components_ProductItemDetails listItem = (Components_ProductItemDetails) e.Item.FindControl( "uxItem" );
        if (listItem != null)
        {
            listItem.SetSpecificationIDs( GetSpecificationItemList( GetProductList() ) );
        }
    }


    public void Refresh()
    {

        uxList.DataSource = GetProductList();
        uxList.DataBind();

    }
}

