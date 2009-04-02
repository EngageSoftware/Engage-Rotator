// <copyright file="RotatorOptions.ascx.cs" company="Engage Software">
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
    using System.Web.UI.WebControls;
    using DotNetNuke.Framework;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.UI.Utilities;
    using Globals = DotNetNuke.Common.Globals;

    /// <summary>
    /// The code-behind for the control that displays an administrative list of all the <see cref="Slide"/>s for this instance of the module
    /// </summary>
    public partial class RotatorOptions : ModuleBase
    {
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            this.RequiresScriptManager = false;

            base.OnInit(e);
            this.Load += this.Page_Load;
            this.BackButton.Click += this.BackButton_Click;
            this.BackButton2.Click += this.BackButton_Click;
            this.NewSlideButton.Click += this.NewSlideButton_Click;
            this.SlidesRepeater.ItemDataBound += this.SlidesRepeater_ItemDataBound;
            this.SlidesRepeater.ItemCommand += this.SlidesRepeater_ItemCommand;
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
                if (AJAX.IsInstalled())
                {
                    AJAX.RegisterScriptManager();
                }

                if (!this.Page.IsPostBack)
                {
                    this.BindData();
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Handles the Click event of the <see cref="BackButton"/> and <see cref="BackButton2"/> controls.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Response.Redirect(Globals.NavigateURL(this.TabId));
        }

        /// <summary>
        /// Handles the Click event of the <see cref="NewSlideButton"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NewSlideButton_Click(object sender, EventArgs e)
        {
            this.Response.Redirect(this.EditUrl("Edit"), false);
        }

        /// <summary>
        /// Handles the <see cref="Repeater.ItemCommand"/> event of the <see cref="SlidesRepeater"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterCommandEventArgs"/> instance containing the event data.</param>
        private void SlidesRepeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e != null)
            {
                if (e.CommandName == "Delete")
                {
                    Slide.Delete(this.GetIdFromIndex(e.Item.ItemIndex));
                    this.BindData();
                }
                else if (e.CommandName == "Edit")
                {
                    this.Response.Redirect(this.EditUrl(
                        "Edit",
                        string.Empty,
                        string.Empty,
                        "id=" + this.GetIdFromIndex(e.Item.ItemIndex).ToString(CultureInfo.InvariantCulture)));
                }
            }
        }

        /// <summary>
        /// Handles the <see cref="Repeater.ItemDataBound"/> event of the <see cref="SlidesRepeater"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
        private void SlidesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e != null)
            {
                Button deleteSlideButton = (Button)e.Item.FindControl("DeleteSlideButton");
                ClientAPI.AddButtonConfirm(deleteSlideButton, Localization.GetString("DeleteConfirm.Text", this.LocalResourceFile));
            }
        }

        /// <summary>
        /// Binds the list of slides for this module to the <see cref="SlidesRepeater"/>.
        /// </summary>
        private void BindData()
        {
            this.SlidesRepeater.DataSource = Slide.GetSlides(this.ModuleId, true);
            this.SlidesRepeater.DataBind();
        }

        /// <summary>
        /// Gets the ID of the <see cref="Slide"/> in the specified row of the repeater.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns>The ID of the <see cref="Slide"/></returns>
        private int GetIdFromIndex(int rowIndex)
        {
            HiddenField slideIdHiddenField = (HiddenField)this.SlidesRepeater.Items[rowIndex].FindControl("SlideIdHiddenField");
            return int.Parse(slideIdHiddenField.Value, CultureInfo.InvariantCulture);
        }
    }
}