// UtilScript.js
// -----------------
// Utilities functions

function SelectAllCheckBoxes(CheckBoxControl , GridName) 
{
    if (CheckBoxControl.checked == true) 
    {
        var i;
        for (i=0; i < document.forms[0].elements.length; i++) 
        {
            if ((document.forms[0].elements[i].type == 'checkbox') && 
            (document.forms[0].elements[i].name.indexOf(GridName) > -1) &&
            !document.forms[0].elements[i].disabled ) 
            {
                document.forms[0].elements[i].checked = true;
            }
        }
     } 
    else 
    {
        var i;
        for (i=0; i < document.forms[0].elements.length; i++) 
        {
            if ((document.forms[0].elements[i].type == 'checkbox') && 
            (document.forms[0].elements[i].name.indexOf(GridName) > -1) &&
            !document.forms[0].elements[i].disabled ) 
            {
                document.forms[0].elements[i].checked = false;
            }
        }
    }
 }
 
 function ShowImageInDiv( elementID, indexImageShow )
 {
    var images = document.getElementById( elementID ).getElementsByTagName("img");
    for(var img_index = 0; img_index < images.length; img_index++)
    {
        if (img_index == indexImageShow)        
            images[img_index].style.display = "block";        
        else
            images[img_index].style.display = "none";
    }
 }
  
 function ResizeListBox( listBoxControl, divID, defaultItem )
 {
    var uxDiv = document.getElementById( divID );
    if( listBoxControl.options.length < defaultItem )
    {
       listBoxControl.size = defaultItem; 
    }
    else
    {
       listBoxControl.size = listBoxControl.options.length;
    }
        
        
    SortOptions( listBoxControl );
    listBoxControl.style.width = '';
    var widthDiv;
    if(listBoxControl.clientWidth <= uxDiv.clientWidth )
    {
        result = uxDiv.clientWidth;        
        listBoxControl.style.width = result + 'px';
        result -= 16;
        widthDiv = result + 'px';
    }
    else
    {
        listBoxControl.style.width = '';
        widthDiv = listBoxControl.clientWidth + 'px';
    }
    
    var browser=navigator.appName;
    
    if( browser != "Microsoft Internet Explorer" )
    {
        var uxDivTemp = uxDiv.getElementsByTagName( 'div' ).item(0);
        uxDivTemp.style.width = widthDiv;
        uxDivTemp.style.overflow = 'hidden';
        uxDiv.style.border = 'solid 1px #cccccc';
    }
 }
 
 function fnMoveItems(lstbxFrom,lstbxTo, divFromID, divToID, defaultItem, hiddenFieldID, controlIDSerialized)
 {
     var varFromBox = document.getElementById(lstbxFrom);
     var varToBox = document.getElementById(lstbxTo); 
     if ((varFromBox != null) && (varToBox != null)) 
     { 
          if(varFromBox.length < 1) 
          {
           alert('There are no items in the source ListBox');
           return false;
          }
          if(varFromBox.options.selectedIndex == -1) // when no Item is selected the index will be -1
          {
           alert('Please select an Item to move');
           return false;
          }
          while ( varFromBox.options.selectedIndex >= 0 ) 
          { 
               var newOption = new Option(); // Create a new instance of ListItem 

               newOption.text = varFromBox.options[varFromBox.options.selectedIndex].text; 
               newOption.value = varFromBox.options[varFromBox.options.selectedIndex].value; 
               varToBox.options[varToBox.length] = newOption; //Append the item in Target Listbox

               varFromBox.remove(varFromBox.options.selectedIndex); //Remove the item from Source Listbox 

          }
                    
          SaveToHiddenField( hiddenFieldID, Serialized(document.getElementById(controlIDSerialized)) );
     }
     return false; 
 } 
 
 function Serialized( listBoxControl )
 {
    var result = '';
    
    for(var i = 0; i < listBoxControl.length; i++)
    {
        if( i != 0 )
            result += ',';
        
        result += listBoxControl.options[i].value;
    }
    return result;
 }
 
 function SaveToHiddenField( hiddenID, val )
 {
    var hidden = document.getElementById( hiddenID );
    hidden.value = val;
 }
 
 function compareOptionText(a,b)
 {
    return a.text!=b.text ? a.text<b.text ? -1 : 1 : 0;
 }
 
 function SortOptions(list)
 {
    var items = list.options.length;
    // create array and make copies of options in list
    var tmpArray = new Array(items);
    for ( i=0; i<items; i++ )
        tmpArray[i] = new Option(list.options[i].text,list.options[i].value);
    // sort options using given function
    tmpArray.sort(compareOptionText);
    // make copies of sorted options back to list
    for ( i=0; i<items; i++ )
        list.options[i] = new Option(tmpArray[i].text,tmpArray[i].value);
 }
 
function LayoutOverrideChange( obj, objRadioPanel, objCategoryPanel, objProductListPanel, objHiddenStatus )
{
    if ( obj.value == "False" )
    {
        document.getElementById( objRadioPanel ).style.display = "none";
        document.getElementById( objCategoryPanel ).style.display = "none";
        document.getElementById( objProductListPanel ).style.display = "none";
    }
    else
    {
        if (document.getElementById( objHiddenStatus ).value == "")
            document.getElementById( objRadioPanel ).style.display = "";                
        else             
            document.getElementById( objRadioPanel ).style.display = "none";            
       
        ShowSubPanel( objRadioPanel, objCategoryPanel, objProductListPanel ); 
    }
} 

function ShowHideObject( objName )
{
    var obj = document.getElementById( objName );
    if ( obj.style.display == "" )
        obj.style.display = "none";
    else
        obj.style.display =  "";
}

function ShowSubPanel( objName, objCategoryPanel, objProductListPanel )
{
    var obj = document.getElementById( objName );

    tags = obj.getElementsByTagName('input');
    for(i = 0; i < tags.length; i++) {
        if ( tags[i].type == "radio" && tags[i].checked )
        {   
            if ( tags[i].value == "Product" )
            {
                document.getElementById( objCategoryPanel ).style.display = "none";
                document.getElementById( objProductListPanel ).style.display = "";                          
            }
            else
            {
                document.getElementById( objCategoryPanel ).style.display = "";
                document.getElementById( objProductListPanel ).style.display = "none";
            }  
        }
    }
}


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