using System;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.WebAppLib;

public partial class Components_RatingControl : Vevo.WebUI.International.BaseLanguageUserControl
{
    private DataTable GetStars()
    {
        DataTable table = new DataTable();
        table.Columns.Add( new DataColumn( "Vote", System.Type.GetType( "System.String" ) ) );

        double count = CurrentRating;

        int maxBound = Convert.ToInt32( DataAccessContext.Configurations.GetValue( "StarRatingAmount" ) );
        count *= maxBound;

        for (int i = 0; i < maxBound; i++)
        {
            DataRow row = table.NewRow();

            if (count >= 1)
            {
                row["Vote"] = "F";
                count -= 1;
            }
            else if (count >= 0.75)
            {
                row["Vote"] = "F";
                count = 0;
            }
            else if (count > 0.25 && count < 0.75)
            {
                row["Vote"] = "H";
                count = 0;
            }
            else
            {
                row["Vote"] = "E";
                count = 0;
            }

            table.Rows.Add( row );
        }

        return table;
    }


    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (HideOnZero && CurrentRating <= 0.0)
        {
            this.Visible = false;
        }
        else
        {
            uxRatingRepeater.DataSource = GetStars();
            uxRatingRepeater.DataBind();
        }
    }


    public string GetStarPicture( string voted )
    {
        string themeName = DataAccessContext.Configurations.GetValue("StoreTheme");
        if (voted == "F")
            return String.Format("Themes/{0}/Images/Design/Icon/Star_Full.gif", themeName);
        else if (voted == "H")
            return String.Format("Themes/{0}/Images/Design/Icon/Star_Half.gif", themeName);
        else
            return String.Format("Themes/{0}/Images/Design/Icon/Star_Empty.gif", themeName);
    }

    public double CurrentRating
    {
        get
        {
            if (ViewState["CurrentRating"] == null)
                return 0.0;
            else
                return (double) ViewState["CurrentRating"];
        }
        set
        {
            ViewState["CurrentRating"] = value;
        }
    }

    public bool HideOnZero
    {
        get
        {
            if (ViewState["HideOnZero"] == null)
                return false;
            else
                return (bool) ViewState["HideOnZero"];
        }
        set
        {
            ViewState["HideOnZero"] = value;
        }
    }

}
