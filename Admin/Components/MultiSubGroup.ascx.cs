using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.Domain;

public partial class Admin_Components_MultiSubGroup : System.Web.UI.UserControl
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
            fromID, toID, divFromID, divToID, _itemListboxDefault, uxSubGroupIDHidden.ClientID, controlIDserialized );
    }

    private bool IsAlreadySubGroup( DataTable tableSubGroup, string subGroupID )
    {
        DataRow[] rows = tableSubGroup.Select( "PromotionSubGroupID = " + subGroupID );
        if (rows.Length > 0)
            return true;
        else
            return false;
    }

    private void InitDropDownList( Culture culture, DataTable source )
    {
        uxSubGroupFromList.Items.Clear();
        uxSubGroupToList.Items.Clear();
        string stringSerialized = String.Empty;

        DataTable currentSubGroup = source;
        IList<PromotionSubGroup> subGroupList;

        subGroupList = DataAccessContextDeluxe.PromotionSubGroupRepository.GetAllExcludeBlank( "PromotionSubGroup.PromotionSubGroupID" );
        
        for (int i = 0; i < subGroupList.Count; i++)
        {
            string displayName = subGroupList[i].Name + " (" + subGroupList[i].PromotionSubGroupID + ")";

            ListItem listItem = new ListItem( displayName, subGroupList[i].PromotionSubGroupID );

            if (IsAlreadySubGroup( currentSubGroup, subGroupList[i].PromotionSubGroupID ))
            {
                uxSubGroupToList.Items.Add( listItem );
                if (uxSubGroupToList.Items.Count != 1)
                    stringSerialized += ",";
                stringSerialized += listItem.Value;
            }
            else
            {
                uxSubGroupFromList.Items.Add( listItem );
            }
        }
        uxSubGroupIDHidden.Value = stringSerialized;
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void uxFromButton_Load( object sender, EventArgs e )
    {
        uxFromButton.Attributes.Add( "onclick", CreateScriptMoveItem(
            uxSubGroupFromList.ClientID, uxSubGroupToList.ClientID, "uxFromDiv", "uxToDiv", uxSubGroupToList.ClientID ) );
    }

    protected void uxToButton_Load( object sender, EventArgs e )
    {
        uxToButton.Attributes.Add( "onclick", CreateScriptMoveItem(
            uxSubGroupToList.ClientID, uxSubGroupFromList.ClientID, "uxToDiv", "uxFromDiv", uxSubGroupToList.ClientID ) );
    }

    public string[] ConvertToSubGroupIDs()
    {
        return uxSubGroupIDHidden.Value.Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
    }

    public void SetupDropDownList( string groupID, Culture culture, bool restore )
    {
        DataTable table = new DataTable();
        table.Columns.Add( new DataColumn( "PromotionGroupID", Type.GetType( "System.Int32" ) ) );
        table.Columns.Add( new DataColumn( "PromotionSubGroupID", Type.GetType( "System.Int32" ) ) );

        if (restore)
        {
            string[] subGroupIDs = uxSubGroupIDHidden.Value.Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
            for (int i = 0; i < subGroupIDs.Length; i++)
            {
                DataRow row = table.NewRow();
                row["PromotionGroupID"] = groupID;
                row["PromotionSubGroupID"] = subGroupIDs[i];
                table.Rows.Add( row );
            }
        }
        else
        {
            PromotionGroup promotionGroup = DataAccessContextDeluxe.PromotionGroupRepository.GetOne( culture, groupID );
            for (int i = 0; i < promotionGroup.PromotionGroupSubGroup.Count; i++)
            {
                DataRow row = table.NewRow();
                row["PromotionGroupID"] = groupID;
                row["PromotionSubGroupID"] = promotionGroup.PromotionGroupSubGroup[i].PromotionSubGroupID;
                table.Rows.Add( row );
            }
        }
        InitDropDownList( culture, table );
    }
}
