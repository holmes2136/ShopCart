using System;
using System.Data;
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
using Vevo.Domain.Base;
using Vevo.Domain.Configurations;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Shared.DataAccess;


public partial class AdminAdvanced_MainControls_SearchConfigurationResult : AdminAdvancedBaseUserControl
{
    #region Private

    private const string _pathUpload = "Images/Configuration/";
    private string[] excludeFreeConfig = { "BundlePromotionShow","BundlePromotionDisplay","BundlePromotionColumn",
                                         "GiftRegistryModuleDisplay","EnableBundlePromo",
                                         "ThubLogEnabled","eBayConfigAppID","eBayConfigCertID","eBayConfigDevID",
                                         "eBayConfigToken","eBayConfigListingMode","AffiliateEnabled",
                                         "AffiliateAutoApprove","AffiliateReference","AffiliateExpirePeriod",
                                         "AffiliateCommissionRate","AffiliateDefaultPaidBalance","PointSystemEnabled",
                                         "RewardPoints","PointValue","WebServiceAdminUser"};

    private bool IsExcludeConfigInFree( string name )
    {
        foreach (string config in excludeFreeConfig)
        {
            if (config == name)
                return true;
        }
        return false;
    }

    private string CurrentSearch
    {
        get
        {
            return MainContext.QueryString["Search"];
        }
    }

    private string CurrentStore
    {
        get
        {
            if (!KeyUtilities.IsMultistoreLicense() )
            {
                return "1";
            }

            return MainContext.QueryString["Store"];
        }
    }

    private string RedirectURL
    {
        get
        {
            return MainContext.QueryString["RedirectURL"];
        }
    }

    private DropDownList CreateDropDownList( Configuration config )
    {
        DropDownList uxDropDownList = ConfigurationControlBuilder.CreateDropDownList( config, CurrentStore );

        IList<Configuration> list
            = DataAccessContext.ConfigurationRepository.GetByEnablerConfigName( config.Name );

        if (config.ChildConfigurations.Count > 0 || list.Count > 0)
        {
            uxDropDownList.SelectedIndexChanged += new EventHandler( DropDownControl_SelectedIndexChanged );
            uxDropDownList.AutoPostBack = true;
        }
        if (config.EnablerConfigName != "")
        {
            if (!Store.CanUseMultiStoreConfig( config.EnablerConfigName ))
            {
                uxDropDownList.Enabled = DataAccessContext.Configurations.GetBoolValue( config.EnablerConfigName );
            }
            else
            {
                uxDropDownList.Enabled = DataAccessContext.Configurations.GetBoolValue(
                    config.EnablerConfigName, DataAccessContext.StoreRepository.GetOne( CurrentStore ) );
            }
        }

        return uxDropDownList;
    }

    private void CreateUploadPanel( Configuration config )
    {
        Panel uploadPanel = new Panel();
        string clientID = "";
        Panel panelControl = (Panel) uxPlaceHolder.FindControl( config.Name + "UploadPanel" );
        if (panelControl != null)
        {
            TextBox textbox = (TextBox) panelControl.FindControl( config.Name );
            if (textbox != null)
                clientID = textbox.ClientID;
        }
        panelControl.Controls.Add( CreateUploadControl( config, clientID ) );
        panelControl.Controls.Add( CreateLinkButton( config ) );
    }

    private AdminAdvanced_Components_Common_Upload CreateUploadControl(
        Configuration config,
        string textBoxClientID )
    {
        AdminAdvanced_Components_Common_Upload controlUpload =
            LoadControl( "../Components/Common/Upload.ascx" ) as AdminAdvanced_Components_Common_Upload;

        controlUpload.ID = config.Name + "Upload";
        controlUpload.PathDestination = _pathUpload;
        controlUpload.ButtonImage = "SelectImages.png";
        controlUpload.ShowControl = false;
        controlUpload.CssClass = "ConfigRow";
        controlUpload.CheckType = UploadFileType.Image;
        //    controlUpload.LeftLabelClass = "Label";
        controlUpload.ButtonWidth = new Unit( 105 );
        controlUpload.ButtonHeight = new Unit( 20 );
        controlUpload.ShowText = false;

        controlUpload.ReturnTextControlClientID = textBoxClientID;

        return controlUpload;
    }

