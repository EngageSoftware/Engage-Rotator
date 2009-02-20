// <copyright file="RotatorSettings.ascx.cs" company="Engage Software">
// Engage: Rotator - http://www.engagemodules.com
// Copyright (c) 2004-2009
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

namespace Engage.Dnn.ContentRotator
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Web.Hosting;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;
    using System.Xml.Schema;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Framework;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.UI.Utilities;

    public partial class RotatorSettings : PortalModuleBase
    {
        private const string DisabledTextBoxCssClass = "disabledTextBox";

        protected decimal AnimationDuration
        {
            get
            {
                return Dnn.Utility.GetDecimalSetting(this.Settings, "AnimationDuration", 0.3m);
            }
        }

        protected int AnimationFramesPerSecond
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "AnimationFramesPerSecond", 30);
            }
        }

        protected DisplayType ContentDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ContentDisplayMode", DisplayType.Content);
            }
        }

        protected string ContentHeaderLink
        {
            get
            {
                return this.Settings["ContentHeaderLink"] as string;
            }
        }

        protected string ContentHeaderLinkText
        {
            get
            {
                return this.Settings["ContentHeaderLinkText"] as string;
            }
        }

        protected string ContentHeaderText
        {
            get
            {
                return this.Settings["ContentHeaderText"] as string;
            }
        }

        protected int? ContentHeight
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "ContentHeight");
            }
        }

        protected DisplayType ContentTitleDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ContentTitleDisplayMode", DisplayType.Link);
            }
        }

        protected int? ContentWidth
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "ContentWidth");
            }
        }

        protected ControlPlacement NextButtonPlacement
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ControlsNextPlacement", ControlPlacement.Below);
            }
        }

        protected ControlPlacement PauseButtonPlacement
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ControlsPausePlacement", ControlPlacement.Above);
            }
        }

        protected bool PauseOnMouseOver
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "AnimationPauseOnMouseOver", true);
            }
        }

        protected ControlPlacement PositionCounterPlacement
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "PositionCounterPlacement", ControlPlacement.Below);
            }
        }

        protected DisplayType PositionThumbnailDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "PositionThumbnailDisplayMode", DisplayType.Link);
            }
        }

        protected int? PositionThumbnailHeight
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "PositionThumbnailHeight");
            }
        }

        protected int? PositionThumbnailWidth
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "PositionThumbnailWidth");
            }
        }

        protected DisplayType PositionTitleDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ControlsTitleDisplayMode", DisplayType.Link);
            }
        }

        protected ControlPlacement PreviousButtonPlacement
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ControlsPreviousPlacement", ControlPlacement.Above);
            }
        }

        protected int RotatorDelay
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "RotatorDelay", 8);
            }
        }

        protected int? RotatorHeight
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "RotatorHeight");
            }
        }

        protected int RotatorPauseDelay
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "RotatorPauseDelay", 3);
            }
        }

        protected int? RotatorWidth
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "RotatorWidth");
            }
        }

        protected bool ShowContentHeader
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "ShowContentHeader", false);
            }
        }

        protected bool ShowContentHeaderLink
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "ShowContentHeaderLink", false);
            }
        }

        protected bool ShowNextButton
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "ControlsShowNextButton", true);
            }
        }

        protected bool ShowPauseButton
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "ControlsShowPauseButton", true);
            }
        }

        protected bool ShowPositionThumbnail
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "ShowPositionThumbnail", true);
            }
        }

        protected bool ShowPreviousButton
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "ControlsShowPrevButton", true);
            }
        }

        protected bool ShowReadMoreLink
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "ShowReadMoreLink", false);
            }
        }

        protected DisplayType ThumbnailDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ThumbnailDisplayMode", DisplayType.Link);
            }
        }

        protected int? ThumbnailHeight
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "ThumbnailHeight");
            }
        }

        protected int? ThumbnailWidth
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "ThumbnailWidth");
            }
        }

        protected bool UseAnimations
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "UseAnimations", true);
            }
        }

        private bool ShowPositionCounter
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "ShowPositionCounter", false);
            }
        }

        private string StyleTemplate
        {
            get
            {
                return this.Settings["StyleTemplate"] as string;
            }
        }

        protected void btnApplyStyleTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                ModuleController modules = new ModuleController();
                modules.UpdateTabModuleSetting(this.TabModuleId, "StyleTemplate", this.ddlStyleTemplates.SelectedValue);

                try
                {
                    TemplateManifest manifest =
                            TemplateManifest.CreateTemplateManifest(this.ddlStyleTemplates.SelectedValue);
                    if (manifest.Settings != null && manifest.Settings.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> setting in manifest.Settings)
                        {
                            modules.UpdateTabModuleSetting(this.TabModuleId, setting.Key, setting.Value);
                        }
                    }

                    // return to this page with the new settings applied
                    this.Response.Redirect(this.EditUrl("ModSettings"), false);
                }
                catch (XmlSchemaValidationException)
                {
                    this.ShowManifestValidationErrorMessage();
                }
                catch (XmlException)
                {
                    this.ShowManifestValidationErrorMessage();
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.Response.Redirect(DotNetNuke.Common.Globals.NavigateURL((this.TabId)), false);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                ModuleController modules = new ModuleController();
                modules.UpdateTabModuleSetting(this.TabModuleId, "RotatorWidth", this.txtRotatorWidth.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "RotatorHeight", this.txtRotatorHeight.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ContentWidth", this.txtContentWidth.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ContentHeight", this.txtContentHeight.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ThumbnailWidth", this.txtThumbnailWidth.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ThumbnailHeight", this.txtThumbnailHeight.Text);
                modules.UpdateTabModuleSetting(
                        this.TabModuleId, "PositionThumbnailWidth", this.txtPositionThumbnailWidth.Text);
                modules.UpdateTabModuleSetting(
                        this.TabModuleId, "PositionThumbnailHeight", this.txtPositionThumbnailHeight.Text);

                modules.UpdateTabModuleSetting(this.TabModuleId, "RotatorDelay", this.txtRotatorDelay.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "RotatorPauseDelay", this.txtRotatorPauseDelay.Text);
                modules.UpdateTabModuleSetting(
                        this.TabModuleId, "AnimationFramesPerSecond", this.txtAnimationFramesPerSecond.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "AnimationDuration", this.txtAnimationDuration.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ContentHeaderText", this.txtContentHeaderTitle.Text);
                modules.UpdateTabModuleSetting(
                        this.TabModuleId,
                        "ContentHeaderLink",
                        Dnn.Utility.CreateUrlFromControl(this.urlContentHeaderLink, this.PortalSettings));
                modules.UpdateTabModuleSetting(
                        this.TabModuleId, "ContentHeaderLinkText", this.txtContentHeaderLinkText.Text);

                modules.UpdateTabModuleSetting(
                        this.TabModuleId, "ControlsTitleDisplayMode", this.rblPositionTitleDisplay.SelectedValue);
                modules.UpdateTabModuleSetting(
                        this.TabModuleId, "ContentTitleDisplayMode", this.rblContentTitleDisplay.SelectedValue);
                modules.UpdateTabModuleSetting(
                        this.TabModuleId, "ContentDisplayMode", this.rblContentDisplay.SelectedValue);
                modules.UpdateTabModuleSetting(
                        this.TabModuleId, "ThumbnailDisplayMode", this.rblThumbnailDisplay.SelectedValue);

                modules.UpdateTabModuleSetting(
                        this.TabModuleId,
                        "ShowContentHeader",
                        this.chkShowContentHeaderTitle.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(
                        this.TabModuleId,
                        "ShowContentHeaderLink",
                        this.chkShowContentHeaderLink.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(
                        this.TabModuleId,
                        "ShowReadMoreLink",
                        this.chkShowReadMoreLink.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(
                        this.TabModuleId,
                        "ControlsShowPrevButton",
                        this.chkShowPreviousButton.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(
                        this.TabModuleId,
                        "ControlsShowPauseButton",
                        this.chkShowPauseButton.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(
                        this.TabModuleId,
                        "ControlsShowNextButton",
                        this.chkShowNextButton.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(
                        this.TabModuleId, "ControlsPreviousPlacement", this.ddlPreviousButtonLocation.SelectedValue);
                modules.UpdateTabModuleSetting(
                        this.TabModuleId, "ControlsPausePlacement", this.ddlPauseButtonLocation.SelectedValue);
                modules.UpdateTabModuleSetting(
                        this.TabModuleId, "ControlsNextPlacement", this.ddlNextButtonLocation.SelectedValue);

                modules.UpdateTabModuleSetting(
                        this.TabModuleId, "PositionThumbnailDisplayMode", this.rblPositionThumbnailDisplay.SelectedValue);
                modules.UpdateTabModuleSetting(
                        this.TabModuleId,
                        "UseAnimations",
                        this.chkUseAnimations.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(
                        this.TabModuleId,
                        "AnimationPauseOnMouseOver",
                        this.chkPauseOnMouseOver.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(
                        this.TabModuleId,
                        "ShowPositionCounter",
                        this.chkShowPositionCounter.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(
                        this.TabModuleId, "PositionCounterPlacement", this.ddlPositionCounterLocation.SelectedValue);
                this.Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(this.TabId), false);
            }
        }

        protected void chkPauseOnMouseOver_CheckedChanged(object sender, EventArgs e)
        {
            this.ProcessMouseOverVisibility();
        }

        protected void chkShowContentHeaderLink_CheckedChanged(object sender, EventArgs e)
        {
            this.ProcessContentHeaderLinkVisiblity();
        }

        protected void chkShowContentHeaderTitle_CheckedChanged(object sender, EventArgs e)
        {
            this.ProcessContentHeaderVisiblity();
        }

        protected void chkShowNextButton_CheckedChanged(object sender, EventArgs e)
        {
            this.ProcessNextVisibility();
        }

        protected void chkShowPauseButton_CheckedChanged(object sender, EventArgs e)
        {
            this.ProcessPauseVisibility();
        }

        protected void chkShowPositionCounter_CheckedChanged(object sender, EventArgs e)
        {
            this.ProcessPositionCounterVisibility();
        }

        protected void chkShowPreviousButton_CheckedChanged(object sender, EventArgs e)
        {
            this.ProcessPreviousVisibility();
        }

        protected void chkUseAnimations_CheckedChanged(object sender, EventArgs e)
        {
            this.ProcessAnimationsVisiblity();
        }

        protected void ddlStyleTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlStyleTemplates.Attributes["OriginalStyleTemplate"] != this.ddlStyleTemplates.SelectedValue)
            {
                ClientAPI.AddButtonConfirm(
                        this.btnSubmit, Localization.GetString("TemplateChangedConfirm", this.LocalResourceFile));
            }

            this.FillTemplateTab();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (AJAX.IsInstalled())
            {
                AJAX.RegisterScriptManager();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    this.rblPositionTitleDisplay.Items.Clear();
                    this.rblPositionTitleDisplay.Items.Add(
                            new ListItem(
                                    Localization.GetString(DisplayType.None.ToString(), this.LocalResourceFile),
                                    DisplayType.None.ToString()));
                    this.rblPositionTitleDisplay.Items.Add(
                            new ListItem(
                                    Localization.GetString(DisplayType.Content.ToString(), this.LocalResourceFile),
                                    DisplayType.Content.ToString()));
                    this.rblPositionTitleDisplay.Items.Add(
                            new ListItem(
                                    Localization.GetString(DisplayType.Link.ToString(), this.LocalResourceFile),
                                    DisplayType.Link.ToString()));
                    this.rblPositionTitleDisplay.Items.Add(
                            new ListItem(
                                    Localization.GetString(DisplayType.RotateContent.ToString(), this.LocalResourceFile),
                                    DisplayType.RotateContent.ToString()));

                    this.rblPositionThumbnailDisplay.Items.Clear();
                    this.rblPositionThumbnailDisplay.Items.Add(
                            new ListItem(
                                    Localization.GetString(DisplayType.None.ToString(), this.LocalResourceFile),
                                    DisplayType.None.ToString()));
                    this.rblPositionThumbnailDisplay.Items.Add(
                            new ListItem(
                                    Localization.GetString(DisplayType.Content.ToString(), this.LocalResourceFile),
                                    DisplayType.Content.ToString()));
                    this.rblPositionThumbnailDisplay.Items.Add(
                            new ListItem(
                                    Localization.GetString(DisplayType.Link.ToString(), this.LocalResourceFile),
                                    DisplayType.Link.ToString()));
                    this.rblPositionThumbnailDisplay.Items.Add(
                            new ListItem(
                                    Localization.GetString(DisplayType.RotateContent.ToString(), this.LocalResourceFile),
                                    DisplayType.RotateContent.ToString()));

                    this.rblContentTitleDisplay.Items.Clear();
                    this.rblContentTitleDisplay.Items.Add(
                            new ListItem(
                                    Localization.GetString(DisplayType.None.ToString(), this.LocalResourceFile),
                                    DisplayType.None.ToString()));
                    this.rblContentTitleDisplay.Items.Add(
                            new ListItem(
                                    Localization.GetString(DisplayType.Content.ToString(), this.LocalResourceFile),
                                    DisplayType.Content.ToString()));
                    this.rblContentTitleDisplay.Items.Add(
                            new ListItem(
                                    Localization.GetString(DisplayType.Link.ToString(), this.LocalResourceFile),
                                    DisplayType.Link.ToString()));

                    this.rblContentDisplay.Items.Clear();
                    this.rblContentDisplay.Items.Add(
                            new ListItem(
                                    Localization.GetString(DisplayType.None.ToString(), this.LocalResourceFile),
                                    DisplayType.None.ToString()));
                    this.rblContentDisplay.Items.Add(
                            new ListItem(
                                    Localization.GetString(DisplayType.Content.ToString(), this.LocalResourceFile),
                                    DisplayType.Content.ToString()));
                    this.rblContentDisplay.Items.Add(
                            new ListItem(
                                    Localization.GetString(DisplayType.Link.ToString(), this.LocalResourceFile),
                                    DisplayType.Link.ToString()));

                    this.rblThumbnailDisplay.Items.Clear();
                    this.rblThumbnailDisplay.Items.Add(
                            new ListItem(
                                    Localization.GetString(DisplayType.None.ToString(), this.LocalResourceFile),
                                    DisplayType.None.ToString()));
                    this.rblThumbnailDisplay.Items.Add(
                            new ListItem(
                                    Localization.GetString(DisplayType.Content.ToString(), this.LocalResourceFile),
                                    DisplayType.Content.ToString()));
                    this.rblThumbnailDisplay.Items.Add(
                            new ListItem(
                                    Localization.GetString(DisplayType.Link.ToString(), this.LocalResourceFile),
                                    DisplayType.Link.ToString()));

                    this.ddlPreviousButtonLocation.Items.Clear();
                    this.ddlPreviousButtonLocation.Items.Add(
                            new ListItem(
                                    Localization.GetString(ControlPlacement.Above.ToString(), this.LocalResourceFile),
                                    ControlPlacement.Above.ToString()));
                    this.ddlPreviousButtonLocation.Items.Add(
                            new ListItem(
                                    Localization.GetString(ControlPlacement.Below.ToString(), this.LocalResourceFile),
                                    ControlPlacement.Below.ToString()));

                    this.ddlPauseButtonLocation.Items.Clear();
                    this.ddlPauseButtonLocation.Items.Add(
                            new ListItem(
                                    Localization.GetString(ControlPlacement.Above.ToString(), this.LocalResourceFile),
                                    ControlPlacement.Above.ToString()));
                    this.ddlPauseButtonLocation.Items.Add(
                            new ListItem(
                                    Localization.GetString(ControlPlacement.Below.ToString(), this.LocalResourceFile),
                                    ControlPlacement.Below.ToString()));

                    this.ddlNextButtonLocation.Items.Clear();
                    this.ddlNextButtonLocation.Items.Add(
                            new ListItem(
                                    Localization.GetString(ControlPlacement.Above.ToString(), this.LocalResourceFile),
                                    ControlPlacement.Above.ToString()));
                    this.ddlNextButtonLocation.Items.Add(
                            new ListItem(
                                    Localization.GetString(ControlPlacement.Below.ToString(), this.LocalResourceFile),
                                    ControlPlacement.Below.ToString()));

                    this.ddlPositionCounterLocation.Items.Clear();
                    this.ddlPositionCounterLocation.Items.Add(
                            new ListItem(
                                    Localization.GetString(ControlPlacement.Above.ToString(), this.LocalResourceFile),
                                    ControlPlacement.Above.ToString()));
                    this.ddlPositionCounterLocation.Items.Add(
                            new ListItem(
                                    Localization.GetString(ControlPlacement.Below.ToString(), this.LocalResourceFile),
                                    ControlPlacement.Below.ToString()));

                    this.ddlStyleTemplates.Items.Clear();
                    this.ddlStyleTemplates.Items.Add(
                            new ListItem(Localization.GetString("None", this.LocalResourceFile), string.Empty));
                    foreach (string directory in
                            Directory.GetDirectories(
                                    HostingEnvironment.MapPath(
                                            Utility.DesktopModuleVirtualPath + Utility.StyleTemplatesFolderName)))
                    {
                        this.ddlStyleTemplates.Items.Add(
                                new ListItem(
                                        directory.Substring(directory.LastIndexOf(Path.DirectorySeparatorChar) + 1)));
                    }

                    this.txtRotatorWidth.Text = this.RotatorWidth.HasValue
                                                        ? this.RotatorWidth.Value.ToString(CultureInfo.CurrentCulture)
                                                        : string.Empty;
                    this.txtRotatorHeight.Text = this.RotatorHeight.HasValue
                                                         ? this.RotatorHeight.Value.ToString(CultureInfo.CurrentCulture)
                                                         : string.Empty;

                    // txtControlsMarginLeft.Text = ControlsMarginLeft.HasValue ? ControlsMarginLeft.Value.ToString(CultureInfo.CurrentCulture) : string.Empty;
                    // txtControlsMarginTop.Text = ControlsMarginTop.HasValue ? ControlsMarginTop.Value.ToString(CultureInfo.CurrentCulture) : string.Empty;
                    this.rblPositionTitleDisplay.SelectedValue = this.PositionTitleDisplayMode.ToString();
                    this.rblPositionThumbnailDisplay.SelectedValue = this.PositionThumbnailDisplayMode.ToString();
                    this.rblContentTitleDisplay.SelectedValue = this.ContentTitleDisplayMode.ToString();

                    this.rblContentDisplay.SelectedValue = this.ContentDisplayMode.ToString();
                    this.txtContentWidth.Text = this.ContentWidth.HasValue
                                                        ? this.ContentWidth.Value.ToString(CultureInfo.CurrentCulture)
                                                        : string.Empty;
                    this.txtContentHeight.Text = this.ContentHeight.HasValue
                                                         ? this.ContentHeight.Value.ToString(CultureInfo.CurrentCulture)
                                                         : string.Empty;
                    this.ProcessContentVisibility();

                    this.rblThumbnailDisplay.SelectedValue = this.ThumbnailDisplayMode.ToString();
                    this.txtThumbnailWidth.Text = this.ThumbnailWidth.HasValue
                                                          ? this.ThumbnailWidth.Value.ToString(
                                                                    CultureInfo.CurrentCulture)
                                                          : string.Empty;
                    this.txtThumbnailHeight.Text = this.ThumbnailHeight.HasValue
                                                           ? this.ThumbnailHeight.Value.ToString(
                                                                     CultureInfo.CurrentCulture)
                                                           : string.Empty;
                    this.ProcessThumbnailVisibility();

                    // chkShowPositionThumbnail.Checked = ShowPositionThumbnail;
                    this.txtPositionThumbnailWidth.Text = this.PositionThumbnailWidth.HasValue
                                                                  ? this.PositionThumbnailWidth.Value.ToString(
                                                                            CultureInfo.CurrentCulture)
                                                                  : string.Empty;
                    this.txtPositionThumbnailHeight.Text = this.PositionThumbnailHeight.HasValue
                                                                   ? this.PositionThumbnailHeight.Value.ToString(
                                                                             CultureInfo.CurrentCulture)
                                                                   : string.Empty;
                    this.ProcessPositionThumbnailVisibility();

                    this.chkShowContentHeaderTitle.Checked = this.ShowContentHeader;
                    this.txtContentHeaderTitle.Text = this.ContentHeaderText;
                    this.ProcessContentHeaderVisiblity();

                    this.chkShowContentHeaderLink.Checked = this.ShowContentHeaderLink;
                    this.txtContentHeaderLinkText.Text = this.ContentHeaderLinkText;
                    this.urlContentHeaderLink.Url = this.ContentHeaderLink;

                    // Show tabs if there is no url, show as a url if there is anything.  BD
                    this.urlContentHeaderLink.UrlType = string.IsNullOrEmpty(this.ContentHeaderLink) ? "T" : "U";
                    this.ProcessContentHeaderLinkVisiblity();

                    this.chkPauseOnMouseOver.Checked = this.PauseOnMouseOver;
                    this.txtRotatorDelay.Text = this.RotatorDelay.ToString(CultureInfo.CurrentCulture);
                    this.txtRotatorPauseDelay.Text = this.RotatorPauseDelay.ToString(CultureInfo.CurrentCulture);
                    this.ProcessMouseOverVisibility();

                    this.chkUseAnimations.Checked = this.UseAnimations;
                    this.txtAnimationFramesPerSecond.Text =
                            this.AnimationFramesPerSecond.ToString(CultureInfo.CurrentCulture);
                    this.txtAnimationDuration.Text = this.AnimationDuration.ToString(CultureInfo.CurrentCulture);
                    this.chkPauseOnMouseOver.Checked = this.PauseOnMouseOver;
                    this.ProcessAnimationsVisiblity();

                    this.chkShowReadMoreLink.Checked = this.ShowReadMoreLink;
                    this.chkShowPreviousButton.Checked = this.ShowPreviousButton;
                    this.ddlPreviousButtonLocation.SelectedValue = this.PreviousButtonPlacement.ToString();
                    this.ProcessPreviousVisibility();
                    this.chkShowPauseButton.Checked = this.ShowPauseButton;
                    this.ddlPauseButtonLocation.SelectedValue = this.PauseButtonPlacement.ToString();
                    this.ProcessPauseVisibility();
                    this.chkShowNextButton.Checked = this.ShowNextButton;
                    this.ddlNextButtonLocation.SelectedValue = this.NextButtonPlacement.ToString();
                    this.ProcessNextVisibility();
                    this.chkShowPositionCounter.Checked = this.ShowPositionCounter;
                    this.ddlPositionCounterLocation.SelectedValue = this.PositionCounterPlacement.ToString();
                    this.ProcessPositionCounterVisibility();

                    this.ddlStyleTemplates.SelectedValue = this.StyleTemplate;
                    this.ddlStyleTemplates.Attributes.Add("OriginalStyleTemplate", this.StyleTemplate);
                    this.FillTemplateTab();
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void rblContentDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ProcessContentVisibility();
        }

        protected void rblPositionThumbnailDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ProcessPositionThumbnailVisibility();
        }

        protected void rblThumbnailDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ProcessThumbnailVisibility();
        }

        protected void valContentHeaderLink_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (e != null)
            {
                // valid if there is a url, or we aren't using the header link.
                // the validator should be disabled if chkShowContentHeaderLink is unchecked, this is just a doublecheck.  BD
                e.IsValid = !string.IsNullOrEmpty(this.urlContentHeaderLink.Url)
                            || !this.chkShowContentHeaderLink.Checked;
            }
        }

        private void FillTemplateTab()
        {
            try
            {
                this.btnApplyStyleTemplate.Enabled = true;
                TemplateManifest manifest = TemplateManifest.CreateTemplateManifest(
                        this.ddlStyleTemplates.SelectedValue);
                this.lblStyleDescription.Text = manifest.Description;
                this.pnlStyleDescription.Visible = Engage.Utility.HasValue(this.lblStyleDescription.Text);
                string templateFolder = Utility.DesktopModuleVirtualPath + Utility.StyleTemplatesFolderName
                                        + this.ddlStyleTemplates.SelectedValue;
                this.imgStylePreview.ImageUrl = templateFolder + "/" + manifest.PreviewImageFilename;
                this.imgStylePreview.Visible = File.Exists(HostingEnvironment.MapPath(this.imgStylePreview.ImageUrl));
            }
            catch (XmlSchemaValidationException)
            {
                this.ShowManifestValidationErrorMessage();
            }
        }

        private void ProcessAnimationsVisiblity()
        {
            this.txtAnimationFramesPerSecond.Enabled = this.chkUseAnimations.Checked;
            this.valAnimationFramesPerSecond.Enabled = this.chkUseAnimations.Checked;
            this.rfvAnimationFramesPerSecond.Enabled = this.chkUseAnimations.Checked;

            this.txtAnimationDuration.Enabled = this.chkUseAnimations.Checked;
            this.valAnimationDuration.Enabled = this.chkUseAnimations.Checked;
            this.rfvAnimationDuration.Enabled = this.chkUseAnimations.Checked;

            this.txtAnimationFramesPerSecond.CssClass = !this.txtAnimationFramesPerSecond.Enabled
                                                                ? Engage.Utility.AddCssClass(
                                                                          this.txtAnimationFramesPerSecond.CssClass,
                                                                          DisabledTextBoxCssClass)
                                                                : Engage.Utility.RemoveCssClass(
                                                                          this.txtAnimationFramesPerSecond.CssClass,
                                                                          DisabledTextBoxCssClass);
            this.txtAnimationDuration.CssClass = !this.txtAnimationDuration.Enabled
                                                         ? Engage.Utility.AddCssClass(
                                                                   this.txtAnimationDuration.CssClass,
                                                                   DisabledTextBoxCssClass)
                                                         : Engage.Utility.RemoveCssClass(
                                                                   this.txtAnimationDuration.CssClass,
                                                                   DisabledTextBoxCssClass);
        }

        private void ProcessContentHeaderLinkVisiblity()
        {
            this.txtContentHeaderLinkText.Enabled = this.chkShowContentHeaderLink.Checked;
            this.rfvContentHeaderLinkText.Enabled = this.chkShowContentHeaderLink.Checked;

            this.urlContentHeaderLink.Visible = this.chkShowContentHeaderLink.Checked;
            this.valContentHeaderLink.Enabled = this.chkShowContentHeaderLink.Checked;

            this.txtContentHeaderLinkText.CssClass = !this.txtContentHeaderLinkText.Enabled
                                                             ? Engage.Utility.AddCssClass(
                                                                       this.txtContentHeaderLinkText.CssClass,
                                                                       DisabledTextBoxCssClass)
                                                             : Engage.Utility.RemoveCssClass(
                                                                       this.txtContentHeaderLinkText.CssClass,
                                                                       DisabledTextBoxCssClass);
        }

        private void ProcessContentHeaderVisiblity()
        {
            this.txtContentHeaderTitle.Enabled = this.chkShowContentHeaderTitle.Checked;
            this.rfvContentHeaderTitle.Enabled = this.chkShowContentHeaderTitle.Checked;

            this.txtContentHeaderTitle.CssClass = !this.txtContentHeaderTitle.Enabled
                                                          ? Engage.Utility.AddCssClass(
                                                                    this.txtContentHeaderTitle.CssClass,
                                                                    DisabledTextBoxCssClass)
                                                          : Engage.Utility.RemoveCssClass(
                                                                    this.txtContentHeaderTitle.CssClass,
                                                                    DisabledTextBoxCssClass);
        }

        private void ProcessContentVisibility()
        {
            this.txtContentHeight.Enabled = this.rblContentDisplay.SelectedValue != DisplayType.None.ToString();
            this.valContentHeight.Enabled = this.txtContentHeight.Enabled;

            this.txtContentWidth.Enabled = this.rblContentDisplay.SelectedValue != DisplayType.None.ToString();
            this.valContentWidth.Enabled = this.txtContentWidth.Enabled;

            this.txtContentHeight.CssClass = !this.txtContentHeight.Enabled
                                                     ? Engage.Utility.AddCssClass(
                                                               this.txtContentHeight.CssClass, DisabledTextBoxCssClass)
                                                     : Engage.Utility.RemoveCssClass(
                                                               this.txtContentHeight.CssClass, DisabledTextBoxCssClass);
            this.txtContentWidth.CssClass = !this.txtContentWidth.Enabled
                                                    ? Engage.Utility.AddCssClass(
                                                              this.txtContentWidth.CssClass, DisabledTextBoxCssClass)
                                                    : Engage.Utility.RemoveCssClass(
                                                              this.txtContentWidth.CssClass, DisabledTextBoxCssClass);
        }

        private void ProcessMouseOverVisibility()
        {
            this.txtRotatorPauseDelay.Enabled = this.chkPauseOnMouseOver.Checked;
            this.valRotatorPauseDelay.Enabled = this.chkPauseOnMouseOver.Checked;
            this.rfvRotatorPauseDelay.Enabled = this.chkPauseOnMouseOver.Checked;

            this.txtRotatorPauseDelay.CssClass = !this.txtRotatorPauseDelay.Enabled
                                                         ? Engage.Utility.AddCssClass(
                                                                   this.txtRotatorPauseDelay.CssClass,
                                                                   DisabledTextBoxCssClass)
                                                         : Engage.Utility.RemoveCssClass(
                                                                   this.txtRotatorPauseDelay.CssClass,
                                                                   DisabledTextBoxCssClass);
        }

        private void ProcessNextVisibility()
        {
            this.ddlNextButtonLocation.Enabled = this.chkShowNextButton.Checked;

            // urlNextButtonImage.Visible = chkShowNextButton.Checked;
            this.ddlNextButtonLocation.CssClass = !this.ddlNextButtonLocation.Enabled
                                                          ? Engage.Utility.AddCssClass(
                                                                    this.ddlNextButtonLocation.CssClass,
                                                                    DisabledTextBoxCssClass)
                                                          : Engage.Utility.RemoveCssClass(
                                                                    this.ddlNextButtonLocation.CssClass,
                                                                    DisabledTextBoxCssClass);
        }

        private void ProcessPauseVisibility()
        {
            this.ddlPauseButtonLocation.Enabled = this.chkShowPauseButton.Checked;

            // urlPauseButtonImage.Visible = chkShowPauseButton.Checked;
            this.ddlPauseButtonLocation.CssClass = !this.ddlPauseButtonLocation.Enabled
                                                           ? Engage.Utility.AddCssClass(
                                                                     this.ddlPauseButtonLocation.CssClass,
                                                                     DisabledTextBoxCssClass)
                                                           : Engage.Utility.RemoveCssClass(
                                                                     this.ddlPauseButtonLocation.CssClass,
                                                                     DisabledTextBoxCssClass);
        }

        private void ProcessPositionCounterVisibility()
        {
            this.ddlPositionCounterLocation.Enabled = this.chkShowPositionCounter.Checked;
            this.ddlPositionCounterLocation.CssClass = !this.ddlPositionCounterLocation.Enabled
                                                               ? Engage.Utility.AddCssClass(
                                                                         this.ddlPositionCounterLocation.CssClass,
                                                                         DisabledTextBoxCssClass)
                                                               : Engage.Utility.RemoveCssClass(
                                                                         this.ddlPositionCounterLocation.CssClass,
                                                                         DisabledTextBoxCssClass);
        }

        private void ProcessPositionThumbnailVisibility()
        {
            DisplayType positionThumbnailDisplayMode =
                    (DisplayType)Enum.Parse(typeof(DisplayType), this.rblPositionThumbnailDisplay.SelectedValue, true);
            this.txtPositionThumbnailHeight.Enabled =
                    this.valPositionThumbnailHeight.Enabled =
                    this.txtPositionThumbnailWidth.Enabled =
                    this.valPositionThumbnailWidth.Enabled = positionThumbnailDisplayMode != DisplayType.None;

            this.txtPositionThumbnailHeight.CssClass = !this.txtPositionThumbnailHeight.Enabled
                                                               ? Engage.Utility.AddCssClass(
                                                                         this.txtPositionThumbnailHeight.CssClass,
                                                                         DisabledTextBoxCssClass)
                                                               : Engage.Utility.RemoveCssClass(
                                                                         this.txtPositionThumbnailHeight.CssClass,
                                                                         DisabledTextBoxCssClass);
            this.txtPositionThumbnailWidth.CssClass = !this.txtPositionThumbnailWidth.Enabled
                                                              ? Engage.Utility.AddCssClass(
                                                                        this.txtPositionThumbnailWidth.CssClass,
                                                                        DisabledTextBoxCssClass)
                                                              : Engage.Utility.RemoveCssClass(
                                                                        this.txtPositionThumbnailWidth.CssClass,
                                                                        DisabledTextBoxCssClass);
        }

        private void ProcessPreviousVisibility()
        {
            this.ddlPreviousButtonLocation.Enabled = this.chkShowPreviousButton.Checked;

            // urlPreviousButtonImage.Visible = chkShowPreviousButton.Checked;
            this.ddlPreviousButtonLocation.CssClass = !this.ddlPreviousButtonLocation.Enabled
                                                              ? Engage.Utility.AddCssClass(
                                                                        this.ddlPreviousButtonLocation.CssClass,
                                                                        DisabledTextBoxCssClass)
                                                              : Engage.Utility.RemoveCssClass(
                                                                        this.ddlPreviousButtonLocation.CssClass,
                                                                        DisabledTextBoxCssClass);
        }

        private void ProcessThumbnailVisibility()
        {
            this.txtThumbnailHeight.Enabled = this.rblThumbnailDisplay.SelectedValue != DisplayType.None.ToString();
            this.valThumbnailHeight.Enabled = this.txtThumbnailHeight.Enabled;

            this.txtThumbnailWidth.Enabled = this.rblThumbnailDisplay.SelectedValue != DisplayType.None.ToString();
            this.valThumbnailWidth.Enabled = this.txtThumbnailWidth.Enabled;

            this.txtThumbnailHeight.CssClass = !this.txtThumbnailHeight.Enabled
                                                       ? Engage.Utility.AddCssClass(
                                                                 this.txtThumbnailHeight.CssClass,
                                                                 DisabledTextBoxCssClass)
                                                       : Engage.Utility.RemoveCssClass(
                                                                 this.txtThumbnailHeight.CssClass,
                                                                 DisabledTextBoxCssClass);
            this.txtThumbnailWidth.CssClass = !this.txtThumbnailWidth.Enabled
                                                      ? Engage.Utility.AddCssClass(
                                                                this.txtThumbnailWidth.CssClass, DisabledTextBoxCssClass)
                                                      : Engage.Utility.RemoveCssClass(
                                                                this.txtThumbnailWidth.CssClass, DisabledTextBoxCssClass);
        }

        private void ShowManifestValidationErrorMessage()
        {
            this.pnlManifestValidation.Controls.Add(
                    new LiteralControl(
                            "<ul><li>" + Localization.GetString("ManifestValidation", this.LocalResourceFile)
                            + "</li></ul>"));
            this.pnlStyleDescription.Visible = false;
            this.imgStylePreview.Visible = false;
            this.btnApplyStyleTemplate.Enabled = false;
        }
    }
}