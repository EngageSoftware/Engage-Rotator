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
    using System.Text;
    using System.Web.UI.WebControls;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Framework;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.UI.Utilities;
    using Globals = DotNetNuke.Common.Globals;

    /// <summary>
    /// The code-behind for the control that displays an administrative list of all the <see cref="ContentItem"/>s for this instance of the module
    /// </summary>
    public partial class RotatorOptions : PortalModuleBase
    {
        /// <summary>
        /// Gets the style to apply to the position thumbnail.
        /// </summary>
        /// <value>The style to apply to the position thumbnail.</value>
        protected string PositionThumbnailStyle
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
        /// Gets the style to apply to the thumbnail.
        /// </summary>
        /// <value>The style to apply to the thumbnail.</value>
        protected string ThumbnailStyle
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
        /// Gets the setting for the height of the position thumbnail.
        /// </summary>
        /// <value>The setting for the height of the position thumbnail.</value>
        private int? PositionThumbnailHeight
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "PositionThumbnailHeight");
            }
        }

        /// <summary>
        /// Gets the setting for the width of the position thumbnail.
        /// </summary>
        /// <value>The setting for the width of the position thumbnail.</value>
        private int? PositionThumbnailWidth
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "PositionThumbnailWidth");
            }
        }

        /// <summary>
        /// Gets the setting for the height of the thumbnail.
        /// </summary>
        /// <value>The setting for the height of the thumbnail.</value>
        private int? ThumbnailHeight
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "ThumbnailHeight");
            }
        }

        /// <summary>
        /// Gets the setting for the width of the thumbnail.
        /// </summary>
        /// <value>The setting for the width of the thumbnail.</value>
        private int? ThumbnailWidth
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "ThumbnailWidth");
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
            this.BackButton.Click += this.BackButton_Click;
            this.BackButton2.Click += this.BackButton_Click;
            this.NewContentItemButton.Click += this.NewContentItemButton_Click;
            this.ContentItemsRepeater.ItemDataBound += this.ContentItemsRepeater_ItemDataBound;
            this.ContentItemsRepeater.ItemCommand += this.ContentItemsRepeater_ItemCommand;
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
        /// Handles the Click event of the <see cref="NewContentItemButton"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NewContentItemButton_Click(object sender, EventArgs e)
        {
            this.Response.Redirect(this.EditUrl("Edit"), false);
        }

        /// <summary>
        /// Handles the ItemCommand event of the <see cref="ContentItemsRepeater"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterCommandEventArgs"/> instance containing the event data.</param>
        private void ContentItemsRepeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e != null)
            {
                if (e.CommandName == "Delete")
                {
                    ContentItem.Delete(this.GetIdFromIndex(e.Item.ItemIndex));
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
        /// Handles the ItemDataBound event of the <see cref="ContentItemsRepeater"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
        private void ContentItemsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e != null)
            {
                Button deleteItemButton = (Button)e.Item.FindControl("DeleteItemButton");
                ClientAPI.AddButtonConfirm(deleteItemButton, Localization.GetString("DeleteConfirm.Text", this.LocalResourceFile));
            }
        }

        /// <summary>
        /// Binds the list of content items for this module to the <see cref="ContentItemsRepeater"/>.
        /// </summary>
        private void BindData()
        {
            this.ContentItemsRepeater.DataSource = ContentItem.GetContentItems(this.ModuleId, true);
            this.ContentItemsRepeater.DataBind();
        }

        /// <summary>
        /// Gets the ID of the <see cref="ContentItem"/> in the specified row of the repeater.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns>The ID of the <see cref="ContentItem"/></returns>
        private int GetIdFromIndex(int rowIndex)
        {
            HiddenField contentItemIdHiddenField = (HiddenField)this.ContentItemsRepeater.Items[rowIndex].FindControl("ContentItemIdHiddenField");
            return int.Parse(contentItemIdHiddenField.Value, CultureInfo.InvariantCulture);
        }
    }
}