<%@ Control Language="c#" AutoEventWireup="True" Codebehind="RotatorEdit.ascx.cs" Inherits="Engage.Dnn.ContentRotator.RotatorEdit" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Url" Src="~/controls/URLControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelControl.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<table class="settingsTable">
    <%--<tr>
        <td class="SubHead"><dnn:Label ID="lblIdLabel" runat="server" EnableViewState="false" /></td>
        <td class="contentColumn"><asp:Label ID="lblId" runat="server"/></td>
    </tr>--%>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblTitle" runat="server" Text="Title: " EnableViewState="false" /></td>
        <td class="contentColumn leftAlign"><asp:TextBox ID="txtTitle" runat="server" CssClass="NormalTextBox TitleTextBox"/></td>
    </tr>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblDescription" runat="server" Text="Content: " EnableViewState="false" /></td>
        <td class="contentColumn leftAlign"><dnn:TextEditor ID="txtDescription" runat="server" Height="400" Width="600" HtmlEncode="false"/></td>
    </tr>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblThumbnailUrl" runat="server" Text="Thumbnail: " EnableViewState="false" /></td>
        <td class="contentColumn leftAlign"><dnn:Url ID="urlThumbnail" runat="server" UrlType="F" ShowTrack="false" ShowLog="false" ShowNewWindow="false" ShowUsers="false" ShowTabs="false" ShowNone="true" ShowDatabase="true" ShowSecure="true" ShowUrls="true" ShowUpLoad="true" ShowFiles="true"/></td>
    </tr><%-- UrlType is F for File --%>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblPositionThumbnailUrl" runat="server" Text="Small Thumbnail: " EnableViewState="false" /></td>
        <td class="contentColumn leftAlign"><dnn:Url ID="urlPositionThumbnail" runat="server" UrlType="F" ShowTrack="false" ShowLog="false" ShowNewWindow="false" ShowUsers="false" ShowTabs="false" ShowNone="true" ShowDatabase="true" ShowSecure="true" ShowUrls="true" ShowUpLoad="true" ShowFiles="true"/></td>
    </tr><%-- UrlType is F for File --%>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblLinkUrl" runat="server" Text="Link: " EnableViewState="false" /></td>
        <td class="contentColumn leftAlign"><dnn:Url ID="urlLink" runat="server" UrlType="T" ShowTrack="false" ShowLog="false" ShowNewWindow="false" ShowUsers="false" ShowDatabase="false" ShowSecure="false" ShowUpLoad="false" ShowFiles="false" ShowNone="true" ShowTabs="true" ShowUrls="true"/></td>
    </tr><%-- UrlType is T for Tab --%>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblStartDate" runat="server" Text="Start Date: " EnableViewState="false" /></td>
        <td class="contentColumn leftAlign">
            <ajaxToolkit:CalendarExtender ID="ajaxStartDate" runat="server" TargetControlID="txtStartDate" PopupButtonID="imgStartCalendarIcon" />
            <asp:TextBox ID="txtStartDate" runat="server" CssClass="NormalTextBox" />&nbsp;<asp:Image ID="imgStartCalendarIcon" runat="server" ImageUrl="~/images/calendar.png" />
            <asp:CompareValidator ID="valStartDate" runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="txtStartDate" Display="None" resourcekey="valStartDate" />
            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" InitialValue="" ControlToValidate="txtStartDate" Display="None" resourcekey="rfvStartDate" />
            <asp:CompareValidator ID="valStartEndDate" runat="server" Type="Date" Operator="LessThan" ControlToValidate="txtStartDate" ControlToCompare="txtEndDate" Display="None" resourcekey="valStartEndDate" />
        </td>
    </tr>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblEndDate" runat="server" Text="End Date: " EnableViewState="false" /></td>
        <td class="contentColumn leftAlign">
            <ajaxToolkit:CalendarExtender ID="ajaxEndDate" runat="server" TargetControlID="txtEndDate" PopupButtonID="imgEndCalendarIcon" />
            <asp:TextBox ID="txtEndDate" runat="server" CssClass="NormalTextBox" />&nbsp;<asp:Image ID="imgEndCalendarIcon" runat="server" ImageUrl="~/images/calendar.png" />
            <asp:CompareValidator ID="valEndDate" runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="txtEndDate" Display="None" resourcekey="valEndDate" />
        </td>
    </tr>
    <tr>
        <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblSortOrder" runat="server" EnableViewState="false" /></td>
        <td class="contentColumn leftAlign">
            <asp:TextBox ID="txtSortOrder" runat="server" CssClass="NormalTextBox"/>
            <asp:RequiredFieldValidator ID="rfvSortOrder" runat="server" InitialValue="" ControlToValidate="txtSortOrder" Display="None" resourcekey="rfvSortOrder" />
            <asp:CompareValidator ID="valSortOrder" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="txtSortOrder" Display="None" resourcekey="valSortOrder" />
        </td>
    </tr>
</table>

<asp:Button ID="btnSubmit" runat="server" resourcekey="btnSubmit" OnClick="btnSubmit_Click" EnableViewState="false" />&nbsp;
<asp:Button ID="btnCancel" runat="server" resourcekey="btnCancel" OnClick="btnCancel_Click" CausesValidation="false" EnableViewState="false" />
<asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />