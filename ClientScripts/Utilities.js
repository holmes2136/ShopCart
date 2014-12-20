// JScript File

function ShowToolTip( objectParentName, objectName, e )
{
    var tooltipParent = document.getElementById( objectParentName );
    var tooltip = document.getElementById( objectName );
    tooltip.className = "showcallout";    
    var aryPosition = ObjectPosition(tooltipParent);
    
    var browser     = navigator.appName;
    var b_version   = navigator.appVersion;
    var version     = parseFloat(b_version);    
    
    //Check Browser
    //FireFox 3     browser = Netscape,                     version = 5
    //IE 6          browser = Microsoft Internet Explorer,  version = 4
    //Chrome 2      browser = Netscape,                     version = 5
    //Safari 4 Beta browser = Netscape,                     version = 5
    //Opera 9       browser = Opera,                        version = 9.64     
    
    if ( browser == "Microsoft Internet Explorer" && version == 4 )
    {
        //tooltip.style.left  =   130;
        //tooltip.style.top   =   0;  // fix for IE6  
    }
    else
    {
        tooltip.style.left  =   aryPosition[0];
        tooltip.style.top   =   aryPosition[1]; 
    }
}

function ObjectPosition(obj) {
    var curleft = 0;
      var curtop = 0;
      if (obj.offsetParent) {
            do {
                  curleft += obj.offsetLeft;
                  curtop += obj.offsetTop;
            } while (obj = obj.offsetParent);
      }
      return [curleft,curtop];
}

function HideToolTip( objectName, e )
{
    var tooltip =document.getElementById( objectName );
    tooltip.className="hidecallout";
}
function HideToolTip( objectName )
{
    var tooltip =document.getElementById( objectName );
    tooltip.className="hidecallout";
}

function format(str)
{
  for(i = 1; i < arguments.length; i++)
  {
    str = str.replace( "{" + (i-1) + "}" , arguments[i]);
  }
   return str;
}

function TrimString(sInString) {
  if ( sInString ) {
    sInString = sInString.replace( /^\s+/g, "" );// strip leading
    return sInString.replace( /\s+$/g, "" );// strip trailing
  }
}

// Populates the country selected with the counties from the country list
function populateCountry(defaultCountry, countryList, selectID, inputID, selectCountryText, otherText ) {  
  var isFound = false;
  var countryLineArray = countryList.split('|');  // Split into lines
  var selObj = document.getElementById(selectID);
  selObj.options[0] = new Option(selectCountryText,'');
  selObj.selectedIndex = 0;
  for (var loop = 0; loop < countryLineArray.length - 1; loop++) {
    lineArray = countryLineArray[loop].split(':');
    countryCode  = TrimString(lineArray[0]);
    countryName  = TrimString(lineArray[1]);
    if ( countryCode != '' ) {
      selObj.options[loop + 1] = new Option(countryName, countryCode);
    }
    if ( defaultCountry == countryCode ) {
      selObj.selectedIndex = loop + 1;
      isFound = true;
    }
  }
  
  var countryText = document.getElementById( inputID ); 
  
  selObj.options[countryLineArray.length] = new Option( otherText, "OT" );
  if ( defaultCountry != "" && !isFound ) {
      selObj.selectedIndex = countryLineArray.length;
      countryText.style.display = '';
      countryText.value = defaultCountry;
    }
    else
    {
      countryText.style.display = 'none';
    }
}

