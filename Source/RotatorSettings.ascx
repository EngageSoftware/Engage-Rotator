<%@ Control Language="c#" AutoEventWireup="True" Codebehind="RotatorSettings.ascx.cs" Inherits="Engage.Dnn.ContentRotator.RotatorSettings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Url" Src="~/controls/URLControl.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<ajaxToolkit:TabContainer ID="tabsSettings" runat="server" ActiveTabIndex="0">
    <ajaxToolkit:TabPanel ID="tabHeader" runat="server">
        <HeaderTemplate><asp:Label runat="server" resourcekey="Header.Header" /></HeaderTemplate>
        <ContentTemplate><asp:UpdatePanel ID="upnlHeaderSettings" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <table class="settingsTable">
                <tr><th colspan="2"><asp:Label ID="lblHeaderHeader" resourcekey="lblHeaderHeader" CssClass="Head" runat="server" EnableViewState="false" /></th></tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblShowContentHeaderTitle" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Checkbox ID="chkShowContentHeaderTitle" runat="server" AutoPostBack="true" OnCheckedChanged="chkShowContentHeaderTitle_CheckedChanged"/>
                    </td>
                </tr>
                <tr id="contentHeaderTitleRow" runat="server">
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblContentHeaderTitle" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Textbox ID="txtContentHeaderTitle" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/>
                        <asp:RequiredFieldValidator id="rfvContentHeaderTitle" runat="server" ControlToValidate="txtContentHeaderTitle" Display="None" EnableClientScript="false" resourcekey="rfvContentHeaderTitle" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblShowContentHeaderLink" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Checkbox ID="chkShowContentHeaderLink" runat="server" AutoPostBack="true" OnCheckedChanged="chkShowContentHeaderLink_CheckedChanged"/>
                    </td>
                </tr>
                <tr id="contentHeaderLinkTextRow" runat="server">
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblContentHeaderLinkText" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Textbox ID="txtContentHeaderLinkText" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/>
                        <asp:RequiredFieldValidator id="rfvContentHeaderLinkText" runat="server" ControlToValidate="txtContentHeaderLinkText" Display="None" EnableClientScript="false" resourcekey="rfvContentHeaderLinkText" />
                    </td>
                </tr>
                <tr id="contentHeaderLinkRow" runat="server">
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblContentHeaderLink" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign"><%-- UrlType is T for Tab --%>
                        <dnn:Url ID="urlContentHeaderLink" runat="server" UrlType="T" ShowTrack="false" ShowLog="false" ShowNewWindow="false" ShowUsers="false" ShowNone="false" ShowDatabase="false" ShowSecure="false" ShowUpLoad="false" ShowFiles="false" ShowTabs="true" ShowUrls="true"/>
                        <asp:CustomValidator id="valContentHeaderLink" runat="server" Display="None" resourcekey="valContentHeaderLink" EnableClientScript="false" OnServerValidate="valContentHeaderLink_ServerValidate" />
                    </td>
                </tr>
            </table>
    	</ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="btnSubmit" /></Triggers></asp:UpdatePanel>
    	</ContentTemplate>
    </ajaxToolkit:TabPanel>
    <ajaxToolkit:TabPanel ID="tabContent" runat="server">
        <HeaderTemplate><asp:Label runat="server" resourcekey="Content.Header"/></HeaderTemplate>
        <ContentTemplate><asp:UpdatePanel ID="upnlContentSettings" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <table class="settingsTable">
                <tr><th colspan="2"><asp:Label ID="lblContentHeader" resourcekey="lblContentHeader" CssClass="Head" runat="server" EnableViewState="false" /></th></tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblRotatorHeight" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Textbox ID="txtRotatorHeight" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                        <asp:CompareValidator id="valRotatorHeight" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="txtRotatorHeight" Display="None" EnableClientScript="false" resourcekey="valRotatorHeight" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblRotatorWidth" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Textbox ID="txtRotatorWidth" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                        <asp:CompareValidator id="valRotatorWidth" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="txtRotatorWidth" Display="None" EnableClientScript="false" resourcekey="valRotatorWidth" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblContentDisplay" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:RadioButtonList id="rblContentDisplay" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblContentDisplay_SelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblContentHeight" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Textbox ID="txtContentHeight" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                        <asp:CompareValidator id="valContentHeight" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="txtContentHeight" Display="None" EnableClientScript="false" resourcekey="valContentHeight" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblContentWidth" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Textbox ID="txtContentWidth" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                        <asp:CompareValidator id="valContentWidth" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="txtContentWidth" Display="None" EnableClientScript="false" resourcekey="valContentWidth" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblContentTitleDisplay" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:RadioButtonList id="rblContentTitleDisplay" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblThumbnailDisplay" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:RadioButtonList id="rblThumbnailDisplay" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblThumbnailDisplay_SelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblThumbnailHeight" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Textbox ID="txtThumbnailHeight" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                        <asp:CompareValidator id="valThumbnailHeight" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="txtThumbnailHeight" Display="None" EnableClientScript="false" resourcekey="valThumbnailHeight" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblThumbnailWidth" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Textbox ID="txtThumbnailWidth" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" /><%-- TODO: Allow user to select unit --%>
                        <asp:CompareValidator id="valThumbnailWidth" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="txtThumbnailWidth" Display="None" EnableClientScript="false" resourcekey="valThumbnailWidth" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblShowReadMoreLink" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Checkbox ID="chkShowReadMoreLink" runat="server"/>
                    </td>
                </tr>
            </table>
        </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="btnSubmit" /></Triggers></asp:UpdatePanel>
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
    <ajaxToolkit:TabPanel ID="tabPosition" runat="server">
        <HeaderTemplate><asp:Label runat="server" resourcekey="Position.Header"/></HeaderTemplate>
        <ContentTemplate><asp:UpdatePanel ID="upnlPositionSettings" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <table class="settingsTable">
                <tr><th colspan="2"><asp:Label ID="lblPositionHeader" resourcekey="lblPositionHeader" CssClass="Head" runat="server" EnableViewState="false" /></th></tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblPositionTitleDisplay" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:RadioButtonList id="rblPositionTitleDisplay" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblPositionThumbnailDisplay" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:RadioButtonList id="rblPositionThumbnailDisplay" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblPositionThumbnailDisplay_SelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblPositionThumbnailHeight" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Textbox ID="txtPositionThumbnailHeight" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                        <asp:CompareValidator id="valPositionThumbnailHeight" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="txtPositionThumbnailHeight" Display="None" EnableClientScript="false" resourcekey="valPositionThumbnailHeight" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblPositionThumbnailWidth" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Textbox ID="txtPositionThumbnailWidth" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" /><%-- TODO: Allow user to select unit --%>
                        <asp:CompareValidator id="valPositionThumbnailWidth" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="txtPositionThumbnailWidth" Display="None" EnableClientScript="false" resourcekey="valPositionThumbnailWidth" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblShowPreviousButton" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Checkbox ID="chkShowPreviousButton" runat="server" AutoPostBack="true" OnCheckedChanged="chkShowPreviousButton_CheckedChanged"/>
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblPreviousButtonLocation" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:DropDownList id="ddlPreviousButtonLocation" runat="server" CssClass="NormalTextBox" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblShowPauseButton" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Checkbox ID="chkShowPauseButton" runat="server" AutoPostBack="true" OnCheckedChanged="chkShowPauseButton_CheckedChanged"/>
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblPauseButtonLocation" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:DropDownList id="ddlPauseButtonLocation" runat="server" CssClass="NormalTextBox" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblShowNextButton" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Checkbox ID="chkShowNextButton" runat="server" AutoPostBack="true" OnCheckedChanged="chkShowNextButton_CheckedChanged"/>
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblNextButtonLocation" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:DropDownList id="ddlNextButtonLocation" runat="server" CssClass="NormalTextBox" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblShowPositionCounter" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:CheckBox id="chkShowPositionCounter" runat="server" AutoPostBack="true" OnCheckedChanged="chkShowPositionCounter_CheckedChanged"/>
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblPositionCounterLocation" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:DropDownList id="ddlPositionCounterLocation" runat="server" CssClass="NormalTextBox" />
                    </td>
                </tr>
            </table> 
        </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="btnSubmit" /></Triggers></asp:UpdatePanel>
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
    <ajaxToolkit:TabPanel ID="tabRotation" runat="server">
        <HeaderTemplate><asp:Label runat="server" resourcekey="Rotation.Header"/></HeaderTemplate>
        <ContentTemplate><asp:UpdatePanel ID="upnlRotationSettings" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <table class="settingsTable">
                <tr><th colspan="2"><asp:Label ID="lblRotationHeader" resourcekey="lblRotationHeader" CssClass="Head" runat="server" EnableViewState="false" /></th></tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblRotatorDelay" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Textbox ID="txtRotatorDelay" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label ID="Label2" runat="server" resourcekey="seconds" />
                        <asp:CompareValidator id="valRotatorDelay" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="txtRotatorDelay" Display="None" EnableClientScript="false" resourcekey="valRotatorDelay" />
                        <asp:RequiredFieldValidator id="rfvRotatorDelay" runat="server" ControlToValidate="txtRotatorDelay" Display="None" EnableClientScript="false" resourcekey="rfvRotatorDelay" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblPauseOnMouseOver" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:CheckBox id="chkPauseOnMouseOver" runat="server" AutoPostBack="true" OnCheckedChanged="chkPauseOnMouseOver_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblRotatorPauseDelay" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Textbox ID="txtRotatorPauseDelay" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label ID="Label3" runat="server" resourcekey="seconds" />
                        <asp:CompareValidator id="valRotatorPauseDelay" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="txtRotatorPauseDelay" Display="None" EnableClientScript="false" resourcekey="valRotatorPauseDelay" />
                        <asp:RequiredFieldValidator id="rfvRotatorPauseDelay" runat="server" ControlToValidate="txtRotatorPauseDelay" Display="None" EnableClientScript="false" resourcekey="rfvRotatorPauseDelay" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblUseAnimations" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Checkbox ID="chkUseAnimations" runat="server" AutoPostBack="true" OnCheckedChanged="chkUseAnimations_CheckedChanged"/>
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblAnimationFramesPerSecond" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Textbox ID="txtAnimationFramesPerSecond" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label ID="Label4" runat="server" resourcekey="fps" />
                        <asp:CompareValidator id="valAnimationFramesPerSecond" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="txtAnimationFramesPerSecond" Display="None" EnableClientScript="false" resourcekey="valAnimationFramesPerSecond" />
                        <asp:RequiredFieldValidator id="rfvAnimationFramesPerSecond" runat="server" ControlToValidate="txtAnimationFramesPerSecond" Display="None" EnableClientScript="false" resourcekey="rfvAnimationFramesPerSecond" />
                    </td>
                </tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblAnimationDuration" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:Textbox ID="txtAnimationDuration" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label ID="Label5" runat="server" resourcekey="seconds" />
                        <asp:CompareValidator id="valAnimationDuration" runat="server" Type="Double" Operator="DataTypeCheck" ControlToValidate="txtAnimationDuration" Display="None" EnableClientScript="false" resourcekey="valAnimationDuration"/>
                        <asp:RequiredFieldValidator id="rfvAnimationDuration" runat="server" ControlToValidate="txtAnimationDuration" Display="None" EnableClientScript="false" resourcekey="rfvAnimationDuration"/>
                    </td>
                </tr>
            </table>
        </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="btnSubmit" /></Triggers></asp:UpdatePanel>
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
    <ajaxToolkit:TabPanel ID="tabTemplates" runat="server">
        <HeaderTemplate><asp:Label runat="server" resourcekey="Templates.Header"/></HeaderTemplate>
        <ContentTemplate><asp:UpdatePanel ID="upnlTemplatesSettings" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <table class="settingsTable">
                <tr><th colspan="2"><asp:Label ID="lblTemplatesHeader" resourcekey="lblTemplatesHeader" CssClass="Head" runat="server" EnableViewState="false" /></th></tr>
                <tr>
                    <td class="SubHead nowrap rightAlign"><dnn:Label ID="lblStyleTemplates" runat="server" EnableViewState="false" /></td>
                    <td class="contentColumn leftAlign">
                        <asp:DropDownList ID="ddlStyleTemplates" runat="server" CssClass="NormalTextBox" AutoPostBack="true" OnSelectedIndexChanged="ddlStyleTemplates_SelectedIndexChanged"/>
                        <asp:Button ID="btnApplyStyleTemplate" runat="server" resourcekey="btnApplyStyleTemplate" OnClick="btnApplyStyleTemplate_Click" />
                        
                        <fieldset id="pnlStyleDescription" runat="server"><legend><asp:Label runat="server" resourcekey="StyleDescription" /></legend>
                            <asp:Label ID="lblStyleDescription" runat="server" />
                        </fieldset>
                        <asp:Image ID="imgStylePreview" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="btnSubmit" /></Triggers></asp:UpdatePanel>
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>

<asp:UpdatePanel ID="upnlValidation" runat="server" UpdateMode="Always"><ContentTemplate>
    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="true" CssClass="NormalRed" />
    <asp:Panel ID="pnlManifestValidation" runat="server" CssClass="NormalRed"/>
</ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="btnSubmit" /><asp:AsyncPostBackTrigger ControlID="tabsSettings$tabTemplates$ddlStyleTemplates" /></Triggers></asp:UpdatePanel>
<br />
<asp:UpdatePanel ID="upnlSubmit" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False" RenderMode="Inline" ><ContentTemplate>
<asp:Button ID="btnSubmit" runat="server" resourcekey="btnSubmit" OnClick="btnSubmit_Click" EnableViewState="false" />&nbsp;
</ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="tabsSettings$tabTemplates$ddlStyleTemplates" /></Triggers></asp:UpdatePanel>
<asp:Button ID="btnCancel" runat="server" resourcekey="btnCancel" OnClick="btnCancel_Click" CausesValidation="false" EnableViewState="false" />
