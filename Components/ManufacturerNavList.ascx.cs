using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.WebUI;
using Vevo.Domain;
using Vevo.Domain.Products;
using System.Text;


public partial class Components_ManufacturerNavList : Vevo.WebUI.International.BaseLanguageUserControl
{
    private void Components_ManufacturerNavList_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        Refresh();
    }

    private void Refresh()
    {
        PopulateControls();
    }

    private void PopulateControls()
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "EnableManufacturer" ))
        {
            this.Visible = false;
        }
        else
        {
            switch (DataAccessContext.Configurations.GetValue( "ManufacturerMenuType" ))
            {
                case "default":
                    uxNormalList.Visible = true;
                    break;

                case "dropdown":
                    uxDropDown.Visible = true;
                    break;

                default:
                    uxNormalList.Visible = true;
                    break;
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( Components_ManufacturerNavList_StoreCultureChanged );

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }
}