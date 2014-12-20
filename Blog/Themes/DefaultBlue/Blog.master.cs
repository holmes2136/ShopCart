using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Domain;
using System.Text;
using System.Web.Security;
using Vevo.Deluxe.WebUI.Marketing;

public partial class Blog_Themes_DefaultBlue_Blog : Vevo.WebUI.BaseControls.BaseBlogMasterPage
{
    private void RegisterFacebookCommentScript()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "FacebookCommentEnabled" ))
        {
            string appId = DataAccessContext.Configurations.GetValue( "FacebookCommentAPIKey" );

            StringBuilder sb = new StringBuilder();
            sb.Append( "(function(d, s, id) {" );
            sb.Append( "    var js,fjs = d.getElementsByTagName(s)[0];" );
            sb.Append( "    if (d.getElementById(id)) return;" );
            sb.Append( "    js = d.createElement(s); js.id = id;" );
            sb.Append( "    js.src = \"//connect.facebook.net/en_US/all.js#xfbml=1&appId=" + appId + "\";" );
            sb.Append( "    fjs.parentNode.insertBefore(js, fjs);" );
            sb.Append( " } (document, 'script', 'facebook-jssdk')" );
            sb.Append( " );" );
            sb.AppendLine();

            String csname = "FacebookCommentBoxScript";
            ClientScriptManager cs = Page.ClientScript;
            if (!cs.IsClientScriptBlockRegistered( this.GetType(), csname ))
            {
                cs.RegisterClientScriptBlock( this.GetType(), csname, sb.ToString(), true );
            }
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (DataAccessContext.Configurations.GetBoolValue( "RestrictAccessToShop" ))
        {
            if (!Page.User.Identity.IsAuthenticated)
            {
                if (IsRestrictedAccessPage())
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
            }
        }

        AffiliateHelper.SetAffiliateCookie( AffiliateCode );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        RegisterFacebookCommentScript();
    }
}
