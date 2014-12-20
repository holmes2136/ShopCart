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

public partial class Components_ItemsPerPageDrop : Vevo.WebUI.International.BaseLanguageUserControl
{
    private string[] _defaultList = new string[] { "20", "40", "60", "All" };

    private static string[] SortArray( string[] array )
    {
        int[] stringToInt = Array.ConvertAll<string, int>
            ( array, new Converter<string, int>( System.Convert.ToInt32 ) );
        Array.Sort( stringToInt );
        string[] Result = Array.ConvertAll<int, string>
            ( stringToInt, new Converter<int, string>( System.Convert.ToString ) );
        return Result;
    }

    private void Refresh()
    {
        string items;
        int countText;
        string defaultValue;
        String[] txt;
        uxDrop.Items.Clear();

        if (!String.IsNullOrEmpty( PageListConfig ))
        {
            items = DataAccessContext.Configurations.GetValue( PageListConfig );
            txt = items.Split( char.Parse( "," ) );
            defaultValue = txt[0];
            txt = SortArray( txt );
        }
        else
        {
            txt = _defaultList;
            defaultValue = txt[0];
        }

        countText = txt.Length;
        
        for (int i = 0; i <= countText - 1; i++)
        {
            if (i != 0)
            {
                if (txt[i] != txt[i - 1])
                {
                    uxDrop.Items.Add( txt[i] );
                }
            }
            else
            {
                uxDrop.Items.Add( txt[i] );
            }
        }

        uxDrop.SelectedValue = SelectedValue;
        uxValueHidden.Value = uxDrop.SelectedValue;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            Refresh();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {

    }

    protected void uxDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        SelectedValue = uxDrop.SelectedValue;

        OnBubbleEvent( e );

        uxValueHidden.Value = uxDrop.SelectedValue;
    }

    public void UpdateSelectedValue( string Number )
    {
        uxDrop.SelectedValue = Number.ToString();
    }

    public string PageListConfig
    {
        get
        {
            if (ViewState["PageListConfig"] == null)
                return String.Empty;
            else
                return ViewState["PageListConfig"].ToString();
        }
        set
        {
            ViewState["PageListConfig"] = value;
        }
    }

    private string CurrentSelected
    {
        get
        {
            if (ViewState["CurrentSelected"] == null)
                return _defaultList[0];
            else
                return (String) ViewState["CurrentSelected"];
        }
        set
        {
            ViewState["CurrentSelected"] = value;
        }
    }

    public string SelectedValue
    {
        get
        {
             return CurrentSelected;
        }
        set
        {
            CurrentSelected = value;
        }
    }

    public string DefaultValue
    {
        get
        {
            if (uxDrop.Items.Count == 0) Refresh();
            return uxDrop.Items[0].Value.ToString();
        }
    }

    public void SelectDefault()
    {
        uxDrop.SelectedIndex = 0;
        SelectedValue = uxDrop.SelectedValue;
        uxValueHidden.Value = uxDrop.SelectedValue;
    }

    public string SelectValue(string value)
    {
        if (uxDrop.Items.FindByValue( value ) == null)
        {
            return uxDrop.SelectedValue;
        }
        uxDrop.SelectedValue = value;
        SelectedValue = uxDrop.SelectedValue;
        uxValueHidden.Value = uxDrop.SelectedValue;
        return SelectedValue;
    }
}
