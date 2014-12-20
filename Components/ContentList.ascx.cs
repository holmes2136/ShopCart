using System;
using System.Collections.Generic;
using Vevo.WebUI;
using Vevo.WebUI.Ajax;
using Vevo.WebUI.International;

public partial class Components_ContentList : BaseLanguageUserControl
{
    private DataAccessCallbacks.ContentListRetriever _retriever;

    public object[] UserDefinedParameters
    {
        get
        {
            return (object[]) ViewState["UserDefinedParameters"];
        }
        set
        {
            ViewState["UserDefinedParameters"] = value;
        }
    }

    public DataAccessCallbacks.ContentListRetriever DataRetriever
    {
        get
        {
            return _retriever;
        }
        set
        {
            _retriever = value;
        }
    }

    private void RegisterStoreEvents()
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( ContentList_StoreCultureChanged );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterStoreEvents();
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        uxItemsPerPageControl.BubbleEvent += new EventHandler( uxItemsPerPageControl_BubbleEvent );
        AjaxUtilities.ScrollToTop( uxGoToTopLink );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateProductControls();
    }

    private void PopulateProductControls()
    {
        if (this.Visible)
            Refresh();
    }

    private void ContentList_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        Refresh();
    }

    protected void uxItemsPerPageControl_BubbleEvent( object sender, EventArgs e )
    {
        uxPagingControl.CurrentPage = 1;
        Refresh();
    }

    protected void uxPagingControl_BubbleEvent( object sender, EventArgs e )
    {
        Refresh();
    }

    public void Refresh()
    {
        int totalItems;
        int selectedValue;
        selectedValue = Convert.ToInt32( uxItemsPerPageControl.SelectedValue );

        uxList.DataSource = GetContentList( selectedValue, " ", out totalItems );
        uxList.DataBind();
        uxPagingControl.NumberOfPages = (int) System.Math.Ceiling( (double) totalItems / selectedValue );
        PopulateText( totalItems );
    }

    private IList<Vevo.Domain.Contents.Content> GetContentList( int itemsPerPage, string sortBy, out int totalItems )
    {
        return DataRetriever(
            StoreContext.Culture,
            sortBy,
            (uxPagingControl.CurrentPage - 1) * itemsPerPage,
            (uxPagingControl.CurrentPage * itemsPerPage) - 1,
            UserDefinedParameters,
            out totalItems );
    }

    public void PopulateText( int totalItems )
    {
        if (totalItems > 0)
            uxMessageLabel.Text = "";
        else
        {
            uxMessageDiv.Visible = true;
            uxMessageLabel.Text = "<div style='text-align: center;'>" + GetLanguageText( "NoData" ) + "</div>";
        }
    }
}
