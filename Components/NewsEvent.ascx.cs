using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Domain.Stores;

public partial class Components_NewsEvent : Vevo.WebUI.International.BaseLanguageUserControl
{
    private void PopulateNewsEvent()
    {
        IList<News> newsList = DataAccessContext.NewsRepository.GetHotNews( StoreContext.Culture, BoolFilter.ShowTrue ,new StoreRetriever().GetCurrentStoreID());

        uxNewsRepeater.DataSource = newsList;
        uxNewsRepeater.DataBind();

        if (newsList.Count == 0)
        {
            uxNewsMoreDiv.Visible = false;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterJavascript();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "NewsModuleDisplay" ))
        {
            uxNewsEventDiv.Visible = false;
        }
        else
        {
            PopulateNewsEvent();
        }
    }

    private void RegisterJavascript()
    {
        String csname = "jqeuryCycleBlock";
        ClientScriptManager cs = Page.ClientScript;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine( "$(document).ready(function() {" );
        sb.AppendLine( "$('.NewsEventRowStyle').cycle({" );
        sb.AppendLine( String.Format( "cleartypeNoBg:   true," ) );
        sb.AppendLine( String.Format( "fx:     'scrollUp'," ) );
        sb.AppendLine( String.Format( "timeout: {0},", 6000 ) );
        sb.AppendLine( String.Format( "pause: {0} ,", 1 ) );
        sb.AppendLine( String.Format( "delay: {0} ", -3000 ) );
        sb.AppendLine( "});" );
        sb.AppendLine( "});" );


        if (!cs.IsClientScriptBlockRegistered( this.GetType(), csname ))
        {
            cs.RegisterClientScriptBlock( this.GetType(), csname, sb.ToString(), true );
        }
    }

    protected string GetNewsUrl( object newsID, object urlName )
    {
        return UrlManager.GetNewsUrl( newsID, urlName );
    }
}
