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
using Vevo.Domain;
using Vevo.Domain.Users;

public partial class AdminAdvanced_Components_Common_CountryList : AdminAdvancedBaseUserControl
{
    private const string _otherValue = "OT";
    private bool _isCountryWithOther = true;
    private string _currentSelected;

    private void InitTextBox()
    {
        if (uxCountryDrop.SelectedValue == _otherValue)
        {
            uxCountryText.Style["display"] = "";
        }
        else
        {
            uxCountryText.Style["display"] = "none";
        }
        uxCountryText.Style["width"] = "100px";
    }

    protected void uxDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        CurrentSelected = uxCountryDrop.SelectedValue;

        // Send event to parent controls
        OnBubbleEvent( e );
    }

    private bool IsEmptyCountry( DropDownList drop )
    {
        if (drop.SelectedValue == "" ||
            (drop.SelectedValue == _otherValue && uxCountryText.Text == ""))
            return true;
        else
            return false;
    }

    public bool IsRequired
    {
        get
        {
            if (ViewState["IsRequired"] == null)
                ViewState["IsRequired"] = false;
            return bool.Parse( ViewState["IsRequired"].ToString() );
        }
        set { ViewState["IsRequired"] = value; }
    }

    public bool IsCountryWithOther
    {
        set { _isCountryWithOther = value; }
        get { return _isCountryWithOther; }
    }

    public string CurrentSelected
    {
        get
        {
            if (uxCountryDrop.SelectedValue == _otherValue)
            {
                _currentSelected = uxCountryText.Text;
                return _currentSelected;
            }
            else
            {
                _currentSelected = uxCountryDrop.SelectedValue;
                return _currentSelected;
            }
        }
        set
        {
            uxSelectedCountryHidden.Value = value;
            _currentSelected = value;
            SetDropdownValue( _currentSelected );
        }
    }

    private void SetDropdownValue( string value )
    {
        if (!String.IsNullOrEmpty( value ))
        {
            bool existValue = false;
            if (uxCountryDrop.Items.Count == 0)
                InitDropdownControls();

            foreach (ListItem item in uxCountryDrop.Items)
            {
                if (item.Text != "--Select--")
                {
                    if (item.Value == value)
                    {
                        existValue = true;
                        break;
                    }
                }
            }

            if (IsCountryWithOther)
            {
                if (existValue)
                {
                    uxCountryDrop.SelectedValue = value;
                }
                else
                {
                    uxCountryDrop.SelectedIndex = uxCountryDrop.Items.Count - 1;
                    uxCountryText.Text = value;
                }
            }
            else
            {
                if (existValue && (value != _otherValue))
                {
                    uxCountryDrop.SelectedValue = value;
                }
            }
        }

        InitTextBox();
    }

    public bool IsUSA()
    {
        if (uxCountryDrop.SelectedValue == "US")
            return true;
        else
            return false;
    }

    public void SetValidGroup( string validGroupName )
    {
        uxCountryDrop.ValidationGroup = validGroupName;
    }

    public void SetEnable( bool status )
    {
        uxCountryDrop.Enabled = status;
        uxCountryText.Enabled = status;
    }

    private void InitDropdownControls()
    {
        uxCountryDrop.Items.Clear();
        uxCountryDrop.AutoPostBack = true;

        uxCountryDrop.Items.Add( new ListItem( "--Select--", "" ) );

        IList<Country> countryList = DataAccessContext.CountryRepository.GetAll( BoolFilter.ShowTrue, "CommonName" );
        for (int index = 0; index < countryList.Count; index++)
        {
            uxCountryDrop.Items.Add( new ListItem( countryList[index].CommonName, countryList[index].CountryCode ) );
        }
        if (IsCountryWithOther)
        {
            uxCountryDrop.Items.Add( new ListItem( "Other (Please specify) ", _otherValue ) );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            InitDropdownControls();

        if (IsRequired)
            uxStar.Visible = true;
        else
            uxStar.Visible = false;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        SetDropdownValue( _currentSelected );
    }

    public bool VerifyEmptyCountry()
    {
        return IsEmptyCountry( uxCountryDrop );
    }
}
