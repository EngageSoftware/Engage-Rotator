// <copyright file="DataProvider.cs" company="Engage Software">
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
    using System.Data;
    using System.Diagnostics;

    /// <summary>
    /// An abstract class for the data access layer
    /// </summary>
    public abstract class DataProvider
    {
        /// <summary>
        /// Singleton reference to the instantiated object 
        /// </summary>
        private static readonly DataProvider objProvider = (DataProvider)DotNetNuke.Framework.Reflection.CreateObject("data", "Engage.Dnn.ContentRotator", string.Empty);

        /// <summary>
        /// Gets the reference to the current instance of the <see cref="DataProvider"/>
        /// </summary>
        /// <returns>An instantiated <see cref="DataProvider"/></returns>
        public static DataProvider Instance
        {
            [DebuggerStepThrough]
            get
            {
                return objProvider;
            }
        }

        /// <summary>
        /// Inserts a new content item.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="thumbnailUrl">The thumbnail URL.</param>
        /// <param name="linkUrl">The link URL.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="moduleId">The module id.</param>
        /// <param name="title">The title.</param>
        /// <param name="positionThumbnailUrl">The position thumbnail URL.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns>The ID of the content item created in the database</returns>
        public abstract int InsertContentItem(string description, string thumbnailUrl, string linkUrl, DateTime startDate, DateTime? endDate, int moduleId, string title, string positionThumbnailUrl, int sortOrder);

        /// <summary>
        /// Updates the content item with the given <see cref="contentItemId"/>.
        /// </summary>
        /// <param name="contentItemId">The ID of the content item to update.</param>
        /// <param name="description">The description.</param>
        /// <param name="thumbnailUrl">The thumbnail URL.</param>
        /// <param name="linkUrl">The link URL.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="title">The title.</param>
        /// <param name="positionThumbnailUrl">The position thumbnail URL.</param>
        /// <param name="sortOrder">The sort order.</param>
        public abstract void UpdateContentItem(int contentItemId, string description, string thumbnailUrl, string linkUrl, DateTime startDate, DateTime? endDate, string title, string positionThumbnailUrl, int sortOrder);

        /// <summary>
        /// Deletes the content item with the given <paramref name="contentItemId"/>.
        /// </summary>
        /// <param name="contentItemId">The ID of the content item to delete.</param>
        public abstract void DeleteContentItem(int contentItemId);

        /// <summary>
        /// Gets the content item with the given <paramref name="contentItemId"/>.
        /// </summary>
        /// <param name="contentItemId">The ID of the content item to retrieve.</param>
        /// <returns>The content item with the given <paramref name="contentItemId"/></returns>
        public abstract IDataReader GetContentItem(int contentItemId);

        /// <summary>
        /// Gets all of the content items for the given <paramref name="moduleId"/>, getting either only items which have started but not ended, or all items if <paramref name="getOutdatedItems"/> is true.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <param name="getOutdatedItems">if set to <c>true</c> gets all content items, regardless of their start date or end date, otherwise only returns items that have started but not ended.</param>
        /// <returns>All of the content items for the given <paramref name="moduleId"/></returns>
        public abstract IDataReader GetContentItems(int moduleId, bool getOutdatedItems);
    }
}