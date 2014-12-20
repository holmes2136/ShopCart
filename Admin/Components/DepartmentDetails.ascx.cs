using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;

public partial class AdminAdvanced_Components_DepartmentDetails : AdminAdvancedBaseUserControl
{
    private const string _pathUploadDepartment = "Images/Departments/";

    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string CurrentID
    {
        get
        {
            return MainContext.QueryString["DepartmentID"];
        }
    }

    private string RootDepartmentID
    {
        get
        {
            return MainContext.QueryString["RootID"];
        }
    }

    private string ParentDepartmentID
    {
        get
        {
            if (ViewState["ParentDepartmentID"] == null)
                return "0";
            else
                return (string) ViewState["ParentDepartmentID"];
        }
        set
        {
            ViewState["ParentDepartmentID"] = value;
        }
    }

    private bool OriginalIsEnabled
    {
        get
        {
            if (ViewState["OriginalIsEnabled"] == null)
                return uxIsEnabledCheck.Checked;
            else
                return (bool) ViewState["OriginalIsEnabled"];
        }
        set
        {
            ViewState["OriginalIsEnabled"] = value;
        }
    }

    private bool IsEnabledChanged()
    {
        if (uxIsEnabledCheck.Checked != OriginalIsEnabled)
            return true;
        else
            return false;
    }

    private void InitParentDropDownList()
    {
        int selectedIndex = uxParentDrop.SelectedIndex;
        uxParentDrop.Items.Clear();
        uxParentDrop.Items.Add( new ListItem( "Root", RootDepartmentID, true ) );

        IList<Department> departmentList = DataAccessContext.DepartmentRepository.GetByRootIDNoProduct(
            uxLanguageControl.CurrentCulture, RootDepartmentID, "SortOrder", BoolFilter.ShowAll );

        for (int i = 0; i < departmentList.Count; i++)
        {
            if (departmentList[i].DiscountGroupID.ToString() == "0")
                uxParentDrop.Items.Add( new ListItem(
                    departmentList[i].CreateFullDepartmentPath() + " (" + departmentList[i].DepartmentID + ")",
                    departmentList[i].DepartmentID ) );
        }

        if (IsEditMode())
        {
            uxParentDrop.Items.Remove( uxParentDrop.Items.FindByValue( CurrentID ) );
        }

        uxParentDrop.SelectedIndex = selectedIndex;

        if (!String.IsNullOrEmpty( CurrentID ))
        {
            Department department = DataAccessContext.DepartmentRepository.GetOne( uxLanguageControl.CurrentCulture, CurrentID );
            if (!department.IsLeafDepartment())
            {
                uxProductListLayoutPanel.Style["display"] = "none";
                uxLayoutRadioPanel.Style["display"] = "none";
                uxLayoutTypeRadioButtonList.Items[0].Selected = true;
                uxHiddenStatus.Value = "department";
            }
        }
    }

    private void ClearInputFields()
    {
        uxNameText.Text = "";
        uxDescriptionText.Text = "";
        uxImageText.Text = "";
        uxParentDrop.SelectedIndex = 0;
        uxQuantityDiscount.Clear();
        uxOtherOneText.Text = "";
        uxOtherTwoText.Text = "";
        uxOtherThreeText.Text = "";
        uxOtherFourText.Text = "";
        uxOtherFiveText.Text = "";
        uxMetaKeywordText.Text = "";
        uxMetaDescriptionText.Text = "";
        uxIsEnabledCheck.Checked = true;
        uxLayoutOverrideDrop.SelectedValue = "False";
        uxImageDepartmentUpload.ShowControl = false;
        uxImgAlternateTextbox.Text = "";
        uxImgTitleTextbox.Text = "";
        uxIsAnchorCheck.Checked = false;
        uxIsShowNewArrivalCheck.Checked = false;
        uxNewArrivalAmountPanel.Visible = uxIsShowNewArrivalCheck.Checked;
        uxNewArrivalAmountText.Text = "";
    }

