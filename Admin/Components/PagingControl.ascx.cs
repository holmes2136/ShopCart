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
using Vevo;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI.ServerControls;

public partial class Advanced_Components_PagingControl
    : AdminAdvancedBaseUserControl, IPagingControl
{
    private const int DefaultMaximumLinks = 6;
    private const int DefaultItemsPerPage = 20;

    private int _currentPageLoading;
    private int _numberOfPagesLoading;
    private bool _isPagePostBack = false;


    private void DeterminePageRange(out int low, out int high)
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

    private void AddLabel(string text)
    {
        AddText(text);
    }

    private void AddLastLabel()
    {
        Label label = new Label();
        label.ID = "uxLastLabel";
        label.Text = String.Format("of {0} pages | view ", NumberOfPages.ToString());
        label.CssClass = "Label";

        uxPlaceHolder.Controls.Add(label);

    }
    private void AddText(string text)
    {
        AdvancedLinkButton link = new AdvancedLinkButton();
        link.ID = String.Format("uxPageText{0}LinkButton", uxPlaceHolder.Controls.Count);
        link.Text = text;
        link.CssClassBegin = "PageLinkLeft1";
        link.CssClassEnd = "PageLinkRight1";
        link.Enabled = false;
        uxPlaceHolder.Controls.Add(link);
    }

    private void AddDots()
    {
        AddText("..");
    }

    private void AddOnePageLink(int pageNumber)
    {
        AdvancedLinkButton link = new AdvancedLinkButton();
        link.ID = "uxPage" + pageNumber + "LinkButton";
        link.Text = pageNumber.ToString();
        link.CommandArgument = pageNumber.ToString();
        link.Command += new CommandEventHandler(Page_Command);
        link.CssClassBegin = "PageLinkLeft1";
        link.CssClassEnd = "PageLinkRight1";

        uxPlaceHolder.Controls.Add(link);
    }

    private void AddLinkPrevious()
    {
        AdvanceButton previous = new AdvanceButton();
        previous.ID = "uxPreviousLinkButton";
        //previous.Text = Resources.PagingMessages.Previous;
        //previous.ShowText = false;

        if (CurrentPage > 1)
        {
            previous.Enabled = true;
            previous.CssClass = "AdminButtonPrevPageActive";
            previous.CssClassBegin = "fl";
            previous.Command += new CommandEventHandler(Previous_Command);
        }
        else
        {
            previous.Enabled = false;
            previous.CssClass = "AdminButtonPrevPageInactive";
            previous.CssClassBegin = "fl";
            //previous.ForeColor = Color.Gray;
        }

        uxPlaceHolder.Controls.Add(previous);

    }

    private void AddLinkNext()
    {
        AdvanceButton next = new AdvanceButton();
        next.ID = "uxNextLinkButton";
        //   next.Text = Resources.PagingMessages.Next;

        if (CurrentPage < NumberOfPages)
        {
            next.Enabled = true;
            next.Command += new CommandEventHandler(Next_Command);
            next.CssClass = "AdminButtonNextPageActive";
            next.CssClassBegin = "fl";

        }
        else
        {
            next.Enabled = false;
            next.ForeColor = Color.Gray;
            next.CssClass = "AdminButtonNextPageInactive";
            next.CssClassBegin = "fl";
        }

        uxPlaceHolder.Controls.Add(next);
    }

    private void AddFirstLinkIfNecessary(int low)
    {
        if (low > 1)
        {
            //           AddSpace();
            AddOnePageLink(1);
            if (low > 2)
            {
                //AddSpace();
                AddDots();
            }
        }
    }

    private void AddLastLinkIfNecessary(int high)
    {
        if (high < NumberOfPages)
        {
            if (high < NumberOfPages - 1)
            {
                AddDots();
                //AddSpace();
            }
            AddOnePageLink(NumberOfPages);
            //AddSpace();
        }
    }

    private void AddLinkPageNumber(int low, int high)
    {
        //AddSpace();
        for (int pageNumber = low; pageNumber <= high; pageNumber++)
        {
            if (pageNumber == CurrentPage)
                AddLabel("[" + pageNumber.ToString() + "]");
            else
                AddOnePageLink(pageNumber);

            //AddSpace();
        }
    }

    private void AddTextBox()
    {
        TextBox textBox = new TextBox();
        textBox.ID = "uxPageNo";
        textBox.Text = CurrentPage.ToString();
        textBox.CssClass = "TextBox";
        textBox.Width = new Unit(20);
        uxPlaceHolder.Controls.Add(textBox);

    }

    private void ChangePage()
    {
        _isPagePostBack = true;

        TextBox textBox = (TextBox)uxPlaceHolder.FindControl("uxPageNo");
        CurrentPage = Convert.ToInt32(textBox.Text);
    }

    private void Previous_Command(object sender, CommandEventArgs e)
    {
        if (CurrentPage > 1)
            CurrentPage -= 1;
        // TextBox PageNo = (TextBox) uxPlaceHolder.FindControl( "uxPageNo" );
        // PageNo.Text = CurrentPage.ToString();
        OnBubbleEvent(e);
    }

    private void Next_Command(object sender, CommandEventArgs e)
    {
        if (CurrentPage < NumberOfPages)
            CurrentPage += 1;

        OnBubbleEvent(e);
    }

    private void Page_Command(object sender, CommandEventArgs e)
    {
        CurrentPage = Int32.Parse(e.CommandArgument.ToString());

        OnBubbleEvent(e);
    }

    private void GeneratePagingLinks()
    {
        int low, high;
        DeterminePageRange(out low, out high);

        AddLinkPrevious();
        //    AddFirstLinkIfNecessary( low );
        //    AddLinkPageNumber( low, high );
        AddTextBox();
        //    AddLastLinkIfNecessary( high );
        AddLinkNext();
        AddLastLabel();
    }

    private void PopulateItemsPerPageDropDown(int currentItemPerPage)
    {
        for (int i = 0; i < uxItemsPerPagesDrop.Items.Count; i++)
        {
            int valueInDropDown = ConvertUtilities.ToInt32(uxItemsPerPagesDrop.Items[i].Value);
            if (valueInDropDown == currentItemPerPage)
            {
                return;
            }
            else if (valueInDropDown > currentItemPerPage)
            {
                uxItemsPerPagesDrop.Items.Insert(i, currentItemPerPage.ToString());
                return;
            }
        }
    }

    private void LoadCurrentPageFromQuery()
    {
        if (MainContext.QueryString["Page"] != null)
        {
            int currentPage;
            if (ConvertUtilities.ToInt32(MainContext.QueryString["Page"]) < 1)
                currentPage = 1;
            else
                currentPage = ConvertUtilities.ToInt32(MainContext.QueryString["Page"]);

            // Cannot use NumberOfPages = CurrentPage directly since CurrentPage will always return 1
            // unless NumberOfPages is greater than 1.
            CurrentPage = currentPage;
            NumberOfPages = currentPage;
        }
    }

    private void LoadItemsPerPageFromQuery()
    {
        if (MainContext.QueryString["ItemsPerPage"] != null)
        {
            ItemsPerPages = ConvertUtilities.ToInt32(MainContext.QueryString["ItemsPerPage"]);
        }
    }

    private void LoadDefaultFromQuery()
    {
        LoadCurrentPageFromQuery();
        LoadItemsPerPageFromQuery();
    }


    protected void uxItemsPerPagesDrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        ItemsPerPages = int.Parse(uxItemsPerPagesDrop.SelectedValue);
        CurrentPage = 1;
        // Send event to parent controls
        OnBubbleEvent(e);
    }


    // Need to generate controls here. In case of the postback, the Command or Click event 
    // requires these controls to properly invoke the correct event.
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!MainContext.IsPostBack)
            LoadDefaultFromQuery();

        GeneratePagingLinks();
        _currentPageLoading = CurrentPage;
        _numberOfPagesLoading = NumberOfPages;


    }

    // Update control status in PreRender event. Wait parent page to set
    // number of pages first
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (CurrentPage != _currentPageLoading ||
            NumberOfPages != _numberOfPagesLoading)
        {
            uxPlaceHolder.Controls.Clear();
            GeneratePagingLinks();
        }
    }
    protected void uxSelectPageButton_Click(object sender, EventArgs e)
    {
        TextBox PageNo = (TextBox)uxPlaceHolder.FindControl("uxPageNo");
        int selectPage = ConvertUtilities.ToInt32(PageNo.Text);
        if (selectPage > NumberOfPages)
            CurrentPage = NumberOfPages;
        else if (selectPage <= 0)
            CurrentPage = 1;
        else
            CurrentPage = selectPage;

        OnBubbleEvent(e);
    }

    public bool isPagePostBack
    {
        get
        {
            return _isPagePostBack;
        }
        set
        {
            _isPagePostBack = value;
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
                int page = (int)ViewState["CurrentPage"];
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
                (int)ViewState["NumberOfPages"] < 1)
                return 1;
            else
                return (int)ViewState["NumberOfPages"];
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
            else if ((int)ViewState["MaximumLinks"] < 1)
                return 1;
            else
                return (int)ViewState["MaximumLinks"];
        }
        set
        {
            ViewState["MaximumLinks"] = value;
        }
    }

    // This is the default ItemsPerPage value. If this value will be inserted into the drop down list.
    public int ItemsPerPages
    {
        get
        {
            if (ViewState["ItemsPerPages"] == null)
                return DefaultItemsPerPage;
            else
                return (int)ViewState["ItemsPerPages"];
        }
        set
        {
            ViewState["ItemsPerPages"] = value;

            PopulateItemsPerPageDropDown(value);
            uxItemsPerPagesDrop.SelectedValue = ViewState["ItemsPerPages"].ToString();
        }
    }

    public int StartIndex
    {
        get { return (CurrentPage - 1) * ItemsPerPages; }
    }

    public int EndIndex
    {
        get { return (CurrentPage * ItemsPerPages) - 1; }
    }

    public void UpdateBrowseQuery(UrlQuery urlQuery)
    {
        urlQuery.AddQuery("Page", CurrentPage.ToString());
        urlQuery.AddQuery("ItemsPerPage", ItemsPerPages.ToString());
    }

}
