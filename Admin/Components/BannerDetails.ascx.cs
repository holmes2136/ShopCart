using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;

public partial class Admin_Components_BannerDetails : AdminAdvancedBaseUserControl
{
    #region Private

    private const string _pathNews = "Images/Banner/";
    private enum Mode { Add, Edit };
    private Mode _mode = Mode.Add;

    private string CurrentID
    {
        get
        {
            return MainContext.QueryString["BannerID"];
        }
    }

    private string StoreID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
            {
                return uxStoreDrop.SelectedValue;
            }
            else
            {
                return Store.RegularStoreID;
            }
        }
    }

    private void ClearInputField()
    {
        uxNameText.Text = "";
        uxFileImageText.Text = "";
        uxUpload.ShowControl = false;
        uxDescriptionText.Text = "";
        uxLinkURLText.Text = "";
        uxCreateDateCalendarPopup.SelectedDate = DateTime.Now;
        uxEndDateCalendarPopup.SelectedDate = DateTime.Now.AddYears(1);
        uxIsEnabledCheck.Checked = true;
    }

    private void InsertStoreInDropDownList()
    {
        uxStoreDrop.Items.Clear();

        IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "StoreID" );

        foreach (Store store in storeList)
        {
            uxStoreDrop.Items.Add( new ListItem( store.StoreName, store.StoreID ) );
        }
    }

    private void PopulateControls()
    {
        ClearInputField();

        if (CurrentID != null && int.Parse( CurrentID ) >= 0)
        {
            Banner banner = DataAccessContext.BannerRepository.GetOneByID( uxLanguageControl.CurrentCulture, CurrentID );

            uxStoreDrop.SelectedValue = banner.StoreID;
            uxNameText.Text = banner.Name;
            uxFileImageText.Text = banner.ImageURL;
            uxDescriptionText.Text = banner.Description;
            uxLinkURLText.Text = banner.LinkURL;
            uxCreateDateCalendarPopup.SelectedDate = banner.CreateDate;
            uxEndDateCalendarPopup.SelectedDate = banner.EndDate;
            uxIsEnabledCheck.Checked = banner.IsEnabled;

            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Test)
            {
                uxCreateDateText.Text = banner.CreateDate.ToShortDateString();
                uxEndDateText.Text = banner.EndDate.ToShortDateString();
            }
        }
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private DateTime GetCreateDate()
    {
        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
        {
            return uxCreateDateCalendarPopup.SelectedDate;
        }
        else
        {
            return ConvertUtilities.ToDateTime( uxCreateDateText.Text );
        }
    }

    private DateTime GetEndDate()
    {
        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
        {
            return uxEndDateCalendarPopup.SelectedDate;
        }
        else
        {
            return ConvertUtilities.ToDateTime( uxEndDateText.Text );
        }
    }

    private Banner SetUpBanner( Banner banner )
    {
        banner.StoreID = StoreID;
        banner.Name = uxNameText.Text;
        banner.ImageURL = uxFileImageText.Text;
        banner.Description = uxDescriptionText.Text;
        banner.LinkURL = uxLinkURLText.Text;
        banner.CreateDate = GetCreateDate();
        banner.EndDate = GetEndDate();
        banner.IsEnabled = uxIsEnabledCheck.Checked;

        return banner;
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        uxLanguageControl.BubbleEvent += new EventHandler( Details_RefreshHandler );

        if (!MainContext.IsPostBack)
        {
            uxUpload.PathDestination = _pathNews;
            uxUpload.ReturnTextControlClientID = uxFileImageText.ClientID;
            InsertStoreInDropDownList();
        }

        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
        {
            uxCreateDateText.Visible = false;
            uxEndDateText.Visible = false;
        }
        else
        {
            uxCreateDateText.Visible = true;
            uxEndDateText.Visible = true;
        }

        if (KeyUtilities.IsMultistoreLicense())
        {
            uxStoreDrop.Enabled = true;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (IsEditMode())
        {
            if (!MainContext.IsPostBack)
            {
                PopulateControls();
                if (IsAdminModifiable())
                {
                    uxUpdateButton.Visible = true;
                }
                else
                {
                    uxUpdateButton.Visible = false;
                    uxUploadLinkButton.Visible = false;
                }

                uxAddButton.Visible = false;
            }
        }
        else
        {
            if (IsAdminModifiable())
            {
                uxCreateDateLabel.Visible = true;
                uxCreateDateCalendarPopup.Visible = true;
                uxEndDateLabel.Visible = true;
                uxEndDateCalendarPopup.Visible = true;

                uxAddButton.Visible = true;
                uxUpdateButton.Visible = false;

                if (!MainContext.IsPostBack)
                {
                    uxCreateDateCalendarPopup.SelectedDate = DateTime.Now;
                    uxEndDateCalendarPopup.SelectedDate = DateTime.Now.AddYears(1);
                }
            }
            else
            {
                MainContext.RedirectMainControl( "BannerList.ascx", "" );
            }
        }
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                Banner banner = new Banner( uxLanguageControl.CurrentCulture );
                banner = SetUpBanner( banner );
                banner = DataAccessContext.BannerRepository.Save( banner );
                ClearInputField();
                uxMessage.DisplayMessage( Resources.BannerMessages.AddSuccess );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                Banner banner = DataAccessContext.BannerRepository.GetOneByID( uxLanguageControl.CurrentCulture, CurrentID );
                banner = SetUpBanner( banner );
                banner = DataAccessContext.BannerRepository.Save( banner );

                uxMessage.DisplayMessage( Resources.BannerMessages.UpdateSuccess );
                PopulateControls();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxUploadLinkButton_Click( object sender, EventArgs e )
    {
        uxUpload.ShowControl = true;
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
