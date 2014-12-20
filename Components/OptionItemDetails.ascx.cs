using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.WebAppLib;
using Vevo.WebUI;

public partial class Components_OptionItemDetails : Vevo.WebUI.International.BaseLanguageUserControl
{
    public string OptionGroupID
    {
        get
        {
            if (ViewState["OptionGroupID"] == null)
                return "0";
            else
                return (string) ViewState["OptionGroupID"];
        }
        set
        {
            ViewState["OptionGroupID"] = value;
        }
    }

    public string ProductID
    {
        get { return ViewState["OptionItemProductID"].ToString(); }
        set { ViewState["OptionItemProductID"] = value; }
    }

    private string CreateOptionNameHtml( string name )
    {
        return String.Format( "<span class='OptionItemName' >{0}</span>&nbsp;&nbsp;", name );
    }

    private string CreateOptionPriceHtml( string price )
    {
        if (DataAccessContext.Configurations.GetBoolValue( "PriceRequireLogin" ) && !Page.User.Identity.IsAuthenticated)
        {
            return string.Empty;
        }
        
        return String.Format( "<span class='OptionPrice'>(+{0:n2})</span>", price );
    }

    private string CreateOptionImgHtml( string pathImg )
    {
        return String.Format( "<br><img src='{0}' class='OptionImg' />", Page.ResolveUrl( "~/" + pathImg ) );
    }

    private bool IsZeroPrice( string price )
    {
        string result = price.Replace( "%", "" );
        return Convert.ToDecimal( result ) == 0.00m;
    }

    private string CreateOptionItemHtml( string name, string price, string pathImg )
    {
        if (String.IsNullOrEmpty( pathImg ))
        {
            if (IsZeroPrice( price ))
            {
                return CreateOptionNameHtml( name );
            }
            else
                return CreateOptionNameHtml( name ) +
                    CreateOptionPriceHtml(
                    (!price.Contains( "%" )) ?
                    StoreContext.Currency.FormatPrice( Convert.ToDecimal( price ) ) : price );
        }
        else
        {
            if (IsZeroPrice( price ))
            {
                return CreateOptionNameHtml( name ) +
                    CreateOptionImgHtml( pathImg );
            }
            else
                return CreateOptionNameHtml( name ) +
                    CreateOptionPriceHtml(
                    (!price.Contains( "%" )) ?
                    StoreContext.Currency.FormatPrice( Convert.ToDecimal( price ) ) : price ) +
                    CreateOptionImgHtml( pathImg );
        }
    }

    private string GenerateAdditionalPrice( object priceToAdd )
    {
        if (Convert.ToDouble( priceToAdd ) != 0.0)
            return " (+" + StoreContext.Currency.FormatPrice( priceToAdd ) + ")";
        else
            return String.Empty;
    }

    private string GenerateAdditionalPercentage( object percentage )
    {
        if (Convert.ToDouble( percentage ) != 0.0)
            return " (+" + percentage.ToString() + "%)";
        else
            return String.Empty;
    }
    private string GenerateStringOptionPrice( OptionItem row, bool formated )
    {
        string result = String.Empty;
        if (row.OptionItemType == OptionItem.PriceType.Price)
        {
            if (formated)
                result = CreateOptionItemHtml(
                    row.Name,
                    row.PriceToAdd.ToString(),
                    row.ImageFile );
            else
                result = row.Name + GenerateAdditionalPrice( row.PriceToAdd );
        }
        else
        {
            if (formated)
                result = CreateOptionItemHtml(
                    row.Name,
                    row.PercentageChange.ToString() + "%",
                    row.ImageFile );
            else
                result = row.Name + GenerateAdditionalPercentage( row.PercentageChange );
        }
        return result;
    }

    private IList<OptionItemDisplay> PopulateColumnForDisplay( IList<OptionItem> optionItemLists, bool formated )
    {
        IList<OptionItemDisplay> optionItemDisplay = new List<OptionItemDisplay>();
        // Set value in column
        for (int i = 0; i <= optionItemLists.Count - 1; i++)
        {
            OptionItemDisplay option = new OptionItemDisplay( optionItemLists[i] );
            option.PriceUp = GenerateStringOptionPrice( optionItemLists[i], formated );
            optionItemDisplay.Add( option );
        }
        return optionItemDisplay;
    }