    private void PouplateThemControlsInPreRender()
    {
        if (!ConvertUtilities.ToBoolean( uxLayoutOverrideDrop.SelectedValue ))
        {
            uxLayoutOverrideDrop.SelectedValue = "False";
            uxLayoutRadioPanel.Style["display"] = "none";
            uxDepartmentLayoutPanel.Style["display"] = "none";
            uxProductListLayoutPanel.Style["display"] = "none";
        }
        else
        {
            //uxLayoutRadioPanel.Style["display"] = "none";
            if (uxLayoutTypeRadioButtonList.SelectedValue == "Department")
            {
                uxDepartmentLayoutPanel.Style["display"] = "";
                uxProductListLayoutPanel.Style["display"] = "none";
            }
            else
            {
                uxDepartmentLayoutPanel.Style["display"] = "none";
                uxProductListLayoutPanel.Style["display"] = "";
            }

            if (!IsEditMode())
                uxLayoutRadioPanel.Style["display"] = "";
        }
    }

    private void PopulateControls()
    {
        if (CurrentID != null &&
             int.Parse( CurrentID ) >= 0)
        {
            Department department = DataAccessContext.DepartmentRepository.GetOne(
                uxLanguageControl.CurrentCulture, CurrentID );

            uxNameText.Text = department.Name;
            uxDescriptionText.Text = department.Description;
            uxImageText.Text = department.ImageFile;
            uxParentDrop.SelectedValue = department.ParentDepartmentID.ToString();
            uxQuantityDiscount.Refresh( department.DiscountGroupID.ToString() );
            uxIsEnabledCheck.Checked = department.IsEnabled;
            ParentDepartmentID = department.ParentDepartmentID.ToString();
            OriginalIsEnabled = department.IsEnabled;
            uxOtherOneText.Text = department.Other1;
            uxOtherTwoText.Text = department.Other2;
            uxOtherThreeText.Text = department.Other3;
            uxOtherFourText.Text = department.Other4;
            uxOtherFiveText.Text = department.Other5;
            uxMetaKeywordText.Text = department.MetaKeyword;
            uxMetaDescriptionText.Text = department.MetaDescription;
            uxDepartmentLayoutDrop.SelectedValue = department.DepartmentListLayoutPath;
            uxProductListLayoutDrop.SelectedValue = department.ProductListLayoutPath;
            uxImgAlternateTextbox.Text = department.ImageAlt;
            uxImgTitleTextbox.Text = department.ImageTitle;
            uxIsAnchorCheck.Checked = department.IsAnchor;
            uxIsShowNewArrivalCheck.Checked = department.IsShowNewArrival;
            uxNewArrivalAmountPanel.Visible = uxIsShowNewArrivalCheck.Checked;
            uxNewArrivalAmountText.Text = department.NewArrivalAmount.ToString();

            if (String.IsNullOrEmpty( department.DepartmentListLayoutPath ) && String.IsNullOrEmpty( department.ProductListLayoutPath ))
            {
                uxLayoutOverrideDrop.SelectedValue = "False";
                uxLayoutRadioPanel.Style["display"] = "none";
                uxDepartmentLayoutPanel.Style["display"] = "none";
                uxProductListLayoutPanel.Style["display"] = "none";
            }
            else
            {
                uxLayoutRadioPanel.Style["display"] = "none";
                uxLayoutOverrideDrop.SelectedValue = "True";
                IList<Department> departmentList = DataAccessContext.DepartmentRepository.GetAll( uxLanguageControl.CurrentCulture, "SortOrder" );
                IList<Product> productList = DataAccessContext.ProductRepository.GetByDepartmentID( uxLanguageControl.CurrentCulture,
                    department.DepartmentID, "ProductID", BoolFilter.ShowAll, new StoreRetriever().GetCurrentStoreID() );
                if ((!String.IsNullOrEmpty( department.DepartmentListLayoutPath ) && productList.Count == 0) || !department.IsLeafDepartment())
                {
                    uxLayoutTypeRadioButtonList.SelectedValue = "Department";
                    uxDepartmentLayoutDrop.SelectedValue = department.DepartmentListLayoutPath;
                    uxProductListLayoutDrop.SelectedValue = "";
                    uxDepartmentLayoutPanel.Style["display"] = "";
                    uxProductListLayoutPanel.Style["display"] = "none";
                }
                else
                {
                    uxLayoutTypeRadioButtonList.SelectedValue = "Product";
                    uxDepartmentLayoutDrop.SelectedValue = "";
                    uxProductListLayoutDrop.SelectedValue = department.ProductListLayoutPath;
                    uxDepartmentLayoutPanel.Style["display"] = "none";
                    uxProductListLayoutPanel.Style["display"] = "";
                }
            }

        }
    }

