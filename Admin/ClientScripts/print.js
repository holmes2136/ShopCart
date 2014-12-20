// JScript File


function getPrint(print_area, vevoData, orderID)
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

HideAllControl();



var printarea = document.getElementById(print_area).innerHTML;

//Creating new page
var pp = window.open();

//Adding HTML opening tag with <HEAD> … </HEAD> portion 

pp.document.writeln('<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">');
pp.document.writeln('<html xmlns="http://www.w3.org/1999/xhtml" style="overflow: scroll; height: auto;">');

pp.document.writeln('<HEAD><title>Print Preview</title>')
pp.document.writeln('<link rel="stylesheet" type="text/css" href="../App_themes/AdminBlueTheme/Default.css" />')
pp.document.writeln('<link rel="stylesheet" type="text/css" href="../App_themes/AdminCommon/AdvancedAdminCommon.css" />')
pp.document.writeln('<style type="text/css" media="print">');
pp.document.writeln('#PRINT ,#CLOSE,#PRINT1 ,#CLOSE1{visibility:hidden;}');
pp.document.writeln('</style>');
pp.document.writeln('<base target="_self"></HEAD>')

//Adding Body Tag
pp.document.writeln('<body style="height: auto;">');

pp.document.writeln('<form method="post">');
//Creating two buttons Print and Close within a HTML table
//Writing print area of the calling page

pp.document.writeln('<div style="width:750px;background-color:#fff; margin: 10px auto; padding: 10px; border: solid 1px #ddd;">');

pp.document.writeln('<div class="PrintPageButtonDiv">');
pp.document.writeln('<div class="PrintButtonDiv">');

pp.document.writeln('<a onclick="window.close();" class="ButtonGrey">Close</a>');
pp.document.writeln('<a onclick="window.location.reload();window.print();" class="ButtonOrange mgr10">Print</a>');

pp.document.writeln( '</div>' );
pp.document.writeln('</div>');

pp.document.writeln('<div class="PrintPageCompanyDiv">');

pp.document.writeln( '<div class="fb al c10 mgl5">' );
pp.document.writeln(companyName);
pp.document.writeln( '</div>' );

pp.document.writeln( '<div class="al mgl5" style="line-height:16px;">' );
pp.document.writeln(address);
pp.document.writeln( '<br/>' );
pp.document.writeln(city + ', ');
pp.document.writeln(state + ' ' );
pp.document.writeln(zipCode + ' ' );
pp.document.writeln(country);
pp.document.writeln( '<br/>' );
pp.document.writeln( 'Phone: ' + phone );
pp.document.writeln( '<br/>' );
pp.document.writeln( 'Fax: ' + fax );
pp.document.writeln( '<br/>' );
pp.document.writeln( 'Email: ' +email );
pp.document.writeln( '</div>' );
pp.document.writeln('<div class="w100p mgt20 fs13">');
pp.document.writeln(printarea);
pp.document.writeln( '</div>' );

pp.document.writeln('<div class="PrintPageButtonDiv">');
pp.document.writeln('<div class="PrintButtonDiv">');

pp.document.writeln('<a onclick="window.close();" class="ButtonGrey">Close</a>');
pp.document.writeln('<a onclick="window.location.reload();window.print();" class="ButtonOrange mgr10">Print</a>');

pp.document.writeln('</div>');
pp.document.writeln('</div>');


pp.document.writeln( '</div>' );

pp.document.writeln('</form></body></HTML>'); 
//pp.location.reload();
pp.document.close();
} 

function HideAllControl()
{
        HideControl('topButtonRemove');
        HideControl('ButtonDeleteRemove');
        HideControl('BottomButtonRemove');
        HideControls('dummyControlID_uxAdminContent_ctl02_uxItemGrid','td','a');
        HideControls('dummyControlID_uxAdminContent_ctl02_uxItemGrid','td','input');
        HideControls('dummyControlID_uxAdminContent_ctl02_uxItemGrid','th','input');
}

function HideControl(tagid)
{
     var control;
     control = document.getElementById(tagid);
     control.style.display = 'none'
}