    private LinkButton CreateLinkButton( Vevo.Domain.Configurations.Configuration config )
    {
        LinkButton linkButtonControl = new LinkButton();
        linkButtonControl.ID = config.Name + "LinkButton";
        linkButtonControl.OnClientClick = "UploadButtonClick_Command";
        linkButtonControl.Text = "Upload...";
        linkButtonControl.CssClass = "fl mgl5";
        linkButtonControl.CommandArgument = config.Name;
        linkButtonControl.Command += new CommandEventHandler( UploadButtonClick_Command );

        return linkButtonControl;
    }

    private void UploadButtonClick_Command( object sender, CommandEventArgs e )
    {
        AdminAdvanced_Components_Common_Upload controlUpload
            = uxPlaceHolder.FindControl( e.CommandArgument.ToString() + "Upload" ) as AdminAdvanced_Components_Common_Upload;
        controlUpload.ShowControl = true;

        LinkButton linkButtonControl
            = (LinkButton) uxPlaceHolder.FindControl( e.CommandArgument.ToString() + "LinkButton" );
        linkButtonControl.Visible = false;
    }

    private Panel CreateChildPanel( Configuration parentConfig, Configuration childConfig )
    {
        Panel childPanel = new Panel();
        childPanel.ID = childConfig.Name + "Panel";
        childPanel.CssClass = "ConfigRow";

        AdminAdvanced_Components_Common_HelpIcon helpIcon
            = LoadControl( "../Components/Common/HelpIcon.ascx" ) as AdminAdvanced_Components_Common_HelpIcon;
        helpIcon.ID = childConfig.Name + "Help";
        helpIcon.ConfigName = childConfig.Name;

        childPanel.Controls.Add( helpIcon );
        childPanel.Controls.Add(
            ConfigurationControlBuilder.CreateLabel(
            childConfig.Descriptions[0].DisplayName, "BulletLabel" ) );
        if (ConfigurationControlBuilder.IsSpecialType( childConfig.Name ))
        {
            childPanel.Controls.Add(
                ConfigurationControlBuilder.GetControl( childConfig, CurrentStore ) );
        }
        else
        {
            switch (childConfig.ConfigType)
            {
                case Configuration.ConfigurationType.DropDown:
                    childPanel.Controls.Add( CreateDropDownList( childConfig ) );
                    break;

                case Configuration.ConfigurationType.TextBox:
                    childPanel.Controls.Add(
                        ConfigurationControlBuilder.CreateTextBox( childConfig, CurrentStore ) );
                    childPanel.Controls.Add( ConfigurationControlBuilder.CreateValidatorPanel( childConfig ) );
                    break;
            }
        }
        childPanel.Visible = ConvertUtilities.ToBoolean( parentConfig.Values[0].ItemValue );

        return childPanel;
    }

    private Panel CreateNewConfigPanel( Configuration config )
    {
        Panel panel = new Panel();
        panel.CssClass = "ConfigRow";
        panel.ID = config.Name + "Panel";
        panel.Visible = true;
        return panel;
    }

    private void AddNormalConfigPanel( PlaceHolder placeHolder, Configuration config )
    {
        Panel panel = CreateNewConfigPanel( config );
        AdminAdvanced_Components_Common_HelpIcon helpIcon
            = LoadControl( "../Components/Common/HelpIcon.ascx" ) as AdminAdvanced_Components_Common_HelpIcon;
        helpIcon.ID = config.Name + "Help";
        helpIcon.ConfigName = config.Name;

        panel.Controls.Add( helpIcon );
        panel.Controls.Add( ConfigurationControlBuilder.CreateLabel(
            config.Descriptions[0].DisplayName, "Label" ) );
        switch (config.ConfigType)
        {
            case Configuration.ConfigurationType.DropDown:
                if (!ConfigurationControlBuilder.IsSpecialType( config.Name ))
                    panel.Controls.Add( CreateDropDownList( config ) );
                else
                    panel.Controls.Add( ConfigurationControlBuilder.GetControl( config, CurrentStore ) );
                placeHolder.Controls.Add( panel );
                break;

            case Configuration.ConfigurationType.TextBox:
                panel.Controls.Add( ConfigurationControlBuilder.CreateTextBox( config, CurrentStore ) );
                panel.Controls.Add( ConfigurationControlBuilder.CreateValidatorPanel( config ) );
                placeHolder.Controls.Add( panel );
                break;

            case Configuration.ConfigurationType.RadioButton:
                panel.Controls.Add( ConfigurationControlBuilder.CreateRadioButtonList( config, CurrentStore ) );
                placeHolder.Controls.Add( panel );
                break;

            case Configuration.ConfigurationType.Upload:
                TextBox textBoxUploadControl
                    = ConfigurationControlBuilder.CreateTextBox( config, CurrentStore, 210 );
                panel.Controls.Add( textBoxUploadControl );
                panel.ID = config.Name + "UploadPanel";
                placeHolder.Controls.Add( panel );
                CreateUploadPanel( config );
                break;
        }

        if (config.ChildConfigurations.Count > 0)
        {
            foreach (Configuration childConfig in config.ChildConfigurations)
            {
                placeHolder.Controls.Add( CreateChildPanel( config, childConfig ) );
            }
        }
    }

