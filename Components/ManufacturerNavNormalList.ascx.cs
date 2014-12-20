using System;
using System.Collections.Generic;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;

public partial class Components_ManufacturerNavNormalList : System.Web.UI.UserControl
{
    private IList<Manufacturer> GetManufacturerList()
    {
        return DataAccessContext.ManufacturerRepository.GetAll( StoreContext.Culture, BoolFilter.ShowTrue, "SortOrder" );
    }

    private void PopulateControls()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "EnableManufacturer" ) && (DataAccessContext.Configurations.GetValue( "ManufacturerMenuType" ) != "dropdown"))
        {
            uxList.DataSource = GetManufacturerList();
            uxList.DataBind();
        }
        else
        {
            this.Visible = false;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    protected string GetNavName( object manufacturer )
    {
        return ((Manufacturer) manufacturer).Name;
    }

    public void Refresh()
    {
        PopulateControls();
    }
}