function populateState(defaultState, stateList, selectID, inputID, countrySelected, selectStateText, otherText, validateStatePanelID ) {
  var selObj = document.getElementById(selectID);
  var inputObj = document.getElementById(inputID);
  var foundState = false;
  // Empty options just in case new drop down is shorter
  if ( selObj.type == 'select-one' ) {
    for (var i = 0; i < selObj.options.length; i++) {
      selObj.options[i] = null;
    }
    selObj.options.length=null;
    selObj.options[0] = new Option(selectStateText,'');
    selObj.selectedIndex = 0;
  }
  // Populate the drop down with states from the selected country
  var stateLineArray = stateList.split("|");  // Split into lines
  var optionCntr = 1;
  var isSetState = false;

  for (var loop = 0; loop < stateLineArray.length - 1; loop++) 
  {
      lineArray = stateLineArray[loop].split(":");
      countryCode  = TrimString(lineArray[0]);
      stateCode    = TrimString(lineArray[1]);
      stateName    = TrimString(lineArray[2]);
      if (countrySelected == countryCode && countryCode != '' ) 
      {
          if ( stateCode != '' )
          {
            selObj.options[optionCntr] = new Option(stateName, stateCode);
          }
          // See if it's selected from a previous post
          if ( stateCode == defaultState && countryCode == countrySelected ) 
          {
            isSetState = true;
            selObj.selectedIndex = optionCntr;
          }
          foundState = true;
          optionCntr++
      }
  }
   
  if ( ! foundState ) 
  {
        selObj.style.display = 'none';
        inputObj.style.display = '';
        inputObj.value = defaultState;
        document.getElementById(validateStatePanelID).style.display = 'none';
  }
  else
  {
        document.getElementById(validateStatePanelID).style.display = '';
        selObj.options[selObj.options.length] = new Option( otherText, 'OT' );
      
      if( defaultState != '' && !isSetState )
      {
        selObj.style.display = '';
        selObj.selectedIndex = selObj.options.length - 1;
        inputObj.style.display = '';
        inputObj.value = defaultState;
      }
      else
      {
        selObj.style.display = '';
        inputObj.style.display = 'none';
      }
  }  
}

// This function is used in "Use Billing as Shipping" check box. 
// It will show/hide the element (shipping address div) depending on
// the status of the check box. Also, validators will be enabled/disabled
// according to the check box.

function TieCheckBoxForDisplay( checkBox, elementToDisplay, validatorList )
{   
    if ( checkBox.checked )
        document.getElementById( elementToDisplay ).style.display = 'block';
    else
        document.getElementById( elementToDisplay ).style.display = 'none';
    
    var validatorListArray = validatorList.split("|");
    for (var loop = 0; loop < validatorListArray.length ; loop++) 
    {
       ValidatorEnable(  document.getElementById( validatorListArray[loop] ), checkBox.checked );
    }
}

function TieCheckBoxForNonDisplay( checkBox, elementToDisplay, validatorList ) 
{
    if (checkBox.checked)
        document.getElementById(elementToDisplay).style.display = 'none';
    else
        document.getElementById(elementToDisplay).style.display = 'block';

    var validatorListArray = validatorList.split("|");
    for (var loop = 0; loop < validatorListArray.length; loop++) 
    {
        ValidatorEnable(document.getElementById(validatorListArray[loop]), !checkBox.checked);
    }
}

function EnableDisableValidator( validatorList, isEnable )
{   
    var validatorListArray = validatorList.split("|");
    for (var loop = 0; loop < validatorListArray.length ; loop++) 
    {
       ValidatorEnable(  document.getElementById( validatorListArray[loop] ), isEnable );
    }
}

function ClearFormElements(ele) {

    tags = ele.getElementsByTagName('input');
    for(i = 0; i < tags.length; i++) {
        switch(tags[i].type) {
            case 'password':
            case 'text':
                tags[i].value = '';
                break;
            case 'checkbox':
            case 'radio':
                tags[i].checked = false;
                break;
        }
    }
   
    tags = ele.getElementsByTagName('select');
    for(i = 0; i < tags.length; i++) {
        if(tags[i].type == 'select-one') {
            tags[i].selectedIndex = 0;
        }
        else {
            for(j = 0; j < tags[i].options.length; j++) {
                tags[i].options[j].selected = false;
            }
        }
    }

    tags = ele.getElementsByTagName('textarea');
    for(i = 0; i < tags.length; i++) {
        tags[i].value = '';
    }
   
}

