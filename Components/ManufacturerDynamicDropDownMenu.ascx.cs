using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.WebUI.Products;
using Vevo;

public partial class Components_ManufacturerDynamicDropDownMenu : BaseCategoryMenuNavListUserControl
{
    private string _rootMenuName;

    private string ManufacturerID
    {
        get
        {
            string id = Request.QueryString["ManufacturerID"];
            if (String.IsNullOrEmpty( id ))
                return "0";
            else
                return id;
        }
    }

    private string ManufacturerName
    {
        get
        {
            if (Request.QueryString["ManufacturerName"] == null)
                return String.Empty;
            else
                return Request.QueryString["ManufacturerName"];
        }
    }

    private void Components_ManufacturerDynamicDropDownMenu_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        PopulateControls();
    }

    private void PopulateControls()
    {
        uxManufacturerDropDownMenu.Items.Clear();
        uxManufacturerDropDownMenu.MaximumDynamicDisplayLevels = 1;

        MenuItem rootMenu = new MenuItem();
        rootMenu.Text = RootMenuName;
        rootMenu.NavigateUrl = "~/Manufacturer.aspx";

        IList<Manufacturer> manufacturerList = DataAccessContext.ManufacturerRepository.GetAll( StoreContext.Culture, BoolFilter.ShowTrue, "SortOrder" );

        ManufacturerNavMenuBuilder menuBuilder = new ManufacturerNavMenuBuilder( UrlManager.GetManufacturerUrl );
        foreach (Manufacturer manufacturer in manufacturerList)
        {
            rootMenu.ChildItems.Add( menuBuilder.CreateMenuItem(manufacturer));
        }
        uxManufacturerDropDownMenu.Items.Add( rootMenu );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler(
            Components_ManufacturerDynamicDropDownMenu_StoreCultureChanged );

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    public string RootMenuName
    {
        get { return _rootMenuName; }
        set { _rootMenuName = value; }
    }
}
