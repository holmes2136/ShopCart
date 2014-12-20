using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Vevo
{
    /// <summary>
    /// Summary description for StoreError
    /// </summary>
    public class StoreError
    {
        //public enum ErrorStatus
        //{
        //    OK,
        //    Error
        //};


        private static StoreError _instance;

        private Exception _exception;


        public StoreError()
        {
            _exception = null;
        }


        public Exception Exception
        {
            get { return _exception; }
            set { _exception = value; }
        }



        public static StoreError Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StoreError();

                return _instance;
            }
        }

    }
}
