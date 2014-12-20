using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.Domain.Products;
using System.Data;
using Vevo.Domain.GoogleFeed;

public partial class Admin_Components_GoogleSpecMappingDetails : AdminAdvancedBaseUserControl
{
    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string CurrentGoogleFeedTagValueID
    {
        get
        {
            return MainContext.QueryString["GoogleFeedTagValueID"];
        }
    }

    private string CurrentSpecificationItemValue
    {
        get
        {
            return MainContext.QueryString["Value"];
        }
    }

    private bool IsDrowDownValid()
    {
        if (uxGoogleTagDrop.SelectedValue.Equals( "0" ))
            return false;
        else if (uxGoogleTagValueDrop.SelectedValue.Equals( "0" ))
            return false;
        else if (uxProSpecItemDrop.SelectedValue.Equals( "0" ))
            return false;
        else if (uxProSpecValueDrop.SelectedValue.Equals( "0" ))
            return false;
        else
            return true;
    }

    private void PopulateGoogleTagDropDown( string tagTypeID )
    {
        uxGoogleTagDrop.DataSource = DataAccessContext.GoogleFeedTagRepository.GetAllByTagTypeID( tagTypeID );
        uxGoogleTagDrop.DataTextField = "TagName";
        uxGoogleTagDrop.DataValueField = "GoogleFeedTagID";
        uxGoogleTagDrop.DataBind();
    }

    private void PopulateGoogleTagValueDropDown( string selectedGoogleTagID )
    {
        uxGoogleTagValueDrop.Items.Clear();
        uxGoogleTagValueDrop.DataSource = DataAccessContext.GoogleFeedTagValueRepository.GetAllByTagID( selectedGoogleTagID );
        uxGoogleTagValueDrop.DataTextField = "ValueName";
        uxGoogleTagValueDrop.DataValueField = "GoogleFeedTagValueID";
        uxGoogleTagValueDrop.DataBind();
    }

    private void PopulateProductSpecificationItemDropDown()
    {
        DataTable productSpecTable = DataAccessContext.SpecificationItemRepository.GetAll( uxLanguageControl.CurrentCulture );
        DataTable newProductSpecTable = new DataTable();
        newProductSpecTable.Columns.Add( "ProductSpecItemID" );
        newProductSpecTable.Columns.Add( "ProductSpecItemName" );

        for (int i = 0; i < productSpecTable.Rows.Count; i++)
        {
            DataRow dummyRow = newProductSpecTable.NewRow();
            dummyRow["ProductSpecItemID"] = productSpecTable.Rows[i]["SpecificationItemID"].ToString();
            dummyRow["ProductSpecItemName"] = productSpecTable.Rows[i]["GroupName"].ToString() + " - " + productSpecTable.Rows[i]["DisplayName"].ToString();
            newProductSpecTable.Rows.Add( dummyRow );
        }

        uxProSpecItemDrop.DataSource = newProductSpecTable;
        uxProSpecItemDrop.DataTextField = "ProductSpecItemName";
        uxProSpecItemDrop.DataValueField = "ProductSpecItemID";
        uxProSpecItemDrop.DataBind();
    }

    private void PopulateProductSpecificationValueDropDown( string specificationItemID )
    {
        uxProSpecValueDrop.Items.Clear();
        uxProSpecValueDrop.DataSource = DataAccessContext.SpecificationItemRepository.GetAllProductSpecificationValueByItemID( specificationItemID );
        uxProSpecValueDrop.DataTextField = "Value";
        uxProSpecValueDrop.DataValueField = "Value";
        uxProSpecValueDrop.DataBind();

        if (uxProSpecValueDrop.Items.Count <= 0)
        {
            uxProSpecValueDrop.Items.Insert( 0, new ListItem( "N/A", "N/A" ) );
        }
    }

    private void PopulateDropDown()
    {
        string tagTypeID = ((int) Vevo.Domain.GoogleFeed.GoogleFeedTag.GoogleFeedTagType.Apparel).ToString();

        PopulateGoogleTagDropDown( tagTypeID );
        PopulateProductSpecificationItemDropDown();

        uxGoogleTagDrop.Items.Insert( 0, new ListItem( "-- Select Google Tag --", "0" ) );
        uxGoogleTagValueDrop.Items.Insert( 0, new ListItem( "-- Select Tag First --", "0" ) );
        uxProSpecItemDrop.Items.Insert( 0, new ListItem( "-- Select Specification --", "0" ) );
        uxProSpecValueDrop.Items.Insert( 0, new ListItem( "-- Select Specification First --", "0" ) );
    }

    private GoogleSpecMapping SetupGoogleSpecMapping( GoogleSpecMapping specMapping )
    {
        specMapping.GoogleFeedTagValueID = uxGoogleTagValueDrop.SelectedValue;
        specMapping.SpecificationItemID = uxProSpecItemDrop.SelectedValue;
        specMapping.Value = uxProSpecValueDrop.SelectedValue;
        return specMapping;
    }

