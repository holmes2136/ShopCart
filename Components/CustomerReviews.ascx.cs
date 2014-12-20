using System;
using System.Data;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;

public partial class Components_CustomerReviews : Vevo.WebUI.International.BaseLanguageUserControl
{
    private string CultureID
    {
        get
        {
            return CultureUtilities.StoreCultureID;
        }
    }

    private DataTable ConvertDetail( DataTable dt )
    {
        DataTable dtConverted = dt.Clone();

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (!String.IsNullOrEmpty( dr["Subject"].ToString() ) &&
                    !String.IsNullOrEmpty( dr["Body"].ToString() ))
                {
                    DataRow drConverted = dtConverted.NewRow();

                    drConverted["ReviewID"] = dr["ReviewID"].ToString();
                    drConverted["ProductID"] = dr["ProductID"].ToString();

                    if (dr["UserName"].ToString() == "")
                        drConverted["UserName"] = "Anonymous";
                    else
                        drConverted["UserName"] = dr["UserName"].ToString();

                    if (dr["FirstName"].ToString() == "")
                        drConverted["FirstName"] = "Anonymous";
                    else
                        drConverted["FirstName"] = dr["FirstName"].ToString();

                    if (dr["FullName"].ToString() == "")
                        drConverted["FullName"] = "Anonymous";
                    else
                        drConverted["FullName"] = dr["FullName"].ToString();

                    if (dr["LastName"].ToString() == "")
                        drConverted["LastName"] = "Anonymous";
                    else
                        drConverted["LastName"] = dr["LastName"].ToString();

                    drConverted["ReviewRating"] = dr["ReviewRating"].ToString();
                    drConverted["Enabled"] = dr["Enabled"].ToString();
                    drConverted["ReviewDate"] = dr["ReviewDate"].ToString();
                    drConverted["Subject"] = dr["Subject"].ToString();
                    drConverted["Body"] = dr["Body"].ToString();
                    drConverted["CultureID"] = dr["CultureID"].ToString();

                    dtConverted.Rows.Add( drConverted );
                }
            }
        }

        return dtConverted;
    }

    private void PopulateReviews()
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "CustomerReview" ) 
            && !DataAccessContext.Configurations.GetBoolValue( "CustomerRating" ))
        {
            this.Visible = false;
        }
        else
        {
            DataTable dt = new DataTable();
            if (DataAccessContext.Configurations.GetBoolValue( "EnableReviewPerCulture" ))
                dt = CustomerReviewAccess.GetByEnabled( true, ProductID, CultureID );
            else
                dt = CustomerReviewAccess.GetByEnabled( true, ProductID );

            if (dt.Rows.Count > 0)
            {
                this.Visible = true;

                uxCustomerReviewList.DataSource = ConvertDetail( dt );
                uxCustomerReviewList.DataBind();
            }
            else
            {
                this.Visible = false;
            }
        }

    }


    protected void Page_Load( object sender, EventArgs e )
    {
        
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateReviews();
    }

    protected double GetCustomerRating( object reviewID )
    {
        CustomerReviewDetails details = new CustomerReviewDetails();
        if (DataAccessContext.Configurations.GetBoolValue( "EnableReviewPerCulture" ))
            details = CustomerReviewAccess.GetDetails( reviewID.ToString(), CultureID );
        else
            details = CustomerReviewAccess.GetDetails( reviewID.ToString() );
        return Convert.ToDouble( details.ReviewRating );
    }

    public string ProductID
    {
        get
        {
            if (ViewState["ProductID"] == null)
                return "0";
            else
                return (string) ViewState["ProductID"];
        }
        set
        {
            ViewState["ProductID"] = value;
        }
    }

}
