<%@ Control Language="c#" AutoEventWireup="True" Codebehind="RotatorOptions.ascx.cs" Inherits="Engage.Dnn.ContentRotator.RotatorOptions" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<table>
    <tr>
        <td align="left">
            <asp:Button ID="btnNewContentItem" runat="server" resourcekey="btnNewContentItem" OnClick="btnNewContentItem_Click" EnableViewState="false" />
            &nbsp;
            <asp:Button ID="btnBack" runat="server" resourcekey="lnkBack" OnClick="btnBack_Click" />
        </td>
    </tr>
    <tr><td>
        <asp:Repeater ID="rpContentItems" runat="server" OnItemDataBound="rpContentItems_ItemDataBound" OnItemCommand="rpContentItems_ItemCommand">
            <ItemTemplate>
                <div id="divArticleToRotate">
                    <div id="topRotatorHeader">
                        <div class="lt-corner">
                        </div>
                        <div class="rotatorHeaderWrapper">
                            <div class="sortOrder Normal">
                                <asp:Label runat="server" resourcekey="lblOrderLabel" /> (<%#Eval("SortOrder") %>)
                            </div>
                            <div class="rotatorContentTitleWrapper SubHead">
                                <%# Eval("Title") %>
                            </div>
                        </div>
                        <div class="rt-corner">
                        </div>
                    </div>
                    <div id="rotatorContent">
                        <div class="rotatorDescription">
                            <div class="rotatorDescriptionTitle SubHead">
                                <asp:Label runat="server" resourcekey="lblContentHeader" />
                            </div>
                            <div class="Normal">
                                <%# Eval("Description") %>
                            </div>
                        </div>
                        <div id="rotatorThumbnails">
                            <div class="rotatorThumbnailWrapper">
                                <div class="rotatorThumbnailTitle SubHead">
                                    <asp:Label runat="server" resourcekey="lblContentThumbnailHeader" />
                                </div>
                                <div class="rotatorThumbnail">
                                    <img style='<%=ThumbnailStyle%>' src='<%# Eval("ThumbnailUrl")%>' alt='<%# Eval("ThumbnailUrl")%>' />
                                </div>
                            </div>
                            <div class="rotatorPositionThumbnailWrapper">
                                <div class="rotatorPositionThumbnailTitle SubHead">
                                    <asp:Label runat="server" resourcekey="lblPositionThumbnailHeader" />
                                </div>
                                <div class="rotatorPositionThumbnail">
                                    <img style='<%=PositionThumbnailStyle %>' src='<%# Eval("PositionThumbnailUrl") %>' alt='<%# Eval("PositionThumbnailUrl") %>' />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div id="rotatorReadMoreLinkWrapper">
                        <div class="rotatorReadMoreLink Normal">
                            <asp:Label runat="server" resourcekey="Link" /><asp:HyperLink ID="lnkReadMore" runat="server" NavigateUrl='<%# Eval("LinkUrl") %>' Text='<%# Eval("LinkUrl") %>' />
                        </div>
                    </div>
                    <div id="bottomRotatorHeader">
                        <div class="lb-corner">
                        </div>
                        <div class="editContent">
                            <div class="startEndDate Normal">
                                <asp:Label runat="server" resourcekey="Starts" />
                                <%#Eval("StartDate", "{0:d}") %>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Label runat="server" resourcekey="Ends" />
                                <%#Eval("EndDate", "{0:d}") %>
                            </div>
                            <div class="editButtons Normal">
                                <asp:Button ID="btnEdit" runat="server" resourcekey="Edit" CausesValidation="false" CommandName="Edit" />&nbsp;
                                <asp:Button ID="btnDeleteItem" runat="server" resourcekey="btnDeleteItem" CausesValidation="false" CommandName="Delete" />
                                <ajaxToolkit:ConfirmButtonExtender ID="ajaxConfirm" runat="server" ConfirmText="DeleteConfirm.Text" TargetControlID="btnDeleteItem" />
                                <asp:HiddenField ID="hfContentItemId" runat="server" Value='<%# Eval("ContentItemId") %>' />
                            </div>
                        </div>
                        <div class="rb-corner">
                        </div>
                    </div>
                </div>
                <div style="clear: both;">
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </td></tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnBack2" runat="server" resourcekey="lnkBack" OnClick="btnBack_Click" CssClass="Normal" />
        </td>
    </tr>
</table>
