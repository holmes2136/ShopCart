using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using Vevo.WebUI.Ajax;
using Vevo.WebUI.International;
using Vevo.Base.Domain;

public partial class NewsPage : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private void PopulateGrid()
    {
        uxNewsGrid.DataSource = DataAccessContext.NewsRepository.GetAll(
            StoreContext.Culture, "NewsDate DESC", new StoreRetriever().GetCurrentStoreID(), BoolFilter.ShowTrue );
        uxNewsGrid.DataBind();
    }

    private string StripDescritpion( string source )
    {
        char[] array = new char[ source.Length ];
        int arrayIndex = 0;
        bool inside = false;

        for ( int i = 0; i < source.Length; i++ )
        {
            char let = source[ i ];
            if ( let == '<' )
            {
                inside = true;
                continue;
            }
            if ( let == '>' )
            {
                inside = false;
                continue;
            }
            if ( !inside )
            {
                array[ arrayIndex ] = let;
                arrayIndex++;
            }
        }

        return new string( array, 0, arrayIndex );
    }

    private IList<News> GetNewsList( int itemPerPage, out int totalItems )
    {
        totalItems = 0;
        return DataAccessContext.NewsRepository.SearchNews(
            StoreContext.Culture,
            "NewsDate DESC",
            new SearchFilter(),
            ( uxPagingControl.CurrentPage - 1 ) * itemPerPage,
            ( uxPagingControl.CurrentPage * itemPerPage ) - 1,
            out totalItems, new StoreRetriever().GetCurrentStoreID(), BoolFilter.ShowTrue );

    }

    private IList<News> DataRetriever()
    {
        throw new NotImplementedException();
    }

    private void News_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        Refresh();
    }

    protected void uxPagingControl_BubbleEvent( object sender, EventArgs e )
    {
        Refresh();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( News_StoreCultureChanged );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        AjaxUtilities.ScrollToTop( uxGoToTopLink );

        if ( !IsPostBack )
            Refresh();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {

    }

    protected string GetNewsUrl( object newsID, object urlName )
    {
        return UrlManager.GetNewsUrl( newsID, urlName );
    }

    protected string CreatePreviewDescritpion( object description )
    {
        string str = description.ToString();
        int characterLimit = 130;

        string temp = StripDescritpion( str );

        int maxLength = temp.Length;
        if ( maxLength > characterLimit )
            maxLength = characterLimit;
        string tempString = temp.Substring( 0, maxLength ).Trim();
        int trimOffset = tempString.LastIndexOf( " " );

        string result = tempString;
        if ( trimOffset > 0 )
        {
            result = tempString.Substring( 0, trimOffset );
        }
        result += "...";

        return result;
    }

    protected void uxNewsGrid_PageIndexChanging( object sender, DataGridPageChangedEventArgs e )
    {
        Refresh();
    }

    protected bool CheckImageExist( object imageFile )
    {
        string imageName = imageFile.ToString();
        bool imageExist = false;
        if ( !String.IsNullOrEmpty( imageName ) )
        {
            imageExist = File.Exists( Server.MapPath( imageName ) );
        }
        return imageExist;
    }

    public void Refresh()
    {
        int totalItems;
        int selectedValue = 6;

        uxNewsGrid.DataSource = GetNewsList( selectedValue, out totalItems );
        uxNewsGrid.DataBind();
        uxPagingControl.NumberOfPages = ( int ) Math.Ceiling( ( double ) totalItems / selectedValue );
    }
}
