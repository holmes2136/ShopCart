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
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.DataAccess;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using Vevo.Domain.Marketing;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.BundlePromotion;

public partial class AdminAdvanced_MainControls_PromotionSubGroupSorting : AdminAdvancedBaseUserControl
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
        PopulateListControls( "PromotionSubGroupID" );
    }

    protected void uxSaveButton_Click( object sender, EventArgs e )
    {
        try
        {
            string[] result;
            if (!String.IsNullOrEmpty( uxStatusHidden.Value ))
            {
                string checkValue = uxStatusHidden.Value.Replace( "promotionSubGroup[]=", "" );
                result = checkValue.Split( '&' );
            }
            else
            {
                MatchCollection mc;
                Regex r = new Regex( @"promotionSubGroup_(\d+)" );
                mc = r.Matches( uxListLabel.Text );
                result = new string[mc.Count];
                for (int i = 0; i < mc.Count; i++)
                {
                    result[i] = mc[i].Value.Replace( "promotionSubGroup_", "" );
                }
            }

            DataAccessContextDeluxe.PromotionGroupRepository.UpdateSortOrder( uxPromotionGroupDrop.SelectedValue, result );
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

        foreach (PromotionGroup promotionGrou in promotionGroupList)
        {
            uxPromotionGroupDrop.Items.Add( new ListItem( promotionGrou.Name, promotionGrou.PromotionGroupID ) );
        }

        if (!KeyUtilities.IsMultistoreLicense())
            uxPromotionGroupFilterPanel.Visible = false;

        PopulateListControls( "SortOrder" );
    }

    private void PopulateListControls( string sortBy )
    {
        uxListLabel.Text = "";
        uxStatusHidden.Value = "";

        IList<PromotionGroupSubGroup> promotionSubGroupList = DataAccessContextDeluxe.PromotionGroupRepository.GetPromotionGroupSubGroup(
            uxPromotionGroupDrop.SelectedValue, sortBy );

        StringBuilder promotionSubGroupListString = new StringBuilder();
        ArrayList productControlID = new ArrayList();

        if (promotionSubGroupList.Count > 0)
        {
            foreach (PromotionGroupSubGroup promotionSubGroup in promotionSubGroupList)
            {
                promotionSubGroupListString.Append( String.Format(
                    "<li id='promotionSubGroup_{0}'>{1} {2}</li>",
                    promotionSubGroup.PromotionSubGroupID,
                    promotionSubGroup.PromotionSubGroupID,
                    DataAccessContextDeluxe.PromotionSubGroupRepository.GetOne( promotionSubGroup.PromotionSubGroupID ).Name ) );
            }
        }
        uxListLabel.Text = String.Format( "<ul id='sortList'>{0}</ul>", promotionSubGroupListString.ToString() );

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

    protected void uxPromotionGroupDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        PopulateListControls( "SortOrder" );
    }
}
