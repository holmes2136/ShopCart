using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;

[ValidationProperty( "SelectedDateText" )]
public partial class AdminAdvanced_Components_CalendarPopup : System.Web.UI.UserControl
{
    private void OnPropertyChanged()
    {
        uxDateCalendarExtender.Format = Format;
        uxDateText.Width = TextBoxWidth;
        uxDateText.Height = TextBoxHeight;
        uxDateText.CssClass = CssClass;
        uxDateImageButton.CssClass = ImageButtonCssClass;
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            OnPropertyChanged();
        }
    }

    public string CssClass
    {
        get
        {
            if (ViewState["CssClass"] == null)
                ViewState["CssClass"] = "fl TextBox";
            return ViewState["CssClass"].ToString();
        }
        set
        {
            ViewState["CssClass"] = value;
            OnPropertyChanged();
        }
    }

    public string ImageButtonCssClass
    {
        get
        {
            if (ViewState["ImageButtonCssClass"] == null)
                ViewState["ImageButtonCssClass"] = "CalendarPosition";

            return ViewState["ImageButtonCssClass"].ToString();
        }
        set
        {
            ViewState["ImageButtonCssClass"] = value;
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

    public Unit TextBoxHeight
    {
        get
        {
            if (ViewState["TextBoxHeight"] == null)
                ViewState["TextBoxHeight"] = new Unit( "14px" );
            return (Unit) ViewState["TextBoxHeight"];
        }
        set
        {
            ViewState["TextBoxHeight"] = value;
            OnPropertyChanged();
        }
    }

    public Unit TextBoxWidth
    {
        get
        {
            if (ViewState["TextBoxWidth"] == null)
                ViewState["TextBoxWidth"] = new Unit( "80px" );

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
            return DateTime.TryParse( uxDateText.Text, out result );
        }
    }

    public DateTime SelectedDate
    {
        get
        {
            return Convert.ToDateTime( uxDateText.Text, CultureUtilities.USCultureInfo );
        }
        set
        {
            uxDateText.Text = value.ToString( Format, CultureUtilities.USCultureInfo );
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

    public bool TextBoxEnabled
    {
        get
        {
            return uxDateText.Enabled;
        }
        set
        {
            uxDateText.Enabled = value;
        }
    }

    public void Reset()
    {
        uxDateText.Text = String.Empty;
    }
}
