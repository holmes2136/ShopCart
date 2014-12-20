using System;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Shared.DataAccess;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;

public partial class AdminAdvanced_Components_SearchFilter : AdminAdvancedBaseUserControl, ISearchFilter
{
    private NameValueCollection FieldTypes
    {
        get
        {
            if (ViewState["FieldTypes"] == null)
                ViewState["FieldTypes"] = new NameValueCollection();

            return (NameValueCollection) ViewState["FieldTypes"];
        }
    }

    private string GenerateShowHideScript( bool textPanel, bool boolPanel, bool valueRangePanel, bool dateRangePanel )
    {
        string script;

        if (textPanel)
            script = String.Format( "document.getElementById('{0}').style.display = '';", uxTextPanel.ClientID );
        else
            script = String.Format( "document.getElementById('{0}').style.display = 'none';", uxTextPanel.ClientID );

        if (boolPanel)
            script += String.Format( "document.getElementById('{0}').style.display = '';", uxBooleanPanel.ClientID );
        else
            script += String.Format( "document.getElementById('{0}').style.display = 'none';", uxBooleanPanel.ClientID );

        if (valueRangePanel)
            script += String.Format( "document.getElementById('{0}').style.display = '';", uxValueRangePanel.ClientID );
        else
            script += String.Format( "document.getElementById('{0}').style.display = 'none';", uxValueRangePanel.ClientID );

        if (dateRangePanel)
            script += String.Format( "document.getElementById('{0}').style.display = '';", uxDateRangePanel.ClientID );
        else
            script += String.Format( "document.getElementById('{0}').style.display = 'none';", uxDateRangePanel.ClientID );
        return script;
    }

    private void ShowTextFilterInput()
    {
        uxTextPanel.Style.Add( HtmlTextWriterStyle.Display, "" );
        uxBooleanPanel.Style.Add( HtmlTextWriterStyle.Display, "none" );
        uxValueRangePanel.Style.Add( HtmlTextWriterStyle.Display, "none" );
        uxDateRangePanel.Style.Add( HtmlTextWriterStyle.Display, "none" );
    }

    private void ShowBooleanFilterInput()
    {
        uxTextPanel.Style.Add( HtmlTextWriterStyle.Display, "none" );
        uxBooleanPanel.Style.Add( HtmlTextWriterStyle.Display, "" );
        uxValueRangePanel.Style.Add( HtmlTextWriterStyle.Display, "none" );
        uxDateRangePanel.Style.Add( HtmlTextWriterStyle.Display, "none" );
    }

    private void ShowValueRangeFilterInput()
    {
        uxTextPanel.Style.Add( HtmlTextWriterStyle.Display, "none" );
        uxBooleanPanel.Style.Add( HtmlTextWriterStyle.Display, "none" );
        uxValueRangePanel.Style.Add( HtmlTextWriterStyle.Display, "" );
        uxDateRangePanel.Style.Add( HtmlTextWriterStyle.Display, "none" );
    }

    private void ShowDateRangeFilterInput()
    {
        uxTextPanel.Style.Add( HtmlTextWriterStyle.Display, "none" );
        uxBooleanPanel.Style.Add( HtmlTextWriterStyle.Display, "none" );
        uxValueRangePanel.Style.Add( HtmlTextWriterStyle.Display, "none" );
        uxDateRangePanel.Style.Add( HtmlTextWriterStyle.Display, "" );
    }

    private void HideAllInput()
    {
        uxTextPanel.Style.Add( HtmlTextWriterStyle.Display, "none" );
        uxBooleanPanel.Style.Add( HtmlTextWriterStyle.Display, "none" );
        uxValueRangePanel.Style.Add( HtmlTextWriterStyle.Display, "none" );
        uxDateRangePanel.Style.Add( HtmlTextWriterStyle.Display, "none" );
    }

    private void RegisterDropDownPostback()
    {
        string script = "if(this.value == ''){ javascript:__doPostBack('" + uxFilterDrop.UniqueID + "',''); }";

        foreach (ListItem item in uxFilterDrop.Items)
        {
            switch (ParseSearchType( FieldTypes[item.Value] ))
            {
                case SearchFilterType.Text:
                    script += "if(this.value == '" + item.Value + "'){" + GenerateShowHideScript( true, false, false, false ) + "}";
                    break;

                case SearchFilterType.Boolean:
                    script += "if(this.value == '" + item.Value + "'){" + GenerateShowHideScript( false, true, false, false ) + "}";
                    break;

                case SearchFilterType.ValueRange:
                    script += "if(this.value == '" + item.Value + "'){" + GenerateShowHideScript( false, false, true, false ) + "}";
                    break;

                case SearchFilterType.Date:
                    script += "if(this.value == '" + item.Value + "'){" + GenerateShowHideScript( false, false, false, true ) + "}";
                    break;
            }
        }

        script += "document.getElementById( '" + uxValueText.ClientID + "' ).value = '';";
        script += "document.getElementById( '" + uxBooleanDrop.ClientID + "' ).value = 'True';";
        script += "document.getElementById( '" + uxLowerText.ClientID + "' ).value = '';";
        script += "document.getElementById( '" + uxUpperText.ClientID + "' ).value = '';";
        script += "document.getElementById( '" + uxStartDateText.ClientID + "' ).value = '';";
        script += "document.getElementById( '" + uxEndDateText.ClientID + "' ).value = '';";

        uxFilterDrop.Attributes.Add( "onchange", script );
    }

    private string GetFilterType( Type dataType )
    {
        string type;

        if (dataType == Type.GetType( "System.Byte" ) ||
            dataType == Type.GetType( "System.SByte" ) ||
            dataType == Type.GetType( "System.Char" ) ||
            dataType == Type.GetType( "System.String" ))
        {
            type = SearchFilterType.Text.ToString();
        }
        else if (dataType == Type.GetType( "System.Boolean" ))
        {
            type = SearchFilterType.Boolean.ToString();
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
            type = SearchFilterType.ValueRange.ToString();
        }
        else if (dataType == Type.GetType( "System.DateTime" ))
        {
            type = SearchFilterType.Date.ToString();
        }
        else
        {
            type = String.Empty;
        }

        return type;
    }

    private SearchFilterType ParseSearchType( string searchFilterType )
    {
        SearchFilterType type;

        if (String.Compare( searchFilterType, SearchFilterType.Text.ToString(), true ) == 0)
            type = SearchFilterType.Text;
        else if (String.Compare( searchFilterType, SearchFilterType.Boolean.ToString(), true ) == 0)
            type = SearchFilterType.Boolean;
        else if (String.Compare( searchFilterType, SearchFilterType.ValueRange.ToString(), true ) == 0)
            type = SearchFilterType.ValueRange;
        else if (String.Compare( searchFilterType, SearchFilterType.Date.ToString(), true ) == 0)
            type = SearchFilterType.Date;
        else
            type = SearchFilterType.None;

        return type;
    }

    private void RemoveSearchFilter()
    {
        SearchType = SearchFilterType.None;
        FieldName = String.Empty;
        FieldValue = String.Empty;
        Value1 = String.Empty;
        Value2 = String.Empty;

        uxMessageLabel.Text = String.Empty;
    }

    private void TieTextBoxesWithButtons()
    {
        WebUtilities.TieButton( this.Page, uxValueText, uxTextSearchButton );

        WebUtilities.TieButton( this.Page, uxLowerText, uxValueRangeSearchButton );
        WebUtilities.TieButton( this.Page, uxUpperText, uxValueRangeSearchButton );
        WebUtilities.TieButton( this.Page, uxStartDateText, uxDateRangeButton );
        WebUtilities.TieButton( this.Page, uxEndDateText, uxDateRangeButton );
    }

    private void DisplayTextSearchMessage( string fieldName, string value )
    {
        if (!String.IsNullOrEmpty( value ))
        {
            uxMessageLabel.Text =
                String.Format( Resources.SearchFilterMessages.TextMessage,
                value, fieldName );
        }
    }

    private void DisplayBooleanSearchMessage( string fieldName, string value )
    {
        bool boolValue;
        if (Boolean.TryParse( value, out boolValue ))
        {
            uxMessageLabel.Text =
                String.Format( Resources.SearchFilterMessages.BooleanMessage,
                ConvertUtilities.ToYesNo( boolValue ), fieldName );
        }
    }

    private void DisplayValueRangeMessage( string fieldName, string value1, string value2 )
    {
        if (!String.IsNullOrEmpty( value1 ) && !String.IsNullOrEmpty( value2 ))
        {
            uxMessageLabel.Text =
                String.Format( Resources.SearchFilterMessages.ValueRangeBothMessage,
                value1, value2, fieldName );
        }
        else if (!String.IsNullOrEmpty( value1 ))
        {
            uxMessageLabel.Text =
                String.Format( Resources.SearchFilterMessages.ValueRangeLowerOnlyMessage,
                value1, fieldName );
        }
        else if (!String.IsNullOrEmpty( value2 ))
        {
            uxMessageLabel.Text =
                String.Format( Resources.SearchFilterMessages.ValueRangeUpperOnlyMessage,
                value2, fieldName );
        }
    }

    private void RestoreControls()
    {
        uxFilterDrop.SelectedValue = FieldValue;

        switch (SearchType)
        {
            case SearchFilterType.Text:
                uxValueText.Text = Value1;
                DisplayTextSearchMessage( FieldName, Value1 );
                break;

            case SearchFilterType.Boolean:
                uxBooleanDrop.SelectedValue = ConvertUtilities.ToBoolean( Value1 ).ToString();
                DisplayBooleanSearchMessage( FieldName, Value1 );
                break;

            case SearchFilterType.ValueRange:
                uxLowerText.Text = Value1;
                uxUpperText.Text = Value2;
                DisplayValueRangeMessage( FieldName, Value1, Value2 );
                break;

            case SearchFilterType.Date:
                Value1 = ConvertUtilities.ToDateTime( Value1 ).ToString( "MMMM d,yyyy" );
                Value2 = ConvertUtilities.ToDateTime( Value2 ).ToString( "MMMM d,yyyy" );
                uxStartDateText.Text = Value1;
                uxEndDateText.Text = Value2;
                DisplayValueRangeMessage( FieldName, Value1, Value2 );
                break;
        }
    }

    private void ShowSelectedInput( SearchFilterType filterType )
    {
        switch (filterType)
        {
            case SearchFilterType.Text:
                ShowTextFilterInput();
                break;

            case SearchFilterType.Boolean:
                ShowBooleanFilterInput();
                break;

            case SearchFilterType.ValueRange:
                ShowValueRangeFilterInput();
                break;

            case SearchFilterType.Date:
                ShowDateRangeFilterInput();
                break;

            default:
                HideAllInput();

                RemoveSearchFilter();
                break;
        }
    }

    private bool IsShowAllSelected()
    {
        return String.IsNullOrEmpty( uxFilterDrop.SelectedValue );
    }

    private void LoadDefaultFromQuery()
    {
        if (!String.IsNullOrEmpty( MainContext.QueryString["Type"] ))
            SearchType = EnumUtilities<SearchFilterType>.Parse( MainContext.QueryString["Type"] );

        if (!String.IsNullOrEmpty( MainContext.QueryString["FieldName"] ))
            FieldName = MainContext.QueryString["FieldName"];

        if (!String.IsNullOrEmpty( MainContext.QueryString["FieldValue"] ))
            FieldValue = MainContext.QueryString["FieldValue"];

        if (!String.IsNullOrEmpty( MainContext.QueryString["Value1"] ))
            Value1 = MainContext.QueryString["Value1"];

        if (!String.IsNullOrEmpty( MainContext.QueryString["Value2"] ))
            Value2 = MainContext.QueryString["Value2"];

        ShowSelectedInput( SearchType );
        RestoreControls();
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        TieTextBoxesWithButtons();

        if (!MainContext.IsPostBack)
        {
            LoadDefaultFromQuery();
        }
    }

