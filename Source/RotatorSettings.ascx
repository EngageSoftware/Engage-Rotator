<%@ Import Namespace="DotNetNuke.Services.Localization"%>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="RotatorSettings.ascx.cs" Inherits="Engage.Dnn.ContentRotator.RotatorSettings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Url" Src="~/controls/URLControl.ascx" %>
<div class="SettingsContainer">
    <ul>
        <li><a href="#template-settings"><asp:Label runat="server" resourcekey="Templates.Header"/></a></li>
        <li><a href="#content-settings"><asp:Label runat="server" resourcekey="Content.Header"/></a></li>
        <li><a href="#position-settings"><asp:Label runat="server" resourcekey="Position.Header"/></a></li>
        <li><a href="#rotation-settings"><asp:Label runat="server" resourcekey="Rotation.Header"/></a></li>
        <li><a href="#advanced-settings"><asp:Label runat="server" resourcekey="Advanced.Header"/></a></li>
    </ul>
    <div id="template-settings">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="settingsTable Normal">
                    <tr><th colspan="2"><asp:Label resourcekey="lblTemplatesHeader" CssClass="Head" runat="server" EnableViewState="false" /></th></tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblStyleTemplates" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:DropDownList ID="TemplatesDropDownList" runat="server" CssClass="NormalTextBox" AutoPostBack="true" />
                            <asp:Button ID="ApplyTemplateButton" runat="server" resourcekey="btnApplyStyleTemplate" />
                            
                            <fieldset id="TemplateDescriptionPanel" runat="server"><legend><asp:Label runat="server" resourcekey="StyleDescription" /></legend>
                                <asp:Label ID="TemplateDescriptionLabel" runat="server" />
                            </fieldset>
                            <asp:Image ID="TemplatePreviewImage" runat="server" />
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
                    <tr><th colspan="2"><asp:Label resourcekey="lblContentHeader" CssClass="Head" runat="server" EnableViewState="false" /></th></tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblRotatorHeight" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="RotatorHeightTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                            <asp:CompareValidator runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="RotatorHeightTextBox" Display="None" EnableClientScript="false" resourcekey="valRotatorHeight" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblRotatorWidth" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="RotatorWidthTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                            <asp:CompareValidator runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="RotatorWidthTextBox" Display="None" EnableClientScript="false" resourcekey="valRotatorWidth" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblContentDisplay" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:RadioButtonList id="ContentDisplayRadioButtonList" runat="server" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblContentHeight" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="ContentHeightTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                            <asp:CompareValidator id="ContentHeightIntegerValidator" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="ContentHeightTextBox" Display="None" EnableClientScript="false" resourcekey="valContentHeight" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblContentWidth" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="ContentWidthTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                            <asp:CompareValidator id="ContentWidthIntegerValidator" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="ContentWidthTextBox" Display="None" EnableClientScript="false" resourcekey="valContentWidth" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblContentTitleDisplay" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:RadioButtonList id="ContentTitleDisplayRadioButtonList" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblThumbnailDisplay" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:RadioButtonList id="ThumbnailDisplayRadioButtonList" runat="server" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblThumbnailHeight" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="ThumbnailHeightTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                            <asp:CompareValidator id="ThumbnailHeightIntegerValidator" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="ThumbnailHeightTextBox" Display="None" EnableClientScript="false" resourcekey="valThumbnailHeight" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblThumbnailWidth" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="ThumbnailWidthTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" /><%-- TODO: Allow user to select unit --%>
                            <asp:CompareValidator id="ThumbnailWidthIntegerValidator" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="ThumbnailWidthTextBox" Display="None" EnableClientScript="false" resourcekey="valThumbnailWidth" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers><asp:AsyncPostBackTrigger ControlID="SubmitButton" /></Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="position-settings">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="settingsTable Normal">
                    <tr><th colspan="2"><asp:Label resourcekey="lblPositionHeader" CssClass="Head" runat="server" EnableViewState="false" /></th></tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblPositionTitleDisplay" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:RadioButtonList id="PositionTitleDisplayRadioButtonList" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblPositionThumbnailDisplay" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:RadioButtonList id="PositionThumbnailDisplayRadioButtonList" runat="server" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblPositionThumbnailHeight" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="PositionThumbnailHeightTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                            <asp:CompareValidator id="PositionThumbnailHeightIntegerValidator" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="PositionThumbnailHeightTextBox" Display="None" EnableClientScript="false" resourcekey="valPositionThumbnailHeight" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblPositionThumbnailWidth" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="PositionThumbnailWidthTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" /><%-- TODO: Allow user to select unit --%>
                            <asp:CompareValidator id="PositionThumbnailWidthIntegerValidator" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="PositionThumbnailWidthTextBox" Display="None" EnableClientScript="false" resourcekey="valPositionThumbnailWidth" />
                        </td>
                    </tr>
                </table> 
            </ContentTemplate>
            <Triggers><asp:AsyncPostBackTrigger ControlID="SubmitButton" /></Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="rotation-settings">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="settingsTable Normal">
                    <tr><th colspan="2"><asp:Label resourcekey="lblRotationHeader" CssClass="Head" runat="server" EnableViewState="false" /></th></tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblRotatorDelay" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="RotatorDelayTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="seconds" />
                            <asp:CompareValidator runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="RotatorDelayTextBox" Display="None" EnableClientScript="false" resourcekey="valRotatorDelay" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RotatorDelayTextBox" Display="None" EnableClientScript="false" resourcekey="rfvRotatorDelay" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblAutoStop" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox ID="AutoStopCheckBox" runat="server" AutoPostBack="true"/><asp:Label runat="server" ResourceKey="AutoStopBegin.Text" /><asp:TextBox ID="AutoStopCountTextBox" runat="server" CssClass="NormalTextBox inlineTextbox" AutoCompleteType="Disabled"/><asp:Label runat="server" ResourceKey="AutoStopEnd.Text" />
                            <asp:CompareValidator ID="AutoStopCountIntegerValidator" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="AutoStopCountTextBox" Display="None" EnableClientScript="false" resourcekey="valAutoStopCount" />
                            <asp:RequiredFieldValidator ID="AutoStopCountRequiredValidator" runat="server" ControlToValidate="AutoStopCountTextBox" Display="None" EnableClientScript="false" resourcekey="rfvAutoStopCount" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblPauseOnMouseOver" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox id="PauseOnMouseOverCheckBox" runat="server" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblRotatorPauseDelay" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="RotatorPauseDelayTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="seconds" />
                            <asp:CompareValidator id="RotatorPauseDelayIntegerValidator" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="RotatorPauseDelayTextBox" Display="None" EnableClientScript="false" resourcekey="valRotatorPauseDelay" />
                            <asp:RequiredFieldValidator id="RotatorPauseDelayRequiredValidator" runat="server" ControlToValidate="RotatorPauseDelayTextBox" Display="None" EnableClientScript="false" resourcekey="rfvRotatorPauseDelay" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblUseAnimations" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Checkbox ID="UseAnimationsCheckBox" runat="server" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblAnimationDuration" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="AnimationDurationTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="seconds" />
                            <asp:CompareValidator id="AnimationDurationIntegerValidator" runat="server" Type="Double" Operator="DataTypeCheck" ControlToValidate="AnimationDurationTextBox" Display="None" EnableClientScript="false" resourcekey="valAnimationDuration"/>
                            <asp:RequiredFieldValidator id="AnimationDurationRequiredValidator" runat="server" ControlToValidate="AnimationDurationTextBox" Display="None" EnableClientScript="false" resourcekey="rfvAnimationDuration"/>
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
                    <tr><th colspan="2"><asp:Label resourcekey="lblAdvancedHeader" CssClass="Head" runat="server" EnableViewState="false" /></th></tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblContainerResize" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox ID="ContainerResizeCheckBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblForceSlidesToFitContainer" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox ID="ForceSlidesToFitContainerCheckBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblContinuous" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox ID="ContinuousCheckBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblLoop" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox ID="LoopCheckBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblRandomOrder" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox ID="RandomOrderCheckBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblSimultaneousTransitions" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox ID="SimultaneousTransitionsCheckBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblInitialDelay" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox ID="InitialDelayCheckBox" runat="server" AutoPostBack="true"/><asp:Label runat="server" ResourceKey="InitialDelayBegin.Text" /><asp:TextBox ID="InitialDelayTextBox" runat="server" CssClass="NormalTextBox inlineTextbox" AutoCompleteType="Disabled"/><asp:Label runat="server" ResourceKey="seconds.Text" />
                            <asp:CompareValidator ID="InitialDelayDecimalValidator" runat="server" Type="Double" Operator="DataTypeCheck" ControlToValidate="InitialDelayTextBox" Display="None" EnableClientScript="false" resourcekey="valInitialDelay" />
                            <asp:RequiredFieldValidator ID="InitialDelayRequiredValidator" runat="server" ControlToValidate="InitialDelayTextBox" Display="None" EnableClientScript="false" resourcekey="rfvInitialDelay" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblManuallyTriggeredTransitionSpeed" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox ID="ManuallyTriggeredTransitionSpeedCheckBox" runat="server" AutoPostBack="true"/><asp:Label runat="server" ResourceKey="ManuallyTriggeredTransitionSpeedBegin.Text" /><asp:TextBox ID="ManuallyTriggeredTransitionSpeedTextBox" runat="server" CssClass="NormalTextBox inlineTextbox" AutoCompleteType="Disabled"/><asp:Label runat="server" ResourceKey="seconds.Text" />
                            <asp:CompareValidator ID="ManuallyTriggeredTransitionSpeedDecimalValidator" runat="server" Type="Double" Operator="DataTypeCheck" ControlToValidate="ManuallyTriggeredTransitionSpeedTextBox" Display="None" EnableClientScript="false" resourcekey="valManuallyTriggeredTransitionSpeed" />
                            <asp:RequiredFieldValidator ID="ManuallyTriggeredTransitionSpeedRequiredValidator" runat="server" ControlToValidate="ManuallyTriggeredTransitionSpeedTextBox" Display="None" EnableClientScript="false" resourcekey="rfvManuallyTriggeredTransitionSpeed" />
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
            <asp:Panel ID="ManifestValidationErrorsPanel" runat="server" CssClass="NormalRed"/>
        </ContentTemplate>
        <Triggers><asp:AsyncPostBackTrigger ControlID="SubmitButton" /><asp:AsyncPostBackTrigger ControlID="TemplatesDropDownList" /></Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False" RenderMode="Inline" >
        <ContentTemplate>
            <asp:Button ID="SubmitButton" runat="server" resourcekey="btnSubmit" EnableViewState="false" />&nbsp;
        </ContentTemplate>
        <Triggers><asp:AsyncPostBackTrigger ControlID="TemplatesDropDownList" /></Triggers>
    </asp:UpdatePanel>
    <asp:Button ID="CancelButton" runat="server" resourcekey="btnCancel" CausesValidation="false" EnableViewState="false" />
</div>
<script type="text/javascript">
    jQuery(function() { jQuery('.SettingsContainer').tabs(); });
</script>