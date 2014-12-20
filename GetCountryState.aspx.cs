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
using System.Xml;
using Vevo.Domain.Users;
using Vevo.Domain;
using System.Collections.Generic;

public partial class CountryStateXML : System.Web.UI.Page
{
    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected override void Render( HtmlTextWriter writer )
    {
        XmlDocument xmlDoc = new XmlDocument();

        // Write down the XML declaration
        XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration( "1.0", "utf-8", null );

        // Create the root element
        XmlElement rootNode = xmlDoc.CreateElement( "CountryStorage" );
        xmlDoc.InsertBefore( xmlDeclaration, xmlDoc.DocumentElement );
        xmlDoc.AppendChild( rootNode );

        // Create a new <Countries> element and add it to the root node
        XmlElement parentNode = xmlDoc.CreateElement( "Countries" );
        xmlDoc.DocumentElement.PrependChild( parentNode );

        IList<Country> countryList = DataAccessContext.CountryRepository.GetAll( BoolFilter.ShowTrue, "CommonName" );

        foreach (Country country in countryList)
        {
            XmlElement countryItem = xmlDoc.CreateElement( "Country" );
            parentNode.AppendChild( countryItem );

            IList<State> stateList = DataAccessContext.StateRepository.GetAllByCountryCode(
                            country.CountryCode, "StateName", BoolFilter.ShowTrue );

            if (stateList.Count > 0)
            {
                XmlElement states = xmlDoc.CreateElement( "States" );
                countryItem.PrependChild( states );

                foreach (State state in stateList)
                {
                    XmlElement stateItem = xmlDoc.CreateElement( "State" );
                    states.AppendChild( stateItem );

                    AddElement( xmlDoc, stateItem, "Name", state.StateName );
                    AddElement( xmlDoc, stateItem, "Code", state.StateCode );
                }
            }

            AddElement( xmlDoc, countryItem, "Name", country.CommonName );
            AddElement( xmlDoc, countryItem, "Code", country.CountryCode );
        }

        Response.ContentType = "text/xml";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.Write( xmlDoc.InnerXml );
        Response.End();
    }

    private void AddElement( XmlDocument xmlDoc, XmlElement parentElement, string childElementName, string childValue )
    {
        XmlElement newElement = xmlDoc.CreateElement( childElementName );
        XmlText newElementText = xmlDoc.CreateTextNode( childValue );

        parentElement.AppendChild( newElement );
        newElement.AppendChild( newElementText );
    }
}
