using System;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Shipping;
using Vevo.Domain.Shipping.Custom;
using Vevo.Shared.Utilities;

public partial class AdminAdvanced_Components_ShippingDetails : AdminAdvancedBaseUserControl
{
    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string ShippingID
    {
        get
        {
            if (string.IsNullOrEmpty( MainContext.QueryString["ShippingID"] ))
                return "0";
            else
                return MainContext.QueryString["ShippingID"];
        }
    }

    private ShippingOptionType SelectedShippingOptionType
    {
        get
        {
            string id = DataAccessContext.ShippingOptionTypeRepository.GetIDByName(
                MainContext.QueryString["ShippingOption"] );

            return DataAccessContext.ShippingOptionTypeRepository.GetOne( id );
        }
    }

    private string CurrentShippingName
    {
        get { return ViewState["currentShippingName"].ToString(); }
        set { ViewState["currentShippingName"] = value; }
    }

    private void FreeValueControl()
    {
        if (ConvertUtilities.ToBoolean( uxFreeShippingDrop.SelectedValue ))
            FreeValueTR.Visible = true;
        else
            FreeValueTR.Visible = false;
    }

    private ShippingOption.ShippingZoneAllowedType GetAllowedType()
    {
        string type = String.Empty;
        type = uxSelectedAllowedTypeRadio.SelectedValue;
        return (ShippingOption.ShippingZoneAllowedType) Enum.Parse(
            typeof( ShippingOption.ShippingZoneAllowedType ), uxSelectedAllowedTypeRadio.SelectedValue );
    }

    private ShippingOption SetUpShippingOption( ShippingOption shippingOption )
    {
        shippingOption.ShippingName = uxShippingNameText.Text;
        shippingOption.FreeShipping = ConvertUtilities.ToBoolean( uxFreeShippingDrop.SelectedValue );
        shippingOption.FreeShippingValue = ConvertUtilities.ToDecimal( uxFreeShippingValueText.Text );
        shippingOption.FixedShippingCost = ConvertUtilities.ToDecimal( uxFixCostText.Text );
        shippingOption.ShippingCostFirstItem = ConvertUtilities.ToDecimal( uxFirstItemText.Text );
        shippingOption.ShippingCostNextItem = ConvertUtilities.ToDecimal( uxNextItemText.Text );
        shippingOption.IsEnabled = ConvertUtilities.ToBoolean( uxIsEnabledDrop.SelectedValue );
        shippingOption.HandlingFee = ConvertUtilities.ToDecimal( uxHandlingFeeText.Text );
        shippingOption.AllowedType = GetAllowedType();
        return shippingOption;
    }

    private ShippingOption SetUpShippingZones( ShippingOption shippingOption )
    {
        shippingOption.ShippingZone.Clear();

        if (uxSelectedAllowedTypeRadio.SelectedValue != ShippingOption.ShippingZoneAllowedType.Worldwide.ToString())
        {
            string[] zoneGroupIDs = uxMultiZones.ConvertToZoneGroupIDs();

            foreach (string id in zoneGroupIDs)
            {
                shippingOption.ShippingZone.Add( new ShippingZone( id ) );
            }
        }
        return shippingOption;
    }

    private void AddShipping()
    {
        ShippingOption shippingOption = new ShippingOption( uxLanguageControl.CurrentCulture );
        shippingOption = SetUpShippingOption( shippingOption );
        shippingOption = SetUpShippingZones( shippingOption );
        shippingOption.ShippingOptionTypeID = SelectedShippingOptionType.ShippingOptionTypeID;
        shippingOption = DataAccessContext.ShippingOptionRepository.Save( shippingOption );
        AddUnlimitedNumberShippingWeightRate( shippingOption );
        AddUnlimitedNumberShippingOrderTotalRate( shippingOption );
    }

