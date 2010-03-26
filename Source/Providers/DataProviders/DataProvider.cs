// <copyright file="DataProvider.cs" company="Engage Software">
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
        private static readonly DataProvider instance = (DataProvider)DotNetNuke.Framework.Reflection.CreateObject("data", "Engage.Dnn.ContentRotator", string.Empty);

        /// <summary>
        /// Gets the reference to the current instance of the <see cref="DataProvider"/>
        /// </summary>
        /// <returns>An instantiated <see cref="DataProvider"/></returns>
        public static DataProvider Instance
        {
            [DebuggerStepThrough]
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Inserts a new slide.
        /// </summary>
        /// <param name="content">The main content being displayed.</param>
        /// <param name="imageUrl">The URL to the main image.</param>
        /// <param name="linkUrl">The link URL.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="moduleId">The module id.</param>
        /// <param name="title">The title.</param>
        /// <param name="pagerImageUrl">The URL to the pager image.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns>The ID of the slide created in the database</returns>
        public abstract int InsertSlide(string content, string imageUrl, string linkUrl, DateTime startDate, DateTime? endDate, int moduleId, string title, string pagerImageUrl, int sortOrder);

        /// <summary>
        /// Updates the slide with the given <see cref="slideId"/>.
        /// </summary>
        /// <param name="slideId">The ID of the slide to update.</param>
        /// <param name="content">The main content being displayed.</param>
        /// <param name="imageUrl">The URL to the main image.</param>
        /// <param name="linkUrl">The link URL.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="title">The title.</param>
        /// <param name="pagerImageUrl">The URL to the pager image.</param>
        /// <param name="sortOrder">The sort order.</param>
        public abstract void UpdateSlide(int slideId, string content, string imageUrl, string linkUrl, DateTime startDate, DateTime? endDate, string title, string pagerImageUrl, int sortOrder);

        /// <summary>
        /// Deletes the slide with the given <paramref name="slideId"/>.
        /// </summary>
        /// <param name="slideId">The ID of the slide to delete.</param>
        public abstract void DeleteSlide(int slideId);

        /// <summary>
        /// Gets the slide with the given <paramref name="slideId"/>.
        /// </summary>
        /// <param name="slideId">The ID of the slide to retrieve.</param>
        /// <returns>The slide with the given <paramref name="slideId"/></returns>
        public abstract IDataReader GetSlide(int slideId);

        /// <summary>
        /// Gets all of the slides for the given <paramref name="moduleId"/>, getting either only slides which have started but not ended, or all slides if <paramref name="getOutdatedSlides"/> is true.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <param name="getOutdatedSlides">if set to <c>true</c> gets all slides, regardless of their start date or end date, otherwise only returns slides that have started but not ended.</param>
        /// <returns>All of the slides for the given <paramref name="moduleId"/></returns>
        public abstract IDataReader GetSlides(int moduleId, bool getOutdatedSlides);
    }
}