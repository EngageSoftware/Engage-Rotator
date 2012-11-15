// <copyright file="CycleOptions.cs" company="Engage Software">
// Engage: Rotator - http://www.engagemodules.com
// Copyright (c) 2004-2010
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
    using System.Web.Script.Serialization;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// The options and settings to send to the Cycle plugin
    /// </summary>
    public class CycleOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CycleOptions"/> class.
        /// </summary>
        /// <param name="autoStop">if set to <c>true</c> rotation automatically stops after <paramref name="autoStopCount"/> transitions.</param>
        /// <param name="autoStopCount">The number of transitions after which rotation stops if <paramref name="autoStop"/> is <c>true</c>.</param>
        /// <param name="containerResize">if set to <c>true</c> automatically resize the container to fit the largest <see cref="Slide"/>.</param>
        /// <param name="containerHeight">Height of the container.</param>
        /// <param name="containerWidth">Width of the container.</param>
        /// <param name="continuous">if set to <c>true</c> start the next transition immediately after the current one completes.</param>
        /// <param name="initialDelay">The additional delay (in milliseconds) for the first transition (hint: can be negative).</param>
        /// <param name="millisecondsBetweenTransitions">The milliseconds between transitions.</param>
        /// <param name="pauseOnHover">if set to <c>true</c> pause rotation when the mouse is over the slide area.</param>
        /// <param name="transitionEffects">The transition effects.</param>
        /// <param name="transitionSpeed">The transition speed in milliseconds.</param>
        /// <param name="manuallyTriggeredTransitionSpeed">The delay (in milliseconds) for transitions triggered manually (through the pager or previous/next button)</param>
        /// <param name="loop">if set to <c>true</c> allow slideshow to loop, i.e. start again after going once through the <see cref="Slide"/>s.</param>
        /// <param name="randomOrder">if set to <c>true</c> display slides in a random order.</param>
        /// <param name="simultaneousTransitions">if set to <c>true</c> transition the current slide out at the same time as the next item transitions in.</param>
        /// <param name="forceSlidesToFitContainer">if set to <c>true</c> force slides to fit within container.</param>
        /// <param name="disableAddingBackgroundColorForClearTypeFix">if set to <c>true</c> do not apply the ClearType fix which adds a background color to each slide.</param>
        /// <param name="randomizeEffects">if set to <c>true</c> randomize the order of transition effects.</param>
        /// <param name="manualTransitionTrumpsActiveTransition">if set to <c>true</c> a manual transition trumps an active transition, rather than being ignored during an active transition.</param>
        /// <param name="containerElement">The <see cref="Control"/> wrapping the rotator.</param>
        /// <exception cref="ArgumentNullException"><paramref name="autoStopCount"/> must not be <c>null</c> if <paramref name="autoStop"/> is <c>true</c></exception>
        public CycleOptions(bool autoStop, int? autoStopCount, bool containerResize, Unit containerHeight, Unit containerWidth, bool continuous, int initialDelay, int millisecondsBetweenTransitions, bool pauseOnHover, Effects transitionEffects, int transitionSpeed, int manuallyTriggeredTransitionSpeed, bool loop, bool randomOrder, bool simultaneousTransitions, bool forceSlidesToFitContainer, bool disableAddingBackgroundColorForClearTypeFix, bool randomizeEffects, bool manualTransitionTrumpsActiveTransition, Control containerElement)
        {
            this.PagerEvent = null;

            if (autoStop && !autoStopCount.HasValue)
            {
                throw new ArgumentNullException("autoStopCount", "autoStopCount must not be null if autoStop is true");
            }

            this.AutoStop = autoStop;
            this.AutoStopCount = autoStop ? autoStopCount.Value : 0;
            this.ContainerResize = containerResize;
            this.ContainerHeight = containerHeight;
            this.ContainerWidth = containerWidth;
            this.Continuous = continuous;
            this.InitialDelay = initialDelay;
            this.MillisecondsBetweenTransitions = millisecondsBetweenTransitions;
            this.PauseOnHover = pauseOnHover;
            this.TransitionEffects = transitionEffects;
            this.TransitionSpeed = transitionSpeed;
            this.ManuallyTriggeredTransitionSpeed = manuallyTriggeredTransitionSpeed;
            this.Loop = loop;
            this.RandomOrder = randomOrder;
            this.SimultaneousTransitions = simultaneousTransitions;
            this.ForceSlidesToFitContainer = forceSlidesToFitContainer;
            this.DisableAddingBackgroundColorForClearTypeFix = disableAddingBackgroundColorForClearTypeFix;
            this.RandomizeEffects = randomizeEffects;
            this.ManualTransitionTrumpsActiveTransition = manualTransitionTrumpsActiveTransition;
            this.ContainerElement = containerElement;
        }

        /// <summary>Gets or sets a value indicating whether to end slideshow after <see cref="AutoStopCount"/> transitions</summary>
        public bool AutoStop
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating the number of transitions to display before stopping when <see cref="AutoStop"/> is <c>true</c></summary>
        public int AutoStopCount
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating the element wrapping the rotator</summary>
        public Control ContainerElement
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating container height, defaults to "auto"</summary>
        public Unit ContainerHeight
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating container width, defaults to <see cref="Unit.Empty"/></summary>
        public Unit ContainerWidth
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating whether to resize the container to fit largest slide</summary>
        public bool ContainerResize
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating whether to start the next transition immediately after the current one completes</summary>
        public bool Continuous
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating the additional delay (in milliseconds) for the first transition (hint: can be negative)</summary>
        public decimal InitialDelay
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating the time in milliseconds for transitions triggered manually (via <c>PagerContainer</c> or <c>PreviousButton</c>/<c>NextButton</c>)</summary>
        public int ManuallyTriggeredTransitionSpeed
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating the time in milliseconds between slide transitions (0 to disable auto advance)</summary>
        public int MillisecondsBetweenTransitions
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating whether to allow slideshow to loop, i.e. start again after going once through the <see cref="Slide"/>s</summary>
        public bool Loop
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating the name of event which drives the pager navigation</summary>
        public string PagerEvent
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating whether to enable "pause on hover"</summary>
        public bool PauseOnHover
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating whether to pause when hovering over pager link</summary>
        public bool PauseOnPagerHover
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating whether slides are shown in random order or in sequence (not applicable to <see cref="Effects.shuffle"/>)</summary>
        public bool RandomOrder
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating whether in/out transitions should occur simultaneously</summary>
        public bool SimultaneousTransitions
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating whether to force slides to fit within the container</summary>
        public bool ForceSlidesToFitContainer
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating the zero-based index of the first slide to be displayed</summary>
        public int StartingSlideIndex
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating the transition effect or effects</summary>
        public Effects TransitionEffects
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating the speed of the transition in milliseconds</summary>
        public int TransitionSpeed
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating whether to disable extra clear-type fixing (leave <c>false</c> to force background color setting on slides)</summary>
        public bool DisableAddingBackgroundColorForClearTypeFix
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating whether to make the effect sequence random.  Valid when multiple effects are used</summary>
        public bool RandomizeEffects
        {
            get;
            set;
        }

        /// <summary>Gets or sets a value indicating whether to cause a manual transition to stop an active transition instead of being ignored.</summary>
        public bool ManualTransitionTrumpsActiveTransition
        {
            get;
            set;
        }

        /// <summary>
        /// Converts this instance into a JSON string.
        /// </summary>
        /// <returns>The serialized JSON string</returns>
        public string Serialize()
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new CycleOptionsConverter() });
            return serializer.Serialize(this);
        }
    }
}