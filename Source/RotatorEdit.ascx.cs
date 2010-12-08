// <copyright file="RotatorEdit.ascx.cs" company="Engage Software">
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
    using System.Globalization;

    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Services.Exceptions;

    /// <summary>
    /// Control to add and edit slides
    /// </summary>
    public partial class RotatorEdit : ModuleBase
    {
        /// <summary>
        /// Backing field for <see cref="SlideId"/>
        /// </summary>
        private int? slideId;

        /// <summary>
        /// Gets the ID of the slide being edited. Returns <c>null</c> if creating a new slide
        /// </summary>
        /// <value>The ID of the slide being edited, or <c>null</c> if the slide is new.</value>
        private int? SlideId
        {
            get
            {
                if (!this.slideId.HasValue)
                {
                    int id;
                    if (int.TryParse(this.Request.QueryString["id"], NumberStyles.Integer, CultureInfo.InvariantCulture, out id))
                    {
                        this.slideId = id;
                    }
                    else
                    {
                        this.slideId = null;
                    }
                }

                return this.slideId;
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
            this.SubmitButton.Click += this.SubmitButton_Click;
            this.CancelButton.Click += this.CancelButton_Click;
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
                    this.TitleTextBox.Focus();
                    Slide slide = this.SlideId.HasValue
                                               ? Slide.GetSlide(this.SlideId.Value)
                                               : null;

                    if (slide != null)
                    {
                        this.LoadSlide(slide);
                    }
                    else
                    {
                        this.StartDateTextBox.Text = DateTime.Today.ToShortDateString();

                        // 5 is database default
                        this.SortOrderTextBox.Text = 5.ToString(CultureInfo.InvariantCulture);
                    }
                }

                this.RegisterDatePickerBehavior();
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
            this.Response.Redirect(this.EditUrl("Options"), false);
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
                Slide slide = this.SlideId.HasValue
                                                  ? new Slide(this.slideId.Value)
                                                  : new Slide();

                slide.Title = this.TitleTextBox.Text;
                slide.Content = this.ContentTextEditor.Text;
                slide.StartDate = DateTime.Parse(this.StartDateTextBox.Text, CultureInfo.CurrentCulture);
                slide.EndDate = Engage.Utility.ParseNullableDateTime(this.EndDateTextBox.Text, CultureInfo.CurrentCulture);
                slide.Link = this.LinkUrlControl.Url;
                slide.TrackLink = this.LinkUrlControl.Track;
                slide.ImageLink = this.ImageUrlControl.Url;
                slide.PagerImageLink = this.PagerImageUrlControl.Url;
                slide.SortOrder = int.Parse(this.SortOrderTextBox.Text, CultureInfo.CurrentCulture);

                slide.Save(this.ModuleId);

                new UrlController().UpdateUrl(
                    this.PortalId,
                    slide.Link,
                    this.LinkUrlControl.UrlType,
                    this.LinkUrlControl.Log,
                    slide.TrackLink,
                    this.ModuleId,
                    false);

                this.Response.Redirect(this.EditUrl("Options"), false);
            }
        }

        /// <summary>
        /// Fills in the form with the information from the given <paramref name="slide"/>
        /// </summary>
        /// <param name="slide">The slide whose information should be filled in.</param>
        private void LoadSlide(Slide slide)
        {
            this.TitleTextBox.Text = slide.Title;
            this.ContentTextEditor.Text = slide.Content;
            this.ImageUrlControl.Url = slide.ImageLink;
            this.LinkUrlControl.Url = slide.Link;
            this.PagerImageUrlControl.Url = slide.PagerImageLink;
            this.StartDateTextBox.Text = slide.StartDate.ToShortDateString();
            this.EndDateTextBox.Text = slide.EndDate.HasValue ? slide.EndDate.Value.ToShortDateString() : string.Empty;
            this.SortOrderTextBox.Text = slide.SortOrder.ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Registers the jQuery date picker plugin on the page.
        /// </summary>
        private void RegisterDatePickerBehavior()
        {
            this.AddJQueryReference();

            this.Page.ClientScript.RegisterClientScriptResource(typeof(RotatorEdit), "Engage.Dnn.ContentRotator.JavaScript.jquery-ui.js");

            var datePickerOptions = new DatePickerOptions(CultureInfo.CurrentCulture, this.LocalResourceFile);
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(RotatorEdit), "datepicker options", "var datePickerOpts = " + datePickerOptions.Serialize() + ";", true);
        }
    }
}