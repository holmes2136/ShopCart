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
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.WebAppLib;
using Vevo.WebUI.International;
using Vevo.Domain.EmailTemplates;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using System.Collections.Generic;
using Vevo.Domain.Base;

public partial class AdminAdvanced_Components_CultureDetails : AdminAdvancedBaseUserControl
{
    private enum Mode { Add, Edit };
    private Mode _mode = Mode.Add;

    private string CurrentCultureID
    {
        get
        {
            return MainContext.QueryString["CultureID"];
        }
    }

    private string OriginalCultureName
    {
        get
        {
            if (ViewState["OriginalCultureName"] == null)
                return String.Empty;
            else
                return ViewState["OriginalCultureName"].ToString().Trim();
        }
        set
        {
            ViewState["OriginalCultureName"] = value;
        }
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        string script =
            "cultureName = document.getElementById('" + uxDisplayNameText.ClientID + "');" +
            "fullName = document.getElementById('" + uxFullNameLabel.ClientID + "');" +
            "fullName.innerHTML = this.value;" +
            "var display = this.value;" +
            "var index = display.indexOf( ' -' );" +
            "if (index != -1) { display = display.substr( 0, index ); }" +
            "index = display.indexOf( ' (' );" +
            "if (index != -1) { display = display.substr( 0, index ); }" +
            "cultureName.value = display;";

        uxNameDrop.Attributes.Add( "onchange", script );

        if (!MainContext.IsPostBack)
        {
            if (IsEditMode())
            {
                PopulateControls();
            }
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (IsEditMode())
        {
            if (!MainContext.IsPostBack)
            {
                if (IsAdminModifiable())
                {
                    uxUpdateButton.Visible = true;
                }
                else
                {
                    uxUpdateButton.Visible = false;
                }

                uxAddButton.Visible = false;
                uxDisplayNameBaseDrop.Visible = false;
                lcDisplayNameBase.Visible = false;
            }

        }
        else
        {
            if (IsAdminModifiable())
            {
                PopulateDropDown();
                uxAddButton.Visible = true;
            }
            else
            {
                uxAddButton.Visible = false;
            }

            uxUpdateButton.Visible = false;
            uxDisplayNameBaseDrop.Visible = true;
            lcDisplayNameBase.Visible = true;
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

    private void ClearInputField()
    {
        uxDisplayNameText.Text = "";
        uxIsEnableddCheck.Checked = true;
    }

    private void PopulateDropDown()
    {
        uxNameDrop.DataSource = CultureUtilities.GetCultureList();
        uxNameDrop.DataTextField = "CultureName";
        uxNameDrop.DataValueField = "Language-Region";
        uxNameDrop.DataBind();
        uxNameDrop.Items.Insert( 0, new ListItem( "Select Culture", "" ) );
        uxNameDrop.SelectedIndex = 0;

        uxDisplayNameBaseDrop.DataSource = DataAccessContext.CultureRepository.GetAll();
        uxDisplayNameBaseDrop.DataTextField = "DisplayName";
        uxDisplayNameBaseDrop.DataValueField = "CultureID";
        uxDisplayNameBaseDrop.DataBind();
    }

    private void PopulateControls()
    {
        ClearInputField();
        PopulateDropDown();
        Culture culture = DataAccessContext.CultureRepository.GetOne( CurrentCultureID );
        if (!String.IsNullOrEmpty( culture.Name ))
        {
            ListItem selected = uxNameDrop.Items.FindByText( culture.Name );
            uxNameDrop.SelectedValue = selected.Value;
            uxFullNameLabel.Text = selected.Value;
        }
        uxDisplayNameText.Text = culture.DisplayName;
        uxIsEnableddCheck.Checked = culture.Enabled;

        OriginalCultureName = culture.Name;
    }

    private Culture SetupCulture( Culture culture )
    {
        culture.Name = uxNameDrop.SelectedItem.Text;
        culture.DisplayName = uxDisplayNameText.Text.Trim();
        culture.Enabled = uxIsEnableddCheck.Checked;
        return culture;
    }

    private void SetUpCultureInformation( Culture culture )
    {
        SetupEmailTemplate( culture );
    }

    private void SetupEmailTemplate( Culture culture )
    {
        IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "StoreID" );

        foreach (Store store in storeList)
        {
            string cultureID = DataAccessContext.CultureRepository.GetIDByName(
                DataAccessContext.Configurations.GetValue( "DefaultWebsiteLanguage", store ) );
            Culture storeCulture = DataAccessContext.CultureRepository.GetOne( cultureID );

            IList<EmailTemplateDetail> emailList = DataAccessContext.EmailTemplateDetailRepository.GetAll(
                storeCulture, "EmailTemplateDetailID", store.StoreID );

            foreach (EmailTemplateDetail email in emailList)
            {
                IList<ILocale> emailLocaleList =
                    DataAccessContext.EmailTemplateDetailRepository.GetLocales( email.EmailTemplateDetailID );

                foreach (ILocale locale in emailLocaleList)
                {
                    EmailTemplateDetailLocale emailLocale = (EmailTemplateDetailLocale) locale;
                    email.Locales.Add( emailLocale );
                }

                EmailTemplateDetailLocale newEmailLocale = new EmailTemplateDetailLocale();
                newEmailLocale.CultureID = culture.CultureID;
                newEmailLocale.EmailTemplateDetailID = "0";
                newEmailLocale.Body = email.Locales[cultureID].Body;
                newEmailLocale.Subject = email.Locales[cultureID].Subject;

                email.Locales.Add( newEmailLocale );

                DataAccessContext.EmailTemplateDetailRepository.Save( email );
            }
        }
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                Culture culture = new Culture();
                culture = SetupCulture( culture );
                culture = DataAccessContext.CultureRepository.Save( culture );

                LanguageTextAccess.CreateByCultureID( culture.CultureID, uxDisplayNameBaseDrop.SelectedValue );

                ClearInputField();

                SetUpCultureInformation( culture );

                uxMessage.DisplayMessage( Resources.CultureMessages.AddSuccess );

                AdminUtilities.ClearLanguageCache();
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
                Culture culture = DataAccessContext.CultureRepository.GetOne( CurrentCultureID );
                culture = SetupCulture( culture );
                culture = DataAccessContext.CultureRepository.Save( culture );

                CultureConfigs cultureConfigs = new CultureConfigs();
                cultureConfigs.RenameCultureName( OriginalCultureName, uxNameDrop.SelectedItem.Text );

                uxMessage.DisplayMessage( Resources.CultureMessages.UpdateSuccess );

                AdminUtilities.ClearLanguageCache();

                PopulateControls();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }
}
