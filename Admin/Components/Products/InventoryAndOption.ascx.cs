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
using Vevo.Domain.Products;
using Vevo.Domain;
using Vevo.WebUI;
using System.Collections.Generic;
using Vevo.Shared.Utilities;
using Vevo.Domain.Stores;

public partial class AdminAdvanced_Components_Products_InventoryAndOption : AdminAdvancedBaseUserControl
{
    #region private

    private const int _optionStock = 1;
    private bool _isEditMode = false;
    private const int DefaultStockNumber = 10;

    private string CurrentID
    {
        get
        {
            if (string.IsNullOrEmpty( MainContext.QueryString["ProductID"] ))
                return "0";
            else
                return MainContext.QueryString["ProductID"];
        }
    }

    private CheckBox GetUseOptionCheck( GridViewRow row )
    {
        return (CheckBox) row.FindControl( "uxUseOptionCheck" );
    }

    private CheckBox GetStockCheck( GridViewRow row )
    {
        return (CheckBox) row.FindControl( "uxUseStockCheck" );
    }

    private HiddenField GetOptionGroupIDHidden( GridViewRow row )
    {
        return (HiddenField) row.FindControl( "uxOptionGroupIDHidden" );
    }

    private void ClearUseStockOption()
    {
        foreach (GridViewRow row in uxOptionGroupGrid.Rows)
        {
            CheckBox useStockCheck = GetStockCheck( row );
            useStockCheck.Checked = false;
        }
    }

    private void ClearUseOptionCheck()
    {
        foreach (GridViewRow row in uxOptionGroupGrid.Rows)
        {
            CheckBox useOptionCheck = GetUseOptionCheck( row );
            useOptionCheck.Checked = false;
        }
    }
    private string[] GetOptionGroupIDsWithStockEnabled()
    {
        ArrayList stockoptionList = new ArrayList();

        foreach (GridViewRow row in uxOptionGroupGrid.Rows)
        {
            CheckBox useOptionCheck = GetUseOptionCheck( row );
            CheckBox useStockCheck = GetStockCheck( row );
            if (useStockCheck.Checked)
            {
                useOptionCheck.Checked = true;
                HiddenField uxOptionGroupIDHidden = GetOptionGroupIDHidden( row );
                stockoptionList.Add( uxOptionGroupIDHidden.Value );
            }
        }

        string[] result = new string[stockoptionList.Count];
        stockoptionList.CopyTo( result );

        return result;
    }

    private DataTable CurrentStockOption
    {
        get
        {
            if (ViewState["CurrentStockOption"] == null)
                return null;
            else
                return (DataTable) ViewState["CurrentStockOption"];
        }
        set
        {
            ViewState["CurrentStockOption"] = value;
        }
    }

    private CheckBox GetEnabledCheck( int row )
    {
        return (CheckBox) uxStockOptionGrid.Rows[row].FindControl( "uxStockEnabledCheck" );
    }


    protected bool IsStockOptionVisible( string type )
    {
        type = type.ToLower();
        if (type == "text" || type == "upload" || type == "uploadrequired")
            return false;
        else
            return true;
    }

