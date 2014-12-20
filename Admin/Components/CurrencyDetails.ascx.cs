using System;
using Vevo;
using Vevo.Domain;

public partial class AdminAdvanced_Components_CurrencyDetails : AdminAdvancedBaseUserControl
{
    private enum Mode { Add, Edit };
    private Mode _mode = Mode.Add;

    private string CurrentCurrencyCode
    {
        get
        {
            if (ViewState["CurrentCurrencyCode"] == null ||
                ViewState["CurrentCurrencyCode"].ToString() == String.Empty)
            {
                return MainContext.QueryString["CurrencyCode"];
            }
            else
            {
                return ViewState["CurrentCurrencyCode"].ToString();
            }
        }
        set
        {
            ViewState["CurrentCurrencyCode"] = value;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            if (IsEditMode())
            {
                PopulateControl();
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
            }
            if (CurrentCurrencyCode == DataAccessContext.Configurations.GetValue( "BaseWebsiteCurrency" ))
                uxEditWarningDiv.Visible = true;
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
                MainContext.RedirectMainControl( "CurrencyList.ascx", "" );
            }
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
        uxCurrencyCodeText.Text = string.Empty;
        uxSymbolText.Text = string.Empty;
        uxNameText.Text = string.Empty;
        uxConversionText.Text = string.Empty;
        uxIsEnabledCheck.Checked = false;
        uxCurrencyPositionDrop.SelectedIndex = 0;
    }

    private void PopulateControl()
    {
        ClearInputField();

        Currency currency = DataAccessContext.CurrencyRepository.GetOne( CurrentCurrencyCode );
        uxCurrencyCodeText.Text = currency.CurrencyCode;
        uxSymbolText.Text = currency.CurrencySymbol;
        uxConversionText.Text = currency.ConversionRate.ToString();
        uxNameText.Text = currency.Name;
        uxIsEnabledCheck.Checked = currency.Enabled;

        uxCurrencyPositionDrop.SelectedValue = currency.CurrencyPosition;

        if (CurrentCurrencyCode == DataAccessContext.Configurations.GetValue( "BaseWebsiteCurrency" ))
        {
            uxConversionText.ReadOnly = true;
            uxIsEnabledCheck.Enabled = false;
        }
        else
        {
            uxConversionText.ReadOnly = false;
            uxIsEnabledCheck.Enabled = true;
        }
    }

    private Currency SetUpCurrency( Currency currency )
    {
        currency.CurrencySymbol = uxSymbolText.Text.Trim();
        currency.Name = uxNameText.Text.Trim();
        currency.ConversionRate = Convert.ToDouble( uxConversionText.Text.Trim() );
        currency.Enabled = uxIsEnabledCheck.Checked;
        currency.CurrencyPosition = uxCurrencyPositionDrop.SelectedItem.Value;

        return currency;
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        try
        {
            Currency currency = new Currency();
            currency = SetUpCurrency( currency );
            DataAccessContext.CurrencyRepository.Save( currency, uxCurrencyCodeText.Text.Trim() );

            ClearInputField();

            uxMessage.DisplayMessage( Resources.CurrencyMessages.AddSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        if (DataAccessContext.Configurations.GetValue( "BaseWebsiteCurrency" ) == CurrentCurrencyCode)
        {
            if (uxIsEnabledCheck.Checked == false)
            {
                uxMessage.DisplayError( Resources.CurrencyMessages.UpdateDefaultError );
                return;
            }
        }

        try
        {
            Currency currency = DataAccessContext.CurrencyRepository.GetOne( CurrentCurrencyCode );
            currency = SetUpCurrency( currency );
            DataAccessContext.CurrencyRepository.Save( currency, uxCurrencyCodeText.Text.Trim() );

            if (DataAccessContext.Configurations.GetValue( "BaseWebsiteCurrency" ) == CurrentCurrencyCode &&
                uxCurrencyCodeText.Text != CurrentCurrencyCode)
            {
                DataAccessContext.ConfigurationRepository.UpdateValue( DataAccessContext.Configurations["BaseWebsiteCurrency"], uxCurrencyCodeText.Text );
                AdminUtilities.LoadSystemConfig();
            }

            AdminUtilities.ClearCurrencyCache();

            uxMessage.DisplayMessage( Resources.CurrencyMessages.UpdateSuccess );
            CurrentCurrencyCode = uxCurrencyCodeText.Text.Trim();

            PopulateControl();
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }
}
