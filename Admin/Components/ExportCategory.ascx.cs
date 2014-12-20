using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.ImportExport;
using Vevo.Domain.Products;
using Vevo.WebAppLib;
using Vevo.Base.Domain;

public partial class Admin_Components_ExportCategory : AdminAdvancedBaseUserControl
{
    #region Private

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.CategoryRepository.GetTableSchema();
        uxExportFilter.SetUpSchema( list );
        SetUpSortDrop( list );
    }

    private void SetUpSortDrop( IList<TableSchemaItem> list )
    {
        uxSortByDrop.Items.Clear();

        for (int i = 0; i < list.Count; i++)
        {
            uxSortByDrop.Items.Add( list[i].ColumnName );
        }
    }

    private void TieGenerateButton()
    {
        uxExportFilter.TieGenerateTextBox( uxGenerateButton );
    }

    #endregion

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            SetUpSearchFilter();
        }

        TieGenerateButton();

        ScriptManager uxScriptManager = ScriptManager.GetCurrent( Page );
        uxScriptManager.RegisterPostBackControl( uxGenerateButton );
    }

    protected void uxGenerateButton_Click( object sender, EventArgs e )
    {
        uxMessage.Clear();

        uxExportFilter.GetSearchFilterObj( e );
        Culture culture = DataAccessContext.CultureRepository.GetOne( uxLanguageDrop.SelectedValue.ToString() );
        IList<Category> categoryList = DataAccessContext.CategoryRepository.SearchCategory( culture, uxSortByDrop.SelectedValue, uxExportFilter.SearchFilterObj );

        if (categoryList.Count > 0)
        {
            CategoryExportHelper exporter = new CategoryExportHelper( "category" );
            MemoryStream memStream = exporter.ExportToStream( categoryList );
            Exception ex;
            WebUtilities.CreateExportFile( System.Web.HttpContext.Current.ApplicationInstance, memStream, "ExportCategory.csv", out ex );
            if (ex != null)
                uxMessage.DisplayError( Resources.ExportDataMessage.Error + ex.GetType().ToString() );
        }
        else
        {
            uxMessage.DisplayError( "No Data Found." );
        }
    }

    #region Public Methods

    public void HideProductGenerateButton()
    {
        uxGenerateButton.Visible = false;
    }

    #endregion
}
