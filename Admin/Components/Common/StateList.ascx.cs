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

public partial class AdminAdvanced_Components_Common_StateList : AdminAdvancedBaseUserControl
{
    private const string _otherValue = "OT";
    private bool _isStateWithOther = true;
    private string _currentSelected;

    private void SetControlsWithoutOther()
    {
        uxStateDrop.Items.RemoveAt( uxStateDrop.Items.Count - 1 );
        uxStateText.Visible = false;
    }

    private void InitDropdownControls()
    {
        uxStateDrop.Items.Clear();
                
        uxStateDrop.DataSource = DataAccessContext.StateRepository.GetAllByCountryCode( CountryCode, "StateName", BoolFilter.ShowAll );
        uxStateDrop.DataTextField = "StateName";
        uxStateDrop.DataValueField = "StateCode";
        uxStateDrop.DataBind();

        if (uxStateDrop.Items.Count > 0)
            uxStateDrop.Items.Insert( 0, new ListItem( "--Select--", "" ) );

        uxStateDrop.Items.Insert( uxStateDrop.Items.Count, new ListItem( "Other (Please specify)", _otherValue ) );

        if (!IsStateWithOther)
            SetControlsWithoutOther();
    }

    public string CountryCode
    {
        get
        {
            if (ViewState["CountryCode"] == null)
                return String.Empty;
            else
                return (string) ViewState["CountryCode"];
        }
        set { ViewState["CountryCode"] = value; }
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

    public bool IsStateWithOther
    {
        set { _isStateWithOther = value; }
        get { return _isStateWithOther; }
    }

    public string CurrentSelected
    {
        get
        {
            if (uxStateDrop.SelectedValue == "OT")
                return uxStateText.Text;
            else
                return uxStateDrop.SelectedValue;
        }

        set
        {
            _currentSelected = value;
        }
    }

    public void SetValidGroup( string validGroupName )
    {
        uxStateDrop.ValidationGroup = validGroupName;
    }

    public void SetEnable( bool status )
    {
        uxStateDrop.Enabled = status;
        uxStateText.Enabled = status;
    }

    public void Refresh()
    {
        InitDropdownControls();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (IsRequired)
            uxStar.Visible = true;
        else
            uxStar.Visible = false;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            InitDropdownControls();

        if (_currentSelected != null)
        {
            bool existValue = false;
            if (uxStateDrop.Items.Count == 0)
                InitDropdownControls();

            foreach (ListItem item in uxStateDrop.Items)
            {
                if (item.Value == _currentSelected)
                {
                    existValue = true;
                    break;
                }
            }

            if (existValue)
            {
                uxStateDrop.SelectedValue = _currentSelected;
            }
            else
            {
                if (IsStateWithOther)
                {
                    uxStateDrop.SelectedValue = "OT";
                    uxStateText.Text = _currentSelected;
                }
            }
        }

        InitTextBox();
        RegisterDropChange();
    }

    private void InitTextBox()
    {
        if (uxStateDrop.SelectedValue == _otherValue)
        {
            uxStateText.Style["visibility"] = "show";
            uxStateText.Style["width"] = "100px";
        }
        else
        {
            uxStateText.Style["visibility"] = "hidden";
            uxStateText.Style["width"] = "100px";
        }
    }

    private void RegisterDropChange()
    {
        string script = "state = document.getElementById('" + uxStateText.ClientID + "');" +
            "if( this.value == 'OT' ){ state.style.visibility = 'visible';}" +
            "else{ state.style.visibility = 'hidden'; }";
        uxStateDrop.Attributes.Add( "onchange", script );
    }

    public bool VerifyEmptyState()
    {
        if (uxStateDrop.SelectedValue == "" || (uxStateDrop.SelectedValue == "OT" && uxStateText.Text == ""))
        {
            return true;
        }
        else
            return false;
    }
}
