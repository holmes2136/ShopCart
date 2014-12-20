using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;

public partial class AdminAdvanced_Components_Products_Recurring : AdminAdvancedBaseUserControl
{
    public bool IsRecurring
    {
        get { return ConvertUtilities.ToBoolean( uxIsRecurringDrop.SelectedValue ); }
    }

    public string CurrentID
    {
        get
        {
            if (string.IsNullOrEmpty( MainContext.QueryString["ProductID"] ))
                return "0";
            else
                return MainContext.QueryString["ProductID"];
        }
    }

    public string CultureID
    {
        get
        {
            if (ViewState["CultureID"] == null)
                return String.Empty;
            else
                return (String) ViewState["CultureID"];
        }
        set { ViewState["CultureID"] = value; }
    }

    public void ClearInputFields()
    {
        uxIsRecurringDrop.SelectedValue = "False";
        uxIntervalText.Text = "";
        uxIntervalUnitDrop.SelectedIndex = 0;
        uxIntervalNumberCycleText.Text = "";
        uxTrialNumberCycleText.Text = "";
        uxTrialAmountText.Text = "";
    }
    public void DisableRecurring()
    {
        uxIsRecurringDrop.Enabled = false;
        uxIsRecurringDrop.SelectedValue = "False";
        uxRecurringDetailsPanel.Visible = false;
    }

    public void EnableRecurring()
    {
        uxIsRecurringDrop.Enabled = true;
    }

    public void HideRecurring()
    {
        uxIsRecurringDrop.SelectedValue = "False";
        uxRecurringDetailsPanel.Visible = false;
        //uxDownloadableCheck.Enabled = true;
    }

    public void ShowRecurring()
    {
        uxIsRecurringDrop.SelectedValue = "True";
        uxRecurringDetailsPanel.Visible = true;
        //uxDownloadableCheck.Checked = false;
        //uxDownloadableCheck.Enabled = false;
    }

    public void PopulateControls(Product product)
    {
        //Product product = DataAccessContext.ProductRepository.GetOne( DataAccessContext.CultureRepository.GetOne( CultureID ), CurrentID );
        if (product.IsRecurring)
        {
            ShowRecurring();
            EnableRecurring();
        }
        else
        {
            HideRecurring();
        }

        uxIsRecurringDrop.SelectedValue = product.IsRecurring.ToString();

        if (product.IsRecurring)
        {
            uxIntervalText.Text = product.ProductRecurring.RecurringInterval.ToString();
            uxIntervalUnitDrop.SelectedValue = product.ProductRecurring.RecurringIntervalUnit;
            uxIntervalNumberCycleText.Text = product.ProductRecurring.RecurringNumberOfCycles.ToString();
            uxTrialNumberCycleText.Text = product.ProductRecurring.RecurringNumberOfTrialCycles.ToString();
            uxTrialAmountText.Text = String.Format( "{0:f2}", product.ProductRecurring.RecurringTrialAmount );
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        
    }    

    public Product Setup( Product product )
    {
        if (ConvertUtilities.ToBoolean( uxIsRecurringDrop.SelectedValue ))
        {
            ProductRecurring recurring = new ProductRecurring();
            recurring.RecurringInterval = ConvertUtilities.ToInt32( uxIntervalText.Text );
            recurring.RecurringIntervalUnit = uxIntervalUnitDrop.SelectedValue;
            recurring.RecurringNumberOfCycles = ConvertUtilities.ToInt32( uxIntervalNumberCycleText.Text );
            recurring.RecurringNumberOfTrialCycles = ConvertUtilities.ToInt32( uxTrialNumberCycleText.Text );
            recurring.RecurringTrialAmount = ConvertUtilities.ToDecimal( uxTrialAmountText.Text );
            product.ProductRecurring = recurring;            
        }

        product.IsRecurring = ConvertUtilities.ToBoolean( uxIsRecurringDrop.SelectedValue );

        return product;
    }
}
