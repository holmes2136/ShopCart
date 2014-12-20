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
using Vevo.Domain;
using Vevo;
using Vevo.Domain.Stores;

public partial class Admin_Components_StoreConfig_StorePointSystemConfig : AdminAdvancedBaseUserControl
{
    private Store CurrentStore
    {
        get
        {
            return DataAccessContext.StoreRepository.GetOne( StoreID );
        }
    }

    public string StoreID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
            {
                return MainContext.QueryString["StoreID"];
            }
            else
            {
                return Store.RegularStoreID;
            }
        }
    }

    public void PopulateControls()
    {
        uxEnablePointSystemDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue( "PointSystemEnabled", CurrentStore ).ToString();
        uxRewardPointsText.Text = DataAccessContext.Configurations.GetValue( "RewardPoints", CurrentStore );
        uxPointValueText.Text = DataAccessContext.Configurations.GetValue( "PointValue", CurrentStore );
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
             DataAccessContext.Configurations["PointSystemEnabled"],
             uxEnablePointSystemDrop.SelectedValue,
             CurrentStore );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["RewardPoints"],
            uxRewardPointsText.Text,
            CurrentStore );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PointValue"],
            uxPointValueText.Text,
            CurrentStore );

    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (uxEnablePointSystemDrop.SelectedValue == "True")
            uxPointSystemDetailsPanel.Visible = true;
        else
            uxPointSystemDetailsPanel.Visible = false;
    }
}
