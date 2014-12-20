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
using Vevo.WebUI.Widget;
using Vevo.Shared.Utilities;
using Vevo.Domain.Stores;

public partial class Components_Widget : System.Web.UI.UserControl
{
    private WidgetDirector widgetDirector = new WidgetDirector();
    private string _style = string.Empty;
    private string _parameterName = string.Empty;
    private Widget widget;
    private string _linkURL = string.Empty;
    private string _title = string.Empty;

    private void WriteOutput()
    {
        string code = widget.Code;
        if (_style == "AddThis")
        {
            code = code.Replace( "<a class=\"addthis_button\"", "<a class=\"addthis_button\" addthis:url=\"" + _linkURL + "\" addthis:title=\"" + _title + "\" " );
        }
        uxWidgetLabel.Text = code;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        //if (!IsPostBack)
        //{
            PopulateControls(new StoreRetriever().GetStore());
        //}
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (widget == null) return;
        if (widget.Enabled) WriteOutput();
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

    public string LinkURL
    {
        get { return _linkURL; }
        set { _linkURL = value; }
    }

    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }

    public void PopulateControls(Store store)
    {
        try
        {
            widget = new Widget( _style,store );
        }
        catch
        {
            throw new Exception( "Widget: Cannot load configuration" );
        }
    }


}
