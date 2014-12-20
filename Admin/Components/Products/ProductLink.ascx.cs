using System;
using System.Collections.Generic;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Stores;

public partial class AdminAdvanced_Components_Products_ProductLink : AdminAdvancedBaseUserControl
{
    private string atagFormat = "<a href=\"http://{0}/AddToCart.aspx?ProductID={1}\">Add to cart</a>";
    private string iframeFormat = "<iframe id=\"uxLinkFrame\" frameborder=\"0\" scrolling=\"no\" width=\"90px\" " +
        "height=\"30px\" src=\"http://{0}/AddToCartFrame.aspx?ProductID={1}\"></iframe>"; 
    
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

    public string CultureID
    {
        get
        {
            if (ViewState["CultureID"] == null)
                return String.Empty;
            else
                return (String) ViewState["CultureID"];
        }
        set { ViewState["CultureID"] = value; }
    }

    public void ClearInputFields()
    {
        uxLinkText.Text = String.Empty;
    }

    private void PopulateControls()
    {
        IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "StoreName" );
        uxStoreDrop.DataSource = storeList;
        uxStoreDrop.DataTextField = "StoreName";
        uxStoreDrop.DataValueField = "UrlName";
        uxStoreDrop.DataBind();
    }
    
    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateControls();
        }
    }
    protected void uxGenerateButton_Click( object sender, EventArgs e )
    {
        ClearInputFields();

        if (uxGenerateTypeDrop.SelectedValue.Equals( "A-tag" ))
        {
            uxLinkText.Text = String.Format( atagFormat, uxStoreDrop.SelectedValue, CurrentID );
        }
        else
        {
            uxLinkText.Text = String.Format( iframeFormat, uxStoreDrop.SelectedValue, CurrentID );
        }
    }
}
