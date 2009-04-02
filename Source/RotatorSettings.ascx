<%@ Import Namespace="DotNetNuke.Services.Localization"%>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="RotatorSettings.ascx.cs" Inherits="Engage.Dnn.ContentRotator.RotatorSettings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Url" Src="~/controls/URLControl.ascx" %>
<div class="SettingsContainer">
    <ul>
        <li><a href="#rotation-settings"><asp:Label runat="server" resourcekey="RotationTab.Header"/></a></li>
        <li><a href="#content-settings"><asp:Label runat="server" resourcekey="ContentTab.Header"/></a></li>
        <li><a href="#advanced-settings"><asp:Label runat="server" resourcekey="AdvancedTab.Header"/></a></li>
    </ul>
    <div id="rotation-settings">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="settingsTable Normal">
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="RotatorDelayLabel" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="RotatorDelayTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="seconds" />
                            <asp:CompareValidator runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="RotatorDelayTextBox" Display="None" EnableClientScript="false" resourcekey="RotatorDelayTypeValidator" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RotatorDelayTextBox" Display="None" EnableClientScript="false" resourcekey="RotatorDelayRequiredValidator" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="AutoStopLabel" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox ID="AutoStopCheckBox" runat="server" AutoPostBack="true"/><asp:Label runat="server" ResourceKey="AutoStopBegin.Text" /><asp:TextBox ID="AutoStopCountTextBox" runat="server" CssClass="NormalTextBox inlineTextbox" AutoCompleteType="Disabled"/><asp:Label runat="server" ResourceKey="AutoStopEnd.Text" />
                            <asp:CompareValidator ID="AutoStopCountIntegerValidator" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="AutoStopCountTextBox" Display="None" EnableClientScript="false" resourcekey="AutoStopCountTypeValidator" />
                            <asp:RequiredFieldValidator ID="AutoStopCountRequiredValidator" runat="server" ControlToValidate="AutoStopCountTextBox" Display="None" EnableClientScript="false" resourcekey="AutoStopCountRequiredValidator" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="PauseOnHoverLabel" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox id="PauseOnHoverCheckBox" runat="server" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="UseAnimationsLabel" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Checkbox ID="UseAnimationsCheckBox" runat="server" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="TransitionDurationLabel" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="TransitionDurationTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="seconds" />
                            <asp:CompareValidator id="TransitionDurationIntegerValidator" runat="server" Type="Double" Operator="DataTypeCheck" ControlToValidate="TransitionDurationTextBox" Display="None" EnableClientScript="false" resourcekey="TransitionDurationTypeValidator"/>
                            <asp:RequiredFieldValidator id="TransitionDurationRequiredValidator" runat="server" ControlToValidate="TransitionDurationTextBox" Display="None" EnableClientScript="false" resourcekey="TransitionDurationRequiredValidator"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="TransitionEffectLabel" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBoxList ID="TransitionEffectCheckBoxList" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" RepeatColumns="3" CssClass="transitionEffectsCheckBoxes Normal" />
                            <asp:CustomValidator ID="TransitionEffectRequiredValidator" runat="server" Display="None" resourcekey="TransitionEffectRequired" ClientValidationFunction="AnimationEffectRequiredValidator_ClientValidate" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers><asp:AsyncPostBackTrigger ControlID="SubmitButton" /></Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="content-settings">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="settingsTable Normal">
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="SlideHeightLabel" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="SlideHeightTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                            <asp:CompareValidator runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="SlideHeightTextBox" Display="None" EnableClientScript="false" resourcekey="SlideHeightTypeValidator" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="SlideWidthLabel" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="SlideWidthTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                            <asp:CompareValidator runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="SlideWidthTextBox" Display="None" EnableClientScript="false" resourcekey="SlideWidthTypeValidator" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers><asp:AsyncPostBackTrigger ControlID="SubmitButton" /></Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="advanced-settings">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="settingsTable Normal">
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="ContainerResizeLabel" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox ID="ContainerResizeCheckBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="ForceSlidesToFitContainerLabel" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox ID="ForceSlidesToFitContainerCheckBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="ContinuousLabel" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox ID="ContinuousCheckBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="LoopLabel" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox ID="LoopCheckBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="RandomOrderLabel" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox ID="RandomOrderCheckBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="SimultaneousTransitionsLabel" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox ID="SimultaneousTransitionsCheckBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="InitialDelayLabel" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox ID="InitialDelayCheckBox" runat="server" AutoPostBack="true"/><asp:Label runat="server" ResourceKey="InitialDelayBegin.Text" /><asp:TextBox ID="InitialDelayTextBox" runat="server" CssClass="NormalTextBox inlineTextbox" AutoCompleteType="Disabled"/><asp:Label runat="server" ResourceKey="seconds.Text" />
                            <asp:CompareValidator ID="InitialDelayDecimalValidator" runat="server" Type="Double" Operator="DataTypeCheck" ControlToValidate="InitialDelayTextBox" Display="None" EnableClientScript="false" resourcekey="InitialDelayTypeValidator" />
                            <asp:RequiredFieldValidator ID="InitialDelayRequiredValidator" runat="server" ControlToValidate="InitialDelayTextBox" Display="None" EnableClientScript="false" resourcekey="InitialDelayRequiredValidator" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="ManuallyTriggeredTransitionSpeedLabel" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox ID="ManuallyTriggeredTransitionSpeedCheckBox" runat="server" AutoPostBack="true"/><asp:Label runat="server" ResourceKey="ManuallyTriggeredTransitionSpeedBegin.Text" /><asp:TextBox ID="ManuallyTriggeredTransitionSpeedTextBox" runat="server" CssClass="NormalTextBox inlineTextbox" AutoCompleteType="Disabled"/><asp:Label runat="server" ResourceKey="seconds.Text" />
                            <asp:CompareValidator ID="ManuallyTriggeredTransitionSpeedDecimalValidator" runat="server" Type="Double" Operator="DataTypeCheck" ControlToValidate="ManuallyTriggeredTransitionSpeedTextBox" Display="None" EnableClientScript="false" resourcekey="ManuallyTriggeredTransitionSpeedTypeValidator" />
                            <asp:RequiredFieldValidator ID="ManuallyTriggeredTransitionSpeedRequiredValidator" runat="server" ControlToValidate="ManuallyTriggeredTransitionSpeedTextBox" Display="None" EnableClientScript="false" resourcekey="ManuallyTriggeredTransitionSpeedRequiredValidator" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers><asp:AsyncPostBackTrigger ControlID="SubmitButton" /></Triggers>
        </asp:UpdatePanel>
    </div>
</div>
<asp:UpdateProgress runat="server">
    <ProgressTemplate>
        <img src='<%=this.ResolveUrl("~/images/progressbar.gif") %>' alt='<%=Localization.GetString("Loading.Alt", this.LocalResourceFile) %>' />
    </ProgressTemplate>
</asp:UpdateProgress>
<div style="clear:both;">
    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:ValidationSummary runat="server" ShowMessageBox="true" ShowSummary="true" CssClass="NormalRed" />
        </ContentTemplate>
        <Triggers><asp:AsyncPostBackTrigger ControlID="SubmitButton" /></Triggers>
    </asp:UpdatePanel>

    <asp:Button ID="SubmitButton" runat="server" resourcekey="SubmitButton" EnableViewState="false" />&nbsp;
    <asp:Button ID="CancelButton" runat="server" resourcekey="CancelButton" CausesValidation="false" EnableViewState="false" />
</div>
<script type="text/javascript">
    jQuery(function() { jQuery('.SettingsContainer').tabs(); });

    function AnimationEffectRequiredValidator_ClientValidate(sender, args) {
        args.isValid = jQuery('#<%=this.TransitionEffectCheckBoxList.ClientID %> :checked').length > 0;
    }
</script>