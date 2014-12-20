using System;
using System.Web.UI;
using Vevo.WebUI.International;

public partial class Blog_Components_Search : BaseLanguageUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void uxSearchButton_Click( object sender, EventArgs e )
    {
        Response.Redirect( "./SearchResult.aspx?Search=" + uxSearchText.Text.Trim() );
    }
}
