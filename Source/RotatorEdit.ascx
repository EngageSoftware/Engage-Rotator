<%@ Control Language="c#" AutoEventWireup="false" Codebehind="RotatorEdit.ascx.cs" Inherits="Engage.Dnn.ContentRotator.RotatorEdit" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Url" Src="~/controls/URLControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelControl.ascx" %>
<div class="dnnForm engageRotatorEdit">
    <fieldset>
        <div class="dnnFormItem">
            <dnn:Label ResourceKey="TitleLabel" ControlName="TitleTextBox" runat="server" EnableViewState="false" />
            <asp:TextBox ID="TitleTextBox" runat="server" CssClass="TitleTextBox" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label ResourceKey="ContentLabel" ControlName="ContentTextEditor" runat="server" EnableViewState="false"  />
            <dnn:TextEditor ID="ContentTextEditor" runat="server" Height="400" Width="600" HtmlEncode="false" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label ResourceKey="ImageUrlLabel" ControlName="ImageUrlControl" runat="server" EnableViewState="false" />
            <asp:UpdatePanel runat="server" UpdateMode="Conditional"><ContentTemplate>
                <dnn:Url ID="ImageUrlControl" runat="server" UrlType="N" ShowTrack="false" ShowLog="false" ShowNewWindow="false" ShowUsers="false" ShowTabs="false" ShowNone="true" ShowDatabase="true" ShowSecure="true" ShowUrls="true" ShowUpLoad="true" ShowFiles="true" />
            </ContentTemplate></asp:UpdatePanel>
        </div>
        <div class="dnnFormItem">
            <dnn:Label ResourceKey="PagerImageUrlLabel" ControlName="PagerImageUrlControl" runat="server" EnableViewState="false" />
            <asp:UpdatePanel runat="server" UpdateMode="Conditional"><ContentTemplate>
                <dnn:Url ID="PagerImageUrlControl" runat="server" UrlType="N" ShowTrack="false" ShowLog="false" ShowNewWindow="false" ShowUsers="false" ShowTabs="false" ShowNone="true" ShowDatabase="true" ShowSecure="true" ShowUrls="true" ShowUpLoad="true" ShowFiles="true" />
            </ContentTemplate></asp:UpdatePanel>
        </div>
        <div class="dnnFormItem">
            <asp:UpdatePanel runat="server" UpdateMode="Conditional"><ContentTemplate>
                <dnn:Label ResourceKey="LinkUrlLabel" ControlName="LinkUrlControl" runat="server" EnableViewState="false" />
                <dnn:Url ID="LinkUrlControl" runat="server" UrlType="N" ShowTrack="true" ShowLog="true" ShowNewWindow="false" ShowUsers="false" ShowDatabase="false" ShowSecure="false" ShowUpLoad="false" ShowFiles="false" ShowNone="true" ShowTabs="true" ShowUrls="true" />
            </ContentTemplate></asp:UpdatePanel>
        </div>
        <div class="dnnFormItem">
            <dnn:Label ResourceKey="StartDateLabel" ControlName="StartDateTextBox" runat="server" EnableViewState="false" />
            <asp:TextBox ID="StartDateTextBox" runat="server" CssClass="DatePicker dnnFormRequired" />
            <asp:CompareValidator runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="StartDateTextBox" Display="None" resourcekey="StartDateTypeValidator" />
            <asp:RequiredFieldValidator runat="server" InitialValue="" ControlToValidate="StartDateTextBox" Display="None" resourcekey="StartDateRequiredValidator" />
            <asp:CompareValidator runat="server" Type="Date" Operator="LessThan" ControlToValidate="StartDateTextBox" ControlToCompare="EndDateTextBox" Display="None" resourcekey="StartEndDateCompareValidator" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label ResourceKey="EndDateLabel" ControlName="EndDateTextBox" runat="server" EnableViewState="false" />
            <asp:TextBox ID="EndDateTextBox" runat="server" CssClass="DatePicker" />
            <asp:CompareValidator runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="EndDateTextBox" Display="None" resourcekey="EndDateTypeValidator" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label ResourceKey="SortOrderLabel" ControlName="SortOrderTextBox" runat="server" EnableViewState="false" />
            <asp:TextBox ID="SortOrderTextBox" runat="server" CssClass="dnnFormRequired" />
            <asp:RequiredFieldValidator runat="server" InitialValue="" ControlToValidate="SortOrderTextBox" Display="None" resourcekey="SortOrderRequiredValidator" />
            <asp:CompareValidator runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="SortOrderTextBox" Display="None" resourcekey="SortOrderTypeValidator" />
        </div>
    </fieldset>
    
    <ul class="dnnActions dnnClear">
        <li><asp:Button ID="SubmitButton" runat="server" ResourceKey="SubmitButton" CssClass="dnnPrimaryAction" EnableViewState="false" /></li>
        <li><asp:Button ID="CancelButton" runat="server" ResourceKey="CancelButton" CssClass="dnnSecondaryAction" CausesValidation="false" EnableViewState="false" /></li>
    </ul>
    <asp:ValidationSummary runat="server" ShowMessageBox="false" ShowSummary="true" CssClass="dnnFormMessage dnnFormValidationSummary" />
</div>

<script type="text/javascript">
    /*global jQuery, datePickerOpts */
    (function ($) {
        'use strict';
        $(function() { $('.DatePicker').datepicker(datePickerOpts); });
        $(window).load(function () { $('#ui-datepicker-div').wrap('<div class="ModEngageRotatorC" />'); });
        
        var ValidationSummaryOnSubmitOrig = window.ValidationSummaryOnSubmit;
        window.ValidationSummaryOnSubmit = function (group) {
            var scrollToOrig = window.scrollTo;
            window.scrollTo = function () { };
            ValidationSummaryOnSubmitOrig(group);
            window.scrollTo = scrollToOrig;
        }
    }(jQuery));
</script>