    protected void uxFilterDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        ShowSelectedInput( ParseSearchType( FieldTypes[uxFilterDrop.SelectedValue] ) );
        if (IsShowAllSelected())
            OnBubbleEvent( e );
    }

    protected void uxTextSearchButton_Click( object sender, EventArgs e )
    {
        SearchType = SearchFilterType.Text;
        FieldName = uxFilterDrop.SelectedItem.ToString();
        FieldValue = uxFilterDrop.SelectedValue;
        Value1 = uxValueText.Text;
        Value2 = String.Empty;

        DisplayTextSearchMessage( FieldName, Value1 );

        // Propagate event to parent
        OnBubbleEvent( e );
    }

    protected void uxBooleanSearchButton_Click( object sender, EventArgs e )
    {
        SearchType = SearchFilterType.Boolean;
        FieldName = uxFilterDrop.SelectedItem.ToString();
        FieldValue = uxFilterDrop.SelectedValue;
        Value1 = uxBooleanDrop.SelectedValue;
        Value2 = String.Empty;

        DisplayBooleanSearchMessage( FieldName, Value1 );

        // Propagate event to parent
        OnBubbleEvent( e );
    }

    protected void uxValueRangeSearchButton_Click( object sender, EventArgs e )
    {
        PopulateValueRangeFilter(
            SearchFilterType.ValueRange,
            uxFilterDrop.SelectedItem.ToString(),
            uxFilterDrop.SelectedValue,
            uxLowerText.Text,
            uxUpperText.Text,
            e );
    }

    protected void uxDateRangeButton_Click( object sender, EventArgs e )
    {
        PopulateValueRangeFilter(
            SearchFilterType.Date,
            uxFilterDrop.SelectedItem.ToString(),
            uxFilterDrop.SelectedValue,
            uxStartDateText.Text,
            uxEndDateText.Text,
            e );
    }

    private void PopulateValueRangeFilter( SearchFilterType typeField,
        string field, string value, string val1, string val2, EventArgs e )
    {
        SearchType = typeField;
        FieldName = field;
        FieldValue = value;
        Value1 = val1;
        Value2 = val2;

        DisplayValueRangeMessage( FieldName, Value1, Value2 );

        // Propagate event to parent
        OnBubbleEvent( e );
    }

    public void SetUpSchema(
        DataColumnCollection columnList,
        params string[] excludingColumns )
    {
        uxFilterDrop.Items.Clear();
        FieldTypes.Clear();

        uxFilterDrop.Items.Add( new ListItem( Resources.SearchFilterMessages.FilterShowAll, String.Empty ) );
        FieldTypes[Resources.SearchFilterMessages.FilterShowAll] = SearchFilterType.None.ToString();

        foreach (DataColumn column in columnList)
        {
            if (!StringUtilities.IsStringInArray( excludingColumns, column.ColumnName, true ))
            {
                string type = GetFilterType( column.DataType );
                if (!String.IsNullOrEmpty( type ))
                {
                    uxFilterDrop.Items.Add( column.ColumnName );
                    FieldTypes[column.ColumnName] = type;
                }
            }
        }
        RegisterDropDownPostback();
    }

    public void SetUpSchema(
        DataColumnCollection columnList,
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
        RegisterDropDownPostback();
    }

    public SearchFilterType SearchType
    {
        get
        {
            if (ViewState["SearchType"] == null)
                return SearchFilterType.None;
            else
                return (SearchFilterType) ViewState["SearchType"];
        }
        set
        {
            ViewState["SearchType"] = value;
        }
    }

    public string FieldName
    {
        get
        {
            if (ViewState["FieldName"] == null)
                return String.Empty;
            else
                return (string) ViewState["FieldName"];
        }
        set
        {
            ViewState["FieldName"] = value;
        }
    }

    public string FieldValue
    {
        get
        {
            if (ViewState["FieldValue"] == null)
                return String.Empty;
            else
                return (string) ViewState["FieldValue"];
        }
        set
        {
            ViewState["FieldValue"] = value;
        }
    }

    public string Value1
    {
        get
        {
            if (ViewState["Value1"] == null)
                return String.Empty;
            else
                return (string) ViewState["Value1"];
        }
        set
        {
            ViewState["Value1"] = value;
        }
    }

    public string Value2
    {
        get
        {
            if (ViewState["Value2"] == null)
                return String.Empty;
            else
                return (string) ViewState["Value2"];
        }
        set
        {
            ViewState["Value2"] = value;
        }
    }

    public void ClearFilter()
    {
        RemoveSearchFilter();

        uxFilterDrop.SelectedValue = "";
        HideAllInput();
    }

    public void UpdateBrowseQuery( UrlQuery urlQuery )
    {
        urlQuery.RemoveQuery( "Type" );
        urlQuery.RemoveQuery( "FieldName" );
        urlQuery.RemoveQuery( "FieldValue" );
        urlQuery.RemoveQuery( "Value1" );
        urlQuery.RemoveQuery( "Value2" );

        if (SearchType != SearchFilterType.None)
        {
            urlQuery.AddQuery( "Type", SearchType.ToString() );
            urlQuery.AddQuery( "FieldName", FieldName );
            urlQuery.AddQuery( "FieldValue", FieldValue );
            urlQuery.AddQuery( "Value1", Value1 );

            if (!String.IsNullOrEmpty( Value2 ))
                urlQuery.AddQuery( "Value2", Value2 );
        }
    }

}
