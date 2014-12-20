using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using Vevo.Domain.Stores;
using Vevo.Domain;
using Vevo.Domain.Blogs;

public partial class Admin_Components_MultiStoreList : System.Web.UI.UserControl
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
            fromID, toID, divFromID, divToID, _itemListboxDefault, uxStoreIDHidden.ClientID, controlIDserialized );
    }

    private bool IsAlreadyStore( DataTable tableStore, string storeID )
    {
        DataRow[] rows = tableStore.Select( "StoreID = " + storeID );
        if (rows.Length > 0)
            return true;
        else
            return false;
    }

    private void InitDropDownList( DataTable source )
    {
        uxStoreFromList.Items.Clear();
        uxStoreToList.Items.Clear();
        string stringSerialized = String.Empty;

        DataTable currentStore = source;
        IList<Store> storeList;
        storeList = DataAccessContext.StoreRepository.GetAll( "StoreID" );

        foreach (Store store in storeList)
        {
            string displayName = store.StoreName + " (" + store.StoreID + ")";

            ListItem listItem = new ListItem( displayName, store.StoreID );

            if (IsAlreadyStore( currentStore, store.StoreID ))
            {
                uxStoreToList.Items.Add( listItem );
                if (uxStoreToList.Items.Count != 1)
                    stringSerialized += ",";
                stringSerialized += listItem.Value;
            }
            else
            {
                uxStoreFromList.Items.Add( listItem );
            }
        }

        uxStoreIDHidden.Value = stringSerialized;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void uxFromButton_Load( object sender, EventArgs e )
    {
        uxFromButton.Attributes.Add( "onclick", CreateScriptMoveItem(
            uxStoreFromList.ClientID, uxStoreToList.ClientID, "uxFromDiv", "uxToDiv", uxStoreToList.ClientID ) );
    }

    protected void uxToButton_Load( object sender, EventArgs e )
    {
        uxToButton.Attributes.Add( "onclick", CreateScriptMoveItem(
            uxStoreToList.ClientID, uxStoreFromList.ClientID, "uxToDiv", "uxFromDiv", uxStoreToList.ClientID ) );
    }

    public string[] ConvertToStoreIDs()
    {
        return uxStoreIDHidden.Value.Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
    }

    public void SetupDropDownList( string blogID, bool restore )
    {
        DataTable table = new DataTable();
        table.Columns.Add( new DataColumn( "BlogID", Type.GetType( "System.Int32" ) ) );
        table.Columns.Add( new DataColumn( "StoreID", Type.GetType( "System.Int32" ) ) );
        if (restore)
        {
            string[] storeIDs = uxStoreIDHidden.Value.Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
            for (int i = 0; i < storeIDs.Length; i++)
            {
                DataRow row = table.NewRow();
                row["BlogID"] = blogID;
                row["StoreID"] = storeIDs[i];
                table.Rows.Add( row );
            }
        }
        else
        {
            Blog blog = DataAccessContext.BlogRepository.GetOne( blogID );
            for (int i = 0; i < blog.StoreIDs.Count; i++)
            {
                DataRow row = table.NewRow();
                row["BlogID"] = blogID;
                row["StoreID"] = blog.StoreIDs[i];
                table.Rows.Add( row );
            }
        }
        InitDropDownList( table );
    }

}
