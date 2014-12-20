using System;
using System.Data;
using System.Configuration;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.Shared.Utilities;

namespace Vevo
{
    /// <summary>
    /// Summary description for InputControlRecorder
    /// </summary>
    public class InputControlRecorder
    {
        private NameValueCollection _savedData = new NameValueCollection();

        private void SaveValue( Control child )
        {
            if (child is TextBox)
            {
                _savedData[child.UniqueID] = ((TextBox) child).Text;
            }
            else if (child is CheckBox)
            {
                _savedData[child.UniqueID] = ((CheckBox) child).Checked.ToString();
            }
            else if (child is DropDownList)
            {
                _savedData[child.UniqueID] = ((DropDownList) child).SelectedValue;
            }
            else if (child is RadioButton)
            {
                _savedData[child.UniqueID] = ((RadioButton) child).Checked.ToString();
            }
            else if (child is RadioButtonList)
            {
                _savedData[child.UniqueID] = ((RadioButtonList) child).SelectedValue;
            }
        }

        private void RestoreValue( Control child )
        {
            if (child is TextBox)
            {
                ((TextBox) child).Text = _savedData[child.UniqueID];
            }
            else if (child is CheckBox)
            {
                ((CheckBox) child).Checked = ConvertUtilities.ToBoolean( _savedData[child.UniqueID] );
            }
            else if (child is DropDownList)
            {
                ((DropDownList) child).SelectedValue = _savedData[child.UniqueID];
            }
            else if (child is RadioButton)
            {
                ((RadioButton) child).Checked = ConvertUtilities.ToBoolean( _savedData[child.UniqueID] );
            }
            else if (child is RadioButtonList)
            {
                ((RadioButtonList) child).SelectedValue = _savedData[child.UniqueID];
            }
        }

        private void SaveInputRecursively( Control parent )
        {
            foreach (Control child in parent.Controls)
            {
                SaveValue( child );
                SaveInputRecursively( child );
            }
        }

        private void RestoreInputRecursively( Control parent )
        {
            foreach (Control child in parent.Controls)
            {
                RestoreValue( child );
                RestoreInputRecursively( child );
            }
        }


        public void Save( Control parentControl )
        {
            SaveInputRecursively( parentControl );
        }

        public void Restore( Control parentControl )
        {
            RestoreInputRecursively( parentControl );
        }

    }
}
