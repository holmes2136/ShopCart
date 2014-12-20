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
using Vevo.Domain;
using Vevo.Domain.Shipping;
using Vevo.Domain.Shipping.Custom;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;

public partial class AdminAdvanced_MainControls_ShippingList : AdminAdvancedBaseListControl
{
    private const int ShippingID = 1;
    private const int RateLinkColumn = 5;

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();

            if (uxGridShipping.Rows.Count == 0)
                uxGridShipping.Visible = false;
            else
                uxGridShipping.Visible = true;
        }

        if (uxGridShipping.Rows.Count > 0)
        {
            DeleteVisible( true );
            uxPagingControl.Visible = true;
        }
        else
        {
            DeleteVisible( false );
            uxPagingControl.Visible = false;
        }

        if (!IsAdminModifiable())
        {
            uxAddButton.Visible = false;
            DeleteVisible( false );
        }
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

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
            uxPagingControl.ItemsPerPages = AdminConfig.ShippingItemPerPage;

        RegisterGridView( uxGridShipping, "ShippingID" );
        uxGridShipping.Columns[RateLinkColumn].Visible = false;

        RegisterLanguageControl( uxLanguageControl );
        RegisterPagingControl( uxPagingControl );

        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxGridShipping.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string id = row.Cells[ShippingID].Text.Trim();
                    DataAccessContext.ShippingOptionRepository.Delete( id );
                    deleted = true;
                }
            }

            if (deleted)
                uxMessage.DisplayMessage( Resources.ShippingMessages.DeleteSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if (uxGridShipping.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "ShippingSelectMethod.ascx", "" );
    }
    public string GetImage( object fieldValue )
    {
        if (ConvertUtilities.ToBoolean( fieldValue ))
            return "CssSymbolTrue";
        else
            return "CssSymbolFalse";
    }

    protected bool IsByWeightOptionType( object typeName )
    {
        return String.Compare(
            typeName.ToString(), ShippingOptionType.TypeEnum.ByWeight.ToString(), true ) == 0;
    }

    protected bool IsByOrderTotalOptionType( object typeName )
    {
        return String.Compare(
            typeName.ToString(), ShippingOptionType.TypeEnum.ByOrderTotal.ToString(), true ) == 0;
    }

    protected override void RefreshGrid()
    {
        int totalItems;

        uxGridShipping.DataSource = DataAccessContext.ShippingOptionRepository.SearchShipping(
            uxLanguageControl.CurrentCulture,
            GridHelper.GetFullSortText(),
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGridShipping.DataBind();
    }

    protected void uxZoneGroupButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "ShippingZoneGroupList.ascx" );
    }

    protected void uxGridShipping_RowDataBound( object sender, GridViewRowEventArgs e )
    {
        if (uxGridShipping.Columns[RateLinkColumn].Visible == false)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Vevo.WebUI.ServerControls.AdvancedLinkButton uxWeightRate = (Vevo.WebUI.ServerControls.AdvancedLinkButton) e.Row.FindControl( "uxWeightRate" );
                Vevo.WebUI.ServerControls.AdvancedLinkButton uxOrderTotalRate = (Vevo.WebUI.ServerControls.AdvancedLinkButton) e.Row.FindControl( "uxOrderTotalRate" );
                if (uxWeightRate.Visible || uxOrderTotalRate.Visible)
                    uxGridShipping.Columns[RateLinkColumn].Visible = true;
            }
        }
    }
}
