using System;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class Components_MobilePagingControl : Vevo.WebUI.International.BaseLanguageUserControl
{
    private const int DefaultMaximumLinks = 3;

    private int _currentPageLoading;
    private int _numberOfPagesLoading;


    private void DeterminePageRange( out int low, out int high )
    {
        low = CurrentPage - MaximumLinks / 2;
        if (low < 1)
            low = 1;

        // Assuming high value according to low value
        high = low + MaximumLinks - 1;
        if (high > NumberOfPages)
            high = NumberOfPages;

        // Need to re-adjust low value if the current page is around the upper bound value
        if (high - low + 1 < MaximumLinks)
        {
            low = high - MaximumLinks + 1;
            if (low < 1)
                low = 1;
        }
    }

    private void AddLabel( string text, string styleSheet )
    {
        Label space = new Label();
        space.Text = text;
        space.Style.Add( "font-weight", "bold" );
        space.CssClass = styleSheet;

        HtmlTableCell tableCell = new HtmlTableCell();
        tableCell.Controls.Add( space );
        uxPlaceHolder.Controls.Add( tableCell );
    }

    private void AddText( string text )
    {
        Literal space = new Literal();
        space.Text = text;
        HtmlTableCell tableCell = new HtmlTableCell();
        tableCell.Controls.Add( space );
        uxPlaceHolder.Controls.Add( tableCell );
    }

    private void AddSpace()
    {
        AddText( "&nbsp;" );
    }

    private void AddDots()
    {
        AddLabel( "..", "MobileNonSelectedPage" );
    }

    private void AddOnePageLink( int pageNumber )
    {
        LinkButton link = new LinkButton();
        link.ID = "uxPage" + pageNumber + "LinkButton";
        link.Text = pageNumber.ToString();
        link.CommandArgument = pageNumber.ToString();
        link.Command += new CommandEventHandler( Page_Command );
        link.CssClass = "MobileNonSelectedPage";
        HtmlTableCell tableCell = new HtmlTableCell();
        tableCell.Controls.Add( link );
        uxPlaceHolder.Controls.Add( tableCell );
    }

    private ScriptManager GetScriptManager()
    {
        return (ScriptManager) Page.Master.FindControl( "uxScriptManager" );
    }

    private void AddLinkPrevious()
    {
        LinkButton previous = new LinkButton();
        previous.ID = "uxPreviousLinkButton";

        if (CurrentPage > 1)
        {
            previous.Enabled = true;
            previous.Command += new CommandEventHandler( Previous_Command );
            previous.CssClass = "MobileActivePrevImage";
        }
        else
        {
            previous.Enabled = false;
            previous.CssClass = "MobileInActivePrevImage";
        }
        HtmlTableCell tableCell = new HtmlTableCell();
        tableCell.Controls.Add( previous );
        uxPlaceHolder.Controls.Add( tableCell );

        ScriptManager scriptManager = GetScriptManager();
        scriptManager.RegisterAsyncPostBackControl( previous );
    }

    private void AddLinkNext()
    {
        LinkButton next = new LinkButton();
        next.ID = "uxNextLinkButton";

        if (CurrentPage < NumberOfPages)
        {
            next.Enabled = true;
            next.Command += new CommandEventHandler( Next_Command );
            next.CssClass = "MobileActiveNextImage";

        }
        else
        {
            next.Enabled = false;
            next.CssClass = "MobileInActiveNextImage";
        }
        HtmlTableCell tableCell = new HtmlTableCell();
        tableCell.Controls.Add( next );
        uxPlaceHolder.Controls.Add( tableCell );

        ScriptManager scriptManager = GetScriptManager();
        scriptManager.RegisterAsyncPostBackControl( next );
    }

    private void AddFirstLinkIfNecessary( int low )
    {
        if (low > 1)
        {
            AddSpace();
            AddOnePageLink( 1 );
            if (low > 2)
            {
                AddSpace();
                AddDots();
            }
        }
    }

    private void AddLastLinkIfNecessary( int high )
    {
        if (high < NumberOfPages)
        {
            if (high < NumberOfPages - 1)
            {
                AddDots();
                AddSpace();
            }
            AddOnePageLink( NumberOfPages );
            AddSpace();
        }
    }

    private void AddLinkPageNumber( int low, int high )
    {
        AddSpace();
        for (int pageNumber = low; pageNumber <= high; pageNumber++)
        {
            if (pageNumber == CurrentPage)
                AddLabel( pageNumber.ToString(), "MobileSelectedPage" );
            else
                AddOnePageLink( pageNumber );

            AddSpace();
        }
    }


    private void Previous_Command( object sender, CommandEventArgs e )
    {
        if (CurrentPage > 1)
            CurrentPage -= 1;

        OnBubbleEvent( e );
    }

    private void Next_Command( object sender, CommandEventArgs e )
    {
        if (CurrentPage < NumberOfPages)
            CurrentPage += 1;

        OnBubbleEvent( e );
    }

    private void Page_Command( object sender, CommandEventArgs e )
    {
        CurrentPage = Int32.Parse( e.CommandArgument.ToString() );

        OnBubbleEvent( e );
    }

    private void GeneratePagingLinks()
    {
        int low, high;
        DeterminePageRange( out low, out high );

        AddLinkPrevious();
        AddFirstLinkIfNecessary( low );
        AddLinkPageNumber( low, high );
        AddLastLinkIfNecessary( high );
        AddLinkNext();
    }

    protected void ScriptManager_Navigate( object sender, HistoryEventArgs e )
    {
    }


    // Need to generate controls here. In case of the postback, the Command or Click event 
    // requires these controls to properly invoke the correct event.
    protected void Page_Load( object sender, EventArgs e )
    {
        GeneratePagingLinks();
        _currentPageLoading = CurrentPage;
        _numberOfPagesLoading = NumberOfPages;

        Vevo.WebUI.Ajax.AjaxUtilities.ScrollToTop( this );
    }

    // Update control status in PreRender event. Wait parent page to set
    // number of pages first
    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (CurrentPage != _currentPageLoading ||
            NumberOfPages != _numberOfPagesLoading)
        {
            uxPlaceHolder.Controls.Clear();
            GeneratePagingLinks();
        }
    }

    public int CurrentPage
    {
        get
        {
            if (ViewState["CurrentPage"] == null)
            {
                return 1;
            }
            else
            {
                int page = (int) ViewState["CurrentPage"];
                if (page > NumberOfPages)
                {
                    return NumberOfPages;
                }
                else
                {
                    return page;
                }
            }
        }
        set
        {
            ViewState["CurrentPage"] = value;
        }
    }

    public int NumberOfPages
    {
        get
        {
            if (ViewState["NumberOfPages"] == null ||
                (int) ViewState["NumberOfPages"] < 1)
                return 1;
            else
                return (int) ViewState["NumberOfPages"];
        }
        set
        {
            ViewState["NumberOfPages"] = value;
        }
    }

    public int MaximumLinks
    {
        get
        {
            if (ViewState["MaximumLinks"] == null)
                return DefaultMaximumLinks;
            else if ((int) ViewState["MaximumLinks"] < 1)
                return 1;
            else
                return (int) ViewState["MaximumLinks"];
        }
        set
        {
            ViewState["MaximumLinks"] = value;
        }
    }

}
