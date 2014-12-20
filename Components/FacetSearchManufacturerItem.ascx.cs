using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.WebUI;
using Vevo.Domain;
using Vevo.Domain.Products;

public partial class Components_FacetSearchManufacturerItem : Vevo.WebUI.Products.BaseFacetSearchControl
{
    #region Private

    private string[] GetCategoryList()
    {
        string categoryID = Request.QueryString["cat"];

        if (String.IsNullOrEmpty( categoryID ))
        {
            return GetCategoryListwithLeaf();
        }
        else
            return GetCategoryListwithLeaf( categoryID );
    }

    private void PopulateControls()
    {
        if (!IsFacetedVisible())
        {
            this.Visible = false;
        }
        else
        {
            if (!String.IsNullOrEmpty( Request.QueryString["manu"] ) || GetManufacturerItem().Count == 0)
            {
                uxManufacturerItemDiv.Visible = false;
            }
            else
            {
                uxList.DataSource = GetManufacturerItem();
                uxList.DataBind();
                uxManufacturerItemDiv.Visible = true;
            }
        }
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected int GetCountItem( string manufacturerID )
    {
        string startPrice = MinPrice;
        string endPrice = MaxPrice;

        IList<string> specKeyList = GetAllSpecKey();


        int itemCount = DataAccessContext.ProductRepository.GetFacetCount(
                StoreContext.Culture,
                GetCategoryList(),
                GetDepartmentListWithLeaf(),
                manufacturerID,
                startPrice,
                endPrice,
                GetSpecItemValueList( specKeyList ),
                "",
                BoolFilter.ShowTrue,
                StoreContext.CurrentStore.StoreID );

        return itemCount;
    }

    protected string GetCountManufacturerItem( object manufacturerID )
    {
        return " (" + GetCountItem( manufacturerID.ToString() ) + ") ";
    }

    protected string GetNavName( object manufacturerName )
    {
        return manufacturerName.ToString();
    }

    protected string GetNavUrl( object manufacturerID )
    {
        string[] index = Request.Url.Query.Split( '&' );
        int count = index[0].Length;
        if (Request.Url.Query.Contains( "&" ))
        {
            count = index[0].Length + 1;
        }

        string query = Request.Url.Query.Remove( 0, count );

        if (String.IsNullOrEmpty( query ))
            return Vevo.UrlManager.GetCategoryUrl( CurrentCategoryID, CurrentCategory.UrlName ) + "?manu=" + manufacturerID;
        else
            return Vevo.UrlManager.GetCategoryUrl( CurrentCategoryID, CurrentCategory.UrlName ) + "?" + query + "&manu=" + manufacturerID;
    }

    protected string GetGroupText( object text )
    {
        return text.ToString();
    }

    #endregion

    #region Public

    public IList<Manufacturer> GetManufacturerItem()
    {
        IList<Manufacturer> manufacturerList = DataAccessContext.ManufacturerRepository.GetAll( StoreContext.Culture, BoolFilter.ShowTrue, "Name" );
        IList<Manufacturer> visibleManufactuereList = new List<Manufacturer>();

        foreach (Manufacturer manufacturer in manufacturerList)
        {
            if (GetCountItem( manufacturer.ManufacturerID ) > 0)
            {
                visibleManufactuereList.Add( manufacturer );
            }
        }

        return visibleManufactuereList;
    }

    #endregion
}