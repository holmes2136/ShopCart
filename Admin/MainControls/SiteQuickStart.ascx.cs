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
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;

using System.IO;

public partial class AdminAdvanced_MainControls_SiteQuickStart : AdminAdvancedBaseUserControl
{
    private int MaximumOrderID
    {
        get
        {
            return ConvertUtilities.ToInt32( DataAccessContext.OrderRepository.GetMaximumOrderID() );
        }
    }

    private int StartOrderID
    {
        get
        {
            return ConvertUtilities.ToInt32( uxStartOrderNoSetupText.Text );
        }
    }

    private bool UpdatePassword()
    {
        string messageError = string.Empty;
        if (uxPassword.UpdatePassword( out messageError ))
            return true;
        else
        {
            uxMessage.DisplayError( messageError );
            return false;
        }
    }

    private void PopulateTaxConfig()
    {
        uxOrderMaximumEnabledDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "OrderMaximumEnabled" );
        uxOrderMaximumAmountText.Text = DataAccessContext.Configurations.GetValue( "OrderMaximumAmount" ).ToString();
        uxOrderMinimumEnabledDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "OrderMinimumEnabled" );
        uxOrderMinimumAmountText.Text = DataAccessContext.Configurations.GetValue( "OrderMinimumAmount" ).ToString();
    }

    private void PopulateStoreDetail()
    {
        uxSiteEditorDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "HtmlEditor" );
    }

    private void PopulateControls()
    {
        PopulateTaxConfig();
        PopulateStoreDetail();
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private bool VerifyStartOrderID()
    {
        bool result = true;

        if (uxStartOrderNoSetupText.Visible == true)
        {
            if (!String.IsNullOrEmpty( uxStartOrderNoSetupText.Text ) && StartOrderID > MaximumOrderID)
            {
            }
            else if (String.IsNullOrEmpty( uxStartOrderNoSetupText.Text ))
            {
            }
            else
            {
                result = false;
            }
        }

        return result;
    }

    private void ShowStartOrderIDTextBox()
    {
        uxStartOrderNoSetupText.Visible = true;
        uxEditOrderNoSetupButton.Visible = false;
        uxAlertMessageRow.Visible = true;
        uxAlertMessageLabel.Visible = true;
    }

    private void HideStartOrderIDTextBox()
    {
        uxStartOrderNoSetupText.Visible = false;
        uxEditOrderNoSetupButton.Visible = true;
        uxAlertMessageRow.Visible = false;
        uxAlertMessageLabel.Visible = false;
    }

    private void ShowStartOrderIDError()
    {
        uxAlertMessageLabel.Visible = true;
    }

    private void HideStartOrderIDError()
    {
        uxAlertMessageLabel.Visible = false;
    }

    private void UpdateOrderSettings()
    {
        if (!String.IsNullOrEmpty( uxStartOrderNoSetupText.Text ) &&
            uxStartOrderNoSetupText.Visible == true)
        {
            DataAccessContext.OrderRepository.SetStartupOrderID( uxStartOrderNoSetupText.Text.Trim() );
        }
        DataAccessContext.ConfigurationRepository.UpdateValue(
             DataAccessContext.Configurations["OrderMaximumEnabled"],
            uxOrderMaximumEnabledDrop.SelectedValue.ToString() );

        DataAccessContext.ConfigurationRepository.UpdateValue(
             DataAccessContext.Configurations["OrderMaximumAmount"],
            uxOrderMaximumAmountText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
             DataAccessContext.Configurations["OrderMinimumEnabled"],
            uxOrderMinimumEnabledDrop.SelectedValue.ToString() );

        DataAccessContext.ConfigurationRepository.UpdateValue(
             DataAccessContext.Configurations["OrderMinimumAmount"],
            uxOrderMinimumAmountText.Text );
    }

    private void UpdateStoreDetail()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["HtmlEditor"],
            uxSiteEditorDrop.SelectedValue );
    }

    private void UpdateDatabase()
    {
        if (Page.IsValid)
        {
            try
            {
                UpdateOrderSettings();
                UpdateStoreDetail();

                AdminUtilities.LoadSystemConfig();

                uxMessage.DisplayMessage( Resources.SetupMessages.UpdateSuccess );
            }
            catch (Exception ex)
            {
                uxMessage.DisplayException( ex );
            }
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxOrderMaximumAmountTR.Visible = true;
        uxOrderMaximumEnabledTR.Visible = true;
        uxOrderMinimumAmountTR.Visible = true;
        uxOrderMinimumEnabaledTR.Visible = true;
        uxOrderSetupPanel.Visible = true;

        if (IsAdminModifiable())
        {
            if (!MainContext.IsPostBack)
            {
                WebUtilities.TieButton( this.Page, uxPassword.PasswordOldControl, uxUpdateButton );
                WebUtilities.TieButton( this.Page, uxPassword.PasswordNewControl, uxUpdateButton );
                WebUtilities.TieButton( this.Page, uxPassword.PasswordConfirmControl, uxUpdateButton );
            }
        }
        else
        {
            uxUpdateButton.Visible = false;
        }

        if (!MainContext.IsPostBack)
        {
            uxSiteEditorDrop.Items.Clear();
            foreach (string item in Enum.GetNames( typeof( EditorList ) ))
            {
                uxSiteEditorDrop.Items.Add( item );
            }
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateControls();
            uxEditOrderNoSetupButton.Visible = true;
            uxStartOrderNoSetupText.Visible = false;
        }
        if (uxStartOrderNoSetupText.Visible != true)
        {
            uxUpdateConfirmButton.TargetControlID = "uxDummyButton";
            uxConfirmModalPopup.TargetControlID = "uxDummyButton";
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        if (!UpdatePassword())
            return;

        if (VerifyStartOrderID())
        {
            UpdateDatabase();
            HideStartOrderIDTextBox();
            HideStartOrderIDError();
        }
        else
        {
            ShowStartOrderIDError();
        }
    }

    protected void uxEditOrderNoSetupButton_Click( object sender, EventArgs e )
    {
        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
        {
            uxUpdateConfirmButton.TargetControlID = "uxUpdateButton";
            uxConfirmModalPopup.TargetControlID = "uxUpdateButton";
        }

        ShowStartOrderIDTextBox();
    }

    protected void uxClearCacheButton_Click( object sender, EventArgs e )
    {
        AdminUtilities.ClearAllCache();
        uxMessage.DisplayMessage( "Cache data is cleared successfully" );
    }

    protected void uxFilePerMissionTestButton_Click( object sender, EventArgs e )
    {
        uxFolderListLabel.Visible = false;
        uxFolderListLabel.Text = "";

        FolderPermissionTest test = new FolderPermissionTest();

        if (test.TestWrite())
        {
            uxFilePermissionTestMessage.DisplayMessageNoNewLIne( "Test passed successfully" );
        }
        else
        {
            uxFilePermissionTestMessage.DisplayErrorNoNewLine(
                "Test failed : The following folder(s) do not have write permission." );
            uxFolderListLabel.Visible = true;

            uxFolderListLabel.Text = test.ErrorMessage + "<br/>";
        }
    }
}
