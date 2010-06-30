// <copyright file="Rotator.ascx.cs" company="Engage Software">
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
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Modules.Actions;
    using DotNetNuke.Security;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using Framework.Templating;
    using Templating;

    /// <summary>
    /// The main control displaying rotating slides
    /// </summary>
    public partial class Rotator : ModuleBase, IActionable
    {
        /// <summary>
        /// Backing field for <see cref="CycleOptions"/>
        /// </summary>
        private CycleOptions cycleOptions;

        /// <summary>
        /// Gets ModuleActions.
        /// </summary>
        public ModuleActionCollection ModuleActions
        {
            get
            {
                return new ModuleActionCollection(new[]
                        {
                                new ModuleAction(
                                        this.GetNextActionID(),
                                        Localization.GetString("Add/Edit Slides", this.LocalResourceFile),
                                        ModuleActionType.AddContent,
                                        string.Empty,
                                        string.Empty,
                                        this.EditUrl("Options"),
                                        string.Empty,
                                        false,
                                        SecurityAccessLevel.Edit,
                                        true,
                                        false),
                                new ModuleAction(
                                        this.GetNextActionID(),
                                        Localization.GetString("Choose Template", this.LocalResourceFile),
                                        ModuleActionType.AddContent,
                                        string.Empty,
                                        string.Empty,
                                        this.EditUrl("Template"),
                                        string.Empty,
                                        false,
                                        SecurityAccessLevel.Edit,
                                        true,
                                        false),
                                new ModuleAction(
                                        this.GetNextActionID(),
                                        Localization.GetString("Rotator Settings", this.LocalResourceFile),
                                        ModuleActionType.AddContent,
                                        string.Empty,
                                        string.Empty,
                                        this.EditUrl("ModSettings"),
                                        string.Empty,
                                        false,
                                        SecurityAccessLevel.Edit,
                                        true,
                                        false)
                        });
            }
        }

        /// <summary>
        /// Gets the <see cref="CycleOptions"/> for this module instance.
        /// </summary>
        /// <returns>The <see cref="CycleOptions"/> for this module instance</returns>
        protected CycleOptions CycleOptions
        {
            get
            {
                if (this.cycleOptions == null)
                {
                    Unit containerHeight = this.SlideHeight.HasValue ? Unit.Pixel(this.SlideHeight.Value) : Unit.Empty;
                    Unit containerWidth = this.SlideWidth.HasValue ? Unit.Pixel(this.SlideWidth.Value) : Unit.Empty;
                    int transitionSpeed = this.UseAnimations ? ConvertSecondsToMilliseconds(this.AnimationDuration) : 1;

                    this.cycleOptions = new CycleOptions(
                            this.AutoStop,
                            this.AutoStopCount,
                            this.ContainerResize,
                            containerHeight,
                            containerWidth,
                            this.Continuous,
                            ConvertSecondsToMilliseconds(this.InitialDelay),
                            ConvertSecondsToMilliseconds(this.RotatorDelay) + transitionSpeed,
                            this.PauseOnHover,
                            this.AnimationEffect,
                            transitionSpeed,
                            ConvertSecondsToMilliseconds(this.ManuallyTriggeredTransitionSpeed),
                            this.Loop,
                            this.RandomOrder,
                            this.SimultaneousTransitions,
                            this.ForceSlidesToFitContainer,
                            this.DisableAddingBackgroundColorForClearTypeFix,
                            this.RandomizeEffects,
                            this.ManualTransitionTrumpsActiveTransition,
                            this.RotatorContainer);
                }

                return this.cycleOptions;
            }
        }

        /// <summary>
        /// Gets AnimationDuration.
        /// </summary>
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
                return Utility.GetEnumSetting(this.Settings, "AnimationEffect", Effects.fade);
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
        /// Gets RotatorDelay.
        /// </summary>
        private decimal RotatorDelay
        {
            get
            {
                return Utility.GetDecimalSetting(this.Settings, "RotatorDelay", 8m);
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
        /// Gets a value indicating whether to use animations to transition between slides.
        /// </summary>
        private bool UseAnimations
        {
            get
            {
                return Utility.GetBoolSetting(this.Settings, "UseAnimations", true);
            }
        }

        /// <summary>
        /// Gets a value indicating whether to disable the ClearType fix that adds a background color to each slide
        /// </summary>
        private bool DisableAddingBackgroundColorForClearTypeFix
        {
            get
            {
                return Utility.GetBoolSetting(this.Settings, "DisableAddingBackgroundColorForClearTypeFix", false);
            }
        }

        /// <summary>
        /// Gets a value indicating whether to randomize the order of the transition effects when <see cref="AnimationEffect"/> is set to multiple effects
        /// </summary>
        private bool RandomizeEffects
        {
            get
            {
                return Utility.GetBoolSetting(this.Settings, "RandomizeEffects", true);
            }
        }

        /// <summary>
        /// Gets a value indicating whether a manual transition trumps an active transition
        /// </summary>
        private bool ManualTransitionTrumpsActiveTransition
        {
            get
            {
                return Utility.GetBoolSetting(this.Settings, "ManualTransitionTrumpsActiveTransition", true);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            this.RequiresScriptManager = false;
            this.TemplateProvider = new TemplateProvider(
                    this.GetTemplateSetting(),
                    this.RotatorContainer,
                    this.ProcessTags,
                    this.GetSlides);

            base.OnInit(e);
            this.Load += this.Page_Load;
        }

        /// <summary>
        /// Converts the given number of seconds into milliseconds.
        /// </summary>
        /// <param name="seconds">The value in seconds.</param>
        /// <returns>The number of milliseconds represented by <paramref name="seconds"/></returns>
        private static int ConvertSecondsToMilliseconds(decimal seconds)
        {
            const int MillisecondsPerSecond = 1000;
            return (int)Math.Round(seconds * MillisecondsPerSecond);
        }

        /// <summary>
        /// Adds the given <paramref name="control"/> to the given <paramref name="container"/> unless <paramref name="control"/> is <c>null</c>.
        /// </summary>
        /// <param name="container">The container to which <paramref name="control"/> is to be added.</param>
        /// <param name="control">The control to add to <paramref name="container"/>.</param>
        private static void AddControl(Control container, Control control)
        {
            if (control != null)
            {
                container.Controls.Add(control);
            }
        }

        /// <summary>
        /// Creates a <see cref="Label"/> whose content is the (1-based) index of the currently displayed <see cref="Slide"/>
        /// </summary>
        /// <param name="tag">The tag whose content is being represented.</param>
        /// <param name="slide">The object from which to get the property.</param>
        /// <param name="resourceFile">The resource file from which to get localized resources.</param>
        /// <returns>
        /// The <see cref="Label"/> instance that was created
        /// </returns>
        private static Control CreateCurrentIndexControl(Tag tag, ITemplateable slide, string resourceFile)
        {
            Label currentSlideIndexWrapper = new Label();
            currentSlideIndexWrapper.CssClass = TemplateEngine.GetAttributeValue(tag, slide, resourceFile, "CssClass", "class");
            currentSlideIndexWrapper.CssClass = Engage.Utility.AddCssClass(currentSlideIndexWrapper.CssClass, "current-slide-index");
            currentSlideIndexWrapper.Text = 1.ToString(CultureInfo.CurrentCulture);
            return currentSlideIndexWrapper;
        }

        /// <summary>
        /// Creates a <see cref="Label"/> whose content is the total number of <see cref="Slide"/>s for this rotator.
        /// </summary>
        /// <param name="tag">The tag whose content is being represented.</param>
        /// <param name="slide">The object from which to get the property.</param>
        /// <param name="resourceFile">The resource file from which to get localized resources.</param>
        /// <returns>
        /// The <see cref="Label"/> instance that was created
        /// </returns>
        private static Control CreateTotalCountControl(Tag tag, ITemplateable slide, string resourceFile)
        {
            Label totalCountLabel = new Label();
            totalCountLabel.CssClass = TemplateEngine.GetAttributeValue(tag, slide, resourceFile, "CssClass", "class");
            totalCountLabel.CssClass = Engage.Utility.AddCssClass(totalCountLabel.CssClass, "total-slide-count");
            return totalCountLabel;
        }

        /// <summary>
        /// Processes template tokens not automatically processed by the <see cref="TemplateEngine"/>, i.e. tokens specific to Rotator functionality.
        /// </summary>
        /// <param name="container">The container to which content is added.</param>
        /// <param name="tag">The tag being processed.</param>
        /// <param name="slide">The slide being processed (or <c>null</c>).</param>
        /// <param name="resourceFile">The resource file to use to find localized text.</param>
        /// <returns>Whether to process the tag's ChildTags collection</returns>
        private bool ProcessTags(Control container, Tag tag, ITemplateable slide, string resourceFile)
        {
            if (tag.TagType == TagType.Open)
            {
                switch (tag.LocalName.ToUpperInvariant())
                {
                    case "ROTATEBACK":
                        AddControl(container, this.CreateBackButton(tag, slide, resourceFile));
                        break;
                    case "ROTATENEXT":
                        AddControl(container, this.CreateNextButton(tag, slide, resourceFile));
                        break;
                    case "ROTATEPAUSE":
                        AddControl(container, this.CreatePauseButton(tag, slide, resourceFile));
                        break;
                    case "ROTATEPLAY":
                        AddControl(container, this.CreatePlayButton(tag, slide, resourceFile));
                        break;
                    case "PAGER":
                        AddControl(container, this.CreatePager(tag, slide, resourceFile));
                        break;
                    case "PAGERITEM":
                        AddControl(container, this.CreatePagerItem(tag, slide, resourceFile));
                        break;
                    case "CURRENTINDEX":
                        AddControl(container, CreateCurrentIndexControl(tag, slide, resourceFile));
                        break;
                    case "TOTALCOUNT":
                        AddControl(container, CreateTotalCountControl(tag, slide, resourceFile));
                        break;
                }
            }

            return false;
        }

        /// <summary>
        /// Creates a <c>div</c> tag for the given <paramref name="tag"/>.
        /// Then sets the tag to cycle back when clicked.
        /// </summary>
        /// <param name="tag">The tag whose content is being represented.</param>
        /// <param name="slide">The object from which to get the property.</param>
        /// <param name="resourceFile">The resource file from which to get localized resources.</param>
        /// <returns>
        /// The created back button
        /// </returns>
        private Control CreateBackButton(Tag tag, ITemplateable slide, string resourceFile)
        {
            Panel backButton = this.CreateRotatorContainer(tag, slide, resourceFile);

            backButton.CssClass = "rotator-prev";

            return backButton;
        }

        /// <summary>
        /// Creates a <c>div</c> tag for the given <paramref name="tag"/>.
        /// Then sets the tag to cycle forward when clicked.
        /// </summary>
        /// <param name="tag">The tag whose content is being represented.</param>
        /// <param name="slide">The object from which to get the property.</param>
        /// <param name="resourceFile">The resource file from which to get localized resources.</param>
        /// <returns>
        /// The created next button
        /// </returns>
        private Control CreateNextButton(Tag tag, ITemplateable slide, string resourceFile)
        {
            Panel nextButton = this.CreateRotatorContainer(tag, slide, resourceFile);

            nextButton.CssClass = "rotator-next";

            return nextButton;
        }

        /// <summary>
        /// Creates a <c>div</c> tag for the given <paramref name="tag"/>.
        /// Then sets the tag to pause rotation when clicked.
        /// </summary>
        /// <param name="tag">The tag whose content is being represented.</param>
        /// <param name="slide">The object from which to get the property.</param>
        /// <param name="resourceFile">The resource file from which to get localized resources.</param>
        /// <returns>
        /// The created pause button
        /// </returns>
        private Control CreatePauseButton(Tag tag, ITemplateable slide, string resourceFile)
        {
            Panel pauseButton = this.CreateRotatorContainer(tag, slide, resourceFile);
            pauseButton.CssClass = Engage.Utility.AddCssClass(pauseButton.CssClass, "rotator-pause");
            return pauseButton;
        }

        /// <summary>
        /// Creates a <c>div</c> tag for the given <paramref name="tag"/>.
        /// Then sets the tag to resume rotation when clicked.
        /// </summary>
        /// <param name="tag">The tag whose content is being represented.</param>
        /// <param name="slide">The object from which to get the property.</param>
        /// <param name="resourceFile">The resource file from which to get localized resources.</param>
        /// <returns>
        /// The created play button
        /// </returns>
        private Control CreatePlayButton(Tag tag, ITemplateable slide, string resourceFile)
        {
            Panel playButton = this.CreateRotatorContainer(tag, slide, resourceFile);

            playButton.CssClass = Engage.Utility.AddCssClass(playButton.CssClass, "rotator-play");
            playButton.CssClass = Engage.Utility.AddCssClass(playButton.CssClass, "rotator-play-on");

            return playButton;
        }

        /// <summary>
        /// Creates a <c>div</c> tag for the given <paramref name="tag"/>.
        /// Then sets the tag to be the container for an auto-generated pager.
        /// </summary>
        /// <param name="tag">The tag whose content is being represented.</param>
        /// <param name="slide">The object from which to get the property.</param>
        /// <param name="resourceFile">The resource file from which to get localized resources.</param>
        /// <returns>
        /// The created pager container
        /// </returns>
        private Control CreatePager(Tag tag, ITemplateable slide, string resourceFile)
        {
            Panel pagerContainer = this.CreateRotatorContainer(tag, slide, resourceFile);

            pagerContainer.CssClass = "rotator-pager";
            this.CycleOptions.PagerEvent = TemplateEngine.GetAttributeValue(tag, slide, resourceFile, "Event") ?? "click";

            return pagerContainer;
        }

        /// <summary>
        /// Creates a <c>div</c> tag for the given <paramref name="tag"/>.
        /// Then sets the tag to be the container for a pager item (which, when clicked, rotates to its associated slide).
        /// </summary>
        /// <param name="tag">The tag whose content is being represented.</param>
        /// <param name="slide">The object from which to get the property.</param>
        /// <param name="resourceFile">The resource file from which to get localized resources.</param>
        /// <returns>
        /// The created pager item
        /// </returns>
        private Control CreatePagerItem(Tag tag, ITemplateable slide, string resourceFile)
        {
            Panel pagerItemWrapper = this.CreateRotatorContainer(tag, slide, resourceFile);
            pagerItemWrapper.CssClass = Engage.Utility.AddCssClass(pagerItemWrapper.CssClass, "pager-item-" + slide.GetValue("INDEX"));
            return pagerItemWrapper;
        }

        /// <summary>
        /// Creates a <see cref="Panel"/> from a tag, setting its CssClass and supplying Text or inner controls, if it has any.
        /// </summary>
        /// <param name="tag">The tag whose content is being represented.</param>
        /// <param name="slide">The object from which to get the property.</param>
        /// <param name="resourceFile">The resource file from which to get localized resources.</param>
        /// <returns>The created container</returns>
        private Panel CreateRotatorContainer(Tag tag, ITemplateable slide, string resourceFile)
        {
            Panel button = new Panel();
            button.CssClass = TemplateEngine.GetAttributeValue(tag, slide, resourceFile, "CssClass", "class");

            if (tag.HasChildTags)
            {
                TemplateEngine.ProcessTags(button, tag.ChildTags, slide, resourceFile, this.ProcessTags, this.GetSlides);
            }
            else
            {
                string innerText = TemplateEngine.GetAttributeValue(tag, slide, resourceFile, "Text");
                if (!string.IsNullOrEmpty(innerText))
                {
                    button.Controls.Add(new LiteralControl(innerText));
                }
            }

            return button;
        }

        /// <summary>
        /// Gets a list of the <see cref="Slide"/>s for this module.  Does not take the <paramref name="listTag"/> or <paramref name="context"/> into account,
        /// effectively only supporting one data source.
        /// </summary>
        /// <param name="listTag">The Engage:List <see cref="Tag"/> for which to return a data source</param>
        /// <param name="context">The current <see cref="ITemplateable"/> item being processed, or <c>null</c> if no list is currently being processed</param>
        /// <returns>A list of the <see cref="Slide"/>s for this module.</returns>
        private IEnumerable<ITemplateable> GetSlides(Tag listTag, ITemplateable context)
        {
            return Slide.GetSlides(this.ModuleId).ConvertAll(delegate(Slide input) { return (ITemplateable)input; });
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
                this.RegisterRotatorJavaScript();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Gets the template from this page's settings.  If no template is set, sets a default.
        /// </summary>
        /// <returns>The template from this page's settings, or <c>null</c> if no valid template is available</returns>
        private TemplateInfo GetTemplateSetting()
        {
            TemplateInfo template = null;
            string templateFolderName = Utility.GetStringSetting(this.Settings, "Template");
            if (!string.IsNullOrEmpty(templateFolderName))
            {
                template = this.GetTemplate(templateFolderName);
            }

            if (template == null)
            {
                template = this.GetDefaultTemplate();
                if (template == null)
                {
                    // if there are no templates, return null and cause the templating framework to throw an ArgumentNullException...
                    return null;
                }

                this.SetTemplateSetting(template.FolderName);
            }

            return template;
        }

        /// <summary>
        /// Gets a default template to use if no template has been set (or the set template is no longer available).
        /// </summary>
        /// <returns>A valid template, or <c>null</c> if there are no valid templates available</returns>
        private TemplateInfo GetDefaultTemplate()
        {
            IList<TemplateInfo> templates = this.GetTemplates(TemplateType.List);
            if (templates.Count > 0)
            {
                return templates[0];
            }

            return null;
        }

        /// <summary>
        /// Sets the template setting for this instance to the given <paramref name="folderName"/>.
        /// </summary>
        /// <param name="folderName">Name of the folder in which the template lives.</param>
        private void SetTemplateSetting(string folderName)
        {
            new ModuleController().UpdateTabModuleSetting(this.TabModuleId, "Template", folderName);
        }

        /// <summary>
        /// Adds the references and code to the page to enable the jQuery Cycle plugin
        /// </summary>
        private void RegisterRotatorJavaScript()
        {
            this.AddJQueryReference();
            this.Page.ClientScript.RegisterClientScriptResource(typeof(Rotator), "Engage.Dnn.ContentRotator.JavaScript.jquery.cycle.all.js");
        }
    }
}