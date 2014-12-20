using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;

public partial class AdminAdvanced_Components_Products_DepartmentInfo : AdminAdvancedBaseUserControl
{
    #region Private

    private void InitDropDownList()
    {
        string storeID = new StoreRetriever().GetCurrentStoreID();
        if (!MainContext.IsPostBack)
        {
            uxMultiDepartment.SetupDropDownList( CurrentID, CurrentCulture, false );
        }
        else
        {
            uxMultiDepartment.SetupDropDownList( CurrentID, CurrentCulture, true );
        }
    }
    #endregion

    #region Protected
    protected void Page_PreRender( object sender, EventArgs e )
    {
        InitDropDownList();
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    #endregion

    #region Public
    public void ClearInputFields()
    {
        uxMultiDepartment.SetupDropDownList( CurrentID, CurrentCulture, false );
    }

    public Product Setup( Product product )
    {
        product.DepartmentIDs = ConvertToDepartmentIDs();
        return product;
    }

    public string[] ConvertToDepartmentIDs()
    {
        return uxMultiDepartment.ConvertToDepartmentIDs();
    }

    public string CurrentID
    {
        get
        {
            if (string.IsNullOrEmpty( MainContext.QueryString["ProductID"] ))
                return "0";
            else
                return MainContext.QueryString["ProductID"];
        }
    }

    public Culture CurrentCulture
    {
        get
        {
            if (ViewState["Culture"] == null)
                return null;
            else
                return (Culture) ViewState["Culture"];
        }
        set
        {
            ViewState["Culture"] = value;
        }
    }

    #endregion
}
