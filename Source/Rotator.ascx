<%@ Control Language="c#" AutoEventWireup="False" Codebehind="Rotator.ascx.cs" Inherits="Engage.Dnn.ContentRotator.Rotator" %>
<asp:PlaceHolder ID="ItemTemplateSection" runat="server" />
<script type="text/javascript">
    jQuery(function() {
        jQuery('#<%=this.Parent.ClientID %> .rotate-wrap').cycle(<%= this.CycleOptions.Serialize() %>);
    });
</script>