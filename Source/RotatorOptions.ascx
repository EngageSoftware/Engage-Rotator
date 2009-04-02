<%@ Control Language="c#" AutoEventWireup="false" Codebehind="RotatorOptions.ascx.cs" Inherits="Engage.Dnn.ContentRotator.RotatorOptions" %>
<table class="RotatorOptions">
    <tr>
        <td align="left">
            <asp:Button ID="NewSlideButton" runat="server" resourcekey="NewSlideButton" EnableViewState="false" />
            &nbsp;
            <asp:Button ID="BackButton" runat="server" resourcekey="BackButton" />
        </td>
    </tr>
    <tr><td>
        <asp:Repeater ID="SlidesRepeater" runat="server">
            <ItemTemplate>
                <div class="divArticleToRotate">
                    <div class="topRotatorHeader">
                        <div class="lt-corner">
                        </div>
                        <div class="rotatorHeaderWrapper">
                            <div class="sortOrder Normal">
                                <asp:Label runat="server" resourcekey="SortOrder" /> (<%#Eval("SortOrder") %>)
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
                                <asp:Label runat="server" resourcekey="ContentHeader" />
                            </div>
                            <div class="Normal">
                                <%# Eval("Content") %>
                            </div>
                        </div>
                        <div class="rotatorThumbnails">
                            <div class="rotatorThumbnailWrapper">
                                <div class="rotatorThumbnailTitle SubHead">
                                    <asp:Label runat="server" resourcekey="ImageHeader" />
                                </div>
                                <div class="rotatorThumbnail">
                                    <img src='<%# Eval("ImageUrl")%>' alt='<%# Eval("ImageUrl")%>' />
                                </div>
                            </div>
                            <div class="rotatorPositionThumbnailWrapper">
                                <div class="rotatorPositionThumbnailTitle SubHead">
                                    <asp:Label runat="server" resourcekey="PagerImageHeader" />
                                </div>
                                <div class="rotatorPositionThumbnail">
                                    <img src='<%# Eval("PagerImageUrl") %>' alt='<%# Eval("PagerImageUrl") %>' />
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
                                <asp:Button ID="DeleteSlideButton" runat="server" resourcekey="DeleteSlideButton" CausesValidation="false" CommandName="Delete" />
                                <asp:HiddenField ID="SlideIdHiddenField" runat="server" Value='<%# Eval("SlideId") %>' />
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
            <asp:Button ID="BackButton2" runat="server" resourcekey="BackButton" CssClass="Normal" />
        </td>
    </tr>
</table>
