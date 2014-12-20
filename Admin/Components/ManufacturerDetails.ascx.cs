using System;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;

public partial class Admin_Components_ManufacturerDetails : AdminAdvancedBaseUserControl
{
    private const string _pathUploadCategory = "Images/Manufacturers/";

    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string CurrentID
    {
        get
        {
            return MainContext.QueryString["ManufacturerID"];
        }
    }

    private void ClearInputFields()
    {
        uxNameText.Text = "";
        uxShortDescriptionText.Text = "";
        uxLongDescriptionText.Text = "";
        uxImageText.Text = "";
        uxMetaKeywordText.Text = "";
        uxMetaDescriptionText.Text = "";
        uxIsEnabledCheck.Checked = true;
        uxImageManufacturerUpload.ShowControl = false;
        uxImgAlternateTextbox.Text = "";
        uxImgTitleTextbox.Text = "";
        uxIsShowNewArrivalCheck.Checked = false;
        uxNewArrivalAmountPanel.Visible = uxIsShowNewArrivalCheck.Checked;
        uxNewArrivalAmountText.Text = "";
    }

    private void PopulateControls()
    {
        if (CurrentID != null &&
             int.Parse( CurrentID ) >= 0)
        {
            Manufacturer manufacturer = DataAccessContext.ManufacturerRepository.GetOne(
                uxLanguageControl.CurrentCulture, CurrentID );
            uxNameText.Text = manufacturer.Name;
            uxShortDescriptionText.Text = manufacturer.Description;
            uxLongDescriptionText.Text = manufacturer.LongDescription;
            uxImageText.Text = manufacturer.ImageFile;
            uxMetaKeywordText.Text = manufacturer.MetaKeyword;
            uxMetaDescriptionText.Text = manufacturer.MetaDescription;
            uxIsEnabledCheck.Checked = manufacturer.IsEnabled;
            uxImgAlternateTextbox.Text = manufacturer.ImageAlt;
            uxImgTitleTextbox.Text = manufacturer.ImageTitle;
            uxIsShowNewArrivalCheck.Checked = manufacturer.IsShowNewArrival;
            uxNewArrivalAmountPanel.Visible = uxIsShowNewArrivalCheck.Checked;
            uxNewArrivalAmountText.Text = manufacturer.NewArrivalAmount.ToString();
        }
    }

    private Manufacturer SetUpManufacturer( Manufacturer manufacturer )
    {
        manufacturer.Name = uxNameText.Text;
        manufacturer.Description = uxShortDescriptionText.Text;
        manufacturer.LongDescription = uxLongDescriptionText.Text;
        manufacturer.ImageFile = uxImageText.Text;
        manufacturer.IsEnabled = uxIsEnabledCheck.Checked;
        manufacturer.MetaKeyword = uxMetaKeywordText.Text;
        manufacturer.MetaDescription = uxMetaDescriptionText.Text;
        manufacturer.ImageAlt = uxImgAlternateTextbox.Text;
        manufacturer.ImageTitle = uxImgTitleTextbox.Text;
        manufacturer.IsShowNewArrival = uxIsShowNewArrivalCheck.Checked;
        manufacturer.NewArrivalAmount = ConvertUtilities.ToInt32( uxNewArrivalAmountText.Text );

        return manufacturer;
    }

    private void AddManufacturer()
    {
        try
        {
            if (Page.IsValid)
            {
                Manufacturer manufacturer = new Manufacturer( uxLanguageControl.CurrentCulture );
                manufacturer = SetUpManufacturer( manufacturer );
                manufacturer = DataAccessContext.ManufacturerRepository.Save( manufacturer );

                uxMessage.DisplayMessage( Resources.ManufacturerMessages.AddSuccess );
                ClearInputFields();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        uxStatusHidden.Value = "Added";

        AdminUtilities.ClearSiteMapCache();
    }

    private void UpdateManufacturer()
    {
        try
        {
            if (Page.IsValid)
            {
                Manufacturer manufacturer = DataAccessContext.ManufacturerRepository.GetOne(
                    uxLanguageControl.CurrentCulture, CurrentID );
                manufacturer = SetUpManufacturer( manufacturer );
                manufacturer = DataAccessContext.ManufacturerRepository.Save( manufacturer );

                uxMessage.DisplayMessage( Resources.CategoryMessages.UpdateSuccess );
                PopulateControls();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        uxStatusHidden.Value = "Updated";

        AdminUtilities.ClearSiteMapCache();
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private void RegisterScript()
    {
        string script = "<script type=\"text/javascript\">" +
            "function ismaxlength(obj){" +
            "var mlength= 255;" +
            "if (obj.value.length > mlength)" +
            "obj.value=obj.value.substring(0,mlength);" +
            "}" +
            "</script>";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "CheckLength", script, false);

        uxShortDescriptionText.Attributes.Add("onkeyup", "return ismaxlength(this)");
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterScript();
        uxLanguageControl.BubbleEvent += new EventHandler( Details_RefreshHandler );
        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Test)
        {
            uxImageText.Visible = true;
        }

        if (!MainContext.IsPostBack)
        {
            uxNewArrivalAmountPanel.Visible = false;

            uxImageManufacturerUpload.PathDestination = _pathUploadCategory;
            uxImageManufacturerUpload.ReturnTextControlClientID = uxImageText.ClientID;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (IsEditMode())
        {
            if (!MainContext.IsPostBack)
            {
                PopulateControls();
            }

            if (IsAdminModifiable())
            {
                uxUpdateButton.Visible = true;
            }
            else
            {
                uxUpdateButton.Visible = false;
                uxImageManufacturerLinkButton.Visible = false;
            }

            uxAddButton.Visible = false;
        }
        else
        {
            if (IsAdminModifiable())
            {
                uxAddButton.Visible = true;
                uxUpdateButton.Visible = false;
            }
            else
            {
                MainContext.RedirectMainControl( "ManufacturerList.ascx" );
            }
        }
    }

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        AddManufacturer();
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        UpdateManufacturer();
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }

    protected void uxImageManufacturerLinkButton_Click( object sender, EventArgs e )
    {
        uxImageManufacturerUpload.ShowControl = true;
    }

    protected void uxIsShowNewArrivalCheck_CheckedChanged( object sender, EventArgs e )
    {
        uxNewArrivalAmountPanel.Visible = uxIsShowNewArrivalCheck.Checked;
    }
}