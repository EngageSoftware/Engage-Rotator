// <copyright file="Rotator.ascx.cs" company="Engage Software">
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
    /// Code-behind for the main control displaying rotating content
    /// </summary>
    public partial class Rotator : ModuleBase, IActionable
    {
        /// <summary>
        /// Gets ModuleActions.
        /// </summary>
        public ModuleActionCollection ModuleActions
        {
            get
            {
                return new ModuleActionCollection(new ModuleAction[]
                        {
                                new ModuleAction(
                                        this.GetNextActionID(),
                                        Localization.GetString("Add/Edit Content", this.LocalResourceFile),
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
        /// Gets or sets the template provider.
        /// </summary>
        /// <value>The template provider.</value>
        protected new RepeaterTemplateProvider TemplateProvider
        {
            get
            {
                return (RepeaterTemplateProvider)base.TemplateProvider;
            }

            set
            {
                base.TemplateProvider = value;
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
                Unit containerHeight = this.RotatorHeight.HasValue ? Unit.Pixel(this.RotatorHeight.Value) : Unit.Empty;
                int transitionSpeed = this.UseAnimations ? ConvertSecondsToMilliseconds(this.AnimationDuration) : 0;
                return new CycleOptions(
                        this.AutoStop,
                        this.AutoStopCount, 
                        this.ContainerResize,
                        containerHeight, 
                        this.Continuous,
                        ConvertSecondsToMilliseconds(this.InitialDelay),
                        ConvertSecondsToMilliseconds(this.RotatorDelay),
                        this.PauseOnMouseOver,
                        Effects.fade,
                        transitionSpeed, 
                        ConvertSecondsToMilliseconds(this.ManuallyTriggeredTransitionSpeed), 
                        this.Loop);
            }
        }

        /// <summary>
        /// Gets AnimationDuration.
        /// </summary>
        private decimal AnimationDuration
        {
            get
            {
                return Dnn.Utility.GetDecimalSetting(this.Settings, "AnimationDuration", 0.3m);
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
        /// Gets a value indicating whether PauseOnMouseOver.
        /// </summary>
        private bool PauseOnMouseOver
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "AnimationPauseOnMouseOver", true);
            }
        }

        /// <summary>
        /// Gets RotatorDelay.
        /// </summary>
        private int RotatorDelay
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "RotatorDelay", 8);
            }
        }

        /// <summary>
        /// Gets RotatorHeight.
        /// </summary>
        private int? RotatorHeight
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "RotatorHeight");
            }
        }

        /// <summary>
        /// Gets a value indicating whether UseAnimations.
        /// </summary>
        private bool UseAnimations
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "UseAnimations", true);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += this.Page_Load;
            this.TemplateProvider = new RepeaterTemplateProvider(
                    this.DesktopModuleName,
                    TemplateEngine.GetTemplate(this.PhysicalTemplatesFolderName, "Header.test.html"),
                    this.HeaderTemplatePlaceholder,
                    TemplateEngine.GetTemplate(this.PhysicalTemplatesFolderName, "Item.test.html"),
                    this.ItemTemplateSection,
                    TemplateEngine.GetTemplate(this.PhysicalTemplatesFolderName, "Footer.test.html"),
                    this.FooterTemplatePlaceholder,
                    string.Empty,
                    new ItemPagingState(),
                    ProcessTags);
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
        /// Method used to process a token. This method is invoked from the <see cref="TemplateEngine"/> class. Since this control knows
        /// best on how to construct the page. ListingHeader, ListingItem and Listing Footer templates are processed here.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="tag">The tag being processed.</param>
        /// <param name="engageObject">The engage object.</param>
        /// <param name="resourceFile">The resource file to use to find localized text.</param>
        private static void ProcessTags(Control container, Tag tag, object engageObject, string resourceFile)
        {
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
                this.TemplateProvider.DataSource = ContentItem.GetContentItems(this.ModuleId);
                this.TemplateProvider.DataBind();

                this.RegisterRotatorJavascript();
                this.DataBind();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Adds the references and code to the page to enable the jQuery Cycle plugin
        /// </summary>
        private void RegisterRotatorJavascript()
        {
            this.AddJQueryReference();

#if DEBUG
            this.Page.ClientScript.RegisterClientScriptResource(typeof(Rotator), "Engage.Dnn.ContentRotator.JavaScript.jquery.cycle.all.js");
#else
            this.Page.ClientScript.RegisterClientScriptResource(typeof(Rotator), "Engage.Dnn.ContentRotator.JavaScript.jquery.cycle.all.min.js");
#endif
            ////this.Page.ClientScript.RegisterClientScriptResource(typeof(Rotator), "Engage.Dnn.ContentRotator.JavaScript.Rotator.js");
        }
    }
}