    public void PopulateControls()
    {
        OptionGroup optionGruop = DataAccessContext.OptionGroupRepository.GetOne( Culture, OptionGroupID );

        uxOptionGroupLabel.Text = optionGruop.DisplayText;

        IList<OptionItem> optionItemLists = DataAccessContext.OptionItemRepository.GetByOptionGroupID( Culture, OptionGroupID );

        if (optionItemLists.Count == 0)
        {
            uxOptionGroupLabel.Visible = false;
            return;
        }

        IList<OptionItemDisplay> optionItemDisplayList = new List<OptionItemDisplay>();

        switch (optionGruop.Type)
        {
            case OptionGroup.OptionGroupType.Radio:
                RadioTR.Visible = true;
                optionItemDisplayList = PopulateColumnForDisplay( optionItemLists, true );
                uxOptionRadioItem.SetupRadio( optionItemDisplayList );
                break;

            case OptionGroup.OptionGroupType.DropDown:
                DropdownTR.Visible = true;
                optionItemDisplayList = PopulateColumnForDisplay( optionItemLists, false );
                uxOptionDropDownItem.SetUpDropDown( optionItemDisplayList );
                break;

            case OptionGroup.OptionGroupType.Text:
                TextTR.Visible = true;
                uxOptionIDHidden.Value = optionItemLists[0].OptionItemID;
                uxOptionTextItem.OptionLabel = GenerateStringOptionPrice( optionItemLists[0], true );
                break;

            case OptionGroup.OptionGroupType.InputList:
                InputListTR.Visible = true;
                optionItemDisplayList = PopulateColumnForDisplay( optionItemLists, true );
                uxOptionInputListItem.SetupInputList( optionItemDisplayList );
                break;

            case OptionGroup.OptionGroupType.Upload:
                UploadTR.Visible = true;
                uxUploadHiddenField.Value = optionItemLists[0].OptionItemID;
                uxOptionUploadItem.OptionLabel = GenerateStringOptionPrice( optionItemLists[0], true );
                break;

            case OptionGroup.OptionGroupType.UploadRequired:
                UploadRQTR.Visible = true;
                uxUploadRQHiddenField.Value = optionItemLists[0].OptionItemID;
                uxOptionUploadRequireItem.OptionLabel = GenerateStringOptionPrice( optionItemLists[0], true );
                break;
        }

    }

    private DataTable AddColumn( DataTable table, string name, string type )
    {
        // Create a DataColumn and set various properties. 
        DataColumn column = new DataColumn();
        column.DataType = System.Type.GetType( type );
        column.AllowDBNull = true;
        column.Caption = name;
        column.ColumnName = name;

        // Add the column to the table. 
        table.Columns.Add( column );
        return table;
    }

    public string CultureID
    {
        get
        {
            if (ViewState["CultureID"] == null)
                return CultureUtilities.StoreCultureID;
            else
                return (string) ViewState["CultureID"];
        }
        set
        {
            ViewState["CultureID"] = value;
        }
    }

    public Culture Culture
    {
        get
        {
            if (ViewState["Culture"] == null)
                return StoreContext.Culture;
            else
                return (Culture) ViewState["Culture"];
        }
        set
        {
            ViewState["Culture"] = value;
        }
    }

