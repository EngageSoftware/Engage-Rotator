<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TemplateSelection.ascx.cs" Inherits="Engage.Dnn.ContentRotator.TemplateSelection" %>
<div class="template-slection-wrap Normal">
	<div class="ts-select-template">
		<asp:Label ResourceKey="Template" runat="server" EnableViewState="false" />
   		<asp:DropDownList ID="TemplatesDropDownList" runat="server" AutoPostBack="true" />
	</div>	
	<div class="ts-template-info">
		<div class="ts-leftcol">
			<div class="ts-description">
				<fieldset id="TemplateDescriptionPanel" runat="server">
        			<legend><asp:Label runat="server" resourcekey="Description" /></legend>
        			<asp:Label ID="TemplateTitleLabel" runat="server" />
        			<asp:Label ID="TemplateDescriptionLabel" runat="server" />
   				 </fieldset>
			</div>
			<div class="settings-table">
				<asp:Label ID="SettingsExplanationLabel" runat="server" CssClass="SubSubHead" ResourceKey="SettingsExplanation" />
        		<asp:GridView ID="SettingsGrid" runat="server" AutoGenerateColumns="false" CssClass="Normal DataGrid_Container" GridLines="None">
            		<AlternatingRowStyle CssClass="DataGrid_AlternatingItem" />
           			<HeaderStyle CssClass="DataGrid_Header" />
            		<RowStyle CssClass="DataGrid_Item" />
            		<Columns>
                		<asp:BoundField HeaderText="Key" DataField="Key" />
                		<asp:BoundField HeaderText="Value" />
                		<asp:BoundField HeaderText="OriginalValue" />
            		</Columns>
        		</asp:GridView>
			</div>
			<div class="ts-buttons">
				<asp:Button ID="SubmitButton" runat="server" resourcekey="Submit" EnableViewState="false" />&nbsp;
    			<asp:Button ID="CancelButton" runat="server" resourcekey="Cancel" CausesValidation="false" EnableViewState="false" />
    			<asp:Panel ID="ManifestValidationErrorsPanel" runat="server" CssClass="NormalRed"/>
			</div>
		</div>
		<div class="ts-rightcol">
			<asp:Panel ID="TemplatePreviewImagePanel" runat="server" CssClass="ts-preview">
            	<asp:Label runat="server" resourcekey="Preview" />
				<asp:Image ID="TemplatePreviewImage" runat="server" />
			</asp:Panel>
		</div>
	</div>
</div>