//Copyright (c) 2004-2008
//by Engage Software ( http://www.engagesoftware.net )

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
//TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
//THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
//CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
//DEALINGS IN THE SOFTWARE.

using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Schema;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

namespace Engage.Dnn.ContentRotator
{
	public partial class Rotator : PortalModuleBase, IActionable
    {
        #region Event Handlers
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        protected void Page_Load(object sender, EventArgs e)
		{
			try 
			{
                string rotatorStyle = RotatorWrapperStyle;
                if (rotatorStyle.Length > 0)
                {
                    divRotator.Attributes.Add("style", rotatorStyle);
                }
			    InsertStyleTemplate();

                DataSet ds = DataProvider.Instance().GetContentItems(TabModuleId);
                if (ds.Tables[0].Rows.Count > 1)
                {
                    rpArticles.DataSource = ds.Tables[0];
                    rpPosition.DataSource = ds.Tables[0];

                    ViewState["TotalCount"] = ds.Tables[0].Rows.Count;
                }
                else
                {
                    lblNoHotTopics.Visible = true;
                    divRotator.Visible = false;
                }
                RegisterRotatorJavascript();
                this.DataBind();
			} 
			catch (Exception exc) 
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}

	    private void InsertStyleTemplate()
	    {
            try
            {
                TemplateManifest manifest = TemplateManifest.CreateTemplateManifest(Settings["StyleTemplate"] as string);

                if (Engage.Utility.HasValue(manifest.StylesheetFilePath))
                {
                    string stylesheetMapPath = HostingEnvironment.MapPath(manifest.StylesheetFilePath);
                    if (File.Exists(stylesheetMapPath))
                    {
                        string parentId = string.Empty;
                        Control visibleParent = this.Parent;
                        while (visibleParent != null && (!visibleParent.Visible || !Engage.Utility.HasValue(visibleParent.ClientID)))
                        {
                            visibleParent = visibleParent.Parent;
                        }
                        if (visibleParent != null)
                        {
                            parentId = visibleParent.ClientID;
                        }

                        string stylesheetContent = File.ReadAllText(stylesheetMapPath);
                        stylesheetContent = stylesheetContent.Replace("%parentId%", parentId);
                        foreach (Match match in Regex.Matches(stylesheetContent, @"url\(([^\)]*)\)", RegexOptions.ECMAScript))
                        {
                            if (match.Groups.Count > 1)
                            {
                                //create an absolute url with the request as the base, pointing to the stylesheet,
                                //and then the relative path from the stylesheet to whatever resource it's pointing to
                                Uri absoluteCaptureUrl = new Uri(new Uri(Request.Url, ResolveUrl(manifest.StylesheetFilePath)), match.Groups[1].Value);
                                stylesheetContent = stylesheetContent.Replace(match.Groups[0].Value, "url(" + absoluteCaptureUrl.ToString() + ")");
                            }
                        }
                        phStyleTemplate.Controls.Add(new LiteralControl("<style type=\"text/css\">"));
                        phStyleTemplate.Controls.Add(new LiteralControl(stylesheetContent));
                        phStyleTemplate.Controls.Add(new LiteralControl("</style>"));
                    }
                }
            }
            catch (XmlSchemaValidationException)
            {
                return;
            }
            catch (IOException)
            {
                return;
            }
	    }

	    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void rpArticles_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
            if (e != null)
            {
                //get the row
                //DataRowView item = (DataRowView)e.Item.DataItem;

                //find the control
                //Image imgThumbnail = (Image)e.Item.FindControl("imgThumbnail");
                //Label lblArticleSummary = (Label)e.Item.FindControl("lblArticleSummary");
                //HyperLink lnkReadMore = (HyperLink)e.Item.FindControl("lnkReadMore");
                //HyperLink lnkArticleTitle = (HyperLink)e.Item.FindControl("lnkArticleTitle");
                HtmlGenericControl divArticleToRotate = (HtmlGenericControl)e.Item.FindControl("divArticleToRotate");

                if (divArticleToRotate != null)
                {
                    if (e.Item.ItemIndex == 0)
                    {
                        divArticleToRotate.Style.Add("display", "block");
                    }
                    else
                    {
                        divArticleToRotate.Style.Add("display", "none");
                    }

                    if (PauseOnMouseOver)
                    {
                        divArticleToRotate.Attributes.Add("onmouseover", "rotator" + TabModuleId.ToString(CultureInfo.InvariantCulture) + ".StopTheClock();");
                        divArticleToRotate.Attributes.Add("onmouseout", "rotator" + TabModuleId.ToString(CultureInfo.InvariantCulture) + ".OnMouseOut();");
                    }
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void rpPosition_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e != null)
            {
                string rotatorOnClickCode = "rotator" + TabModuleId.ToString(CultureInfo.InvariantCulture) + ".Show(" + e.Item.ItemIndex.ToString(CultureInfo.InvariantCulture) + ");";
                if (PositionTitleDisplayMode == DisplayType.RotateContent)
                {
                    HtmlGenericControl positionTitle = (HtmlGenericControl)e.Item.FindControl("positionTitle");
                    positionTitle.Attributes.Add("onclick", rotatorOnClickCode);
                }
                if (PositionThumbnailDisplayMode == DisplayType.RotateContent)
                {
                    HtmlGenericControl positionThumbnail = (HtmlGenericControl)e.Item.FindControl("positionThumbnail");
                    positionThumbnail.Attributes.Add("onclick", rotatorOnClickCode);
                }
            }
        }
        #endregion

