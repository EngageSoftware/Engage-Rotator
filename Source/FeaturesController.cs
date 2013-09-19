// <copyright file="FeaturesController.cs" company="Engage Software">
// Engage: Rotator - http://www.EngageSoftware.com
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
#if TRIAL
    using System;
#endif
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;

    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Tabs;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.FileSystem;

    /// <summary>
    /// Controls which DNN features are available for this module.
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instantiated through reflection by DNN")]
    internal class FeaturesController : IPortable
    {
#if TRIAL
        /// <summary>
        /// The license key for this module
        /// </summary>
        public static readonly Guid ModuleLicenseKey = new Guid("E59D0904-B59F-4785-9498-26BE5503A123");
#endif

        /// <summary>
        /// The export module.
        /// </summary>
        /// <param name="moduleId">
        /// The module id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ExportModule(int moduleId)
        {
            var output = new StringBuilder();

            output.Append("<slides>");

            try
            {
                foreach (var slide in Slide.GetSlides(moduleId))
                {
                    output.Append("<slide>");
                    output.AppendFormat("<content>{0}</content>", slide.Content);
                    output.AppendFormat("<link>{0}</link>", slide.Link);
                    
                    output.AppendFormat("<imagelink>{0}</imagelink>", slide.ImageLink);
                    output.AppendFormat("<startdate>{0}</startdate>", slide.StartDate);
                    
                    if (slide.EndDate.HasValue)
                    {
                        output.AppendFormat("<enddate>{0}</enddate>", slide.EndDate);
                    }

                    output.AppendFormat("<pagerimagelink>{0}</pagerimagelink>", slide.PagerImageLink);
                    output.AppendFormat("<title>{0}</title>", slide.Title);
                    output.AppendFormat("<sortorder>{0}</sortorder>", slide.SortOrder);
                    output.AppendFormat("<tracklink>{0}</tracklink>", slide.TrackLink);

                    // output imagedata and tab paths for multi-instance portibility
                    ProcessExport(slide.ImageLink, "image", ref output);
                    ProcessExport(slide.Link, "link", ref output);

                    output.Append("</slide>");
                }
            }
            catch (Exception exc)
            {
                Exceptions.LogException(exc);
            }

            output.Append("</slides>");
            return output.ToString();
        }

        /// <summary>
        /// The import module.
        /// </summary>
        /// <param name="moduleId">
        /// The module id.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        public void ImportModule(int moduleId, string content, string version, int userId)
        {
            // ReSharper disable PossibleNullReferenceException
            // ReSharper disable UseObjectOrCollectionInitializer
            try
            {
                var slides = DotNetNuke.Common.Globals.GetContent(content, "slides");

                foreach (XmlNode slide in slides.SelectNodes("slide"))
                {
                    var newSlide = new Slide();

                    newSlide.Content = slide.SelectSingleNode("content").InnerText;
                    newSlide.Link = slide.SelectSingleNode("link").InnerText;
                    newSlide.ImageLink = slide.SelectSingleNode("imagelink").InnerText;
                    newSlide.StartDate = DateTime.Parse(slide.SelectSingleNode("startdate").InnerText);

                    var endDateXml = slide.SelectSingleNode("enddate");
                    if (endDateXml != null)
                    {
                        newSlide.EndDate = DateTime.Parse(endDateXml.InnerText);
                    }

                    newSlide.PagerImageLink = slide.SelectSingleNode("pagerimagelink").InnerText;
                    newSlide.Title = slide.SelectSingleNode("title").InnerText;
                    newSlide.SortOrder = int.Parse(slide.SelectSingleNode("sortorder").InnerText);
                    newSlide.TrackLink = bool.Parse(slide.SelectSingleNode("tracklink").InnerText);

                    this.ProcessImport(slide, "image", ref newSlide);
                    this.ProcessImport(slide, "link", ref newSlide);
                    
                    newSlide.Save(moduleId);
                }
            }
            catch (Exception exc)
            {
                Exceptions.LogException(exc);
            }
        }

        /// <summary>
        /// process export for multi-instance portability, rewrite images into base64 and tab id's into paths
        /// </summary>
        /// <param name="slideUrl">
        /// The slide url.
        /// </param>
        /// <param name="outputPrefix">
        /// The output prefix. (image, or link)
        /// </param>
        /// <param name="output">
        /// The output.
        /// </param>
        private static void ProcessExport(string slideUrl, string outputPrefix, ref StringBuilder output)
        {
            var ps = DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings();

            // var linkType = DotNetNuke.Common.Globals.GetURLType(slide.LinkUrl);
            if (slideUrl.ToLower().StartsWith("fileid="))
            {
                var fileId = int.Parse(UrlUtils.GetParameterValue(slideUrl));
                var fileController = new FileController();
                var file = fileController.GetFileById(fileId, ps.PortalId);

                if (file != null)
                {
                    // todo should we check content type before converting to an image?
                    var buffer = File.ReadAllBytes(file.PhysicalPath);
                    var base64 = Convert.ToBase64String(buffer);

                    output.AppendFormat("<{1}fileextension>{0}</{1}fileextension>", file.Extension, outputPrefix);
                    output.AppendFormat("<{1}filename>{0}</{1}filename>", file.FileName, outputPrefix);
                    output.AppendFormat("<{1}contenttype>{0}</{1}contenttype>", file.ContentType, outputPrefix);
                    output.AppendFormat("<{1}folderpath>{0}</{1}folderpath>", file.Folder, outputPrefix);
                    output.AppendFormat("<{1}filedata>{0}</{1}filedata>", base64, outputPrefix);
                }
            }
            else if (slideUrl.ToLower().StartsWith("tabid="))
            {
                var tabId = int.Parse(UrlUtils.GetParameterValue(slideUrl));
                var tabController = new TabController();
                var tab = tabController.GetTab(tabId, ps.PortalId, true);

                if (tab != null)
                {
                    output.AppendFormat("<{1}tabpath>{0}</{1}tabpath>", tab.TabPath, outputPrefix);
                }
            }
        }

        /// <summary>
        /// process import of multi-instance records. 
        /// </summary>
        /// <param name="xml">
        /// The xml.
        /// </param>
        /// <param name="prefix">
        /// The prefix (image, or link)
        /// </param>
        /// <param name="slide">
        /// The slide.
        /// </param>
        private void ProcessImport(XmlNode xml, string prefix, ref Slide slide)
        {
            var ps = DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings();

            var contentTypeXml = xml.SelectSingleNode(prefix + "contenttype");
            var contentFileNameXml = xml.SelectSingleNode(prefix + "filename");
            var contentFileExtensionXml = xml.SelectSingleNode(prefix + "fileextension");
            var contentFolderPathXml = xml.SelectSingleNode(prefix + "folderpath");
            var contentBase64Xml = xml.SelectSingleNode(prefix + "filedata");
            var contentTabPathXml = xml.SelectSingleNode(prefix + "tabpath");

            // this item appears to be an encoded tabpath.... lets continue
            if (contentTabPathXml != null)
            {
                // todo, when upgrading  DNN reference, switch this to GetTabByTabPath on the TabController
                var tabInfo = ps.DesktopTabs.Cast<TabInfo>().SingleOrDefault(desktopTab => desktopTab.TabPath == contentTabPathXml.InnerText);
                if (tabInfo != null)
                {
                    switch (prefix)
                    {
                        case "image":
                            slide.ImageLink = "TabID=" + tabInfo.TabID.ToString(CultureInfo.InvariantCulture);
                            break;
                        default:
                            slide.Link = "TabID=" + tabInfo.TabID.ToString(CultureInfo.InvariantCulture);
                            break;
                    }
                }
            }

            // this item appears to be an encoded image... lets continue
            if (contentTypeXml != null && contentBase64Xml != null && contentFolderPathXml != null && contentFileNameXml != null && contentFileExtensionXml != null)
            {
                var folderController = new FolderController();
                var fileController = new FileController();

                // for now, just put imported images into the root folder... 
                var folder = folderController.GetFolder(ps.PortalId, contentFolderPathXml.InnerText, true);

                if (folder == null)
                {
                    folderController.AddFolder(ps.PortalId, contentFolderPathXml.InnerText);

                    folder = folderController.GetFolder(ps.PortalId, contentFolderPathXml.InnerText, true);
                }

                var file = fileController.GetFile(contentFileNameXml.InnerText, ps.PortalId, folder.FolderID);

                if (file == null)
                {
                    var content = Convert.FromBase64String(contentBase64Xml.InnerText);
                    file = new DotNetNuke.Services.FileSystem.FileInfo
                                   {
                                       PortalId = ps.PortalId,
                                       ContentType = contentTypeXml.InnerText,
                                       FileName = contentFileNameXml.InnerText,
                                       Extension =
                                           contentFileExtensionXml.InnerText,
                                       FolderId = folder.FolderID,
                                       Size = content.Length,
                                   };

                    // save the file the dnn filesystem
                    File.WriteAllBytes(ps.HomeDirectoryMapPath + file.FileName, content);

                    // add the file to the dnn database
                    file.FileId = fileController.AddFile(file);
                }

                // update the files content.... just incase, it never hurts.... right?
                //fileController.UpdateFileContent(file.FileId, content);

                switch (prefix)
                {
                    case "image":
                        slide.ImageLink = "FileID=" + file.FileId.ToString(CultureInfo.InvariantCulture);
                        break;
                    default:
                        slide.Link = "FileID=" + file.FileId.ToString(CultureInfo.InvariantCulture);
                        break;
                }
            }
        }
    }
}