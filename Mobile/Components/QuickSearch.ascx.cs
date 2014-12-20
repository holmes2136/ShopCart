using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using Vevo.WebAppLib;

public partial class Mobile_Components_QuickSearch : Vevo.WebUI.International.BaseLanguageUserControl
{
    string param = String.Empty;
    private void RegisterScript()
    {
        uxSearchText.Text = "[$Search]";
        uxSearchText.Attributes.Add( "onblur", "javascript:if(this.value=='') this.value=" + "[$Search]" + "';" );
        uxSearchText.Attributes.Add( "onfocus", "javascript:if(this.value=='" + "[$Search]" + "') this.value='';" );
    }
    private void ResultSearch()
    {
        Response.Redirect( "AdvancedSearchResult.aspx?" +
            "Keyword=" + param );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "SearchModuleDisplay" ))
            this.Visible = false;

        if (!IsPostBack)
        {
            RegisterScript();
        }
    }

    protected void uxSearchButton_Click( object sender, EventArgs e )
    {
        if (uxSearchText.Text != "Insert Keyword")
            param = uxSearchText.Text;

        ResultSearch();
    }

   

}
