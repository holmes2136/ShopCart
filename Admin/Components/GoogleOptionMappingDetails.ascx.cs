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

public partial class Admin_Components_GoogleOptionMappingDetails : AdminAdvancedBaseUserControl
{
    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string CurrentGoogleFeedTagID
    {
        get
        {
            return MainContext.QueryString["GoogleFeedTagID"];
        }
    }

    private string CurrentOptionGroupID
    {
        get
        {
            return MainContext.QueryString["OptionGroupID"];
        }
    }

    private bool IsDrowDownValid()
    {
        if (uxGoogleTagDrop.SelectedValue.Equals( "0" ))
            return false;
        else if (uxOptionGroupDrop.SelectedValue.Equals( "0" ))
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

    private void PopulateOptionGroupDropDown()
    {
        uxOptionGroupDrop.DataSource = DataAccessContext.OptionGroupRepository.GetAll( uxLanguageControl.CurrentCulture );
        uxOptionGroupDrop.DataTextField = "Name";
        uxOptionGroupDrop.DataValueField = "OptionGroupID";
        uxOptionGroupDrop.DataBind();
    }

    private void PopulateDropDown()
    {
        string tagTypeID = ((int) Vevo.Domain.GoogleFeed.GoogleFeedTag.GoogleFeedTagType.Variant).ToString();

        PopulateGoogleTagDropDown( tagTypeID );
        PopulateOptionGroupDropDown();

        uxGoogleTagDrop.Items.Insert( 0, new ListItem( "-- Select Google Tag --", "0" ) );
        uxOptionGroupDrop.Items.Insert( 0, new ListItem( "-- Select Option Group --", "0" ) );
    }

    private GoogleOptionMapping SetupGoogleOptionMapping( GoogleOptionMapping optionMapping )
    {
        optionMapping.GoogleFeedTagID = uxGoogleTagDrop.SelectedValue;
        optionMapping.OptionGroupID = uxOptionGroupDrop.SelectedValue;
        return optionMapping;
    }

    private void SetupField( GoogleOptionMapping optionMapping )
    {
        uxGoogleTagDrop.SelectedValue = optionMapping.GoogleFeedTagID;
        uxOptionGroupDrop.SelectedValue = optionMapping.OptionGroupID;
    }

    private bool IsGoogleOptionMappingValid( GoogleOptionMapping optionMapping )
    {
        GoogleOptionMapping result = DataAccessContext.GoogleFeedMappingRepository.GetOneGoogleOptionMapping(
            uxLanguageControl.CurrentCulture,
            optionMapping.GoogleFeedTagID,
            optionMapping.OptionGroupID );

        return result.IsNull;
    }

    private GoogleOptionMapping UpdateOptionMapping( GoogleOptionMapping optionMapping )
    {
        optionMapping = DataAccessContext.GoogleFeedMappingRepository.UpdateGoogleOptionMapping(
                        uxLanguageControl.CurrentCulture,
                        CurrentGoogleFeedTagID,
                        CurrentOptionGroupID,
                        optionMapping );
        return optionMapping;
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
                SetupField( DataAccessContext.GoogleFeedMappingRepository.GetOneGoogleOptionMapping(
                    uxLanguageControl.CurrentCulture,
                    CurrentGoogleFeedTagID,
                    CurrentOptionGroupID ) );
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
                    MainContext.RedirectMainControl( "GoogleOptionMappingList.ascx" );
                }
            }
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
                GoogleOptionMapping optionMapping = new GoogleOptionMapping();
                optionMapping = SetupGoogleOptionMapping( optionMapping );

                if (IsGoogleOptionMappingValid( optionMapping ))
                {
                    optionMapping = DataAccessContext.GoogleFeedMappingRepository.SaveGoogleOptionMapping(
                        uxLanguageControl.CurrentCulture, optionMapping );
                    MainContext.RedirectMainControl( "GoogleOptionMappingList.ascx" );
                }
                else
                {
                    uxMessage.DisplayError( Resources.GoogleOptionMappingMessages.AddDuplicate );
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
                GoogleOptionMapping optionMapping = new GoogleOptionMapping();
                optionMapping = SetupGoogleOptionMapping( optionMapping );

                if (IsGoogleOptionMappingValid( optionMapping ))
                {
                    optionMapping = UpdateOptionMapping( optionMapping );
                    uxMessage.DisplayMessage( Resources.GoogleOptionMappingMessages.UpdateSuccess );
                }
                else
                {
                    if ((CurrentGoogleFeedTagID == optionMapping.GoogleFeedTagID) && (CurrentOptionGroupID == optionMapping.OptionGroupID))
                    {
                        optionMapping = UpdateOptionMapping( optionMapping );
                        uxMessage.DisplayMessage( Resources.GoogleOptionMappingMessages.UpdateSuccess );
                    }
                    else
                    {
                        uxMessage.DisplayError( Resources.GoogleOptionMappingMessages.UpdateDuplicate );
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