	    private void RegisterRotatorJavascript()
	    {
            if (UseAnimations && DotNetNuke.Framework.AJAX.IsInstalled())
            {
                DotNetNuke.Framework.AJAX.AddScriptManager(Page);
                //Add references to Animations scripts
                ScriptManager manager = (ScriptManager)DotNetNuke.Framework.AJAX.ScriptManagerControl(Page);
                manager.Scripts.Add(new ScriptReference("AjaxControlToolkit.ExtenderBase.BaseScripts.js", "AjaxControlToolkit"));
                manager.Scripts.Add(new ScriptReference("AjaxControlToolkit.Common.Common.js", "AjaxControlToolkit"));
                manager.Scripts.Add(new ScriptReference("AjaxControlToolkit.Animation.Animations.js", "AjaxControlToolkit"));
                manager.Scripts.Add(new ScriptReference("AjaxControlToolkit.Animation.AnimationBehavior.js", "AjaxControlToolkit"));
                manager.Scripts.Add(new ScriptReference("AjaxControlToolkit.Compat.Timer.Timer.js", "AjaxControlToolkit"));
            }

            if (!Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "rotator"))
            {
                Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "rotator", ResolveUrl("~/DesktopModules/EngageRotator/Rotator.js"));
            }
	    }

        protected static string FormatThumbnailUrl(string url)
        {
            //test the thumbnail path
            return Engage.Utility.HasValue(url) ? url : "/images/1x1.gif";
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

        //protected int? ControlsMarginTop
        //{
        //    get
        //    {
        //        return Engage.Dnn.Utility.GetIntSetting(Settings, "ControlsMarginTop");
        //    }
        //}

        //protected int? ControlsMarginLeft
        //{
        //    get
        //    {
        //        return Engage.Dnn.Utility.GetIntSetting(Settings, "ControlsMarginLeft");
        //    }
        //}

        protected int? PositionThumbnailWidth
        {
            get { return Engage.Dnn.Utility.GetIntSetting(Settings, "PositionThumbnailWidth"); }
        }

        protected int? PositionThumbnailHeight
        {
            get { return Engage.Dnn.Utility.GetIntSetting(Settings, "PositionThumbnailHeight"); }
        }

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

        protected bool ShowContentHeaderLink
        {
            get { return Engage.Dnn.Utility.GetBoolSetting(Settings, "ShowContentHeaderLink", false); }
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

        protected DisplayType PositionThumbnailDisplayMode
        {
            get { return Engage.Dnn.Utility.GetEnumSetting(Settings, "PositionThumbnailDisplayMode", DisplayType.Link); }
        }

        protected bool UseAnimations
        {
            get { return Engage.Dnn.Utility.GetBoolSetting(Settings, "UseAnimations", true); }
        }

        protected bool PauseOnMouseOver
        {
            get { return Engage.Dnn.Utility.GetBoolSetting(Settings, "AnimationPauseOnMouseOver", true); }
        }

        protected string RotatorWrapperStyle
        {
            get
            {
                StringBuilder style = new StringBuilder();
                Utility.AddStyle(style, "width", RotatorWidth);
                Utility.AddStyle(style, "height", RotatorHeight);
                return style.ToString();
            }
        }

        protected string ContentWrapperStyle
        {
            get
            {
                StringBuilder style = new StringBuilder();
                Utility.AddStyle(style, "width", ContentWidth);
                Utility.AddStyle(style, "height", ContentHeight);
                return style.ToString();
            }
        }

        protected string ThumbnailStyle
        {
            get
            {
                StringBuilder style = new StringBuilder();
                Utility.AddStyle(style, "width", ThumbnailWidth);
                Utility.AddStyle(style, "height", ThumbnailHeight);
                return style.ToString();
            }
        }

        protected string PositionThumbnailStyle
        {
            get
            {
                StringBuilder style = new StringBuilder();
                Utility.AddStyle(style, "width", PositionThumbnailWidth);
                Utility.AddStyle(style, "height", PositionThumbnailHeight);
                return style.ToString();
            }
        }

        //protected string ControlsWrapperStyle
        //{
        //    get
        //    {
        //        StringBuilder style = new StringBuilder();
        //        AddStyle(style, "margin-left", ControlsMarginLeft);
        //        AddStyle(style, "margin-top", ControlsMarginTop);
        //        return style.ToString();
        //    }
        //}

        protected string ContentHeaderText
        {
            get { return Settings["ContentHeaderText"] as string; }
        }

        protected string ContentHeaderLink
        {
            get { return Settings["ContentHeaderLink"] as string; }
        }

        protected string ContentHeaderLinkText
        {
            get { return Settings["ContentHeaderLinkText"] as string; }
        }

	    protected bool ShowPositionCounter
	    {
            get { return Engage.Dnn.Utility.GetBoolSetting(Settings, "ShowPositionCounter", false); }
	    }

        protected ControlPlacement PositionCounterPlacement
        {
            get { return Engage.Dnn.Utility.GetEnumSetting(Settings, "PositionCounterPlacement", ControlPlacement.Below); }
        }
        #endregion

        #region IActionable
        public ModuleActionCollection ModuleActions 
	    {
		    get 
		    {
			    ModuleActionCollection actions = new ModuleActionCollection();
			    actions.Add(GetNextActionID(), Localization.GetString("Add/Edit Content", LocalResourceFile), ModuleActionType.AddContent, "", "", EditUrl("Options"), false, SecurityAccessLevel.Edit, true, false);
                actions.Add(GetNextActionID(), Localization.GetString("Rotator Settings", LocalResourceFile), ModuleActionType.AddContent, "", "", EditUrl("ModSettings"), false, SecurityAccessLevel.Edit, true, false);
			    return actions;
		    }
        }

	    #endregion
    }
}