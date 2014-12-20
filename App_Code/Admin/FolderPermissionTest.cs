using System;
using System.Data;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Vevo.WebAppLib;

namespace Vevo
{
    /// <summary>
    /// Summary description for FolderPermissionTest
    /// </summary>
    public class FolderPermissionTest
    {
        private const string WriteTestFileName = "VevoWriteTestFile.txt";

        private bool _success = true;
        private StringCollection _errorPaths = new StringCollection();
        private String[] _testFolders = new String[] { 
            "~/Images", 
            "~/Images/Configuration", 
            "~/Images/Categories",               
            "~/Images/Products", 
            "~/Images/Gateway",
            "~/Images/News",
            "~/Images/UploadFile",
            "~/Images/UploadFileTemp",
            String.Format( "~/{0}", WebConfiguration.ProductDownloadPath ) , 
            "~/Import", 
            "~/ContentTemplates",            
            "~/Export"
        };

        private void TestFolder( string path )
        {
            string localPath = HttpContext.Current.Server.MapPath( path );
            string fullpath = localPath + "\\" + WriteTestFileName;
            try
            {
                if (Directory.Exists( localPath ))
                {
                    if (File.Exists( fullpath ))
                    {
                        File.Delete( fullpath );
                    }

                    FileStream file = new FileStream( fullpath, FileMode.Create );
                    file.Close();
                    File.Delete( fullpath );

                    _success &= true;
                }
                else
                {
                    _errorPaths.Add( localPath );

                    _success &= false;
                }
            }
            catch (Exception)
            {
                _errorPaths.Add( localPath );

                _success &= false;
            }
        }


        public string ErrorMessage
        {
            get
            {
                string message = String.Empty;
                foreach (string path in _errorPaths)
                {
                    message += path + "<br />";
                }

                return message;
            }
        }

        public bool TestWrite()
        {
            foreach (string path in _testFolders)
            {
                TestFolder( path );
            }
            return _success;
        }

    }
}
