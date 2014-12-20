using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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

public partial class AdminAdvanced_Components_SearchFilter_SearchFilter : AdminAdvancedBaseUserControl, ISearchFilter
{
    #region Private

    private void SetUpFilterStyle()
    {
        if (IsExportFilter)
        {
            uxFilterTopTR.Attributes["Class"] = "ExportFilter";
            uxFilterLeftTR.Attributes["Class"] = "ExportFilterDefault";
            uxFilterRightTR.Attributes["Class"] = "ExportFilterDefault";
            uxTextPanel.CssClass = "ExportFilterValuePanel";
            uxBooleanPanel.CssClass = "ExportFilterValuePanel";
            uxValueRangePanel.CssClass = "ExportFilterValuePanel";
            uxDateRangePanel.CssClass = "ExportFilterValuePanel";
            uxFilterSpace.Attributes["Class"] = "ExportFilterDefault";
            uxFilterMessageTR.Attributes["Class"] = "ExportFilterMessage";

            uxMessageLabel.Visible = false;
            uxSearchTextButton.Visible = false;
            uxSearchBooleanButton.Visible = false;
            uxSearchRangeButton.Visible = false;
            uxSearchDateRangeButton.Visible = false;
        }
        else
        {
            uxFilterTopTR.Attributes["Class"] = "SearchFilter";
            uxFilterLeftTR.Attributes["Class"] = "SearchFilterDefault";
            uxFilterRightTR.Attributes["Class"] = "SearchFilterLabel";
            uxTextPanel.CssClass = "SearchFilterValuePanel";
            uxBooleanPanel.CssClass = "SearchFilterValuePanel";
            uxValueRangePanel.CssClass = "SearchFilterValuePanel";
            uxDateRangePanel.CssClass = "SearchFilterValuePanel";
            uxFilterSpace.Style.Add( HtmlTextWriterStyle.Display, "none" );
            uxFilterMessageTR.Attributes["Class"] = "SearchFilterMessage";
        }
    }

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
                case SearchFilter.SearchFilterType.Text:
                    script += "if(this.value == '" + item.Value + "'){" + GenerateShowHideScript( true, false, false, false ) + "}";
                    break;

                case SearchFilter.SearchFilterType.Boolean:
                    script += "if(this.value == '" + item.Value + "'){" + GenerateShowHideScript( false, true, false, false ) + "}";
                    break;

                case SearchFilter.SearchFilterType.ValueRange:
                    script += "if(this.value == '" + item.Value + "'){" + GenerateShowHideScript( false, false, true, false ) + "}";
                    break;