    private void PopulateStockText()
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, CurrentID, new StoreRetriever().GetCurrentStoreID() );

        for (int i = 0; i < uxStockOptionGrid.Rows.Count; i++)
        {
            string[] optionItemIDs = GetOptionItemIDs( i );
            ProductStock stock = product.FindProductStock( optionItemIDs );

            if (stock != null && stock.OptionCombinationID != null)
            {
                TextBox stockText = GetStockOptionText( i );
                HiddenField combinationID = GetOptionCombinationIDHidden( i );
                CheckBox enabledCheck = GetEnabledCheck( i );
                stockText.Text = stock.Stock.ToString();
                combinationID.Value = stock.OptionCombinationID;
                enabledCheck.Checked = stock.StockEnabled;
            }
        }
    }

    private string[] GetOptionItemIDs( int rowIndex )
    {
        ArrayList arOption = new ArrayList();
        DataRow row = CurrentStockOption.Rows[rowIndex];
        int totalCol = CurrentStockOption.Columns.Count - 1;

        for (int i = 0; i < totalCol; i = i + 2)
        {
            arOption.Add( row[i].ToString() );
        }

        string[] result = new string[arOption.Count];
        arOption.CopyTo( result );
        return result;
    }

    private TextBox GetStockOptionText( GridViewRow row )
    {
        return (TextBox) row.FindControl( "uxStockOptionText" );
    }

    private TextBox GetStockOptionText( int row )
    {
        return (TextBox) uxStockOptionGrid.Rows[row].FindControl( "uxStockOptionText" );
    }

    private HiddenField GetOptionCombinationIDHidden( int row )
    {
        return (HiddenField) uxStockOptionGrid.Rows[row].FindControl( "uxOptionCombinationIDHidden" );
    }

    private bool IsSelectedOptionGroupChanging()
    {
        bool result = false;
        string[] currentSelect = GetOptionGroup();
        if (currentSelect.Length != OriginalOptionGroup.Length)
        {
            result = true;
        }
        else
        {
            for (int i = 0; i < currentSelect.Length; i++)
            {
                if (currentSelect[i] != OriginalOptionGroup[i])
                    result = true;
            }
        }
        return result;
    }

    private string[] OriginalOptionGroup
    {
        get { return (string[]) ViewState["OriginalOptionGroup"]; }
        set { ViewState["OriginalOptionGroup"] = value; }
    }


    private DataTable CreateTableNewStock( string[] optionGroupIDs )
    {
        return DataAccessContext.ProductRepository.GetStockOptionLine( CurrentCulture, optionGroupIDs );
    }


    private string[] GetOptionGroup()
    {
        ArrayList arOptionGroup = new ArrayList();
        foreach (GridViewRow row in uxOptionGroupGrid.Rows)
        {
            HiddenField optionGroupHidden = GetOptionGroupIDHidden( row );
            CheckBox useOption = GetUseOptionCheck( row );
            CheckBox useStock = GetStockCheck( row );

            if (useOption.Checked && useStock.Checked)
            {
                arOptionGroup.Add( optionGroupHidden.Value );
            }
        }

        string[] result = new string[arOptionGroup.Count];
        arOptionGroup.CopyTo( result );
        return result;
    }


    private IList<OptionGroup> OptionGroupSource()
    {
        IList<OptionGroup> optionGroupList = DataAccessContext.OptionGroupRepository.GetAll( CurrentCulture );
        IList<OptionGroup> list = new List<OptionGroup>();
        return optionGroupList;
    }

    private int GetStock()
    {
        return ConvertUtilities.ToInt32( uxStockText.Text );
    }

    #endregion

    #region protected

    protected void uxUseInventory_CheckedChanged( object sender, EventArgs e )
    {
        PopulateStockOptionControl();
    }

    protected void uxUseStockCheck_CheckedChanged( object sender, EventArgs e )
    {
        PopulateStockOptionControl();
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }


    protected string CreateStockRequiredMessage( string name )
    {
        return String.Format(
            GetLocalResourceObject( "uxStockOptionRequiredValidator.ErrorMessage" ).ToString(),
            name );
    }

    protected string CreateStockCompareMessage( string name )
    {
        return String.Format(
            GetLocalResourceObject( "uxStockOptionCompareValidator.ErrorMessage" ).ToString(),
            name );
    }
    #endregion

    #region public methods

    public Culture CurrentCulture
    {
        get
        {
            if (ViewState["Culture"] == null)
                return null;
            else
                return (Culture) ViewState["Culture"];
        }
        set
        {
            ViewState["Culture"] = value;
        }
    }

    public bool IsUseInventory
    {
        get
        {
            return uxUseInventory.Checked;
        }
        set
        {
            uxUseInventory.Checked = value;
        }
    }

    public void HideStockOption()
    {
        uxStockOptionTR.Visible = false;
    }

    public void PopulateStockOptionControl()
    {
        PopulateStockVisibility();

        if (uxUseInventory.Checked && DataAccessContext.Configurations.GetBoolValue( "UseStockControl" ))
        {
            string[] stockoptionList = GetOptionGroupIDsWithStockEnabled();
            if (stockoptionList.Length > 0)
            {
                CurrentStockOption = CreateTableNewStock( stockoptionList );

                uxStockOptionGrid.DataSource = CurrentStockOption;
                uxStockOptionGrid.DataBind();

                if (IsEditMode && !IsSelectedOptionGroupChanging())
                    PopulateStockText();
            }
        }
    }

    public void PopulateStockVisibility()
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "UseStockControl" ))
            uxUseInventoryTR.Visible = false;

        if (uxUseInventory.Checked && DataAccessContext.Configurations.GetBoolValue( "UseStockControl" ))
        {
            string[] stockoptionList = GetOptionGroupIDsWithStockEnabled();
            if (stockoptionList.Length > 0)
            {
                uxStockTR.Visible = false;
                uxStockOptionTR.Visible = true;
                uxStockOptionGrid.Visible = true;
            }
            else
            {
                uxStockOptionTR.Visible = false;
                uxStockTR.Visible = true;
                uxStockOptionGrid.Visible = false;
            }

            uxOptionGroupGrid.Columns[_optionStock].HeaderStyle.CssClass = "GridHeadStyle1 th";
            uxOptionGroupGrid.Columns[_optionStock].ItemStyle.CssClass = "GridHeadStyle1 th";
        }
        else
        {
            ClearUseStockOption();
            uxStockOptionTR.Visible = false;
            uxStockTR.Visible = false;
            uxStockOptionGrid.Visible = false;

            uxOptionGroupGrid.Columns[_optionStock].HeaderStyle.CssClass = "UseStockOptionGridDisabled";
            uxOptionGroupGrid.Columns[_optionStock].ItemStyle.CssClass = "UseStockOptionGridDisabled";
        }
    }
    public void ClearInputFields()
    {
        uxStockText.Text = "";
        uxUseInventory.Checked = DataAccessContext.Configurations.GetBoolValue( "UseStockControl" );

        uxOptionGroupGrid.Columns[_optionStock].HeaderStyle.CssClass = "GridHeadStyle1 th";
        uxOptionGroupGrid.Columns[_optionStock].ItemStyle.CssClass = "GridHeadStyle1 th";

        ClearUseStockOption();
        ClearUseOptionCheck();
    }

    public void PopulateControls( Product product )
    {
        uxStockText.Text = product.SumStock.ToString();
        uxUseInventory.Checked = product.UseInventory;
    }

    public void SetUpDisplay()
    {
        if (uxOptionGroupGrid.Rows.Count > 0)
        {
            if (!uxUseInventory.Checked)
            {
                uxOptionGroupGrid.Columns[_optionStock].HeaderStyle.CssClass = "UseStockOptionGridDisabled";
                uxOptionGroupGrid.Columns[_optionStock].ItemStyle.CssClass = "UseStockOptionGridDisabled";
            }
        }
    }

    public void SetOptionList()
    {
        IList<OptionGroup> list = OptionGroupSource();

        if (list.Count > 0)
        {
            uxOptionGroupGrid.DataSource = list;
            uxOptionGroupGrid.DataBind();
        }
        else
        {
            uxProductOptionTR.Visible = false;
        }
    }

    public void IsProductOptionEnabled( bool isEnabled )
    {
        if (!isEnabled)
        {
            uxOptionGroupTR.Enabled = false;
        }
        else
        {
            uxOptionGroupTR.Enabled = true;
        }
    }

    public void IsProductOptionVisible( bool isVisible )
    {
        if (!isVisible)
        {
            uxProductOptionTR.Visible = false;
            ClearUseOptionCheck();
            ClearUseStockOption();
        }
        else
        {
            uxProductOptionTR.Visible = true;
        }
    }

    public void IsStockOptionVisible( bool isVisible )
    {
        if (!isVisible)
        {
            uxStockOptionTR.Visible = false;
            ClearUseOptionCheck();
            ClearUseStockOption();
            if (uxUseInventory.Checked)
                uxStockTR.Visible = true;
        }
        else
        {
            PopulateStockVisibility();
        }
    }
    public void SelectOptionList( Product product )
    {
        ArrayList arOriginalOptionGroup = new ArrayList();

        foreach (GridViewRow row in uxOptionGroupGrid.Rows)
        {
            foreach (ProductOptionGroup productOptionGroup in product.ProductOptionGroups)
            {
                HiddenField optionGroupHidden = GetOptionGroupIDHidden( row );
                if (optionGroupHidden.Value == productOptionGroup.OptionGroupID)
                {
                    CheckBox useOption = GetUseOptionCheck( row );
                    useOption.Checked = true;

                    CheckBox useStock = GetStockCheck( row );
                    if (productOptionGroup.UseStock)
                    {
                        useStock.Checked = true;
                        arOriginalOptionGroup.Add( productOptionGroup.OptionGroupID );
                    }

                    break;
                }
            }
        }

        string[] result = new string[arOriginalOptionGroup.Count];
        arOriginalOptionGroup.CopyTo( result );
        OriginalOptionGroup = result;
    }

    public bool VerifyInputListOption()
    {
        bool result = true;
        int countOption = 0;

        foreach (GridViewRow item in uxOptionGroupGrid.Rows)
        {
            OptionGroup optionGroup = DataAccessContext.OptionGroupRepository.GetOne(
                CurrentCulture, GetOptionGroupIDHidden( item ).Value );

            if (optionGroup.Type == OptionGroup.OptionGroupType.InputList && GetUseOptionCheck( item ).Checked)
            {
                countOption++;
                if (countOption > 1)
                {
                    result = false;
                    break;
                }
            }
        }
        return result;
    }

    public void AddOptionGroup( Product product )
    {
        foreach (GridViewRow row in uxOptionGroupGrid.Rows)
        {
            CheckBox useOption = GetUseOptionCheck( row );

            if (useOption.Checked)
            {
                CheckBox useStock = GetStockCheck( row );
                HiddenField optionGroupIDHidden = GetOptionGroupIDHidden( row );

                OptionGroup optionGroup = DataAccessContext.OptionGroupRepository.GetOne(
                    CurrentCulture, optionGroupIDHidden.Value );

                product.AddOptionGroup( optionGroup, useStock.Checked );
            }
        }
    }

    public void CreateStockOption( Product product )
    {
        GridView stockOptionGrid = uxStockOptionGrid;

        if (stockOptionGrid.Visible == false)
            product.SetStock( GetStock() );
        else
            for (int i = 0; i < stockOptionGrid.Rows.Count; i++)
            {
                TextBox uxStock = (TextBox) stockOptionGrid.Rows[i].FindControl( "uxStockOptionText" );
                string[] optionItemIDs = GetOptionItemIDs( i );

                product.SetStock( ConvertUtilities.ToInt32( uxStock.Text ), optionItemIDs );
            }
    }

    public void SetStockWhenAdd()
    {
        if (!uxUseInventory.Checked)
            uxStockText.Text = "0";
    }


    public bool IsEditMode
    {
        set { _isEditMode = value; }
        get { return _isEditMode; }
    }
    #endregion

}
