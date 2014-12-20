using System;
using System.Data;
//using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Configurations;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;

/// <summary>
/// Summary description for ConfigurationControlBuilder
/// </summary>
/// 
namespace Vevo
{
    public static class ConfigurationControlBuilder
    {
        #region Private

        private static String[] _configurationSpecialTypeList = new String[] { 
            "ShippingTaxClass", "BaseWebsiteCurrency",
            "UrlCultureName", "DefaultDisplayCurrencyCode"
        };

        private static String[] _configurationSpecialUserControlList = new String[] { 
            "WidgetAddThisIsEnabled","WidgetLivePersonIsEnabled","WidgetLikeBoxIsEnabled"
        };

        private static string GetConfigurationValue( Configuration config, string currentStore )
        {
            foreach (ConfigurationValue value in config.Values)
            {
                if (value.StoreID == currentStore)
                {
                    return value.ItemValue;
                }
            }

            return config.Values[0].ItemValue;
        }

        #endregion

        #region Public

        public static TextBox CreateTextBox( Configuration config, string currentStore )
        {
            TextBox uxTextBox = new TextBox();
            uxTextBox.ID = config.Name;
            uxTextBox.CssClass = "TextBox";

            uxTextBox.Text = GetConfigurationValue( config, currentStore );

            return uxTextBox;
        }

        public static TextBox CreateTextBox( Configuration config, string currentStore, int width )
        {
            TextBox uxTextBox = CreateTextBox( config, currentStore );
            uxTextBox.Width = new Unit( width );
            return uxTextBox;
        }

        public static Label CreateLabel( string text, string cssClass )
        {
            Label uxLabel = new Label();
            uxLabel.Text = text;
            uxLabel.CssClass = cssClass;

            return uxLabel;
        }

        public static RequiredFieldValidator CreateRequiredFieldValidator( Configuration config )
        {
            RequiredFieldValidator uxRequiredFieldValidator = new RequiredFieldValidator();
            uxRequiredFieldValidator.ID = config.Name + "Required";
            uxRequiredFieldValidator.ValidationGroup = "SiteConfigValid";
            uxRequiredFieldValidator.Display = ValidatorDisplay.Dynamic;
            uxRequiredFieldValidator.ControlToValidate = config.Name;
            uxRequiredFieldValidator.Text = " <-- ";
            uxRequiredFieldValidator.ErrorMessage = "Required " + config.Descriptions[0].DisplayName;

            return uxRequiredFieldValidator;
        }

        public static CompareValidator CreateCompareValidator(
            Configuration config,
             ValidationDataType type )
        {
            CompareValidator uxCompareValidator = new CompareValidator();
            uxCompareValidator.ID = config.Name + "Compare";
            uxCompareValidator.Operator = ValidationCompareOperator.DataTypeCheck;
            uxCompareValidator.Type = type;
            uxCompareValidator.ControlToValidate = config.Name;
            uxCompareValidator.ValidationGroup = "SiteConfigValid";
            uxCompareValidator.Text = "*";
            uxCompareValidator.ErrorMessage = config.Descriptions[0].DisplayName + " is invalid";
            return uxCompareValidator;
        }

        public static CompareValidator CreateCompareWithZeroValidator( Configuration config )
        {
            CompareValidator uxCompareValidator = new CompareValidator();
            uxCompareValidator.ID = config.Name + "CompareWithZero";
            uxCompareValidator.Operator = ValidationCompareOperator.GreaterThan;
            uxCompareValidator.ValueToCompare = "0";
            uxCompareValidator.ControlToValidate = config.Name;
            uxCompareValidator.ValidationGroup = "SiteConfigValid";
            uxCompareValidator.Text = "*";
            uxCompareValidator.ErrorMessage = config.Descriptions[0].DisplayName + " must greater than 0";

            return uxCompareValidator;
        }

