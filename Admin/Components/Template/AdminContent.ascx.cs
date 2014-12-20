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

public partial class AdminAdvanced_Components_Template_AdminContent : System.Web.UI.UserControl
{
    private ITemplate _messageTemplate;
    private ITemplate _validationSummaryTemplate;
    private ITemplate _validationDenotesTemplate;
    private ITemplate _languageControlTemplate;
    private ITemplate _buttonEventTemplate;
    private ITemplate _buttonCommandTemplate;
    private ITemplate _buttonEventInnerBoxTemplate;
    private ITemplate _topContentBoxTemplate;
    private ITemplate _headerMessageTemplate;

    //For Grid Template.
    private ITemplate _filterTemplate;
    private ITemplate _specialFilterTemplate;
    private ITemplate _pageNumberTemplate;
    private ITemplate _gridTemplate;
    private ITemplate _bottomContentBoxTemplate;

    private ITemplate _contentTemplate;
    private ITemplate _plainContentTemplate;
    private string _headerText;
    private bool _showBorderContent = true;

    [TemplateInstance( TemplateInstance.Single )]
    [PersistenceMode( PersistenceMode.InnerProperty )]
    public ITemplate PlainContentTemplate
    {
        get { return _plainContentTemplate; }
        set { _plainContentTemplate = value; }
    }

    [TemplateInstance( TemplateInstance.Single )]
    [PersistenceMode( PersistenceMode.InnerProperty )]
    public ITemplate ContentTemplate
    {
        get { return _contentTemplate; }
        set { _contentTemplate = value; }
    }

    [TemplateInstance( TemplateInstance.Single )]
    [PersistenceMode( PersistenceMode.InnerProperty )]
    public ITemplate MessageTemplate
    {
        get { return _messageTemplate; }
        set { _messageTemplate = value; }
    }

    [TemplateInstance( TemplateInstance.Single )]
    [PersistenceMode( PersistenceMode.InnerProperty )]
    public ITemplate ValidationSummaryTemplate
    {
        get { return _validationSummaryTemplate; }
        set { _validationSummaryTemplate = value; }
    }

    [TemplateInstance( TemplateInstance.Single )]
    [PersistenceMode( PersistenceMode.InnerProperty )]
    public ITemplate ValidationDenotesTemplate
    {
        get { return _validationDenotesTemplate; }
        set { _validationDenotesTemplate = value; }
    }

    [TemplateInstance( TemplateInstance.Single )]
    [PersistenceMode( PersistenceMode.InnerProperty )]
    public ITemplate LanguageControlTemplate
    {
        get { return _languageControlTemplate; }
        set { _languageControlTemplate = value; }
    }

    [TemplateInstance( TemplateInstance.Single )]
    [PersistenceMode( PersistenceMode.InnerProperty )]
    public ITemplate ButtonEventTemplate
    {
        get { return _buttonEventTemplate; }
        set { _buttonEventTemplate = value; }
    }

    [TemplateInstance( TemplateInstance.Single )]
    [PersistenceMode( PersistenceMode.InnerProperty )]
    public ITemplate ButtonCommandTemplate
    {
        get { return _buttonCommandTemplate; }
        set { _buttonCommandTemplate = value; }
    }

    [TemplateInstance( TemplateInstance.Single )]
    [PersistenceMode( PersistenceMode.InnerProperty )]
    public ITemplate ButtonEventInnerBoxTemplate
    {
        get { return _buttonEventInnerBoxTemplate; }
        set { _buttonEventInnerBoxTemplate = value; }
    }

    [TemplateInstance( TemplateInstance.Single )]
    [PersistenceMode( PersistenceMode.InnerProperty )]
    public ITemplate TopContentBoxTemplate
    {
        get { return _topContentBoxTemplate; }
        set { _topContentBoxTemplate = value; }
    }

    [TemplateInstance( TemplateInstance.Single )]
    [PersistenceMode( PersistenceMode.InnerProperty )]
    public ITemplate HeaderMessageTemplate
    {
        get { return _headerMessageTemplate; }
        set { _headerMessageTemplate = value; }
    }

    [TemplateInstance( TemplateInstance.Single )]
    [PersistenceMode( PersistenceMode.InnerProperty )]
    public ITemplate FilterTemplate
    {
        get { return _filterTemplate; }
        set { _filterTemplate = value; }
    }

    [TemplateInstance( TemplateInstance.Single )]
    [PersistenceMode( PersistenceMode.InnerProperty )]
    public ITemplate SpecialFilterTemplate
    {
        get { return _specialFilterTemplate; }
        set { _specialFilterTemplate = value; }
    }

