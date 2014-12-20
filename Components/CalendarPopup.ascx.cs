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
using Vevo.WebAppLib;

[ValidationProperty( "SelectedDateText" )]
public partial class Components_CalendarPopup : System.Web.UI.UserControl
{
    private void OnPropertyChanged()
    {
        uxDateCalendarExtender.Format = Format;
        uxDateText.Width = TextBoxWidth;
        //uxRequiredValidator.ValidationGroup = ValidationGroup;
        //uxRequiredValidator.ErrorMessage = ErrorMessage;
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            OnPropertyChanged();
        }
    }


    public string Format
    {
        get
        {
            if (ViewState["Format"] == null)
                ViewState["Format"] = "d";

            return ViewState["Format"].ToString();
        }
        set
        {
            ViewState["Format"] = value;
            OnPropertyChanged();
        }
    }

    public Unit TextBoxWidth
    {
        get
        {
            if (ViewState["TextBoxWidth"] == null)
                ViewState["TextBoxWidth"] = new Unit( "116px" );

            return (Unit) ViewState["TextBoxWidth"];
        }
        set
        {
            ViewState["TextBoxWidth"] = value;
            OnPropertyChanged();
        }
    }

    public bool IsValid
    {
        get
        {
            DateTime result;
            return DateTime.TryParse( uxDateText.Text, CultureUtilities.USCultureInfo, System.Globalization.DateTimeStyles.None, out result );
        }
    }

    public void BindCalendarButton( Page page, LinkButton button )
    {
        WebUtilities.TieButton( page, uxDateText, button );
    }

    //public string ValidationGroup
    //{
    //    get
    //    {
    //        if (ViewState["ValidationGroup"] == null)
    //            ViewState["ValidationGroup"] = String.Empty;

    //        return ViewState["ValidationGroup"].ToString();
    //    }
    //    set
    //    {
    //        ViewState["ValidationGroup"] = value;
    //        OnPropertyChanged();
    //    }
    //}

    //public string ErrorMessage
    //{
    //    get
    //    {
    //        if (ViewState["ErrorMessage"] == null)
    //            ViewState["ErrorMessage"] = String.Empty;

    //        return ViewState["ErrorMessage"].ToString();
    //    }
    //    set
    //    {
    //        ViewState["ErrorMessage"] = value;
    //        OnPropertyChanged();
    //    }
    //}


    public DateTime SelectedDate
    {
        get
        {
            return Convert.ToDateTime( uxDateText.Text, CultureUtilities.USCultureInfo );
        }
        set
        {
            uxDateText.Text = ((DateTime) value).ToString( "d", CultureUtilities.USCultureInfo );
        }
    }

    public string SelectedDateText
    {
        get
        {
            return uxDateText.Text;
        }
        set
        {
            uxDateText.Text = value;
        }
    }

    //public bool TextBoxEnabled
    //{
    //    get
    //    {
    //        return uxDateText.Enabled;
    //    }
    //    set
    //    {
    //        uxDateText.Enabled = value;
    //    }
    //}

    public void Reset()
    {
        uxDateText.Text = String.Empty;
    }

}
