using System;
using System.Collections.Generic;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.WebUI.Products;

public partial class Components_FacetSearchDepartmentItem : BaseFacetSearchControl
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
        if (!IsFacetedVisible() || !(DataAccessContext.Configurations.GetBoolValue("DepartmentListModuleDisplay") || DataAccessContext.Configurations.GetBoolValue("DepartmentHeaderMenuDisplay")))
        {
            this.Visible = false;
        }
        else
        {
            if (GetDepartmentItem().Count == 0)
            {
                uxDepartmentItemDiv.Visible = false;
            }
            else
            {
                uxList.DataSource = GetDepartmentItem();
                uxList.DataBind();
                uxDepartmentItemDiv.Visible = true;
            }
        }
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected int GetCountItem( string departmentID )
    {
        string startPrice = MinPrice;
        string endPrice = MaxPrice;

        IList<string> specKeyList = GetAllSpecKey();


        int itemCount = DataAccessContext.ProductRepository.GetFacetCount(
                StoreContext.Culture,
                GetCategoryList(),
                GetDepartmentListWithLeaf( departmentID ),
                GetManufacturerID(),
                startPrice,
                endPrice,
                GetSpecItemValueList( specKeyList ),
                "",
                BoolFilter.ShowTrue,
                StoreContext.CurrentStore.StoreID );

        return itemCount;
    }

    protected string GetCountDepartmentItem( object departmentID )
    {
        return " (" + GetCountItem( departmentID.ToString() ) + ") ";
    }

    protected string GetNavName( object departmentName )
    {
        return departmentName.ToString();
    }

    protected string GetNavUrl( object departmentID )
    {
        string[] index = Request.Url.Query.Split( '&' );
        int count = index[0].Length;
        if (Request.Url.Query.Contains( "&" ))
        {
            count = index[0].Length + 1;
        }

        string query = Request.Url.Query.Remove( 0, count );

        if (!String.IsNullOrEmpty( Request.QueryString["dep"] ))
        {
            query = query.Replace( "dep=" + Request.QueryString["dep"], "dep=" + departmentID );
            return Vevo.UrlManager.GetCategoryUrl( CurrentCategoryID, CurrentCategory.UrlName ) + "?" + query;
        }

        if (String.IsNullOrEmpty( query ))
            return Vevo.UrlManager.GetCategoryUrl( CurrentCategoryID, CurrentCategory.UrlName ) + "?dep=" + departmentID;
        else
            return Vevo.UrlManager.GetCategoryUrl( CurrentCategoryID, CurrentCategory.UrlName ) + "?" + query + "&dep=" + departmentID;
    }

    protected string GetGroupText( object text )
    {
        return text.ToString();
    }

    #endregion

    #region Public

    public IList<Department> GetDepartmentItem()
    {
        string departmentID = DataAccessContext.Configurations.GetValue( "RootDepartment", StoreContext.CurrentStore );

        if (!String.IsNullOrEmpty( Request.QueryString["dep"] ))
        {
            departmentID = Request.QueryString["dep"];
        }


        IList<Department> departmentList = DataAccessContext.DepartmentRepository.GetByParentID( StoreContext.Culture, departmentID, "DepartmentID", BoolFilter.ShowTrue );
        IList<Department> visibleDepartmentList = new List<Department>();

        foreach (Department department in departmentList)
        {
            if (GetCountItem( department.DepartmentID ) > 0)
            {
                visibleDepartmentList.Add( department );
            }
        }

        return visibleDepartmentList;
    }

    #endregion
}