    private void PopulateControls()
    {
        uxSearchTitle.Text = "Configuration result from keyword :  " + CurrentSearch;
        uxPlaceHolder.Controls.Clear();
        IList<Configuration> list = DataAccessContext.ConfigurationRepository.SearchConfiguration(
            AdminConfig.CurrentCulture,
            "Configuration.ConfigID",
            CurrentSearch,
            StringUtilities.SplitString( ConfigurationDescription.ConfigurationSearchBy, ',' ) );

        if (list.Count > 0)
        {
            int countConfig = 0;

            foreach (Configuration item in list)
            {
                if (IsExcludeConfigInFree( item.Name ) && !KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
                    continue;

                if ((!Store.CanUseMultiStoreConfig( item.Name ) && CurrentStore.Equals( "0" )) ||
                     (Store.CanUseMultiStoreConfig( item.Name ) && !CurrentStore.Equals( "0" )))
                {
                    if (ConfigurationControlBuilder.IsSpecialType( item.Name ))
                    {
                        Panel panel = CreateNewConfigPanel( item );

                        AdminAdvanced_Components_Common_HelpIcon helpIcon
                            = LoadControl( "../Components/Common/HelpIcon.ascx" ) as AdminAdvanced_Components_Common_HelpIcon;
                        helpIcon.ID = item.Name + "Help";
                        helpIcon.ConfigName = item.Name;
                        panel.Controls.Add( helpIcon );
                        panel.Controls.Add( ConfigurationControlBuilder.CreateLabel(
                                                item.Descriptions[0].DisplayName, "Label" ) );
                        panel.Controls.Add( ConfigurationControlBuilder.GetControl( item, CurrentStore ) );
                        uxPlaceHolder.Controls.Add( panel );
                    }
                    else if (item.ConfigType == Configuration.ConfigurationType.UserControl)
                    {
                        Panel panel = CreateNewConfigPanel( item );
                        UserControl control = (UserControl) LoadControl( "../" + item.DisplayUserControl );
                        control.ID = item.Name;

                        //"WidgetAddThisIsEnabled","WidgetLivePersonIsEnabled"
                        //This control need to set some property before populate control
                        if (ConfigurationControlBuilder.IsSpecialUserControl( item.Name ))
                        {
                            Admin_Components_SiteConfig_WidgetDetails widgetDetailsControl
                                = (Admin_Components_SiteConfig_WidgetDetails) control;

                            string name = String.Empty;
                            if (item.Name.Contains( "AddThis" ))
                            {
                                widgetDetailsControl.ParameterName = "AddThis Username";
                                widgetDetailsControl.WidgetStyle = "AddThis";
                            }
                            else if (item.Name.Contains( "LivePerson" ))
                            {
                                widgetDetailsControl.ParameterName = "LivePerson Account";
                                widgetDetailsControl.WidgetStyle = "LivePerson";
                            }
                            else if (item.Name.Contains( "LikeBox" ))
                            {
                                widgetDetailsControl.ParameterName = "Fanpage URL";
                                widgetDetailsControl.WidgetStyle = "LikeBox";
                            }
                        }

                        panel.Controls.Add( control );
                        uxPlaceHolder.Controls.Add( panel );
                        // Need to add dynamic controls after adding parent controls to place holder.
                        // Otherwise, the dynamic controls' ClientID may not be correct 
                        // (e.g. Parent_uxDynmaic instead of uxFront_uxDiv_Parent_uxDynamic).
                        // If this happens, view state for the dynamic control will not be restored correctly.
                        ((IConfigUserControl) control).Populate( item );
                    }
                    else
                    {
                        // Normal configurations (non-special, non-user-control)
                        AddNormalConfigPanel( uxPlaceHolder, item );
                    }

                    countConfig++;
                }
            }

            if (countConfig == 0)
            {
                uxUpdateButton.Visible = false;
            }
        }
        else
        {
            uxUpdateButton.Visible = false;
        }

    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsAdminModifiable())
        {
            uxUpdateButton.Visible = false;
        }
    }

