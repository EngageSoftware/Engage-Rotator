//Copyright (c) 2004-2008
//by Engage Software ( http://www.engagesoftware.net )

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
//TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
//THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
//CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
//DEALINGS IN THE SOFTWARE.

using System;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using AjaxControlToolkit;

namespace Engage.Dnn.ContentRotator
{
	public partial class RotatorOptions : PortalModuleBase
	{
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        protected void Page_Load(object sender, EventArgs e)
		{
			try 
			{
                if (DotNetNuke.Framework.AJAX.IsInstalled())
                {
                    DotNetNuke.Framework.AJAX.RegisterScriptManager();
                }
                if (!Page.IsPostBack)
                {
                    BindData();
                }
			} 
			catch (Exception exc) 
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(TabId));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void btnNewContentItem_Click(object sender, EventArgs e)
        {
            Response.Redirect(EditUrl("Edit"), false);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void rpContentItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e != null)
            {
                ConfirmButtonExtender ajaxConfirm = (ConfirmButtonExtender)e.Item.FindControl("ajaxConfirm");
                if (ajaxConfirm != null)
                {
                    ajaxConfirm.ConfirmText = Localization.GetString(ajaxConfirm.ConfirmText, LocalResourceFile);
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void rpContentItems_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e != null)
            {
                if (e.CommandName == "Delete")
                {
                    DataProvider.Instance().DeleteContentItem(GetIdFromIndex(e.Item.ItemIndex));
                    BindData();
                }
                else if (e.CommandName == "Edit")
                {
                    Response.Redirect(EditUrl("Edit", string.Empty, string.Empty, "id=" + GetIdFromIndex(e.Item.ItemIndex).ToString(CultureInfo.InvariantCulture)));
                }
            }
        }

        private int GetIdFromIndex(int rowIndex)
        {
            HiddenField hfContentItemId = (HiddenField)rpContentItems.Items[rowIndex].FindControl("hfContentItemId");
            return int.Parse(hfContentItemId.Value, CultureInfo.InvariantCulture);
        }

        private void BindData()
        {
            rpContentItems.DataSource = DataProvider.Instance().GetContentItems(TabModuleId, true);
            rpContentItems.DataBind();
        }

        protected static string TrimDescription(object descriptionValue)
        {
            string description = string.Empty;
            if (descriptionValue != null)
            {
                description = descriptionValue.ToString();
                if (description.Length > 250)
                {
                    description = description.Substring(0, 250) + "...";
                }
            }
            return description;
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

        protected int? PositionThumbnailWidth
        {
            get
            {
                return Engage.Dnn.Utility.GetIntSetting(Settings, "PositionThumbnailWidth");
            }
        }

        protected int? PositionThumbnailHeight
        {
            get
            {
                return Engage.Dnn.Utility.GetIntSetting(Settings, "PositionThumbnailHeight");
            }
        }

        protected int? ThumbnailWidth
        {
            get
            {
                return Engage.Dnn.Utility.GetIntSetting(Settings, "ThumbnailWidth");
            }
        }

        protected int? ThumbnailHeight
        {
            get
            {
                return Engage.Dnn.Utility.GetIntSetting(Settings, "ThumbnailHeight");
            }
        }
    }
}