        public static RangeValidator CreateRangeValidator( Configuration config, string minimum, string maximum,
            ValidationDataType type )
        {
            RangeValidator uxRangeValidator = new RangeValidator();
            uxRangeValidator.ControlToValidate = config.Name;
            uxRangeValidator.Display = ValidatorDisplay.None;
            uxRangeValidator.Type = type;
            uxRangeValidator.MaximumValue = maximum;
            uxRangeValidator.MinimumValue = minimum;
            uxRangeValidator.ValidationGroup = "SiteConfigValid";
            uxRangeValidator.ErrorMessage = config.Descriptions[0].DisplayName + " must be between " +
                minimum + " to " + maximum;
            return uxRangeValidator;
        }

        public static RegularExpressionValidator CreateRegularExpressionValidator( Configuration config )
        {
            RegularExpressionValidator uxRegularExpressionValidator = new RegularExpressionValidator();

            uxRegularExpressionValidator.ValidationExpression = @"\d{1,3}(,\d{1,3})*";
            uxRegularExpressionValidator.ControlToValidate = config.Name;
            uxRegularExpressionValidator.Display = ValidatorDisplay.Dynamic;
            uxRegularExpressionValidator.ValidationGroup = "SiteConfigValid";
            uxRegularExpressionValidator.ErrorMessage = "wrong expression";
            return uxRegularExpressionValidator;
        }

        public static Panel CreateValidatorPanel( Configuration config )
        {
            Panel uxPanel = new Panel();
            uxPanel.CssClass = "validator1 fl";
            return uxPanel = GetValidatorPanel( (config), uxPanel );
        }

        public static RadioButtonList CreateRadioButtonList( Configuration config, string currentStore )
        {
            string[] displayName = config.SelectionNames.Split( '|' );
            string[] value = config.SelectionValues.Split( '|' );
            string[] imagePath = config.SelectionImages.Split( '|' );

            RadioButtonList radioButtonControl = new RadioButtonList();
            radioButtonControl.ID = config.Name;
            radioButtonControl.CssClass = "fl DropDown RadioProductStyle";
            if (displayName.Length == imagePath.Length)
                for (int i = 0; i < displayName.Length; i++)
                    radioButtonControl.Items.Add(
                        new ListItem( displayName[i] + "<img src='" + imagePath[i] + "'  alt='' />", value[i] ) );
            else
                for (int i = 0; i < displayName.Length; i++)
                    radioButtonControl.Items.Add( new ListItem( displayName[i], value[i] ) );

            radioButtonControl.SelectedValue = GetConfigurationValue( config, currentStore );

            return radioButtonControl;
        }

        public static bool IsSpecialType( string configName )
        {
            foreach (string name in _configurationSpecialTypeList)
            {
                if (name == configName)
                    return true;
            }
            return false;
        }

        public static bool IsSpecialUserControl( string configName )
        {
            foreach (string name in _configurationSpecialUserControlList)
            {
                if (name == configName)
                    return true;
            }
            return false;
        }

        public static DropDownList GetControl( Configuration config, string currentStore )
        {
            DropDownList dropDownList = new DropDownList();
            dropDownList.Items.Clear();
            dropDownList.ID = config.Name;
            dropDownList.CssClass = "fl DropDown";

            if (config.Name == "ShippingTaxClass")
            {
                dropDownList.DataSource = DataAccessContext.TaxClassRepository.GetAll( "TaxClassID" );
                dropDownList.DataTextField = "TaxClassName";
                dropDownList.DataValueField = "TaxClassID";
                dropDownList.DataBind();
                dropDownList.Items.Insert( 0, new ListItem( "None", "0" ) );
                dropDownList.SelectedValue = GetConfigurationValue( config, currentStore );
            }
            else if (config.Name == "UrlCultureName")
            {
                dropDownList.DataSource = DataAccessContext.CultureRepository.GetAll();
                dropDownList.DataTextField = "DisplayName";
                dropDownList.DataValueField = "Name";
                dropDownList.DataBind();
                dropDownList.SelectedValue = GetConfigurationValue( config, currentStore );
            }
            else if (config.Name == "BaseWebsiteCurrency")
            {
                CurrencyUtilities.BaseCurrencyCode = DataAccessContext.Configurations.GetValue(
                    "BaseWebsiteCurrency" );
                dropDownList.DataSource = DataAccessContext.CurrencyRepository.GetByEnabled( BoolFilter.ShowTrue );
                dropDownList.DataTextField = "Name";
                dropDownList.DataValueField = "CurrencyCode";
                dropDownList.DataBind();
                dropDownList.SelectedValue = CurrencyUtilities.BaseCurrencyCode;
            }
            else if (config.Name == "DefaultDisplayCurrencyCode")
            {
                string currencyCode = DataAccessContext.Configurations.GetValue(
                    "DefaultDisplayCurrencyCode", StoreContext.CurrentStore );
                dropDownList.DataSource = DataAccessContext.CurrencyRepository.GetByEnabled( BoolFilter.ShowTrue );
                dropDownList.DataTextField = "Name";
                dropDownList.DataValueField = "CurrencyCode";
                dropDownList.DataBind();
                dropDownList.SelectedValue = currencyCode;
            }

            return dropDownList;
        }

