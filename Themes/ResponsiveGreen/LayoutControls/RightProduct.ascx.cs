using System;
using Vevo.WebUI.BaseControls;
using System.IO;

public partial class Themes_ResponsiveGreen_LayoutControls_RightProduct : BaseLayoutControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        uxContentMenuNavList.Position = "right";
        
    }
     protected void Page_PreRender( object sender, EventArgs e )
    {

        string filename = Path.GetFileName(Request.Path).ToLower();
        //if (filename == "manufacturer.aspx")
        //    uxNewArrivalList.Visible = false;
        
    }
    
}
