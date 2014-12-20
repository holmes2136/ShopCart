using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.DataAccess;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using Vevo;

public partial class AdminAdvanced_Components_MultiCatalog : System.Web.UI.UserControl
{
    private const int _itemListboxDefault = 8;

    private string CreateScriptMoveItem(
        string fromID,
        string toID,
        string divFromID,
        string divToID,
        string controlIDserialized )
    {
        return String.Format( "return fnMoveItems('{0}','{1}', '{2}', '{3}', {4}, '{5}', '{6}')",
            fromID, toID, divFromID, divToID, _itemListboxDefault, uxCategoryIDHidden.ClientID, controlIDserialized );
    }

    private bool IsAlreadyCategory( DataTable tableCategory, string categoryID )
    {
        DataRow[] rows = tableCategory.Select( "CategoryID = " + categoryID );
        if (rows.Length > 0)
            return true;
        else
            return false;
    }

    private void InitDropDownList( Culture culture, DataTable source )
    {
        uxCategoryFromList.Items.Clear();
        uxCategoryToList.Items.Clear();
        string stringSerialized = String.Empty;

        DataTable currentCategory = source;
        IList<Category> categoryList;

        if (KeyUtilities.IsMultistoreLicense())
            categoryList = DataAccessContext.CategoryRepository.GetAllLeafCategory( culture, "ParentCategoryID" );
        else
            categoryList = DataAccessContext.CategoryRepository.GetByRootIDLeafOnly( culture, DataAccessContext.Configurations.GetValue( "RootCategory", DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID ) ), "ParentCategoryID", BoolFilter.ShowAll );

        IList<Category> rootCategoryList = DataAccessContext.CategoryRepository.GetByRootID( culture, "0", "CategoryID", BoolFilter.ShowAll );
        Dictionary<string, string> rootDict = new Dictionary<string, string>();
        foreach (Category rootCategory in rootCategoryList)
        {
            if (!rootDict.ContainsKey( rootCategory.CategoryID ))
                rootDict.Add( rootCategory.CategoryID, rootCategory.Name );
        }

        string currentParentID = "";
        string tmpFullPath = "";
        for (int i = 0; i < categoryList.Count; i++)
        {
            if (categoryList[i].RootID == "0")
                continue;

            if (currentParentID != categoryList[i].ParentCategoryID)
            {
                tmpFullPath = categoryList[i].CreateFullCategoryPathParentOnly();
                currentParentID = categoryList[i].ParentCategoryID;
            }

            string displayName = categoryList[i].CreateFullCategoryPath() + " (" + categoryList[i].CategoryID + ")";

            if (KeyUtilities.IsMultistoreLicense())
            {
                string rootName = "";
                if (rootDict.ContainsKey( categoryList[i].RootID ))
                    rootName = rootDict[categoryList[i].RootID];

                displayName = rootName + " >> " + displayName;
            }
            ListItem listItem = new ListItem( displayName, categoryList[i].CategoryID );

            if (IsAlreadyCategory( currentCategory, categoryList[i].CategoryID ))
            {
                uxCategoryToList.Items.Add( listItem );
                if (uxCategoryToList.Items.Count != 1)
                    stringSerialized += ",";
                stringSerialized += listItem.Value;
            }
            else
            {
                uxCategoryFromList.Items.Add( listItem );
            }
        }
        uxCategoryIDHidden.Value = stringSerialized;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void uxFromButton_Load( object sender, EventArgs e )
    {
        uxFromButton.Attributes.Add( "onclick", CreateScriptMoveItem(
            uxCategoryFromList.ClientID, uxCategoryToList.ClientID, "uxFromDiv", "uxToDiv", uxCategoryToList.ClientID ) );
    }

    protected void uxToButton_Load( object sender, EventArgs e )
    {
        uxToButton.Attributes.Add( "onclick", CreateScriptMoveItem(
            uxCategoryToList.ClientID, uxCategoryFromList.ClientID, "uxToDiv", "uxFromDiv", uxCategoryToList.ClientID ) );
    }

    public string[] ConvertToCategoryIDs()
    {
        return uxCategoryIDHidden.Value.Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
    }

    public void SetupDropDownList( string productID, Culture culture, bool restore )
    {
        DataTable table = new DataTable();
        table.Columns.Add( new DataColumn( "ProductID", Type.GetType( "System.Int32" ) ) );
        table.Columns.Add( new DataColumn( "CategoryID", Type.GetType( "System.Int32" ) ) );
        if (restore)
        {
            string[] categoryIDs = uxCategoryIDHidden.Value.Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
            for (int i = 0; i < categoryIDs.Length; i++)
            {
                DataRow row = table.NewRow();
                row["ProductID"] = productID;
                row["CategoryID"] = categoryIDs[i];
                table.Rows.Add( row );
            }
        }
        else
        {
            Product product = DataAccessContext.ProductRepository.GetOne( culture, productID, new StoreRetriever().GetCurrentStoreID() );
            for (int i = 0; i < product.CategoryIDs.Count; i++)
            {
                DataRow row = table.NewRow();
                row["ProductID"] = productID;
                row["CategoryID"] = product.CategoryIDs[i];
                table.Rows.Add( row );
            }
        }
        InitDropDownList( culture, table );
    }

}
