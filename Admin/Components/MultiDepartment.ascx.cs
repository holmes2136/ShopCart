using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;

public partial class AdminAdvanced_Components_MultiDepartment : UserControl
{
    private const int _itemListboxDefault = 8;

    #region Private
    private string CreateScriptMoveItem(
        string fromID,
        string toID,
        string divFromID,
        string divToID,
        string controlIDserialized )
    {
        return String.Format( "return fnMoveItems('{0}','{1}', '{2}', '{3}', {4}, '{5}', '{6}')",
            fromID, toID, divFromID, divToID, _itemListboxDefault, uxDepartmentIDHidden.ClientID, controlIDserialized );
    }

    private bool IsAlreadyDepartment( DataTable tableDepartment, string departmentID )
    {
        DataRow[] rows = tableDepartment.Select( "DepartmentID = " + departmentID );
        if (rows.Length > 0)
            return true;
        else
            return false;
    }

    private void InitDropDownList( Culture culture, DataTable source )
    {
        uxDepartmentFromList.Items.Clear();
        uxDepartmentToList.Items.Clear();
        string stringSerialized = String.Empty;

        DataTable currentDepartment = source;
        IList<Department> departmentList;

        if (KeyUtilities.IsMultistoreLicense())
            departmentList = DataAccessContext.DepartmentRepository.GetAllLeafDepartment( culture, "ParentDepartmentID" );
        else
            departmentList = DataAccessContext.DepartmentRepository.GetByRootIDLeafOnly( culture, DataAccessContext.Configurations.GetValue( "RootDepartment", DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID ) ), "ParentDepartmentID", BoolFilter.ShowAll );

        IList<Department> rootDepartmentList = DataAccessContext.DepartmentRepository.GetByRootID( culture, "0", "DepartmentID", BoolFilter.ShowAll );
        Dictionary<string, string> rootDict = new Dictionary<string, string>();
        foreach (Department rootDepartment in rootDepartmentList)
        {
            if (!rootDict.ContainsKey( rootDepartment.DepartmentID ))
                rootDict.Add( rootDepartment.DepartmentID, rootDepartment.Name );
        }

        string currentParentID = "";
        string tmpFullPath = "";
        for (int i = 0; i < departmentList.Count; i++)
        {
            if (departmentList[i].RootID == "0")
                continue;

            if (currentParentID != departmentList[i].ParentDepartmentID)
            {
                tmpFullPath = departmentList[i].CreateFullDepartmentPathParentOnly();
                currentParentID = departmentList[i].ParentDepartmentID;
            }

            string displayName = departmentList[i].CreateFullDepartmentPath() + " (" + departmentList[i].DepartmentID + ")";

            if (KeyUtilities.IsMultistoreLicense())
            {
                string rootName = "";
                if (rootDict.ContainsKey( departmentList[i].RootID ))
                    rootName = rootDict[departmentList[i].RootID];

                displayName = rootName + " >> " + displayName;
            }
            ListItem listItem = new ListItem( displayName, departmentList[i].DepartmentID );

            if (IsAlreadyDepartment( currentDepartment, departmentList[i].DepartmentID ))
            {
                uxDepartmentToList.Items.Add( listItem );
                if (uxDepartmentToList.Items.Count != 1)
                    stringSerialized += ",";
                stringSerialized += listItem.Value;
            }
            else
            {
                uxDepartmentFromList.Items.Add( listItem );
            }      
        }
        uxDepartmentIDHidden.Value = stringSerialized;
    }
    #endregion

    #region protected
    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void uxFromButton_Load( object sender, EventArgs e )
    {
        uxFromButton.Attributes.Add( "onclick", CreateScriptMoveItem(
            uxDepartmentFromList.ClientID, uxDepartmentToList.ClientID, "uxFromDiv", "uxToDiv", uxDepartmentToList.ClientID ) );
    }

    protected void uxToButton_Load( object sender, EventArgs e )
    {
        uxToButton.Attributes.Add( "onclick", CreateScriptMoveItem(
            uxDepartmentToList.ClientID, uxDepartmentFromList.ClientID, "uxToDiv", "uxFromDiv", uxDepartmentToList.ClientID ) );
    }
    #endregion

    #region Public
    public string[] ConvertToDepartmentIDs()
    {
        return uxDepartmentIDHidden.Value.Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
    }

    public void SetupDropDownList( string productID, Culture culture, bool restore )
    {
        DataTable table = new DataTable();
        table.Columns.Add( new DataColumn( "ProductID", Type.GetType( "System.Int32" ) ) );
        table.Columns.Add( new DataColumn( "DepartmentID", Type.GetType( "System.Int32" ) ) );
        if (restore)
        {
            string[] departmentIDs = uxDepartmentIDHidden.Value.Split( 
                new char[] { ',' }, 
                StringSplitOptions.RemoveEmptyEntries );

            for (int i = 0; i < departmentIDs.Length; i++)
            {
                DataRow row = table.NewRow();
                row["ProductID"] = productID;
                row["DepartmentID"] = departmentIDs[i];
                table.Rows.Add( row );
            }
        }
        else
        {
            Product product = DataAccessContext.ProductRepository.GetOne( 
                culture, productID, new StoreRetriever().GetCurrentStoreID() );

            for (int i = 0; i < product.DepartmentIDs.Count; i++)
            {
                DataRow row = table.NewRow();
                row["ProductID"] = productID;
                row["DepartmentID"] = product.DepartmentIDs[i];
                table.Rows.Add( row );
            }
        }
        InitDropDownList( culture, table );
    }
    #endregion
}
