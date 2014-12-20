using System;
using System.Web.Security;
using System.Web.UI;
using Vevo.Domain;
using Vevo.WebUI.BaseControls;
using Vevo.Deluxe.WebUI.Marketing;

public partial class Themes_ResponsiveGreen_Product : BaseMasterPage
{
    private string GetCurrentPage()
    {
        return Request.Url.AbsolutePath.ToLower();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if ( DataAccessContext.Configurations.GetBoolValue( "RestrictAccessToShop" ) )
        {
            if ( !Page.User.Identity.IsAuthenticated )
            {
                uxLeft.Visible = false;
                MainDivCenter.Style.Add( "margin", "auto" );
                MainDivCenter.Style.Add( "float", "none" );

                if ( IsRestrictedAccessPage() )
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
            }
            else
            {
                uxLeft.Visible = true;
            }
        }

        if ( DataAccessContext.Configurations.GetBoolValue( "PriceRequireLogin" ) )
        {
            if ( !Page.User.Identity.IsAuthenticated )
            {
                uxPriceRequireLoginPanel.Visible = true;
            }
        }

        AffiliateHelper.SetAffiliateCookie( AffiliateCode );
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        string currentPage = GetCurrentPage();

        if (currentPage.Contains("department.aspx"))
        {
            if ((ViewState["DepartmentID"] == null) && (Request.QueryString["DepartmentName"] == null))
                uxMainDiv.Attributes.Add("class", "row MainDiv");
        }
        else if (currentPage.Contains("manufacturer.aspx"))
        {
            if ((ViewState["ManufacturerID"] == null) && (Request.QueryString["ManufacturerName"] == null))
                uxMainDiv.Attributes.Add("class", "row MainDiv");
        }
        else
        {
            if ((ViewState["CategoryID"] == null) && (Request.QueryString["CategoryName"] == null) && (Request.QueryString["CategoryID"] == null))
                uxMainDiv.Attributes.Add("class", "row MainDiv");
        }
    }
}
