using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.ServerControls;
using Vevo.WebUI.Users;

namespace Vevo
{
    /// <summary>
    /// Summary description for AdminAdvancedBaseUserControl
    /// </summary>
    public class AdminAdvancedBaseUserControl : Vevo.WebUI.BaseControls.BasePublisherUserControl
    {
        public MainContext MainContext
        {
            get
            {
                AdminAdvancedBasePage page = (AdminAdvancedBasePage) Page;
                return page.MainContext;
            }
        }

        public enum MenuMode
        {
            Text,
            Image
        }

        protected void ChangePage_Click( object sender, EventArgs e )
        {
            AdvancedLinkButton mylink = (AdvancedLinkButton) sender;
            MainContext.RedirectMainControl( mylink.PageName, mylink.PageQueryString );
        }

        protected void uxGrid_RowDataBound( object sender, GridViewRowEventArgs e )
        {
            if (e.Row.Cells.Count > 1)
            {
                if (e.Row.RowIndex > -1)
                {
                    if ((e.Row.RowIndex % 2) == 0)
                    {
                        // Even
                        e.Row.Attributes.Add( "onmouseover", "this.className='DefaultGridRowStyleHover'" );
                        e.Row.Attributes.Add( "onmouseout", "this.className='DefaultGridRowStyle'" );
                    }
                    else
                    {
                        // Odd
                        e.Row.Attributes.Add( "onmouseover", "this.className='DefaultGridRowStyleHover'" );
                        e.Row.Attributes.Add( "onmouseout", "this.className='DefaultGridAlternatingRowStyle'" );
                    }
                }
            }
        }

        public AdminAdvancedBaseUserControl()
        {
            Load += new EventHandler( AdminAdvancedBaseUserControl_Load );
        }

        void AdminAdvancedBaseUserControl_Load( object sender, EventArgs e )
        {
        }

        protected bool IsAdminModifiable()
        {
            AdminHelper admin = AdminHelper.LoadByUserName( Page.User.Identity.Name );
            return admin.CanModifyPage( MainContext.LastControl );
        }

        protected bool IsAdminViewable( string pageUrl )
        {
            AdminHelper admin = AdminHelper.LoadByUserName( Page.User.Identity.Name );
            return admin.CanViewPage( pageUrl );
        }

        private string _messageControlID;

        private IMessageControl _messageControl;

        private IMessageControl GetMessageControl()
        {
            if (!String.IsNullOrEmpty( _messageControlID ))
            {
                IMessageControl messageControl =
                    (IMessageControl) WebUtilities.FindControlRecursive( Page, _messageControlID );

                if (messageControl != null)
                    return messageControl;
            }

            return null;
        }

        protected IMessageControl MessageControl
        {
            get
            {
                if (_messageControl == null)
                    _messageControl = GetMessageControl();

                if (_messageControl == null)
                    return NullMessageControl.Instance;

                return _messageControl;
            }
        }

        public string MessageControlID
        {
            get
            {
                return _messageControlID;
            }
            set
            {
                _messageControlID = value.ToString().Trim();
            }
        }
    }
}