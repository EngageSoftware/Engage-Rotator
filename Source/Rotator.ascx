<%@ Import namespace="System.Globalization"%>
<%@ Import namespace="DotNetNuke.Services.Localization"%>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Rotator.ascx.cs" Inherits="Engage.Dnn.ContentRotator.Rotator" %>
<%@ Import Namespace="Engage.Dnn.ContentRotator" %>
<asp:PlaceHolder ID="phStyleTemplate" runat="server"/>
<div id="divRotator" runat="server" class="rotatorWrapper">
	<div id="articleRotator" class="rotator" runat="server">
		<asp:Repeater Runat="server" ID="rpArticles" OnItemDataBound="rpArticles_ItemDataBound">
			<ItemTemplate>
				<div runat="server" id="divArticleToRotate">
				    <div class="rotatorContentWrapper" style='<%=ContentWrapperStyle %>'>
				        <div class="rotatorContent">
				            <% if (ShowContentHeader) { %><asp:Label runat="server" id="lblHotTopicTitle" CssClass="rotatorContentHeader"><%= ContentHeaderText %></asp:Label><% } %>
				            <% if (ShowContentHeaderLink) { %><span class="rotatorContentHeaderLinkWrapper"><a class="rotatorContentHeaderLink" href='<%= ContentHeaderLink %>'><%= ContentHeaderLinkText %></a></span><% } %>
					        <div class="rotatorThumbnailWrapper">
					            <% if (ThumbnailDisplayMode == DisplayType.Link) { %><asp:HyperLink ID="lnkThumbnail" runat="server" CssClass="rotatorThumbnailLink" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "LinkUrl") %>'>
						            <img style='<%=ThumbnailStyle %>' class="rotatorThumbnail" src='<%# FormatThumbnailUrl(DataBinder.Eval(Container.DataItem, "ThumbnailUrl").ToString()) %>' alt='' />
					            </asp:HyperLink><% } %>
                                <% if (ThumbnailDisplayMode == DisplayType.Content) { %><img class="rotatorThumbnail" src='<%# FormatThumbnailUrl(DataBinder.Eval(Container.DataItem, "ThumbnailUrl").ToString()) %>' alt='' /><% } %>
				            </div>
				            <div class="rotatorContentBodyWrapper">
				                <span class="rotatorContentTitleWrapper">
					                <% if (ContentTitleDisplayMode == DisplayType.Content) { %> <%# DataBinder.Eval(Container.DataItem, "Title") %> <% } %>
					                <% if (ContentTitleDisplayMode == DisplayType.Link) { %><asp:HyperLink ID="lnkArticleTitle" Runat="server" CssClass="rotatorContentTitleLink" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "LinkUrl") %>'><%# DataBinder.Eval(Container.DataItem, "Title") %></asp:HyperLink><% } %>
				                </span>
			                    <div id="rotatorContentDescriptionWrapper" class="rotatorContentDescriptionWrapper">
			                        <% if (ContentDisplayMode == DisplayType.Content) { %><span class="rotatorDescription"><%# DataBinder.Eval(Container.DataItem, "Description") %></span><% } %>
			                        <% if (ContentDisplayMode == DisplayType.Link) { %><asp:HyperLink ID="lnkDescription" Runat="server" CssClass="rotatorDescriptionLink" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "LinkUrl") %>' Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'/><% } %>
					                <% if (ShowReadMoreLink) { %><div class="rotatorReadMoreLinkWrapper"><asp:HyperLink ID="lnkReadMore" Runat="server" CssClass="rotatorReadMoreLink" ResourceKey="lnkReadMore" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "LinkUrl") %>' /></div><% } %>
				                </div>
				            </div>
				        </div>
				    </div>
				</div>
			</ItemTemplate>
		</asp:Repeater>
	</div>
	
	<div id="controls" class="rotatorControlsWrapper">
		<% if (ShowPreviousButton && PreviousButtonPlacement == ControlPlacement.Above) { %><div id="btnPrev" class="rotatorControlsPreviousLink" onclick="rotator<%=TabModuleId.ToString(System.Globalization.CultureInfo.InvariantCulture) %>.Previous();" ></div>&nbsp;<% } %>
		<% if (ShowPauseButton && PauseButtonPlacement == ControlPlacement.Above){ %><div id="btnPause" class="rotatorControlsPauseLink" onclick="rotator<%=TabModuleId.ToString(System.Globalization.CultureInfo.InvariantCulture) %>.Pause();"></div>&nbsp;<% } %>
		<% if (ShowNextButton && NextButtonPlacement == ControlPlacement.Above){ %><div id="btnNext" class="rotatorControlsNextLink" onclick="rotator<%=TabModuleId.ToString(System.Globalization.CultureInfo.InvariantCulture) %>.Next();"></div>&nbsp;<% } %>
        <% if (ShowPositionCounter && PositionCounterPlacement == ControlPlacement.Above) { %><div class="rotatorPositionCounter"><%= Localization.GetString("lblPositionCounterBegin", LocalResourceFile) %><span id="positionCounter<%=TabModuleId.ToString(CultureInfo.InvariantCulture) %>">1</span><%= string.Format(CultureInfo.CurrentCulture, Localization.GetString("lblPositionCounterEnd", LocalResourceFile), ViewState["TotalCount"]) %></div><% } %>
		<div id="articlePosition" runat="server" class="rotatorPositionsWrapper">
		    <asp:Repeater ID="rpPosition" runat="server" OnItemDataBound="rpPosition_ItemDataBound">
		        <ItemTemplate>
		            <div id="positionToRotate" class="rotatorPosition" runat="server">
		                <div id="positionThumbnail" runat="server">
		                    <% if (PositionThumbnailDisplayMode == DisplayType.Content || PositionThumbnailDisplayMode == DisplayType.RotateContent) { %><img class="rotatorPositionThumbnail" style='<%=PositionThumbnailStyle %>' src='<%# FormatThumbnailUrl(DataBinder.Eval(Container.DataItem, "PositionThumbnailUrl").ToString()) %>' alt='<%# DataBinder.Eval(Container.DataItem, "Title") %>' /><% } %>
		                    <% if (PositionThumbnailDisplayMode == DisplayType.Link) { %><asp:HyperLink CssClass="rotatorPositionThumbnailLink" ID="lnkThumbnail" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "LinkUrl") %>'><img class="rotatorPositionThumbnail" style='<%=PositionThumbnailStyle %>' border="0" src='<%# FormatThumbnailUrl(DataBinder.Eval(Container.DataItem, "PositionThumbnailUrl").ToString()) %>' alt='<%# DataBinder.Eval(Container.DataItem, "Title") %>' /></asp:HyperLink><% } %>
		                </div>
		                <div id="positionTitle" runat="server">
		                    <% if (PositionTitleDisplayMode == DisplayType.Content || PositionTitleDisplayMode == DisplayType.RotateContent) { %><div class="rotatorPositionTitle"><%# DataBinder.Eval(Container.DataItem, "Title") %></div><% } %>
		                    <% if (PositionTitleDisplayMode == DisplayType.Link) { %><asp:HyperLink CssClass="rotatorPositionTitleLink" ID="lnkDescription" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "LinkUrl") %>' Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>'/><% } %>
		                </div>
		            </div>
		        </ItemTemplate>
		    </asp:Repeater>
		</div>
		<% if (ShowPreviousButton && PreviousButtonPlacement == ControlPlacement.Below) { %><div id="btnPrev" class="rotatorControlsPreviousLink" onclick="rotator<%=TabModuleId.ToString(System.Globalization.CultureInfo.InvariantCulture) %>.Previous();" ></div>&nbsp;<% } %>
		<% if (ShowPauseButton && PauseButtonPlacement == ControlPlacement.Below){ %><div id="btnPause" class="rotatorControlsPauseLink" onclick="rotator<%=TabModuleId.ToString(System.Globalization.CultureInfo.InvariantCulture) %>.Pause();"></div>&nbsp;<% } %>
		<% if (ShowNextButton && NextButtonPlacement == ControlPlacement.Below){ %><div id="btnNext" class="rotatorControlsNextLink" onclick="rotator<%=TabModuleId.ToString(System.Globalization.CultureInfo.InvariantCulture) %>.Next();"></div>&nbsp;<% } %>
		<% if (ShowPositionCounter && PositionCounterPlacement == ControlPlacement.Below) { %><div class="rotatorPositionCounter"><%= Localization.GetString("lblPositionCounterBegin", LocalResourceFile) %><span id="positionCounter<%=TabModuleId.ToString(CultureInfo.InvariantCulture) %>">1</span><%= string.Format(CultureInfo.CurrentCulture, Localization.GetString("lblPositionCounterEnd", LocalResourceFile), ViewState["TotalCount"]) %></div><% } %>
	</div>
    <script type="text/javascript">
        var rotator<%=TabModuleId.ToString(System.Globalization.CultureInfo.InvariantCulture) %> = new EngageRotator(<%=RotatorDelay.ToString() %>, <%=RotatorPauseDelay.ToString() %>, '<%=articleRotator.ClientID %>', '<%=articlePosition.ClientID %>', <%=UseAnimations.ToString(System.Globalization.CultureInfo.InvariantCulture).ToLowerInvariant() %>, <%=AnimationDuration.ToString(System.Globalization.CultureInfo.InvariantCulture) %>, <%=AnimationFramesPerSecond.ToString(System.Globalization.CultureInfo.InvariantCulture) %>, 'positionCounter<%=TabModuleId.ToString(CultureInfo.InvariantCulture) %>');
    </script>
</div>

<asp:Label Runat="server" ID="lblNoHotTopics" Visible="False" ResourceKey="lblNoHotTopics"/>