using System;
using System.Data;
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


public partial class Components_PagingLink : Vevo.WebUI.International.BaseLanguageUserControl
{
    private int _limitPage = 10;


    private void DeterminePageRange( out int low, out int high )
    {
        low = CurrentPage - _limitPage / 2;
        if (low < 1)
            low = 1;

        // Assuming high value according to low value
        high = low + _limitPage - 1;
        if (high > NumberOfPages)
            high = NumberOfPages;

        // Need to re-adjust low value if the current page is around the upper bound value
        if (high - low + 1 < _limitPage)
        {
            low = high - _limitPage + 1;
            if (low < 1)
                low = 1;
        }
    }

    private string CreateUrlWithPageNumber( int page )
    {
        Uri url = new Uri( Request.Url.Scheme + "://" + Request.Url.Host + Request.RawUrl );

        StringBuilder sb = new StringBuilder();
        sb.Append( url.AbsolutePath );
        sb.Append( '?' );

        NameValueCollection query = Request.QueryString;
        string key;
        for (int i = 0; i < query.Count; i++)
        {
            if (query.AllKeys[i] != null &&
                query.AllKeys[i].ToString().ToLower() != "categoryname" &&
                query.AllKeys[i].ToString().ToLower() != "categoryid" &&
                query.AllKeys[i].ToString().ToLower() != "productname")
            {
                key = query.AllKeys[i].ToString();
                if (key.ToLower() != "page")
                    sb.AppendFormat( "{0}={1}&", key, query[i] );
            }
        }
        sb.AppendFormat( "Page={0}", page );

        return sb.ToString();
    }

    private void AddText( string text )
    {
        Literal space = new Literal();
        space.Text = text;
        uxPanel.Controls.Add( space );
    }

    private void AddSpace()
    {
        AddText( "&nbsp;" );
    }

    private void AddDots()
    {
        AddText( ".." );
    }

    private void AddOnePageLink( int pageNumber )
    {
        HyperLink link = new HyperLink();
        link.Text = pageNumber.ToString();
        link.NavigateUrl = CreateUrlWithPageNumber( pageNumber );
        uxPanel.Controls.Add( link );
    }

    private void AddLinkPrevious()
    {
        HyperLink previous = new HyperLink();
        previous.Text = "[$Previous]";

        if (CurrentPage > 1)
        {
            previous.Enabled = true;
            previous.NavigateUrl = CreateUrlWithPageNumber( CurrentPage - 1 );
        }
        else
        {
            previous.Enabled = false;
        }

        uxPanel.Controls.Add( previous );
    }

    private void AddLinkNext()
    {
        HyperLink next = new HyperLink();
        next.Text = "[$Next]";

        if (CurrentPage < NumberOfPages)
        {
            next.Enabled = true;
            next.NavigateUrl = CreateUrlWithPageNumber( CurrentPage + 1 );
        }
        else
        {
            next.Enabled = false;
        }

        uxPanel.Controls.Add( next );
    }

    private void AddLinkPreviousPageNumber()
    {
        if (CurrentPage < (_limitPage / 2))
        {
            for (int pageNumber = 1; pageNumber <= (_limitPage / 2); pageNumber++)
            {
                if (pageNumber == CurrentPage)
                    AddText( pageNumber.ToString() );
                else
                    AddOnePageLink( pageNumber );

                AddSpace();
            }
        }
        else
        {
            AddOnePageLink( 1 );
            AddSpace();
            for(int pageNumber = (CurrentPage - (_limitPage / 2));pageNumber <= CurrentPage;pageNumber++)
            {
                if (pageNumber == CurrentPage)
                    AddText( pageNumber.ToString() );
                else
                    AddOnePageLink( pageNumber );

                AddSpace();
            }
        }
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
                AddText( pageNumber.ToString() );
            else
                AddOnePageLink( pageNumber );

            AddSpace();
        }
    }


    protected void Page_Load( object sender, EventArgs e )
    {
    }

    // Update control status in PreRender event. Wait parent page to set
    // number of pages first
    protected void Page_PreRender( object sender, EventArgs e )
    {
        int low, high;
        DeterminePageRange( out low, out high );

        AddLinkPrevious();
        AddFirstLinkIfNecessary( low );
        AddLinkPageNumber( low, high );
        AddLastLinkIfNecessary( high );
        AddLinkNext();
    }

    public int CurrentPage
    {
        get
        {
            int result;
            string page = Request.QueryString["Page"];
            if (String.IsNullOrEmpty( page ) ||
                !int.TryParse( page, out result ))
                return 1;
            else
                return result;
        }
    }

    public int NumberOfPages
    {
        get
        {
            if (ViewState["NumberOfPages"] == null)
                return 1;
            else
                return (int) ViewState["NumberOfPages"];
        }
        set
        {
            ViewState["NumberOfPages"] = value;
        }
    }

}
