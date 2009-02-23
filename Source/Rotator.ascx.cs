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
    using System.Text;
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
                ModuleActionCollection actions = new ModuleActionCollection();
                actions.Add(
                        this.GetNextActionID(),
                        Localization.GetString("Add/Edit Content", this.LocalResourceFile),
                        ModuleActionType.AddContent,
                        string.Empty,
                        string.Empty,
                        this.EditUrl("Options"),
                        false,
                        SecurityAccessLevel.Edit,
                        true,
                        false);
                actions.Add(
                        this.GetNextActionID(),
                        Localization.GetString("Rotator Settings", this.LocalResourceFile),
                        ModuleActionType.AddContent,
                        string.Empty,
                        string.Empty,
                        this.EditUrl("ModSettings"),
                        false,
                        SecurityAccessLevel.Edit,
                        true,
                        false);
                return actions;
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
        /// Gets AnimationFramesPerSecond.
        /// </summary>
        private int AnimationFramesPerSecond
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "AnimationFramesPerSecond", 30);
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
        /// Gets ContentDisplayMode.
        /// </summary>
        private DisplayType ContentDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ContentDisplayMode", DisplayType.Content);
            }
        }

        /// <summary>
        /// Gets ContentHeight.
        /// </summary>
        private int? ContentHeight
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "ContentHeight");
            }
        }

        /// <summary>
        /// Gets ContentTitleDisplayMode.
        /// </summary>
        private DisplayType ContentTitleDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ContentTitleDisplayMode", DisplayType.Link);
            }
        }

        /// <summary>
        /// Gets ContentWidth.
        /// </summary>
        private int? ContentWidth
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "ContentWidth");
            }
        }

        /// <summary>
        /// Gets ContentWrapperStyle.
        /// </summary>
        private string ContentWrapperStyle
        {
            get
            {
                StringBuilder style = new StringBuilder();
                Utility.AddStyle(style, "width", this.ContentWidth);
                Utility.AddStyle(style, "height", this.ContentHeight);
                return style.ToString();
            }
        }

        /// <summary>
        /// Gets NextButtonPlacement.
        /// </summary>
        private ControlPlacement NextButtonPlacement
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ControlsNextPlacement", ControlPlacement.Below);
            }
        }

        /// <summary>
        /// Gets PauseButtonPlacement.
        /// </summary>
        private ControlPlacement PauseButtonPlacement
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ControlsPausePlacement", ControlPlacement.Above);
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
        /// Gets PositionCounterPlacement.
        /// </summary>
        private ControlPlacement PositionCounterPlacement
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "PositionCounterPlacement", ControlPlacement.Below);
            }
        }

        /// <summary>
        /// Gets PositionThumbnailDisplayMode.
        /// </summary>
        private DisplayType PositionThumbnailDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "PositionThumbnailDisplayMode", DisplayType.Link);
            }
        }

        /// <summary>
        /// Gets PositionThumbnailHeight.
        /// </summary>
        private int? PositionThumbnailHeight
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "PositionThumbnailHeight");
            }
        }

        /// <summary>
        /// Gets PositionThumbnailStyle.
        /// </summary>
        private string PositionThumbnailStyle
        {
            get
            {
                StringBuilder style = new StringBuilder();
                Utility.AddStyle(style, "width", this.PositionThumbnailWidth);
                Utility.AddStyle(style, "height", this.PositionThumbnailHeight);
                return style.ToString();
            }
        }

        /// <summary>
        /// Gets PositionThumbnailWidth.
        /// </summary>
        private int? PositionThumbnailWidth
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "PositionThumbnailWidth");
            }
        }

        /// <summary>
        /// Gets PositionTitleDisplayMode.
        /// </summary>
        private DisplayType PositionTitleDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ControlsTitleDisplayMode", DisplayType.Link);
            }
        }

        /// <summary>
        /// Gets PreviousButtonPlacement.
        /// </summary>
        private ControlPlacement PreviousButtonPlacement
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ControlsPreviousPlacement", ControlPlacement.Above);
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
        /// Gets RotatorPauseDelay.
        /// </summary>
        private int RotatorPauseDelay
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "RotatorPauseDelay", 3);
            }
        }

        /// <summary>
        /// Gets RotatorWidth.
        /// </summary>
        private int RotatorWidth
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "RotatorWidth", 200);
            }
        }

        /// <summary>
        /// Gets RotatorWrapperStyle.
        /// </summary>
        private string RotatorWrapperStyle
        {
            get
            {
                StringBuilder style = new StringBuilder();
                Utility.AddStyle(style, "width", this.RotatorWidth);
                Utility.AddStyle(style, "height", this.RotatorHeight);
                return style.ToString();
            }
        }
        
        /// <summary>
        /// Gets a value indicating whether ShowNextButton.
        /// </summary>
        private bool ShowNextButton
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "ControlsShowNextButton", true);
            }
        }

        /// <summary>
        /// Gets a value indicating whether ShowPauseButton.
        /// </summary>
        private bool ShowPauseButton
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "ControlsShowPauseButton", true);
            }
        }

        /// <summary>
        /// Gets a value indicating whether ShowPositionCounter.
        /// </summary>
        private bool ShowPositionCounter
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "ShowPositionCounter", false);
            }
        }

        /// <summary>
        /// Gets a value indicating whether ShowPreviousButton.
        /// </summary>
        private bool ShowPreviousButton
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "ControlsShowPrevButton", true);
            }
        }

        /// <summary>
        /// Gets a value indicating whether ShowReadMoreLink.
        /// </summary>
        private bool ShowReadMoreLink
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "ShowReadMoreLink", false);
            }
        }

        /// <summary>
        /// Gets ThumbnailDisplayMode.
        /// </summary>
        private DisplayType ThumbnailDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ThumbnailDisplayMode", DisplayType.Link);
            }
        }

        /// <summary>
        /// Gets ThumbnailHeight.
        /// </summary>
        private int? ThumbnailHeight
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "ThumbnailHeight");
            }
        }

        /// <summary>
        /// Gets ThumbnailStyle.
        /// </summary>
        private string ThumbnailStyle
        {
            get
            {
                StringBuilder style = new StringBuilder();
                Utility.AddStyle(style, "width", this.ThumbnailWidth);
                Utility.AddStyle(style, "height", this.ThumbnailHeight);
                return style.ToString();
            }
        }

        /// <summary>
        /// Gets ThumbnailWidth.
        /// </summary>
        private int? ThumbnailWidth
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "ThumbnailWidth");
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
        /// Gets the <see cref="CycleOptions"/> for this module instance.
        /// </summary>
        /// <returns>The <see cref="CycleOptions"/> for this module instance</returns>
        protected CycleOptions GetCycleOptions()
        {
            Unit containerHeight = this.RotatorHeight.HasValue ? Unit.Pixel(this.RotatorHeight.Value) : Unit.Empty;
            int transitionSpeed = this.UseAnimations ? (int)(this.AnimationDuration * 1000) : 0;
            return new CycleOptions(this.AutoStop, this.AutoStopCount, containerHeight, this.RotatorDelay * 1000, this.PauseOnMouseOver, Effects.fade, transitionSpeed);
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