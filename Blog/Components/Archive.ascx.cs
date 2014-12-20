using System;
using System.Text;
using System.Web.UI;
using Vevo.Domain;
using Vevo.WebUI.International;

public partial class Blog_Components_Archive : BaseLanguageUserControl
{
    private void RegisterScript()
    {
        String csname = "toggle";

        StringBuilder sb = new StringBuilder();
        ClientScriptManager cs = Page.ClientScript;
        sb.AppendLine( "$(document).ready(function(){ " );
        sb.AppendLine( "   $('.ArchiveBox .BlogSidebarLeft').hide();" );
        sb.AppendLine( "   $('.ArchiveBox .SidebarTopTitle').click(function () {" );
        sb.AppendLine( "        $('.ArchiveBox .SidebarTopTitle').toggleClass('ArchiveBox SidebarTopTitleHide');" );
        sb.AppendLine( "        $('.ArchiveBox .BlogSidebarTop').next('.BlogSidebarLeft').slideToggle('slow');});" );
        sb.AppendLine( "});" );

        if (!cs.IsClientScriptBlockRegistered( this.GetType(), csname ))
        {
            cs.RegisterClientScriptBlock( this.GetType(), csname, sb.ToString(), true );
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        RegisterScript();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            uxNormalList.Refresh();
        }
        if (DataAccessContext.Configurations.GetBoolValue( "DisplayArchivesEnabled" ))
        {
            uxArchiveListDiv.Visible = uxNormalList.HasArchiveItem();
        }
        else
        {
            uxArchiveListDiv.Visible = false;
        }
    }
}
