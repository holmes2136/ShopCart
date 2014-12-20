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
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Shared.Utilities;
using System.Drawing;
using Vevo.Domain.Stores;
using Vevo.Shared.WebUI;
using Vevo.WebUI.Ajax;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Domain.Contents;
using Vevo.Domain.Products;
using Vevo.Domain.EmailTemplates;
using Vevo.WebUI.Widget;
using Vevo.Base.Domain;


public partial class Admin_MainControls_StoreList : AdminAdvancedBaseListControl
{
    #region Private

    private string _currentID = "0";
    private bool _emptyRow = false;

    private bool IsContainingOnlyEmptyRow
    {
        get { return _emptyRow; }
        set { _emptyRow = value; }
    }


    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.StoreRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list );
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }

        if (uxGrid.Rows.Count > 0)
        {
            DeleteVisible( true );
            uxPagingControl.Visible = true;
        }
        else
        {
            DeleteVisible( false );
            uxPagingControl.Visible = false;
        }

        if (!IsAdminModifiable())
        {
            uxAddButton.Visible = false;
            DeleteVisible( false );
        }
    }

    private void DeleteVisible( bool value )
    {
        uxDeleteButton.Visible = value;
        if (value)
        {
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {
                uxDeleteConfirmButton.TargetControlID = "uxDeleteButton";
                uxConfirmModalPopup.TargetControlID = "uxDeleteButton";
            }
            else
            {
                uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
                uxConfirmModalPopup.TargetControlID = "uxDummyButton";
            }
        }
        else
        {
            uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
            uxConfirmModalPopup.TargetControlID = "uxDummyButton";
        }
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.StoreItemPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxGrid, "StoreID" );

        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }


    private bool VerifyStoreName( string name, string id )
    {
        return DataAccessContext.StoreRepository.GetStoreIDByStoreName( name ) == id;
    }
    private void RefreshDeleteButton()
    {
        if (IsContainingOnlyEmptyRow)
            uxDeleteButton.Visible = false;
        else
            uxDeleteButton.Visible = true;
    }
    private void CreateDummyRow( IList<Store> list )
    {
        Store store = new Store();
        store.StoreID = "-1";
        list.Add( store );
    }
    private void DeleteStoreInformation( string storeID )
    {
        Culture culture = DataAccessContext.CultureRepository.GetOne(
            DataAccessContext.CultureRepository.GetIDByName( SystemConst.USCultureName ) );

        DeleteContentManagement( culture, storeID );
        DeleteEmailTemplate( culture, storeID );
    }

    private void DeleteContentManagement( Culture culture, string storeID )
    {
        IList<ContentMenuItem> menuList = DataAccessContext.ContentMenuItemRepository.GetByStoreID(
            culture, storeID, "ContentMenuItemID", BoolFilter.ShowAll );
        foreach (ContentMenuItem item in menuList)
        {
            DataAccessContext.ContentMenuRepository.Delete( item.ReferringMenuID );
            DataAccessContext.ContentMenuItemRepository.Delete( item.ContentMenuItemID );
        }
    }

    private void DeleteEmailTemplate( Culture culture, string storeID )
    { 
        IList<EmailTemplateDetail> emailList = DataAccessContext.EmailTemplateDetailRepository.GetAll(
                culture, "EmailTemplateDetailID", storeID );

        foreach (EmailTemplateDetail email in emailList)
        {
            DataAccessContext.EmailTemplateDetailRepository.Delete( email.EmailTemplateDetailID );
        }
    }

    private void SetFooterRowFocus()
    {
        Control textBox = uxGrid.FooterRow.FindControl( "uxNameText" );
        AjaxUtilities.GetScriptManager( this ).SetFocus( textBox );
    }
    private string GetUrlPath( string url )
    {
        return UrlPath.ExtractWWWandHTTPinUrl( url.Trim() );
    }
    private bool VerifyUrlWithKey( string url )
    {
        return KeyUtilities.Verify( NamedConfig.DomainRegistrationKey, url );
    }
    #endregion

    #region Protected
    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected override void RefreshGrid()
    {
        int totalItems;

        IList<Store> list = DataAccessContext.StoreRepository.SearchStore(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );

        if (list == null || list.Count == 0)
        {
            IsContainingOnlyEmptyRow = true;
            list = new List<Store>();
            CreateDummyRow( list );
        }
        else
        {
            IsContainingOnlyEmptyRow = false;
        }

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
        uxGrid.DataSource = list;
        uxGrid.DataBind();

        RefreshDeleteButton();

        if (uxGrid.ShowFooter)
        {
            Control nameText = uxGrid.FooterRow.FindControl( "uxNameText" );
            Control addButton = uxGrid.FooterRow.FindControl( "uxAddLinkButton" );

            WebUtilities.TieButton( this.Page, nameText, addButton );
        }
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string id = uxGrid.DataKeys[row.RowIndex]["StoreID"].ToString().Trim();
                    DataAccessContext.StoreRepository.Delete( id );
                    DeleteStoreInformation( id );
                    DataAccessContext.ConfigurationRepository.DeleteConfigValueByStoreID( id );
                    DataAccessContext.NewsLetterRepository.DeleteEmailByStore( DataAccessContext.StoreRepository.GetOne( id ) );
                    DataAccessContext.NewsRepository.DeleteByStoreID( id );
                    DataAccessContext.ProductRepository.DeleteProductPricesByStoreID( id );
                    DataAccessContext.ProductRepository.DeleteProductMetaInformationByStoreID( id );
                    deleted = true;
                }
            }
            uxGrid.EditIndex = -1;
            if (deleted)
            {
                uxMessage.DisplayMessage( Resources.StoreMessages.DeleteSuccess );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if (uxGrid.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        uxGrid.EditIndex = -1;
        uxGrid.ShowFooter = true;
        uxGrid.ShowHeader = true;
        RefreshGrid();

        uxAddButton.Visible = false;

        SetFooterRowFocus();
    }

    protected Color GetEnabledColor( object isEnabled )
    {
        if (ConvertUtilities.ToBoolean( isEnabled ))
            return Color.DarkGreen;
        else
            return Color.Chocolate;
    }

    protected void uxGrid_RowEditing( object sender, GridViewEditEventArgs e )
    {
        uxGrid.EditIndex = e.NewEditIndex;
        RefreshGrid();
    }

    protected void uxGrid_CancelingEdit( object sender, GridViewCancelEditEventArgs e )
    {
        uxGrid.EditIndex = -1;
        RefreshGrid();
    }

    private void SetDefaultDisplayCurrencyCode( Store store )
    {
        string baseCurrency = DataAccessContext.Configurations.GetValue( "BaseWebsiteCurrency" );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["DefaultDisplayCurrencyCode"],
            baseCurrency,
            store );
    }

    private void SetProductPrice( Store store )
    {
        DataAccessContext.ProductRepository.CreateProductPricesFromDefaultByStoreID( store.StoreID );
    }

    protected void uxGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        string url = ((TextBox) uxGrid.FooterRow.FindControl( "uxUrlNameText" )).Text;
        string name = ((TextBox) uxGrid.FooterRow.FindControl( "uxNameText" )).Text;
        try
        {
            if (e.CommandName == "Add")
            {
                if (!VerifyStoreName( name, "0" ))
                {
                    uxMessage.DisplayError( Resources.StoreMessages.DuplicateStoreName );
                    return;
                }
                if (VerifyUrlWithKey( url ))
                {
                    if (VerifyUrlName( url ))
                    {
                        Store store = new Store();
                        store.StoreName = name;
                        store.UrlName = GetUrlPath( url );
                        store.IsEnabled = ((CheckBox) uxGrid.FooterRow.FindControl( "uxIsEnabledCheck" )).Checked;

                        store = DataAccessContext.StoreRepository.Save( store );
                        store.CreateStoreConfigCollection( store.StoreID );

                        SetDefaultDisplayCurrencyCode( store );
                        SetProductPrice( store );

                        WidgetDirector widgetDirector = new WidgetDirector();
                        foreach (string widgetConfigName in widgetDirector.WidgetConfigurationNameAll)
                        {
                            DataAccessContext.ConfigurationRepository.UpdateValue(
                                DataAccessContext.Configurations[widgetConfigName],
                            DataAccessContext.Configurations.GetValue( widgetConfigName, Store.Null ),
                                store );
                        }

                        uxMessage.DisplayMessage( Resources.StoreMessages.AddSuccess );

                        RefreshGrid();
                    }
                    else
                    {
                        uxMessage.DisplayError( Resources.StoreMessages.DuplicateUrl );
                    }
                }
                else
                {
                    uxMessage.DisplayError( Resources.StoreMessages.DomainKeyError );
                }
            }
            else if (e.CommandName == "Edit")
            {
                uxGrid.ShowFooter = false;
                uxAddButton.Visible = true;
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }


    }

    protected void uxGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            string storeID = ((Label) uxGrid.Rows[e.RowIndex].FindControl( "uxStoreIDLabel" )).Text;
            string name = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxNameText" )).Text;
            string url = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxUrlNameText" )).Text;

            if (!String.IsNullOrEmpty( storeID ))
            {
                _currentID = storeID;
                if (!VerifyStoreName( name, _currentID ) && !VerifyStoreName( name, "0" ))
                {
                    uxMessage.DisplayError( Resources.StoreMessages.DuplicateStoreName );
                    return;
                }
                if (VerifyUrlWithKey( url ))
                {
                    if (VerifyUrlName( url ))
                    {
                        Store store = DataAccessContext.StoreRepository.GetOne( storeID );
                        store.StoreName = name;
                        store.UrlName = GetUrlPath( url );
                        store.IsEnabled
                            = ((CheckBox) uxGrid.Rows[e.RowIndex].FindControl( "uxIsEnabledCheck" )).Checked;
                        store = DataAccessContext.StoreRepository.Save( store );
                        uxMessage.DisplayMessage( Resources.StoreMessages.UpdateSuccess );
                        // End editing
                        uxGrid.EditIndex = -1;
                        RefreshGrid();
                    }
                    else
                        uxMessage.DisplayError( Resources.StoreMessages.DuplicateUrl );
                }
                else
                {
                    uxMessage.DisplayError( Resources.StoreMessages.DomainKeyError );
                }
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;
        }
    }

    protected void uxGrid_DataBound( object sender, EventArgs e )
    {
        if (IsContainingOnlyEmptyRow)
        {
            uxGrid.Rows[0].Visible = false;
        }
    }
    #endregion

    #region Public
    public bool VerifyUrlName( string url )
    {
        string storeID = DataAccessContext.StoreRepository.GetStoreIDByUrlName( GetUrlPath( url ) );
        if (_currentID == "0") //Add mode
        {
            return (storeID == "0");
        }
        else //Edit mode
        {
            if (storeID != _currentID)
            {
                return (storeID == "0");
            }
            else
            {
                return true;
            }
        }
    }
    #endregion
}
