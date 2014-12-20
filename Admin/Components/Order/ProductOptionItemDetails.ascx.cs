using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.Domain.Products;
using System.Collections.Generic;
using Vevo.Domain.Orders;
using Vevo.Domain.Stores;

public partial class Admin_Components_Order_ProductOptionItemDetails : AdminAdvancedBaseUserControl
{
    public OptionItemValueCollection SelectedItemValue
    {
        get
        {
            if (ViewState["SelectedItemValue"] == null)
                return new OptionItemValueCollection();
            else
                return (OptionItemValueCollection) ViewState["SelectedItemValue"];
        }
        set
        {
            ViewState["SelectedItemValue"] = value;
        }
    }

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

        return String.Format( "<br><img src='{0}' class='OptionImg' />", Page.ResolveUrl( "../" + pathImg ) );
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
                    Currency.FormatPrice( Convert.ToDecimal( price ) ) : price );
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
                    Currency.FormatPrice( Convert.ToDecimal( price ) ) : price ) +
                    CreateOptionImgHtml( pathImg );
        }
    }

    private string GenerateAdditionalPrice( object priceToAdd )
    {
        if (Convert.ToDouble( priceToAdd ) != 0.0)
            return " (+" + Currency.FormatPrice( priceToAdd ) + ")";
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

    private OptionItemValue GetSelectedItem( OptionGroup optionGruop, OptionGroup.OptionGroupType type )
    {
        foreach (OptionItemValue item in SelectedItemValue)
        {
            if (item.OptionItem.OptionGroupID == optionGruop.OptionGroupID)
                return item;
        }
        return null;

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
                uxOptionRadioItem.SetupRadio( optionItemDisplayList, GetSelectedItem( optionGruop, OptionGroup.OptionGroupType.Radio ) );
                break;

            case OptionGroup.OptionGroupType.DropDown:
                DropdownTR.Visible = true;
                optionItemDisplayList = PopulateColumnForDisplay( optionItemLists, false );
                uxOptionDropDownItem.SetUpDropDown( optionItemDisplayList, GetSelectedItem( optionGruop, OptionGroup.OptionGroupType.DropDown ) );
                break;

            case OptionGroup.OptionGroupType.Text:
                TextTR.Visible = true;
                uxOptionIDHidden.Value = optionItemLists[0].OptionItemID;
                uxOptionTextItem.OptionLabel
                    = GenerateStringOptionPrice( optionItemLists[0], true );
                uxOptionTextItem.SetUpText( GetSelectedItem( optionGruop, OptionGroup.OptionGroupType.Text ) );
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

    public string StoreID
    {
        get
        {
            if (ViewState["StoreID"] == null)
                return Store.RegularStoreID;
            else
                return (string) ViewState["StoreID"];
        }
        set
        {
            ViewState["StoreID"] = value;
        }
    }

    public Currency Currency
    {
        get
        {
            return DataAccessContext.CurrencyRepository.GetOne( CurrencyCode );
        }
    }

    public string CurrencyCode
    {
        get
        {
            if (ViewState["CurrencyCode"] == null)
                return DataAccessContext.CurrencyRepository.GetOne(
                    DataAccessContext.Configurations.GetValueNoThrow( "DefaultDisplayCurrencyCode",
                    DataAccessContext.StoreRepository.GetOne( StoreID ) ) ).CurrencyCode;
            else
                return (string) ViewState["CurrencyCode"];
        }
        set
        {
            ViewState["CurrencyCode"] = value;
        }
    }

    public bool IsValidInput()
    {
        bool result = false;
        if (TextTR.Visible)
        {
            result = uxOptionTextItem.ValidateInput();
        }
        else if (InputListTR.Visible)
        {
            result = uxOptionInputListItem.ValidateInput();
        }
        else if (RadioTR.Visible)
        {
            result = uxOptionRadioItem.ValidateInput();
        }
        else if (DropdownTR.Visible)
        {
            result = uxOptionDropDownItem.ValidateInput();
        }
        else if (UploadTR.Visible)
        {
            result = uxOptionUploadItem.ValidateInput();
        }
        else if (UploadRQTR.Visible)
        {
            result = uxOptionUploadRequireItem.ValidateInput();
        }
        else
            result = true;

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

        if (RadioTR.Visible)
        {
            uxOptionRadioItem.CreateOption( selectedList, useStock );
        }

        if (DropdownTR.Visible)
        {
            uxOptionDropDownItem.CreateOption( selectedList, useStock );
        }

        if (TextTR.Visible)
        {
            uxOptionTextItem.CreateOption( selectedList, uxOptionIDHidden.Value, useStock );
        }

        if (InputListTR.Visible)
        {
            uxOptionInputListItem.CreateOption( selectedList, useStock );
        }

        if (UploadTR.Visible)
        {
            uxOptionUploadItem.CreateOption( selectedList, uxUploadHiddenField.Value, useStock );
        }

        if (UploadRQTR.Visible)
        {
            uxOptionUploadRequireItem.CreateOption( selectedList, uxUploadRQHiddenField.Value, useStock );
        }

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
