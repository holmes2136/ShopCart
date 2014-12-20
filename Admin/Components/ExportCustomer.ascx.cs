using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.ImportExport;
using Vevo.Domain.Users;
using System.Web;
using Vevo.WebAppLib;
using Vevo.Base.Domain;

public partial class AdminAdvanced_Components_ExportCustomer : AdminAdvancedBaseUserControl
{
    #region Private

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.CustomerRepository.GetTableSchema();
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

    private void SetUpShippingAddressSortBy()
    {
        uxShippingSoryByDrop.Items.Clear();
        IList<TableSchemaItem> list = DataAccessContext.CustomerRepository.GetShippingAddressTableSchemas();

        for (int i = 0; i < list.Count; i++)
        {
            uxShippingSoryByDrop.Items.Add( list[i].ColumnName );
        }
    }

    private void TieGenerateButton()
    {
        uxExportFilter.TieGenerateTextBox( uxGenerateButton );
        uxExportFilter.TieGenerateTextBox( uxExportShippingAddress );
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            SetUpSearchFilter();
            SetUpShippingAddressSortBy();
        }

        TieGenerateButton();

        ScriptManager uxScriptManager = ScriptManager.GetCurrent( Page );
        uxScriptManager.RegisterPostBackControl( uxGenerateButton );
        uxScriptManager.RegisterPostBackControl( uxExportShippingAddress );
    }

    protected void uxGenerateButton_Click( object sender, EventArgs e )
    {
        uxMessage.Clear();

        uxExportFilter.GetSearchFilterObj( e );

        IList<Customer> list = DataAccessContext.CustomerRepository.ExportCustomer(
            uxSortByDrop.SelectedValue,
            uxExportFilter.SearchFilterObj,
            BoolFilter.ShowAll );

        if (list.Count > 0)
        {
            CustomerExporter exporter = new CustomerExporter();
            MemoryStream memStream = exporter.ExportToStream( list );
            Exception ex;
            WebUtilities.CreateExportFile( System.Web.HttpContext.Current.ApplicationInstance, memStream, "ExportCustomer.csv", out ex );
            if (ex != null)
                uxMessage.DisplayError( Resources.ExportDataMessage.Error + ex.GetType().ToString() );
        }
        else
        {
            uxMessage.DisplayError( Resources.ExportDataMessage.CannotFindData );
        }
    }

    protected void uxExportShippingAddress_Click( object sender, EventArgs e )
    {
        uxMessage.Clear();

        IList<ShippingAddress> shippingList = DataAccessContext.CustomerRepository.GetAllShippingAddress( uxShippingSoryByDrop.SelectedValue );

        if (shippingList.Count > 0)
        {
            CustomerExporter exporter = new CustomerExporter();
            MemoryStream memStream = exporter.ExportShippingAddressToStream( shippingList );
            Exception ex;
            WebUtilities.CreateExportFile( System.Web.HttpContext.Current.ApplicationInstance, memStream, "ExportShippingAddress.csv", out ex );
            if (ex != null)
                uxMessage.DisplayError( Resources.ExportDataMessage.Error + ex.GetType().ToString() );
        }
        else
        {
            uxMessage.DisplayError( Resources.ExportDataMessage.CannotFindData );
        }
    }

    #endregion

    #region Public Methods

    public void HideCustomerGenerateButton()
    {
        uxGenerateButton.Visible = false;
    }

    #endregion
}