    private void SetupField( GoogleSpecMapping specMapping )
    {
        uxGoogleTagDrop.SelectedValue = specMapping.GoogleFeedTagID;
        PopulateGoogleTagValueDropDown( uxGoogleTagDrop.SelectedValue );
        uxGoogleTagValueDrop.SelectedValue = specMapping.GoogleFeedTagValueID;

        uxProSpecItemDrop.SelectedValue = specMapping.SpecificationItemID;
        PopulateProductSpecificationValueDropDown( uxProSpecItemDrop.SelectedValue );
        uxProSpecValueDrop.SelectedValue = specMapping.Value;
    }

    private bool IsGoogleSpecMappingValid( GoogleSpecMapping specMapping )
    {
        GoogleSpecMapping result = DataAccessContext.GoogleFeedMappingRepository.GetOneGoogleSpecMapping( 
            uxLanguageControl.CurrentCulture, 
            specMapping.GoogleFeedTagValueID, 
            specMapping.Value );

        return result.IsNull;
    }

    private GoogleSpecMapping UpdateSpecMapping( GoogleSpecMapping specMapping )
    {
        specMapping = DataAccessContext.GoogleFeedMappingRepository.UpdateGoogleSpecMapping(
                        uxLanguageControl.CurrentCulture,
                        CurrentGoogleFeedTagValueID,
                        CurrentSpecificationItemValue,
                        specMapping );
        return specMapping;
    }

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateDropDown();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            if (IsEditMode())
            {
                //PopulateDropDown();
                SetupField( DataAccessContext.GoogleFeedMappingRepository.GetOneGoogleSpecMapping( 
                    uxLanguageControl.CurrentCulture, 
                    CurrentGoogleFeedTagValueID, 
                    CurrentSpecificationItemValue ) );
                uxAddButton.Visible = false;
                uxUpdateButton.Visible = true;

                if (IsAdminModifiable())
                {
                    uxUpdateButton.Visible = true;
                }
                else
                {
                    uxUpdateButton.Visible = false;
                }
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
                    MainContext.RedirectMainControl( "GoogleSpecMappingList.ascx" );
                }
            }
        }
    }

    protected void uxGoogleTagDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxGoogleTagDrop.SelectedValue != "0")
        {
            PopulateGoogleTagValueDropDown( uxGoogleTagDrop.SelectedValue );
        }
        else
        {
            uxGoogleTagValueDrop.Items.Clear();
            uxGoogleTagValueDrop.Items.Insert( 0, new ListItem( "-- Select Tag First --", "0" ) );
        }
    }

    protected void uxProSpecItemDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxProSpecItemDrop.SelectedValue != "0")
        {
            PopulateProductSpecificationValueDropDown( uxProSpecItemDrop.SelectedValue );
        }
        else
        {
            uxProSpecValueDrop.Items.Clear();
            uxProSpecValueDrop.Items.Insert( 0, new ListItem( "-- Select Specification First --", "0" ) );
        }
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        if (!IsDrowDownValid())
            return;
        try
        {
            if (Page.IsValid)
            {
                GoogleSpecMapping specMapping = new GoogleSpecMapping();
                specMapping = SetupGoogleSpecMapping( specMapping );

                if (IsGoogleSpecMappingValid( specMapping ))
                {
                    specMapping = DataAccessContext.GoogleFeedMappingRepository.SaveGoogleSpecMapping( uxLanguageControl.CurrentCulture, specMapping );
                    MainContext.RedirectMainControl( "GoogleSpecMappingList.ascx" );
                }
                else
                {
                    uxMessage.DisplayError( Resources.GoogleSpecMappingMessages.AddDuplicate );
                }
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        if (!IsDrowDownValid())
            return;
        try
        {
            if (Page.IsValid)
            {
                GoogleSpecMapping specMapping = new GoogleSpecMapping();
                specMapping = SetupGoogleSpecMapping( specMapping );

                if (IsGoogleSpecMappingValid( specMapping ))
                {
                    specMapping = UpdateSpecMapping( specMapping );
                    uxMessage.DisplayMessage( Resources.GoogleSpecMappingMessages.UpdateSuccess );
                }
                else
                {
                    if ((CurrentGoogleFeedTagValueID == specMapping.GoogleFeedTagValueID) && (CurrentSpecificationItemValue == specMapping.Value))
                    {
                        specMapping = UpdateSpecMapping( specMapping );
                        uxMessage.DisplayMessage( Resources.GoogleSpecMappingMessages.UpdateSuccess );
                    }
                    else
                    {
                        uxMessage.DisplayError( Resources.GoogleSpecMappingMessages.UpdateDuplicate );
                    }
                }
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }
}
