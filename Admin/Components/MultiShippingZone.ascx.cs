using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.Domain;
using Vevo.Domain.Shipping;
using System.Collections.Generic;

public partial class Admin_Components_MultiShippingZone : System.Web.UI.UserControl
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
            fromID, toID, divFromID, divToID, _itemListboxDefault, uxZoneIDHidden.ClientID, controlIDserialized );
    }

    private bool IsAlreadyShippingZone( DataTable table, string shippingZoneID )
    {
        DataRow[] rows = table.Select( "ZoneGroupID = " + shippingZoneID );
        if (rows.Length > 0)
            return true;
        else
            return false;
    }

    private void InitDropDownList( Culture culture, DataTable source )
    {
        uxZoneFromList.Items.Clear();
        uxZoneToList.Items.Clear();
        string stringSerialized = String.Empty;

        DataTable currentShippingZoneGroup = source;

        IList<ShippingZoneGroup> zoneGroupList = DataAccessContext.ShippingZoneGroupRepository.GetAll( "ZoneGroupID" );

        foreach (ShippingZoneGroup zone in zoneGroupList)
        {
            ListItem listItem = new ListItem( zone.ZoneName + " (" + zone.ZoneGroupID + ")", zone.ZoneGroupID );

            if (IsAlreadyShippingZone( currentShippingZoneGroup, zone.ZoneGroupID))
            {
                uxZoneToList.Items.Add( listItem );
                if (uxZoneToList.Items.Count != 1)
                    stringSerialized += ",";
                stringSerialized += listItem.Value;
            }
            else
            {
                uxZoneFromList.Items.Add( listItem );
            }
        }

        uxZoneIDHidden.Value = stringSerialized;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void uxFromButton_Load( object sender, EventArgs e )
    {
        uxFromButton.Attributes.Add( "onclick", CreateScriptMoveItem(
            uxZoneFromList.ClientID, uxZoneToList.ClientID, "uxFromDiv", "uxToDiv", uxZoneToList.ClientID ) );
    }

    protected void uxToButton_Load( object sender, EventArgs e )
    {
        uxToButton.Attributes.Add( "onclick", CreateScriptMoveItem(
            uxZoneToList.ClientID, uxZoneFromList.ClientID, "uxToDiv", "uxFromDiv", uxZoneToList.ClientID ) );
    }

    public string[] ConvertToZoneGroupIDs()
    {
        return uxZoneIDHidden.Value.Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
    }


    public void SetupDropDownList( string shippingID, Culture culture, bool restore )
    {
        DataTable table = new DataTable();
        table.Columns.Add( new DataColumn( "ShippingID", Type.GetType( "System.Int32" ) ) );
        table.Columns.Add( new DataColumn( "ZoneGroupID", Type.GetType( "System.Int32" ) ) );

        if (restore)
        {
            string[] zoneGroupIDs = uxZoneIDHidden.Value.Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
            for (int i = 0; i < zoneGroupIDs.Length; i++)
            {
                DataRow row = table.NewRow();
                row["ShippingID"] = shippingID;
                row["ZoneGroupID"] = zoneGroupIDs[i];
                table.Rows.Add( row );
            }
        }
        else
        {
            uxZoneIDHidden.Value = "";
            ShippingOption shipping = DataAccessContext.ShippingOptionRepository.GetOne( culture, shippingID );
            for (int i = 0; i < shipping.ShippingZone.Count; i++)
            {
                DataRow row = table.NewRow();
                row["ShippingID"] = shippingID;
                row["ZoneGroupID"] = shipping.ShippingZone[i].ZoneGroupID;
                table.Rows.Add( row );
            }
        }
        InitDropDownList( culture, table );
    }

    public void Clear()
    {
        uxZoneFromList.Items.Clear();
        uxZoneToList.Items.Clear();
        uxZoneIDHidden.Value = "";
    }
}
