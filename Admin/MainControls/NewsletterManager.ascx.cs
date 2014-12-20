using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Marketing;
using Vevo.Shared.DataAccess;
using Vevo.WebAppLib;
using Vevo.Domain.Stores;
using Vevo.Base.Domain;

public partial class AdminAdvanced_MainControls_NewsletterManager : AdminAdvancedBaseListControl
{
    private const int ColumnEmail = 1;
    private const int ColumnCustomerID = 2;

    #region private
    private string EmailOld
    {
        get
        {
            if (ViewState["EmailOld"] != null)
                return ViewState["EmailOld"].ToString();
            else
                return "";
        }
        set
        {
            ViewState["EmailOld"] = value;
        }
    }

    private string StoreIDOld
    {
        get
        {
            if (ViewState["StoreIDOld"] != null)
                return ViewState["StoreIDOld"].ToString();
            else
                return "";
        }
        set
        {
            ViewState["StoreIDOld"] = value;
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.NewsLetterRepository.GetTableSchema();
        if (KeyUtilities.IsMultistoreLicense())
            uxSearchFilter.SetUpSchema( list );
        else
            uxSearchFilter.SetUpSchema( list, "StoreName" );
    }

    private void ApplyPermission()
    {
        if (!IsAdminModifiable())
        {
            uxAddButton.Visible = false;
            DeleteVisible( false );
        }
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }

        if (uxGridNewsletter.Rows.Count > 0)
        {
            DeleteVisible( true );
            uxPagingControl.Visible = true;
        }
        else
        {
            DeleteVisible( false );
            uxPagingControl.Visible = false;
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

    private void ClearField()
    {
        uxEmailText.Text = "";
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.NewsletterItemsPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxGridNewsletter, "Email" );

        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }

    private bool checkEmailFormat( string email, string storeID )
    {
        if (email == null | email == string.Empty)
        {
            uxMessage.DisplayError( Resources.NewsletterManager.RegisterEmpty );
            return false;
        }

        if (storeID == null | storeID == string.Empty)
        {
            uxMessage.DisplayError( Resources.NewsletterManager.StoreEmpty );
            return false;
        }
        Regex emailregex = new Regex( @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" );
        Match m = emailregex.Match( email );
        return m.Success;
    }

    private void RegisterEmailUpdate( string email, string emailOld, string storeID, string storeIDOld )
    {
        bool isCorrectFormat = checkEmailFormat( email, storeID );
        if (isCorrectFormat)
        {
            string emailHash =
                SecurityUtilities.HashMD5( email + WebConfiguration.SecretKey );
            string emailOldHash =
                SecurityUtilities.HashMD5( emailOld + WebConfiguration.SecretKey );

            Store store = DataAccessContext.StoreRepository.GetOne( storeID );
            Store storeOld = DataAccessContext.StoreRepository.GetOne( storeIDOld );
            NewsLetter newsLetter = DataAccessContext.NewsLetterRepository.GetOne( email, store );
            if (newsLetter.IsNull)
            {
                NewsLetter newsLetterOld = DataAccessContext.NewsLetterRepository.GetOne( emailOld, storeOld );
                newsLetterOld.EmailHash = emailHash;
                DataAccessContext.NewsLetterRepository.Update( newsLetterOld, email, storeID );
            }
            else
                uxMessage.DisplayError( Resources.NewsletterManager.RegisterAlready );
        }
        else
        {
            uxMessage.DisplayError( Resources.NewsletterManager.RegisterInvalidEmail );
        }
    }

    private void RegisterEmail( string email, string storeID )
    {
        bool isCorrectFormat = checkEmailFormat( email, storeID );
        if (isCorrectFormat)
        {
            string emailHash =
                SecurityUtilities.HashMD5( email + WebConfiguration.SecretKey );

            Store store = DataAccessContext.StoreRepository.GetOne( storeID );
            NewsLetter newsLetter = DataAccessContext.NewsLetterRepository.GetOne( email, store );
            if (newsLetter.IsNull)
            {
                newsLetter.Email = email;
                newsLetter.EmailHash = emailHash;
                newsLetter.JoinDate = DateTime.Now;
                newsLetter.StoreID = storeID;
                DataAccessContext.NewsLetterRepository.Create( newsLetter );
                uxMessage.DisplayMessage( Resources.NewsletterManager.RegisterSuccess );
                ClearField();
            }
            else
                uxMessage.DisplayError( Resources.NewsletterManager.RegisterAlready );
        }
        else
        {
            uxMessage.DisplayError( Resources.NewsletterManager.RegisterInvalidEmail );
        }
    }

    private void CancelGrid()
    {
        uxGridNewsletter.EditIndex = -1;
        EmailOld = "";
        StoreIDOld = "";
        RefreshGrid();
    }

    #endregion

    #region protected

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsMultistoreLicense())
        {
            uxGridNewsletter.Columns[3].Visible = false;
            uxStoreListDiv.Visible = false;
        }
        
        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
        ApplyPermission();
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        uxGridNewsletter.EditIndex = -1;
        RefreshGrid();

        uxAddButton.Visible = false;
        uxAddEmailUserPanel.Visible = true;
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxGridNewsletter.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string email = ((Label) row.FindControl( "uxEmailLabel" )).Text;
                    string storeName = ((Label) row.FindControl( "uxStoreNameLabelItem" )).Text;
                    DataAccessContext.NewsLetterRepository.DeleteEmailNoHash( email, DataAccessContext.StoreRepository.GetOne( DataAccessContext.StoreRepository.GetStoreIDByStoreName( storeName ) ) );
                    deleted = true;
                }
            }

            if (deleted)
                uxMessage.DisplayMessage( Resources.NewsletterManager.DeleteSuccess );
            ClearField();
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if (uxGridNewsletter.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }

        uxStatusHidden.Value = "Deleted";
    }

    protected void uxEditLinkButton_PreRender( object sender, EventArgs e )
    {
        if (!IsAdminModifiable())
        {
            LinkButton linkButton = (LinkButton) sender;
            linkButton.Visible = false;
        }
    }

    protected void uxGrid_DataBound( object sender, EventArgs e )
    {
        // Do not display empty row
        if (IsContainingOnlyEmptyRow())
        {
            uxGridNewsletter.Rows[0].Visible = false;
        }
    }

    private bool IsContainingOnlyEmptyRow()
    {
        if (uxGridNewsletter.Rows.Count == 1)
            return true;
        else
            return false;
    }

    protected void uxNewsLetterSourceSource_Selected( object sender, ObjectDataSourceStatusEventArgs e )
    {
        // Add an empty row if no data returned for this data source
        DataTable table = (DataTable) e.ReturnValue;
        if (table.Rows.Count == 0)
        {
            DataRow row = table.NewRow();
            row["PageID"] = -1;
            row["Path"] = DBNull.Value;

            table.Rows.Add( row );
        }
    }

    protected void uxGridNewsletter_RowEditing( object sender, GridViewEditEventArgs e )
    {
        uxGridNewsletter.EditIndex = e.NewEditIndex;
        RefreshGrid();
        EmailOld = ((TextBox) uxGridNewsletter.Rows[e.NewEditIndex].FindControl( "uxEmailText" )).Text;
        if (KeyUtilities.IsMultistoreLicense())
        {
            StoreIDOld = ((Admin_Components_StoreDropDownList) uxGridNewsletter.Rows[e.NewEditIndex].FindControl( "uxStoreList" )).CurrentSelected;
        }
        else
        {
            StoreIDOld = Store.RegularStoreID;
        }
    }

    protected void uxGridNewsletter_RowCancelingEdit( object sender, GridViewCancelEditEventArgs e )
    {
        CancelGrid();

    }
    protected void uxGridNewsletter_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            string email = ((TextBox) uxGridNewsletter.Rows[e.RowIndex].FindControl( "uxEmailText" )).Text;
            string storeID;
            if (KeyUtilities.IsMultistoreLicense())
            {
                storeID = ((Admin_Components_StoreDropDownList) uxGridNewsletter.Rows[e.RowIndex].FindControl( "uxStoreList" )).CurrentSelected;
            }
            else
            {
                storeID = Store.RegularStoreID;
            }

            RegisterEmailUpdate( email, EmailOld, storeID, StoreIDOld );
            CancelGrid();
        }
        catch (Exception ex)
        {
            if (ex.InnerException is DuplicatedPrimaryKeyException)
                uxMessage.DisplayError( Resources.NewsletterManager.UpdateErrorDuplicated );
            else
                uxMessage.DisplayException( ex );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            CancelGrid();
        }
    }




    protected void uxAddEmailButton_Click( object sender, EventArgs e )
    {
        if (KeyUtilities.IsMultistoreLicense())
        {
            RegisterEmail( uxEmailText.Text, uxStoreList.CurrentSelected );
        }
        else
        {
            RegisterEmail( uxEmailText.Text, Store.RegularStoreID );
        }

        CancelGrid();

        uxAddButton.Visible = true;
        uxAddEmailUserPanel.Visible = false;
    }

    protected override void RefreshGrid()
    {
        int totalItems;
        uxGridNewsletter.DataSource = DataAccessContext.NewsLetterRepository.SearchNewsletter(
             GridHelper.GetFullSortText(),
             uxSearchFilter.SearchFilterObj,
             uxPagingControl.StartIndex,
             uxPagingControl.EndIndex,
             out totalItems );
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGridNewsletter.DataBind();
    }

    protected string GetStoreNameFromStoreID( string storeID )
    {
        Store store = DataAccessContext.StoreRepository.GetOne( storeID );
        return store.StoreName;
    }

    #endregion
}
