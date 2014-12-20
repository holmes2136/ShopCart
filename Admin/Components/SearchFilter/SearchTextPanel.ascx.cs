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
using Vevo.Domain.DataInterfaces;
using Vevo.WebAppLib;
using Vevo.Base.Domain;

public partial class AdminAdvanced_Components_SearchFilter_SearchTextPanel : AdminAdvancedBaseUserControl
{
    #region Private

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    #endregion

    #region Public Methods

    public string GetValueTextClientID()
    {
        return uxValueText.ClientID;
    }

    public string GetTextSearchValue()
    {
        return uxValueText.Text;
    }

    public SearchFilter GetSearchResult( SearchFilter.SearchFilterType typeField,
         string value, string val1, out string message )
    {
        SearchFilter searchFilterObj = SearchFilter.GetFactory()
          .WithFilterType( typeField )
          .WithFieldName( value )
          .WithValue1( val1 )
          .Create();

        message = DisplayTextSearchMessage( searchFilterObj.FieldName, searchFilterObj.Value1 );
        return searchFilterObj;
    }

    public void RestoreControls( SearchFilter searchFilterObj )
    {
        uxValueText.Text = searchFilterObj.Value1;
    }

    public string DisplayTextSearchMessage( string fieldName, string value )
    {
        if (!String.IsNullOrEmpty( value ))
        {
            return String.Format( Resources.SearchFilterMessages.TextMessage,
                value, fieldName );
        }
        return String.Empty;
    }

    public void TieTextBoxesWithButtons( Control button )
    {
        WebUtilities.TieButton( this.Page, uxValueText, button );
    }

    #endregion
}
