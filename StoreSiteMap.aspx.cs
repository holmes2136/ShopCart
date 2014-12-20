using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.DataAccess;
using Vevo.WebUI;
using Vevo.Domain.Stores;

public partial class StoreSiteMap : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private bool IsDisplayCategoriesOnly()
    {
        if (DataAccessContext.Configurations.GetValue("SiteMapDisplayType").ToLower() == "showcategoriesonly")
            return true;
        else
            return false;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsDisplayCategoriesOnly())
        {
            uxInformationPanel.Visible = uxContentNavList.HaveData();
            uxCategoryNavList.PopulateProduct();
            uxDepartmentNavList.PopulateDepartment();
        }
        else
        {
            uxInformationPanel.Visible = false;
            uxCategoryNavList.PopulateCategoriesOnly();
            uxDepartmentNavList.Visible = false;
        }
    }
}
