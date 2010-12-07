// <copyright file="Slide.cs" company="Engage Software">
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
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Globalization;

    using DotNetNuke.Common;

    using Framework.Templating;

    /// <summary>
    /// A slide of content to be rotated
    /// </summary>
    public class Slide : ITemplateable
    {
        /// <summary>
        /// Backing field for <see cref="SlideId"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int slideId;

        /// <summary>
        /// <c>true</c> if this instance does not exist in the database; otherwise <c>false</c>
        /// </summary>
        private bool isNew = true;

        /// <summary>
        /// The index of this slide in the list by which it was retrieved, or <c>null</c> if it wasn't retrieved in a list
        /// </summary>
        private int? itemIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Slide"/> class.
        /// </summary>
        public Slide()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Slide"/> class.
        /// </summary>
        /// <param name="slideId">The ID of this already existing slide.</param>
        public Slide(int slideId)
        {
            this.slideId = slideId;
            this.isNew = false;
        }

        /// <summary>
        /// Gets the ID of this slide.
        /// </summary>
        /// <value>The slide ID.</value>
        public int SlideId
        {
            [DebuggerStepThrough]
            get
            {
                Debug.Assert(!this.isNew, "It is invalid to get the SlideId on an instance that is not from the database");
                return this.slideId;
            }
        }

        /// <summary>
        /// Gets the ID of the module to which this slide belongs.
        /// </summary>
        /// <value>The module ID.</value>
        public int ModuleId { get; private set; }

        /// <summary>
        /// Gets or sets the main content of this instance.
        /// </summary>
        /// <value>The main HTML content.</value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets when this instance stops being displayed.
        /// </summary>
        /// <value>When this instance stops being displayed.</value>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the relative sort order of this instance.
        /// </summary>
        /// <value>The relative sort order of this instance.</value>
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets when this instance starts displaying.
        /// </summary>
        /// <value>When this instance starts displaying.</value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the title of this instance.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the DNN-style URL to which links in this instance point.
        /// </summary>
        /// <value>A DNN-style URL (just a number for a tab ID, or real URL, or <see cref="string.Empty"/>).</value>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the DNN-style URL at which the position thumbnail is located
        /// </summary>
        /// <value>A DNN-style URL (FileID=57, or real URL, or <see cref="string.Empty"/>).</value>
        public string PagerImageLink { get; set; }

        /// <summary>
        /// Gets or sets the DNN-style URL at which the main thumbnail is located.
        /// </summary>
        /// <value>A DNN-style URL (FileID=57, or real URL, or <see cref="string.Empty"/>).</value>
        public string ImageLink { get; set; }

        /// <summary>
        /// Gets the URL to which this slide links.
        /// </summary>
        /// <value>The URL for the link.</value>
        public string LinkUrl
        {
            get { return this.ResolveDnnLink(this.Link); }
        }

        /// <summary>
        /// Gets the URL at which the pager thumbnail is located.
        /// </summary>
        /// <value>A URL to the image.</value>
        public string PagerImageUrl
        {
            get { return this.ResolveDnnLink(this.PagerImageLink); }
        }

        /// <summary>
        /// Gets the URL at which the main thumbnail is located.
        /// </summary>
        /// <value>A URL to the image.</value>
        public string ImageUrl
        {
            get { return this.ResolveDnnLink(this.ImageLink); }
        }

        /// <summary>
        /// Gets the slide with the given <paramref name="slideId"/>.
        /// </summary>
        /// <param name="slideId">The ID of the slide to retrieve.</param>
        /// <returns>The slide with the given <paramref name="slideId"/></returns>
        public static Slide GetSlide(int slideId)
        {
            using (IDataReader reader = DataProvider.Instance.GetSlide(slideId))
            {
                if (reader.Read())
                {
                    return Fill(reader);
                }
            }

            return null;
        }

        /// <summary>
        /// Deletes the slide with the given <paramref name="slideId"/>.
        /// </summary>
        /// <param name="slideId">The ID of the slide to delete.</param>
        public static void Delete(int slideId)
        {
            DataProvider.Instance.DeleteSlide(slideId);
        }

        /// <summary>
        /// Saves this instance to the database
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        public void Save(int moduleId)
        {
            if (this.isNew)
            {
                this.slideId = DataProvider.Instance.InsertSlide(this.Content, this.ImageLink, this.Link, this.StartDate, this.EndDate, moduleId, this.Title, this.PagerImageLink, this.SortOrder);
                this.isNew = false;
            }
            else
            {
                DataProvider.Instance.UpdateSlide(this.slideId, this.Content, this.ImageLink, this.Link, this.StartDate, this.EndDate, this.Title, this.PagerImageLink, this.SortOrder);
            }
        }

        /// <summary>
        /// Gets the value of the property with the given <paramref name="propertyName"/>, or <see cref="F:System.String.Empty"/> if a property with that name does not exist on this object or is <c>null</c>.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// The string representation of the value of this instance.
        /// </returns>
        public string GetValue(string propertyName)
        {
            return this.GetValue(propertyName, null);
        }

        /// <summary>
        /// Gets the value of the property with the given <paramref name="propertyName"/>, or <see cref="F:System.String.Empty"/> if a property with that name does not exist on this object or is <c>null</c>.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param><param name="format">A numeric or DateTime format string, or <c>null</c> or <see cref="F:System.String.Empty"/> to apply the default format.</param>
        /// <returns>
        /// The string representation of the value of this instance as specified by <paramref name="format"/>.
        /// </returns>
        public string GetValue(string propertyName, string format)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                switch (propertyName.ToUpperInvariant())
                {
                    case "CONTENT":
                        return this.Content;
                    case "ENDDATE":
                    case "END DATE":
                        return this.EndDate.HasValue ? this.EndDate.Value.ToString(format, CultureInfo.CurrentCulture) : string.Empty;
                    case "LINKURL":
                    case "LINK URL":
                        return this.LinkUrl;
                    case "PAGERIMAGEURL":
                    case "PAGER IMAGE URL":
                        return this.PagerImageUrl;
                    case "SORTORDER":
                    case "SORT ORDER":
                        return this.SortOrder.ToString(format, CultureInfo.CurrentCulture);
                    case "STARTDATE":
                    case "START DATE":
                        return this.StartDate.ToString(format, CultureInfo.CurrentCulture);
                    case "IMAGEURL":
                    case "IMAGE URL":
                        return this.ImageUrl;
                    case "TITLE":
                        return this.Title;
                    case "SLIDEID":
                    case "SLIDE ID":
                        return this.slideId.ToString(format, CultureInfo.CurrentCulture);

                        // Index is for internal use only, not intended to be documented to the public
                    case "INDEX":
                        if (this.itemIndex.HasValue)
                        {
                            return this.itemIndex.Value.ToString(format, CultureInfo.InvariantCulture);
                        }

                        break;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets all of the slides for the given <paramref name="moduleId"/>, getting only slides which have started but not ended.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <returns>All of the slides for the given <paramref name="moduleId"/></returns>
        internal static List<Slide> GetSlides(int moduleId)
        {
            return GetSlides(moduleId, false);
        }

        /// <summary>
        /// Gets all of the slides for the given <paramref name="moduleId"/>, getting either only slides which have started but not ended, or all slides if <paramref name="getOutdatedSlides"/> is true.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <param name="getOutdatedSlides">if set to <c>true</c> gets all slides, regardless of their start date or end date, otherwise only returns slides that have started but not ended.</param>
        /// <returns>All of the slides for the given <paramref name="moduleId"/></returns>
        internal static List<Slide> GetSlides(int moduleId, bool getOutdatedSlides)
        {
            var items = new List<Slide>();
            using (IDataReader reader = DataProvider.Instance.GetSlides(moduleId, getOutdatedSlides))
            {
                int itemIndex = 0;
                while (reader.Read())
                {
                    items.Add(Fill(reader, itemIndex++));
                }
            }

            return items;
        }

        /// <summary>
        /// Instantiates a <see cref="Slide"/> from the given <paramref name="slideRecord"/>
        /// </summary>
        /// <param name="slideRecord">The <see cref="IDataRecord"/> representing the <see cref="Slide"/> to instantiate.</param>
        /// <returns>The <see cref="Slide"/> represented by the given <paramref name="slideRecord"/></returns>
        private static Slide Fill(IDataRecord slideRecord)
        {
            return Fill(slideRecord, null);
        }

        /// <summary>
        /// Instantiates a <see cref="Slide"/> from the given <paramref name="slideRecord"/>
        /// </summary>
        /// <param name="slideRecord">The <see cref="IDataRecord"/> representing the <see cref="Slide"/> to instantiate.</param>
        /// <param name="itemIndex">The index of the slide record in the query which retrieved it, or <c>null</c> if it was not retrieved in a list.</param>
        /// <returns>
        /// The <see cref="Slide"/> represented by the given <paramref name="slideRecord"/>
        /// </returns>
        private static Slide Fill(IDataRecord slideRecord, int? itemIndex)
        {
            return new Slide
                {
                    isNew = false,
                    slideId = (int)slideRecord["SlideId"],
                    Content = slideRecord["Content"].ToString(),
                    Title = slideRecord["Title"].ToString(),
                    Link = slideRecord["LinkUrl"].ToString(),
                    ImageLink = slideRecord["ImageUrl"].ToString(),
                    PagerImageLink = slideRecord["PagerImageUrl"].ToString(),
                    StartDate = (DateTime)slideRecord["StartDate"],
                    EndDate = slideRecord["EndDate"] as DateTime?,
                    SortOrder = (int)slideRecord["SortOrder"],
                    ModuleId = (int)slideRecord["ModuleId"],
                    itemIndex = itemIndex
                };
        }

        /// <summary>
        /// Resolves a DNN-style link (tab ID, FileID=57, plain URL) into a useable URL.
        /// </summary>
        /// <param name="link">The link (one of: an integer [as a tab ID], <c>FileID=#</c>, <c>http://...</c> or <see cref="string.Empty"/>).</param>
        /// <returns>A URL pointing to the resource</returns>
        private string ResolveDnnLink(string link)
        {
            return Globals.LinkClick(link, Globals.GetPortalSettings().ActiveTab.TabID, this.ModuleId, false, false);
        }
    }
}