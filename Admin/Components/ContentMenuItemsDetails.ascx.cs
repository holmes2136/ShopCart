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
using Vevo;
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.Shared.Utilities;
using Vevo.WebUI.ServerControls;
using Vevo.Domain.Stores;

public partial class AdminAdvanced_Components_ContentMenuItemsDetails : AdminAdvancedBaseUserControl
{
    #region Private
    private enum Mode { Add, Edit };
    private Mode _mode = Mode.Add;
    private string OriginalReferring
    {
        get
        {
            return ViewState["OriginalReferring"].ToString();
        }
        set
        {
            ViewState["OriginalReferring"] = value; ;
        }
    }

    private string ParentContentMenuItemID
    {
        get
        {
            if (ViewState["ParentContentMenuItemID"] == null)
                return "0";
            else
                return (string) ViewState["ParentContentMenuItemID"];
        }
        set
        {
            ViewState["ParentContentMenuItemID"] = value;
        }
    }

    private string Position
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["Position"] ))
                return MainContext.QueryString["Position"];
            else
                return String.Empty;
        }
    }

    private Store CurrentStore
    {
        get
        {
            return DataAccessContext.StoreRepository.GetOne( StoreID );
        }
    }

    private string StoreID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
            {
                if (MainContext.QueryString["StoreID"] != null)
                    return MainContext.QueryString["StoreID"];
                else
                    return Store.RegularStoreID;
            }
            else
            {
                return Store.RegularStoreID;
            }
        }
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private void UpdateContentMenuItemTypeControls()
    {
        switch (uxContentMenuItemTypeDrop.SelectedValue)
        {
            case "0":
                uxContentListPanel.Visible = false;
                break;

            case "1":
                PopulateContentList();
                break;
        }

    }

    private void PopulateControls()
    {
        if (CurrentID != null && int.Parse( CurrentID ) >= 0)
        {
            ContentMenuItem contentMenuItem = DataAccessContext.ContentMenuItemRepository.GetOne( uxLanguageControl.CurrentCulture, CurrentID );

            uxContentMenuItemNameText.Text = contentMenuItem.Name;
            uxContentMenuItemDescriptionText.Text = contentMenuItem.Description;

            if (!contentMenuItem.LinksToContent())
            {
                uxContentMenuItemTypeDrop.SelectedValue = "0";
                OriginalReferring = "0";
                IList<ContentMenuItem> list = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID( uxLanguageControl.CurrentCulture, contentMenuItem.ReferringMenuID, "", BoolFilter.ShowAll );
                if (list.Count > 0)
                {
                    uxContentMenuItemTypeDrop.Enabled = false;
                }
                uxContentListPanel.Visible = false;
            }
            else
            {
                uxContentMenuItemTypeDrop.SelectedValue = "1";
                OriginalReferring = "1";
                PopulateContentList();
                uxContentListDrop.SelectedValue = contentMenuItem.ContentID.ToString();
            }
            uxParentDrop.SelectedValueItem = contentMenuItem.ContentMenuID;
            ParentContentMenuItemID = contentMenuItem.ContentMenuID;
            uxContentMenuItemEnabledCheck.Checked = contentMenuItem.IsEnabled;
            uxOther1Text.Text = contentMenuItem.Other1;
            uxOther2Text.Text = contentMenuItem.Other2;
            uxOther3Text.Text = contentMenuItem.Other3;

        }

    }

    private void PopulateContentList()
    {
        uxContentListDrop.Items.Clear();
        uxContentListDrop.DataSource 
            = DataAccessContext.ContentRepository.GetAll( uxLanguageControl.CurrentCulture, BoolFilter.ShowAll, "ContentID" );
        uxContentListDrop.DataBind();
        uxContentListPanel.Visible = true;

    }


    private void SetUpContentMenuItem( ContentMenuItem contentmenuitem )
    {
        contentmenuitem.ContentMenuID = uxParentDrop.SelectedValueItem;
        contentmenuitem.Name = uxContentMenuItemNameText.Text.Trim();
        contentmenuitem.Description = uxContentMenuItemDescriptionText.Text.Trim();
        contentmenuitem.IsEnabled = uxContentMenuItemEnabledCheck.Checked;
        contentmenuitem.Other1 = uxOther1Text.Text.Trim();
        contentmenuitem.Other2 = uxOther2Text.Text.Trim();
        contentmenuitem.Other3 = uxOther3Text.Text.Trim();
    }

    private bool IsReferringChanged()
    {
        if (uxContentMenuItemTypeDrop.SelectedValue != OriginalReferring)
            return true;
        else
            return false;
    }

    private void SetUpContentID( ContentMenuItem item )
    {
        if (uxContentMenuItemTypeDrop.SelectedValue == "0")
        {
            item.ContentID = "0";
        }
        else
        {
            item.ContentID = uxContentListDrop.SelectedValue;
        }
    }

    private void SetUpNewReferring( ContentMenuItem item )
    {
        if (uxContentMenuItemTypeDrop.SelectedValue == "0")
        {
            ContentMenu newContentMenu = new ContentMenu();
            newContentMenu.IsEnabled = item.IsEnabled;
            newContentMenu = DataAccessContext.ContentMenuRepository.Save( newContentMenu );
            item.ReferringMenuID = newContentMenu.ContentMenuID;
        }
        else
        {
            item.ReferringMenuID = "0";
        }
    }

    private void CleanUpUnreferredMenu( ContentMenuItem item )
    {
        if (uxContentMenuItemTypeDrop.SelectedValue != "0")
        {
            // Delete old menu since no one refers to it anymore.
            DataAccessContext.ContentMenuRepository.Delete( item.ReferringMenuID );
        }
    }

    private void ClearInputFields()
    {
        uxContentListPanel.Visible = false;
        uxContentMenuItemNameText.Text = string.Empty;
        uxContentMenuItemDescriptionText.Text = string.Empty;

        uxContentMenuItemEnabledCheck.Checked = true;
        uxOther1Text.Text = string.Empty;
        uxOther2Text.Text = string.Empty;
        uxOther3Text.Text = string.Empty;
        uxContentMenuItemTypeDrop.SelectedValue = null;
        uxParentDrop.SelectedValueItem = "0";
    }

    private IList<ContentMenuItem> GetMenuList( IList<ContentMenuItem> allmenuList, string parentID )
    {
        IList<ContentMenuItem> list = DataAccessContext.ContentMenuItemRepository.GetByContentMenuID(
    uxLanguageControl.CurrentCulture, parentID, "SortOrder", BoolFilter.ShowAll );
        foreach (ContentMenuItem item in list)
        {
            if (item.ReferringMenuID != "0")
            {
                allmenuList.Add( item );
                allmenuList = GetMenuList( allmenuList, item.ReferringMenuID );
            }
        }
        return allmenuList;
    }

    private void SetUpParentMenuDrop()
    {
        uxParentDrop.CultureID = uxLanguageControl.CurrentCulture.CultureID;
        uxParentDrop.RootContentMenuID = ContentMenuID;
        if (IsEditMode())
        {
            ContentMenuItem contentMenuItem = DataAccessContext.ContentMenuItemRepository.GetOne( uxLanguageControl.CurrentCulture, CurrentID );
            uxParentDrop.ContentMenuItemIDToExclude = CurrentID;
        }
    }

    private ContentMenuItem.MenuPositionType GetPosition()
    {
        return EnumUtilities<ContentMenuItem.MenuPositionType>.Parse( Position );
    }

    private void SetUpMenuLocationHeader()
    {
        if (KeyUtilities.IsMultistoreLicense())
            uxBreadcrumbLabel.Text = "Store: " + CurrentStore.StoreName + " (" + CurrentStore.StoreID + "), Position: " + Position + " Menu";
        else
            uxBreadcrumbLabel.Text = Position + " Menu";
    }

    #endregion

    #region Protected

    protected string CurrentID
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["ContentMenuItemID"] ))
                return MainContext.QueryString["ContentMenuItemID"];
            else
                return "0";
        }
    }

    protected string ContentMenuID
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["ContentMenuID"] ))
                return MainContext.QueryString["ContentMenuID"];
            else
                return "0";
        }
    }

    protected void uxContentMenuItemTypeDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        UpdateContentMenuItemTypeControls();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpMenuLocationHeader();
        uxLanguageControl.BubbleEvent += new EventHandler( Details_RefreshHandler );
        uxContentListPanel.Visible = false;
        uxContentMenuItemEnabledCheck.Checked = true;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        SetUpParentMenuDrop();

        if (IsEditMode())
        {
            if (!MainContext.IsPostBack)
            {
                PopulateControls();

            }

            if (IsAdminModifiable())
            {
                uxEditButton.Visible = true;
            }
            else
            {
                uxEditButton.Visible = false;
            }

            uxAddButton.Visible = false;

        }
        else
        {
            if (IsAdminModifiable())
            {
                uxAddButton.Visible = true;
                uxEditButton.Visible = false;
            }
            else
            {
                MainContext.RedirectMainControl( "ContentMenuItemList.ascx", "" );
            }
        }
    }

    protected void uxEditButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                ContentMenuItem contentmenuitem = DataAccessContext.ContentMenuItemRepository.GetOne(
                    uxLanguageControl.CurrentCulture, CurrentID );

                if (uxParentDrop.SelectedValueItem != contentmenuitem.ContentMenuID.ToString())
                {
                    DataAccessContext.ContentMenuItemRepository.ReArrangeSortOrder(
                        uxLanguageControl.CurrentCulture, uxParentDrop.SelectedValueItem,
                        CurrentID, BoolFilter.ShowTrue );
                }

                SetUpContentMenuItem( contentmenuitem );
                SetUpContentID( contentmenuitem );
                if (IsReferringChanged())
                {
                    CleanUpUnreferredMenu( contentmenuitem );
                    SetUpNewReferring( contentmenuitem );
                }
                contentmenuitem = DataAccessContext.ContentMenuItemRepository.Save( contentmenuitem );

                DataAccessContext.ContentMenuItemRepository.ReArrangeSortOrderByContentMenuID(
                    uxLanguageControl.CurrentCulture, ParentContentMenuItemID, BoolFilter.ShowTrue );

                uxMessage.DisplayMessage( Resources.ContentMenuItemMessages.UpdateSuccess );
                PopulateControls();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                ContentMenuItem contentmenuitem = new ContentMenuItem(
                    uxLanguageControl.CurrentCulture );
                SetUpContentMenuItem( contentmenuitem );
                SetUpNewReferring( contentmenuitem );
                SetUpContentID( contentmenuitem );
                contentmenuitem.MenuPosition = GetPosition();
                contentmenuitem.StoreID = StoreID;
                contentmenuitem = DataAccessContext.ContentMenuItemRepository.Save( contentmenuitem );

                uxMessage.DisplayMessage( Resources.ContentMenuItemMessages.AddSuccess );
                ClearInputFields();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    #endregion

    #region Public
    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }
    #endregion

}
