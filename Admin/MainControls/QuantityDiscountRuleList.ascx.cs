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
using Vevo.Domain.Discounts;
using Vevo.Shared.Utilities;
using Vevo.WebUI.Ajax;

public partial class AdminAdvanced_MainControls_QuantityDiscountRuleList : AdminAdvancedBaseListControl
{
    private const int UnlimitedNumber = SystemConst.UnlimitedNumber;

    private string CurrentDiscountGroupID
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["DiscountGroupID"] ))
                return MainContext.QueryString["DiscountGroupID"];
            else
                return "0";
        }
    }

    private string CurrentDiscountType
    {
        get
        {
            if (ViewState["CurrentDiscountType"] == null)
                return "";
            else
                return ViewState["CurrentDiscountType"].ToString();
        }
        set { ViewState["CurrentDiscountType"] = value; }
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }
    }

    private void ApplyPermissions()
    {
        if (!IsAdminModifiable())
        {
            uxAddButton.Visible = false;
            DeleteVisible( false );
        }
        else
            DeleteVisible( true );
    }

    private bool IsContainingOnlyEmptyRow()
    {
        if (uxDiscountGrid.Rows.Count == 1 &&
            ConvertUtilities.ToInt32( uxDiscountGrid.DataKeys[0]["DiscountRuleID"] ) == -1)
            return true;
        else
            return false;
    }

    private void DeleteVisible( bool value )
    {
        uxDeleteButton.Visible = value;
        if (value)
        {
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {
                uxDeleteConfirmButton.TargetControlID = "uxDeleteButton";
                uxConfirmModalPopup.TargetControlID = "uxDeleteButton";
            }
            else
            {
                uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
                uxConfirmModalPopup.TargetControlID = "uxDummyButton";
            }
        }
        else
        {
            uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
            uxConfirmModalPopup.TargetControlID = "uxDummyButton";
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            DiscountGroup discountGroup = DataAccessContext.DiscountGroupRepository.GetOne( CurrentDiscountGroupID );
            uxGroupNameLabel.Text = discountGroup.GroupName;
            CurrentDiscountType = discountGroup.DiscountType.ToString();
        }
    }

    private void SetFooterRowFocus()
    {
        Control textBox = uxDiscountGrid.FooterRow.FindControl( "uxToItemsText" );
        AjaxUtilities.GetScriptManager( this ).SetFocus( textBox );
    }


    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
        ApplyPermissions();
    }

    protected void uxEditLinkButton_PreRender( object sender, EventArgs e )
    {
        if (!IsAdminModifiable())
        {
            LinkButton linkButton = (LinkButton) sender;
            linkButton.Visible = false;
        }
    }

    protected void uxDiscountGrid_DataBound( object sender, EventArgs e )
    {
        if (IsContainingOnlyEmptyRow())
        {
            uxDiscountGrid.Rows[0].Visible = false;
        }
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        uxDiscountGrid.EditIndex = -1;
        uxDiscountGrid.ShowFooter = true;
        RefreshGrid();

        uxAddButton.Visible = false;

        SetFooterRowFocus();

        uxStatusHidden.Value = "FooterShown";
    }

    private void ClearData( GridViewRow row )
    {
        ((TextBox) row.FindControl( "uxToItemsText" )).Text = "";
        ((TextBox) row.FindControl( "uxAmountText" )).Text = "";
    }

    private DiscountRule GetNewDiscontRuleFromGrid( DiscountRule discountRule, GridViewRow row )
    {
        string minitemText = ((TextBox) row.FindControl( "uxToItemsText" )).Text;
        discountRule.ToItems = (String.IsNullOrEmpty( minitemText )) ? 0 : int.Parse( minitemText );

        string amountText = ((TextBox) row.FindControl( "uxAmountText" )).Text;
        string discountResult = (String.IsNullOrEmpty( amountText ) ? "0" : amountText);
        switch (CurrentDiscountType)
        {
            case "Percentage":
                discountRule.Percentage = ConvertUtilities.ToDouble( discountResult );
                discountRule.Amount = 0;
                break;
            case "Price":
                discountRule.Amount = decimal.Parse( discountResult );
                discountRule.Percentage = 0;
                break;
            default:
                discountRule.Percentage = 0;
                discountRule.Amount = 0;
                break;
        }

        return discountRule;
    }

    private DiscountGroup SetUpDiscontRuleFromGrid( DiscountGroup discountGroup, GridViewRow row, string discountRuleID )
    {
        for (int i = 0; i < discountGroup.DiscountRules.Count; i++)
        {
            if (discountGroup.DiscountRules[i].DiscountRuleID == discountRuleID)
            {
                string minitemText = ((TextBox) row.FindControl( "uxToItemsText" )).Text;
                discountGroup.DiscountRules[i].ToItems = (String.IsNullOrEmpty( minitemText )) ? 0 : int.Parse( minitemText );

                string amountText = ((TextBox) row.FindControl( "uxAmountText" )).Text;
                string discountResult = (String.IsNullOrEmpty( amountText ) ? "0" : amountText);
                switch (CurrentDiscountType)
                {
                    case "Percentage":
                        discountGroup.DiscountRules[i].Percentage = ConvertUtilities.ToDouble( discountResult );
                        discountGroup.DiscountRules[i].Amount = 0;
                        break;
                    case "Price":
                        discountGroup.DiscountRules[i].Amount = decimal.Parse( discountResult );
                        discountGroup.DiscountRules[i].Percentage = 0;
                        break;
                    default:
                        discountGroup.DiscountRules[i].Percentage = 0;
                        discountGroup.DiscountRules[i].Amount = 0;
                        break;
                }
            }
        }

        return discountGroup;
    }

    private bool IsExisted( object addMinItem )
    {
        bool isExisted = false;
        foreach (GridViewRow row in uxDiscountGrid.Rows)
        {
            string minItem = ((Label) row.FindControl( "uxToItemsLable" )).Text;
            if (minItem.ToLower() == "above")
                minItem = SystemConst.UnlimitedNumber.ToString();
            if (ConvertUtilities.ToDecimal( minItem ) == ConvertUtilities.ToDecimal( addMinItem ))
                isExisted = true;
        }
        return isExisted;
    }

    private DiscountGroup RemovedDiscountRule( DiscountGroup discountGroup, string discountRuleID )
    {
        for (int i = 0; i < discountGroup.DiscountRules.Count; i++)
        {
            if (discountGroup.DiscountRules[i].DiscountRuleID == discountRuleID)
            {
                discountGroup.DiscountRules.RemoveAt( i );
                return discountGroup;
            }
        }
        return discountGroup;
    }

    protected void uxDiscountGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        if (e.CommandName == "Add")
        {
            GridViewRow rowAdd = uxDiscountGrid.FooterRow;

            DiscountGroup discountGroup = DataAccessContext.DiscountGroupRepository.GetOne( CurrentDiscountGroupID );
            DiscountRule discountRule = new DiscountRule();
            discountRule = GetNewDiscontRuleFromGrid( discountRule, rowAdd );

            if (!IsExisted( discountRule.ToItems ))
            {
                if (discountRule.ToItems <= UnlimitedNumber)
                {
                    discountGroup.DiscountRules.Add( discountRule );

                    DataAccessContext.DiscountGroupRepository.Save( discountGroup );
                    ClearData( rowAdd );

                    RefreshGrid();

                    uxMessage.DisplayMessage( Resources.DiscountMessage.ItemAddSuccess );

                    uxStatusHidden.Value = "Added";
                }
                else
                    uxMessage.DisplayError( Resources.DiscountMessage.TomuchItemError );
            }
            else
            {
                uxStatusHidden.Value = "Error";
                uxMessage.DisplayError( Resources.DiscountMessage.MinItemError );
            }
        }
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxDiscountGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck != null &&
                    deleteCheck.Checked)
                {
                    string discountRuleID =
                        uxDiscountGrid.DataKeys[row.RowIndex]["DiscountRuleID"].ToString();

                    DiscountGroup discountGroup = DataAccessContext.DiscountGroupRepository.GetOne( CurrentDiscountGroupID );
                    discountGroup = RemovedDiscountRule( discountGroup, discountRuleID );
                    DataAccessContext.DiscountGroupRepository.Save( discountGroup );
                    deleted = true;
                }
            }
            uxDiscountGrid.EditIndex = -1;

            if (deleted)
            {
                uxMessage.DisplayMessage( Resources.DiscountMessage.ItemDeleteSuccess );
            }

            uxStatusHidden.Value = "Deleted";
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();
    }

    protected void uxDiscountGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            GridViewRow rowGrid = uxDiscountGrid.Rows[e.RowIndex];

            string DiscountRuleID = uxDiscountGrid.DataKeys[e.RowIndex]["DiscountRuleID"].ToString();
            DiscountGroup discountGroup = DataAccessContext.DiscountGroupRepository.GetOne( CurrentDiscountGroupID );

            discountGroup = SetUpDiscontRuleFromGrid( discountGroup, rowGrid, DiscountRuleID );

            DataAccessContext.DiscountGroupRepository.Save( discountGroup );

            uxDiscountGrid.EditIndex = -1;
            RefreshGrid();

            uxMessage.DisplayMessage( Resources.DiscountMessage.ItemUpdateSuccess );

            uxStatusHidden.Value = "Updated";
        }
        finally
        {
            e.Cancel = true;
        }

    }

    public string ShowFromItem( string toItem )
    {
        int fromItems = 0;
        if (CurrentDiscountGroupID != "0")
        {
            DiscountGroup discountGroup = DataAccessContext.DiscountGroupRepository.GetOne( CurrentDiscountGroupID );
            for (int i = 0; i < discountGroup.DiscountRules.Count; i++)
            {
                if (discountGroup.DiscountRules[i].ToItems == ConvertUtilities.ToInt32( toItem ))
                    return String.Format( "{0}", ++fromItems );
                else
                    fromItems = discountGroup.DiscountRules[i].ToItems;
            }
        }
        else
            MainContext.RedirectMainControl( "QuantityDiscountList.ascx", "" );
        return String.Format( "{0}", ++fromItems );
    }

    public bool CheckVisibleFromToItems( string toItems )
    {
        if (toItems == UnlimitedNumber.ToString())
            return false;
        else
            return true;
    }

    public string LastToItems( string toItems )
    {
        if (toItems == UnlimitedNumber.ToString())
            return "Above";
        else
            return toItems;
    }

    protected override void RefreshGrid()
    {
        DiscountGroup discountGroup = DataAccessContext.DiscountGroupRepository.GetOne( CurrentDiscountGroupID );

        uxDiscountGrid.DataSource = discountGroup.DiscountRules;
        uxDiscountGrid.DataBind();
    }

    protected string GetAmount( object percentage, object amount )
    {
        if (CurrentDiscountType == "Percentage")
            return percentage.ToString() + "%";
        else
            return String.Format( "{0:f2}", amount );
    }

    protected string GetEditAmount( object percentage, object amount )
    {
        if (CurrentDiscountType == "Percentage")
            return percentage.ToString();
        else
            return String.Format( "{0:f2}", amount );
    }

    protected void uxDiscountGrid_RowEditing( object sender, GridViewEditEventArgs e )
    {
        uxDiscountGrid.EditIndex = e.NewEditIndex;
        RefreshGrid();
    }

    protected void uxDiscountGrid_CancelingEdit( object sender, GridViewCancelEditEventArgs e )
    {
        uxDiscountGrid.EditIndex = -1;
        RefreshGrid();
    }
}
