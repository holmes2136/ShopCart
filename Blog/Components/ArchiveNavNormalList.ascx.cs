using System;
using System.Data;
using Vevo.Domain;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebUI.International;

public partial class Blog_Components_ArchiveNavNormalList : BaseLanguageUserControl
{
    private string _displayArchiveListItem = "5";
    private int _countArchiveListItem = 0;
    private DataTable CreateDataSource()
    {
        string storeID = new StoreRetriever().GetCurrentStoreID();
        DataTable list = DataAccessContext.BlogRepository.GetArchiveList( storeID, _displayArchiveListItem );

        return list;
    }

    private void PopulateControls()
    {
        uxList.DataSource = CreateDataSource();
        uxList.DataBind();

        _countArchiveListItem = uxList.Items.Count;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if ( !IsPostBack )
        {
            PopulateControls();
        }
    }

    protected string GetTextName( object item )
    {
        DataRowView data = ( DataRowView ) item;
        DateTime time = new DateTime( ConvertUtilities.ToInt32( data.Row[ "CreateYear" ] ), ConvertUtilities.ToInt32( data.Row[ "CreateMonth" ] ), 1 );
        string monthName = time.ToString( "MMMM" );
        return monthName + " " + data.Row[ "CreateYear" ] + " ( " + data.Row[ "NumberOfBlog" ] + " )";
    }

    protected string GetNavURL( object item )
    {
        DataRowView data = ( DataRowView ) item;
        return data.Row[ "CreateMonth" ] + "_" + data.Row[ "CreateYear" ];
    }

    public void Refresh()
    {
        PopulateControls();
    }

    public bool HasArchiveItem()
    {
        if (_countArchiveListItem == 0)
            return false;
        else
            return true;
    }
}
