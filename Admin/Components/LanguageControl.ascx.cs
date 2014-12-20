using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using Vevo.WebAppLib;


public partial class AdminAdvanced_Components_LanguageControl
    : AdminAdvancedBaseUserControl, ILanguageControl
{
    private void UpdateLanguageHidden()
    {
        if (uxDrop.SelectedItem != null)
        {
            uxLanguageHidden.Value = uxDrop.SelectedItem.Text;
        }
        else
        {
            uxLanguageHidden.Value = DataAccessContext.CultureRepository.GetOne( CurrentCultureID ).Name;
        }
    }

    public void PopulateControls()
    {
        uxDrop.DataSource = DataAccessContext.CultureRepository.GetAll();
        uxDrop.DataBind();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        uxDrop.SelectedValue = CurrentCultureID;

        UpdateLanguageHidden();

        if (!MainContext.IsPostBack)
            PopulateControls();
    }

    protected void uxDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        CurrentCultureID = uxDrop.SelectedValue;

        // Send event to parent controls
        OnBubbleEvent( e );
    }


    public string CurrentCultureID
    {
        get
        {
            return AdminConfig.CurrentContentCultureID;
        }
        set
        {
            AdminConfig.CurrentContentCultureID = value;
        }
    }

    public Culture CurrentCulture
    {
        get
        {
            return DataAccessContext.CultureRepository.GetOne( CurrentCultureID );
        }
    }

    public bool ShowTitle
    {
        get
        {
            return uxTitleLabel.Visible;
        }
        set
        {
            uxTitleLabel.Visible = value;
        }
    }

    public bool ShowLanguageHeader
    {
        get
        {
            return uxLanguageHeaderLabel.Visible;
        }
        set
        {
            uxLanguageHeaderLabel.Visible = value;
        }
    }

    public bool ShowLanguageDescription
    {
        get
        {
            return uxLanguageDescriptionLabel.Visible;
        }
        set
        {
            uxLanguageDescriptionLabel.Visible = value;
        }
    }

    public void UpdateBrowseQuery( UrlQuery urlQuery )
    {
    }

}
