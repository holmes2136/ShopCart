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

public partial class Components_ManufacturerTab : System.Web.UI.UserControl
{
    private IList<Manufacturer> GetManufacturerList()
    {
        return DataAccessContext.ManufacturerRepository.GetAll( StoreContext.Culture, BoolFilter.ShowTrue, "SortOrder" );
    }

    private void PopulateManufacturerControls()
    {
        uxList.DataSource = GetManufacturerList();
        uxList.DataBind();
    }

    private void RegisterScriptaculousJavaScript()
    {
        String csname = "jqeuryCycleBlock";
        ClientScriptManager cs = Page.ClientScript;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine( "$(window).load(function(){(function(){" );
        sb.AppendLine( "var trigger = $('.ManufacturerPanelTrigger');" );
        sb.AppendLine( "var panel = trigger.find('.ManufacturerPanel').hide();" );
        sb.AppendLine( "var main = $(document).find('.MainDiv');" );
        sb.AppendLine( "panel.css('left', ($(window).width()- main.width())/2 + 'px');" );
        sb.AppendLine( "panel.css('width', (main.width()) + 'px');" );
        sb.AppendLine( "trigger.mouseenter(function()" );
        sb.AppendLine( "{panel.slideDown('fast');}).mouseleave(function()" );
        sb.AppendLine( "{panel.slideUp('fast');});" );
        sb.AppendLine( "})();});" );

        if (!cs.IsClientScriptBlockRegistered( this.GetType(), csname ))
        {
            cs.RegisterClientScriptBlock( this.GetType(), csname, sb.ToString(), true );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterScriptaculousJavaScript();

        uxList.RepeatColumns = 4;
        uxList.RepeatDirection = RepeatDirection.Horizontal;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        Refresh();
    }

    public void Refresh()
    {
        PopulateManufacturerControls();
    }
}