using System;

public partial class GenericError : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        uxBack.Attributes.Add( "onClick", "javascript:history.back(); return false;" );
    }
}
