//Copyright (c) 2004-2008
//by Engage Software ( http://www.engagesoftware.net )

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
//TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
//THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
//CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
//DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.Hosting;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Globals=DotNetNuke.Common.Globals;
using System.Web.UI;

namespace Engage.Dnn.ContentRotator
{
	public partial class RotatorSettings : PortalModuleBase
	{
        private const string DisabledTextBoxCssClass = "disabledTextBox";

	    protected void Page_Init(object sender, EventArgs e)
        {
            if (DotNetNuke.Framework.AJAX.IsInstalled())
            {
                DotNetNuke.Framework.AJAX.RegisterScriptManager();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        protected void Page_Load(object sender, EventArgs e)
		{
			try 
			{
                if (!Page.IsPostBack)
                {
                    rblPositionTitleDisplay.Items.Clear();
                    rblPositionTitleDisplay.Items.Add(new ListItem(Localization.GetString(DisplayType.None.ToString(), LocalResourceFile), DisplayType.None.ToString()));
                    rblPositionTitleDisplay.Items.Add(new ListItem(Localization.GetString(DisplayType.Content.ToString(), LocalResourceFile), DisplayType.Content.ToString()));
                    rblPositionTitleDisplay.Items.Add(new ListItem(Localization.GetString(DisplayType.Link.ToString(), LocalResourceFile), DisplayType.Link.ToString()));
                    rblPositionTitleDisplay.Items.Add(new ListItem(Localization.GetString(DisplayType.RotateContent.ToString(), LocalResourceFile), DisplayType.RotateContent.ToString()));

                    rblPositionThumbnailDisplay.Items.Clear();
                    rblPositionThumbnailDisplay.Items.Add(new ListItem(Localization.GetString(DisplayType.None.ToString(), LocalResourceFile), DisplayType.None.ToString()));
                    rblPositionThumbnailDisplay.Items.Add(new ListItem(Localization.GetString(DisplayType.Content.ToString(), LocalResourceFile), DisplayType.Content.ToString()));
                    rblPositionThumbnailDisplay.Items.Add(new ListItem(Localization.GetString(DisplayType.Link.ToString(), LocalResourceFile), DisplayType.Link.ToString()));
                    rblPositionThumbnailDisplay.Items.Add(new ListItem(Localization.GetString(DisplayType.RotateContent.ToString(), LocalResourceFile), DisplayType.RotateContent.ToString()));

                    rblContentTitleDisplay.Items.Clear();
                    rblContentTitleDisplay.Items.Add(new ListItem(Localization.GetString(DisplayType.None.ToString(), LocalResourceFile), DisplayType.None.ToString()));
                    rblContentTitleDisplay.Items.Add(new ListItem(Localization.GetString(DisplayType.Content.ToString(), LocalResourceFile), DisplayType.Content.ToString()));
                    rblContentTitleDisplay.Items.Add(new ListItem(Localization.GetString(DisplayType.Link.ToString(), LocalResourceFile), DisplayType.Link.ToString()));

                    rblContentDisplay.Items.Clear();
                    rblContentDisplay.Items.Add(new ListItem(Localization.GetString(DisplayType.None.ToString(), LocalResourceFile), DisplayType.None.ToString()));
                    rblContentDisplay.Items.Add(new ListItem(Localization.GetString(DisplayType.Content.ToString(), LocalResourceFile), DisplayType.Content.ToString()));
                    rblContentDisplay.Items.Add(new ListItem(Localization.GetString(DisplayType.Link.ToString(), LocalResourceFile), DisplayType.Link.ToString()));

                    rblThumbnailDisplay.Items.Clear();
                    rblThumbnailDisplay.Items.Add(new ListItem(Localization.GetString(DisplayType.None.ToString(), LocalResourceFile), DisplayType.None.ToString()));
                    rblThumbnailDisplay.Items.Add(new ListItem(Localization.GetString(DisplayType.Content.ToString(), LocalResourceFile), DisplayType.Content.ToString()));
                    rblThumbnailDisplay.Items.Add(new ListItem(Localization.GetString(DisplayType.Link.ToString(), LocalResourceFile), DisplayType.Link.ToString()));

                    ddlPreviousButtonLocation.Items.Clear();
                    ddlPreviousButtonLocation.Items.Add(new ListItem(Localization.GetString(ControlPlacement.Above.ToString(), LocalResourceFile), ControlPlacement.Above.ToString()));
                    ddlPreviousButtonLocation.Items.Add(new ListItem(Localization.GetString(ControlPlacement.Below.ToString(), LocalResourceFile), ControlPlacement.Below.ToString()));
                    
                    ddlPauseButtonLocation.Items.Clear();
                    ddlPauseButtonLocation.Items.Add(new ListItem(Localization.GetString(ControlPlacement.Above.ToString(), LocalResourceFile), ControlPlacement.Above.ToString()));
                    ddlPauseButtonLocation.Items.Add(new ListItem(Localization.GetString(ControlPlacement.Below.ToString(), LocalResourceFile), ControlPlacement.Below.ToString()));
                    
                    ddlNextButtonLocation.Items.Clear();
                    ddlNextButtonLocation.Items.Add(new ListItem(Localization.GetString(ControlPlacement.Above.ToString(), LocalResourceFile), ControlPlacement.Above.ToString()));
                    ddlNextButtonLocation.Items.Add(new ListItem(Localization.GetString(ControlPlacement.Below.ToString(), LocalResourceFile), ControlPlacement.Below.ToString()));

                    ddlPositionCounterLocation.Items.Clear();
                    ddlPositionCounterLocation.Items.Add(new ListItem(Localization.GetString(ControlPlacement.Above.ToString(), LocalResourceFile), ControlPlacement.Above.ToString()));
                    ddlPositionCounterLocation.Items.Add(new ListItem(Localization.GetString(ControlPlacement.Below.ToString(), LocalResourceFile), ControlPlacement.Below.ToString()));

                    ddlStyleTemplates.Items.Clear();
                    ddlStyleTemplates.Items.Add(new ListItem(Localization.GetString("None", LocalResourceFile), string.Empty));
                    foreach (string directory in Directory.GetDirectories(HostingEnvironment.MapPath(Utility.DesktopModuleVirtualPath + Utility.StyleTemplatesFolderName)))
                    {
                        ddlStyleTemplates.Items.Add(new ListItem(directory.Substring(directory.LastIndexOf(Path.DirectorySeparatorChar) + 1)));
                    }

                    txtRotatorWidth.Text = RotatorWidth.HasValue ? RotatorWidth.Value.ToString(CultureInfo.CurrentCulture) : string.Empty;
                    txtRotatorHeight.Text = RotatorHeight.HasValue ? RotatorHeight.Value.ToString(CultureInfo.CurrentCulture) : string.Empty;
                    //txtControlsMarginLeft.Text = ControlsMarginLeft.HasValue ? ControlsMarginLeft.Value.ToString(CultureInfo.CurrentCulture) : string.Empty;
                    //txtControlsMarginTop.Text = ControlsMarginTop.HasValue ? ControlsMarginTop.Value.ToString(CultureInfo.CurrentCulture) : string.Empty;

                    rblPositionTitleDisplay.SelectedValue = PositionTitleDisplayMode.ToString();
                    rblPositionThumbnailDisplay.SelectedValue = PositionThumbnailDisplayMode.ToString();
                    rblContentTitleDisplay.SelectedValue = ContentTitleDisplayMode.ToString();

                    rblContentDisplay.SelectedValue = ContentDisplayMode.ToString();
                    txtContentWidth.Text = ContentWidth.HasValue ? ContentWidth.Value.ToString(CultureInfo.CurrentCulture) : string.Empty;
                    txtContentHeight.Text = ContentHeight.HasValue ? ContentHeight.Value.ToString(CultureInfo.CurrentCulture) : string.Empty;
                    ProcessContentVisibility();

                    rblThumbnailDisplay.SelectedValue = ThumbnailDisplayMode.ToString();
                    txtThumbnailWidth.Text = ThumbnailWidth.HasValue ? ThumbnailWidth.Value.ToString(CultureInfo.CurrentCulture) : string.Empty;
                    txtThumbnailHeight.Text = ThumbnailHeight.HasValue ? ThumbnailHeight.Value.ToString(CultureInfo.CurrentCulture) : string.Empty;
                    ProcessThumbnailVisibility();

                    //chkShowPositionThumbnail.Checked = ShowPositionThumbnail;
                    txtPositionThumbnailWidth.Text = PositionThumbnailWidth.HasValue ? PositionThumbnailWidth.Value.ToString(CultureInfo.CurrentCulture) : string.Empty;
                    txtPositionThumbnailHeight.Text = PositionThumbnailHeight.HasValue ? PositionThumbnailHeight.Value.ToString(CultureInfo.CurrentCulture) : string.Empty;
                    ProcessPositionThumbnailVisibility();

                    chkShowContentHeaderTitle.Checked = ShowContentHeader;
                    txtContentHeaderTitle.Text = ContentHeaderText;
                    ProcessContentHeaderVisiblity();

                    chkShowContentHeaderLink.Checked = ShowContentHeaderLink;
                    txtContentHeaderLinkText.Text = ContentHeaderLinkText;
                    urlContentHeaderLink.Url = ContentHeaderLink;
                    //Show tabs if there is no url, show as a url if there is anything.  BD
                    urlContentHeaderLink.UrlType = string.IsNullOrEmpty(ContentHeaderLink) ? "T" : "U";
                    ProcessContentHeaderLinkVisiblity();
                    
                    chkPauseOnMouseOver.Checked = PauseOnMouseOver;
                    txtRotatorDelay.Text = RotatorDelay.ToString(CultureInfo.CurrentCulture);
                    txtRotatorPauseDelay.Text = RotatorPauseDelay.ToString(CultureInfo.CurrentCulture);
                    ProcessMouseOverVisibility();

                    chkUseAnimations.Checked = UseAnimations;
                    txtAnimationFramesPerSecond.Text = AnimationFramesPerSecond.ToString(CultureInfo.CurrentCulture);
                    txtAnimationDuration.Text = AnimationDuration.ToString(CultureInfo.CurrentCulture);
                    chkPauseOnMouseOver.Checked = PauseOnMouseOver;
                    ProcessAnimationsVisiblity();

                    chkShowReadMoreLink.Checked = ShowReadMoreLink;
                    chkShowPreviousButton.Checked = ShowPreviousButton;
                    ddlPreviousButtonLocation.SelectedValue = PreviousButtonPlacement.ToString();
                    ProcessPreviousVisibility();
                    chkShowPauseButton.Checked = ShowPauseButton;
                    ddlPauseButtonLocation.SelectedValue = PauseButtonPlacement.ToString();
                    ProcessPauseVisibility();
                    chkShowNextButton.Checked = ShowNextButton;
                    ddlNextButtonLocation.SelectedValue = NextButtonPlacement.ToString();
                    ProcessNextVisibility();
                    chkShowPositionCounter.Checked = ShowPositionCounter;
                    ddlPositionCounterLocation.SelectedValue = PositionCounterPlacement.ToString();
                    ProcessPositionCounterVisibility();

                    ddlStyleTemplates.SelectedValue = StyleTemplate;
                    ddlStyleTemplates.Attributes.Add("OriginalStyleTemplate", StyleTemplate);
                    FillTemplateTab();
                }
            } 
			catch (Exception exc) 
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}

	    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ModuleController modules = new ModuleController();
                modules.UpdateTabModuleSetting(TabModuleId, "RotatorWidth", txtRotatorWidth.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "RotatorHeight", txtRotatorHeight.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "ContentWidth", txtContentWidth.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "ContentHeight", txtContentHeight.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "ThumbnailWidth", txtThumbnailWidth.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "ThumbnailHeight", txtThumbnailHeight.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "PositionThumbnailWidth", txtPositionThumbnailWidth.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "PositionThumbnailHeight", txtPositionThumbnailHeight.Text);
                //modules.UpdateTabModuleSetting(TabModuleId, "ControlsMarginTop", txtControlsMarginTop.Text);
                //modules.UpdateTabModuleSetting(TabModuleId, "ControlsMarginLeft", txtControlsMarginLeft.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "RotatorDelay", txtRotatorDelay.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "RotatorPauseDelay", txtRotatorPauseDelay.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "AnimationFramesPerSecond", txtAnimationFramesPerSecond.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "AnimationDuration", txtAnimationDuration.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "ContentHeaderText", txtContentHeaderTitle.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "ContentHeaderLink", Engage.Dnn.Utility.CreateUrlFromControl(urlContentHeaderLink, PortalSettings));
                modules.UpdateTabModuleSetting(TabModuleId, "ContentHeaderLinkText", txtContentHeaderLinkText.Text);

                modules.UpdateTabModuleSetting(TabModuleId, "ControlsTitleDisplayMode", rblPositionTitleDisplay.SelectedValue);
                modules.UpdateTabModuleSetting(TabModuleId, "ContentTitleDisplayMode", rblContentTitleDisplay.SelectedValue);
                modules.UpdateTabModuleSetting(TabModuleId, "ContentDisplayMode", rblContentDisplay.SelectedValue);
                modules.UpdateTabModuleSetting(TabModuleId, "ThumbnailDisplayMode", rblThumbnailDisplay.SelectedValue);

                modules.UpdateTabModuleSetting(TabModuleId, "ShowContentHeader", chkShowContentHeaderTitle.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(TabModuleId, "ShowContentHeaderLink", chkShowContentHeaderLink.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(TabModuleId, "ShowReadMoreLink", chkShowReadMoreLink.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(TabModuleId, "ControlsShowPrevButton", chkShowPreviousButton.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(TabModuleId, "ControlsShowPauseButton", chkShowPauseButton.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(TabModuleId, "ControlsShowNextButton", chkShowNextButton.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(TabModuleId, "ControlsPreviousPlacement", ddlPreviousButtonLocation.SelectedValue);
                modules.UpdateTabModuleSetting(TabModuleId, "ControlsPausePlacement", ddlPauseButtonLocation.SelectedValue);
                modules.UpdateTabModuleSetting(TabModuleId, "ControlsNextPlacement", ddlNextButtonLocation.SelectedValue);
                //modules.UpdateTabModuleSetting(TabModuleId, "ControlsPreviousImageUrl", Engage.Dnn.Utility.CreateUrlFromControl(urlPreviousButtonImage, PortalSettings));
                //modules.UpdateTabModuleSetting(TabModuleId, "ControlsPauseImageUrl", Engage.Dnn.Utility.CreateUrlFromControl(urlPauseButtonImage, PortalSettings));
                //modules.UpdateTabModuleSetting(TabModuleId, "ControlsNextImageUrl", Engage.Dnn.Utility.CreateUrlFromControl(urlNextButtonImage, PortalSettings));
                modules.UpdateTabModuleSetting(TabModuleId, "PositionThumbnailDisplayMode", rblPositionThumbnailDisplay.SelectedValue);
                modules.UpdateTabModuleSetting(TabModuleId, "UseAnimations", chkUseAnimations.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(TabModuleId, "AnimationPauseOnMouseOver", chkPauseOnMouseOver.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(TabModuleId, "ShowPositionCounter", chkShowPositionCounter.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(TabModuleId, "PositionCounterPlacement", ddlPositionCounterLocation.SelectedValue);
                Response.Redirect(Globals.NavigateURL((TabId)), false);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void btnApplyStyleTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                ModuleController modules = new ModuleController();
                modules.UpdateTabModuleSetting(TabModuleId, "StyleTemplate", ddlStyleTemplates.SelectedValue);

                try
                {
                    TemplateManifest manifest = TemplateManifest.CreateTemplateManifest(ddlStyleTemplates.SelectedValue);
                    if (manifest.Settings != null && manifest.Settings.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> setting in manifest.Settings)
                        {
                            modules.UpdateTabModuleSetting(TabModuleId, setting.Key, setting.Value);
                        }
                    }
                    //return to this page with the new settings applied
                    Response.Redirect(EditUrl("ModSettings"), false);
                }
                catch (XmlSchemaValidationException)
                {
                    ShowManifestValidationErrorMessage();
                }
                catch (XmlException)
                {
                    ShowManifestValidationErrorMessage();
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

	    private void ShowManifestValidationErrorMessage()
	    {
	        pnlManifestValidation.Controls.Add(new LiteralControl("<ul><li>" + Localization.GetString("ManifestValidation", LocalResourceFile) + "</li></ul>"));
            pnlStyleDescription.Visible = false;
            imgStylePreview.Visible = false;
            btnApplyStyleTemplate.Enabled = false;
	    }

	    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL((TabId)), false);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void valContentHeaderLink_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (e != null)
            {
                //valid if there is a url, or we aren't using the header link.
                //the validator should be disabled if chkShowContentHeaderLink is unchecked, this is just a doublecheck.  BD
                e.IsValid = !string.IsNullOrEmpty(urlContentHeaderLink.Url) || !chkShowContentHeaderLink.Checked;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void rblPositionThumbnailDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcessPositionThumbnailVisibility();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void rblContentDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcessContentVisibility();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void rblThumbnailDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcessThumbnailVisibility();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void chkPauseOnMouseOver_CheckedChanged(object sender, EventArgs e)
        {
            ProcessMouseOverVisibility();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void chkUseAnimations_CheckedChanged(object sender, EventArgs e)
        {
            ProcessAnimationsVisiblity();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void chkShowContentHeaderTitle_CheckedChanged(object sender, EventArgs e)
        {
            ProcessContentHeaderVisiblity();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void chkShowContentHeaderLink_CheckedChanged(object sender, EventArgs e)
        {
            ProcessContentHeaderLinkVisiblity();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void chkShowPreviousButton_CheckedChanged(object sender, EventArgs e)
        {
            ProcessPreviousVisibility();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void chkShowPauseButton_CheckedChanged(object sender, EventArgs e)
        {
            ProcessPauseVisibility();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void chkShowNextButton_CheckedChanged(object sender, EventArgs e)
        {
            ProcessNextVisibility();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void chkShowPositionCounter_CheckedChanged(object sender, EventArgs e)
        {
            ProcessPositionCounterVisibility();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void ddlStyleTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStyleTemplates.Attributes["OriginalStyleTemplate"] != ddlStyleTemplates.SelectedValue)
            {
                ClientAPI.AddButtonConfirm(btnSubmit, Localization.GetString("TemplateChangedConfirm", LocalResourceFile));
            }
            FillTemplateTab();
        }

	    private void FillTemplateTab()
	    {
            try
            {
                btnApplyStyleTemplate.Enabled = true;
                TemplateManifest manifest = TemplateManifest.CreateTemplateManifest(ddlStyleTemplates.SelectedValue);
                lblStyleDescription.Text = manifest.Description;
                pnlStyleDescription.Visible = Engage.Utility.HasValue(lblStyleDescription.Text);
                string templateFolder = Utility.DesktopModuleVirtualPath + Utility.StyleTemplatesFolderName + ddlStyleTemplates.SelectedValue;
                imgStylePreview.ImageUrl = templateFolder + "/" + manifest.PreviewImageFilename;
                imgStylePreview.Visible = File.Exists(HostingEnvironment.MapPath(imgStylePreview.ImageUrl));
            }
            catch (XmlSchemaValidationException)
            {
                ShowManifestValidationErrorMessage();
            }
	    }

	    private void ProcessPositionThumbnailVisibility()
        {
            DisplayType positionThumbnailDisplayMode = (DisplayType)Enum.Parse(typeof(DisplayType), rblPositionThumbnailDisplay.SelectedValue, true);
            txtPositionThumbnailHeight.Enabled = valPositionThumbnailHeight.Enabled = txtPositionThumbnailWidth.Enabled = valPositionThumbnailWidth.Enabled = positionThumbnailDisplayMode != DisplayType.None;

            txtPositionThumbnailHeight.CssClass = !txtPositionThumbnailHeight.Enabled ? Engage.Utility.AddCssClass(txtPositionThumbnailHeight.CssClass, DisabledTextBoxCssClass) : Engage.Utility.RemoveCssClass(txtPositionThumbnailHeight.CssClass, DisabledTextBoxCssClass);
            txtPositionThumbnailWidth.CssClass = !txtPositionThumbnailWidth.Enabled ? Engage.Utility.AddCssClass(txtPositionThumbnailWidth.CssClass, DisabledTextBoxCssClass) : Engage.Utility.RemoveCssClass(txtPositionThumbnailWidth.CssClass, DisabledTextBoxCssClass);
        }

        private void ProcessContentVisibility()
        {
            txtContentHeight.Enabled = rblContentDisplay.SelectedValue != DisplayType.None.ToString();
            valContentHeight.Enabled = txtContentHeight.Enabled;

            txtContentWidth.Enabled = rblContentDisplay.SelectedValue != DisplayType.None.ToString();
            valContentWidth.Enabled = txtContentWidth.Enabled;

            txtContentHeight.CssClass = !txtContentHeight.Enabled ? Engage.Utility.AddCssClass(txtContentHeight.CssClass, DisabledTextBoxCssClass) : Engage.Utility.RemoveCssClass(txtContentHeight.CssClass, DisabledTextBoxCssClass);
            txtContentWidth.CssClass = !txtContentWidth.Enabled ? Engage.Utility.AddCssClass(txtContentWidth.CssClass, DisabledTextBoxCssClass) : Engage.Utility.RemoveCssClass(txtContentWidth.CssClass, DisabledTextBoxCssClass);
        }

        private void ProcessThumbnailVisibility()
        {
            txtThumbnailHeight.Enabled = rblThumbnailDisplay.SelectedValue != DisplayType.None.ToString();
            valThumbnailHeight.Enabled = txtThumbnailHeight.Enabled;

            txtThumbnailWidth.Enabled = rblThumbnailDisplay.SelectedValue != DisplayType.None.ToString();
            valThumbnailWidth.Enabled = txtThumbnailWidth.Enabled;

            txtThumbnailHeight.CssClass = !txtThumbnailHeight.Enabled ? Engage.Utility.AddCssClass(txtThumbnailHeight.CssClass, DisabledTextBoxCssClass) : Engage.Utility.RemoveCssClass(txtThumbnailHeight.CssClass, DisabledTextBoxCssClass);
            txtThumbnailWidth.CssClass = !txtThumbnailWidth.Enabled ? Engage.Utility.AddCssClass(txtThumbnailWidth.CssClass, DisabledTextBoxCssClass) : Engage.Utility.RemoveCssClass(txtThumbnailWidth.CssClass, DisabledTextBoxCssClass);
        }

        private void ProcessMouseOverVisibility()
        {
            txtRotatorPauseDelay.Enabled = chkPauseOnMouseOver.Checked;
            valRotatorPauseDelay.Enabled = chkPauseOnMouseOver.Checked;
            rfvRotatorPauseDelay.Enabled = chkPauseOnMouseOver.Checked;

            txtRotatorPauseDelay.CssClass = !txtRotatorPauseDelay.Enabled ? Engage.Utility.AddCssClass(txtRotatorPauseDelay.CssClass, DisabledTextBoxCssClass) : Engage.Utility.RemoveCssClass(txtRotatorPauseDelay.CssClass, DisabledTextBoxCssClass);
        }

        private void ProcessAnimationsVisiblity()
        {
            txtAnimationFramesPerSecond.Enabled = chkUseAnimations.Checked;
            valAnimationFramesPerSecond.Enabled = chkUseAnimations.Checked;
            rfvAnimationFramesPerSecond.Enabled = chkUseAnimations.Checked;

            txtAnimationDuration.Enabled = chkUseAnimations.Checked;
            valAnimationDuration.Enabled = chkUseAnimations.Checked;
            rfvAnimationDuration.Enabled = chkUseAnimations.Checked;

            txtAnimationFramesPerSecond.CssClass = !txtAnimationFramesPerSecond.Enabled ? Engage.Utility.AddCssClass(txtAnimationFramesPerSecond.CssClass, DisabledTextBoxCssClass) : Engage.Utility.RemoveCssClass(txtAnimationFramesPerSecond.CssClass, DisabledTextBoxCssClass);
            txtAnimationDuration.CssClass = !txtAnimationDuration.Enabled ? Engage.Utility.AddCssClass(txtAnimationDuration.CssClass, DisabledTextBoxCssClass) : Engage.Utility.RemoveCssClass(txtAnimationDuration.CssClass, DisabledTextBoxCssClass);
        }

        private void ProcessContentHeaderVisiblity()
        {
            txtContentHeaderTitle.Enabled = chkShowContentHeaderTitle.Checked;
            rfvContentHeaderTitle.Enabled = chkShowContentHeaderTitle.Checked;

            txtContentHeaderTitle.CssClass = !txtContentHeaderTitle.Enabled ? Engage.Utility.AddCssClass(txtContentHeaderTitle.CssClass, DisabledTextBoxCssClass) : Engage.Utility.RemoveCssClass(txtContentHeaderTitle.CssClass, DisabledTextBoxCssClass);
        }

        private void ProcessContentHeaderLinkVisiblity()
        {
            txtContentHeaderLinkText.Enabled = chkShowContentHeaderLink.Checked;
            rfvContentHeaderLinkText.Enabled = chkShowContentHeaderLink.Checked;

            urlContentHeaderLink.Visible = chkShowContentHeaderLink.Checked;
            valContentHeaderLink.Enabled = chkShowContentHeaderLink.Checked;

            txtContentHeaderLinkText.CssClass = !txtContentHeaderLinkText.Enabled ? Engage.Utility.AddCssClass(txtContentHeaderLinkText.CssClass, DisabledTextBoxCssClass) : Engage.Utility.RemoveCssClass(txtContentHeaderLinkText.CssClass, DisabledTextBoxCssClass);
        }

        private void ProcessPreviousVisibility()
        {
            ddlPreviousButtonLocation.Enabled = chkShowPreviousButton.Checked;
            //urlPreviousButtonImage.Visible = chkShowPreviousButton.Checked;

            ddlPreviousButtonLocation.CssClass = !ddlPreviousButtonLocation.Enabled ? Engage.Utility.AddCssClass(ddlPreviousButtonLocation.CssClass, DisabledTextBoxCssClass) : Engage.Utility.RemoveCssClass(ddlPreviousButtonLocation.CssClass, DisabledTextBoxCssClass);
        }

	    private void ProcessPauseVisibility()
	    {
	        ddlPauseButtonLocation.Enabled = chkShowPauseButton.Checked;
            //urlPauseButtonImage.Visible = chkShowPauseButton.Checked;

	        ddlPauseButtonLocation.CssClass = !ddlPauseButtonLocation.Enabled ? Engage.Utility.AddCssClass(ddlPauseButtonLocation.CssClass, DisabledTextBoxCssClass) : Engage.Utility.RemoveCssClass(ddlPauseButtonLocation.CssClass, DisabledTextBoxCssClass);
	    }

        private void ProcessNextVisibility()
        {
            ddlNextButtonLocation.Enabled = chkShowNextButton.Checked;
            //urlNextButtonImage.Visible = chkShowNextButton.Checked;

            ddlNextButtonLocation.CssClass = !ddlNextButtonLocation.Enabled ? Engage.Utility.AddCssClass(ddlNextButtonLocation.CssClass, DisabledTextBoxCssClass) : Engage.Utility.RemoveCssClass(ddlNextButtonLocation.CssClass, DisabledTextBoxCssClass);
        }

        private void ProcessPositionCounterVisibility()
        {
            ddlPositionCounterLocation.Enabled = chkShowPositionCounter.Checked;
            ddlPositionCounterLocation.CssClass = !ddlPositionCounterLocation.Enabled ? Engage.Utility.AddCssClass(ddlPositionCounterLocation.CssClass, DisabledTextBoxCssClass) : Engage.Utility.RemoveCssClass(ddlPositionCounterLocation.CssClass, DisabledTextBoxCssClass);
        }

        #region Settings
        protected int? RotatorWidth
        {
            get { return Engage.Dnn.Utility.GetIntSetting(Settings, "RotatorWidth"); }
        }

        protected int? RotatorHeight
        {
            get { return Engage.Dnn.Utility.GetIntSetting(Settings, "RotatorHeight"); }
        }

        protected int? ContentWidth
        {
            get { return Engage.Dnn.Utility.GetIntSetting(Settings, "ContentWidth"); }
        }

        protected int? ContentHeight
        {
            get { return Engage.Dnn.Utility.GetIntSetting(Settings, "ContentHeight"); }
        }

        protected int? ThumbnailWidth
        {
            get { return Engage.Dnn.Utility.GetIntSetting(Settings, "ThumbnailWidth"); }
        }

        protected int? ThumbnailHeight
        {
            get { return Engage.Dnn.Utility.GetIntSetting(Settings, "ThumbnailHeight"); }
        }

        protected int? PositionThumbnailWidth
        {
            get { return Engage.Dnn.Utility.GetIntSetting(Settings, "PositionThumbnailWidth"); }
        }

        protected int? PositionThumbnailHeight
        {
            get { return Engage.Dnn.Utility.GetIntSetting(Settings, "PositionThumbnailHeight"); }
        }

        //protected int? ControlsMarginTop
        //{
        //    get
        //    {
        //        return Utility.GetIntSetting(Settings, "ControlsMarginTop");
        //    }
        //}

        //protected int? ControlsMarginLeft
        //{
        //    get
        //    {
        //        return Utility.GetIntSetting(Settings, "ControlsMarginLeft");
        //    }
        //}

        protected int RotatorDelay
        {
            get { return Engage.Dnn.Utility.GetIntSetting(Settings, "RotatorDelay", 8); }
        }

        protected int RotatorPauseDelay
        {
            get { return Engage.Dnn.Utility.GetIntSetting(Settings, "RotatorPauseDelay", 3); }
        }

        protected int AnimationFramesPerSecond
        {
            get { return Engage.Dnn.Utility.GetIntSetting(Settings, "AnimationFramesPerSecond", 30); }
        }

        protected decimal AnimationDuration
        {
            get { return Engage.Dnn.Utility.GetDecimalSetting(Settings, "AnimationDuration", 0.3m); }
        }

        protected DisplayType PositionTitleDisplayMode
        {
            get { return Engage.Dnn.Utility.GetEnumSetting(Settings, "ControlsTitleDisplayMode", DisplayType.Link); }
        }

        protected DisplayType PositionThumbnailDisplayMode
        {
            get { return Engage.Dnn.Utility.GetEnumSetting(Settings, "PositionThumbnailDisplayMode", DisplayType.Link); }
        }

        protected DisplayType ContentTitleDisplayMode
        {
            get { return Engage.Dnn.Utility.GetEnumSetting(Settings, "ContentTitleDisplayMode", DisplayType.Link); }
        }

        protected DisplayType ContentDisplayMode
        {
            get { return Engage.Dnn.Utility.GetEnumSetting(Settings, "ContentDisplayMode", DisplayType.Content); }
        }

        protected DisplayType ThumbnailDisplayMode
        {
            get { return Engage.Dnn.Utility.GetEnumSetting(Settings, "ThumbnailDisplayMode", DisplayType.Link); }
        }

        protected bool ShowContentHeader
        {
            get { return Engage.Dnn.Utility.GetBoolSetting(Settings, "ShowContentHeader", false); }
        }

        protected string ContentHeaderText
        {
            get { return Settings["ContentHeaderText"] as string; }
        }

        protected bool ShowContentHeaderLink
        {
            get { return Engage.Dnn.Utility.GetBoolSetting(Settings, "ShowContentHeaderLink", false); }
        }

        protected string ContentHeaderLink
        {
            get { return Settings["ContentHeaderLink"] as string; }
        }

        protected string ContentHeaderLinkText
        {
            get { return Settings["ContentHeaderLinkText"] as string; }
        }

        protected bool ShowReadMoreLink
        {
            get { return Engage.Dnn.Utility.GetBoolSetting(Settings, "ShowReadMoreLink", false); }
        }

        protected bool ShowPreviousButton
        {
            get { return Engage.Dnn.Utility.GetBoolSetting(Settings, "ControlsShowPrevButton", true); }
        }

        protected ControlPlacement PreviousButtonPlacement
        {
            get { return Engage.Dnn.Utility.GetEnumSetting(Settings, "ControlsPreviousPlacement", ControlPlacement.Above); }
        }

        //protected string PreviousButtonImageUrl
        //{
        //    get
        //    {
        //        //if setting is null, then return the path to the provided image
        //        return Settings["ControlsPreviousImageUrl"] as string ?? ResolveUrl("~/DesktopModules/EngageRotator/images/previous.gif");
        //    }
        //}

        protected bool ShowPauseButton
        {
            get { return Engage.Dnn.Utility.GetBoolSetting(Settings, "ControlsShowPauseButton", true); }
        }

        protected ControlPlacement PauseButtonPlacement
        {
            get { return Engage.Dnn.Utility.GetEnumSetting(Settings, "ControlsPausePlacement", ControlPlacement.Above); }
        }

        //protected string PauseButtonImageUrl
        //{
        //    get
        //    {
        //        //if setting is null, then return the path to the provided image
        //        return Settings["ControlsPauseImageUrl"] as string ?? ResolveUrl("~/DesktopModules/EngageRotator/images/pause.gif");
        //    }
        //}
        
        protected bool ShowNextButton
        {
            get { return Engage.Dnn.Utility.GetBoolSetting(Settings, "ControlsShowNextButton", true); }
        }

        protected ControlPlacement NextButtonPlacement
        {
            get { return Engage.Dnn.Utility.GetEnumSetting(Settings, "ControlsNextPlacement", ControlPlacement.Below); }
        }

        //protected string NextButtonImageUrl
        //{
        //    get
        //    {
        //        //if setting is null, then return the path to the provided image
        //        return Settings["ControlsNextImageUrl"] as string ?? ResolveUrl("~/DesktopModules/EngageRotator/images/next.gif");
        //    }
        //}

        protected bool ShowPositionThumbnail
        {
            get { return Engage.Dnn.Utility.GetBoolSetting(Settings, "ShowPositionThumbnail", true); }
        }

        protected bool UseAnimations
        {
            get { return Engage.Dnn.Utility.GetBoolSetting(Settings, "UseAnimations", true); }
        }

        protected bool PauseOnMouseOver
        {
            get { return Engage.Dnn.Utility.GetBoolSetting(Settings, "AnimationPauseOnMouseOver", true); }
        }

        private string StyleTemplate
        {
            get { return Settings["StyleTemplate"] as string; }
        }

        private bool ShowPositionCounter
        {
            get { return Engage.Dnn.Utility.GetBoolSetting(Settings, "ShowPositionCounter", false); }
        }

        protected ControlPlacement PositionCounterPlacement
        {
            get { return Engage.Dnn.Utility.GetEnumSetting(Settings, "PositionCounterPlacement", ControlPlacement.Below); }
        }
        #endregion

    }
}