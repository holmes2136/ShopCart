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
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.Base.Domain;

public partial class AdminAdvanced_Components_SearchFilter_SearchBooleanPanel : AdminAdvancedBaseUserControl
{
    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    #endregion

    #region Public Methods

    public string GetBoolleanDropClientID()
    {
        return uxBooleanDrop.ClientID;
    }

    public string GetBoolSearchValue()
    {
        return uxBooleanDrop.SelectedValue;
    }

    public void RestoreControls( SearchFilter searchFilterObj )
    {
        uxBooleanDrop.SelectedValue = searchFilterObj.Value1;
    }

    public SearchFilter GetSearchResult( SearchFilter.SearchFilterType typeField, string value, string val1, out string message )
    {
        SearchFilter searchFilterObj = SearchFilter.GetFactory()
                        .WithFilterType( typeField )
                        .WithFieldName( value )
                        .WithValue1( val1 )
                        .Create();
        message = DisplayBooleanSearchMessage( searchFilterObj.FieldName, searchFilterObj.Value1 );

        return searchFilterObj;
    }

    public string DisplayBooleanSearchMessage( string fieldName, string value )
    {
        bool boolValue;
        if (Boolean.TryParse( value, out boolValue ))
        {
            return String.Format( Resources.SearchFilterMessages.BooleanMessage,
                ConvertUtilities.ToYesNo( boolValue ), fieldName );
        }

        return String.Empty;
    }    

    #endregion
}