    protected void DropDownControl_SelectedIndexChanged( object sender, EventArgs e )
    {
        DropDownList sourceDropDownList = sender as DropDownList;
        if (sourceDropDownList != null)
        {
            string dropDownName = sourceDropDownList.ID;
            if (DataAccessContext.Configurations[dropDownName].ChildConfigurations.Count > 0)
            {
                foreach (Configuration childConfig
                    in DataAccessContext.Configurations[dropDownName].ChildConfigurations)
                {
                    Control childControl = uxPlaceHolder.FindControl( childConfig.Name + "Panel" );
                    childControl.Visible = ConvertUtilities.ToBoolean( sourceDropDownList.SelectedValue );
                }
            }

            IList<Configuration> list
                = DataAccessContext.ConfigurationRepository.GetByEnablerConfigName( dropDownName );
            if (list != null)
            {
                foreach (Configuration item in list)
                {
                    DropDownList dropdown = (DropDownList) uxPlaceHolder.FindControl( item.Name );
                    if (dropdown != null)
                    {
                        dropdown.SelectedValue = sourceDropDownList.SelectedValue;
                        dropdown.Enabled = ConvertUtilities.ToBoolean( sourceDropDownList.SelectedValue );
                    }
                }
            }
        }
    }

    protected void uxBackButton_Click( object sender, EventArgs e )
    {
        if (RedirectURL.Equals( "SiteConfig.ascx" ))
        {
            MainContext.RedirectMainControl( RedirectURL );
        }
        else if (RedirectURL.Equals( "StoreConfig.ascx" ))
        {
            MainContext.RedirectMainControl( RedirectURL, String.Format( "StoreID={0}", CurrentStore ) );
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        ConfigurationCollection configurations = DataAccessContext.Configurations;
        Store store = DataAccessContext.StoreRepository.GetOne( CurrentStore );
        try
        {
            foreach (Control control in uxPlaceHolder.Controls)
            {
                if (control is Panel)
                {
                    Panel panelControl = (Panel) control;
                    foreach (Control controlItem in panelControl.Controls)
                    {
                        if (controlItem is TextBox)
                        {
                            TextBox textBox = (TextBox) controlItem;
                            if (configurations.ContainsKey( textBox.ID ))
                                DataAccessContext.ConfigurationRepository.UpdateValue(
                                    configurations[textBox.ID], textBox.Text, store );
                        }
                        else if (controlItem is DropDownList)
                        {
                            DropDownList dropdown = (DropDownList) controlItem;

                            if (ConfigurationControlBuilder.IsSpecialType( dropdown.ID ))
                            {
                                ConfigurationControlBuilder.UpdateControlSpecialType( dropdown.ID, dropdown.SelectedValue.ToString() );
                            }
                            else
                            {
                                if (configurations.ContainsKey( dropdown.ID ))
                                    DataAccessContext.ConfigurationRepository.UpdateValue(
                                        configurations[dropdown.ID], dropdown.SelectedValue.ToString(), store );
                            }
                        }
                        else if (controlItem is RadioButtonList)
                        {
                            RadioButtonList radioButton = (RadioButtonList) controlItem;
                            if (configurations.ContainsKey( radioButton.ID ))
                                DataAccessContext.ConfigurationRepository.UpdateValue(
                                    configurations[radioButton.ID], radioButton.SelectedValue.ToString(), store );
                        }
                        else if (controlItem is UserControl)
                        {
                            if (configurations.ContainsKey( controlItem.ID ))
                                ((IConfigUserControl) controlItem).Update();
                        }
                    }
                }
            }

            SystemConfig.Load();
            uxMessage.DisplayMessage( Resources.SetupMessages.UpdateSuccess );
            PopulateControls();
        }
        catch (Exception ex)
        {
            // uxMessage.ClearMessage();
            uxMessage.DisplayException( ex );
        }
    }

    #endregion

}
