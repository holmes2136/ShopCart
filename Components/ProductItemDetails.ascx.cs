using System;
using System.Collections;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebUI.Products;

public partial class Components_ProductItemDetails : BaseProductListItemUserControl
{
    #region Private
    private ArrayList specificationIDs = new ArrayList();

    private string GetSpecificationValue( string productID, SpecificationItem specificationItem )
    {
        Product currentProduct = DataAccessContext.ProductRepository.GetOne(
            StoreContext.Culture, productID, StoreContext.CurrentStore.StoreID );

        return currentProduct.GetSpecificationItemValue( specificationItem.SpecificationItemID );
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        AddToCartNotificationParent = uxAddToCartNotification;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateSpecificationPanel();
    }

    private void PopulateSpecificationPanel()
    {
        string txt = "";

        if ( specificationIDs.Count != 0 )
        {
            foreach ( string id in specificationIDs )
            {
                SpecificationItem specificationItem = DataAccessContext.SpecificationItemRepository.GetOne(
                    StoreContext.Culture, id );

                if ( uxProductIDLabel.Text == "0" )
                    txt += "<tr class='RowDiv'> <td class='ItemListTD'><div class='CompareProductSpecification CompareLabel'>" +
                        specificationItem.DisplayName + "</div></td></tr>";
                else
                    txt += "<tr class='RowDiv'> <td class='ItemListTD'><div class='CompareProductSpecification'>" +
                        GetSpecificationValue( uxProductIDLabel.Text, specificationItem ) + "</div></td></tr>";
            }
            uxSpecificationLiteral.Text = txt;
        }
    }

    protected string GetDisplayString( object message, int length )
    {
        string displayMessage = ConvertUtilities.ToString( message );
        if ( displayMessage.Length > length )
        {
            return displayMessage.Substring( 0, length ) + "  ...";
        }

        return displayMessage;
    }

    protected string GetManufacturerName( object manufacturerID )
    {
        string manuID = ConvertUtilities.ToString( manufacturerID );
        Manufacturer manufacturer = DataAccessContext.ManufacturerRepository.GetOne( StoreContext.Culture, manuID );
        return manufacturer.Name;
    }

    protected bool IsTooltipVisible( object message, int length )
    {
        string messageTxt = ConvertUtilities.ToString( message );
        if ( messageTxt.Length > length )
        {
            return true;
        }

        return false;
    }

    protected string GetFormattedRetailPriceFromContainer( object productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne(
                    StoreContext.Culture, ( string ) productID, StoreContext.CurrentStore.StoreID );

        if ( !product.IsCustomPrice )
        {
            return StoreContext.Currency.FormatPrice( GetRetailPrice( product.ProductID ) );
        }
        else
        {
            return "-";
        }
    }

    protected string GetFormattedPriceFromContainer( object productID )
    {
        Product currentProduct = DataAccessContext.ProductRepository.GetOne(
          StoreContext.Culture, ( string ) productID, StoreContext.CurrentStore.StoreID );
        if ( !currentProduct.IsCustomPrice )
        {
            decimal price = currentProduct.GetDisplayedPrice( StoreContext.WholesaleStatus );
            return StoreContext.Currency.FormatPrice( price );
        }
        else
        {
            return "-";
        }
    }

    protected string GetDisplayText( object value )
    {
        if ( String.IsNullOrEmpty( value.ToString() ) )
            return "-";
        else
            return value.ToString();
    }

    #endregion

    #region Public Properties

    public void SetSpecificationIDs( ArrayList result )
    {
        specificationIDs = result;
    }


    #endregion

}
