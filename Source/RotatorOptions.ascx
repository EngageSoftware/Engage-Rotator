<%@ Control Language="c#" AutoEventWireup="false" Codebehind="RotatorOptions.ascx.cs" Inherits="Engage.Dnn.ContentRotator.RotatorOptions" %>
<%@ Import Namespace="Globals=DotNetNuke.Common.Globals" %>
<%@ Import Namespace="Engage.Dnn.ContentRotator" %>
<%@ Register TagPrefix="dnn" TagName="UrlTracking" Src="~/controls/URLTrackingControl.ascx" %>
<div class="engageRotatorOptions">
    <ul class="top-buttons dnnActions dnnClear">
        <li><asp:Button ID="NewSlideButton" runat="server" ResourceKey="NewSlideButton" EnableViewState="false" CssClass="dnnPrimaryAction" /></li>
        <li><asp:Button ID="BackButton" runat="server" ResourceKey="BackButton" CssClass="dnnSecondaryAction" /></li>
    </ul>
    <asp:Repeater ID="SlidesRepeater" runat="server">
        <HeaderTemplate><ol class="slides dnnClear"></HeaderTemplate>
        <ItemTemplate>
            <li class="slide">
                <div class="slide-header">
                    <h3 class="slide-title"><%# Eval("Title") %></h3>
                    <span class="slide-order"><h6><%=Localize("SortOrder")%></h6> <%#Eval("SortOrder") %></span>
                </div>
                <div class="slide-body">
                    <div class="slide-body-box <%=IsContentInTemplate && IsImageInTemplate ? "both" : "single" %>">
                        <asp:Panel runat="server" CssClass="slide-content" Visible="<%#IsContentInTemplate %>">
                            <h6><%=Localize("ContentHeader")%></h6>
                            <div class="content-wrap"><%# Eval("Content") %></div>
                        </asp:Panel>
                        <asp:Panel runat="server" CssClass="slide-image" Visible="<%#IsImageInTemplate %>">
                            <h6><%=Localize("ImageHeader")%></h6>
                            <img src='<%# Eval("ImageUrl") %>' alt='<%# Eval("ImageUrl") %>' />
                        </asp:Panel>
                    </div>
                    <asp:Panel runat="server" CssClass="slide-link" Visible="<%#IsLinkInTemplate %>">
                        <h6><%=Localize("Link")%></h6>
                        <asp:PlaceHolder runat="server" Visible='<%# !string.IsNullOrEmpty((string)Eval("Link")) %>'>
                            <asp:HyperLink runat="server" NavigateUrl='<%# Eval("LinkUrl") %>' Text='<%#GetPlainUrl((string)Eval("Link")) %>' />
                                    
                            <asp:PlaceHolder runat="server" Visible='<%#(bool)Eval("TrackLink") %>'>
                                <fieldset class="urlTrackingWrap">
                                    <legend><a href="#" class="view-url-tracking"><%= HttpUtility.HtmlEncode(Localize("View Link Statistics")) %></a></legend>
                                    <asp:HiddenField runat="server" Value="false" />
                                    <div class="urlTracking">
                                        <dnn:UrlTracking runat="server" URL='<%# Eval("Link") %>' FormattedURL='<%# GetPlainUrl((string)Eval("Link")) %>' ModuleID='<%# ModuleId %>' />
                                    </div>
                                </fieldset>
                            </asp:PlaceHolder>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" Visible='<%# string.IsNullOrEmpty((string)Eval("Link")) %>'>
                            <%= Localize("No Link") %>
                        </asp:PlaceHolder>
                    </asp:Panel>
                </div>
                <div class="slide-footer">
                    <ol class="slide-dates">
                        <li><h6><%=Localize("Starts")%></h6> <%#Eval("StartDate", "{0:d}") %></li>
                        <asp:PlaceHolder runat="server" Visible='<%#Eval("EndDate") != null %>'>
                            <li><h6><%=Localize("Ends")%></h6> <%#Eval("EndDate", "{0:d}") %></li>
                        </asp:PlaceHolder>
                    </ol>
                    <ul class="slide-buttons dnnActions dnnClear">
                        <li><asp:Button runat="server" CssClass="dnnPrimaryAction" ResourceKey="Edit" CausesValidation="false" CommandName="Edit" /></li>
                        <li><asp:Button ID="DeleteSlideButton" CssClass="dnnSecondaryAction" runat="server" ResourceKey="DeleteSlideButton" CausesValidation="false" CommandName="Delete" /></li>
                    </ul>
                    <asp:HiddenField ID="SlideIdHiddenField" runat="server" Value='<%# Eval("SlideId") %>' />
                </div>
            </li>
        </ItemTemplate>
        <FooterTemplate></ol></FooterTemplate>
    </asp:Repeater>
    <asp:PlaceHolder ID="NoSlidesSection" runat="server" Visible="false">
        <%= Localize("No Slides") %>
    </asp:PlaceHolder>
    
    <ul class="bottom-buttons dnnActions dnnClear">
        <li><asp:Button ID="BackButton2" runat="server" ResourceKey="BackButton" CssClass="dnnSecondaryAction" /></li>
    </ul>
</div>
<script type="text/javascript" src="<%= GetRotatorOptionsScriptUrl() %>"></script>
<script type="text/javascript">
    jQuery(function ($) {
        $('.slide-link').toggleUrlTracking();
    });
</script>