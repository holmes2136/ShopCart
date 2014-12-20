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
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Contents;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


public partial class AdminAdvanced_MainControls_ContentMenuSorting : AdminAdvancedBaseUserControl
{
    #region Private
    private string ContentMenuID
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["ContentMenuID"] ))
                return MainContext.QueryString["ContentMenuID"];
            else
                return String.Empty;
        }
    }

    private bool ContentMenuItemCount()
    {
        IList<ContentMenuItem> list = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
    uxLanguageControl.CurrentCulture, ContentMenuID, "SortOrder", BoolFilter.ShowAll );


        if (list.Count == 0)
            return false;
        else
            return true;
    }

    private void PopulateDropDown()
    {
        uxContentMenuDrop.Items.Clear();
        uxContentMenuDrop.Items.Add( new ListItem( "Root", ContentMenuID, true ) );
        IList<ContentMenuItem> allMenuList = new List<ContentMenuItem>();
        allMenuList = GetMenuList( allMenuList, ContentMenuID );

        foreach (ContentMenuItem item in allMenuList)
        {
            uxContentMenuDrop.Items.Add( new ListItem(
                item.GetFullContentPath( uxLanguageControl.CurrentCulture ), item.ReferringMenuID ) );
        }
    }

    private void PopulateControl()   //drop down Parent 
    {
        PopulateDropDown();
        if (ContentMenuItemCount())
            PopulateListControls( "SortOrder" );
        else
        {
            uxSortByNameButton.Visible = false;
            uxSortByIDButton.Visible = false;
            uxContentData.Visible = false;
            uxNoData.Visible = true;
        }

    }

    private IList<ContentMenuItem> GetMenuList( IList<ContentMenuItem> allMenuList, string parentID )
    {
        IList<ContentMenuItem> list = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
    uxLanguageControl.CurrentCulture, parentID, "SortOrder", BoolFilter.ShowAll );
        foreach (ContentMenuItem item in list)
        {
            if (item.ReferringMenuID != "0")
            {
                allMenuList.Add( item );
                allMenuList = GetMenuList( allMenuList, item.ReferringMenuID );
            }
        }
        return allMenuList;
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

    private void PopulateListControls( string sortBy )
    {
        uxListLabel.Text = "";
        uxStatusHidden.Value = "";

        IList<ContentMenuItem> list = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
    uxLanguageControl.CurrentCulture, uxContentMenuDrop.SelectedValue, sortBy, BoolFilter.ShowTrue );

        string contentMenuList = String.Empty;
        ArrayList contentMenuControlID = new ArrayList();

        if (list.Count > 0)
        {
            foreach (ContentMenuItem item in list)
            {
                contentMenuList += String.Format( "<li id='contentmenuitem_{0}'> {1}</li>", item.ContentMenuItemID, item.Name );
            }
        }
        uxListLabel.Text = String.Format( "<ul id='sortList'>{0}</ul>", contentMenuList );

        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Test)
            uxMessage.DisplayMessage( "Test Sort Success" );
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateListControls( "SortOrder" );
    }

    #endregion


    #region Protected
    protected void Page_Load( object sender, EventArgs e )
    {
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

    protected void uxContentMenuDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        PopulateListControls( "SortOrder" );
    }

    protected void uxSortByNameButton_Click( object sender, EventArgs e )
    {
        PopulateListControls( "Name" );
    }

    protected void uxSortByIDButton_Click( object sender, EventArgs e )
    {
        PopulateListControls( "ContentMenuItemID" );
    }

    protected void uxSaveButton_Click( object sender, EventArgs e )
    {
        try
        {
            string[] result;
            if (!String.IsNullOrEmpty( uxStatusHidden.Value ))
            {
                string checkValue = uxStatusHidden.Value.Replace( "contentmenuitem[]=", "" );
                result = checkValue.Split( '&' );
            }
            else
            {
                MatchCollection mc;
                Regex r = new Regex( @"contentmenuitem_(\d+)" );
                mc = r.Matches( uxListLabel.Text );
                result = new string[mc.Count];
                for (int i = 0; i < mc.Count; i++)
                {
                    result[i] = mc[i].Value.Replace( "contentmenuitem_", "" );
                }
            }
            DataAccessContext.ContentMenuItemRepository.UpdateSortOrder( result );
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
        MainContext.RedirectMainControl( "ContentMenuItemList.ascx" );
    }
    #endregion
}
