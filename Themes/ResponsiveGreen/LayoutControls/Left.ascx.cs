using System;
using Vevo.WebUI.BaseControls;

public partial class Themes_ResponsiveGreen_LayoutControls_Left : BaseLayoutControl
{
    
    protected void Page_Load( object sender, EventArgs e )
    {
        uxContentMenuNavList.Position = "left";
    }
    
}
