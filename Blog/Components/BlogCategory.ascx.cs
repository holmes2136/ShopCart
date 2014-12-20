using System;
using System.Text;
using System.Web.UI;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class Blog_Components_BlogCategory : BaseLanguageUserControl
{
    private void RegisterScript()
    {
        String csname = "toggle";

        StringBuilder sb = new StringBuilder();
        ClientScriptManager cs = Page.ClientScript;
        sb.AppendLine( "$(document).ready(function(){ " );
        sb.AppendLine( "   $('.BlogCategoryBox .BlogSidebarTop').click(function () {" );
        sb.AppendLine( "        $('.BlogCategoryBox .BlogSidebarTop').toggleClass('BlogCategoryBox SidebarTopHide');" );
        sb.AppendLine( "        $('.BlogCategoryBox .BlogSidebarLeft').slideToggle('slow');});" );
        sb.AppendLine( "});" );

        if (!cs.IsClientScriptBlockRegistered( this.GetType(), csname ))
        {
            cs.RegisterClientScriptBlock( this.GetType(), csname, sb.ToString(), true );
        }
    }

    private void PopulateControls()
    {
        uxBlogCategoryList.DataSource = DataAccessContext.BlogCategoryRepository.GetAll( StoreContext.Culture, BoolFilter.ShowTrue, "SortOrder" );
        uxBlogCategoryList.DataBind();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterScript();

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    protected string GetBlogCategoryName( string name, string categoryID )
    {
        if (DataAccessContext.Configurations.GetBoolValue( "BlogCountInCategoryBox", StoreContext.CurrentStore ))
        {
            int count = DataAccessContext.BlogRepository.GetBlogCountByCategoryID( categoryID, StoreContext.CurrentStore.StoreID, BoolFilter.ShowTrue );
            name += " (" + count.ToString() + ")";
        }
        return name;
    }
}