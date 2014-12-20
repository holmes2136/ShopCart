<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LargeBulkImport.aspx.cs"
    Inherits="AdminAdvanced_LargeBulkImport" %>
<%@ Register Src="Components/Common/Upload.ascx" TagName="Upload" TagPrefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bulk Import</title>
</head>
<body style="background-color: #ffffff;">
    <form id="form1" runat="server">
        <div style="width: 650px;">
            <h3>Bulk Import</h3>
            <asp:Label ID="uxMessageLabel" runat="server" ForeColor="Red"></asp:Label><br />
            <asp:Label ID="uxTimeLabel" runat="server" ForeColor="Green"></asp:Label><br />
        <div>
            Type in File Name         &nbsp;        &nbsp;        &nbsp;
            <asp:TextBox Width="300" ID="uxFileNameText" runat="server"></asp:TextBox>
        </div>
        
        <div>
            <asp:Label ID="uxSampleLabel" runat="server" Text="<i>( Example : import\xxxxx.csv</i> )"></asp:Label>
        </div>
        <br />
        <div >
            Language           &nbsp;        &nbsp;        &nbsp;
            <asp:DropDownList ID="uxLanguageDrop" runat="server" DataSourceID="uxCultureSource"
                DataTextField="DisplayName" DataValueField="CultureID" CssClass="fl DropDown">
            </asp:DropDownList>
           
        </div>
        <br />
        <asp:Panel ID="uxStorePanel" runat="server" >
            Store             &nbsp;        &nbsp;        &nbsp;
            <asp:DropDownList ID="uxStoreDrop" runat="server" CssClass="fl DropDown" />
         </asp:Panel>
         <br />
        <div>
            Import Mode             
            <asp:RadioButtonList ID="uxModeRadioList" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="Purge" Selected="True">Purge All</asp:ListItem>
                <asp:ListItem Value="Overwrite">Overwrite Existing Data</asp:ListItem>
            </asp:RadioButtonList>

        </div>
        <br />
        <div >
                Update Images and Thumbnails        &nbsp;        &nbsp;        &nbsp;
   
            <asp:CheckBox ID="uxImageProcessCheck" runat="server"  />

        </div>
        <div >

             <p style="color: Red;">
                * If "No", image-related fields from CSV will be ignored. This can speed up the
                Bulk Import process.
            </p>
            <div class="Clear">
            </div>
        </div>
        
        <div >
               &nbsp;        &nbsp; - Skip Image Processing        &nbsp;        &nbsp;        &nbsp;
   
            <asp:CheckBox ID="uxSkipImageProcessingCheck" runat="server"  />

        </div>
        <div >

             <p style="color: Red;">
                &nbsp;        &nbsp; * If this check box is checked, Image file path will be added to database but image processing will be skipped. 
                It needs to upload regular, thumbnail and large image files manually. 
            </p>
            <div class="Clear">
            </div>
        </div>
        
       <br />
       <asp:Button ID="uxExecuteButton" runat="server" Text="Import" OnClick="uxExecuteButton_Click" Width="144px" /></div>
       
       <br />
       <br />
              <asp:Button ID="uxDelButton" runat="server" Text="Delete All Products" OnClick="uxDelButton_Click" /></div>

        </form>
</body>
</html>
<asp:ObjectDataSource ID="uxCultureSource" runat="server" SelectMethod="GetAll" TypeName="Vevo.Domain.DataSources.CultureDataSource">
</asp:ObjectDataSource>
       
   
