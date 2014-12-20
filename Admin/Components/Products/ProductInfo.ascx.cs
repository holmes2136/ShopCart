using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.Domain.Stores;

public partial class AdminAdvanced_Components_Products_ProductInfo : AdminAdvancedBaseUserControl
{
    #region Private

    private void InitDropDownList()
    {
        string storeID = new StoreRetriever().GetCurrentStoreID();
        if (!MainContext.IsPostBack)
        {
            uxMultiCatalog.SetupDropDownList( CurrentID, CurrentCulture, false );
        }
        else
        {
            uxMultiCatalog.SetupDropDownList( CurrentID, CurrentCulture, true );
        }
    }

    private void RegisterScript()
    {
        string script = "<script type=\"text/javascript\">" +
            "function ismaxlength(obj){" +
            "var mlength= 255;" +
            "if (obj.value.length > mlength)" +
            "obj.value=obj.value.substring(0,mlength);" +
            "}" +
            "</script>";

        ScriptManager.RegisterStartupScript( this, this.GetType(), "CheckLength", script, false );

        uxShortDescriptionText.Attributes.Add( "onkeyup", "return ismaxlength(this)" );
    }

    #endregion


    #region Protected
    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateControls();
        }
        InitDropDownList();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterScript();
        uxNameRequiredValidator.ValidationGroup = ValidationGroup;
    }

    #endregion


    #region Public
    public string ValidationGroup
    {
        get
        {
            if (ViewState["ValidationGroup"] == null)
                return "VaildProduct";
            else
                return (string) ViewState["ValidationGroup"];
        }

        set
        {
            ViewState["ValidationGroup"] = value;
        }
    }

    public void ClearInputFields()
    {
        uxNameText.Text = "";
        uxShortDescriptionText.Text = "";
        uxLongDescriptionText.Text = "";
        uxMultiCatalog.SetupDropDownList( CurrentID, CurrentCulture, false );
    }

    public Product Setup( Product product )
    {
        product.Name = uxNameText.Text;
        product.ShortDescription = uxShortDescriptionText.Text;
        product.LongDescription = uxLongDescriptionText.Text;
        product.CategoryIDs = ConvertToCategoryIDs();
        return product;
    }

    public string[] ConvertToCategoryIDs()
    {
        return uxMultiCatalog.ConvertToCategoryIDs();
    }

    public void PopulateControls()
    {
        Product product = DataAccessContext.ProductRepository.GetOne( CurrentCulture, CurrentID, new StoreRetriever().GetCurrentStoreID() );

        uxNameText.Text = product.Name;
        uxShortDescriptionText.Text = product.ShortDescription;
        uxLongDescriptionText.Text = product.LongDescription;
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
