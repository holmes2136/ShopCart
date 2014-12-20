using System;
using Vevo.WebUI.BaseControls;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;

public partial class Themes_ResponsiveGreen_LayoutControls_LeftProduct : BaseLayoutControl
{
    private string GetCurrentPage()
    {
        string currentPage = Request.Url.AbsolutePath.ToLower();
        if(String.IsNullOrEmpty(currentPage))
            currentPage = "catalog.aspx";

        return currentPage;
    }
    protected void Page_Load(object sender, EventArgs e)
    {      
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        string currentPage = GetCurrentPage();

        if (FacetSearch.IsFacetedVisible())
        {
            uxCategoryNavList.Visible = false;
            uxDepartmentNavList.Visible = false;
            uxManufacturerNavList.Visible = false;

        }
        else if (currentPage.Contains("department.aspx"))
        {
            uxCategoryNavList.Visible = false;
            uxDepartmentNavList.Visible = true;
            uxManufacturerNavList.Visible = false;
        }
        else if (currentPage.Contains("manufacturer.aspx"))
        {
            uxCategoryNavList.Visible = false;
            uxDepartmentNavList.Visible = false;
            uxManufacturerNavList.Visible = true;
        }
        else
        {
            uxCategoryNavList.Visible = true;
            uxDepartmentNavList.Visible = false;
            uxManufacturerNavList.Visible = false;
        }
    }


}
