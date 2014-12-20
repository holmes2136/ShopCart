using Vevo.WebUI.BaseControls;
using System;

public partial class LayoutControls_Header : BaseLayoutControl
{
    private string GetCurrentPage()
    {
        return Request.Url.AbsolutePath.ToLower();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        uxWebsiteTitleLabel.Text = Vevo.WebUI.NamedConfig.SiteName;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        string currentPage = GetCurrentPage();

        if (currentPage.Contains("department.aspx"))
        {
            if (!((ViewState["DepartmentID"] == null) && (Request.QueryString["DepartmentName"] == null)))
                uxWebsiteTitleLabel.Visible = false;
        }
        else if (currentPage.Contains("manufacturer.aspx"))
        {
            if (!((ViewState["ManufacturerID"] == null) && (Request.QueryString["ManufacturerName"] == null)))
                uxWebsiteTitleLabel.Visible = false;
        }
        else if (currentPage.Contains("product.aspx"))
        {
            uxWebsiteTitleLabel.Visible = false;
        }
        else if (currentPage.Contains("newsdetails.aspx"))
        {
            uxWebsiteTitleLabel.Visible = false;
        }
        else
        {
            if (!((ViewState["CategoryID"] == null) && (Request.QueryString["CategoryName"] == null) && (Request.QueryString["CategoryID"] == null)))
                uxWebsiteTitleLabel.Visible = false;
        }
    }
}
