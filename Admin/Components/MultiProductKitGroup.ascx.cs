using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;

public partial class Admin_Components_MultiProductKitGroup : System.Web.UI.UserControl
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
            fromID, toID, divFromID, divToID, _itemListboxDefault, uxProductKitGroupIDHidden.ClientID, controlIDserialized );
    }

    private bool IsAlreadyProductKitGroup( DataTable tableProductKitGroup, string productKitGroupID )
    {
        DataRow[] rows = tableProductKitGroup.Select( "ProductKitGroupID = " + productKitGroupID );
        if (rows.Length > 0)
            return true;
        else
            return false;
    }

    private void InitDropDownList( Culture culture, DataTable source, string productID )
    {
        uxProductKitGroupFromList.Items.Clear();
        uxProductKitGroupToList.Items.Clear();
        string stringSerialized = String.Empty;

        DataTable currentProductKitGroup = source;
        IList<ProductKitGroup> productKitGroupList;

        productKitGroupList = DataAccessContext.ProductKitGroupRepository.GetAllExceptBaseProduct( culture, productID );
                
        Dictionary<string, string> listDict = new Dictionary<string, string>();
        foreach (ProductKitGroup productKitGroup in productKitGroupList)
        {
            if (!listDict.ContainsKey( productKitGroup.ProductKitGroupID ))
                listDict.Add( productKitGroup.ProductKitGroupID, productKitGroup.Name );
        }

        for (int i = 0; i < productKitGroupList.Count; i++)
        {
            string displayName = productKitGroupList[i].Name + " (" + productKitGroupList[i].ProductKitGroupID + ")";

            ListItem listItem = new ListItem( displayName, productKitGroupList[i].ProductKitGroupID );

            if (IsAlreadyProductKitGroup( currentProductKitGroup, productKitGroupList[i].ProductKitGroupID ))
            {
                uxProductKitGroupToList.Items.Add( listItem );
                if (uxProductKitGroupToList.Items.Count != 1)
                    stringSerialized += ",";
                stringSerialized += listItem.Value;
            }
            else
            {
                uxProductKitGroupFromList.Items.Add( listItem );
            }
        }
        uxProductKitGroupIDHidden.Value = stringSerialized;
    }
    #endregion

    #region protected
    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void uxFromButton_Load( object sender, EventArgs e )
    {
        uxFromButton.Attributes.Add( "onclick", CreateScriptMoveItem(
            uxProductKitGroupFromList.ClientID, uxProductKitGroupToList.ClientID, "uxFromDiv", "uxToDiv", uxProductKitGroupToList.ClientID ) );
    }

    protected void uxToButton_Load( object sender, EventArgs e )
    {
        uxToButton.Attributes.Add( "onclick", CreateScriptMoveItem(
            uxProductKitGroupToList.ClientID, uxProductKitGroupFromList.ClientID, "uxToDiv", "uxFromDiv", uxProductKitGroupToList.ClientID ) );
    }
    #endregion

    #region Public
    public string[] ConvertToProductKitGroupIDs()
    {
        return uxProductKitGroupIDHidden.Value.Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
    }

    public void SetupDropDownList( string productID, Culture culture, bool restore )
    {
        DataTable table = new DataTable();
        table.Columns.Add( new DataColumn( "ProductID", Type.GetType( "System.Int32" ) ) );
        table.Columns.Add( new DataColumn( "ProductKitGroupID", Type.GetType( "System.Int32" ) ) );
        if (restore)
        {
            string[] productKitGroupIDs = uxProductKitGroupIDHidden.Value.Split(
                new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries );

            for (int i = 0; i < productKitGroupIDs.Length; i++)
            {
                DataRow row = table.NewRow();
                row["ProductID"] = productID;
                row["ProductKitGroupID"] = productKitGroupIDs[i];
                table.Rows.Add( row );
            }
        }
        else
        {
            Product product = DataAccessContext.ProductRepository.GetOne(
                culture, productID, new StoreRetriever().GetCurrentStoreID() );

            for (int i = 0; i < product.ProductKits.Count; i++)
            {
                DataRow row = table.NewRow();
                row["ProductID"] = productID;
                row["ProductKitGroupID"] = product.ProductKits[i].ProductKitGroupID;
                table.Rows.Add( row );
            }
        }
        InitDropDownList( culture, table, productID );
    }
    #endregion
}
