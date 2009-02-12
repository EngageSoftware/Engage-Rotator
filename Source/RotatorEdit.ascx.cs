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
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

namespace Engage.Dnn.ContentRotator
{
	public partial class RotatorEdit : PortalModuleBase
	{
        protected void Page_Init(object sender, EventArgs e)
        {
            //System.InvalidOperationException: The EnableScriptGlobalization property cannot be changed during async postbacks or after the Init event.
            if (!IsPostBack && DotNetNuke.Framework.AJAX.IsInstalled())
            {
                DotNetNuke.Framework.AJAX.RegisterScriptManager();
                DotNetNuke.Framework.AJAX.SetScriptManagerProperty(Page, "EnableScriptGlobalization", new object[] { true });
                DotNetNuke.Framework.AJAX.SetScriptManagerProperty(Page, "EnableScriptLocalization", new object[] { true });
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        protected void Page_Load(object sender, EventArgs e)
		{
			try 
			{
                if (!Page.IsPostBack)
                {
                    int contentItemId;
                    txtTitle.Focus();
                    if (int.TryParse(Request.QueryString["id"], out contentItemId))
                    {
                        LoadContentItem(contentItemId);

                        //set UrlControls to type Url, so we don't have to convert back to a tabId or fileId
                        urlLink.UrlType = "U";
                        urlThumbnail.UrlType = "U";
                        urlPositionThumbnail.UrlType = "U";
                    }
                    else
                    {
                        //lblId.Visible = false;
                        //lblIdLabel.Visible = false;

                        //lblId.Text = string.Empty;
                        txtDescription.Text = string.Empty;
                        urlThumbnail.Url = string.Empty;
                        urlThumbnail.UrlType = "F"; //file
                        urlPositionThumbnail.Url = string.Empty;
                        urlPositionThumbnail.UrlType = "F";
                        urlLink.Url = string.Empty;
                        urlLink.UrlType = "T"; //tab
                        txtStartDate.Text = DateTime.Today.ToShortDateString();
                        txtEndDate.Text = string.Empty;
                        txtTitle.Text = string.Empty;
                        txtSortOrder.Text = 5.ToString(CultureInfo.InvariantCulture); //database default
                    }
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
                int contentItemId;
                if (int.TryParse(Request.QueryString["id"], out contentItemId))
                {
                    DataProvider.Instance().UpdateContentItem(contentItemId, txtDescription.Text, Engage.Dnn.Utility.CreateUrlFromControl(urlThumbnail, PortalSettings), Engage.Dnn.Utility.CreateUrlFromControl(urlLink, PortalSettings), DateTime.Parse(txtStartDate.Text, CultureInfo.CurrentCulture), Engage.Utility.ParseNullableDateTime(txtEndDate.Text, CultureInfo.CurrentCulture), TabModuleId, txtTitle.Text, Engage.Dnn.Utility.CreateUrlFromControl(urlPositionThumbnail, PortalSettings), int.Parse(txtSortOrder.Text, CultureInfo.CurrentCulture));
                }
                else
                {
                    DataProvider.Instance().InsertContentItem(txtDescription.Text, Engage.Dnn.Utility.CreateUrlFromControl(urlThumbnail, PortalSettings), Engage.Dnn.Utility.CreateUrlFromControl(urlLink, PortalSettings), DateTime.Parse(txtStartDate.Text, CultureInfo.CurrentCulture), Engage.Utility.ParseNullableDateTime(txtEndDate.Text, CultureInfo.CurrentCulture), TabModuleId, txtTitle.Text, Engage.Dnn.Utility.CreateUrlFromControl(urlPositionThumbnail, PortalSettings), int.Parse(txtSortOrder.Text, CultureInfo.CurrentCulture));
                }

                Response.Redirect(EditUrl("Options"), false);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Member")]
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(EditUrl("Options"), false);
        }

        private void LoadContentItem(int contentItemId)
        {
            using (IDataReader dr = DataProvider.Instance().GetContentItem(contentItemId))
            {
                if (dr.Read())
                {
                    //lblId.Text = dr["ContentItemId"].ToString();
                    txtDescription.Text = dr["Description"].ToString();
                    urlThumbnail.Url = dr["ThumbnailUrl"].ToString();
                    urlLink.Url = dr["LinkUrl"].ToString();
                    urlPositionThumbnail.Url = dr["PositionThumbnailUrl"].ToString();
                    txtStartDate.Text = ((DateTime)dr["StartDate"]).ToShortDateString();
                    txtEndDate.Text = string.Empty;
                    object endDate = dr["EndDate"];
                    if (endDate != DBNull.Value)
                    {
                        txtEndDate.Text = ((DateTime)dr["EndDate"]).ToShortDateString();
                    }
                    txtTitle.Text = dr["Title"].ToString();
                    txtSortOrder.Text = dr["SortOrder"].ToString();
                }
            }
        }
	}
}