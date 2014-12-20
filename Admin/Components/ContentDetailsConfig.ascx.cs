using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.WebAppLib;
using System.Collections.Specialized;
using System.Collections.Generic;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.Domain.Configurations;
using Vevo.Domain.DataInterfaces;
using Vevo.Shared.Utilities;
using Vevo.Base.Domain;

public partial class AdminAdvanced_Components_ContentDetailsConfig : AdminAdvancedBaseUserControl
{
    #region Private
    private string[] NoShowColumns = { "UrlName" };

    private StringCollection GetNoShowList()
    {
        StringCollection collection = new StringCollection();
        collection.AddRange( NoShowColumns );
        return collection;
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

        IList<TableSchemaItem> columnList = DataAccessContext.ContentRepository.GetTableSchema();
        string[] noShowList = NoShowColumns;
        DataTable table = new DataTable();
        DataRow tableRow;
        table = AddColumn( table, "Content", "System.String" );
        table = AddColumn( table, "Display", "System.Boolean" );
        for (int i = 0; i < columnList.Count; i++)
        {
            if ((!StringUtilities.IsStringInArray( noShowList, columnList[i].ColumnName, true )) &&
            (columnList[i].DataType.ToString() == "System.String"))
            {
                //  columnContent.Add(columnList[i]);
                tableRow = table.NewRow();
                tableRow["Content"] = columnList[i].ColumnName;
                tableRow["Display"] = CheckValueInConfig( columnList[i].ColumnName );
                table.Rows.Add( tableRow );
            }

        }
        uxGridContent.DataSource = table;
        uxGridContent.DataBind();
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
            foreach (GridViewRow row in uxGridContent.Rows)
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
            DataAccessContext.ConfigurationRepository.UpdateValue( DataAccessContext.Configurations["ContentSearchBy"], totalSearch );

            uxMessage.DisplayMessage( Resources.SearchMessages.UpdateSuccess );

            AdminUtilities.LoadSystemConfig();
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }
    #endregion

    #region Public

    public bool CheckValueInConfig( string nameColumn )
    {
        string totalFieldSearch = DataAccessContext.Configurations.GetValueNoThrow( "ContentSearchBy" );
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
