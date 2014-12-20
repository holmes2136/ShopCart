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
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebUI.Ajax;
using Vevo.Base.Domain;

public partial class Components_OptionItemsDetails : AdminAdvancedBaseListControl
{
    private const int _columnOptionItemID = 1;

    public string CurrentID
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["OptionGroupID"] ))
                return MainContext.QueryString["OptionGroupID"];
            else
                return "0";
        }
    }

    private string CurrentAdditionalPrice
    {
        get
        {
            return (string) ViewState["CurrentAdditionalPrice"];
        }
        set
        {
            ViewState["CurrentAdditionalPrice"] = value;
        }
    }

    private bool IsContainingOnlyEmptyRow()
    {
        if (uxOptionItemGrid.Rows.Count == 1 &&
            ConvertUtilities.ToInt32( uxOptionItemGrid.DataKeys[0]["OptionItemID"] ) == -1)
            return true;
        else
            return false;
    }

    private int RecordOptionItems()
    {
        IList<OptionItem> list = DataAccessContext.OptionItemRepository.GetByOptionGroupID(
            uxLanguageControl.CurrentCulture, CurrentID );
        return list.Count;
    }

    private OptionGroup.OptionGroupType OptionGroupType
    {
        get
        {
            if (ViewState["OptiongroupType"] == null)
            {
                OptionGroup optionGroup = DataAccessContext.OptionGroupRepository.GetOne(
                    uxLanguageControl.CurrentCulture, CurrentID );
                ViewState["OptiongroupType"] = optionGroup.Type;
            }
            return (OptionGroup.OptionGroupType) ViewState["OptiongroupType"];
        }
    }

    private void PopulateHeader()
    {
        OptionGroup optionGroup =
            DataAccessContext.OptionGroupRepository.GetOne( uxLanguageControl.CurrentCulture, CurrentID );
        uxOptionGroupLabel.Text = optionGroup.Name;
    }

    // This function is useful during switching from ItemTemplate to EditTemplate
    private void PopulatePriceTypeDropDown()
    {
        if (uxOptionItemGrid.EditIndex != -1)
        {
            Label additionalPrice =
                (Label) (uxOptionItemGrid.Rows[uxOptionItemGrid.EditIndex].Cells[3].FindControl( "uxTypeLabel" ));

            // If label is not found, this control is already in Edit mode
            if (additionalPrice != null)
                CurrentAdditionalPrice = additionalPrice.Text;
        }
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            PopulateHeader();

            RefreshGrid();

            if (IsAdminModifiable())
            {
                if (OptionGroupType == OptionGroup.OptionGroupType.Text ||
                    OptionGroupType == OptionGroup.OptionGroupType.Upload ||
                    OptionGroupType == OptionGroup.OptionGroupType.UploadRequired)
                {
                    if (RecordOptionItems() >= 1)
                    {
                        uxAddOptionButton.Visible = false;
                    }
                    else
                    {
                        uxAddOptionButton.Visible = true;
                    }
                }
                DeleteVisible( true );
            }
            else
            {
                uxAddOptionButton.Visible = false;
                DeleteVisible( false );

                foreach (GridViewRow gr in uxOptionItemGrid.Rows)
                {
                    gr.FindControl( "uxEditLinkButton" ).Visible = false;
                }
            }
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

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.OptionItemRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list, "WeightToAdd", "PercentageChange", "PriceToAdd" );
    }

    private OptionItem.PriceType GetOptionItemType( string priceType )
    {
        return (OptionItem.PriceType) Enum.Parse( typeof( OptionItem.PriceType ), priceType );
    }

    private OptionItem GetDetailsFromPanel()
    {
        OptionItem optionItem = new OptionItem( uxLanguageControl.CurrentCulture );
        optionItem.OptionGroupID = CurrentID;
        optionItem.Name = uxNameText.Text;
        optionItem.OptionItemType = GetOptionItemType( uxPriceTypeDrop.SelectedValue );
        optionItem.WeightToAdd = ConvertUtilities.ToDouble( uxWeightToAddText.Text );
        optionItem.ImageFile = uxImageFileText.Text;


        if (optionItem.OptionItemType == OptionItem.PriceType.Price)
        {
            optionItem.PriceToAdd = ConvertUtilities.ToDecimal( uxPriceTypeText.Text );
        }
        else
        {
            optionItem.PercentageChange = ConvertUtilities.ToDouble( uxPriceTypeText.Text );
        }

        return optionItem;
    }

    private void GetDetailsFromGrid( OptionItem optionItem, GridViewRow rowGrid )
    {
        optionItem.Name = ((TextBox) rowGrid.FindControl( "uxNameText" )).Text;
        optionItem.OptionItemType = GetOptionItemType( ((DropDownList) rowGrid.FindControl( "uxPriceTypeDrop" )).SelectedValue );
        optionItem.WeightToAdd = ConvertUtilities.ToDouble( ((TextBox) rowGrid.FindControl( "uxweightToAddText" )).Text );
        optionItem.ImageFile = ((TextBox) rowGrid.FindControl( "uxImageFileText" )).Text;

        if (optionItem.OptionItemType == OptionItem.PriceType.Price)
        {
            optionItem.PriceToAdd = ConvertUtilities.ToDecimal( ((TextBox) rowGrid.FindControl( "uxPriceTypeText" )).Text );
            optionItem.PercentageChange = 0;
        }
        else
        {
            optionItem.PriceToAdd = 0;
            optionItem.PercentageChange = ConvertUtilities.ToDouble( ((TextBox) rowGrid.FindControl( "uxPriceTypeText" )).Text );
        }
    }

    private void SetFooterRowFocus()
    {
        //Control textBox = uxOptionItemGrid.FooterRow.FindControl( "uxNameText" );
        AjaxUtilities.GetScriptManager( this ).SetFocus( uxNameText );
    }

    private void PanelAddShow( bool value )
    {
        if (value)
            uxAddPanel.CssClass = "b15 GridBottomAddPanel";
        else
            uxAddPanel.CssClass = "b15 GridBottomAddPanel dn";
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.OptionItemsPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxOptionItemGrid, "OptionItemID" );

        RegisterLanguageControl( uxLanguageControl );
        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }

    private void ClearInput()
    {
        uxNameText.Text = "";
        uxPriceTypeText.Text = "";
        uxWeightToAddText.Text = "";
        uxImageFileText.Text = "";
        uxPriceTypeDrop.SelectedValue = "Price";
    }

    private void CreateDummyRow( IList<OptionItem> list )
    {
        OptionItem optionItem = new OptionItem( uxLanguageControl.CurrentCulture );
        optionItem.OptionItemID = "-1";
        optionItem.Name = string.Empty;
        optionItem.WeightToAdd = -1;
        optionItem.ImageFile = string.Empty;
        list.Add( optionItem );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpGridSupportControls();

        uxUpload.ReturnTextControlClientID = uxImageFileText.ClientID;

        if (!MainContext.IsPostBack)
        {
            PanelAddShow( false );
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void AddOption_Click( object sender, EventArgs e )
    {
        OptionItem optionItem = GetDetailsFromPanel();
        optionItem = DataAccessContext.OptionItemRepository.Save( optionItem );

        ClearInput();
        RefreshGrid();

        if (OptionGroupType == OptionGroup.OptionGroupType.Text ||
            OptionGroupType == OptionGroup.OptionGroupType.Upload ||
            OptionGroupType == OptionGroup.OptionGroupType.UploadRequired)
        {
            if (RecordOptionItems() >= 1)
            {
                uxAddOptionButton.Visible = false;
                PanelAddShow( false );
            }
        }

        uxMessage.DisplayMessage( Resources.ProductOptionMessages.ItemAddSuccess );
    }

    protected void uxOptionItemGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            GridViewRow rowGrid = uxOptionItemGrid.Rows[e.RowIndex];

            string optionItemID = ((Label) rowGrid.FindControl( "uxOptionItemIDLabel" )).Text;
            OptionItem optionItem = DataAccessContext.OptionItemRepository.GetOne( uxLanguageControl.CurrentCulture, optionItemID );
            GetDetailsFromGrid( optionItem, rowGrid );

            if (String.IsNullOrEmpty( optionItem.Name ))
                throw new Exception( "Empty name!" );

            DataAccessContext.OptionItemRepository.Save( optionItem );
            uxOptionItemGrid.EditIndex = -1;
            RefreshGrid();

            uxMessage.DisplayMessage( Resources.ProductOptionMessages.ItemUpdateSuccess );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;
        }
    }

    protected void uxOptionItemGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        if (e.CommandName == "edit")
            PopulatePriceTypeDropDown();
    }

    protected void uxOptionItemGrid_RowEditing( object sender, GridViewEditEventArgs e )
    {
        uxOptionItemGrid.EditIndex = e.NewEditIndex;
        PopulatePriceTypeDropDown();
        RefreshGrid();
    }

    protected void uxOptionItemGrid_DataBound( object sender, EventArgs e )
    {
        if (IsContainingOnlyEmptyRow())
        {
            uxOptionItemGrid.Rows[0].Visible = false;
        }
    }

    protected void uxOptionItemGrid_CancelingEdit( object sender, GridViewCancelEditEventArgs e )
    {
        uxOptionItemGrid.EditIndex = -1;
        RefreshGrid();
    }

    protected void uxPriceTypeDrop_PreRender( object sender, EventArgs e )
    {
        DropDownList typePrice = (DropDownList) sender;
        if (CurrentAdditionalPrice == "Percentage")
            typePrice.SelectedValue = "Percentage";
        else
            typePrice.SelectedValue = "Price";

    }

    protected bool IsPercentageType( object priceToAddField )
    {
        if (priceToAddField == null ||
            priceToAddField == DBNull.Value ||
            Decimal.Parse( priceToAddField.ToString() ) == -1.0m)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        bool deleted = false;
        foreach (GridViewRow row in uxOptionItemGrid.Rows)
        {
            CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
            if (deleteCheck != null &&
                deleteCheck.Checked)
            {
                string optionItemID = uxOptionItemGrid.DataKeys[row.RowIndex]["OptionItemID"].ToString();
                DataAccessContext.OptionItemRepository.Delete( optionItemID );
                deleted = true;

                if (OptionGroupType == OptionGroup.OptionGroupType.Text ||
                    OptionGroupType == OptionGroup.OptionGroupType.Upload ||
                    OptionGroupType == OptionGroup.OptionGroupType.UploadRequired)
                {
                    if (RecordOptionItems() >= 1)
                    {
                        uxAddOptionButton.Visible = false;
                    }
                    else
                    {
                        uxAddOptionButton.Visible = true;
                    }
                }
            }
        }
        uxOptionItemGrid.EditIndex = -1;

        if (deleted)
        {
            uxMessage.DisplayMessage( Resources.ProductOptionMessages.ItemDeleteSuccess );
        }

        RefreshGrid();

        if (uxOptionItemGrid.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }
    }

    protected void uxAddOptionButton_Click( object sender, EventArgs e )
    {
        uxOptionItemGrid.EditIndex = -1;
        uxAddOptionButton.Visible = false;
        PanelAddShow( true );
        RefreshGrid();
        SetFooterRowFocus();
    }

    protected void uxUpload_Init( object sender, EventArgs e )
    {
        AdminAdvanced_Components_Common_Upload upload =
            (AdminAdvanced_Components_Common_Upload) sender;

        TextBox textbox = (TextBox) upload.Parent.Parent.FindControl( "uxImageFileText" );
        upload.ReturnTextControlClientID = textbox.ClientID;
    }

    protected override void RefreshGrid()
    {
        int totalItems = 0;
        IList<OptionItem> list = DataAccessContext.OptionItemRepository.SearchOptionItem(
            uxLanguageControl.CurrentCulture,
            CurrentID,
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );

        if (list == null || list.Count == 0)
        {
            list = new List<OptionItem>();
            CreateDummyRow( list );
        }

        uxOptionItemGrid.DataSource = list;
        uxOptionItemGrid.DataBind();

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
    }

}
