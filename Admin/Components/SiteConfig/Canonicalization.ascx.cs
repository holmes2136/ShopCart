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
using Vevo.Domain.Stores;
using System.Collections.Generic;
using Vevo;

public partial class Admin_Components_SiteConfig_Canonicalization : System.Web.UI.UserControl, IConfigUserControl
{
    #region Public

    public void PopulateControls()
    {
        uxCanonicalizationEnabledDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "CanonicalizationLayout" );
        enableCanonicalization();

        getCanonicalizationStoreList();
        uxCanonicalizationURLDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "CanonicalizationStore" );
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CanonicalizationLayout"],
            uxCanonicalizationEnabledDrop.SelectedValue );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CanonicalizationStore"],
            uxCanonicalizationURLDrop.SelectedValue );
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsMultistoreLicense())
        {
            uxCanonicalizationEnabledTR.Visible = false;
            uxCanonicalizationURLTR.Visible = false;
        }
    }

    protected void uxCanonicalizationEnabledDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        enableCanonicalization();
    }

    protected void getCanonicalizationStoreList()
    {
        IList<Store> list = DataAccessContext.StoreRepository.GetAll( "StoreID" );
        uxCanonicalizationURLDrop.Items.Clear();
        ListItem item;

        foreach (Store store in list)
        {
            item = new ListItem();
            item.Value = store.StoreID;
            item.Text = store.StoreName;
            uxCanonicalizationURLDrop.Items.Add( item );
        }
    }

    protected void enableCanonicalization()
    {
        if (uxCanonicalizationEnabledDrop.SelectedValue.Equals( "True" ))
        {
            uxCanonicalizationURLTR.Visible = true;
        }
        else
        {
            uxCanonicalizationURLTR.Visible = false;
        }
    }

    #endregion

    #region IConfigUserControl Members

    public void Populate( Vevo.Domain.Configurations.Configuration config )
    {
        PopulateControls();
    }

    #endregion
}
