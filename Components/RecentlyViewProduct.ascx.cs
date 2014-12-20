using System;
using System.Collections;
using System.Collections.Generic;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;

public partial class Components_RecentlyViewProduct : Vevo.WebUI.International.BaseLanguageUserControl
{
    private IList<Product> CreateDataSource()
    {
        int recentViewedShow = DataAccessContext.Configurations.GetIntValue( "RecentlyViewedProductShow" );
        IList<Product> list = new List<Product>();

        foreach (string id in StoreContext.RecentltViewedProduct)
        {
            Product product = DataAccessContext.ProductRepository.GetOne(
                StoreContext.Culture, id, new StoreRetriever().GetStore().StoreID );

            if (product.IsProductAvailable( new StoreRetriever().GetStore().StoreID ) && list.Count < recentViewedShow)
                list.Add( product );
        }

        return list;
    }

    private void PopulateControls()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "RecentlyViewedProductDisplay" )
            && (CreateDataSource().Count > 0))
        {
            uxList.DataSource = CreateDataSource();
            uxList.DataBind();
            RecentlyViewedDiv.Attributes.Add( "style", "display: block;" );
        }
        else
        {
            RecentlyViewedDiv.Attributes.Add( "style", "display: none;" );
        }
    }


    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected string GetNavName( object product )
    {
        return ((Product) product).Name;
    }

    public void Refresh()
    {
        PopulateControls();
    }

}