        public static void UpdateControlSpecialType( string configName, string value )
        {
            if (configName == "ShippingTaxClass")
            {
                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["ShippingTaxClass"], value );

            }
            else if (configName == "UrlCultureName")
            {
                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["UrlCultureName"], value );
            }
            else if (configName == "BaseWebsiteCurrency")
            {
                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["BaseWebsiteCurrency"], value );

                CurrencyUtilities.BaseCurrencyCode = DataAccessContext.Configurations.GetValue( "BaseWebsiteCurrency" );
                Currency currency = DataAccessContext.CurrencyRepository.GetOne( value );
                currency.ConversionRate = 1;
                DataAccessContext.CurrencyRepository.Save( currency, value );
            }
            else if (configName == "DefaultDisplayCurrencyCode")
            {
                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["DefaultDisplayCurrencyCode"], value, StoreContext.CurrentStore );
            }
        }

        public static DropDownList CreateDropDownList( Configuration config, string currentStore )
        {
            DropDownList uxDropDownList = new DropDownList();
            string[] displayName = config.SelectionNames.Split( '|' );
            string[] value = config.SelectionValues.Split( '|' );

            uxDropDownList.ID = config.Name;
            uxDropDownList.CssClass = "fl DropDown";

            for (int i = 0; i < displayName.Length; i++)
                uxDropDownList.Items.Add( new ListItem( displayName[i], value[i] ) );

            uxDropDownList.SelectedValue = GetConfigurationValue( config, currentStore );

            return uxDropDownList;
        }

        public static Panel GetValidatorPanel( Configuration config, Panel uxPanel )
        {
            //  string fieldValidation = config.FieldValidation;
            string[] validateList = config.FieldValidation.Split( '|' );

            for (int i = 0; i < validateList.Length; i++)
            {
                if (validateList[i] == "Required")
                {
                    uxPanel.Controls.Add( CreateLabel( " * ", "Asterisk" ) );
                    uxPanel.Controls.Add( CreateRequiredFieldValidator( config ) );
                }

                if (validateList[i] == "Expression")
                {
                    uxPanel.Controls.Add( CreateRegularExpressionValidator( config ) );
                }

                if (validateList[i].StartsWith( "Integer" ))
                {
                    uxPanel.Controls.Add( CreateCompareValidator( config, ValidationDataType.Integer ) );
                    uxPanel.Controls.Add( CreateCompareWithZeroValidator( config ) );
                    string validateRange = validateList[i].Substring( 7 );
                    string[] range = validateRange.Split( ',' );
                    if (range.Length > 1)
                    {
                        uxPanel.Controls.Add( CreateRangeValidator( config, range[0], range[1], ValidationDataType.Integer ) );
                    }
                }

                if (validateList[i].StartsWith( "Double" ))
                {
                    uxPanel.Controls.Add( CreateCompareValidator( config, ValidationDataType.Double ) );
                    uxPanel.Controls.Add( CreateCompareWithZeroValidator( config ) );
                    string validateRange = validateList[i].Substring( 6 );
                    string[] range = validateRange.Split( ',' );
                    if (range.Length > 1)
                    {
                        uxPanel.Controls.Add( CreateRangeValidator( config, range[0], range[1], ValidationDataType.Double ) );
                    }
                }
            }

            return uxPanel;
        }

        #endregion
    }
}
