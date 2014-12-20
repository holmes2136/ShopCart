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
using Vevo.Domain.Stores;

public partial class Admin_Components_StoreDropDownList : AdminAdvancedBaseUserControl
{
    private string _currentSelected;
    private string _firstItemText = "-- Please Select --";
    private string _firstItemValue = "";
    private bool _firstItemVisible = true;

    protected void uxDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        CurrentSelected = uxStoreDrop.SelectedValue;

        // Send event to parent controls
        OnBubbleEvent( e );
    }

    public string CurrentSelected
    {
        get
        {
            _currentSelected = uxStoreDrop.SelectedValue;
            return _currentSelected;
        }
        set
        {
            uxSelectedStoreHidden.Value = value;
            _currentSelected = value;
            SetDropdownValue( _currentSelected );
        }
    }

    public bool AutoPostBack
    {
        get
        {
            return uxStoreDrop.AutoPostBack;
        }
        set
        {
            uxStoreDrop.AutoPostBack = value;
        }
    }


    public string FirstItemText
    {
        get
        {
            return _firstItemText;
        }
        set
        {
            _firstItemText = value;
        }
    }

    public string FirstItemValue
    {
        get
        {
            return _firstItemValue;
        }
        set
        {
            _firstItemValue = value;
        }
    }

    public bool FirstItemVisible
    {
        get
        {
            return _firstItemVisible;
        }
        set
        {
            _firstItemVisible = value;
        }
    }

    private void SetDropdownValue( string value )
    {
        if (!String.IsNullOrEmpty( value ))
        {
            bool existValue = false;
            if (uxStoreDrop.Items.Count == 0)
                Refresh();

            foreach (ListItem item in uxStoreDrop.Items)
            {
                if (item.Text != _firstItemText)
                {
                    if (item.Value == value)
                    {
                        existValue = true;
                        break;
                    }
                }
            }

            if (existValue)
            {
                uxStoreDrop.SelectedValue = value;
            }
        }

    }

    public void SetValidGroup( string validGroupName )
    {
        uxStoreDrop.ValidationGroup = validGroupName;
    }

    public void SetEnable( bool status )
    {
        uxStoreDrop.Enabled = status;
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

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            Refresh();

        if (IsRequired)
            uxStar.Visible = true;
        else
            uxStar.Visible = false;
    }

    public void Refresh()
    {
        uxStoreDrop.Items.Clear();
        uxStoreDrop.AutoPostBack = true;

        if (FirstItemVisible)
        {
            uxStoreDrop.Items.Add( new ListItem( _firstItemText, _firstItemValue ) );
        }

        IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "StoreID" );

        for (int index = 0; index < storeList.Count; index++)
        {
            uxStoreDrop.Items.Add( new ListItem( storeList[index].StoreName, storeList[index].StoreID ) );
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        SetDropdownValue( _currentSelected );
    }
}
