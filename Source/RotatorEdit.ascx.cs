// <copyright file="RotatorEdit.ascx.cs" company="Engage Software">
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
    using System.Globalization;
    using DotNetNuke.Services.Exceptions;

    /// <summary>
    /// Code-behind for the control to add and edit content items
    /// </summary>
    public partial class RotatorEdit : ModuleBase
    {
        /// <summary>
        /// Backing field for <see cref="ContentItemId"/>
        /// </summary>
        private int? contentItemId;

        /// <summary>
        /// Gets the ID of the content item being edited. Returns <c>null</c> if creating a new content item
        /// </summary>
        /// <value>The ID of the content item being edited, or <c>null</c> if the content item is new.</value>
        private int? ContentItemId
        {
            get
            {
                if (!this.contentItemId.HasValue)
                {
                    int id;
                    if (int.TryParse(
                            this.Request.QueryString["id"], NumberStyles.Integer, CultureInfo.InvariantCulture, out id))
                    {
                        this.contentItemId = id;
                    }
                    else
                    {
                        this.contentItemId = null;
                    }
                }

                return this.contentItemId;
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
                    ContentItem item = this.ContentItemId.HasValue
                                               ? ContentItem.GetContentItem(this.ContentItemId.Value)
                                               : null;

                    if (item != null)
                    {
                        this.LoadContentItem(item);

                        // set UrlControls to type Url, so we don't have to convert back to a tabId or fileId
                        this.LinkUrlControl.UrlType = "U";
                        this.ThumbnailUrlControl.UrlType = "U";
                        this.PositionThumbnailUrlControl.UrlType = "U";
                    }
                    else
                    {
                        this.TitleTextBox.Text = string.Empty;
                        this.DescriptionTextEditor.Text = string.Empty;

                        // F = file
                        this.ThumbnailUrlControl.UrlType = "F";
                        this.ThumbnailUrlControl.Url = string.Empty;
                        this.PositionThumbnailUrlControl.UrlType = "F";
                        this.PositionThumbnailUrlControl.Url = string.Empty;

                        // T = tab
                        this.LinkUrlControl.UrlType = "T";
                        this.LinkUrlControl.Url = string.Empty;

                        this.StartDateTextBox.Text = DateTime.Today.ToShortDateString();
                        this.EndDateTextBox.Text = string.Empty;

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
                ContentItem contentItem = this.ContentItemId.HasValue
                                                  ? new ContentItem(this.contentItemId.Value)
                                                  : new ContentItem();

                contentItem.Title = this.TitleTextBox.Text;
                contentItem.Description = this.DescriptionTextEditor.Text;
                contentItem.StartDate = DateTime.Parse(this.StartDateTextBox.Text, CultureInfo.CurrentCulture);
                contentItem.EndDate = Engage.Utility.ParseNullableDateTime(this.EndDateTextBox.Text, CultureInfo.CurrentCulture);
                contentItem.LinkUrl = Dnn.Utility.CreateUrlFromControl(this.LinkUrlControl, this.PortalSettings);
                contentItem.ThumbnailUrl = Dnn.Utility.CreateUrlFromControl(this.ThumbnailUrlControl, this.PortalSettings);
                contentItem.PositionThumbnailUrl = Dnn.Utility.CreateUrlFromControl(this.PositionThumbnailUrlControl, this.PortalSettings);
                contentItem.SortOrder = int.Parse(this.SortOrderTextBox.Text, CultureInfo.CurrentCulture);

                contentItem.Save(this.ModuleId);

                this.Response.Redirect(this.EditUrl("Options"), false);
            }
        }

        /// <summary>
        /// Fills in the form with the information from the given <paramref name="item"/>
        /// </summary>
        /// <param name="item">The item whose information should be filled in.</param>
        private void LoadContentItem(ContentItem item)
        {
            this.TitleTextBox.Text = item.Title;
            this.DescriptionTextEditor.Text = item.Description;
            this.ThumbnailUrlControl.Url = item.ThumbnailUrl;
            this.LinkUrlControl.Url = item.LinkUrl;
            this.PositionThumbnailUrlControl.Url = item.PositionThumbnailUrl;
            this.StartDateTextBox.Text = item.StartDate.ToShortDateString();
            this.EndDateTextBox.Text = item.EndDate.HasValue ? item.EndDate.Value.ToShortDateString() : string.Empty;
            this.SortOrderTextBox.Text = item.SortOrder.ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Registers the jQuery date picker plugin on the page.
        /// </summary>
        private void RegisterDatePickerBehavior()
        {
            this.AddJQueryReference();

#if DEBUG
            this.Page.ClientScript.RegisterClientScriptResource(typeof(RotatorEdit), "Engage.Dnn.ContentRotator.JavaScript.jquery-ui.js");
#else
            this.Page.ClientScript.RegisterClientScriptResource(typeof(RotatorEdit), "Engage.Dnn.ContentRotator.JavaScript.jquery-ui.min.js");
#endif

            DatePickerOptions datePickerOptions = new DatePickerOptions(CultureInfo.CurrentCulture, this.LocalResourceFile);
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(RotatorEdit), "datepicker options", "var datePickerOpts = " + datePickerOptions.Serialize() + ";", true);
        }
    }
}