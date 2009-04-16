<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TemplateSelection.ascx.cs" Inherits="Engage.Dnn.ContentRotator.TemplateSelection" %>
<div class="Normal">
    <asp:Label ResourceKey="Template" runat="server" EnableViewState="false" />
    <asp:DropDownList ID="TemplatesDropDownList" runat="server" AutoPostBack="true" />
    <fieldset id="TemplateDescriptionPanel" runat="server">
        <legend><asp:Label runat="server" resourcekey="Description" /></legend>
        <asp:Label ID="TemplateDescriptionLabel" runat="server" />
    </fieldset>
    <asp:Image ID="TemplatePreviewImage" runat="server" />

    <div>
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

    <asp:Panel ID="ManifestValidationErrorsPanel" runat="server" CssClass="NormalRed"/>
    <asp:Button ID="SubmitButton" runat="server" resourcekey="Submit" EnableViewState="false" />&nbsp;
    <asp:Button ID="CancelButton" runat="server" resourcekey="Cancel" CausesValidation="false" EnableViewState="false" />
</div>