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
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.UI.Utilities;
    using Globals = DotNetNuke.Common.Globals;

    /// <summary>
    /// Code-behind for the settings control for Rotator
    /// </summary>
    public partial class RotatorSettings : ModuleBase
    {
        /// <summary>
        /// The CSS class to use for disabled text boxes
        /// </summary>
        private const string DisabledTextBoxCssClass = "NormalDisabled";

        /// <summary>
        /// An array of <see cref="ListItem"/>s for the common <see cref="DisplayType"/> options
        /// </summary>
        private ListItem[] displayTypeItems;

        /// <summary>
        /// Gets the duration of the transition animation.
        /// </summary>
        /// <value>The duration of the animation (in seconds).</value>
        private decimal AnimationDuration
        {
            get
            {
                return Dnn.Utility.GetDecimalSetting(this.Settings, "AnimationDuration", 0.3m);
            }
        }

        /// <summary>
        /// Gets a value indicating whether to automatically resize the container to fit the largest <see cref="ContentItem"/>.
        /// </summary>
        /// <value><c>true</c> if the option to automatically resize the container to fit the largest <see cref="ContentItem"/> is set; otherwise, <c>false</c>.</value>
        private bool ContainerResize
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "ContainerResize", true);
            }
        }

        /// <summary>
        /// Gets a value indicating whether to start the next transition immediately after the current one completes.
        /// </summary>
        /// <value><c>true</c> if the option to start the next transition immediately after the current one completes is set; otherwise, <c>false</c>.</value>
        private bool Continuous
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "Continuous", false);
            }
        }

        /// <summary>
        /// Gets the setting for the display mode of the main content.
        /// </summary>
        /// <value>The content display mode.</value>
        private DisplayType ContentDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ContentDisplayMode", DisplayType.Content);
            }
        }

        /// <summary>
        /// Gets the setting for the height of the main content.
        /// </summary>
        /// <value>The height of the main content (in <c>px</c>).</value>
        private int? ContentHeight
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "ContentHeight");
            }
        }

        /// <summary>
        /// Gets the setting for the display mode of the content title.
        /// </summary>
        /// <value>The content title display mode.</value>
        private DisplayType ContentTitleDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ContentTitleDisplayMode", DisplayType.Link);
            }
        }

        /// <summary>
        /// Gets the setting for the width of the main content.
        /// </summary>
        /// <value>The width of the main content (in <c>px</c>).</value>
        private int? ContentWidth
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "ContentWidth");
            }
        }

        /// <summary>
        /// Gets a value indicating whether to pause rotation when the content is moused over.
        /// </summary>
        /// <value><c>true</c> if the module is set to pause rotation when the content is moused over; otherwise, <c>false</c>.</value>
        private bool PauseOnMouseOver
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "AnimationPauseOnMouseOver", true);
            }
        }

        /// <summary>
        /// Gets a value indicating whether to stop rotation after a certain number of transitions.
        /// </summary>
        /// <value><c>true</c> if the module is set to stop rotation after a certain number of transitions; otherwise, <c>false</c>.</value>
        private bool AutoStop
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "AutoStop", false);
            }
        }

        /// <summary>
        /// Gets a value indicating whether to stop rotation after a certain number of transitions.
        /// </summary>
        /// <value><c>true</c> if the module is set to stop rotation after a certain number of transitions; otherwise, <c>false</c>.</value>
        private int AutoStopCount
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "AutoStopCount", 100);
            }
        }

        /// <summary>
        /// Gets a value indicating the additional delay (in seconds) for the first transition (hint: can be negative).
        /// </summary>
        /// <value>A value indicating the additional delay (in seconds) for the first transition (hint: can be negative)</value>
        private decimal InitialDelay
        {
            get
            {
                return Dnn.Utility.GetDecimalSetting(this.Settings, "InitialDelay", 0);
            }
        }

        /// <summary>
        /// Gets a value indicating the delay (in seconds) for transitions triggered manually (through the pager or previous/next button).
        /// </summary>
        /// <value>A value indicating the delay (in seconds) for transitions triggered manually (through the pager or previous/next button)</value>
        private decimal ManuallyTriggeredTransitionSpeed
        {
            get
            {
                return Dnn.Utility.GetDecimalSetting(this.Settings, "ManuallyTriggeredTransitionSpeed", 0);
            }
        }

        /// <summary>
        /// Gets a value indicating whether to loop rotation, or just display each item once.
        /// </summary>
        /// <value><c>true</c> if the module is set to only show each item once; otherwise, <c>false</c>.</value>
        private bool Loop
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "Loop", true);
            }
        }

        /// <summary>
        /// Gets the setting for the display mode of the position thumbnail.
        /// </summary>
        /// <value>The position thumbnail display mode.</value>
        private DisplayType PositionThumbnailDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "PositionThumbnailDisplayMode", DisplayType.Link);
            }
        }

        /// <summary>
        /// Gets the setting for the height of the position thumbnail.
        /// </summary>
        /// <value>The height of the position thumbnail (in <c>px</c>).</value>
        private int? PositionThumbnailHeight
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "PositionThumbnailHeight");
            }
        }

        /// <summary>
        /// Gets the setting for the width of the position thumbnail.
        /// </summary>
        /// <value>The width of the position thumbnail (in <c>px</c>).</value>
        private int? PositionThumbnailWidth
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "PositionThumbnailWidth");
            }
        }

        /// <summary>
        /// Gets the setting for the display mode of the position title.
        /// </summary>
        /// <value>The position title display mode.</value>
        private DisplayType PositionTitleDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ControlsTitleDisplayMode", DisplayType.Link);
            }
        }

        /// <summary>
        /// Gets the setting for the delay between each item.
        /// </summary>
        /// <value>The rotator delay (in seconds).</value>
        private int RotatorDelay
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "RotatorDelay", 8);
            }
        }

        /// <summary>
        /// Gets the setting for the height of the entire rotator.
        /// </summary>
        /// <value>The height of the rotator (in <c>px</c>).</value>
        private int? RotatorHeight
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "RotatorHeight");
            }
        }

        /// <summary>
        /// Gets the setting for the delay before continuing rotation after the mouse leaves the content area (is <see cref="PauseOnMouseOver"/> is set).
        /// </summary>
        /// <value>The rotator pause delay (in seconds).</value>
        private int RotatorPauseDelay
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "RotatorPauseDelay", 3);
            }
        }

        /// <summary>
        /// Gets the setting for the width of the entire rotator.
        /// </summary>
        /// <value>The width of the rotator (in <c>px</c>).</value>
        private int? RotatorWidth
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "RotatorWidth");
            }
        }

        /// <summary>
        /// Gets the setting for the display mode for the thumbnail.
        /// </summary>
        /// <value>The thumbnail display mode.</value>
        private DisplayType ThumbnailDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ThumbnailDisplayMode", DisplayType.Link);
            }
        }

        /// <summary>
        /// Gets the setting for the height of the thumbnail.
        /// </summary>
        /// <value>The height of the thumbnail (in <c>px</c>).</value>
        private int? ThumbnailHeight
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "ThumbnailHeight");
            }
        }

        /// <summary>
        /// Gets the setting for the width of the thumbnail.
        /// </summary>
        /// <value>The width of the thumbnail (in <c>px</c>).</value>
        private int? ThumbnailWidth
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "ThumbnailWidth");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance of the module is set to use animations.
        /// </summary>
        /// <value><c>true</c> if this instance of the module is set to use animations; otherwise, <c>false</c>.</value>
        private bool UseAnimations
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "UseAnimations", true);
            }
        }

        /// <summary>
        /// Gets the setting for the selected style template.
        /// </summary>
        /// <value>The selected style template.</value>
        private string StyleTemplate
        {
            get
            {
                return this.Settings["StyleTemplate"] as string;
            }
        }

        /// <summary>
        /// Gets an array of <see cref="ListItem"/>s for the common <see cref="DisplayType"/> options
        /// </summary>
        private ListItem[] DisplayTypeItems
        {
            get
            {
                if (this.displayTypeItems == null)
                {
                    this.displayTypeItems = new ListItem[]
                                        {
                                                new ListItem(
                                                    Localization.GetString(DisplayType.None.ToString(), this.LocalResourceFile),
                                                    DisplayType.None.ToString()),
                                                new ListItem(
                                                    Localization.GetString(DisplayType.Content.ToString(), this.LocalResourceFile),
                                                    DisplayType.Content.ToString()),
                                                new ListItem(
                                                    Localization.GetString(DisplayType.Link.ToString(), this.LocalResourceFile),
                                                    DisplayType.Link.ToString())
                                        };
                }

                return this.displayTypeItems;
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.Init"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += this.Page_Load;
            this.ApplyTemplateButton.Click += this.ApplyTemplateButton_Click;
            this.CancelButton.Click += this.CancelButton_Click;
            this.SubmitButton.Click += this.SubmitButton_Click;
            this.PauseOnMouseOverCheckBox.CheckedChanged += this.PauseOnMouseOverCheckBox_CheckedChanged;
            this.AutoStopCheckBox.CheckedChanged += this.AutoStopCheckBox_CheckedChanged;
            this.InitialDelayCheckBox.CheckedChanged += this.InitialDelayCheckBox_CheckedChanged;
            this.ManuallyTriggeredTransitionSpeedCheckBox.CheckedChanged += this.ManuallyTriggeredTransitionSpeedCheckBox_CheckedChanged;
            this.UseAnimationsCheckBox.CheckedChanged += this.UseAnimationsCheckBox_CheckedChanged;
            this.TemplatesDropDownList.SelectedIndexChanged += this.TemplatesDropDownList_SelectedIndexChanged;
            this.ContentDisplayRadioButtonList.SelectedIndexChanged += this.ContentDisplayRadioButtonList_SelectedIndexChanged;
            this.PositionThumbnailDisplayRadioButtonList.SelectedIndexChanged += this.PositionThumbnailDisplayRadioButtonList_SelectedIndexChanged;
            this.ThumbnailDisplayRadioButtonList.SelectedIndexChanged += this.ThumbnailDisplayRadioButtonList_SelectedIndexChanged;
        }

        /// <summary>
        /// Gets the text representation of a <see cref="Nullable{T}"/> <see cref="int"/> <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A culture-aware representation of the given <paramref name="value"/></returns>
        private static string GetValueText(int? value)
        {
            return value.HasValue
                           ? value.Value.ToString(CultureInfo.CurrentCulture)
                           : string.Empty;
        }

        /// <summary>
        /// Adds the <see cref="DisabledTextBoxCssClass"/> to the given 
        /// <see cref="textbox"/> if it is not <see cref="TextBox.Enabled"/>; otherwise
        /// removes the <see cref="DisabledTextBoxCssClass"/>.
        /// </summary>
        /// <param name="textbox">The textbox on which to set the CSS class.</param>
        private static void SetDisabledCssClass(TextBox textbox)
        {
            textbox.CssClass = !textbox.Enabled
                                       ? Engage.Utility.AddCssClass(textbox.CssClass, DisabledTextBoxCssClass)
                                       : Engage.Utility.RemoveCssClass(textbox.CssClass, DisabledTextBoxCssClass);
        }

        /// <summary>
        /// Converts <paramref name="valueText"/> from <see cref="CultureInfo.CurrentCulture"/> to <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        /// <param name="valueText">The text representing a <see cref="decimal"/> value in the <see cref="CultureInfo.CurrentCulture"/>.</param>
        /// <returns><paramref name="valueText"/> represented in the <see cref="CultureInfo.InvariantCulture"/></returns>
        private static string ConvertCurrentCultureDecimalToInvariantCulture(string valueText)
        {
            decimal value;
            if (!decimal.TryParse(valueText, NumberStyles.Number, CultureInfo.CurrentCulture, out value))
            {
                value = default(decimal);
            }

            return value.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts <paramref name="valueText"/> from <see cref="CultureInfo.CurrentCulture"/> to <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        /// <param name="valueText">The text representing an <see cref="int"/> value in the <see cref="CultureInfo.CurrentCulture"/>.</param>
        /// <returns><paramref name="valueText"/> represented in the <see cref="CultureInfo.InvariantCulture"/></returns>
        private static string ConvertCurrentCultureIntegerToInvariantCulture(string valueText)
        {
            return ConvertCurrentCultureIntegerToInvariantCulture(valueText, default(int));
        }

        /// <summary>
        /// Converts <paramref name="valueText"/> from <see cref="CultureInfo.CurrentCulture"/> to <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        /// <param name="valueText">The text representing an <see cref="int"/> value in the <see cref="CultureInfo.CurrentCulture"/>.</param>
        /// <param name="defaultValue">The value to use if <paramref cref="valueText"/> is not an <see cref="int"/> value.</param>
        /// <returns>
        /// <paramref name="valueText"/> represented in the <see cref="CultureInfo.InvariantCulture"/>
        /// </returns>
        private static string ConvertCurrentCultureIntegerToInvariantCulture(string valueText, int? defaultValue)
        {
            int value;
            if (!int.TryParse(valueText, NumberStyles.Integer, CultureInfo.CurrentCulture, out value))
            {
                if (!defaultValue.HasValue)
                {
                    return string.Empty;
                }

                value = defaultValue.Value;
            }

            return value.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    this.FillDisplayTypeListControl(this.PositionTitleDisplayRadioButtonList, true);
                    this.FillDisplayTypeListControl(this.PositionThumbnailDisplayRadioButtonList, true);
                    this.FillDisplayTypeListControl(this.ContentTitleDisplayRadioButtonList, false);
                    this.FillDisplayTypeListControl(this.ContentDisplayRadioButtonList, false);
                    this.FillDisplayTypeListControl(this.ThumbnailDisplayRadioButtonList, false);

                    this.FillTemplatesListControl();

                    this.RotatorWidthTextBox.Text = GetValueText(this.RotatorWidth);
                    this.RotatorHeightTextBox.Text = GetValueText(this.RotatorHeight);

                    this.PositionTitleDisplayRadioButtonList.SelectedValue = this.PositionTitleDisplayMode.ToString();
                    this.PositionThumbnailDisplayRadioButtonList.SelectedValue = this.PositionThumbnailDisplayMode.ToString();
                    this.ContentTitleDisplayRadioButtonList.SelectedValue = this.ContentTitleDisplayMode.ToString();

                    this.ContentDisplayRadioButtonList.SelectedValue = this.ContentDisplayMode.ToString();
                    this.ContentWidthTextBox.Text = GetValueText(this.ContentWidth);
                    this.ContentHeightTextBox.Text = GetValueText(this.ContentHeight);
                    this.ProcessContentVisibility();

                    this.ThumbnailDisplayRadioButtonList.SelectedValue = this.ThumbnailDisplayMode.ToString();
                    this.ThumbnailWidthTextBox.Text = GetValueText(this.ThumbnailWidth);
                    this.ThumbnailHeightTextBox.Text = GetValueText(this.ThumbnailHeight);
                    this.ProcessThumbnailVisibility();

                    this.PositionThumbnailWidthTextBox.Text = GetValueText(this.PositionThumbnailWidth);
                    this.PositionThumbnailHeightTextBox.Text = GetValueText(this.PositionThumbnailHeight);
                    this.ProcessPositionThumbnailVisibility();

                    this.PauseOnMouseOverCheckBox.Checked = this.PauseOnMouseOver;
                    this.RotatorDelayTextBox.Text = this.RotatorDelay.ToString(CultureInfo.CurrentCulture);
                    this.RotatorPauseDelayTextBox.Text = this.RotatorPauseDelay.ToString(CultureInfo.CurrentCulture);
                    this.ProcessMouseOverVisibility();

                    this.AutoStopCheckBox.Checked = this.AutoStop;
                    this.AutoStopCountTextBox.Text = this.AutoStopCount.ToString(CultureInfo.CurrentCulture);
                    this.ProcessAutoStopVisibility();

                    this.UseAnimationsCheckBox.Checked = this.UseAnimations;
                    this.AnimationDurationTextBox.Text = this.AnimationDuration.ToString(CultureInfo.CurrentCulture);
                    this.PauseOnMouseOverCheckBox.Checked = this.PauseOnMouseOver;
                    this.ProcessAnimationsVisiblity();

                    this.ContainerResizeCheckBox.Checked = this.ContainerResize;
                    this.ContinuousCheckBox.Checked = this.Continuous;
                    this.LoopCheckBox.Checked = this.Loop;

                    this.InitialDelayTextBox.Text = this.InitialDelay.ToString(CultureInfo.CurrentCulture);
                    this.InitialDelayCheckBox.Checked = this.InitialDelay != 0;
                    this.ProcessInitialDelayVisibility();

                    this.ManuallyTriggeredTransitionSpeedTextBox.Text = this.ManuallyTriggeredTransitionSpeed.ToString(CultureInfo.CurrentCulture);
                    this.ManuallyTriggeredTransitionSpeedCheckBox.Checked = this.ManuallyTriggeredTransitionSpeed != 0;
                    this.ProcessManuallyTriggeredTransitionSpeedVisibility();

                    this.TemplatesDropDownList.SelectedValue = this.TemplatesDropDownList.Attributes["OriginalStyleTemplate"] = this.StyleTemplate;
                    this.FillTemplateTab();
                }

                this.RegisterTabsContainer();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Handles the Click event of the ApplyTemplateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ApplyTemplateButton_Click(object sender, EventArgs e)
        {
            try
            {
                ModuleController modules = new ModuleController();
                modules.UpdateTabModuleSetting(this.TabModuleId, "StyleTemplate", this.TemplatesDropDownList.SelectedValue);

                try
                {
                    TemplateManifest manifest = TemplateManifest.CreateTemplateManifest(this.TemplatesDropDownList.SelectedValue);
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

        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Response.Redirect(Globals.NavigateURL(this.TabId), false);
        }

        /// <summary>
        /// Handles the Click event of the SubmitButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                ModuleController modules = new ModuleController();
                modules.UpdateTabModuleSetting(this.TabModuleId, "RotatorWidth", ConvertCurrentCultureIntegerToInvariantCulture(this.RotatorWidthTextBox.Text, null));
                modules.UpdateTabModuleSetting(this.TabModuleId, "RotatorHeight", ConvertCurrentCultureIntegerToInvariantCulture(this.RotatorHeightTextBox.Text, null));
                modules.UpdateTabModuleSetting(this.TabModuleId, "ContentWidth", ConvertCurrentCultureIntegerToInvariantCulture(this.ContentWidthTextBox.Text, null));
                modules.UpdateTabModuleSetting(this.TabModuleId, "ContentHeight", ConvertCurrentCultureIntegerToInvariantCulture(this.ContentHeightTextBox.Text, null));
                modules.UpdateTabModuleSetting(this.TabModuleId, "ThumbnailWidth", ConvertCurrentCultureIntegerToInvariantCulture(this.ThumbnailWidthTextBox.Text, null));
                modules.UpdateTabModuleSetting(this.TabModuleId, "ThumbnailHeight", ConvertCurrentCultureIntegerToInvariantCulture(this.ThumbnailHeightTextBox.Text, null));
                modules.UpdateTabModuleSetting(this.TabModuleId, "PositionThumbnailWidth", ConvertCurrentCultureIntegerToInvariantCulture(this.PositionThumbnailWidthTextBox.Text, null));
                modules.UpdateTabModuleSetting(this.TabModuleId, "PositionThumbnailHeight", ConvertCurrentCultureIntegerToInvariantCulture(this.PositionThumbnailHeightTextBox.Text, null));

                modules.UpdateTabModuleSetting(this.TabModuleId, "RotatorDelay", ConvertCurrentCultureIntegerToInvariantCulture(this.RotatorDelayTextBox.Text));
                modules.UpdateTabModuleSetting(this.TabModuleId, "RotatorPauseDelay", ConvertCurrentCultureIntegerToInvariantCulture(this.RotatorPauseDelayTextBox.Text));
                modules.UpdateTabModuleSetting(this.TabModuleId, "AnimationDuration", ConvertCurrentCultureDecimalToInvariantCulture(this.AnimationDurationTextBox.Text));

                modules.UpdateTabModuleSetting(this.TabModuleId, "ControlsTitleDisplayMode", this.PositionTitleDisplayRadioButtonList.SelectedValue);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ContentTitleDisplayMode", this.ContentTitleDisplayRadioButtonList.SelectedValue);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ContentDisplayMode", this.ContentDisplayRadioButtonList.SelectedValue);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ThumbnailDisplayMode", this.ThumbnailDisplayRadioButtonList.SelectedValue);

                modules.UpdateTabModuleSetting(this.TabModuleId, "AutoStop", this.AutoStopCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(this.TabModuleId, "AutoStopCount", this.AutoStopCountTextBox.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "PositionThumbnailDisplayMode", this.PositionThumbnailDisplayRadioButtonList.SelectedValue);
                modules.UpdateTabModuleSetting(this.TabModuleId, "UseAnimations", this.UseAnimationsCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(this.TabModuleId, "AnimationPauseOnMouseOver", this.PauseOnMouseOverCheckBox.Checked.ToString(CultureInfo.InvariantCulture));

                modules.UpdateTabModuleSetting(this.TabModuleId, "ContainerResize", this.ContainerResizeCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(this.TabModuleId, "Continuous", this.ContinuousCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(this.TabModuleId, "Loop", this.LoopCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(this.TabModuleId, "InitialDelay", this.InitialDelayCheckBox.Checked ? ConvertCurrentCultureDecimalToInvariantCulture(this.InitialDelayTextBox.Text) : 0m.ToString(CultureInfo.InvariantCulture));
                this.Response.Redirect(Globals.NavigateURL(this.TabId), false);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the PauseOnMouseOverCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PauseOnMouseOverCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.ProcessMouseOverVisibility();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the AutoStopCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AutoStopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.ProcessAutoStopVisibility();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the InitialDelayCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void InitialDelayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.ProcessInitialDelayVisibility();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the ManuallyTriggeredTransitionSpeedCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ManuallyTriggeredTransitionSpeedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.ProcessManuallyTriggeredTransitionSpeedVisibility();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the UseAnimationsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UseAnimationsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.ProcessAnimationsVisiblity();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the TemplatesDropDownList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TemplatesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.TemplatesDropDownList.Attributes["OriginalStyleTemplate"] != this.TemplatesDropDownList.SelectedValue)
            {
                ClientAPI.AddButtonConfirm(this.SubmitButton, Localization.GetString("TemplateChangedConfirm", this.LocalResourceFile));
            }

            this.FillTemplateTab();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ContentDisplayRadioButtonList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ContentDisplayRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ProcessContentVisibility();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the PositionThumbnailDisplayRadioButtonList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PositionThumbnailDisplayRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ProcessPositionThumbnailVisibility();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ThumbnailDisplayRadioButtonList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ThumbnailDisplayRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ProcessThumbnailVisibility();
        }

        /// <summary>
        /// Fills the given <paramref name="list"/> with the <see cref="DisplayType"/> options
        /// </summary>
        /// <param name="list">The list to fill.</param>
        /// <param name="showRotateContentOption">if set to <c>true</c> adds the <see cref="DisplayType.RotateContent"/> option.</param>
        private void FillDisplayTypeListControl(ListControl list, bool showRotateContentOption)
        {
            list.Items.Clear();
            list.Items.AddRange(this.DisplayTypeItems);

            if (showRotateContentOption)
            {
                list.Items.Add(new ListItem(
                    Localization.GetString(DisplayType.RotateContent.ToString(), this.LocalResourceFile),
                    DisplayType.RotateContent.ToString()));
            }
        }

        /// <summary>
        /// Fills <see cref="TemplatesDropDownList"/>.
        /// </summary>
        private void FillTemplatesListControl()
        {
            this.TemplatesDropDownList.Items.Clear();
            this.TemplatesDropDownList.Items.Add(new ListItem(Localization.GetString("None", this.LocalResourceFile), string.Empty));

            string templatesDirectory = HostingEnvironment.MapPath(Utility.DesktopModuleVirtualPath + Utility.StyleTemplatesFolderName);
            if (!string.IsNullOrEmpty(templatesDirectory))
            {
                foreach (string directory in Directory.GetDirectories(templatesDirectory))
                {
                    this.TemplatesDropDownList.Items.Add(new ListItem(directory.Substring(directory.LastIndexOf(Path.DirectorySeparatorChar) + 1)));
                }
            }
        }

        /// <summary>
        /// Displays information about the selected template
        /// </summary>
        private void FillTemplateTab()
        {
            try
            {
                this.ApplyTemplateButton.Enabled = true;
                TemplateManifest manifest = TemplateManifest.CreateTemplateManifest(this.TemplatesDropDownList.SelectedValue);
                this.TemplateDescriptionLabel.Text = manifest.Description;
                this.TemplateDescriptionPanel.Visible = Engage.Utility.HasValue(this.TemplateDescriptionLabel.Text);
                string templateFolder = Utility.DesktopModuleVirtualPath + Utility.StyleTemplatesFolderName + this.TemplatesDropDownList.SelectedValue;
                this.TemplatePreviewImage.ImageUrl = templateFolder + "/" + manifest.PreviewImageFilename;
                this.TemplatePreviewImage.Visible = File.Exists(HostingEnvironment.MapPath(this.TemplatePreviewImage.ImageUrl));
            }
            catch (XmlSchemaValidationException)
            {
                this.ShowManifestValidationErrorMessage();
            }
        }

        /// <summary>
        /// Hides and shows controls based on whether the <see cref="AutoStop"/> setting is selected
        /// </summary>
        private void ProcessAutoStopVisibility()
        {
            this.AutoStopCountTextBox.Enabled =
                    this.AutoStopCountIntegerValidator.Enabled =
                    this.AutoStopCountRequiredValidator.Enabled = this.AutoStopCheckBox.Checked;

            SetDisabledCssClass(this.AutoStopCountTextBox);
        }

        /// <summary>
        /// Hides and shows controls based on whether the <see cref="InitialDelay"/> setting is selected
        /// </summary>
        private void ProcessInitialDelayVisibility()
        {
            this.InitialDelayTextBox.Enabled =
                    this.InitialDelayDecimalValidator.Enabled =
                    this.InitialDelayRequiredValidator.Enabled = this.InitialDelayCheckBox.Checked;

            SetDisabledCssClass(this.InitialDelayTextBox);
        }

        /// <summary>
        /// Hides and shows controls based on whether the <see cref="ManuallyTriggeredTransitionSpeed"/> setting is selected
        /// </summary>
        private void ProcessManuallyTriggeredTransitionSpeedVisibility()
        {
            this.ManuallyTriggeredTransitionSpeedTextBox.Enabled =
                    this.ManuallyTriggeredTransitionSpeedDecimalValidator.Enabled =
                    this.ManuallyTriggeredTransitionSpeedRequiredValidator.Enabled = this.ManuallyTriggeredTransitionSpeedCheckBox.Checked;

            SetDisabledCssClass(this.ManuallyTriggeredTransitionSpeedTextBox);
        }

        /// <summary>
        /// Hides and shows controls based on whether the <see cref="UseAnimations"/> setting is selected
        /// </summary>
        private void ProcessAnimationsVisiblity()
        {
            this.AnimationDurationTextBox.Enabled =
                    this.AnimationDurationIntegerValidator.Enabled =
                    this.AnimationDurationRequiredValidator.Enabled = this.UseAnimationsCheckBox.Checked;

            SetDisabledCssClass(this.AnimationDurationTextBox);
        }

        /// <summary>
        /// Hides and shows controls based on the selected display type for the main content
        /// </summary>
        private void ProcessContentVisibility()
        {
            this.ContentHeightTextBox.Enabled =
                    this.ContentHeightIntegerValidator.Enabled =
                    this.ContentWidthTextBox.Enabled =
                    this.ContentWidthIntegerValidator.Enabled =
                    this.ContentDisplayRadioButtonList.SelectedValue != DisplayType.None.ToString();

            SetDisabledCssClass(this.ContentHeightTextBox);
            SetDisabledCssClass(this.ContentWidthTextBox);
        }

        /// <summary>
        /// Hides and shows controls based on whether the <see cref="PauseOnMouseOver"/> setting is selected
        /// </summary>
        private void ProcessMouseOverVisibility()
        {
            this.RotatorPauseDelayTextBox.Enabled =
                    this.RotatorPauseDelayIntegerValidator.Enabled =
                    this.RotatorPauseDelayRequiredValidator.Enabled = this.PauseOnMouseOverCheckBox.Checked;

            SetDisabledCssClass(this.RotatorPauseDelayTextBox);
        }

        /// <summary>
        /// Hides and shows controls based on the selected display type for the position thumbnail
        /// </summary>
        private void ProcessPositionThumbnailVisibility()
        {
            DisplayType positionThumbnailDisplayMode = (DisplayType)Enum.Parse(typeof(DisplayType), this.PositionThumbnailDisplayRadioButtonList.SelectedValue, true);
            this.PositionThumbnailHeightTextBox.Enabled =
                    this.PositionThumbnailHeightIntegerValidator.Enabled =
                    this.PositionThumbnailWidthTextBox.Enabled =
                    this.PositionThumbnailWidthIntegerValidator.Enabled = positionThumbnailDisplayMode != DisplayType.None;

            SetDisabledCssClass(this.PositionThumbnailHeightTextBox);
            SetDisabledCssClass(this.PositionThumbnailWidthTextBox);
        }

        /// <summary>
        /// Hides and shows controls based on the selected display type for the thumbnail
        /// </summary>
        private void ProcessThumbnailVisibility()
        {
            this.ThumbnailHeightTextBox.Enabled = this.ThumbnailDisplayRadioButtonList.SelectedValue != DisplayType.None.ToString();
            this.ThumbnailHeightIntegerValidator.Enabled = this.ThumbnailHeightTextBox.Enabled;

            this.ThumbnailWidthTextBox.Enabled = this.ThumbnailDisplayRadioButtonList.SelectedValue != DisplayType.None.ToString();
            this.ThumbnailWidthIntegerValidator.Enabled = this.ThumbnailWidthTextBox.Enabled;

            SetDisabledCssClass(this.ThumbnailHeightTextBox);
            SetDisabledCssClass(this.ThumbnailWidthTextBox);
        }

        /// <summary>
        /// Displays the error message that the selected template's manifest does not pass validation
        /// </summary>
        private void ShowManifestValidationErrorMessage()
        {
            this.ManifestValidationErrorsPanel.Controls.Add(new LiteralControl("<ul><li>" + Localization.GetString("ManifestValidation", this.LocalResourceFile) + "</li></ul>"));
            this.TemplateDescriptionPanel.Visible = false;
            this.TemplatePreviewImage.Visible = false;
            this.ApplyTemplateButton.Enabled = false;
        }

        /// <summary>
        /// Registers the JavaScript to create the tabs container.
        /// </summary>
        private void RegisterTabsContainer()
        {
            this.AddJQueryReference();

#if DEBUG
            this.Page.ClientScript.RegisterClientScriptResource(typeof(RotatorEdit), "Engage.Dnn.ContentRotator.JavaScript.jquery-ui-1.5.3.js");
#else
            this.Page.ClientScript.RegisterClientScriptResource(typeof(RotatorEdit), "Engage.Dnn.ContentRotator.JavaScript.jquery-ui-1.5.3.min.js");
#endif
        }
    }
}