<%@ Control Language="c#" AutoEventWireup="false" Codebehind="RotatorOptions.ascx.cs" Inherits="Engage.Dnn.ContentRotator.RotatorOptions" %>
<%@ Import Namespace="Globals=DotNetNuke.Common.Globals" %>
<%@ Register TagPrefix="dnn" TagName="UrlTracking" Src="~/controls/URLTrackingControl.ascx" %>
<div class="RotatorOptions">
    <div class="ro-top"><asp:Button ID="NewSlideButton" runat="server" resourcekey="NewSlideButton" EnableViewState="false" />&nbsp;<asp:Button ID="BackButton" runat="server" resourcekey="BackButton" /></div>            
    <div class="ro-body">
        <asp:Repeater ID="SlidesRepeater" runat="server">
            <ItemTemplate>
                    <div class="divArticleToRotate">
                        <div class="topRotatorHeader">
                            <div class="sortOrder Normal"><asp:Label runat="server" resourcekey="SortOrder" /> (<%#Eval("SortOrder") %>)</div>
                            <div class="rotatorContentTitleWrapper SubHead"><%# Eval("Title") %></div>
                        </div>
                        <div class="rotatorContent">
                            <div class="rotatorDescription">
                                <div class="rotatorDescriptionTitle SubHead"><asp:Label runat="server" resourcekey="ContentHeader" /></div>
                                <div class="Normal"><%# Eval("Content") %></div>
                            </div>
                            <div class="rotatorThumbnails">
                                <div class="rotatorThumbnailWrapper">
                                    <div class="rotatorThumbnailTitle SubHead"><asp:Label runat="server" resourcekey="ImageHeader" /></div>
                                    <div class="rotatorThumbnail"><img src='<%# Eval("ImageUrl") %>' alt='<%# Eval("ImageUrl") %>' /></div>
                                </div>
                                <div class="rotatorPositionThumbnailWrapper">
                                    <div class="rotatorPositionThumbnailTitle SubHead"><asp:Label runat="server" resourcekey="PagerImageHeader" /></div>
                                    <div class="rotatorPositionThumbnail"><img src='<%# Eval("PagerImageUrl") %>' alt='<%# Eval("PagerImageUrl") %>' /></div>
                                </div>
                            </div>
                            <div class="rotatorReadMoreLink Normal">
                                <asp:Label runat="server" resourcekey="Link" />
                                <asp:PlaceHolder runat="server" Visible='<%# !string.IsNullOrEmpty((string)Eval("LinkUrl")) %>'>
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
                                <asp:PlaceHolder runat="server" Visible='<%# string.IsNullOrEmpty((string)Eval("LinkUrl")) %>'>
                                    <%= Localize("No Link") %>
                                </asp:PlaceHolder>
                            </div>
                        </div>
                        <div class="editContent">
                            <div class="startEndDate Normal">
                                <asp:Label runat="server" resourcekey="Starts" CssClass="NormalBold" />
                                <%#Eval("StartDate", "{0:d}") %>&nbsp;&nbsp;&nbsp;
                                <asp:Label runat="server" resourcekey="Ends" CssClass="NormalBold" />
                                <%#Eval("EndDate", "{0:d}") %>
                            </div>
                            <div class="editButtons Normal">
                                <asp:Button runat="server" resourcekey="Edit" CausesValidation="false" CommandName="Edit" />&nbsp;
                                <asp:Button ID="DeleteSlideButton" runat="server" resourcekey="DeleteSlideButton" CausesValidation="false" CommandName="Delete" />
                                <asp:HiddenField ID="SlideIdHiddenField" runat="server" Value='<%# Eval("SlideId") %>' />
                            </div>
                        </div>
                    </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="ro-bottom"><asp:Button ID="BackButton2" runat="server" resourcekey="BackButton" CssClass="Normal" /></div>

</div>
<script type="text/javascript">
    jQuery(function ($) {
        var isEnclosingSectionOpen = function ($elem) {
            return $elem.closest('.rotatorReadMoreLink').find('input[type="hidden"]').val() === 'true';
        };
        
        $('.urlTracking').filter(function () {
            return !isEnclosingSectionOpen($(this));
        }).hide();

        $('.view-url-tracking')
            .filter(function () {
                return !isEnclosingSectionOpen($(this));
            }).addClass('expand-link')
            .end().filter(function () {
                return isEnclosingSectionOpen($(this));
            }).addClass('collapse-link')
            .end().click(function (event) {
                var $viewUrlTrackingLink = $(this),
                    $wrappingSection = $viewUrlTrackingLink.closest('.rotatorReadMoreLink')
                    $isOpenedHiddenField = $wrappingSection.find('input[type="hidden"]'),
                    $urlTrackingSection = $wrappingSection.find('.urlTracking');

                event.preventDefault();
                $urlTrackingSection.slideToggle();
                $viewUrlTrackingLink.toggleClass('expand-link collapse-link');
                $isOpenedHiddenField.val($viewUrlTrackingLink.hasClass('collapse-link'));
        });
    });
</script>