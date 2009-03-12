<%@ Control Language="c#" AutoEventWireup="false" Codebehind="RotatorOptions.ascx.cs" Inherits="Engage.Dnn.ContentRotator.RotatorOptions" %>
<table class="RotatorOptions">
    <tr>
        <td align="left">
            <asp:Button ID="NewContentItemButton" runat="server" resourcekey="btnNewContentItem" EnableViewState="false" />
            &nbsp;
            <asp:Button ID="BackButton" runat="server" resourcekey="lnkBack" />
        </td>
    </tr>
    <tr><td>
        <asp:Repeater ID="ContentItemsRepeater" runat="server">
            <ItemTemplate>
                <div class="divArticleToRotate">
                    <div class="topRotatorHeader">
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
                    <div class="rotatorContent">
                        <div class="rotatorDescription">
                            <div class="rotatorDescriptionTitle SubHead">
                                <asp:Label runat="server" resourcekey="lblContentHeader" />
                            </div>
                            <div class="Normal">
                                <%# Eval("Description") %>
                            </div>
                        </div>
                        <div class="rotatorThumbnails">
                            <div class="rotatorThumbnailWrapper">
                                <div class="rotatorThumbnailTitle SubHead">
                                    <asp:Label runat="server" resourcekey="lblContentThumbnailHeader" />
                                </div>
                                <div class="rotatorThumbnail">
                                    <img src='<%# Eval("ThumbnailUrl")%>' alt='<%# Eval("ThumbnailUrl")%>' />
                                </div>
                            </div>
                            <div class="rotatorPositionThumbnailWrapper">
                                <div class="rotatorPositionThumbnailTitle SubHead">
                                    <asp:Label runat="server" resourcekey="lblPositionThumbnailHeader" />
                                </div>
                                <div class="rotatorPositionThumbnail">
                                    <img src='<%# Eval("PositionThumbnailUrl") %>' alt='<%# Eval("PositionThumbnailUrl") %>' />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div class="rotatorReadMoreLinkWrapper">
                        <div class="rotatorReadMoreLink Normal">
                            <asp:Label runat="server" resourcekey="Link" /><asp:HyperLink runat="server" NavigateUrl='<%# Eval("LinkUrl") %>' Text='<%# Eval("LinkUrl") %>' />
                        </div>
                    </div>
                    <div class="bottomRotatorHeader">
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
                                <asp:Button runat="server" resourcekey="Edit" CausesValidation="false" CommandName="Edit" />&nbsp;
                                <asp:Button ID="DeleteItemButton" runat="server" resourcekey="btnDeleteItem" CausesValidation="false" CommandName="Delete" />
                                <asp:HiddenField ID="ContentItemIdHiddenField" runat="server" Value='<%# Eval("ContentItemId") %>' />
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
            <asp:Button ID="BackButton2" runat="server" resourcekey="lnkBack" CssClass="Normal" />
        </td>
    </tr>
</table>
