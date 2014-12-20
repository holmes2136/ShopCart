using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain.DataInterfaces;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.Base.Domain;

public partial class AdminAdvanced_Components_SearchBox : AdminAdvancedBaseUserControl
{
    private SearchFilter _searchFilterObj;

    private NameValueCollection FieldTypes
    {
        get
        {
            if (ViewState["FieldTypes"] == null)
                ViewState["FieldTypes"] = new NameValueCollection();

            return (NameValueCollection) ViewState["FieldTypes"];
        }
    }

    private string GetFilterType( Type dataType )
    {
        string type;

        if (dataType == Type.GetType( "System.Byte" ) ||
            dataType == Type.GetType( "System.SByte" ) ||
            dataType == Type.GetType( "System.Char" ) ||
            dataType == Type.GetType( "System.String" ))
        {
            type = SearchFilter.SearchFilterType.Text.ToString();
        }
        else if (dataType == Type.GetType( "System.Boolean" ))
        {
            type = SearchFilter.SearchFilterType.Boolean.ToString();
        }
        else if (
            dataType == Type.GetType( "System.Decimal" ) ||
            dataType == Type.GetType( "System.Double" ) ||
            dataType == Type.GetType( "System.Int16" ) ||
            dataType == Type.GetType( "System.Int32" ) ||
            dataType == Type.GetType( "System.Int64" ) ||
            dataType == Type.GetType( "System.UInt16" ) ||
            dataType == Type.GetType( "System.UInt32" ) ||
            dataType == Type.GetType( "System.UInt64" ))
        {
            type = SearchFilter.SearchFilterType.ValueRange.ToString();
        }
        else if (dataType == Type.GetType( "System.DateTime" ))
        {
            type = SearchFilter.SearchFilterType.Date.ToString();
        }
        else
        {
            type = String.Empty;
        }

        return type;
    }

    private SearchFilter.SearchFilterType ParseSearchType( string searchFilterType )
    {
        SearchFilter.SearchFilterType type;

        if (String.Compare( searchFilterType, SearchFilter.SearchFilterType.Text.ToString(), true ) == 0)
            type = SearchFilter.SearchFilterType.Text;
        else if (String.Compare( searchFilterType, SearchFilter.SearchFilterType.Boolean.ToString(), true ) == 0)
            type = SearchFilter.SearchFilterType.Boolean;
        else if (String.Compare( searchFilterType, SearchFilter.SearchFilterType.ValueRange.ToString(), true ) == 0)
            type = SearchFilter.SearchFilterType.ValueRange;
        else if (String.Compare( searchFilterType, SearchFilter.SearchFilterType.Date.ToString(), true ) == 0)
            type = SearchFilter.SearchFilterType.Date;
        else
            type = SearchFilter.SearchFilterType.None;

        return type;
    }

    private string JavaObject( string item )
    {
        return String.Format( "document.getElementById('{0}')", item );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        UpdatePanel myTitleUpdatePanel = (UpdatePanel) WebUtilities.FindControlRecursive( this.Page, "uxContentUpdatePanel" );
        AsyncPostBackTrigger titleTrigger = new AsyncPostBackTrigger();
        titleTrigger.ControlID = uxSearchButton.UniqueID;
        myTitleUpdatePanel.Triggers.Add( titleTrigger );

        uxSearchRequiredValidator.ValidationGroup = ValidationGroup;
        uxSearchButton.ValidationGroup = ValidationGroup;
    }

    protected void uxSearchButton_Click( object sender, EventArgs e )
    {
        UrlQuery urlQuery = new UrlQuery();
        if (ParseSearchType( FieldTypes[uxHiddenFilterSearch.Value] )
            != SearchFilter.SearchFilterType.ValueRange)
        {
            PopulateValueFilter(
                SearchFilter.SearchFilterType.Text,
                uxHiddenFilterSearch.Value,
                uxHiddenValueSearch.Value,
                e );

            urlQuery.AddQuery( "Type", _searchFilterObj.FilterType.ToString() );
            urlQuery.AddQuery( "FieldName", _searchFilterObj.FieldName );
            urlQuery.AddQuery( "Value1", _searchFilterObj.Value1 );
        }
        else
        {
            PopulateValueRangeFilter(
                SearchFilter.SearchFilterType.ValueRange,
                uxHiddenFilterSearch.Value,
                uxHiddenValueSearch.Value,
                uxHiddenValueSearch.Value,
                e );

            urlQuery.AddQuery( "Type", _searchFilterObj.FilterType.ToString() );
            urlQuery.AddQuery( "FieldName", _searchFilterObj.FieldName );
            urlQuery.AddQuery( "Value1", _searchFilterObj.Value1 );
            urlQuery.AddQuery( "Value2", _searchFilterObj.Value2 );
        }

        MainContext.RedirectMainControl( ResultPage, urlQuery.RawQueryString );
    }

    private void PopulateValueFilter( SearchFilter.SearchFilterType typeField,
        string value, string val1, EventArgs e )
    {
        _searchFilterObj = SearchFilter.GetFactory()
            .WithFilterType( typeField )
            .WithFieldName( value )
            .WithValue1( val1 )
            .Create();
    }

    private void PopulateValueRangeFilter( SearchFilter.SearchFilterType typeField,
        string value, string val1, string val2, EventArgs e )
    {
        _searchFilterObj = SearchFilter.GetFactory()
            .WithFilterType( typeField )
            .WithFieldName( value )
            .WithValue1( val1 )
            .WithValue2( val2 )
            .Create();
    }

    public void SetUpSchema(
       IList<TableSchemaItem> columnList,
       params string[] excludingColumns )
    {
        uxFilterDrop.Items.Clear();
        FieldTypes.Clear();

        uxFilterDrop.Items.Add( new ListItem( "Select", String.Empty ) );
        FieldTypes["Select"] = SearchFilter.SearchFilterType.None.ToString();

        for (int i = 0; i < columnList.Count; i++)
        {
            if (!StringUtilities.IsStringInArray( excludingColumns, columnList[i].ColumnName, true ))
            {
                string type = GetFilterType( columnList[i].DataType );
                if (!String.IsNullOrEmpty( type ))
                {
                    uxFilterDrop.Items.Add( columnList[i].ColumnName );
                    FieldTypes[columnList[i].ColumnName] = type;
                }
            }
        }
    }

    public void SetUpSchema(
        IList<TableSchemaItem> columnList,
        NameValueCollection renameList,
        params string[] excludingColumns )
    {
        SetUpSchema( columnList, excludingColumns );
        for (int i = 0; i < renameList.Count; i++)
        {
            for (int j = 0; j < columnList.Count; j++)
            {
                if (renameList.AllKeys[i].ToString() == columnList[j].ColumnName)
                {
                    string type = GetFilterType( columnList[j].DataType );
                    if (!String.IsNullOrEmpty( type ))
                    {
                        uxFilterDrop.Items[j + 1].Text = renameList[i].ToString();
                        uxFilterDrop.Items[j + 1].Value = renameList.AllKeys[i].ToString();
                        FieldTypes[renameList[i].ToString()] = type;
                    }
                }
            }
        }
    }

    public string ResultPage
    {
        get
        {
            if (ViewState["ResultPage"] == null)
                return "";
            else
                return ViewState["ResultPage"].ToString();
        }
        set
        {
            ViewState["ResultPage"] = value;
        }
    }

    public string ValidationGroup
    {
        get
        {
            if (ViewState["ValidationGroup"] == null)
                return "";
            else
                return ViewState["ValidationGroup"].ToString();
        }
        set
        {
            ViewState["ValidationGroup"] = value;
        }
    }
}