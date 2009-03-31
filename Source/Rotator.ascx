<%@ Control Language="c#" AutoEventWireup="False" Codebehind="Rotator.ascx.cs" Inherits="Engage.Dnn.ContentRotator.Rotator" %>
<asp:Panel ID="RotatorContainer" runat="server" CssClass="engage-rotator-container"/>
<script type="text/javascript">
    jQuery(function() {
        jQuery('#<%=this.RotatorContainer.ClientID %> .rotate-wrap').cycle(<%= this.CycleOptions.Serialize() %>);
    });
</script>