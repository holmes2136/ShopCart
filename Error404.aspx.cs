using System;
using Vevo.WebUI.International;

public partial class Error404 : BaseLanguagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        uxBack.Attributes.Add( "onClick", "javascript:history.back(); return false;" );
    }
}
