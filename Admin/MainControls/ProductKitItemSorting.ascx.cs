using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;

public partial class Admin_MainControls_ProductKitItemSorting : AdminAdvancedBaseUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        uxLanguageControl.BubbleEvent += new EventHandler( Details_RefreshHandler );

        if ( !MainContext.IsPostBack )
            PopulateControl();

        RegisterJavaScript();

        if ( !IsAdminModifiable() )
        {
            uxSortByNameButton.Visible = false;
            uxSortByIDButton.Visible = false;
            uxSaveButton.Visible = false;
        }
    }

    protected void uxFilterDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        PopulateListControls( "SortOrder" );
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
            if ( !String.IsNullOrEmpty( uxStatusHidden.Value ) )
            {
                string checkValue = uxStatusHidden.Value.Replace( "item[]=", "" );
                result = checkValue.Split( '&' );
            }
            else
            {
                MatchCollection mc;
                Regex r = new Regex( @"item_(\d+)" );
                mc = r.Matches( uxListLabel.Text );
                result = new string[ mc.Count ];
                for ( int i = 0; i < mc.Count; i++ )
                {
                    result[ i ] = mc[ i ].Value.Replace( "item_", "" );
                }
            }

            DataAccessContext.ProductKitGroupRepository.UpdateProductKitItemSortOrder( uxFilterDrop.SelectedValue, result );
            PopulateListControls( "SortOrder" );
            uxMessage.DisplayMessage( "Update sort order successfully." );
        }
        catch ( Exception ex )
        {
            uxMessage.DisplayError( ex.Message );
        }
    }

    protected void uxCancelButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "ProductKitGroupList.ascx" );
    }

    private void PopulateControl()
    {
        uxFilterDrop.Items.Clear();

        IList<ProductKitGroup> productKitGroupList = DataAccessContext.ProductKitGroupRepository.GetAllExceptBaseProduct(
            uxLanguageControl.CurrentCulture, uxFilterDrop.SelectedValue );

        foreach ( ProductKitGroup group in productKitGroupList )
        {
            uxFilterDrop.Items.Add( new ListItem( group.Name, group.ProductKitGroupID ) );
        }

        PopulateListControls( "SortOrder" );
    }

    private void PopulateListControls( string sortBy )
    {
        uxListLabel.Text = "";
        uxStatusHidden.Value = "";

        IList<Product> productKitItemList = DataAccessContext.ProductRepository.GetProductKitItems(
            uxLanguageControl.CurrentCulture,
            uxFilterDrop.SelectedValue,
            sortBy );

        string itemListString = String.Empty;
        ArrayList groupControlID = new ArrayList();

        if ( productKitItemList.Count > 0 )
        {
            foreach ( Product item in productKitItemList )
            {
                itemListString += String.Format( "<li id='item_{0}'>{1} {2}</li>", item.ProductID, item.ProductID, item.Name );
            }
        }
        uxListLabel.Text = String.Format( "<ul id='sortList'>{0}</ul>", itemListString );

        if ( AdminConfig.CurrentTestMode == AdminConfig.TestMode.Test )
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
}