    private void UpdateShipping()
    {
        ShippingOption shippingOption = DataAccessContext.ShippingOptionRepository.GetOne( uxLanguageControl.CurrentCulture, ShippingID );
        shippingOption = SetUpShippingOption( shippingOption );
        shippingOption = SetUpShippingZones( shippingOption );
        shippingOption = DataAccessContext.ShippingOptionRepository.Save( shippingOption );
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private string GetDisplayShippingOption( string typeName )
    {
        switch (typeName)
        {
            case FixedShippingMethod.ShippingOptionTypeName:
                return "Fixed Cost";
            case PerItemShippingMethod.ShippingOptionTypeName:
                return "By Quantity";
            case ByWeightShippingMethod.ShippingOptionTypeName:
                return "By Weight";
            case ByOrderTotalShippingMethod.ShippingOptionTypeName:
                return "By Order Total";
            default:
                return typeName;
        }
    }

    private void AddUnlimitedNumberShippingWeightRate( ShippingOption shippingOption )
    {
        if (shippingOption.ShippingOptionType.TypeName == ByWeightShippingMethod.ShippingOptionTypeName)
        {
            ShippingWeightRate shippingWeightRate = new ShippingWeightRate();
            shippingWeightRate.ShippingID = shippingOption.ShippingID;
            shippingWeightRate.ToWeight = SystemConst.UnlimitedNumber;
            shippingWeightRate.WeightRate = 0;
            DataAccessContext.ShippingWeightRateRepository.Save( shippingWeightRate );
        }
    }

    private void AddUnlimitedNumberShippingOrderTotalRate( ShippingOption shippingOption )
    {
        if (shippingOption.ShippingOptionType.TypeName == ByOrderTotalShippingMethod.ShippingOptionTypeName)
        {
            ShippingOrderTotalRate shippingOrderTotalRate = new ShippingOrderTotalRate();
            shippingOrderTotalRate.ShippingID = shippingOption.ShippingID;
            shippingOrderTotalRate.ToOrderTotal = SystemConst.UnlimitedNumberDecimal;
            shippingOrderTotalRate.OrderTotalRate = 0;
            DataAccessContext.ShippingOrderTotalRateRepository.Save( shippingOrderTotalRate );
        }
    }

    private void PopulateShippingAllowedType( ShippingOption.ShippingZoneAllowedType type )
    {
        switch (type)
        {
            case ShippingOption.ShippingZoneAllowedType.Deny:
                {
                    uxSelectedAllowedTypeRadio.SelectedValue = type.ToString();
                    uxSelectedZonesPanel.Visible = true;
                    uxSelectedMultiZonesPanel.Visible = true;
                    break;
                }
            case ShippingOption.ShippingZoneAllowedType.Allow:
                {
                    uxSelectedAllowedTypeRadio.SelectedValue = type.ToString();
                    uxSelectedZonesPanel.Visible = true;
                    uxSelectedMultiZonesPanel.Visible = true;
                    break;
                }
            default:
                {
                    uxSelectedZonesPanel.Visible = false;
                    uxSelectedAllowedTypeRadio.SelectedValue = type.ToString();
                    uxSelectedMultiZonesPanel.Visible = false;
                    break;
                }
        }

    }

    private void PopulateControls()
    {
        if (ShippingID != null &&
            int.Parse( ShippingID ) > 0)
        {
            ShippingOption shippingOption = DataAccessContext.ShippingOptionRepository.GetOne( uxLanguageControl.CurrentCulture, ShippingID );
            CurrentShippingName = shippingOption.ShippingName;
            uxShippingNameText.Text = shippingOption.ShippingName;
            uxFreeShippingDrop.SelectedValue = shippingOption.FreeShipping.ToString();
            uxFreeShippingValueText.Text = String.Format( "{0:f2}", shippingOption.FreeShippingValue );
            uxShippingOption.Text = GetDisplayShippingOption( shippingOption.ShippingOptionType.TypeName );
            uxHandlingFeeText.Text = String.Format( "{0:f2}", shippingOption.HandlingFee );
            uxIsEnabledDrop.SelectedValue = shippingOption.IsEnabled.ToString();
            uxFixCostText.Text = String.Format( "{0:f2}", shippingOption.FixedShippingCost );
            uxFirstItemText.Text = String.Format( "{0:f2}", shippingOption.ShippingCostFirstItem );
            uxNextItemText.Text = String.Format( "{0:f2}", shippingOption.ShippingCostNextItem );
            PopulateShippingOption( shippingOption.ShippingOptionType.TypeName );
            PopulateShippingAllowedType( shippingOption.AllowedType );
        }
        FreeValueControl();
    }

    private void PopulateShippingOption( string typeName )
    {
        switch (typeName)
        {
            case FixedShippingMethod.ShippingOptionTypeName:
                uxFixedCostTR.Visible = true;
                uxPerItemTR.Visible = false;
                uxByWeightTR.Visible = false;
                uxByOrderTotalTR.Visible = false;
                break;
            case PerItemShippingMethod.ShippingOptionTypeName:
                uxFixedCostTR.Visible = false;
                uxPerItemTR.Visible = true;
                uxByWeightTR.Visible = false;
                uxByOrderTotalTR.Visible = false;
                break;
            case ByWeightShippingMethod.ShippingOptionTypeName:
                uxFixedCostTR.Visible = false;
                uxPerItemTR.Visible = false;
                uxByWeightTR.Visible = true;
                uxByOrderTotalTR.Visible = false;
                break;
            case ByOrderTotalShippingMethod.ShippingOptionTypeName:
                uxFixedCostTR.Visible = false;
                uxPerItemTR.Visible = false;
                uxByWeightTR.Visible = false;
                uxByOrderTotalTR.Visible = true;
                break;
        }
        PopulateShippingWeightRateLink( typeName );
        PopulateShippingOrderTotalRateLink( typeName );
    }

    private void PopulateShippingWeightRateLink( string typeName )
    {
        if (IsEditMode() && typeName == ByWeightShippingMethod.ShippingOptionTypeName)
            uxAddWeightRateButton.Visible = true;
        else
            uxAddWeightRateButton.Visible = false;
    }

    private void PopulateShippingOrderTotalRateLink( string typeName )
    {
        if (IsEditMode() && typeName == ByOrderTotalShippingMethod.ShippingOptionTypeName)
            uxAddOrderTotalRateButton.Visible = true;
        else
            uxAddOrderTotalRateButton.Visible = false;
    }

    private void InitMultiZonesControl()
    {
        if (!MainContext.IsPostBack)
        {
            uxMultiZones.SetupDropDownList( ShippingID, uxLanguageControl.CurrentCulture, false );
        }
        else
        {
            uxMultiZones.SetupDropDownList( ShippingID, uxLanguageControl.CurrentCulture, true );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxLanguageControl.BubbleEvent += new EventHandler( Details_RefreshHandler );

        uxHandlingFeeTR.Visible = DataAccessContext.Configurations.GetBoolValue( "HandlingFeeEnabled" );
        uxSelectedAllowedTypeRadio.SelectedValue = ShippingOption.ShippingZoneAllowedType.Worldwide.ToString();
        uxSelectedZonesPanel.Visible = false;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (IsEditMode())
        {
            if (!MainContext.IsPostBack)
            {
                PopulateControls();
            }

            if (IsAdminModifiable())
            {
                uxUpdateButton.Visible = true;
            }
            else
            {
                uxUpdateButton.Visible = false;
            }

            uxAddButton.Visible = false;
        }
        else
        {
            if (IsAdminModifiable())
            {

                uxAddButton.Visible = true;
                uxUpdateButton.Visible = false;
            }
            else
            {
                MainContext.RedirectMainControl( "ShippingList.ascx", "" );
            }
            PopulateShippingOption( SelectedShippingOptionType.TypeName );
            uxShippingOption.Text = GetDisplayShippingOption( SelectedShippingOptionType.TypeName );
        }

        FreeValueControl();
        InitMultiZonesControl();
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        if (!Page.IsValid)
            return;

        string shippingID = DataAccessContext.ShippingOptionRepository.GetIDFromName( uxShippingNameText.Text );

        if (shippingID != "0")
        {
            uxMessage.DisplayError( Resources.ShippingMessages.AddDuplicatedError );
            return;
        }

        if (uxSelectedAllowedTypeRadio.SelectedValue != ShippingOption.ShippingZoneAllowedType.Worldwide.ToString()
            && uxMultiZones.ConvertToZoneGroupIDs().Length == 0)
        {
            uxMessage.DisplayError( Resources.ShippingMessages.AddErrorZoneCannotEmpty );
            return;
        }

        try
        {
            AddShipping();
            uxMultiZones.Clear();
            MainContext.RedirectMainControl( "ShippingList.ascx", "" );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        if (!Page.IsValid)
            return;

        string shippingID = DataAccessContext.ShippingOptionRepository.GetIDFromName( uxShippingNameText.Text );

        if (shippingID != "0" && CurrentShippingName != uxShippingNameText.Text.Trim())
        {
            uxMessage.DisplayError( Resources.ShippingMessages.AddDuplicatedError );
            return;
        }

        if (uxSelectedAllowedTypeRadio.SelectedValue != ShippingOption.ShippingZoneAllowedType.Worldwide.ToString()
            && uxMultiZones.ConvertToZoneGroupIDs().Length == 0)
        {
            uxMessage.DisplayError( Resources.ShippingMessages.AddErrorZoneCannotEmpty );
            return;
        }

        try
        {
            UpdateShipping();

            uxMessage.DisplayMessage( Resources.ShippingMessages.UpdateSuccess );
            PopulateControls();

        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxAddWeightRateButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "ShippingWeightRate.ascx", String.Format( "ShippingID={0}", ShippingID ) );
    }

    protected void uxAddOrderTotalRateButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "ShippingOrderTotalRate.ascx", String.Format( "ShippingID={0}", ShippingID ) );
    }

    protected void uxSelectedAllowedTypeRadio_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxSelectedAllowedTypeRadio.SelectedValue != ShippingOption.ShippingZoneAllowedType.Worldwide.ToString())
        {
            uxSelectedZonesPanel.Visible = true;
            uxSelectedMultiZonesPanel.Visible = true;
        }
        else
        {
            uxSelectedZonesPanel.Visible = false;
            uxSelectedMultiZonesPanel.Visible = false;
        }
    }

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }
}
