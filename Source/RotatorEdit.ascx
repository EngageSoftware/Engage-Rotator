<%@ Control Language="c#" AutoEventWireup="false" Codebehind="RotatorEdit.ascx.cs" Inherits="Engage.Dnn.ContentRotator.RotatorEdit" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Url" Src="~/controls/URLControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelControl.ascx" %>
<table class="settingsTable">
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblTitle" runat="server" EnableViewState="false" /></td>
        <td class="contentColumn leftAlign"><asp:TextBox ID="TitleTextBox" runat="server" CssClass="NormalTextBox TitleTextBox"/></td>
    </tr>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblDescription" runat="server" EnableViewState="false" /></td>
        <td class="contentColumn leftAlign"><dnn:TextEditor ID="DescriptionTextEditor" runat="server" Height="400" Width="600" HtmlEncode="false"/></td>
    </tr>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblThumbnailUrl" runat="server" EnableViewState="false" /></td>
        <td class="contentColumn leftAlign"><dnn:Url ID="ThumbnailUrlControl" runat="server" UrlType="F" ShowTrack="false" ShowLog="false" ShowNewWindow="false" ShowUsers="false" ShowTabs="false" ShowNone="true" ShowDatabase="true" ShowSecure="true" ShowUrls="true" ShowUpLoad="true" ShowFiles="true"/></td>
    </tr><%-- UrlType is F for File --%>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblPositionThumbnailUrl" runat="server" EnableViewState="false" /></td>
        <td class="contentColumn leftAlign"><dnn:Url ID="PositionThumbnailUrlControl" runat="server" UrlType="F" ShowTrack="false" ShowLog="false" ShowNewWindow="false" ShowUsers="false" ShowTabs="false" ShowNone="true" ShowDatabase="true" ShowSecure="true" ShowUrls="true" ShowUpLoad="true" ShowFiles="true"/></td>
    </tr><%-- UrlType is F for File --%>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblLinkUrl" runat="server" EnableViewState="false" /></td>
        <td class="contentColumn leftAlign"><dnn:Url ID="LinkUrlControl" runat="server" UrlType="T" ShowTrack="false" ShowLog="false" ShowNewWindow="false" ShowUsers="false" ShowDatabase="false" ShowSecure="false" ShowUpLoad="false" ShowFiles="false" ShowNone="true" ShowTabs="true" ShowUrls="true"/></td>
    </tr><%-- UrlType is T for Tab --%>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblStartDate" runat="server" EnableViewState="false" /></td>
        <td class="contentColumn leftAlign">
            <asp:TextBox ID="StartDateTextBox" runat="server" CssClass="NormalTextBox DatePicker" />
            <asp:CompareValidator runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="StartDateTextBox" Display="None" resourcekey="valStartDate" />
            <asp:RequiredFieldValidator runat="server" InitialValue="" ControlToValidate="StartDateTextBox" Display="None" resourcekey="rfvStartDate" />
            <asp:CompareValidator runat="server" Type="Date" Operator="LessThan" ControlToValidate="StartDateTextBox" ControlToCompare="EndDateTextBox" Display="None" resourcekey="valStartEndDate" />
        </td>
    </tr>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblEndDate" runat="server" EnableViewState="false" /></td>
        <td class="contentColumn leftAlign">
            <asp:TextBox ID="EndDateTextBox" runat="server" CssClass="NormalTextBox DatePicker" />
            <asp:CompareValidator runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="EndDateTextBox" Display="None" resourcekey="valEndDate" />
        </td>
    </tr>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblSortOrder" runat="server" EnableViewState="false" /></td>
        <td class="contentColumn leftAlign">
            <asp:TextBox ID="SortOrderTextBox" runat="server" CssClass="NormalTextBox"/>
            <asp:RequiredFieldValidator runat="server" InitialValue="" ControlToValidate="SortOrderTextBox" Display="None" resourcekey="rfvSortOrder" />
            <asp:CompareValidator runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="SortOrderTextBox" Display="None" resourcekey="valSortOrder" />
        </td>
    </tr>
</table>

<asp:Button ID="SubmitButton" runat="server" resourcekey="btnSubmit" EnableViewState="false" />&nbsp;
<asp:Button ID="CancelButton" runat="server" resourcekey="btnCancel" CausesValidation="false" EnableViewState="false" />
<asp:ValidationSummary runat="server" ShowMessageBox="true" ShowSummary="false" />

<script type="text/javascript">
    jQuery(function() { jQuery('.DatePicker').datepicker(datePickerOpts); });
</script>