function HideControls(tagid,tagfind,taghidden)
{
     var control,taggotdata;
        control=document.getElementById(tagid).getElementsByTagName(tagfind);
     for(i=0;i<control.length;i++)
        {
            taggotdata = control[i].getElementsByTagName(taghidden)[0];
            if (taggotdata != null)
            {
                control[i].style.display = 'none';
            
            }
        }
}

function clearCurrentLink(){
    var a = document.getElementsByTagName("A");
    for(var i=0;i<a.length;i++){
        if(a[i].href.split("#")[0] == window.location.href.split("#")[0]){
            removeNode(a[i]);
        }
    }
}

function removeNode(n){
    if(n.hasChildNodes()){
        for(var i=0;i<n.childNodes.length;i++){
            n.parentNode.insertBefore(n.childNodes[i].cloneNode(true),n);
        }
    }
    n.parentNode.removeChild(n);
}


function getPrintInvoice(print_area)
{

var printarea = document.getElementById(print_area).innerHTML;

//Creating new page
var pp = window.open();
//Adding HTML opening tag with <HEAD> … </HEAD> portion 

pp.document.writeln('<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">');
pp.document.writeln('<html xmlns="http://www.w3.org/1999/xhtml" style="overflow: scroll; height: auto;">');

pp.document.writeln('<HEAD><title>Invoice</title>')

pp.document.writeln('<link rel="stylesheet" type="text/css" href="../App_themes/AdminBlueTheme/Default.css" />')
pp.document.writeln('<link rel="stylesheet" type="text/css" href="../App_themes/AdminCommon/AdvancedAdminCommon.css" />')
pp.document.writeln('<style type="text/css" media="print">');
pp.document.writeln('#PRINT ,#CLOSE,#PRINT1 ,#CLOSE1{visibility:hidden;}');
pp.document.writeln('</style>');

//End Add style for print page!
pp.document.writeln('<base target="_self"></HEAD>');
//Adding Body Tag
pp.document.writeln('<body style="height: auto;">');
//pp.document.writeln(' leftMargin="0" topMargin="0" rightMargin="0">');
//Adding form Tag
pp.document.writeln('<form method="post">');
//Creating two buttons Print and Close within a HTML table
//Writing print area of the calling page
pp.document.writeln('<div style="width:770px;background-color:#fff;font-size:11px; margin: auto;" class="PrintPackingSlip">');
pp.document.writeln('<table align="center" width="100%">');
pp.document.writeln('<tr><td align="center">');
//window.location.reload(); *don't know why use this when print
pp.document.writeln('<INPUT ID="PRINT" type="image" src="../App_Themes/AdminBlueTheme/Images/AdminButton/Btn_Print.gif" value="Print" ');
pp.document.writeln('onclick="window.print();">');
pp.document.writeln('<INPUT ID="CLOSE" type="image" src="../App_Themes/AdminBlueTheme/Images/AdminButton/Btn_Close.gif" value="Close" onclick="window.close();">');
pp.document.writeln('</td></tr><tr><td>');

pp.document.writeln('<TABLE><TR >');
pp.document.writeln('<TD class="packingSlip">');
pp.document.writeln(printarea);
pp.document.writeln('</TD>');
pp.document.writeln('</TR></TABLE>');

//Ending Tag of </form>, </body> and </HTML>

pp.document.writeln('</td></tr><tr><td align="center">');
pp.document.writeln('<INPUT ID="PRINT1" type="image" src="../App_Themes/AdminBlueTheme/Images/AdminButton/Btn_Print.gif" value="Print" ');
pp.document.writeln('onclick="window.print();">');
pp.document.writeln('<INPUT ID="CLOSE1" type="image" src="../App_Themes/AdminBlueTheme/Images/AdminButton/Btn_Close.gif" value="Close" onclick="window.close();">');
pp.document.writeln('</td></tr></table>');
pp.document.writeln('</div>');
pp.document.writeln('</form></body></HTML>'); 
//pp.location.reload();
pp.document.close();
} 

