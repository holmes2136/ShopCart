using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Domain.Users;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class Components_ComparisonListBox : BaseLanguageUserControl
{
    private void DeleteItem( string productID )
    {
        StoreContext.ProductIDsCompareList.Remove( productID );

        if ( UserUtilities.GetCurrentCustomerID() != "0" )
        {
            Customer currentUser = DataAccessContext.CustomerRepository.GetOne( UserUtilities.GetCurrentCustomerID() );
            ArrayList compareList = currentUser.GetProductIDsCompare();
            compareList.Remove( productID );
            currentUser.SetProductIDsCompare( compareList );
            DataAccessContext.CustomerRepository.Save( currentUser );
        }
    }

    private bool IsExitsInCurrentStore( Product product )
    {
        string rootID = DataAccessContext.Configurations.GetValue(
                "RootCategory",
                DataAccessContext.StoreRepository.GetOne( DataAccessContext.StoreRetriever.GetCurrentStoreID() ) );

        return product.IsAvailable( rootID );
    }

    private IList<Product> CreateDataSource()
    {
        ArrayList list = StoreContext.ProductIDsCompareList;
        if ( UserUtilities.GetCurrentCustomerID() != "0" )
        {
            DataAccessContext.CustomerRepository.ClearCustomerCache();
            Customer currentUser = DataAccessContext.CustomerRepository.GetOne( UserUtilities.GetCurrentCustomerID() );
            list = currentUser.GetProductIDsCompare();
            if ( StoreContext.ProductIDsCompareList.Count > 0 )
            {
                ArrayList sessionList = StoreContext.ProductIDsCompareList;
                foreach ( string id in sessionList )
                {
                    if ( !list.Contains( id ) )
                        list.Add( id );
                }
            }


            currentUser.SetProductIDsCompare( list );
            DataAccessContext.CustomerRepository.Save( currentUser );
        }

        int itemShow = DataAccessContext.Configurations.GetIntValue( "CompareProductShow" );
        IList<Product> displayList = new List<Product>();

        int itemCount = 0;

        foreach ( string id in list )
        {
            Product product = DataAccessContext.ProductRepository.GetOne(
                StoreContext.Culture, id, new StoreRetriever().GetStore().StoreID );

            if ( !product.IsNull && IsExitsInCurrentStore( product ) && itemCount < itemShow )
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

    private void PopulateControls()
    {
        if ( DataAccessContext.Configurations.GetBoolValue( "CompareListEnabled" )
            && ( CreateDataSource().Count > 0 ) )
        {
            uxList.DataSource = CreateDataSource();
            uxList.DataBind();
        }
        else
        {
            this.Visible = false;
            uxTitle.Visible = false;
        }
    }

    private void Comparison_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        PopulateControls();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( Comparison_StoreCultureChanged );

        if ( !IsPostBack )
        {
            PopulateControls();
        }

        uxCompareButton.Attributes.Add( "onclick", String.Format( "window.open('{0}');", "ComparisonListPopup.aspx" ) );
    }

    protected void uxList_DeleteCommand( object source, DataListCommandEventArgs e )
    {
        DeleteItem( e.CommandArgument.ToString() );
        PopulateControls();
    }

    protected void uxDeleteAllButton_Click( object sender, EventArgs e )
    {
        StoreContext.ProductIDsCompareList.Clear();

        if ( UserUtilities.GetCurrentCustomerID() != "0" )
        {
            Customer currentUser = DataAccessContext.CustomerRepository.GetOne( UserUtilities.GetCurrentCustomerID() );
            ArrayList compareList = currentUser.GetProductIDsCompare();
            compareList.Clear();
            currentUser.SetProductIDsCompare( compareList );
            DataAccessContext.CustomerRepository.Save( currentUser );
        }

        PopulateControls();
    }

    protected string GetNavName( object product )
    {
        return ( ( Product ) product ).Name;
    }

    protected string GetItemImage( object product )
    {
        ProductImage productImage = ( ( Product ) product ).GetPrimaryProductImage();

        if ( String.IsNullOrEmpty( productImage.ThumbnailImage ) )
        {
            return "~/Images/Products/Thumbnail/DefaultNoImage.gif";
        }
        else
        {
            return "~/" + productImage.ThumbnailImage;
        }
    }

    public void Refresh()
    {
        PopulateControls();
    }
}
