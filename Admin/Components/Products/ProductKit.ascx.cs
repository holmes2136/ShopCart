using System;
using System.Web.UI;
using Vevo;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.Domain;
using Vevo.Domain.Stores;
using System.Collections;
using System.Collections.Generic;

public partial class Admin_Components_Products_ProductKit : AdminAdvancedBaseUserControl
{
    private void InitMultiProductKitGroup()
    {
        if (!MainContext.IsPostBack)
        {
            uxMultiProductKitGroup.SetupDropDownList( CurrentID, CurrentCulture, false );
        }
        else
        {
            uxMultiProductKitGroup.SetupDropDownList( CurrentID, CurrentCulture, true );
        }
    }

    private Control GetAjaxTabControl( Control control )
    {
        if (control.GetType().ToString() == "AjaxControlToolkit.TabContainer")
            return control;
        if (control.Parent == null)
            return null;
        return GetAjaxTabControl( control.Parent );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        InitMultiProductKitGroup();
    }

    protected void uxAddProductKitGroup_Click( object sender, EventArgs e )
    {
        Control control = GetAjaxTabControl( this );

        AjaxControlToolkit.TabContainer tabControl = (AjaxControlToolkit.TabContainer) control;
        tabControl.ActiveTab = tabControl.Tabs[2];
    }

    public void SetIsProductKit( bool isProductKit )
    {
        uxIsProductKitDrop.SelectedValue = isProductKit.ToString();
        uxLinkToProductKitGroupTR.Visible = isProductKit;
    }

    public void EnableProductKitControl()
    {
        uxIsProductKitDrop.Enabled = true;
    }

    public void DisableProductKitControl()
    {
        uxIsProductKitDrop.Enabled = false;
    }

    public void PopulateControls( Product product )
    {
        SetIsProductKit( product.IsProductKit );
    }

    public void ClearInputFields()
    {
        SetIsProductKit( false );
        uxMultiProductKitGroup.SetupDropDownList( CurrentID, CurrentCulture, false );
    }

    public string CurrentID
    {
        get
        {
            if (String.IsNullOrEmpty( MainContext.QueryString["ProductID"] ))
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

    public Product Setup( Product product )
    {
		product.ProductKits.Clear();

        if (ConvertUtilities.ToBoolean( uxIsProductKitDrop.SelectedValue ))
        {
            
            foreach (string groupID in uxMultiProductKitGroup.ConvertToProductKitGroupIDs())
            {
                ProductKit productKit = new ProductKit( CurrentCulture );
                productKit.ProductKitGroupID = groupID;
                product.ProductKits.Add( productKit );
            }
        }

        product.IsProductKit = ConvertUtilities.ToBoolean( uxIsProductKitDrop.SelectedValue );

        return product;

    }

    public bool IsProductKit
    {
        get { return ConvertUtilities.ToBoolean( uxIsProductKitDrop.SelectedValue ); }
    }

    public string[] GetSelectedGroupID()
    {
        return uxMultiProductKitGroup.ConvertToProductKitGroupIDs();
    }
}