    [TemplateInstance( TemplateInstance.Single )]
    [PersistenceMode( PersistenceMode.InnerProperty )]
    public ITemplate PageNumberTemplate
    {
        get { return _pageNumberTemplate; }
        set { _pageNumberTemplate = value; }
    }

    [TemplateInstance( TemplateInstance.Single )]
    [PersistenceMode( PersistenceMode.InnerProperty )]
    public ITemplate GridTemplate
    {
        get { return _gridTemplate; }
        set { _gridTemplate = value; }
    }

    [TemplateInstance( TemplateInstance.Single )]
    [PersistenceMode( PersistenceMode.InnerProperty )]
    public ITemplate BottomContentBoxTemplate
    {
        get { return _bottomContentBoxTemplate; }
        set { _bottomContentBoxTemplate = value; }
    }


    [PersistenceMode( PersistenceMode.Attribute )]
    public String HeaderText
    {
        get { return _headerText; }
        set { _headerText = value; }
    }

    [PersistenceMode( PersistenceMode.Attribute )]
    public Boolean ShowBorderContent
    {
        get { return _showBorderContent; }
        set { _showBorderContent = value; }
    }

    protected override void OnInit( EventArgs e )
    {
        base.OnInit( e );

        uxMessagePlaceHolder.Controls.Clear();
        uxValidationSummaryPlaceHolder.Controls.Clear();
        uxValidationDenotePlaceHolder.Controls.Clear();
        uxLanguageControlPlaceHolder.Controls.Clear();
        uxButtonEventPlaceHolder.Controls.Clear();
        uxButtonCommandPlaceHolder.Controls.Clear();
        uxButtonEventInnerBoxPlaceHolder.Controls.Clear();
        uxTopContentBoxPlaceHolder.Controls.Clear();

        uxFilterPlaceHolder.Controls.Clear();
        uxSpecialFilterPlaceHolder.Controls.Clear();
        uxPageNumberPlaceHolder.Controls.Clear();
        uxGridPlaceHolder.Controls.Clear();
        uxBottomContentBoxPlaceHolder.Controls.Clear();

        uxContentPlaceHolder.Controls.Clear();

        // Message.
        ContentContainer container = new ContentContainer();
        if (MessageTemplate != null)
        {
            MessageTemplate.InstantiateIn( container );
            uxMessagePlaceHolder.Controls.Add( container );
            uxMessagePlaceHolder.Visible = true;
        }
        else
        {
            uxMessagePlaceHolder.Controls.Add( new LiteralControl( "No Message Defined" ) );
            uxMessagePlaceHolder.Visible = false;
        }

        // Validation Summary
        container = new ContentContainer();
        if (ValidationSummaryTemplate != null)
        {
            ValidationSummaryTemplate.InstantiateIn( container );
            uxValidationSummaryPlaceHolder.Controls.Add( container );
            uxValidationSummaryPanel.Visible = true;
        }
        else
        {
            uxValidationSummaryPlaceHolder.Controls.Add( new LiteralControl( "No Validation Summary Defined" ) );
            uxValidationSummaryPanel.Visible = false;
        }

        // Validation Denotes
        container = new ContentContainer();
        if (ValidationDenotesTemplate != null)
        {
            ValidationDenotesTemplate.InstantiateIn( container );
            uxValidationDenotePlaceHolder.Controls.Add( container );
            uxValidationDenotePanel.Visible = true;
        }
        else
        {
            uxValidationDenotePlaceHolder.Controls.Add( new LiteralControl( "No Validation Denotes Defined" ) );
            uxValidationDenotePanel.Visible = false;
        }

        // If all message disapear message panel will not show.
        if (!uxMessagePlaceHolder.Visible & !uxValidationSummaryPanel.Visible)
            uxMessagePanel.Visible = false;
        else
            uxMessagePanel.Visible = true;

        container = new ContentContainer();
        if (LanguageControlTemplate != null)
        {
            LanguageControlTemplate.InstantiateIn( container );
            uxLanguageControlPlaceHolder.Controls.Add( container );
            uxLanguageControlPanel.Visible = true;
        }
        else
        {
            uxLanguageControlPlaceHolder.Controls.Add( new LiteralControl( "No Language Control Defined" ) );
            uxLanguageControlPanel.Visible = false;
        }

        // If don't have any language and message top panel will not show.
        if (!uxFilterPanel.Visible & !uxLanguageControlPanel.Visible & !uxSpecialFilterPanel.Visible)
            uxTopPagePanel.Visible = false;
        else
            uxTopPagePanel.Visible = true;


        if (ButtonEventTemplate == null)
            uxButtonEventPanel.Visible = false;
        else
        {
            container = new ContentContainer();
            ButtonEventTemplate.InstantiateIn( container );
            uxButtonEventPlaceHolder.Controls.Add( container );
            uxButtonEventPanel.Visible = true;
        }

        if (ButtonCommandTemplate == null)
        {
            uxButtonCommandPanel.Visible = false;
        }
        else
        {
            container = new ContentContainer();
            ButtonCommandTemplate.InstantiateIn( container );
            uxButtonCommandPlaceHolder.Controls.Add( container );
            uxButtonCommandPanel.Visible = true;
        }

        if (ButtonEventInnerBoxTemplate == null)
            uxButtonEventInnerBoxPanel.Visible = false;
        else
        {
            container = new ContentContainer();
            ButtonEventInnerBoxTemplate.InstantiateIn( container );
            uxButtonEventInnerBoxPlaceHolder.Controls.Add( container );
            uxButtonEventInnerBoxPanel.Visible = true;
        }

        container = new ContentContainer();
        if (TopContentBoxTemplate != null)
        {
            TopContentBoxTemplate.InstantiateIn( container );
            uxTopContentBoxPlaceHolder.Controls.Add( container );
            uxTopContentBoxPanel.Visible = true;
        }
        else
        {
            uxTopContentBoxPlaceHolder.Controls.Add( new LiteralControl( "No TopContentBox Content Defined" ) );
            uxTopContentBoxPlaceHolder.Visible = false;
            uxTopContentBoxPanel.Visible = false;
        }

        container = new ContentContainer();
        if (ContentTemplate != null)
        {
            ContentTemplate.InstantiateIn( container );
            uxContentPlaceHolder.Controls.Add( container );
            uxContentPanel.Visible = true;
        }
        else
        {
            uxContentPlaceHolder.Controls.Add( new LiteralControl( "No Template Defined" ) );
            uxContentPanel.Visible = false;
        }

        container = new ContentContainer();
        if (PlainContentTemplate != null)
        {
            PlainContentTemplate.InstantiateIn( container );
            uxPlainContentPlaceHolder.Controls.Add( container );
            uxPlainContentPanel.Visible = true;
        }
        else
        {
            uxPlainContentPlaceHolder.Controls.Add( new LiteralControl( "No Template Defined" ) );
            uxPlainContentPanel.Visible = false;
        }

        if (FilterTemplate == null)
            uxFilterPanel.Visible = false;
        else
        {
            container = new ContentContainer();
            FilterTemplate.InstantiateIn( container );
            uxFilterPlaceHolder.Controls.Add( container );
            uxFilterPanel.Visible = true;
        }

        if (SpecialFilterTemplate == null)
            uxSpecialFilterPanel.Visible = false;
        else
        {
            container = new ContentContainer();
            SpecialFilterTemplate.InstantiateIn( container );
            uxSpecialFilterPlaceHolder.Controls.Add( container );
            uxSpecialFilterPanel.Visible = true;
        }

        if (PageNumberTemplate == null)
            uxPageNumberPanel.Visible = false;
        else
        {
            container = new ContentContainer();
            PageNumberTemplate.InstantiateIn( container );
            uxPageNumberPlaceHolder.Controls.Add( container );
            uxPageNumberPanel.Visible = true;
        }

        if (GridTemplate == null)
            uxGridPanel.Visible = false;
        else
        {
            container = new ContentContainer();
            GridTemplate.InstantiateIn( container );
            uxGridPlaceHolder.Controls.Add( container );
            uxGridPanel.Visible = true;
        }

        if (uxGridPanel.Visible || uxPageNumberPanel.Visible || uxTopContentBoxPlaceHolder.Visible || uxButtonCommandPanel.Visible || uxButtonEventInnerBoxPanel.Visible)
            uxTopContentBoxSet.Visible = true;
        else
            uxTopContentBoxSet.Visible = false;

        if (BottomContentBoxTemplate == null)
        {
            uxBottomContentBoxPlaceHolder.Visible = false;
            uxBottomContentBoxPanel.Visible = false;
        }
        else
        {
            container = new ContentContainer();
            BottomContentBoxTemplate.InstantiateIn( container );
            uxBottomContentBoxPlaceHolder.Controls.Add( container );
            uxBottomContentBoxPlaceHolder.Visible = true;
        }

        if (HeaderMessageTemplate != null)
        {
            HeaderMessageTemplate.InstantiateIn( container );
            uxHeaderMeassagePlaceHolder.Controls.Add( container );
            uxHeaderMeassagePanel.Visible = true;
        }
        else
        {
            uxHeaderMeassagePlaceHolder.Controls.Add( new LiteralControl( "No Template Defined" ) );
            uxHeaderMeassagePanel.Visible = false;
        }

    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!ShowBorderContent)
            uxTopContentBoxSet.CssClass = "CssAdminContentPanel dn";
        else
            uxTopContentBoxSet.CssClass = "CssAdminContentPanel";
    }

}