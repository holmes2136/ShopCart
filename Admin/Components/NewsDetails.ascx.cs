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
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.Shared.Utilities;
using Vevo.Domain.Stores;

public partial class AdminAdvanced_Components_NewsDetails : AdminAdvancedBaseUserControl
{
    private const string _pathNews = "Images/News/";

    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string CurrentID
    {
        get
        {
            return MainContext.QueryString["NewsID"];
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
        uxTopicText.Text = "";
        uxDescriptionText.Text = "";
        uxNewsMetaTitleText.Text = "";
        uxNewsMetaKeywordText.Text = "";
        uxNewsMetaDescriptionText.Text = "";
        uxFileImageText.Text = "";
        uxIsHotNewsCheck.Checked = false;
        uxIsEnabledCheck.Checked = false;
        uxUpload.ShowControl = false;
        uxNewDateCalendarPopup.SelectedDate = DateTime.Now;
    }

    private void InsertStoreInDropDownList()
    {
        uxStoreDrop.Items.Clear();
        uxStoreDrop.AutoPostBack = false;

        IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "StoreID" );

        for (int index = 0; index < storeList.Count; index++)
        {
            uxStoreDrop.Items.Add( new ListItem( storeList[index].StoreName, storeList[index].StoreID ) );
        }
    }

    private void PopulateControls()
    {
        ClearInputField();
        if (CurrentID != null &&
            int.Parse( CurrentID ) >= 0)
        {
            News news = DataAccessContext.NewsRepository.GetOne( uxLanguageControl.CurrentCulture, CurrentID);
            uxTopicText.Text = news.Topic;

            uxDescriptionText.Text = news.Description;
            uxNewsMetaTitleText.Text = news.MetaTitle;
            uxNewsMetaKeywordText.Text = news.MetaKeyword;
            uxNewsMetaDescriptionText.Text = news.MetaDescription;
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Test)
            {
                uxDateText.Text = news.NewsDate.ToShortDateString();
            }
            uxNewDateCalendarPopup.SelectedDate = news.NewsDate;

            uxFileImageText.Text = news.ImageFile;
            uxIsHotNewsCheck.Checked = news.IsHotNews;
            uxIsEnabledCheck.Checked = news.IsEnabled;
            uxNewDateCalendarPopup.SelectedDate = news.NewsDate;
            uxStoreDrop.SelectedValue = news.StoreID;
        }
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private DateTime GetNewsDate()
    {
        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
        {
            return uxNewDateCalendarPopup.SelectedDate;
        }
        else
        {
            return ConvertUtilities.ToDateTime( uxDateText.Text );
        }
    }

    private News SetUpNews( News news )
    {
        news.Topic = uxTopicText.Text;
        news.Description = uxDescriptionText.Text;
        news.MetaDescription = uxNewsMetaDescriptionText.Text.Trim();
        news.MetaKeyword = uxNewsMetaKeywordText.Text.Trim();
        news.MetaTitle = uxNewsMetaTitleText.Text.Trim();
        news.ImageFile = uxFileImageText.Text;
        news.IsHotNews = uxIsHotNewsCheck.Checked;
        news.IsEnabled = uxIsEnabledCheck.Checked;
        news.NewsDate = GetNewsDate();
        news.StoreID = StoreID;

        return news;
    }

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
            uxDateText.Visible = false;
        }
        else
        {
            uxDateText.Visible = true;
        }

        if (!KeyUtilities.IsMultistoreLicense())
            uxStorePanel.Visible = false;
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
                uxNewsDateLabel.Visible = true;
                uxNewDateCalendarPopup.Visible = true;
                uxAddButton.Visible = true;
                uxUpdateButton.Visible = false;

                if (!MainContext.IsPostBack)
                    uxNewDateCalendarPopup.SelectedDate = DateTime.Now;
            }
            else
            {
                MainContext.RedirectMainControl( "NewsList.ascx", "" );
            }
        }
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                News news = new News( uxLanguageControl.CurrentCulture );
                news = SetUpNews( news );
                news = DataAccessContext.NewsRepository.Save( news );

                uxMessage.DisplayMessage( Resources.NewsMessage.AddSuccess );

                ClearInputField();
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
                News news = DataAccessContext.NewsRepository.GetOne( uxLanguageControl.CurrentCulture, CurrentID);
                news = SetUpNews( news );
                news = DataAccessContext.NewsRepository.Save( news );

                uxMessage.DisplayMessage( Resources.NewsMessage.UpdateSuccess );
                PopulateControls();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }


    protected void uxUploadLinkButton_Click( object sender, EventArgs e )
    {
        uxUpload.ShowControl = true;
    }

}
