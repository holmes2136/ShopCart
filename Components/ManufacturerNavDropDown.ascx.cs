using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.WebUI.BaseControls;

public partial class Components_ManufacturerNavDropDown : BaseUserControl
{
    private IList<Manufacturer> GetManufacturerList()
    {
        return DataAccessContext.ManufacturerRepository.GetAll( StoreContext.Culture, BoolFilter.ShowTrue, "SortOrder" );
    }

    private void PopulateControls()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "EnableManufacturer" ) && (DataAccessContext.Configurations.GetValue( "ManufacturerMenuType" ) == "dropdown"))
        {
            IList<Manufacturer> ManufacturerList = GetManufacturerList();

            uxDropDown.Items.Add( "[$Please Selected]" );

            foreach (Manufacturer manufac in ManufacturerList)
            {
                uxDropDown.Items.Add( new ListItem( manufac.Name, manufac.ManufacturerID + "|" + manufac.UrlName ) );
            }

            uxDropDown.DataBind();
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

    protected void uxDropDown_SelectedIndexChanged( object sender, EventArgs e )
    {
        if ( uxDropDown.SelectedIndex != 0 )
        {
            string[] split = uxDropDown.SelectedItem.Value.ToString().Split( '|' );
            Response.Redirect( UrlManager.GetManufacturerUrl( Convert.ToString( split[ 0 ] ), Convert.ToString( split[ 1 ] ) ) );
        }
    }
}