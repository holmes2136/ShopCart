using System;
using Vevo.WebUI.International;

public partial class Blog_Components_RecentPost : BaseLanguageUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if ( !IsPostBack )
        {
            uxNormalList.Refresh();
            uxRecentPostDiv.Visible = uxNormalList.HasRecentPostItem();
        }
    }
}
