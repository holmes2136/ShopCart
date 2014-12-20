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

public partial class AdminAdvanced_Components_Template_AdminUserControlContent : System.Web.UI.UserControl
{
    private ITemplate _messageTemplate;
    private ITemplate _validationSummaryTemplate;
    private ITemplate _validationDenotesTemplate;
    private ITemplate _languageControlTemplate;
    private ITemplate _buttonEventTemplate;
    private ITemplate _contentTemplate;
    private ITemplate _plainContentTemplate;
    private ITemplate _topContentBoxTemplate;

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
    public ITemplate TopContentBoxTemplate
    {
        get { return _topContentBoxTemplate; }
        set { _topContentBoxTemplate = value; }
    }

    protected override void OnInit( EventArgs e )
    {
        base.OnInit( e );

        uxMessagePlaceHolder.Controls.Clear();
        uxValidationSummaryPlaceHolder.Controls.Clear();
        uxValidationDenotePlaceHolder.Controls.Clear();
        uxLanguageControlPlaceHolder.Controls.Clear();
        uxButtonEventPlaceHolder.Controls.Clear();
        uxTopContentBoxPlaceHolder.Controls.Clear();
        uxContentPlaceHolder.Controls.Clear();

        // Message.
        ContentContainer container = new ContentContainer();
        if (MessageTemplate != null)
        {
            MessageTemplate.InstantiateIn( container );
            uxMessagePlaceHolder.Controls.Add( container );
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
        }
        else
        {
            uxValidationDenotePlaceHolder.Controls.Add( new LiteralControl( "No Validation Denotes Defined" ) );
            uxValidationDenotePanel.Visible = false;
        }

        container = new ContentContainer();
        if (LanguageControlTemplate != null)
        {
            LanguageControlTemplate.InstantiateIn( container );
            uxLanguageControlPlaceHolder.Controls.Add( container );
        }
        else
        {
            uxLanguageControlPlaceHolder.Controls.Add( new LiteralControl( "No Language Control Defined" ) );
            uxLanguageControlPanel.Visible = false;
        }
        
        container = new ContentContainer();
        if (ButtonEventTemplate != null)
        {
            ButtonEventTemplate.InstantiateIn( container );
            uxButtonEventPlaceHolder.Controls.Add( container );
        }
        else
        {
            uxButtonEventPlaceHolder.Controls.Add( container );
            uxButtonEventPanel.Visible = false;
        }

        container = new ContentContainer();
        if (TopContentBoxTemplate != null)
        {
            TopContentBoxTemplate.InstantiateIn( container );
            uxTopContentBoxPlaceHolder.Controls.Add( container );
        }
        else
        {
            uxTopContentBoxPlaceHolder.Controls.Add( new LiteralControl( "No TopContentBox Content Defined" ) );
            uxTopContentBoxSet.Visible = false;
        }

        container = new ContentContainer();
        if (ContentTemplate != null)
        {
            ContentTemplate.InstantiateIn( container );
            uxContentPlaceHolder.Controls.Add( container );
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
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

}

