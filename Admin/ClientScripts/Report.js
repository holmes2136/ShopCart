// JScript File

function CreateChart(period, report, startdate, enddate, browser, isDisplayTitle, storeID){
    swfobject.embedSWF("ClientScripts/OpenFlashChart/open-flash-chart.swf", "my_chart", "100%", "300", "9.0.0", "expressInstall.swf",
    { "data-file": "Components/Report/SaleReportData.aspx?period=" + period + "%26report=" + report + "%26startdate=" + startdate + "%26enddate=" + enddate + "%26IsDisplayTitle=" + isDisplayTitle + "%26storeID=" + storeID});
    if (browser != "IE")
    {
        var x = document.getElementById( "my_chart" );
        x.setAttribute("wmode", "transparent");
    }
}
function CreateCustomerChart(period, report, startdate, enddate, browser){
    swfobject.embedSWF("ClientScripts/OpenFlashChart/open-flash-chart.swf", "my_chart", "100%", "300", "9.0.0", "expressInstall.swf",
    { "data-file": "Components/Report/CustomerReportData.aspx?period=" + period + "%26report=" + report + "%26startdate=" + startdate + "%26enddate=" + enddate });
    if (browser == "Firefox")
    {
        var x = document.getElementById( "my_chart" );
        x.setAttribute("wmode", "transparent");
    }
}
function CreatePaymentChart(period,startdate,enddate,browser){
    swfobject.embedSWF("ClientScripts/OpenFlashChart/open-flash-chart.swf", "my_chart", "100%", "330", "9.0.0", "expressInstall.swf",
    {"data-file":"Components/Report/PaymentReportData.aspx?period=" + period +"%26startdate=" + startdate + "%26enddate=" + enddate } );    
    if (browser == "Firefox")
    {
        var x = document.getElementById( "my_chart" );
        x.setAttribute("wmode", "transparent");
    }
}
function CreateShippingChart(period,startdate,enddate,browser){
    swfobject.embedSWF("ClientScripts/OpenFlashChart/open-flash-chart.swf", "my_chart", "100%", "330", "9.0.0", "expressInstall.swf",
    {"data-file":"Components/Report/ShippingReportData.aspx?period=" + period +"%26startdate=" + startdate + "%26enddate=" + enddate } );    
    if (browser == "Firefox")
    {
        var x = document.getElementById( "my_chart" );
        x.setAttribute("wmode", "transparent");
    }
}
function GetCustomDate(period, id){
    object = document.getElementById(id);                        
    if (period == "Custom")
    {
        object.style.display = "block";
    }
    else
    {
        object.style.display = "none";
    }    
}
function GetCustomerRegister(report, id){
    object = document.getElementById(id);
    if (report != "Customer Registration")
    {
        object.style.display = "block";
    }
    else
    {
        object.style.display = "none";
    }    
}