    public bool IsValidInput()
    {
        bool result = false;
        OptionGroup optionGruop = DataAccessContext.OptionGroupRepository.GetOne( Culture, OptionGroupID );

        switch ( optionGruop.Type )
        {
            case OptionGroup.OptionGroupType.Radio:
                result = uxOptionRadioItem.ValidateInput();
                uxErrorMessageLabel.Visible = !result;
                break;

            case OptionGroup.OptionGroupType.DropDown:
                result = uxOptionDropDownItem.ValidateInput();
                uxErrorMessageLabel.Visible = !result;
                break;

            case OptionGroup.OptionGroupType.Text:
                result = uxOptionTextItem.ValidateInput();
                break;

            case OptionGroup.OptionGroupType.InputList:
                result = uxOptionInputListItem.ValidateInput();
                break;

            case OptionGroup.OptionGroupType.Upload:
                result = uxOptionUploadItem.ValidateInput();
                break;

            case OptionGroup.OptionGroupType.UploadRequired:
                result = uxOptionUploadRequireItem.ValidateInput();
                break;
            default:
                result = true;
                break;
        }

        //if (TextTR.Visible)
        //{
        //    result = uxOptionTextItem.ValidateInput();
        //}
        //else if (InputListTR.Visible)
        //{
        //    result = uxOptionInputListItem.ValidateInput();
        //}
        //else if (RadioTR.Visible)
        //{
        //    result = uxOptionRadioItem.ValidateInput();
        //    uxErrorMessageLabel.Visible = !result;
        //}
        //else if (DropdownTR.Visible)
        //{
        //    result = uxOptionDropDownItem.ValidateInput();
        //    uxErrorMessageLabel.Visible = !result;
        //}
        //else if (UploadTR.Visible)
        //{
        //    result = uxOptionUploadItem.ValidateInput();
        //}
        //else if (UploadRQTR.Visible)
        //{
        //    result = uxOptionUploadRequireItem.ValidateInput();
        //}
        //else
        //    result = true;

        return result;
    }

    public bool IsOptionUseStock()
    {
        if (DataAccessContext.OptionGroupRepository.IsUseStock( ProductID, OptionGroupID ))
            return true;
        else
            return false;
    }

    public OptionItemValue[] GetSelectedItem()
    {
        ArrayList selectedList = new ArrayList();

        bool useStock;
        if (DataAccessContext.OptionGroupRepository.IsUseStock( ProductID, OptionGroupID ))
            useStock = true;
        else
            useStock = false;

        OptionGroup optionGruop = DataAccessContext.OptionGroupRepository.GetOne( Culture, OptionGroupID );

        switch ( optionGruop.Type )
        {
            case OptionGroup.OptionGroupType.Radio:
                uxOptionRadioItem.CreateOption( selectedList, useStock );
                break;

            case OptionGroup.OptionGroupType.DropDown:
                uxOptionDropDownItem.CreateOption( selectedList, useStock );
                break;

            case OptionGroup.OptionGroupType.Text:
                uxOptionTextItem.CreateOption( selectedList, uxOptionIDHidden.Value, useStock );
                break;

            case OptionGroup.OptionGroupType.InputList:
                uxOptionInputListItem.CreateOption( selectedList, useStock );
                break;

            case OptionGroup.OptionGroupType.Upload:
                uxOptionUploadItem.CreateOption( selectedList, uxUploadHiddenField.Value, useStock );
                break;

            case OptionGroup.OptionGroupType.UploadRequired:
                uxOptionUploadRequireItem.CreateOption( selectedList, uxUploadRQHiddenField.Value, useStock );
                break;
        }

        //if (RadioTR.Visible)
        //{
        //    uxOptionRadioItem.CreateOption( selectedList, useStock );
        //}

        //if (DropdownTR.Visible)
        //{
        //    uxOptionDropDownItem.CreateOption( selectedList, useStock );
        //}

        //if (TextTR.Visible)
        //{
        //    uxOptionTextItem.CreateOption( selectedList, uxOptionIDHidden.Value, useStock );
        //}

        //if (InputListTR.Visible)
        //{
        //    uxOptionInputListItem.CreateOption( selectedList, useStock );
        //}

        //if (UploadTR.Visible)
        //{
        //    uxOptionUploadItem.CreateOption( selectedList, uxUploadHiddenField.Value, useStock );
        //}

        //if (UploadRQTR.Visible)
        //{
        //    uxOptionUploadRequireItem.CreateOption( selectedList, uxUploadRQHiddenField.Value, useStock );
        //}

        OptionItemValue[] result = new OptionItemValue[selectedList.Count];
        selectedList.CopyTo( result );

        return result;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }

    public void SetValidGroup( string groupName )
    {
        uxOptionDropDownItem.SetValidGroup( groupName );
        uxOptionRadioItem.SetValidGroup( groupName );
        uxOptionInputListItem.SetValidGroup( groupName );
    }
}
