using System;
using System.Drawing;
using System.Web.UI;

public partial class Components_ProductListViewType : Vevo.WebUI.International.BaseLanguageUserControl
{
    public string CurrentSelectedView
    {
        get
        {
            if (ViewState["CurrentSelectedView"] == null)
                ViewState["CurrentSelectedView"] = "Grid";

            return (string) ViewState["CurrentSelectedView"];
        }
        set
        {
            ViewState["CurrentSelectedView"] = value;
        }
    }

    public string SelectedView
    {
        get
        {
            return CurrentSelectedView;
        }
        set
        {
            CurrentSelectedView = value;
        }
    }

    public void SetViewTypeText( string value )
    {
        if (value == "List")
        {
            uxListViewLinkButton.Enabled = false;
            uxGridViewLinkButton.Enabled = true;
            uxTableViewLinkButton.Enabled = true;
            uxListViewLinkButton.CssClass = "ProductListViewButtonDisable";
            uxGridViewLinkButton.CssClass = "ProductGridViewButton";
            uxTableViewLinkButton.CssClass = "ProductTableViewButton";
            SelectedView = "List";
        }
        else if (value == "Grid")
        {
            uxGridViewLinkButton.Enabled = false;
            uxListViewLinkButton.Enabled = true;
            uxTableViewLinkButton.Enabled = true;
            uxGridViewLinkButton.CssClass = "ProductGridViewButtonDisable";
            uxListViewLinkButton.CssClass = "ProductListViewButton";
            uxTableViewLinkButton.CssClass = "ProductTableViewButton";
            SelectedView = "Grid";
        }
        else
        {
            uxGridViewLinkButton.Enabled = true;
            uxListViewLinkButton.Enabled = true;
            uxTableViewLinkButton.Enabled = false;
            uxGridViewLinkButton.CssClass = "ProductGridViewButton";
            uxListViewLinkButton.CssClass = "ProductListViewButton";
            uxTableViewLinkButton.CssClass = "ProductTableViewButtonDisable";
            SelectedView = "Table";
        }
    }

    private void Refresh()
    {
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!Page.IsPostBack)
        {
            Refresh();
        }
    }

    public string ProductColumnConfig
    {
        get
        {
            if (ViewState["ProductColumnConfig"] == null)
                return String.Empty;
            else
                return ViewState["ProductColumnConfig"].ToString();
        }
        set
        {
            ViewState["ProductColumnConfig"] = value;
        }
    }

    protected void uxListViewLinkButton_Click( object sender, EventArgs e )
    {

        SelectedView = "List";
        SetViewTypeText(SelectedView);

        OnBubbleEvent( e );
    }
    protected void uxGridViewLinkButton_Click( object sender, EventArgs e )
    {
        SelectedView = "Grid";
        SetViewTypeText(SelectedView);
        OnBubbleEvent( e );
    }
    protected void uxTableViewLinkButton_Click(object sender, EventArgs e)
    {
        SelectedView = "Table";
        SetViewTypeText(SelectedView);
        OnBubbleEvent(e);
    }
}