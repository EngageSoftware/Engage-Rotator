<%@ Control Language="c#" AutoEventWireup="False" Codebehind="Rotator.ascx.cs" Inherits="Engage.Dnn.ContentRotator.Rotator" %>
<asp:PlaceHolder ID="HeaderTemplatePlaceholder" runat="server"/>
<asp:Panel ID="ItemTemplateSection" runat="server" CssClass="RotatorBody" />
<asp:PlaceHolder ID="FooterTemplatePlaceholder" runat="server"/>
<script type="text/javascript">
jQuery(function() {
    jQuery('div.RotatorBody').cycle(cycleOptions);
});
</script>