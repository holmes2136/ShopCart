using System;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebUI.Widget;

public partial class Admin_Components_SiteConfig_WidgetDetails : AdminAdvancedBaseUserControl, IConfigUserControl
{
    private WidgetDirector widgetDirector = new WidgetDirector();
    private string _style = string.Empty;
    private string _parameterName = string.Empty;
    private Widget widget;
    private string _validationGroup = string.Empty;

    private void ShowEnabled()
    {
        uxWidgetParameterPanel.Visible = true;
        UpdateCodeTypePanel();
    }

    private void ShowDisable()
    {
        uxWidgetParameterPanel.Visible = false;
        UpdateCodeTypePanel();
    }

    private void ShowDefaultCode()
    {
        if (uxWidgetEnableDrop.SelectedValue.Equals( "True" ))
        {
            uxWidgetDefaultPanel.Visible = true;
        }
        else
        {
            uxWidgetDefaultPanel.Visible = false;
        }
        uxWidgetCustomPanel.Visible = false;
    }

    private void ShowCustomCode()
    {
        if (uxWidgetEnableDrop.SelectedValue.Equals( "True" ))
        {
            uxWidgetCustomPanel.Visible = true;
        }
        else
        {
            uxWidgetCustomPanel.Visible = false;
        }
        uxWidgetDefaultPanel.Visible = false;
    }

    private void UpdateCodeTypePanel()
    {
        if (uxWidgetTypeDrop.SelectedValue.Equals( "Default" ))
        {
            ShowDefaultCode();
        }
        else
        {
            ShowCustomCode();
        }
    }

    private string GetDefaultCode()
    {
        WidgetBuilder[] widgetBuilder = widgetDirector.WidgetBuilderSupport;
        for (int i = 0; i < widgetBuilder.Length; i++)
        {
            if (_style == widgetBuilder[i].DefaultWidgetName)
            {
                return widgetBuilder[i].DefaultCode;
            }
        }
        throw new Exception( "Widget: Cannot get default code." );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        uxWidgetEnabledHelp.ConfigName = "Widget" + _style + "IsEnabled";
        uxWidgetTypeHelp.ConfigName = "Widget" + _style + "Type";
        uxWidgetDefaultCodeHelp.ConfigName = "Widget" + _style + "ParameterValue";
        uxWidgetCustomCodeHelp.ConfigName = "Widget" + _style + "Code";
        uxWidgetParameter.Text = _parameterName;
        lcWidgetEnabled.Text = "Enabled " + _style;
        lcWidgetType.Text = "Code Type " + _style;

        if (!MainContext.IsPostBack)
        {
            uxWidgetParameterRequiredValidator.ValidationGroup = _validationGroup;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void uxWidgetEnableDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxWidgetEnableDrop.SelectedValue.Equals( "True" ))
        {
            ShowEnabled();
        }
        else
        {
            ShowDisable();
        }
    }

    protected void uxWidgetTypeDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        UpdateCodeTypePanel();
    }

    protected void uxWidgetCustomLink_Click( object sender, EventArgs e )
    {
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
    }

    public string WidgetStyle
    {
        get { return _style; }
        set
        {
            WidgetBuilder[] widgetBuilder = widgetDirector.WidgetBuilderSupport;
            for (int i = 0; i < widgetBuilder.Length; i++)
            {
                if (value == widgetBuilder[i].DefaultWidgetName)
                {
                    _style = value;
                    return;
                }
            }
            throw new Exception( "Widget: Style is not support." );
        }
    }

    public string ParameterName
    {
        get { return _parameterName; }
        set { _parameterName = value; }
    }

    public void UpdateControlFromWidgetClass()
    {
        if (widget.Enabled)
        {
            ShowEnabled();
            if (widget.IsDefaultCode)
            {
                ShowDefaultCode();
            }
            else
            {
                ShowCustomCode();
            }
        }
        else
        {
            ShowDisable();
        }
    }

    public void PopulateControls( Store store )
    {
        try
        {
            widget = new Widget( _style, store );
        }
        catch (Exception ex)
        {
            SaveLogFile.SaveLog( ex );
            widget = new Widget( _style, GetDefaultCode(), "" );
        }
        uxWidgetEnableDrop.SelectedValue = widget.Enabled.ToString();
        uxWidgetTypeDrop.SelectedValue = widget.CodeType;
        if (widget.CodeType == Widget.DEFAULT_TYPE)
        {
            uxWidgetParameterText.Text = widget.GetParameterValue( 0 );
            uxWidgetCustomCodeText.Text = "";
        }
        else
        {
            uxWidgetParameterText.Text = "";
            uxWidgetCustomCodeText.Text = widget.Code;
        }
        UpdateControlFromWidgetClass();
    }

    public void PopulateControls()
    {
        Store store = DataAccessContext.StoreRepository.GetOne( MainContext.QueryString["Store"] );
        PopulateControls( store );
    }

    public void Update( Store store )
    {
        if (uxWidgetTypeDrop.SelectedValue.Equals( "Default" ))
        {
            uxWidgetCustomCodeText.Text = "";
            string defaultCode = GetDefaultCode();
            widget = new Widget( _style, defaultCode, uxWidgetParameterText.Text );
        }
        else
        {
            uxWidgetParameterText.Text = "";
            widget = new Widget( _style, uxWidgetCustomCodeText.Text );
        }
        widget.Enabled = ConvertUtilities.ToBoolean( uxWidgetEnableDrop.SelectedValue );
        widget.SaveConfig( store );
    }

    public void Update()
    {
        Store store = DataAccessContext.StoreRepository.GetOne( MainContext.QueryString["Store"] );
        Update( store );
    }

    public string ValidationGroup
    {
        get { return _validationGroup; }
        set { _validationGroup = value; }
    }

    #region IConfigUserControl Members
    public void Populate( Vevo.Domain.Configurations.Configuration config )
    {
        PopulateControls();
    }
    #endregion

}
