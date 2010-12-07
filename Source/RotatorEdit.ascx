<%@ Control Language="c#" AutoEventWireup="false" Codebehind="RotatorEdit.ascx.cs" Inherits="Engage.Dnn.ContentRotator.RotatorEdit" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Url" Src="~/controls/URLControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelControl.ascx" %>
<table class="settingsTable">
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="TitleLabel" runat="server" EnableViewState="false" /></td>
        <td class="contentColumn leftAlign"><asp:TextBox ID="TitleTextBox" runat="server" CssClass="NormalTextBox TitleTextBox"/></td>
    </tr>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="ContentLabel" runat="server" EnableViewState="false" /></td>
        <td class="contentColumn leftAlign"><dnn:TextEditor ID="ContentTextEditor" runat="server" Height="400" Width="600" HtmlEncode="false"/></td>
    </tr>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="ImageUrlLabel" runat="server" EnableViewState="false" /></td>
        <td class="contentColumn leftAlign">
            <asp:UpdatePanel runat="server" UpdateMode="Conditional"><ContentTemplate>
                <dnn:Url ID="ImageUrlControl" runat="server" UrlType="N" ShowTrack="false" ShowLog="false" ShowNewWindow="false" ShowUsers="false" ShowTabs="false" ShowNone="true" ShowDatabase="true" ShowSecure="true" ShowUrls="true" ShowUpLoad="true" ShowFiles="true"/>
            </ContentTemplate></asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="PagerImageUrlLabel" runat="server" EnableViewState="false" /></td>
        <td class="contentColumn leftAlign">
            <asp:UpdatePanel runat="server" UpdateMode="Conditional"><ContentTemplate>
                <dnn:Url ID="PagerImageUrlControl" runat="server" UrlType="N" ShowTrack="false" ShowLog="false" ShowNewWindow="false" ShowUsers="false" ShowTabs="false" ShowNone="true" ShowDatabase="true" ShowSecure="true" ShowUrls="true" ShowUpLoad="true" ShowFiles="true"/>
            </ContentTemplate></asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="LinkUrlLabel" runat="server" EnableViewState="false" /></td>
        <td class="contentColumn leftAlign">
            <asp:UpdatePanel runat="server" UpdateMode="Conditional"><ContentTemplate>
                <dnn:Url ID="LinkUrlControl" runat="server" UrlType="N" ShowTrack="false" ShowLog="false" ShowNewWindow="false" ShowUsers="false" ShowDatabase="false" ShowSecure="false" ShowUpLoad="false" ShowFiles="false" ShowNone="true" ShowTabs="true" ShowUrls="true"/>
            </ContentTemplate></asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="StartDateLabel" runat="server" EnableViewState="false" /></td>
        <td class="contentColumn leftAlign">
            <asp:TextBox ID="StartDateTextBox" runat="server" CssClass="NormalTextBox DatePicker" />
            <asp:CompareValidator runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="StartDateTextBox" Display="None" resourcekey="StartDateTypeValidator" />
            <asp:RequiredFieldValidator runat="server" InitialValue="" ControlToValidate="StartDateTextBox" Display="None" resourcekey="StartDateRequiredValidator" />
            <asp:CompareValidator runat="server" Type="Date" Operator="LessThan" ControlToValidate="StartDateTextBox" ControlToCompare="EndDateTextBox" Display="None" resourcekey="StartEndDateCompareValidator" />
        </td>
    </tr>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="EndDateLabel" runat="server" EnableViewState="false" /></td>
        <td class="contentColumn leftAlign">
            <asp:TextBox ID="EndDateTextBox" runat="server" CssClass="NormalTextBox DatePicker" />
            <asp:CompareValidator runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="EndDateTextBox" Display="None" resourcekey="EndDateTypeValidator" />
        </td>
    </tr>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="SortOrderLabel" runat="server" EnableViewState="false" /></td>
        <td class="contentColumn leftAlign">
            <asp:TextBox ID="SortOrderTextBox" runat="server" CssClass="NormalTextBox"/>
            <asp:RequiredFieldValidator runat="server" InitialValue="" ControlToValidate="SortOrderTextBox" Display="None" resourcekey="SortOrderRequiredValidator" />
            <asp:CompareValidator runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="SortOrderTextBox" Display="None" resourcekey="SortOrderTypeValidator" />
        </td>
    </tr>
</table>

<asp:Button ID="SubmitButton" runat="server" resourcekey="SubmitButton" EnableViewState="false" />&nbsp;
<asp:Button ID="CancelButton" runat="server" resourcekey="CancelButton" CausesValidation="false" EnableViewState="false" />
<asp:ValidationSummary runat="server" ShowMessageBox="true" ShowSummary="false" />

<script type="text/javascript">
    jQuery(function() { jQuery('.DatePicker').datepicker(datePickerOpts); });
</script>