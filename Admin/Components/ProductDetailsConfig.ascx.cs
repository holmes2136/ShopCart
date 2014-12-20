using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Configurations;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Products;
using Vevo.WebAppLib;
using Vevo.Shared.DataAccess;
using Vevo.Shared.Utilities;
using Vevo.Base.Domain;

public partial class AdminAdvanced_Components_ProductDetailsConfig : AdminAdvancedBaseUserControl
{
    #region Private

    private string[] NoShowColumns = { "DownloadPath", "UrlName" };

    private static bool _isVerified = false;

    private static bool IsVerified
    {
        get
        {
            if (_isVerified == false)
            {
                _isVerified =
                    KeyUtilities.Verify( DataAccessHelper.DomainRegistrationkey ) ||
                    KeyUtilities.VerifyLicenseFile( DataAccessHelper.LicenseFilePath );
            }

            return _isVerified;
        }
    }

    private StringCollection GetNoShowList()
    {
        StringCollection collection = new StringCollection();
        collection.AddRange( NoShowColumns );
        return collection;
    }

    private DataTable GetColumnProduct()
    {
        DataTable table = new DataTable();
        DataRow tableRow;
        table = AddColumn( table, "Product", "System.String" );
        table = AddColumn( table, "Display", "System.Boolean" );
        IList<TableSchemaItem> itemList = DataAccessContext.ProductRepository.GetTableSchema();

        StringCollection noShowList = GetNoShowList();

        foreach (TableSchemaItem item in itemList)
        {
            if (item.DataType.ToString() == "System.String" &&
                !noShowList.Contains( item.ColumnName ))
            {
                tableRow = table.NewRow();
                tableRow["Product"] = item.ColumnName;
                tableRow["Display"] = CheckValueInConfig( item.ColumnName );
                table.Rows.Add( tableRow );
            }
        }

        return table;
    }

    private DataTable AddColumn( DataTable myTable, string columnName, string typeColumn )
    {
        DataColumn myColumn;
        myColumn = new DataColumn();
        myColumn.ColumnName = columnName;
        myColumn.DataType = System.Type.GetType( typeColumn );
        myColumn.Unique = false;
        myColumn.AutoIncrement = false;
        myColumn.Caption = columnName;
        myColumn.ReadOnly = false;
        myTable.Columns.Add( myColumn );
        return myTable;
    }

    private void RefreshGrid()
    {
        uxGridProduct.DataSource = GetColumnProduct();
        uxGridProduct.DataBind();
    }

    private string[] SplitColumn( string str )
    {
        char[] delimiter = new char[] { ',', ':', ';' };
        string[] result = str.Split( delimiter );
        return result;
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsAdminModifiable())
            uxButtonUpdateConfig.Visible = false;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            RefreshGrid();
    }

    protected void uxButtonUpdateConfig_Click( object sender, EventArgs e )
    {
        try
        {
            string totalSearch = "";
            foreach (GridViewRow row in uxGridProduct.Rows)
            {
                CheckBox insertCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (insertCheck.Checked)
                {
                    string nameSearch = row.Cells[1].Text.Trim();
                    totalSearch = totalSearch + nameSearch + ",";
                }
            }
            if (String.IsNullOrEmpty( totalSearch ))
            {
                uxMessage.DisplayError( "Error:<br/>Please select" );
                return;
            }
            DataAccessContext.ConfigurationRepository.UpdateValue( DataAccessContext.Configurations["ProductSearchBy"], totalSearch );

            uxMessage.DisplayMessage( Resources.SearchMessages.UpdateSuccess );

            AdminUtilities.LoadSystemConfig();
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    #endregion


    #region Public Methods

    public bool CheckValueInConfig( string nameColumn )
    {
        string totalFieldSearch = DataAccessContext.Configurations.GetValueNoThrow( "ProductSearchBy" );
        string[] arField = SplitColumn( totalFieldSearch );
        for (int i = 0; i <= arField.Length - 1; i++)
        {
            if (nameColumn == arField[i])
                return true;
        }
        return false;
    }

    #endregion

}
