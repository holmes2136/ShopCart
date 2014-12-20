using System;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.WebUI;

public partial class Components_ContentMenuNavList : Vevo.WebUI.International.BaseLanguageUserControl
{
    private string _position;
    private string _rootID;
    private string _menuType;
    private string _rootMenuName = "[$RootMenuName]";
    public string Position
    {
        get
        {
            return _position;
        }
        set
        {
            _position = value;
        }
    }

    private void Components_ContentMenuNavList_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        Refresh();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( Components_ContentMenuNavList_StoreCultureChanged );



        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    private void Refresh()
    {
        PopulateControls();
        switch (_menuType)
        {
            case "cascade":
                uxMenuList.Refresh();
                break;

            default:
                uxNormalList.Refresh();
                break;
        }

    }

    private void PopulateControls()
    {
        switch (Position)
        {
            case "top":
                _rootID = DataAccessContext.Configurations.GetValue( "TopContentMenu" );
                _menuType = DataAccessContext.Configurations.GetValue( "TopContentMenuType" );
                
                break;

            case "left":
                uxContentMenuNavTitle.Visible = true;
                _rootID = DataAccessContext.Configurations.GetValue( "LeftContentMenu" );
                _menuType = DataAccessContext.Configurations.GetValue( "LeftContentMenuType" );
                break;

            case "right":
                uxContentMenuNavTitle.Visible = true;
                _rootID = DataAccessContext.Configurations.GetValue( "RightContentMenu" );
                _menuType = DataAccessContext.Configurations.GetValue( "RightContentMenuType" );
                break;
        }


        ContentMenu contentMenu = DataAccessContext.ContentMenuRepository.GetOne( _rootID );
        if (!contentMenu.IsEnabled)
        {
            this.Visible = false;
        }
        else
        {
            switch (_menuType)
            {
                case "cascade":
                    uxMenuList.MaxNode = DataAccessContext.Configurations.GetIntValue( "CategoryMenuLevel" );
                    uxMenuList.RootID = _rootID;
                    uxMenuList.RootMenuName = _rootMenuName;
                    uxMenuList.Visible = true;
                    uxNormalList.Visible = false;
                    break;

                default:
                    uxNormalList.RootID = _rootID;
                    uxNormalList.RootMenuName = _rootMenuName;
                    uxNormalList.Visible = true;
                    uxMenuList.Visible = false;
                    break;
            }

        }


    }


}
