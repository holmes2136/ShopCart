using System;
using System.Data;
using Vevo.Domain;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebUI.International;

public partial class Blog_ArchiveList : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private DataTable CreateDataSource()
    {
        string storeID = new StoreRetriever().GetCurrentStoreID();
        DataTable list = DataAccessContext.BlogRepository.GetArchiveList( storeID );

        return list;
    }

    private void PopulateGrid()
    {
        uxBlogGrid.DataSource = CreateDataSource();
        uxBlogGrid.DataBind();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if ( !IsPostBack )
        {
            PopulateGrid();
        }
    }

    protected string GetTextName( object item )
    {
        DataRowView data = ( DataRowView ) item;
        DateTime time = new DateTime( ConvertUtilities.ToInt32( data.Row[ "CreateYear" ] ), ConvertUtilities.ToInt32( data.Row[ "CreateMonth" ] ), 1 );
        string monthName = time.ToString( "MMMM" );
        return monthName + " " + data.Row[ "CreateYear" ];
    }

    protected string GetNavURL( object item )
    {
        DataRowView data = ( DataRowView ) item;
        return data.Row[ "CreateMonth" ] + "_" + data.Row[ "CreateYear" ];
    }

    protected string GetAmount( object item )
    {
        DataRowView data = ( DataRowView ) item;
        return "("+data.Row[ "NumberOfBlog" ].ToString()+")";
    }
}
