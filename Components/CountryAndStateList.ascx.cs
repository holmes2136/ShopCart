using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.Domain.Users;
using Vevo.WebUI;

public partial class Components_CountryAndStateList : Vevo.WebUI.International.BaseLanguageUserControl
{
    private string _currentCountry;
    private string _currentState;
    private const string _otherValue = "OT";
    private bool _isRequireCountry = false;
    private bool _isRequireState = false;
    private bool _isCountryWithOther = false;
    private bool _isStateWithOther = false;
    private string _cssPanel;
    private string _cssLabel;
    private string _cssInputText;
    private string _cssInputDrop;
    private string _cssValidate;

    private HiddenField _countryHidden = new HiddenField();
    private HiddenField _stateHidden = new HiddenField();

    private string _countryHiddenID = "uxCountryHidden";
    private string _stateHiddenID = "uxStateHidden";

    private string GetCountryFormat( string code, string name )
    {
        return String.Format( "{0}:{1}|", code, name.Replace( "'", "\\'" ) );
    }

    private string GenerateCountryList()
    {
        IList<Country> countryList = DataAccessContext.CountryRepository.GetAll( BoolFilter.ShowTrue, "CommonName" );

        string result = String.Empty;

        for (int index = 0; index < countryList.Count; index++)
        {
            result += GetCountryFormat( countryList[index].CountryCode, countryList[index].CommonName );
        }

        return result;
    }

    private string GetStateFormat( string countryCode, string stateCode, string stateName )
    {
        return String.Format( "{0}:{1}:{2}|", countryCode, stateCode, stateName.Replace( "'", "\\'" ) );
    }

    private string GenerateStateList()
    {
        string result = String.Empty;
        IList<Country> countryList = DataAccessContext.CountryRepository.GetAll( BoolFilter.ShowTrue, "CommonName" );

        for (int index = 0; index < countryList.Count; index++)
        {
            IList<State> stateList = DataAccessContext.StateRepository.GetAllByCountryCode(
                countryList[index].CountryCode, "StateName", BoolFilter.ShowTrue );

            for (int stateIndex = 0; stateIndex < stateList.Count; stateIndex++)
            {
                result += GetStateFormat( countryList[index].CountryCode, stateList[stateIndex].StateCode, stateList[stateIndex].StateName );
            }
        }
        return result;
    }

