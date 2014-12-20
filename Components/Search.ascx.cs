using System;
using Vevo.Domain;

public partial class Themes_ResponsiveGreen_Components_Search : Vevo.WebUI.International.BaseLanguageUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "SearchModuleDisplay" ))
            this.Visible = false;
        
    }
}
