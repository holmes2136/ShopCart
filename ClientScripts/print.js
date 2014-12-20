// JScript File

function getPrint(print_area, vevoData, theme)
{ 
var data = vevoData.split("|");
var sitename = data[0];
var companyName = data[1];
var address = data[2];
var city = data[3];
var state = data[4];
var zipCode = data[5];
var country = data[6];
var phone = data[7];
var fax = data[8];
var email = data[9];
var storeTheme = theme;
if(theme.replace(/\s/g,"") == "")
{
    storeTheme = "Default";
}

//Creating new page
var pp = window.open();

//Adding HTML opening tag with <HEAD> … </HEAD> portion 
pp.document.writeln('<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">');
pp.document.writeln('<html xmlns="http://www.w3.org/1999/xhtml">');
pp.document.writeln('<head><title>Print Preview</title>')

//Add style for print page!
pp.document.writeln('<link rel="stylesheet" type="text/css" href="App_themes/')
pp.document.writeln(storeTheme)
pp.document.writeln('/Print.css" />')
//End Add style for print page!
pp.document.writeln('<base target="_self"></head>')
//Adding Body Tag
pp.document.writeln('<body class="PrintPage" MS_POSITIONING="GridLayout" ');
pp.document.writeln('>');
//Adding form Tag
pp.document.writeln('<form method="post">');
//Creating two buttons Print and Close within a HTML table
//Writing print area of the calling page
pp.document.writeln('<div class="PrintPageDiv">');
pp.document.writeln('<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">');


pp.document.writeln('<tr>');
pp.document.writeln('<td align=center style="width: 100%">');

pp.document.writeln('<a onclick="window.location.reload();window.print();" class="PrintButton">Print</a>');
pp.document.writeln('<a onclick="window.close();"class="CloseButton">Close</a>');

pp.document.writeln('</td>');
pp.document.writeln('</tr>');

pp.document.writeln('<tr>');
pp.document.writeln('<td style="width: 100%"><h4>');
pp.document.writeln(companyName);
pp.document.writeln('</h4></td>');
pp.document.writeln('</tr>');

pp.document.writeln('<tr>');
pp.document.writeln('<td style="width: 100%">');
pp.document.writeln(address);
pp.document.writeln('</td>');
pp.document.writeln('</tr>');

pp.document.writeln('<tr>');
pp.document.writeln('<td style="width: 100%">');
pp.document.writeln(city + ', ');
pp.document.writeln(state + ' ' );
pp.document.writeln('</td>');
pp.document.writeln('</tr>');

pp.document.writeln('<tr>');
pp.document.writeln('<td style="width: 100%">');
pp.document.writeln(zipCode);
pp.document.writeln('</td>');
pp.document.writeln('</tr>');

pp.document.writeln('<tr>');
pp.document.writeln('<td style="width: 100%">');
pp.document.writeln(country);
pp.document.writeln('</td>');
pp.document.writeln('</tr>');

pp.document.writeln('<tr>');
pp.document.writeln('<td style="width: 100%">Phone: ');
pp.document.writeln(phone);
pp.document.writeln('</td>');
pp.document.writeln('</tr>');

pp.document.writeln('<tr>');
pp.document.writeln('<td style="width: 100%">Fax: ');
pp.document.writeln(fax);
pp.document.writeln('</td>');
pp.document.writeln('</tr>');

pp.document.writeln('<tr>');
pp.document.writeln('<td style="width: 100%">Email: ');
pp.document.writeln(email);
pp.document.writeln('</td>');
pp.document.writeln('</tr>');

pp.document.writeln('</table>');
pp.document.writeln(document.getElementById(print_area).innerHTML);

//Ending Tag of </form>, </body> and </HTML>

pp.document.writeln('<TABLE width=100%><TR>');
pp.document.writeln('<td align=center style="width: 100%; padding-top: 10px;">');

pp.document.writeln('<a onclick="window.location.reload();window.print();" class="PrintButton">Print</a>');
pp.document.writeln('<a onclick="window.close();"class="CloseButton">Close</a>');

pp.document.writeln('</td></TR></TABLE>');
pp.document.writeln('</div>');
pp.document.writeln('</form></body></html>'); 

pp.document.close();

} 

