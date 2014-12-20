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
using Vevo.Domain.Tax;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.Domain.Stores;


public partial class AdminAdvanced_Components_Products_ProductSeo : AdminAdvancedBaseUserControl
{
    #region Private
    private void UpdateVisibleUseDefault( string storeID )
    {
        if (storeID != "0")
        {
            uxMetaKeywordCheck.Visible = true;
            uxMetaDescriptionCheck.Visible = true;
            uxMetaKeywordCheckLabel.Visible = true;
            uxMetaDesciptionCheckLabel.Visible = true;
        }
        else
        {
            uxMetaKeywordCheck.Visible = false;
            uxMetaDescriptionCheck.Visible = false;
            uxMetaKeywordCheckLabel.Visible = false;
            uxMetaDesciptionCheckLabel.Visible = false;
            uxMetaKeywordText.Enabled = true;
            uxMetaDescriptionText.Enabled = true;
        }
    }
    #endregion

    #region Protected
    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateControls();
        }
    }

    protected void uxMetaKeywordCheck_OnCheckedChanged( object sender, EventArgs e )
    {
        uxMetaKeywordText.Enabled = !uxMetaKeywordCheck.Checked;
    }

    protected void uxMetaDescriptionCheck_OnCheckedChanged( object sender, EventArgs e )
    {
        uxMetaDescriptionText.Enabled = !uxMetaDescriptionCheck.Checked;
    }
    #endregion

    #region Public
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

    public string StoreID
    {
        get
        {
            if (ViewState["StoreID"] == null)
                return "0";
            else
                return (string) ViewState["StoreID"];
        }
        set
        {
            ViewState["StoreID"] = value;
        }
    }

    public bool UseDefaultMetaKeyword
    {
        get
        {
            return uxMetaKeywordCheck.Checked;
        }
    }

    public bool UseDefaultMetaDescription
    {
        get
        {
            return uxMetaDescriptionCheck.Checked;
        }
    }

    public Product Setup( Product product,string storeID )
    {
        if ((uxMetaKeywordCheck.Checked) && (storeID !="0"))
            product.SetMetaKeyword( CurrentCulture, storeID, product.GetMetaKeyword( CurrentCulture, "0" ) );
        else
            product.SetMetaKeyword( CurrentCulture, storeID, uxMetaKeywordText.Text );

        if ((uxMetaDescriptionCheck.Checked)&& (storeID !="0"))
            product.SetMetaDescription( CurrentCulture, storeID, product.GetMetaDescription( CurrentCulture, "0" ) );
        else
            product.SetMetaDescription( CurrentCulture, storeID, uxMetaDescriptionText.Text );

        product.SetUseDefaultValueMetaKeyword( CurrentCulture, storeID, uxMetaKeywordCheck.Checked );
        product.SetUseDefaultValueMetaDescription( CurrentCulture, storeID, uxMetaDescriptionCheck.Checked );
        return product;
    }

    public void ClearInputFields()
    {
        uxMetaKeywordText.Text = "";
        uxMetaDescriptionText.Text = "";
    }

    public void PopulateControls()
    {
        string storeID = StoreID;
        Product product = DataAccessContext.ProductRepository.GetOne( CurrentCulture, CurrentID, storeID);
        ProductMetaInformation meta = product.GetMetaInformation(CurrentCulture,storeID);
        if (meta != null)
        {
            if (meta.UseDefaultMetaKeyword)
                uxMetaKeywordText.Text = product.GetMetaKeyword( CurrentCulture, storeID );
            else
                uxMetaKeywordText.Text = meta.MetaKeyword;

            if (meta.UseDefaultMetaDescription)
                uxMetaDescriptionText.Text = product.GetMetaDescription( CurrentCulture, storeID );
            else
                uxMetaDescriptionText.Text = meta.MetaDescription;
  
            uxMetaKeywordCheck.Checked = meta.UseDefaultMetaKeyword;
            uxMetaKeywordText.Enabled = !meta.UseDefaultMetaKeyword;
            uxMetaDescriptionCheck.Checked = meta.UseDefaultMetaDescription;
            uxMetaDescriptionText.Enabled = !meta.UseDefaultMetaDescription;
        }
        UpdateVisibleUseDefault( storeID );
    }

    public void PopulateControls(Product product,string storeID)
    {
        ProductMetaInformation meta = product.GetMetaInformation( CurrentCulture, storeID );
        if (meta != null)
        {
            if (meta.UseDefaultMetaKeyword)
                uxMetaKeywordText.Text = product.GetMetaKeyword( CurrentCulture, storeID );
            else
                uxMetaKeywordText.Text = meta.MetaKeyword;

            if (meta.UseDefaultMetaDescription)
                uxMetaDescriptionText.Text = product.GetMetaDescription( CurrentCulture, storeID );
            else
                uxMetaDescriptionText.Text = meta.MetaDescription;

            uxMetaKeywordText.Text = meta.MetaKeyword;
            uxMetaDescriptionText.Text = meta.MetaDescription;
            uxMetaKeywordCheck.Checked = meta.UseDefaultMetaKeyword;
             uxMetaKeywordText.Enabled = !meta.UseDefaultMetaKeyword;
            uxMetaDescriptionCheck.Checked = meta.UseDefaultMetaDescription;
            uxMetaDescriptionText.Enabled = !meta.UseDefaultMetaDescription;            
        }
        UpdateVisibleUseDefault( storeID );
    }

    public void SerVisibleUseDefaultValueCheckbox(bool flag)
    {
        uxMetaKeywordCheck.Visible = flag;
        uxMetaKeywordCheckLabel.Visible = flag;
        uxMetaDescriptionCheck.Visible = flag;
        uxMetaDesciptionCheckLabel.Visible = flag;
    }

    #endregion
}
