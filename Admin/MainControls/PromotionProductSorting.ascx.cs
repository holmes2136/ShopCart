using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Domain.Stores;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Shared.DataAccess;

public partial class AdminAdvanced_MainControls_PromotionProductSorting : AdminAdvancedBaseUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
            MainContext.RedirectMainControl( "Default.ascx", String.Empty );

        uxLanguageControl.BubbleEvent += new EventHandler( Details_RefreshHandler );

        if (!MainContext.IsPostBack)
            PopulateControl();

        RegisterJavaScript();

        if (!IsAdminModifiable())
        {
            uxSortByNameButton.Visible = false;
            uxSortByIDButton.Visible = false;
            uxSaveButton.Visible = false;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {

    }

    protected void uxSortByNameButton_Click( object sender, EventArgs e )
    {
        PopulateListControls( "Name" );
    }

    protected void uxSortByIDButton_Click( object sender, EventArgs e )
    {
        PopulateListControls( "ProductID" );
    }

    protected void uxSaveButton_Click( object sender, EventArgs e )
    {
        try
        {
            string[] result;
            if (!String.IsNullOrEmpty( uxStatusHidden.Value ))
            {
                string checkValue = uxStatusHidden.Value.Replace( "product[]=", "" );
                result = checkValue.Split( '&' );
            }
            else
            {
                MatchCollection mc;
                Regex r = new Regex( @"product_(\d+)" );
                mc = r.Matches( uxListLabel.Text );
                result = new string[mc.Count];
                for (int i = 0; i < mc.Count; i++)
                {
                    result[i] = mc[i].Value.Replace( "product_", "" );
                }
            }

            DataAccessContextDeluxe.PromotionProductRepository.UpdateSortOrder( uxPromotionSubGroupDrop.SelectedValue, result );
            PopulateListControls( "SortOrder" );
            uxMessage.DisplayMessage( "Update sort order successfully." );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayError( ex.Message );
        }
    }

    protected void uxCancelButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "PromotionGroupList.ascx" );
    }

    private void PopulateControl()
    {
        uxPromotionGroupDrop.Items.Clear();
        IList<PromotionGroup> promotionGroupList = DataAccessContextDeluxe.PromotionGroupRepository.GetAllWithoutSubGroup(
            uxLanguageControl.CurrentCulture, "" );

        foreach (PromotionGroup promotionGroup in promotionGroupList)
        {
            uxPromotionGroupDrop.Items.Add( new ListItem( promotionGroup.Name, promotionGroup.PromotionGroupID ) );
        }

        uxPromotionSubGroupDrop.Items.Clear();

        IList<PromotionGroupSubGroup> promotionGroupSubGroupList = DataAccessContextDeluxe.PromotionGroupRepository.GetPromotionGroupSubGroup(
            uxPromotionGroupDrop.SelectedValue, "SortOrder" );

        for (int i = 0; i < promotionGroupSubGroupList.Count; i++)
        {
            ListItem listItem = new ListItem(
                DataAccessContextDeluxe.PromotionSubGroupRepository.GetOne( promotionGroupSubGroupList[i].PromotionSubGroupID ).Name + " (" + promotionGroupSubGroupList[i].PromotionSubGroupID + ")",
                promotionGroupSubGroupList[i].PromotionSubGroupID );

            uxPromotionSubGroupDrop.Items.Add( listItem );
        }

        if (!KeyUtilities.IsMultistoreLicense())
            uxPromotionGroupFilterPanel.Visible = false;

        PopulateListControls( "SortOrder" );
    }

    private void PopulateListControls( string sortBy )
    {
        uxListLabel.Text = "";
        uxStatusHidden.Value = "";

        IList<PromotionProduct> promotionProductList = DataAccessContextDeluxe.PromotionProductRepository.GetAllByPromotionSubGroupID(
            uxLanguageControl.CurrentCulture,
            uxPromotionSubGroupDrop.SelectedValue,
            sortBy );

        StringBuilder promotionProductListString = new StringBuilder();
        ArrayList productControlID = new ArrayList();

        if (promotionProductList.Count > 0)
        {
            foreach (PromotionProduct promotionProduct in promotionProductList)
            {
                promotionProductListString.Append( String.Format( "<li id='product_{0}'>{1} {2}</li>",
                    promotionProduct.ProductID,
                    promotionProduct.ProductID,
                    DataAccessContext.ProductRepository.GetOne( uxLanguageControl.CurrentCulture, promotionProduct.ProductID, new StoreRetriever().GetCurrentStoreID() ).Name ) );
            }
        }

        uxListLabel.Text = String.Format( "<ul id='sortList'>{0}</ul>", promotionProductListString.ToString() );

        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Test)
            uxMessage.DisplayMessage( "Test Sort Success" );
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControl();
    }

    private void RegisterJavaScript()
    {
        RegisterCustomScript();
    }

    private void RegisterCustomScript()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine( "$(document).ready(function(){" );
        sb.AppendLine( "$(\"#sortList\").sortable({" );
        sb.AppendLine( "stop: function() { Sortable_Changed(); }" );
        sb.AppendLine( "});" );
        sb.AppendLine( "});" );

        sb.AppendLine( "function Sortable_Changed()" );
        sb.AppendLine( "{" );
        sb.AppendLine( "var hiddenfield = document.getElementById( '" + uxStatusHidden.ClientID + "' );" );
        sb.AppendLine( "var state = $(\"#sortList\").sortable( 'serialize' )" );
        sb.AppendLine( "hiddenfield.value = state;" );
        sb.AppendLine( "}" );

        // Inside  UpdatePanel, Use ScriptManager to register Javascript
        // Don't use Page.ClientScript.RegisterStartupScript(typeof(Page), "script", sb.ToString(), true);
        ScriptManager.RegisterStartupScript( this, typeof( Page ), "sortingListScript", sb.ToString(), true );
    }

    protected void uxPromotionSubGroupDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        PopulateListControls( "SortOrder" );
    }

    protected void uxPromotionGroupDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        uxPromotionSubGroupDrop.Items.Clear();

        IList<PromotionGroupSubGroup> promotionGroupSubGroupList = DataAccessContextDeluxe.PromotionGroupRepository.GetPromotionGroupSubGroup(
            uxPromotionGroupDrop.SelectedValue, "SortOrder" );

        for (int i = 0; i < promotionGroupSubGroupList.Count; i++)
        {
            ListItem listItem = new ListItem(
                DataAccessContextDeluxe.PromotionSubGroupRepository.GetOne( promotionGroupSubGroupList[i].PromotionSubGroupID ).Name + " (" + promotionGroupSubGroupList[i].PromotionSubGroupID + ")",
                promotionGroupSubGroupList[i].PromotionSubGroupID );

            uxPromotionSubGroupDrop.Items.Add( listItem );
        }

        PopulateListControls( "SortOrder" );
    }
}
