using System;
using System.Collections.Generic;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;

public partial class Components_FacetSearchCategoryItem : Vevo.WebUI.Products.BaseFacetSearchControl
{
    #region Private

    private void PopulateControls()
    {
        if (!IsFacetedVisible())
        {
            this.Visible = false;
        }
        else
        {
            if (GetCategoryItem().Count == 0)
            {
                uxCategoryItemDiv.Visible = false;
            }
            else
            {
                uxList.DataSource = GetCategoryItem();
                uxList.DataBind();
                uxCategoryItemDiv.Visible = true;
            }
        }
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected int GetCountItem( string categoryID )
    {
        string startPrice = MinPrice;
        string endPrice = MaxPrice;

        IList<string> specKeyList = GetAllSpecKey();


        int itemCount = DataAccessContext.ProductRepository.GetFacetCount(
                StoreContext.Culture,
                GetCategoryListwithLeaf( categoryID ),
                GetDepartmentListWithLeaf(),
                GetManufacturerID(),
                startPrice,
                endPrice,
                GetSpecItemValueList( specKeyList ),
                "",
                BoolFilter.ShowTrue,
                StoreContext.CurrentStore.StoreID );

        return itemCount;
    }

    protected string GetCountCategoryItem( object categoryID )
    {
        return " (" + GetCountItem( categoryID.ToString() ) + ") ";
    }

    protected string GetNavName( object categoryName )
    {
        return categoryName.ToString();
    }

    protected string GetNavUrl( object categoryID )
    {
        string[] index = Request.Url.Query.Split( '&' );
        int count = index[0].Length;
        if (Request.Url.Query.Contains( "&" ))
        {
            count = index[0].Length + 1;
        }

        string query = Request.Url.Query.Remove( 0, count );

        if (!String.IsNullOrEmpty( Request.QueryString["cat"] ))
        {
            query = query.Replace( "cat=" + Request.QueryString["cat"], "cat=" + categoryID );
            return Vevo.UrlManager.GetCategoryUrl( CurrentCategoryID, CurrentCategory.UrlName ) + "?" + query;
        }

        if (String.IsNullOrEmpty( query ))
            return Vevo.UrlManager.GetCategoryUrl( CurrentCategoryID, CurrentCategory.UrlName ) + "?cat=" + categoryID;
        else
            return Vevo.UrlManager.GetCategoryUrl( CurrentCategoryID, CurrentCategory.UrlName ) + "?" + query + "&cat=" + categoryID;
    }

    protected string GetGroupText( object text )
    {
        return text.ToString();
    }

    #endregion

    #region Public

    public IList<Category> GetCategoryItem()
    {
        string categoryID = CurrentCategoryID;

        if (!String.IsNullOrEmpty( Request.QueryString["cat"] ))
        {
            categoryID = Request.QueryString["cat"];
        }

        IList<Category> categoryList = DataAccessContext.CategoryRepository.GetByParentID( StoreContext.Culture, categoryID, "CategoryID", BoolFilter.ShowTrue );
        IList<Category> visibleCategoryList = new List<Category>();

        foreach (Category category in categoryList)
        {
            if (GetCountItem( category.CategoryID ) > 0)
            {
                visibleCategoryList.Add( category );
            }
        }

        return visibleCategoryList;
    }

    #endregion
}