                case SearchFilter.SearchFilterType.Date:
                    script += "if(this.value == '" + item.Value + "'){" + GenerateShowHideScript( false, false, false, true ) + "}";
                    break;
            }
        }

        script += "document.getElementById( '" + uxSearchTextPanel.GetValueTextClientID() + "').value = '';";
        script += "document.getElementById( '" + uxSearchBooleanPanel.GetBoolleanDropClientID() + "').value = 'True';";
        script += "document.getElementById( '" + uxSearchRangePanel.GetLowerTextClientID() + "').value = '';";
        script += "document.getElementById( '" + uxSearchRangePanel.GetUpperTextClientID() + "').value = '';";
        script += "document.getElementById( '" + uxSearchDateRangePanel.GetStartDateTextClientID() + "').value = '';";
        script += "document.getElementById( '" + uxSearchDateRangePanel.GetEndDateTextClientID() + "').value = '';";

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

    private void RemoveSearchFilter()
    {
        SearchFilterObj = SearchFilter.GetFactory()
            .Create();

        uxMessageLabel.Text = String.Empty;
    }

    private void TieTextBoxesWithButtons()
    {
        if (!IsExportFilter)
        {
            uxSearchTextPanel.TieTextBoxesWithButtons( uxSearchTextButton );            
            uxSearchRangePanel.TieTextBoxesWithButtons( uxSearchRangeButton );
            uxSearchDateRangePanel.TieTextBoxesWithButtons( uxSearchDateRangeButton );
        }
    }

    private void DisplayTextSearchMessage( string fieldName, string value )
    {
        uxMessageLabel.Text = uxSearchTextPanel.DisplayTextSearchMessage( fieldName, value );
    }

    private void DisplayBooleanSearchMessage( string fieldName, string value )
    {
        uxMessageLabel.Text = uxSearchBooleanPanel.DisplayBooleanSearchMessage( fieldName, value );
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
        uxFilterDrop.SelectedValue = SearchFilterObj.FieldName;

        switch (SearchFilterObj.FilterType)
        {
            case SearchFilter.SearchFilterType.Text:
                uxSearchTextPanel.RestoreControls( SearchFilterObj );
                uxMessageLabel.Text = uxSearchTextPanel.DisplayTextSearchMessage( SearchFilterObj.FieldName, SearchFilterObj.Value1 );
                break;

            case SearchFilter.SearchFilterType.Boolean:
                uxSearchBooleanPanel.RestoreControls( SearchFilterObj );
                uxMessageLabel.Text = uxSearchBooleanPanel.DisplayBooleanSearchMessage( SearchFilterObj.FieldName, SearchFilterObj.Value1 );
                break;

            case SearchFilter.SearchFilterType.ValueRange:
                uxSearchRangePanel.RestoreControls( SearchFilterObj );
                DisplayValueRangeMessage( SearchFilterObj.FieldName, SearchFilterObj.Value1, SearchFilterObj.Value2 );
                break;

            case SearchFilter.SearchFilterType.Date:
                uxSearchDateRangePanel.RestoreControls( SearchFilterObj );
                DisplayValueRangeMessage( SearchFilterObj.FieldName,
                    uxSearchDateRangePanel.GetStartDateText(), uxSearchDateRangePanel.GetEndDateText() );
                break;
        }
    }

    private void ShowSelectedInput( SearchFilter.SearchFilterType filterType )
    {
        switch (filterType)
        {
            case SearchFilter.SearchFilterType.Text:
                ShowTextFilterInput();
                break;

            case SearchFilter.SearchFilterType.Boolean:
                ShowBooleanFilterInput();
                break;

            case SearchFilter.SearchFilterType.ValueRange:
                ShowValueRangeFilterInput();
                break;

            case SearchFilter.SearchFilterType.Date:
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
        {
            SearchFilter.SearchFilterType type = (SearchFilter.SearchFilterType) Enum.Parse( typeof( SearchFilter.SearchFilterType ), MainContext.QueryString["Type"], true );

            string fieldName = MainContext.QueryString["FieldName"];
            string value1 = MainContext.QueryString["Value1"];
            string value2 = MainContext.QueryString["Value2"];

            if (!String.IsNullOrEmpty( value2 ))
            {
                PopulateValueRangeFilter(
                    type,
                    fieldName,
                    value1,
                    value2 );
            }
            else
            {
                PopulateValueFilter(
                    type,
                    fieldName,
                    value1 );
            }
        }

        ShowSelectedInput( SearchFilterObj.FilterType );
        RestoreControls();
    }


    private void PopulateValueFilter( SearchFilter.SearchFilterType typeField,
        string value, string val1, EventArgs e )
    {
        PopulateValueFilter(
            typeField,
            value,
            val1 );

        // Propagate event to parent
        OnBubbleEvent( e );
    }

    private void PopulateValueFilter( SearchFilter.SearchFilterType typeField,
         string value, string val1 )
    {
        string message = String.Empty;

        if (typeField == SearchFilter.SearchFilterType.Text)
        {
            SearchFilterObj = uxSearchTextPanel.GetSearchResult( typeField, value, val1, out message );
        }
        else if (typeField == SearchFilter.SearchFilterType.Boolean)
        {
            SearchFilterObj = uxSearchBooleanPanel.GetSearchResult( typeField, value, val1, out message );
        }
        uxMessageLabel.Text = message;
    }

    private void PopulateValueRangeFilter( SearchFilter.SearchFilterType typeField,
        string value, string val1, string val2, EventArgs e )
    {
        PopulateValueRangeFilter(
            typeField,
            value,
            val1,
            val2 );

        // Propagate event to parent
        OnBubbleEvent( e );
    }

    private void PopulateValueRangeFilter( SearchFilter.SearchFilterType typeField,
        string value, string val1, string val2 )
    {
        SearchFilterObj = SearchFilter.GetFactory()
            .WithFilterType( typeField )
            .WithFieldName( value )
            .WithValue1( val1 )
            .WithValue2( val2 )
            .Create();

        DisplayValueRangeMessage( SearchFilterObj.FieldName, SearchFilterObj.Value1, SearchFilterObj.Value2 );
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        TieTextBoxesWithButtons();

        if (!MainContext.IsPostBack)
        {
            LoadDefaultFromQuery();
            SetUpFilterStyle();
        }
    }

    protected void uxFilterDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        ShowSelectedInput( ParseSearchType( FieldTypes[uxFilterDrop.SelectedValue] ) );
        if (IsShowAllSelected())
            OnBubbleEvent( e );
    }

    protected void uxSearchButton_Click( object sender, EventArgs e )
    {
        GetSearchFilterObj( e );
    }

    #endregion

    #region Public Methods

    public bool IsExportFilter
    {
        get
        {
            if (ViewState["IsExportFilter"] == null)
                ViewState["IsExportFilter"] = false;
            return ConvertUtilities.ToBoolean( ViewState["IsExportFilter"] );
        }
        set
        {
            ViewState["IsExportFilter"] = value;
            SetUpFilterStyle();
        }
    }

    public void SetUpSchema(
        IList<TableSchemaItem> columnList,
        params string[] excludingColumns )
    {
        uxFilterDrop.Items.Clear();
        FieldTypes.Clear();

        uxFilterDrop.Items.Add( new ListItem( Resources.SearchFilterMessages.FilterShowAll, String.Empty ) );
        FieldTypes[Resources.SearchFilterMessages.FilterShowAll] = SearchFilter.SearchFilterType.None.ToString();

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
        RegisterDropDownPostback();
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
        RegisterDropDownPostback();
    }

    public SearchFilter SearchFilterObj
    {
        get
        {
            if (ViewState["SearchFilter"] == null)
                return SearchFilter.GetFactory()
                    .Create();
            else
                return (SearchFilter) ViewState["SearchFilter"];
        }
        set
        {
            ViewState["SearchFilter"] = value;
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

        if (SearchFilterObj.FilterType != SearchFilter.SearchFilterType.None)
        {
            urlQuery.AddQuery( "Type", SearchFilterObj.FilterType.ToString() );
            urlQuery.AddQuery( "FieldName", SearchFilterObj.FieldName );
            urlQuery.AddQuery( "Value1", SearchFilterObj.Value1 );

            if (!String.IsNullOrEmpty( SearchFilterObj.Value2 ))
                urlQuery.AddQuery( "Value2", SearchFilterObj.Value2 );
        }
    }

    public void TieGenerateTextBox( Control button )
    {
        uxSearchTextPanel.TieTextBoxesWithButtons( button );        
        uxSearchRangePanel.TieTextBoxesWithButtons( button );
        uxSearchDateRangePanel.TieTextBoxesWithButtons( button );
    }

    public void GetSearchFilterObj( EventArgs e )
    {
        SearchFilter.SearchFilterType filterType = ParseSearchType( FieldTypes[uxFilterDrop.SelectedValue] );
        if (filterType == SearchFilter.SearchFilterType.Text)
        {
            PopulateValueFilter(
                SearchFilter.SearchFilterType.Text,
                uxFilterDrop.SelectedValue,
                uxSearchTextPanel.GetTextSearchValue(),
                e );
        }

        if (filterType == SearchFilter.SearchFilterType.Boolean)
        {
            PopulateValueFilter(
                SearchFilter.SearchFilterType.Boolean,
                uxFilterDrop.SelectedValue,
                uxSearchBooleanPanel.GetBoolSearchValue(),
                e );
        }

        if (filterType == SearchFilter.SearchFilterType.ValueRange)
        {
            PopulateValueRangeFilter(
            SearchFilter.SearchFilterType.ValueRange,
            uxFilterDrop.SelectedValue,
            uxSearchRangePanel.GetLowerValue(),
            uxSearchRangePanel.GetUpperValue(),
            e );
        }

        if (filterType == SearchFilter.SearchFilterType.Date)
        {
            PopulateValueRangeFilter(
           SearchFilter.SearchFilterType.Date,
           uxFilterDrop.SelectedValue,
           uxSearchDateRangePanel.GetStartDateText(),
           uxSearchDateRangePanel.GetEndDateText(),
           e );
        }
    }


    #endregion
}