    private void AddDepartment()
    {
        try
        {
            if (Page.IsValid)
            {
                Department department = new Department( uxLanguageControl.CurrentCulture );
                department = SetUpDepartment( department );
                department = DataAccessContext.DepartmentRepository.Save( department );

                HttpContext.Current.Session[SystemConst.DepartmentTreeViewLeftKey] = null;
                uxMessage.DisplayMessage( Resources.DepartmentMessages.AddSuccess );
                ClearInputFields();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        uxStatusHidden.Value = "Added";

        AdminUtilities.ClearSiteMapCache();
    }

    private void UpdateDepartment()
    {
        try
        {
            if (Page.IsValid)
            {
                if (uxParentDrop.SelectedValue != ParentDepartmentID)
                {
                    DataAccessContext.DepartmentRepository.ReArrangeSortOrder( uxLanguageControl.CurrentCulture, uxParentDrop.SelectedValue, CurrentID, BoolFilter.ShowTrue );
                }

                Department department = DataAccessContext.DepartmentRepository.GetOne(
                    uxLanguageControl.CurrentCulture, CurrentID );
                department = SetUpDepartment( department );
                department = DataAccessContext.DepartmentRepository.Save( department );

                if (IsEnabledChanged())
                    DataAccessContext.ProductRepository.RefreshIsParentVisibleByDepartmentID( CurrentID, new StoreRetriever().GetCurrentStoreID() );

                DataAccessContext.DepartmentRepository.ReArrangeSortOrderByParentDepartmentID(
                    uxLanguageControl.CurrentCulture,
                    ParentDepartmentID,
                    BoolFilter.ShowTrue );

                HttpContext.Current.Session[SystemConst.DepartmentTreeViewLeftKey] = null;
                uxMessage.DisplayMessage( Resources.DepartmentMessages.UpdateSuccess );

                InitParentDropDownList();
                PopulateControls();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        uxStatusHidden.Value = "Updated";

        AdminUtilities.ClearSiteMapCache();
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private string SetParentDepartmentID()
    {
        if (uxParentDrop.SelectedValue == "0")
        {
            return RootDepartmentID;
        }
        else
        {
            return uxParentDrop.SelectedValue;
        }
    }

    private Department SetUpDepartment( Department department )
    {
        department.Name = uxNameText.Text;
        department.Description = uxDescriptionText.Text;
        department.ImageFile = uxImageText.Text;
        department.ParentDepartmentID = SetParentDepartmentID();
        department.DiscountGroupID = ConvertUtilities.ToInt32( uxQuantityDiscount.DiscountGounpID );
        department.IsEnabled = uxIsEnabledCheck.Checked;
        department.Other1 = uxOtherOneText.Text;
        department.Other2 = uxOtherTwoText.Text;
        department.Other3 = uxOtherThreeText.Text;
        department.Other4 = uxOtherFourText.Text;
        department.Other5 = uxOtherFiveText.Text;
        department.MetaKeyword = uxMetaKeywordText.Text;
        department.MetaDescription = uxMetaDescriptionText.Text;
        department.DepartmentListLayoutPath = uxDepartmentLayoutDrop.SelectedValue;
        department.ProductListLayoutPath = uxProductListLayoutDrop.SelectedValue;
        department.RootID = RootDepartmentID;
        department.ImageAlt = uxImgAlternateTextbox.Text;
        department.ImageTitle = uxImgTitleTextbox.Text;
        department.IsAnchor = uxIsAnchorCheck.Checked;
        department.IsShowNewArrival = uxIsShowNewArrivalCheck.Checked;
        department.NewArrivalAmount = ConvertUtilities.ToInt32( uxNewArrivalAmountText.Text );

        if (!ConvertUtilities.ToBoolean( uxLayoutOverrideDrop.SelectedValue ))
        {
            department.ProductListLayoutPath = String.Empty;
            department.DepartmentListLayoutPath = String.Empty;
        }
        else
        {
            if (uxLayoutTypeRadioButtonList.SelectedValue == "Department")
            {
                department.ProductListLayoutPath = String.Empty;
                department.DepartmentListLayoutPath = uxDepartmentLayoutDrop.SelectedValue;
            }
            else
            {
                department.ProductListLayoutPath = uxProductListLayoutDrop.SelectedValue;
                department.DepartmentListLayoutPath = String.Empty;
            }
        }

        return department;
    }

    private void PopulateDropdown()
    {
        string[] fileList = Directory.GetFiles( Server.MapPath( SystemConst.LayoutDepartmentListPath ) );
        uxDepartmentLayoutDrop.Items.Clear();
        uxDepartmentLayoutDrop.Items.Add( new ListItem( "Please Select...", "" ) );
        foreach (string item in fileList)
        {
            string itemName = item.Substring( item.LastIndexOf( "\\" ) + 1 );
            if (itemName.Substring( itemName.LastIndexOf( "." ) + 1 ) == "ascx")
            {
                uxDepartmentLayoutDrop.Items.Add( new ListItem( itemName, itemName ) );
            }
        }
        uxDepartmentLayoutDrop.SelectedValue =
            DataAccessContext.Configurations.GetValueNoThrow( "DefaultDepartmentListLayout" );

        fileList = Directory.GetFiles( Server.MapPath( SystemConst.LayoutProductListPath ) );
        uxProductListLayoutDrop.Items.Clear();
        uxProductListLayoutDrop.Items.Add( new ListItem( "Please Select...", "" ) );
        foreach (string item in fileList)
        {
            string itemName = item.Substring( item.LastIndexOf( "\\" ) + 1 );
            if (itemName.Substring( itemName.LastIndexOf( "." ) + 1 ) == "ascx")
            {
                uxProductListLayoutDrop.Items.Add( new ListItem( itemName, itemName ) );
            }
        }
        uxProductListLayoutDrop.SelectedValue =
            DataAccessContext.Configurations.GetValueNoThrow( "DefaultProductListLayout" );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxLanguageControl.BubbleEvent += new EventHandler( Details_RefreshHandler );

        uxLayoutRadioPanel.Style["display"] = "none";
        uxDepartmentLayoutPanel.Style["display"] = "none";
        uxProductListLayoutPanel.Style["display"] = "none";
        uxHiddenStatus.Value = "";
        uxLayoutOverrideDrop.Attributes.Add( "onchange", String.Format( "LayoutOverrideChange( this,'{0}', '{1}', '{2}', '{3}' );", uxLayoutRadioPanel.ClientID, uxDepartmentLayoutPanel.ClientID, uxProductListLayoutPanel.ClientID, uxHiddenStatus.ClientID ) );
        foreach (ListItem item in uxLayoutTypeRadioButtonList.Items)
        {
            item.Attributes.Add( "onClick", String.Format( "ShowSubPanel('{0}','{1}','{2}')", uxLayoutRadioPanel.ClientID, uxDepartmentLayoutPanel.ClientID, uxProductListLayoutPanel.ClientID ) );
        }

        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Test)
        {
            uxImageText.Visible = true;
        }

        if (!MainContext.IsPostBack)
        {
            uxNewArrivalAmountPanel.Visible = false;

            uxImageDepartmentUpload.PathDestination = _pathUploadDepartment;
            uxImageDepartmentUpload.ReturnTextControlClientID = uxImageText.ClientID;
            PopulateDropdown();
        }

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (IsEditMode())
        {
            InitParentDropDownList();

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
                uxImageDepartmentLinkButton.Visible = false;
            }

            uxAddButton.Visible = false;
        }
        else
        {
            InitParentDropDownList();

            if (IsAdminModifiable())
            {
                uxAddButton.Visible = true;
                uxUpdateButton.Visible = false;
            }
            else
            {
                MainContext.RedirectMainControl( "DepartmentList.ascx" );
            }
        }
        PouplateThemControlsInPreRender();
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        AddDepartment();
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        UpdateDepartment();
    }

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }

    protected void uxImageDepartmentLinkButton_Click( object sender, EventArgs e )
    {
        uxImageDepartmentUpload.ShowControl = true;
    }

    protected void uxIsShowNewArrivalCheck_CheckedChanged( object sender, EventArgs e )
    {
        uxNewArrivalAmountPanel.Visible = uxIsShowNewArrivalCheck.Checked;
    }
}