    private void RegisterJavascriptCountryState()
    {
        string selectCountryText = GetLanguageText( "SelectCountry" );
        string selectStateText = GetLanguageText( "SelectState" );
        string selectOtherText = GetLanguageText( "SelectOther" );

        string script = "<script type=\"text/javascript\">" +
            "var defaultValue = document.getElementById( '" + CountryHidden.ClientID + "' );" +
            "populateCountry('" + CurrentCountry + "', '" + GenerateCountryList() + "', '" +
            uxCountryDrop.ClientID + "', '" + uxCountryText.ClientID + "', '" + selectCountryText +
            "', '" + selectOtherText + "' );" +
            "populateState('" + CurrentState + "', '" + GenerateStateList() + "', '" + uxStateDrop.ClientID + "', '" +
            uxStateText.ClientID + "', '" + CurrentCountry + "', '" + selectStateText + "', '" + selectOtherText + "', '" + uxValidationStatePanel.ClientID + "' );" +
            "</script>";
        ScriptManager.RegisterStartupScript( Page, GetType(), this.ClientID + "_CountryList", script, false );

        script = "var countryText = document.getElementById( '" + uxCountryText.ClientID + "' );" +
            "var countryHidden = document.getElementById( '" + CountryHidden.ClientID + "' );" +
            "if(this.value == 'OT'){countryText.style.display = '';countryHidden.value = '';countryText.value = '';}" +
            "else{countryText.style.display = 'none';countryHidden.value = this.value;}" +
            "populateState('" + CurrentState + "', '" + GenerateStateList() + "', '" + uxStateDrop.ClientID + "', '" +
            uxStateText.ClientID + "', this.value, '" + selectStateText + "', '" + selectOtherText + "', '" + uxValidationStatePanel.ClientID + "' );";

        uxCountryDrop.Attributes.Add( "onchange", script );

        script = "var stateText = document.getElementById('" + uxStateText.ClientID + "');" +
            "var stateHidden = document.getElementById('" + StateHidden.ClientID + "');" +
            "if(this.value == 'OT'){stateText.style.display = '';stateHidden.value = '';stateText.value = '';}" +
            "else{stateText.style.display = 'none';stateHidden.value = this.value;}";

        uxStateDrop.Attributes.Add( "onchange", script );

        script = "var countryHidden = document.getElementById( '" + CountryHidden.ClientID + "' );" +
            "countryHidden.value = this.value;";
        uxCountryText.Attributes.Add( "onchange", script );
        uxCountryText.Attributes.Add( "onBlur", script );

        script = "var stateHidden = document.getElementById('" + StateHidden.ClientID + "');" +
            "stateHidden.value = this.value;";
        uxStateText.Attributes.Add( "onchange", script );
        uxStateText.Attributes.Add( "onBlur", script );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        _countryHidden.ID = _countryHiddenID;
        uxContryStatePanel.Controls.Add( _countryHidden );

        _stateHidden.ID = _stateHiddenID;
        uxContryStatePanel.Controls.Add( _stateHidden );

        if (IsRequiredCountry)
            uxValidationCountryPanel.Style["display"] = "block";
        else
            uxValidationCountryPanel.Style["display"] = "none";

        if (IsRequiredState)
            uxStatesAsteriskDiv.Style["display"] = "block";
        else
            uxStatesAsteriskDiv.Style["display"] = "none";

        if (!String.IsNullOrEmpty( CssPanel ))
        {
            uxContryStatePanel.CssClass = CssPanel;
        }

        if (!String.IsNullOrEmpty( CssLabel ))
        {
            uxCountryLabel.CssClass = CssLabel;
            uxStateLabel.CssClass = CssLabel;
        }

        if (!String.IsNullOrEmpty( CssInputText ))
        {
            uxCountryText.CssClass = CssInputText;
            uxStateText.CssClass = CssInputText;
        }

        if (!String.IsNullOrEmpty( CssInputDrop ))
        {
            uxCountryDrop.CssClass = CssInputDrop;
            uxStateDrop.CssClass = CssInputDrop;
        }

        if (!String.IsNullOrEmpty( CssValidate ))
        {
            uxValidationCountryPanel.CssClass = CssValidate;
            uxValidationStatePanel.CssClass = CssValidate;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (String.IsNullOrEmpty( _currentCountry ) && !Page.IsPostBack)
        {
            CurrentCountry = DataAccessContext.Configurations.GetValue( "StoreDefaultCountry", StoreContext.CurrentStore ).ToString();
        }
        RegisterJavascriptCountryState();
    }

    private bool CheckItemIfExist( ArrayList arrayList, string item )
    {
        foreach (string items in arrayList)
        {
            if (items == item)
                return true;
        }
        return false;
    }

    protected override void Render( HtmlTextWriter writer )
    {
        IList<Country> countryList = DataAccessContext.CountryRepository.GetAll( BoolFilter.ShowTrue, "CommonName" );
        Page.ClientScript.RegisterForEventValidation( uxCountryDrop.UniqueID, "" );
        Page.ClientScript.RegisterForEventValidation( uxCountryDrop.UniqueID, "OT" );
        Page.ClientScript.RegisterForEventValidation( uxStateDrop.UniqueID, "" );
        Page.ClientScript.RegisterForEventValidation( uxStateDrop.UniqueID, "OT" );

        ArrayList stateArrayList = new ArrayList();
        for (int index = 0; index < countryList.Count; index++)
        {
            Page.ClientScript.RegisterForEventValidation( uxCountryDrop.UniqueID, countryList[index].CountryCode );
            IList<State> stateList = DataAccessContext.StateRepository.GetAllByCountryCode(
                countryList[index].CountryCode, "StateName", BoolFilter.ShowTrue );

            for (int stateIndex = 0; stateIndex < stateList.Count; stateIndex++)
            {
                if (!CheckItemIfExist( stateArrayList, stateList[stateIndex].StateCode ))
                    Page.ClientScript.RegisterForEventValidation( uxStateDrop.UniqueID, stateList[stateIndex].StateCode );

                //result += GetStateFormat( countryList[index].CountryCode, stateList[stateIndex].StateCode, stateList[stateIndex].StateName );
            }
        }


        //Page.ClientScript.RegisterForEventValidation( uxCountryDrop.UniqueID, "AA" );
        //Page.ClientScript.RegisterForEventValidation( uxCountryDrop.UniqueID, "Anaconda" );

        //ClientScript.RegisterForEventValidation( "DropDownList1", "This is Option 1" );
        //ClientScript.RegisterForEventValidation( "DropDownList1", "This is Option 2" );
        //ClientScript.RegisterForEventValidation( "DropDownList1", "This is Option 3" );
        // Uncomment the line below when you want to specifically register the option for event validation.
        // ClientScript.RegisterForEventValidation("DropDownList1", "Is this option registered for event validation?");
        base.Render( writer );
    }

    public string CurrentCountry
    {
        get
        {
            return CountryHidden.Value;
        }
        set
        {
            _currentCountry = value;
            //if (CountryHidden != null)
            CountryHidden.Value = _currentCountry;
        }
    }

    public void SetCountryByName( string countryName )
    {
        Country country = DataAccessContext.CountryRepository.GetOneByName( countryName );
        if (country != null)
        {
            CurrentCountry = country.CountryCode;
        }
    }

    public string CurrentState
    {
        get
        {
            return StateHidden.Value;
        }
        set
        {
            _currentState = value;
            StateHidden.Value = _currentState;
        }
    }

    public bool IsRequiredCountry
    {
        get { return _isRequireCountry; }
        set { _isRequireCountry = value; }
    }

    public bool IsRequiredState
    {
        get { return _isRequireState; }
        set { _isRequireState = value; }
    }

    public bool IsCountryWithOther
    {
        get { return _isCountryWithOther; }
        set { _isCountryWithOther = value; }
    }

    public bool IsStateWithOther
    {
        get { return _isStateWithOther; }
        set { _isStateWithOther = value; }
    }

    public bool VerifyCountryIsValid
    {
        get
        {
            if (String.IsNullOrEmpty( CurrentCountry ))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public bool VerifyStateIsValid
    {
        get
        {
            IList<State> stateList = DataAccessContext.StateRepository.GetAllByCountryCode( CurrentCountry, "StateName", BoolFilter.ShowTrue );

            if (stateList.Count != 0)
            {
                if (String.IsNullOrEmpty( CurrentState ) || CurrentState == "0")
                    return false;
                else
                    return true;
            }
            return true;
        }
    }

    public string CssPanel
    {
        get { return _cssPanel; }
        set { _cssPanel = value; }
    }

    public string CssLabel
    {
        get { return _cssLabel; }
        set { _cssLabel = value; }
    }

    public string CssInputText
    {
        get { return _cssInputText; }
        set { _cssInputText = value; }
    }

    public string CssInputDrop
    {
        get { return _cssInputDrop; }
        set { _cssInputDrop = value; }
    }

    public string CssValidate
    {
        get { return _cssValidate; }
        set { _cssValidate = value; }
    }

    public bool Validate( out bool validateCountry, out bool validateState )
    {
        validateCountry = true;
        validateState = true;
        if (IsRequiredCountry)
        {
            if (!VerifyCountryIsValid)
            {
                validateCountry = false;
            }
        }

        if (IsRequiredState)
        {
            if (!VerifyStateIsValid)
            {
                validateState = false;
            }
        }

        return validateCountry && validateState;
    }

    public String FormatErrorHtml( string headerMessage, bool validateCountry, bool validateState )
    {
        String errorMessage = String.Empty;

        if (!validateCountry)
            errorMessage += "<li>Required Country</li>";
        if (!validateState)
            errorMessage += "<li>Required State</li>";

        return String.Format( "{0}<ul>{1}</ul>", headerMessage, errorMessage );
    }

    public String FormatErrorHtml( string headerMessage, bool validateBillingCountry, bool validateBillingState, bool validateShippingCountry, bool validateShippingState )
    {
        String errorMessage = String.Empty;

        if (!validateBillingCountry)
            errorMessage += "<li>Required Billing Country</li>";
        if (!validateBillingState)
            errorMessage += "<li>Required Billing State</li>";
        if (!validateShippingCountry)
            errorMessage += "<li>Required Shipping Country</li>";
        if (!validateShippingState)
            errorMessage += "<li>Required Shipping State</li>";

        return String.Format( "{0}<ul>{1}</ul>", headerMessage, errorMessage );
    }

    private HiddenField CountryHidden
    {
        get { return _countryHidden; }
    }

    public string CountryHiddenID
    {
        get { return _countryHiddenID; }
        set { _countryHiddenID = value; }
    }

    private HiddenField StateHidden
    {
        get { return _stateHidden; }
    }

    public string StateHiddenID
    {
        get { return _stateHiddenID; }
        set { _stateHiddenID = value; }
    }
}
