// <copyright file="Rotator.ascx.cs" company="Engage Software">
// Engage: Rotator
// Copyright (c) 2004-2012
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
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Modules.Actions;
    using DotNetNuke.Security;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using Framework.Templating;
    using Templating;

    using Utility = Engage.Utility;

    /// <summary>The main control displaying rotating slides</summary>
    public partial class Rotator : ModuleBase, IActionable
    {
        /// <summary>Backing field for <see cref="CycleOptions" /></summary>
        private CycleOptions cycleOptions;

        /// <summary>Gets ModuleActions.</summary>
        /// <value>The module actions.</value>
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

        /// <summary>Gets the <see cref="CycleOptions" /> for this module instance.</summary>
        /// <value>The cycle options.</value>
        /// <returns>The <see cref="CycleOptions" /> for this module instance</returns>
        protected CycleOptions CycleOptions
        {
            get
            {
                if (this.cycleOptions == null)
                {
// ReSharper disable PossibleInvalidOperationException
                    var slideHeight = ModuleSettings.SlideHeight.GetValueAsInt32For(this);
                    var slideWidth = ModuleSettings.SlideWidth.GetValueAsInt32For(this);
                    var containerHeight = slideHeight.HasValue ? Unit.Pixel(slideHeight.Value) : Unit.Empty;
                    var containerWidth = slideWidth.HasValue ? Unit.Pixel(slideWidth.Value) : Unit.Empty;
                    var transitionSpeed = ModuleSettings.UseAnimations.GetValueAsBooleanFor(this).Value
                                              ? ConvertSecondsToMilliseconds(ModuleSettings.AnimationDuration.GetValueAsDecimalFor(this).Value)
                                              : 1;

                    this.cycleOptions = new CycleOptions(
                            ModuleSettings.AutoStop.GetValueAsBooleanFor(this).Value,
                            ModuleSettings.AutoStopCount.GetValueAsInt32For(this).Value,
                            ModuleSettings.ContainerResize.GetValueAsBooleanFor(this).Value,
                            containerHeight,
                            containerWidth,
                            ModuleSettings.Continuous.GetValueAsBooleanFor(this).Value,
                            ConvertSecondsToMilliseconds(ModuleSettings.InitialDelay.GetValueAsDecimalFor(this).Value),
                            ConvertSecondsToMilliseconds(ModuleSettings.RotatorDelay.GetValueAsDecimalFor(this).Value) + transitionSpeed,
                            ModuleSettings.PauseOnHover.GetValueAsBooleanFor(this).Value,
                            ModuleSettings.AnimationEffect.GetValueAsEnumFor<Effects>(this).Value,
                            transitionSpeed,
                            ConvertSecondsToMilliseconds(ModuleSettings.ManuallyTriggeredTransitionSpeed.GetValueAsDecimalFor(this).Value),
                            ModuleSettings.Loop.GetValueAsBooleanFor(this).Value,
                            ModuleSettings.RandomOrder.GetValueAsBooleanFor(this).Value,
                            ModuleSettings.SimultaneousTransitions.GetValueAsBooleanFor(this).Value,
                            ModuleSettings.ForceSlidesToFitContainer.GetValueAsBooleanFor(this).Value,
                            ModuleSettings.DisableAddingBackgroundColorForClearTypeFix.GetValueAsBooleanFor(this).Value,
                            ModuleSettings.RandomizeEffects.GetValueAsBooleanFor(this).Value,
                            ModuleSettings.ManualTransitionTrumpsActiveTransition.GetValueAsBooleanFor(this).Value,
                            this.RotatorContainer);
// ReSharper restore PossibleInvalidOperationException
                }

                return this.cycleOptions;
            }
        }

        /// <summary>Raises the <see cref="Control.Init" /> event.</summary>
        /// <param name="e">An <see cref="EventArgs" /> object that contains the event data.</param>
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

        /// <summary>Converts the given number of seconds into milliseconds.</summary>
        /// <param name="seconds">The value in seconds.</param>
        /// <returns>The number of milliseconds represented by <paramref name="seconds" /></returns>
        private static int ConvertSecondsToMilliseconds(decimal seconds)
        {
            const int MillisecondsPerSecond = 1000;
            return (int)Math.Round(seconds * MillisecondsPerSecond);
        }

        /// <summary>Adds the given <paramref name="control" /> to the given <paramref name="container" /> unless <paramref name="control" /> is <c>null</c>.</summary>
        /// <param name="container">The container to which <paramref name="control" /> is to be added.</param>
        /// <param name="control">The control to add to <paramref name="container" />.</param>
        private static void AddControl(Control container, Control control)
        {
            if (control != null)
            {
                container.Controls.Add(control);
            }
        }

        /// <summary>Creates a <see cref="Label" /> whose content is the (1-based) index of the currently displayed <see cref="Slide" /></summary>
        /// <param name="tag">The tag whose content is being represented.</param>
        /// <param name="slide">The object from which to get the property.</param>
        /// <param name="resourceFile">The resource file from which to get localized resources.</param>
        /// <returns>The <see cref="Label" /> instance that was created</returns>
        private static Control CreateCurrentIndexControl(Tag tag, ITemplateable slide, string resourceFile)
        {
            Label currentSlideIndexWrapper = null;
            try
            {
                currentSlideIndexWrapper = new Label();
                currentSlideIndexWrapper.CssClass = TemplateEngine.GetAttributeValue(tag, slide, null, resourceFile, "CssClass", "class");
                currentSlideIndexWrapper.CssClass = Utility.AddCssClass(currentSlideIndexWrapper.CssClass, "current-slide-index");
                currentSlideIndexWrapper.Text = 1.ToString(CultureInfo.CurrentCulture);
                return currentSlideIndexWrapper;
            }
            catch
            {
                if (currentSlideIndexWrapper != null)
                {
                    currentSlideIndexWrapper.Dispose();
                }

                throw;
            }
        }

        /// <summary>Creates a <see cref="Label" /> whose content is the total number of <see cref="Slide" />s for this rotator.</summary>
        /// <param name="tag">The tag whose content is being represented.</param>
        /// <param name="slide">The object from which to get the property.</param>
        /// <param name="resourceFile">The resource file from which to get localized resources.</param>
        /// <returns>The <see cref="Label" /> instance that was created</returns>
        private static Control CreateTotalCountControl(Tag tag, ITemplateable slide, string resourceFile)
        {
            Label totalCountLabel = null;
            try
            {
                totalCountLabel = new Label();
                totalCountLabel.CssClass = TemplateEngine.GetAttributeValue(tag, slide, null, resourceFile, "CssClass", "class");
                totalCountLabel.CssClass = Utility.AddCssClass(totalCountLabel.CssClass, "total-slide-count");
                return totalCountLabel;
            }
            catch
            {
                if (totalCountLabel != null)
                {
                    totalCountLabel.Dispose();
                }

                throw;
            }
        }

        /// <summary>Processes template tokens not automatically processed by the <see cref="TemplateEngine" />, i.e. tokens specific to Rotator functionality.</summary>
        /// <param name="container">The container to which content is added.</param>
        /// <param name="tag">The tag being processed.</param>
        /// <param name="slide">The slide being processed (or <c>null</c>).</param>
        /// <param name="resourceFile">The resource file to use to find localized text.</param>
        /// <returns>Whether to process the tag's ChildTags collection</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Container takes care of disposing")]
        private bool ProcessTags(Control container, Tag tag, ITemplateable slide, string resourceFile)
        {
            if (tag.TagType != TagType.Open)
            {
                return false;
            }

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

            return false;
        }

        /// <summary>Creates a <c>div</c> tag for the given <paramref name="tag" />.
        /// Then sets the tag to cycle back when clicked.</summary>
        /// <param name="tag">The tag whose content is being represented.</param>
        /// <param name="slide">The object from which to get the property.</param>
        /// <param name="resourceFile">The resource file from which to get localized resources.</param>
        /// <returns>The created back button</returns>
        private Control CreateBackButton(Tag tag, ITemplateable slide, string resourceFile)
        {
            Panel backButton = null;
            try
            {
                backButton = this.CreateRotatorContainer(tag, slide, resourceFile);

                backButton.CssClass = Utility.AddCssClass(backButton.CssClass, "rotator-prev");

                return backButton;
            }
            catch
            {
                if (backButton != null)
                {
                    backButton.Dispose();
                }

                throw;
            }
        }

        /// <summary>Creates a <c>div</c> tag for the given <paramref name="tag" />.
        /// Then sets the tag to cycle forward when clicked.</summary>
        /// <param name="tag">The tag whose content is being represented.</param>
        /// <param name="slide">The object from which to get the property.</param>
        /// <param name="resourceFile">The resource file from which to get localized resources.</param>
        /// <returns>The created next button</returns>
        private Control CreateNextButton(Tag tag, ITemplateable slide, string resourceFile)
        {
            Panel nextButton = null;
            try
            {
                nextButton = this.CreateRotatorContainer(tag, slide, resourceFile);

                nextButton.CssClass = Utility.AddCssClass(nextButton.CssClass, "rotator-next");

                return nextButton;
            }
            catch
            {
                if (nextButton != null)
                {
                    nextButton.Dispose();
                }

                throw;
            }
        }

        /// <summary>Creates a <c>div</c> tag for the given <paramref name="tag" />.
        /// Then sets the tag to pause rotation when clicked.</summary>
        /// <param name="tag">The tag whose content is being represented.</param>
        /// <param name="slide">The object from which to get the property.</param>
        /// <param name="resourceFile">The resource file from which to get localized resources.</param>
        /// <returns>The created pause button</returns>
        private Control CreatePauseButton(Tag tag, ITemplateable slide, string resourceFile)
        {
            Panel pauseButton = null;
            try
            {
                pauseButton = this.CreateRotatorContainer(tag, slide, resourceFile);
                pauseButton.CssClass = Utility.AddCssClass(pauseButton.CssClass, "rotator-pause");
                return pauseButton;
            }
            catch
            {
                if (pauseButton != null)
                {
                    pauseButton.Dispose();
                }

                throw;
            }
        }

        /// <summary>Creates a <c>div</c> tag for the given <paramref name="tag" />.
        /// Then sets the tag to resume rotation when clicked.</summary>
        /// <param name="tag">The tag whose content is being represented.</param>
        /// <param name="slide">The object from which to get the property.</param>
        /// <param name="resourceFile">The resource file from which to get localized resources.</param>
        /// <returns>The created play button</returns>
        private Control CreatePlayButton(Tag tag, ITemplateable slide, string resourceFile)
        {
            Panel playButton = null;
            try
            {
                playButton = this.CreateRotatorContainer(tag, slide, resourceFile);

                playButton.CssClass = Utility.AddCssClass(playButton.CssClass, "rotator-play");
                playButton.CssClass = Utility.AddCssClass(playButton.CssClass, "rotator-play-on");

                return playButton;
            }
            catch
            {
                if (playButton != null)
                {
                    playButton.Dispose();
                }

                throw;
            }
        }

        /// <summary>Creates a <c>div</c> tag for the given <paramref name="tag" />.
        /// Then sets the tag to be the container for an auto-generated pager.</summary>
        /// <param name="tag">The tag whose content is being represented.</param>
        /// <param name="slide">The object from which to get the property.</param>
        /// <param name="resourceFile">The resource file from which to get localized resources.</param>
        /// <returns>The created pager container</returns>
        private Control CreatePager(Tag tag, ITemplateable slide, string resourceFile)
        {
            Panel pagerContainer = null;
            try
            {
                pagerContainer = this.CreateRotatorContainer(tag, slide, resourceFile);

                pagerContainer.CssClass = Utility.AddCssClass(pagerContainer.CssClass, "rotator-pager");
                this.CycleOptions.PagerEvent = TemplateEngine.GetAttributeValue(tag, slide, (ITemplateable)null, resourceFile, "Event") ?? "click";

                return pagerContainer;
            }
            catch
            {
                if (pagerContainer != null)
                {
                    pagerContainer.Dispose();
                }

                throw;
            }
        }

        /// <summary>Creates a <c>div</c> tag for the given <paramref name="tag" />.
        /// Then sets the tag to be the container for a pager item (which, when clicked, rotates to its associated slide).</summary>
        /// <param name="tag">The tag whose content is being represented.</param>
        /// <param name="slide">The object from which to get the property.</param>
        /// <param name="resourceFile">The resource file from which to get localized resources.</param>
        /// <returns>The created pager item</returns>
        private Control CreatePagerItem(Tag tag, ITemplateable slide, string resourceFile)
        {
            Panel pagerItemWrapper = null;
            try
            {
                pagerItemWrapper = this.CreateRotatorContainer(tag, slide, resourceFile);
                pagerItemWrapper.CssClass = Utility.AddCssClass(pagerItemWrapper.CssClass, "pager-item-" + slide.GetValue("INDEX"));
                return pagerItemWrapper;
            }
            catch
            {
                if (pagerItemWrapper != null)
                {
                    pagerItemWrapper.Dispose();
                }

                throw;
            }
        }

        /// <summary>Creates a <see cref="Panel" /> from a tag, setting its <see cref="Panel.CssClass" /> and supplying Text or inner controls, if it has any.</summary>
        /// <param name="tag">The tag whose content is being represented.</param>
        /// <param name="slide">The object from which to get the property.</param>
        /// <param name="resourceFile">The resource file from which to get localized resources.</param>
        /// <returns>The created container</returns>
        private Panel CreateRotatorContainer(Tag tag, ITemplateable slide, string resourceFile)
        {
            Panel container = null;
            try
            {
                container = new Panel();
                container.CssClass = TemplateEngine.GetAttributeValue(tag, slide, null, resourceFile, "CssClass", "class");

                if (tag.HasChildTags)
                {
                    TemplateEngine.ProcessTags(container, tag.ChildTags, slide, null, resourceFile, this.ProcessTags, this.GetSlides);
                }
                else
                {
                    var innerText = TemplateEngine.GetAttributeValue(tag, slide, (ITemplateable)null, resourceFile, "Text");
                    if (!string.IsNullOrEmpty(innerText))
                    {
                        container.Controls.Add(new LiteralControl(innerText));
                    }
                }

                return container;
            }
            catch
            {
                if (container != null)
                {
                    container.Dispose();
                }

                throw;
            }
        }

        /// <summary>Gets a list of the <see cref="Slide" />s for this module.  Does not take the <paramref name="listTag" /> or <paramref name="context" /> into account,
        /// effectively only supporting one data source.</summary>
        /// <param name="listTag">The Engage:List <see cref="Tag" /> for which to return a data source</param>
        /// <param name="context">The current <see cref="ITemplateable" /> item being processed, or <c>null</c> if no list is currently being processed</param>
        /// <returns>A list of the <see cref="Slide" />s for this module.</returns>
        private IEnumerable<ITemplateable> GetSlides(Tag listTag, ITemplateable context)
        {
            return Slide.GetSlides(this.ModuleId).ConvertAll(input => (ITemplateable)input);
        }

        /// <summary>Handles the <see cref="Control.Load"/> event of this control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "ProcessModuleLoadException handles exception, no need to rethrow")]
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

        /// <summary>Gets the template from this page's settings.  If no template is set, sets a default.</summary>
        /// <returns>The template from this page's settings, or <c>null</c> if no valid template is available</returns>
        private TemplateInfo GetTemplateSetting()
        {
            TemplateInfo template = null;
            string templateFolderName = ModuleSettings.TemplateFolderName.GetValueAsStringFor(this);
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

        /// <summary>Gets a default template to use if no template has been set (or the set template is no longer available).</summary>
        /// <returns>A valid template, or <c>null</c> if there are no valid templates available</returns>
        private TemplateInfo GetDefaultTemplate()
        {
            return this.GetTemplates(TemplateType.List).FirstOrDefault();
        }

        /// <summary>Sets the template setting for this instance to the given <paramref name="folderName" />.</summary>
        /// <param name="folderName">Name of the folder in which the template lives.</param>
        private void SetTemplateSetting(string folderName)
        {
            ModuleSettings.TemplateFolderName.Set(this, folderName);
        }

        /// <summary>Adds the references and code to the page to enable the jQuery Cycle plugin</summary>
        private void RegisterRotatorJavaScript()
        {
            this.AddJQueryReference();
            this.Page.ClientScript.RegisterClientScriptResource(typeof(Rotator), "Engage.Dnn.ContentRotator.JavaScript.rotator.all.js");
        }
    }
}