<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImageList.ascx.cs" Inherits="AdminAdvanced_Components_Products_ImageList" %>
<%@ Register Src="../Template/AdminUserControlContent.ascx" TagName="AdminUserControlContent"
    TagPrefix="uc1" %>
<%@ Register Src="~/Components/Upload/Upload.ascx" TagName="Upload" TagPrefix="uc1" %>
<%@ Register Src="../Message.ascx" TagName="Message" TagPrefix="uc2" %>
<uc1:AdminUserControlContent ID="uxAdminUserControlContent" runat="server">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <PlainContentTemplate>
        <div class="ProductImageUpload" id="uxProductImageUploadPanel" runat="server">
            <uc1:Upload ID="uxUpload" runat="server" UploadDirectory="Images/Products" UploadFilePageName="Upload.aspx" />
        </div>
        <div class="ProductImageDenote">
            <asp:Image ID="uxPrimarImage" SkinID="PrimarImage" runat="server" />
            <asp:Label ID="lcPrimaryImageMessageLabel" runat="server" meta:resourcekey="lcPrimaryImageMessageLabel"/>
            <div class="Clear">
            </div>
        </div>
        <div class="CommonAdminBorder">
            <asp:GridView ID="uxGridProductImage" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
                AllowSorting="<%# IsSorting() %>" OnRowCreated="SetFooter" ShowFooter="True"
                OnSorting="uxGrid_Sorting" OnRowDataBound="uxGrid_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="<%$ Resources:ProductImageFields, Image %>" SortExpression="LargeImage">
                        <ItemTemplate>
                            <asp:HiddenField ID="uxImageIDHidden" Value='<%# Bind("ProductImageID") %>' runat="server" />
                            <table style="width: 90%">
                                <tr>
                                    <td style="width: 18px;">
                                        <asp:Image ID="uxPrimarImage" SkinID="PrimarImage" runat="server" Visible='<%# IsPrimaryImage(Eval("SortOrder")) %>' />
                                    </td>
                                    <td>
                                        <asp:Image ID="uxAttachImage" SkinID="AttachImage" runat="server" />
                                        <asp:Label ID="uxImageNameLabel" runat="server" Text='<%# GetImageName( Eval( "LargeImage" ).ToString() )%>' />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="left" />
                        <HeaderStyle HorizontalAlign="Left" CssClass="pdl25" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:ProductImageFields, AltTag %>">
                        <ItemTemplate>
                            <asp:TextBox Width="120px" ID="uxAltTag" runat="server" Text='<%# Eval( "AltTag" )%>'
                                CssClass="TextBox"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" CssClass="pdl10" Width="130px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:ProductImageFields, TitleTag %>">
                        <ItemTemplate>
                            <asp:TextBox Width="120px" ID="uxTitleTag" runat="server" Text='<%# Eval( "TitleTag" )%>'
                                CssClass="TextBox"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" CssClass="pdl10" Width="130px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:ProductImageFields, Size %>" SortExpression="ImageSize">
                        <ItemTemplate>
                            <%# Eval( "ImageSize" )%>
                            KB.</ItemTemplate>
                        <ItemStyle HorizontalAlign="right" CssClass="pdr10" Width="70px" />
                        <FooterStyle HorizontalAlign="right" CssClass="pdr10" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="uxRemove" runat="server" Text="<%$ Resources:ProductImageFields, Remove %>"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="uxRemoveImageButton" runat="server" Visible='<%# IsAdminModifiable() %>'
                                OnClick="uxRemoveImageButton_Click" OnPreRender="uxRemoveImageButton_PreRender"
                                CommandArgument='<%# Eval("ProductImageID").ToString() %>'>
                                <asp:Image ID="uxRemoveImage" runat="server" SkinID="RemoveImage" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsZoom">
                        <HeaderTemplate>
                            <input type="CheckBox" name="SelectAllZoomCheckBox" onclick="SelectAllCheckBoxes(this , 'uxZoomCheck')" />
                            <asp:Label ID="uxZoom" runat="server" Text="<%$ Resources:ProductImageFields, Zoom %>"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="uxZoomCheck" runat="server" Checked='<%# Eval("IsZoom") %>' Enabled='<%# IsZoomableSize( Eval( "ImageWidth" ), Eval( "ImageHeight" ) )  %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsEnlarge">
                        <HeaderTemplate>
                            <input type="CheckBox" name="SelectAllEnlargeCheckBox" onclick="SelectAllCheckBoxes(this , 'uxEnlargeCheck')" />
                            <asp:Label ID="uxEnlarge" runat="server" Text="<%$ Resources:ProductImageFields, Enlarge %>"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="uxEnlargeCheck" runat="server" Checked='<%# Eval("IsEnlarge") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="uxSetAsPrimayImageButton" runat="server" Visible='<%# !IsPrimaryImage( Eval( "SortOrder" ) ) && IsAdminModifiable() %>'
                                OnClick="uxSetAsPrimayImageButton_Click" CommandArgument='<%# Eval("ProductImageID").ToString() %>'>
                                <asp:Image ID="uxSetAsPrimayImage" runat="server" SkinID="SetAsPrimayImage" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:ProductImageFields, TableEmpty  %>"></asp:Label>
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:DataList ID="uxThumbnailDataList" runat="server" Width="100%" CssClass="BlockCenter mgt20"
                RepeatDirection="Horizontal" RepeatColumns="5" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <table cellpadding="5" border="0">
                        <tr>
                            <td align="center">
                                <table cellpadding="5" border="0">
                                    <tr>
                                        <td style="text-align: center;" class="ImageBorder">
                                            <asp:HiddenField ID="uxImageIDHidden" Value='<%# Bind("ProductImageID") %>' runat="server" />
                                            <asp:Image ID="uxThumbImage" runat="server" ImageUrl='<%# ImageUrl(Eval("ThumbnailImage").ToString()) %>'
                                                Width="70px" Height="70px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="uxThumbnailNameLabel" runat="server" Text='<%# GetImageName( Eval( "ThumbnailImage" ).ToString() ) %>'></asp:Label>
                            </td>
                        </tr>
                        <tr id="uxPrimaryTR" runat="server" visible='<%# IsPrimaryImage( Eval( "SortOrder" ) ) %>'>
                            <td align="center">
                                <asp:Image ID="uxPrimarImage" SkinID="PrimarImage" runat="server" />
                            </td>
                        </tr>
                        <tr id="uxSortingTR" runat="server" visible='<%# !IsPrimaryImage( Eval( "SortOrder" ) ) %>'>
                            <td align="center" class="SortTextBox">
                                <asp:TextBox ID="uxSortOrderText" Text='<%# Eval( "SortOrder" ) %>' runat="server"
                                    Width="30px" CssClass="TextBox">
                                </asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <ItemStyle Width="20%" />
            </asp:DataList></div>
        <asp:HiddenField ID="uxStatusHidden" runat="server" />
        <asp:HiddenField ID="uxSecondaryImageHidden" runat="server" />
        <div class="Clear">
        </div>
    </PlainContentTemplate>
</uc1:AdminUserControlContent>
