using System;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class Components_FacetSearchSelected : Vevo.WebUI.Products.BaseFacetSearchControl
{
    #region Private

    private void Components_FacetSearch_StoreCurrencyChanged( object sender, CurrencyEventArgs e )
    {
        PopulateControls();
    }

    private void Components_FacetSearch_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        PopulateControls();
    }

    private void PopulateControls()
    {
        if (FacetSearchSource().Count > 0)
        {
            uxFacetSearchList.DataSource = FacetSearchSource();
            uxFacetSearchList.DataBind();
            uxFacetedSearchSelectedDiv.Visible = true;
        }
        else
        {
            uxFacetedSearchSelectedDiv.Visible = false;
        }
    }

    private IList<string> FacetSearchSource()
    {
        string[] allKey = Request.QueryString.AllKeys;

        IList<string> list = new List<string>();
        foreach (string key in allKey)
        {
            if (key.ToLower() != "minprice" &&
                key.ToLower() != "maxprice" &&
                key.ToLower() != "categoryname" &&
                key.ToLower() != "categoryid" &&
                key.ToLower() != "cat" &&
                key.ToLower() != "dep" &&
                key.ToLower() != "manu")
            {
                list.Add( key );
            }
            else if (key.ToLower() == "minprice")
            {
                list.Add( "Price" );
            }
            else if (key.ToLower() == "cat")
            {
                list.Add( "[$Category]" );
            }
            else if (key.ToLower() == "dep")
            {
                list.Add( "[$Department]" );
            }
            else if (key.ToLower() == "manu")
            {
                list.Add( "[$Manufacturer]" );
            }
        }

        return list;
    }


    #endregion

    #region Protected

    protected string GetRemovePostBackUrl( object groupName )
    {
        string currentValue = groupName.ToString();
        string[] index = Request.Url.Query.Split( '&' );
        int count = index[0].Length;
        if (Request.Url.Query.Contains( "&" ))
        {
            count = index[0].Length + 1;
        }

        string[] allKey = Request.QueryString.AllKeys;

        string query = String.Empty;
        int indexCount = 0;

        string catQuery = String.Empty;

        for (int i = 0; i < index.Length; i++)
        {
            if ( !allKey[ i ].ToLower().Equals( "categoryname" ) && 
                 !allKey[ i ].ToLower().Equals( "categoryid" ) && 
                 !allKey[ i ].Equals( currentValue ) )
            {
                if (currentValue.ToLower().Equals( "price" ) &&
                    (allKey[i].ToLower().Equals( "minprice" ) ||
                    allKey[i].ToLower().Equals( "maxprice" )))
                {
                    continue;
                }

                if (currentValue.Equals( "[$Category]" ) && allKey[i].ToLower().Equals( "cat" ))
                {
                    Category category = DataAccessContext.CategoryRepository.GetOne( StoreContext.Culture, Request.QueryString["cat"] );
                    if (category.ParentCategoryID != CurrentCategoryID)
                    {
                        index[i] = index[i].Replace( "cat=" + category.CategoryID, "cat=" + category.ParentCategoryID );
                    }
                    else
                    {
                        continue;
                    }
                }

                if (currentValue.Equals( "[$Department]" ) && allKey[i].ToLower().Equals( "dep" ))
                {
                    Department department = DataAccessContext.DepartmentRepository.GetOne( StoreContext.Culture, Request.QueryString["dep"] );
                    if (department.ParentDepartmentID != DataAccessContext.Configurations.GetValue( "RootDepartment" ))
                    {
                        index[i] = index[i].Replace( "dep=" + department.DepartmentID, "dep=" + department.ParentDepartmentID );
                    }
                    else
                    {
                        continue;
                    }
                }

                if (currentValue.Equals( "[$Manufacturer]" ) && allKey[i].ToLower().Equals( "manu" ))
                {
                    continue;
                }
                if (indexCount == 0)
                {
                    query += "?";
                }
                else
                {
                    query += "&";
                }

                query += index[i];
                indexCount++;
            }
        }

        return Vevo.UrlManager.GetCategoryUrl( CurrentCategoryID, CurrentCategory.UrlName ) + query;
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCurrencyChanged +=
            new StorefrontEvents.CurrencyEventHandler( Components_FacetSearch_StoreCurrencyChanged );

        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( Components_FacetSearch_StoreCultureChanged );

        PopulateControls();
        hyperLink.NavigateUrl = GetNavUrl();
    }

    protected string GetGroupName( object groupName )
    {
        if (groupName.ToString().ToLower() == "price" ||
            groupName.ToString() == "[$Category]" ||
            groupName.ToString() == "[$Department]" ||
            groupName.ToString() == "[$Manufacturer]")
        {
            return groupName.ToString() + " : ";
        }
        SpecificationItem specItem = DataAccessContext.SpecificationItemRepository.GetOneByName( StoreContext.Culture, groupName.ToString() );
        return specItem.DisplayName + " : ";
    }

    protected string GetDisplayText( object groupName )
    {
        if (groupName.ToString().ToLower() == "price")
        {
            if (String.IsNullOrEmpty( MaxPrice ))
            {
                return StoreContext.Currency.FormatPrice( ConvertUtilities.ToDecimal( MinPrice ) ) + GetLanguageText( "andabove" );
            }

            return StoreContext.Currency.FormatPrice( ConvertUtilities.ToDecimal( MinPrice ) ) + " - " + StoreContext.Currency.FormatPrice( ConvertUtilities.ToDecimal( MaxPrice ) );
        }
        else if (groupName.ToString() == "[$Category]")
        {
            Category category = DataAccessContext.CategoryRepository.GetOne( StoreContext.Culture, Request.QueryString["cat"] );
            return category.Name;
        }
        else if (groupName.ToString() == "[$Department]")
        {
            Department department = DataAccessContext.DepartmentRepository.GetOne( StoreContext.Culture, Request.QueryString["dep"] );
            return department.Name;
        }
        else if (groupName.ToString() == "[$Manufacturer]")
        {
            Manufacturer manufacturer = DataAccessContext.ManufacturerRepository.GetOne( StoreContext.Culture, Request.QueryString["manu"] );
            return manufacturer.Name;
        }
        else
        {
            SpecificationItem specItem = DataAccessContext.SpecificationItemRepository.GetOneByName( StoreContext.Culture, groupName.ToString() );
            SpecificationItemValue specValue = DataAccessContext.SpecificationItemValueRepository.GetOneBySpecItemIDAndValue( StoreContext.Culture, specItem.SpecificationItemID, Request.QueryString[groupName.ToString()] );
            return specValue.DisplayValue;
        }
    }

    protected string GetNavUrl()
    {
        return Vevo.UrlManager.GetCategoryUrl( CurrentCategoryID, CurrentCategory.UrlName );
    }

    #endregion
}