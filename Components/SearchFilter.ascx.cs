using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Domain.DataInterfaces;
using Vevo.Shared.DataAccess;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.Base.Domain;

public partial class Components_SearchFilter : Vevo.WebUI.International.BaseLanguageUserControl
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


    private void ClearAllInput()
    {
        uxValueText.Text = String.Empty;
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
        script += "document.getElementById( '" + uxBooleanDrop.ClientID + "' ).value = 'Yes';";
        script += "document.getElementById( '" + uxLowerText.ClientID + "' ).value = '';";
        script += "document.getElementById( '" + uxUpperText.ClientID + "' ).value = '';";
        script += "document.getElementById( '" + uxStartDateText.ClientID + "' ).value = '';";
        script += "document.getElementById( '" + uxEndDateText.ClientID + "' ).value = '';";

        uxFilterDrop.Attributes.Add( "onchange", script );
    }

    private void TieTextBoxesWithButtons()
    {
        WebUtilities.TieButton( this.Page, uxValueText, uxTextSearchImageButton );

        WebUtilities.TieButton( this.Page, uxLowerText, uxValueRangeSearchImageButton );
        WebUtilities.TieButton( this.Page, uxUpperText, uxValueRangeSearchImageButton );
        WebUtilities.TieButton( this.Page, uxStartDateText, uxDateRangeImageButton );
        WebUtilities.TieButton( this.Page, uxEndDateText, uxDateRangeImageButton );
    }

    private void DisplayTextSearchMessage( string fieldName, string value )
    {
        if (!String.IsNullOrEmpty( value ))
        {
            uxMessageLabel.Text =
                "<br/>" +
                String.Format( GetLanguageText( "TextMessage" ),
                value, fieldName ) +
                "<br/><br/>";
        }
    }

    private void DisplayBooleanSearchMessage( string fieldName, string value )
    {
        bool boolValue;
        if (Boolean.TryParse( value, out boolValue ))
        {
            uxMessageLabel.Text =
                "<br/>" +
                String.Format( GetLanguageText( "BooleanMessage" ),
                ConvertUtilities.ToYesNo( boolValue ), fieldName ) +
                "<br/><br/>";
        }
    }

    private void DisplayValueRangeMessage( string fieldName, string value1, string value2 )
    {
        if (!String.IsNullOrEmpty( value1 ) && !String.IsNullOrEmpty( value2 ))
        {
            uxMessageLabel.Text =
                "<br/>" +
                String.Format( GetLanguageText( "ValueRangeBothMessage" ),
                value1, value2, fieldName ) +
                "<br/><br/>";
        }
        else if (!String.IsNullOrEmpty( value1 ))
        {
            uxMessageLabel.Text =
                "<br/>" +
                String.Format( GetLanguageText( "ValueRangeLowerOnlyMessage" ),
                value1, fieldName ) +
                "<br/><br/>";
        }
        else if (!String.IsNullOrEmpty( value2 ))
        {
            uxMessageLabel.Text =
                "<br/>" +
                String.Format( GetLanguageText( "ValueRangeUpperOnlyMessage" ),
                value2, fieldName ) +
                "<br/><br/>";
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        TieTextBoxesWithButtons();
    }

    protected void uxFilterDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        switch (ParseSearchType( FieldTypes[uxFilterDrop.SelectedValue] ))
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
                OnBubbleEvent( e );
                break;
        }

    }

    protected void uxTextSearchImageButton_Click( object sender, EventArgs e )
    {
        SearchType = SearchFilterType.Text;
        FieldName = uxFilterDrop.SelectedValue;
        FieldValue = uxFilterDrop.SelectedValue;
        Value1 = uxValueText.Text;
        Value2 = String.Empty;

        DisplayTextSearchMessage( uxFilterDrop.SelectedItem.Text, Value1 );

        // Propagate event to parent
        OnBubbleEvent( e );
    }

    protected void uxBooleanSearchButton_Click( object sender, EventArgs e )
    {
        SearchType = SearchFilterType.Boolean;
        FieldName = uxFilterDrop.SelectedValue;
        FieldValue = uxFilterDrop.SelectedValue;
        Value1 = uxBooleanDrop.SelectedValue;
        Value2 = String.Empty;

        DisplayBooleanSearchMessage( uxFilterDrop.SelectedItem.Text, Value1 );

        // Propagate event to parent
        OnBubbleEvent( e );
    }

    protected void uxValueRangeSearchButton_Click( object sender, EventArgs e )
    {
        PopulateValueRangeFilter(
            SearchFilterType.ValueRange,
            uxFilterDrop.SelectedValue,
            uxFilterDrop.SelectedValue,
            uxLowerText.Text,
            uxUpperText.Text,
            e );
    }

    protected void uxDateRangeButton_Click( object sender, EventArgs e )
    {
        PopulateValueRangeFilter(
            SearchFilterType.Date,
            uxFilterDrop.SelectedValue,
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

        DisplayValueRangeMessage( uxFilterDrop.SelectedItem.Text, Value1, Value2 );

        // Propagate event to parent
        OnBubbleEvent( e );
    }

    public void SetUpSchema(
        IList<TableSchemaItem> columnList,
        params string[] excludingColumns )
    {
        uxFilterDrop.Items.Clear();
        FieldTypes.Clear();

        uxFilterDrop.Items.Add( new ListItem( GetLanguageText( "FilterShowAll" ), String.Empty ) );
        FieldTypes[Resources.SearchFilterMessages.FilterShowAll] = SearchFilterType.None.ToString();

        /*[$FilterBy][$LowerBound][$No][$SearchButton][$TextToSearch][$UpperBound][$Yes][$YesNoValue]*/
        for (int i = 0; i < columnList.Count; i++)
        {
            if (!StringUtilities.IsStringInArray( excludingColumns, columnList[i].ColumnName, true ))
            {
                string type = GetFilterType( columnList[i].DataType );
                if (!String.IsNullOrEmpty( type ))
                {
                    uxFilterDrop.Items.Add( new ListItem( "[$" + columnList[i].ColumnName + "]", columnList[i].ColumnName ) );
                    FieldTypes[columnList[i].ColumnName] = type;
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
}
