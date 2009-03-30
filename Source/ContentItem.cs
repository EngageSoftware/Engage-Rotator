// <copyright file="ContentItem.cs" company="Engage Software">
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
    using System.Data;
    using System.Diagnostics;
    using System.Globalization;
    using Framework.Templating;

    /// <summary>
    /// An item of content to be rotated
    /// </summary>
    public class ContentItem : ITemplateable
    {
        /// <summary>
        /// Backing field for <see cref="ContentItemId"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int contentItemId;

        /// <summary>
        /// Backing field for <see cref="Description"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string description;

        /// <summary>
        /// Backing field for <see cref="EndDate"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime? endDate;

        /// <summary>
        /// <c>true</c> if this instance does not exist in the database; otherwise <c>false</c>
        /// </summary>
        private bool isNew = true;

        /// <summary>
        /// Backing field for <see cref="LinkUrl"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string linkUrl;

        /// <summary>
        /// Backing field for <see cref="PositionThumbnailUrl"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string positionThumbnailUrl;

        /// <summary>
        /// Backing field for <see cref="SortOrder"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int sortOrder;

        /// <summary>
        /// Backing field for <see cref="StartDate"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime startDate;

        /// <summary>
        /// Backing field for <see cref="ThumbnailUrl"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string thumbnailUrl;

        /// <summary>
        /// Backing field for <see cref="Title"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string title;

        /// <summary>
        /// The index of this item in the list by which it was retrieved, or <c>null</c> if it wasn't retrieved in a list
        /// </summary>
        private int? itemIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentItem"/> class.
        /// </summary>
        public ContentItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentItem"/> class.
        /// </summary>
        /// <param name="contentItemId">The ID of this already existing content item.</param>
        public ContentItem(int contentItemId)
        {
            this.contentItemId = contentItemId;
            this.isNew = false;
        }

        /// <summary>
        /// Gets the ID of this content item.
        /// </summary>
        /// <value>The content item ID.</value>
        public int ContentItemId
        {
            [DebuggerStepThrough]
            get
            {
                Debug.Assert(!this.isNew, "It is invalid to get the ContentItemId on an instance that is not from the database");
                return this.contentItemId;
            }
        }

        /// <summary>
        /// Gets or sets the main content of this instance.
        /// </summary>
        /// <value>The main HTML content.</value>
        public string Description
        {
            [DebuggerStepThrough]
            get
            {
                return this.description;
            }

            [DebuggerStepThrough]
            set
            {
                this.description = value;
            }
        }

        /// <summary>
        /// Gets or sets when this instance stops being displayed.
        /// </summary>
        /// <value>When this instance stops being displayed.</value>
        public DateTime? EndDate
        {
            [DebuggerStepThrough]
            get
            {
                return this.endDate;
            }

            [DebuggerStepThrough]
            set
            {
                this.endDate = value;
            }
        }

        /// <summary>
        /// Gets or sets the URL to which links in this instance point.
        /// </summary>
        /// <value>The URL to which links in this instance point.</value>
        public string LinkUrl
        {
            [DebuggerStepThrough]
            get
            {
                return this.linkUrl;
            }

            [DebuggerStepThrough]
            set
            {
                this.linkUrl = value;
            }
        }

        /// <summary>
        /// Gets or sets the URL at which the position thumbnail is located
        /// </summary>
        /// <value>The URL at which the position thumbnail is located.</value>
        public string PositionThumbnailUrl
        {
            [DebuggerStepThrough]
            get
            {
                return this.positionThumbnailUrl;
            }

            [DebuggerStepThrough]
            set
            {
                this.positionThumbnailUrl = value;
            }
        }

        /// <summary>
        /// Gets or sets the relative sort order of this instance.
        /// </summary>
        /// <value>The relative sort order of this instance.</value>
        public int SortOrder
        {
            [DebuggerStepThrough]
            get
            {
                return this.sortOrder;
            }

            [DebuggerStepThrough]
            set
            {
                this.sortOrder = value;
            }
        }

        /// <summary>
        /// Gets or sets when this instance starts displaying.
        /// </summary>
        /// <value>When this instance starts displaying.</value>
        public DateTime StartDate
        {
            [DebuggerStepThrough]
            get
            {
                return this.startDate;
            }

            [DebuggerStepThrough]
            set
            {
                this.startDate = value;
            }
        }

        /// <summary>
        /// Gets or sets the URL at which the main thumbnail is located.
        /// </summary>
        /// <value>The URL at which the main thumbnail is located.</value>
        public string ThumbnailUrl
        {
            [DebuggerStepThrough]
            get
            {
                return this.thumbnailUrl;
            }

            [DebuggerStepThrough]
            set
            {
                this.thumbnailUrl = value;
            }
        }

        /// <summary>
        /// Gets or sets the title of this instance.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            [DebuggerStepThrough]
            get
            {
                return this.title;
            }

            [DebuggerStepThrough]
            set
            {
                this.title = value;
            }
        }

        /// <summary>
        /// Gets the content item with the given <paramref name="contentItemId"/>.
        /// </summary>
        /// <param name="contentItemId">The ID of the content item to retrieve.</param>
        /// <returns>The content item with the given <paramref name="contentItemId"/></returns>
        public static ContentItem GetContentItem(int contentItemId)
        {
            using (IDataReader reader = DataProvider.Instance.GetContentItem(contentItemId))
            {
                if (reader.Read())
                {
                    return Fill(reader);
                }
            }

            return null;
        }

        /// <summary>
        /// Deletes the content item with the given <paramref name="contentItemId"/>.
        /// </summary>
        /// <param name="contentItemId">The ID of the content item to delete.</param>
        public static void Delete(int contentItemId)
        {
            DataProvider.Instance.DeleteContentItem(contentItemId);
        }

        /// <summary>
        /// Saves this instance to the database
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        public void Save(int moduleId)
        {
            if (this.isNew)
            {
                this.contentItemId = DataProvider.Instance.InsertContentItem(this.description, this.thumbnailUrl, this.linkUrl, this.startDate, this.endDate, moduleId, this.title, this.positionThumbnailUrl, this.sortOrder);
                this.isNew = false;
            }
            else
            {
                DataProvider.Instance.UpdateContentItem(this.contentItemId, this.description, this.thumbnailUrl, this.linkUrl, this.startDate, this.endDate, this.title, this.positionThumbnailUrl, this.sortOrder);
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
            switch (propertyName.ToUpperInvariant())
            {
                case "DESCRIPTION":
                    return this.description;
                case "ENDDATE":
                    return this.endDate.HasValue ? this.endDate.Value.ToString(format, CultureInfo.CurrentCulture) : string.Empty;
                case "LINKURL":
                    return this.linkUrl;
                case "POSITIONTHUMBNAILURL":
                    return this.positionThumbnailUrl;
                case "SORTORDER":
                    return this.sortOrder.ToString(format, CultureInfo.CurrentCulture);
                case "STARTDATE":
                    return this.startDate.ToString(format, CultureInfo.CurrentCulture);
                case "THUMBNAILURL":
                    return this.thumbnailUrl;
                case "TITLE":
                    return this.title;

                // Index is for internal use only, not intended to be documented to the public
                case "INDEX":
                    if (this.itemIndex.HasValue)
                    {
                        return this.itemIndex.Value.ToString(format, CultureInfo.InvariantCulture);
                    }

                    break;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets all of the content items for the given <paramref name="moduleId"/>, getting only items which have started but not ended.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <returns>All of the content items for the given <paramref name="moduleId"/></returns>
        internal static List<ContentItem> GetContentItems(int moduleId)
        {
            return GetContentItems(moduleId, false);
        }

        /// <summary>
        /// Gets all of the content items for the given <paramref name="moduleId"/>, getting either only items which have started but not ended, or all items if <paramref name="getOutdatedItems"/> is true.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <param name="getOutdatedItems">if set to <c>true</c> gets all content items, regardless of their start date or end date, otherwise only returns items that have started but not ended.</param>
        /// <returns>All of the content items for the given <paramref name="moduleId"/></returns>
        internal static List<ContentItem> GetContentItems(int moduleId, bool getOutdatedItems)
        {
            List<ContentItem> items = new List<ContentItem>();
            using (IDataReader reader = DataProvider.Instance.GetContentItems(moduleId, getOutdatedItems))
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
        /// Instantiates a <see cref="ContentItem"/> from the given <paramref name="contentItemRecord"/>
        /// </summary>
        /// <param name="contentItemRecord">The <see cref="IDataRecord"/> representing the <see cref="ContentItem"/> to instantiate.</param>
        /// <returns>The <see cref="ContentItem"/> represented by the given <paramref name="contentItemRecord"/></returns>
        private static ContentItem Fill(IDataRecord contentItemRecord)
        {
            return Fill(contentItemRecord, null);
        }

        /// <summary>
        /// Instantiates a <see cref="ContentItem"/> from the given <paramref name="contentItemRecord"/>
        /// </summary>
        /// <param name="contentItemRecord">The <see cref="IDataRecord"/> representing the <see cref="ContentItem"/> to instantiate.</param>
        /// <param name="itemIndex">if set to <c>true</c>, <paramref name="contentItemRecord"/> has an "Index" column with the index of this item in the list of items retrieved.</param>
        /// <returns>
        /// The <see cref="ContentItem"/> represented by the given <paramref name="contentItemRecord"/>
        /// </returns>
        private static ContentItem Fill(IDataRecord contentItemRecord, int? itemIndex)
        {
            ContentItem item = new ContentItem();

            item.isNew = false;
            item.contentItemId = (int)contentItemRecord["ContentItemId"];
            item.description = contentItemRecord["Description"].ToString();
            item.title = contentItemRecord["Title"].ToString();
            item.linkUrl = contentItemRecord["LinkUrl"].ToString();
            item.thumbnailUrl = contentItemRecord["ThumbnailUrl"].ToString();
            item.positionThumbnailUrl = contentItemRecord["PositionThumbnailUrl"].ToString(); 
            item.startDate = (DateTime)contentItemRecord["StartDate"];
            item.endDate = contentItemRecord["EndDate"] as DateTime?;
            item.sortOrder = (int)contentItemRecord["SortOrder"];
            item.itemIndex = itemIndex;

            return item;
        }
    }
}