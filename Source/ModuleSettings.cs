// <copyright file="ModuleSettings.cs" company="Engage Software">
// Engage: Rotator
// Copyright (c) 2004-2014
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

namespace Engage.Dnn.ContentRotator
{
    using System.Diagnostics.CodeAnalysis;

    using Engage.Dnn.Framework;

    /// <summary>
    /// Contains the settings for this module
    /// </summary>
    public static class ModuleSettings
    {
        /// <summary>
        /// The number of seconds that the transition animation takes
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<decimal> AnimationDuration = new Setting<decimal>("AnimationDuration", SettingScope.TabModule, 0.3m);

        /// <summary>
        /// The transition effect or effects to use
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<Effects> AnimationEffect = new Setting<Effects>("AnimationEffect", SettingScope.TabModule, Effects.fade);

        /// <summary>
        /// Whether to stop rotation after a certain number of transitions (specified in <see cref="AutoStopCount"/>)
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<bool> AutoStop = new Setting<bool>("AutoStop", SettingScope.TabModule, false);

        /// <summary>
        /// The number of transitions after which to stop rotation is <see cref="AutoStop"/> is <c>true</c>
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<int> AutoStopCount = new Setting<int>("AutoStopCount", SettingScope.TabModule, 100);

        /// <summary>
        /// Whether to automatically resize the container to fit the largest <see cref="Slide"/>
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<bool> ContainerResize = new Setting<bool>("ContainerResize", SettingScope.TabModule, true);

        /// <summary>
        /// Whether to start the next transition immediately after the current one completes
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<bool> Continuous = new Setting<bool>("Continuous", SettingScope.TabModule, false);

        /// <summary>
        /// The additional delay (in seconds) for the first transition (hint: can be negative)
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<decimal> InitialDelay = new Setting<decimal>("InitialDelay", SettingScope.TabModule, 0);

        /// <summary>
        /// The delay (in seconds) for transitions triggered manually (through the pager or previous/next button)
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<decimal> ManuallyTriggeredTransitionSpeed = new Setting<decimal>("ManuallyTriggeredTransitionSpeed", SettingScope.TabModule, 0);

        /// <summary>
        /// Whether to loop rotation, or just display each slide once
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<bool> Loop = new Setting<bool>("Loop", SettingScope.TabModule, true);

        /// <summary>
        /// Whether to display slides in a random order
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<bool> RandomOrder = new Setting<bool>("RandomOrder", SettingScope.TabModule, false);

        /// <summary>
        /// Whether to pause rotation when the slides are hovered over
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<bool> PauseOnHover = new Setting<bool>("AnimationPauseOnMouseOver", SettingScope.TabModule, true);

        /// <summary>
        /// The number of seconds each slide is shown before the next slide appears
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<decimal> RotatorDelay = new Setting<decimal>("RotatorDelay", SettingScope.TabModule, 8m);

        /// <summary>
        /// The height in pixels for the slide container, or <c>null</c> to not set a height
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Don't be silly, nesting nullable types is not big deal")]
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<int?> SlideHeight = new Setting<int?>("ContentHeight", SettingScope.TabModule, null);

        /// <summary>
        /// The width in pixels for the slide container, or <c>null</c> to not set a height
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Don't be silly, nesting nullable types is not big deal")]
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<int?> SlideWidth = new Setting<int?>("ContentWidth", SettingScope.TabModule, null);

        /// <summary>
        /// Whether in and out transitions occur simultaneously
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<bool> SimultaneousTransitions = new Setting<bool>("SimultaneousTransitions", SettingScope.TabModule, true);

        /// <summary>
        /// Whether to force all slides to fit exactly within the container
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<bool> ForceSlidesToFitContainer = new Setting<bool>("ForceSlidesToFitContainer", SettingScope.TabModule, false);

        /// <summary>
        /// Whether to use animations to transition between slides
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<bool> UseAnimations = new Setting<bool>("UseAnimations", SettingScope.TabModule, true);

        /// <summary>
        /// Whether to disable the ClearType fix that adds a background color to each slide
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<bool> DisableAddingBackgroundColorForClearTypeFix = new Setting<bool>("DisableAddingBackgroundColorForClearTypeFix", SettingScope.TabModule, false);

        /// <summary>
        /// Whether to randomize the order of the transition effects when <see cref="AnimationEffect"/> is set to multiple effects
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<bool> RandomizeEffects = new Setting<bool>("RandomizeEffects", SettingScope.TabModule, true);

        /// <summary>
        /// Whether a manual transition trumps an active transition
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<bool> ManualTransitionTrumpsActiveTransition = new Setting<bool>("ManualTransitionTrumpsActiveTransition", SettingScope.TabModule, true);

        /// <summary>
        /// The name of the folder in which the template lives to use for displaying the slides
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<string> TemplateFolderName = new Setting<string>("Template", SettingScope.TabModule, null);
    }
}