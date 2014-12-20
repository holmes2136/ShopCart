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

public partial class AdminAdvanced_Components_BoxSet_BoxSet : System.Web.UI.UserControl
{
    private ITemplate _titleTemplate;
    private ITemplate _contentTemplate;

    protected override void OnInit( EventArgs e )
    {
        base.OnInit( e );

        uxTitlePlaceHolder.Controls.Clear();
        uxContentPlaceHolder.Controls.Clear();

        ContentContainer container = new ContentContainer();
        if (TitleTemplate != null)
        {
            TitleTemplate.InstantiateIn( container );
            uxTitlePlaceHolder.Controls.Add( container );
        }
        else
            uxTitlePlaceHolder.Visible = false;

        container = new ContentContainer();
        if (ContentTemplate != null)
        {
            ContentTemplate.InstantiateIn( container );
            uxContentPlaceHolder.Controls.Add( container );
        }
        else
            uxContentPanel.CssClass = "Clear";
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        uxBoxSetPanel.CssClass = String.Format( "{0}", CssClass );
    }

    [TemplateInstance( TemplateInstance.Single )]
    [PersistenceMode( PersistenceMode.InnerProperty )]
    public ITemplate TitleTemplate
    {
        get { return _titleTemplate; }
        set { _titleTemplate = value; }
    }

    [TemplateInstance( TemplateInstance.Single )]
    [PersistenceMode( PersistenceMode.InnerProperty )]
    public ITemplate ContentTemplate
    {
        get { return _contentTemplate; }
        set { _contentTemplate = value; }
    }

    [PersistenceMode( PersistenceMode.Attribute )]
    public string CssClass
    {
        get { return uxBoxSetPanel.CssClass; }
        set { uxBoxSetPanel.CssClass = value; }
    }

    [PersistenceMode( PersistenceMode.Attribute )]
    public Unit Width
    {
        get { return uxBoxSetPanel.Width; }
        set { uxBoxSetPanel.Width = value; }
    }
}
