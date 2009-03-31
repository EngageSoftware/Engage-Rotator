// <copyright file="TemplateSelection.ascx.cs" company="Engage Software">
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
    using System.Collections.Generic;
    using System.Threading;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml.Schema;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using Framework.Templating;
    using Globals = DotNetNuke.Common.Globals;

    /// <summary>
    /// Control to select the template to use with the rotator
    /// </summary>
    public partial class TemplateSelection : ModuleBase
    {
        /// <summary>
        /// Gets the setting for the selected style template.
        /// </summary>
        /// <value>The selected style template.</value>
        private string Template
        {
            get
            {
                return Utility.GetStringSetting(this.Settings, "Template");
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.Init"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += this.Page_Load;
            this.CancelButton.Click += this.CancelButton_Click;
            this.SubmitButton.Click += this.SubmitButton_Click;
            this.SettingsGrid.RowDataBound += SettingsGrid_RowDataBound;
            this.TemplatesDropDownList.SelectedIndexChanged += this.TemplatesDropDownList_SelectedIndexChanged;
        }

        /// <summary>
        /// Handles the <see cref="GridView.RowDataBound"/> event of the <see cref="SettingsGrid"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        private static void SettingsGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                KeyValuePair<string, Pair<string, string>> settingInfo = (KeyValuePair<string, Pair<string, string>>)e.Row.DataItem;

                // new setting value
                e.Row.Cells[1].Text = settingInfo.Value.First;

                // current setting value
                e.Row.Cells[2].Text = settingInfo.Value.Second;
            }
        }

        /// <summary>
        /// Handles the <see cref="Control.Load"/> event of this control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    Utility.LocalizeGridView(ref this.SettingsGrid, this.LocalResourceFile);
                    this.FillTemplatesList();
                    this.TemplatesDropDownList.SelectedValue = this.Template;
                    this.FillTemplateTab();
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Handles the <see cref="Button.Click"/> event of the <see cref="CancelButton"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Response.Redirect(Globals.NavigateURL(this.TabId), false);
        }

        /// <summary>
        /// Handles the <see cref="Button.Click"/> event of the <see cref="SubmitButton"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                ModuleController modules = new ModuleController();
                modules.UpdateTabModuleSetting(this.TabModuleId, "Template", this.TemplatesDropDownList.SelectedValue);

                try
                {
                    TemplateInfo manifest = this.GetTemplate(this.TemplatesDropDownList.SelectedValue);
                    if (manifest.Settings != null)
                    {
                        foreach (KeyValuePair<string, string> setting in manifest.Settings)
                        {
                            modules.UpdateTabModuleSetting(this.TabModuleId, setting.Key, setting.Value);
                        }
                    }

                    this.Response.Redirect(Globals.NavigateURL(this.TabId));
                }
                catch (XmlSchemaValidationException)
                {
                    this.ShowManifestValidationErrorMessage();
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Handles the <see cref="ListControl.SelectedIndexChanged"/> event of the <see cref="TemplatesDropDownList"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TemplatesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FillTemplateTab();
        }

        /// <summary>
        /// Fills <see cref="TemplatesDropDownList"/>.
        /// </summary>
        private void FillTemplatesList()
        {
            this.TemplatesDropDownList.DataSource = this.GetTemplates();
            this.TemplatesDropDownList.DataTextField = "Title";
            this.TemplatesDropDownList.DataValueField = "FolderName";
            this.TemplatesDropDownList.DataBind();
        }

        /// <summary>
        /// Displays information about the selected template
        /// </summary>
        private void FillTemplateTab()
        {
            try
            {
                TemplateInfo manifest = this.GetTemplate(this.TemplatesDropDownList.SelectedValue);
                if (manifest != null)
                {
                    this.TemplateDescriptionLabel.Text = manifest.Description;
                    this.TemplatePreviewImage.ImageUrl = manifest.GetRelativePath(manifest.PreviewImage, true);
                    this.TemplateDescriptionPanel.Visible = Engage.Utility.HasValue(manifest.Description);
                    this.TemplatePreviewImage.Visible = Engage.Utility.HasValue(manifest.PreviewImage);

                    this.SettingsGrid.DataSource = this.GetChangedSettings(manifest.Settings);
                    this.SettingsGrid.DataBind();
                    this.LocalizeSettingsGrid();
                    this.SettingsExplanationLabel.Visible = this.SettingsGrid.Rows.Count > 0;
                }
                else
                {
                    this.TemplateDescriptionPanel.Visible = false;
                    this.TemplatePreviewImage.Visible = false;
                }
            }
            catch (XmlSchemaValidationException)
            {
                this.ShowManifestValidationErrorMessage();
            }
        }

        /// <summary>
        /// Gets a list of the settings that will be changed by apply the template.
        /// </summary>
        /// <param name="settings">The settings collection for the template.</param>
        /// <returns>
        /// A <see cref="IDictionary{TKey,TValue}"/> of the settings that will be changed by applying the template,  where the key is the name of the setting,
        /// and the value is a <see cref="Pair{TFirst,TSecond}"/> where the 
        /// <see cref="Pair{TFirst,TSecond}.First"/> property has the new value and the 
        /// <see cref="Pair{TFirst,TSecond}.Second"/> property has the current value
        /// </returns>
        private IDictionary<string, Pair<string, string>> GetChangedSettings(ICollection<KeyValuePair<string, string>> settings)
        {
            IDictionary<string, Pair<string, string>> changedSettings = new Dictionary<string, Pair<string, string>>(settings.Count);
            foreach (KeyValuePair<string, string> settingPair in settings)
            {
                // TODO: We need to take default settings into account, in case they haven't changed any of the settings yet
                string currentSetting = Utility.GetStringSetting(this.Settings, settingPair.Key);
                if (!settingPair.Value.Equals(currentSetting, StringComparison.OrdinalIgnoreCase))
                {
                    changedSettings.Add(settingPair.Key, new Pair<string, string>(settingPair.Value, currentSetting));
                }
            }

            return changedSettings;
        }

        /// <summary>
        /// Localizes the setting names in <see cref="SettingsGrid"/>.
        /// </summary>
        private void LocalizeSettingsGrid()
        {
            foreach (GridViewRow row in this.SettingsGrid.Rows)
            {
                string settingKey = row.Cells[0].Text;
                string localizedKey = Localization.GetString(settingKey, this.LocalResourceFile);
                row.Cells[0].Text = string.IsNullOrEmpty(localizedKey) ? settingKey : localizedKey;
            }
        }

        /// <summary>
        /// Displays the error message that the selected template's manifest does not pass validation
        /// </summary>
        private void ShowManifestValidationErrorMessage()
        {
            this.ManifestValidationErrorsPanel.Controls.Add(new LiteralControl("<ul><li>" + Localization.GetString("ManifestValidation", this.LocalResourceFile) + "</li></ul>"));
            this.TemplateDescriptionPanel.Visible = false;
            this.TemplatePreviewImage.Visible = false;
        }
    }
}