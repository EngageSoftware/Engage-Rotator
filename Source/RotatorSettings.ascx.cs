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
    using System.Diagnostics;
    using System.Globalization;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Exceptions;
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
        /// Backing field for <see cref="AnimationEffect"/>
        /// </summary>
        private Effects animationEffect = Effects.None;

        /// <summary>
        /// Gets the duration of the transition animation.
        /// </summary>
        /// <value>The duration of the animation (in seconds).</value>
        private decimal AnimationDuration
        {
            get
            {
                return Utility.GetDecimalSetting(this.Settings, "AnimationDuration", 0.3m);
            }
        }

        /// <summary>
        /// Gets the transition effect or effects to use.
        /// </summary>
        /// <value>The animation effect or effects to use for transitions.</value>
        private Effects AnimationEffect
        {
            get
            {
                if (this.animationEffect == Effects.None)
                {
                    this.animationEffect = Utility.GetEnumSetting(this.Settings, "AnimationEffect", Effects.fade);
                }

                return this.animationEffect;
            }
        }

        /// <summary>
        /// Gets a value indicating whether to automatically resize the container to fit the largest <see cref="Slide"/>.
        /// </summary>
        /// <value><c>true</c> if the option to automatically resize the container to fit the largest <see cref="Slide"/> is set; otherwise, <c>false</c>.</value>
        private bool ContainerResize
        {
            get
            {
                return Utility.GetBoolSetting(this.Settings, "ContainerResize", true);
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
                return Utility.GetBoolSetting(this.Settings, "Continuous", false);
            }
        }

        /// <summary>
        /// Gets a value indicating whether to pause rotation when the slides are hovered over.
        /// </summary>
        /// <value><c>true</c> if the module is set to pause rotation when the slides are hovered over; otherwise, <c>false</c>.</value>
        private bool PauseOnHover
        {
            get
            {
                return Utility.GetBoolSetting(this.Settings, "AnimationPauseOnMouseOver", true);
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
                return Utility.GetBoolSetting(this.Settings, "AutoStop", false);
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
                return Utility.GetIntSetting(this.Settings, "AutoStopCount", 100);
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
                return Utility.GetDecimalSetting(this.Settings, "InitialDelay", 0);
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
                return Utility.GetDecimalSetting(this.Settings, "ManuallyTriggeredTransitionSpeed", 0);
            }
        }

        /// <summary>
        /// Gets a value indicating whether to loop rotation, or just display each slide once.
        /// </summary>
        /// <value><c>true</c> if the module is set to only show each slide once; otherwise, <c>false</c>.</value>
        private bool Loop
        {
            get
            {
                return Utility.GetBoolSetting(this.Settings, "Loop", true);
            }
        }

        /// <summary>
        /// Gets a value indicating whether to display slides in a random order.
        /// </summary>
        /// <value><c>true</c> if the module is set to display slides in a random order; otherwise, <c>false</c>.</value>
        private bool RandomOrder
        {
            get
            {
                return Utility.GetBoolSetting(this.Settings, "RandomOrder", false);
            }
        }

        /// <summary>
        /// Gets a value indicating whether in and out transitions occur simultaneously.
        /// </summary>
        /// <value><c>true</c> if the module is set to display in and out transitions simultaneously; otherwise, <c>false</c>.</value>
        private bool SimultaneousTransitions
        {
            get
            {
                return Utility.GetBoolSetting(this.Settings, "SimultaneousTransitions", true);
            }
        }

        /// <summary>
        /// Gets a value indicating whether to force slides to fit exactly within the container.
        /// </summary>
        /// <value><c>true</c> if the module is set to force slides to fit the dimensions of the container; otherwise, <c>false</c>.</value>
        private bool ForceSlidesToFitContainer
        {
            get
            {
                return Utility.GetBoolSetting(this.Settings, "ForceSlidesToFitContainer", false);
            }
        }

        /// <summary>
        /// Gets the height in pixels for the slide container, or <c>null</c> to 
        /// </summary>
        private int? SlideHeight
        {
            get
            {
                return Utility.GetIntSetting(this.Settings, "ContentHeight");
            }
        }

        /// <summary>
        /// Gets the width in pixels for the slide container
        /// </summary>
        private int? SlideWidth
        {
            get
            {
                return Utility.GetIntSetting(this.Settings, "ContentWidth");
            }
        }

        /// <summary>
        /// Gets the setting for the delay between each slide.
        /// </summary>
        /// <value>The rotator delay (in seconds).</value>
        private int RotatorDelay
        {
            get
            {
                return Utility.GetIntSetting(this.Settings, "RotatorDelay", 8);
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
                return Utility.GetBoolSetting(this.Settings, "UseAnimations", true);
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
            this.CancelButton.Click += this.CancelButton_Click;
            this.SubmitButton.Click += this.SubmitButton_Click;
            this.AutoStopCheckBox.CheckedChanged += this.AutoStopCheckBox_CheckedChanged;
            this.InitialDelayCheckBox.CheckedChanged += this.InitialDelayCheckBox_CheckedChanged;
            this.ManuallyTriggeredTransitionSpeedCheckBox.CheckedChanged += this.ManuallyTriggeredTransitionSpeedCheckBox_CheckedChanged;
            this.UseAnimationsCheckBox.CheckedChanged += this.UseAnimationsCheckBox_CheckedChanged;
            this.TransitionEffectRequiredValidator.ServerValidate += this.AnimationEffectRequiredValidator_ServerValidate;
        }

        /// <summary>
        /// Adds the <see cref="DisabledTextBoxCssClass"/> to the given 
        /// <paramref name="textBox"/> if it is not <see cref="TextBox.Enabled"/>; otherwise
        /// removes the <see cref="DisabledTextBoxCssClass"/>.
        /// </summary>
        /// <param name="textBox">The <see cref="TextBox"/> on which to set the CSS class.</param>
        private static void SetDisabledCssClass(TextBox textBox)
        {
            textBox.CssClass = !textBox.Enabled
                                       ? Engage.Utility.AddCssClass(textBox.CssClass, DisabledTextBoxCssClass)
                                       : Engage.Utility.RemoveCssClass(textBox.CssClass, DisabledTextBoxCssClass);
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
                    this.SetupAnimationEffectsListControl();

                    this.PauseOnHoverCheckBox.Checked = this.PauseOnHover;
                    this.RotatorDelayTextBox.Text = this.RotatorDelay.ToString(CultureInfo.CurrentCulture);

                    this.AutoStopCheckBox.Checked = this.AutoStop;
                    this.AutoStopCountTextBox.Text = this.AutoStopCount.ToString(CultureInfo.CurrentCulture);
                    this.ProcessAutoStopVisibility();

                    this.UseAnimationsCheckBox.Checked = this.UseAnimations;
                    this.TransitionDurationTextBox.Text = this.AnimationDuration.ToString(CultureInfo.CurrentCulture);
                    this.PauseOnHoverCheckBox.Checked = this.PauseOnHover;
                    this.ProcessAnimationsVisibility();

                    this.ContainerResizeCheckBox.Checked = this.ContainerResize;
                    this.ForceSlidesToFitContainerCheckBox.Checked = this.ForceSlidesToFitContainer;
                    this.ContinuousCheckBox.Checked = this.Continuous;
                    this.LoopCheckBox.Checked = this.Loop;
                    this.RandomOrderCheckBox.Checked = this.RandomOrder;
                    this.SimultaneousTransitionsCheckBox.Checked = this.SimultaneousTransitions;

                    this.InitialDelayTextBox.Text = this.InitialDelay.ToString(CultureInfo.CurrentCulture);
                    this.InitialDelayCheckBox.Checked = this.InitialDelay != 0;
                    this.ProcessInitialDelayVisibility();

                    this.SlideHeightTextBox.Text = this.SlideHeight.HasValue
                                                           ? this.SlideHeight.Value.ToString(CultureInfo.CurrentCulture)
                                                           : string.Empty;
                    this.SlideWidthTextBox.Text = this.SlideWidth.HasValue
                                                           ? this.SlideWidth.Value.ToString(CultureInfo.CurrentCulture)
                                                           : string.Empty;

                    this.ManuallyTriggeredTransitionSpeedTextBox.Text = this.ManuallyTriggeredTransitionSpeed.ToString(CultureInfo.CurrentCulture);
                    this.ManuallyTriggeredTransitionSpeedCheckBox.Checked = this.ManuallyTriggeredTransitionSpeed != 0;
                    this.ProcessManuallyTriggeredTransitionSpeedVisibility();
                }

                this.RegisterTabsContainer();
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

                modules.UpdateTabModuleSetting(this.TabModuleId, "RotatorDelay", ConvertCurrentCultureIntegerToInvariantCulture(this.RotatorDelayTextBox.Text));
                modules.UpdateTabModuleSetting(this.TabModuleId, "AutoStop", this.AutoStopCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(this.TabModuleId, "AutoStopCount", this.AutoStopCountTextBox.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "AnimationPauseOnMouseOver", this.PauseOnHoverCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(this.TabModuleId, "UseAnimations", this.UseAnimationsCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(this.TabModuleId, "AnimationDuration", ConvertCurrentCultureDecimalToInvariantCulture(this.TransitionDurationTextBox.Text));
                modules.UpdateTabModuleSetting(this.TabModuleId, "AnimationEffect", this.GetSelectedEffects().ToString());

                modules.UpdateTabModuleSetting(this.TabModuleId, "ContentHeight", ConvertCurrentCultureIntegerToInvariantCulture(this.SlideHeightTextBox.Text, null));
                modules.UpdateTabModuleSetting(this.TabModuleId, "ContentWidth", ConvertCurrentCultureIntegerToInvariantCulture(this.SlideWidthTextBox.Text, null));

                modules.UpdateTabModuleSetting(this.TabModuleId, "ContainerResize", this.ContainerResizeCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(this.TabModuleId, "ForceSlidesToFitContainer", this.ForceSlidesToFitContainerCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(this.TabModuleId, "Continuous", this.ContinuousCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(this.TabModuleId, "Loop", this.LoopCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(this.TabModuleId, "RandomOrder", this.RandomOrderCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(this.TabModuleId, "SimultaneousTransitions", this.SimultaneousTransitionsCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(this.TabModuleId, "InitialDelay", this.InitialDelayCheckBox.Checked ? ConvertCurrentCultureDecimalToInvariantCulture(this.InitialDelayTextBox.Text) : 0m.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(this.TabModuleId, "ManuallyTriggeredTransitionSpeed", this.ManuallyTriggeredTransitionSpeedCheckBox.Checked ? ConvertCurrentCultureDecimalToInvariantCulture(this.ManuallyTriggeredTransitionSpeedTextBox.Text) : 0m.ToString(CultureInfo.InvariantCulture));

                this.Response.Redirect(Globals.NavigateURL(this.TabId), false);
            }
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
            this.ProcessAnimationsVisibility();
        }

        /// <summary>
        /// Handles the ServerValidate event of the TransitionEffectRequiredValidator control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.</param>
        private void AnimationEffectRequiredValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = this.TransitionEffectCheckBoxList.SelectedIndex > -1;
        }

        /// <summary>
        /// Fills <see cref="TransitionEffectCheckBoxList"/> and selects the initial values
        /// </summary>
        private void SetupAnimationEffectsListControl()
        {
            this.FillAnimationEffectsListControl();
            this.SelectAnimationEffectsValues();
        }

        /// <summary>
        /// Fills <see cref="TransitionEffectCheckBoxList"/>.
        /// </summary>
        private void FillAnimationEffectsListControl()
        {
            this.TransitionEffectCheckBoxList.DataSource = Enum.GetNames(typeof(Effects));
            this.TransitionEffectCheckBoxList.DataBind();
            this.TransitionEffectCheckBoxList.Items.Remove(Effects.None.ToString());
            Utility.LocalizeListControl(this.TransitionEffectCheckBoxList, this.LocalResourceFile);
        }

        /// <summary>
        /// Selects the initial values of <see cref="TransitionEffectCheckBoxList"/>.
        /// </summary>
        private void SelectAnimationEffectsValues()
        {
            foreach (ListItem item in this.TransitionEffectCheckBoxList.Items)
            {
                Effects itemEffect = (Effects)Enum.Parse(typeof(Effects), item.Value);
                item.Selected = (this.AnimationEffect & itemEffect) != 0;
            }
        }

        /// <summary>
        /// Gets the effects selected in <see cref="TransitionEffectCheckBoxList"/>.
        /// </summary>
        /// <returns>The selected animation transition effects</returns>
        private Effects GetSelectedEffects()
        {
            Effects effects = Effects.None;
            foreach (ListItem item in this.TransitionEffectCheckBoxList.Items)
            {
                if (item.Selected)
                {
                    Effects itemEffect = (Effects)Enum.Parse(typeof(Effects), item.Value);
                    effects |= itemEffect;
                }
            }

            Debug.Assert(effects != Effects.None, "Page validation required that an effect has been selected");
            return effects;
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
        private void ProcessAnimationsVisibility()
        {
            this.TransitionDurationTextBox.Enabled =
                    this.TransitionDurationIntegerValidator.Enabled =
                    this.TransitionDurationRequiredValidator.Enabled = 
                    this.TransitionEffectCheckBoxList.Enabled = 
                    this.TransitionEffectRequiredValidator.Enabled = this.UseAnimationsCheckBox.Checked;

            SetDisabledCssClass(this.TransitionDurationTextBox);
        }

        /// <summary>
        /// Registers the JavaScript to create the tabs container.
        /// </summary>
        private void RegisterTabsContainer()
        {
            this.AddJQueryReference();

            this.Page.ClientScript.RegisterClientScriptResource(typeof(RotatorEdit), "Engage.Dnn.ContentRotator.JavaScript.jquery-ui.js");
        }
    }
}