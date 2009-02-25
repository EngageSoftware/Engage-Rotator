// <copyright file="SqlDataProvider.cs" company="Engage Software">
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
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using DotNetNuke.Framework.Providers;
    using Microsoft.ApplicationBlocks.Data;

    /// <summary>
    /// SQL Server implementation of the abstract <see cref="DataProvider"/> class
    /// </summary>
    public class SqlDataProvider : DataProvider
    {
        /// <summary>
        /// The prefix used for database objects belonging to this module
        /// </summary>
        private const string ModuleQualifier = "EngageRotator_";

        /// <summary>
        /// The size of text fields in the database
        /// </summary>
        private const int TextFieldSize = 255;

        /// <summary>
        /// The connection string to access the database
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The prefix for all DNN database objects
        /// </summary>
        private readonly string objectQualifier;

        /// <summary>
        /// The owner or schema name to prefix object with
        /// </summary>
        private readonly string databaseOwner;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDataProvider"/> class.
        /// </summary>
        public SqlDataProvider()
        {
            // Read the configuration specific information for this provider
            ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
            Provider objProvider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];
            if (!String.IsNullOrEmpty(objProvider.Attributes["connectionStringName"]) && !String.IsNullOrEmpty(ConfigurationManager.AppSettings[objProvider.Attributes["connectionStringName"]]))
            {
                this.connectionString = ConfigurationManager.AppSettings[objProvider.Attributes["connectionStringName"]];
            }
            else
            {
                this.connectionString = objProvider.Attributes["connectionString"];
            }

            this.objectQualifier = objProvider.Attributes["objectQualifier"];
            if (!String.IsNullOrEmpty(this.objectQualifier) && !this.objectQualifier.EndsWith("_", StringComparison.OrdinalIgnoreCase))
            {
                this.objectQualifier += "_";
            }

            this.databaseOwner = objProvider.Attributes["databaseOwner"];
            if (!String.IsNullOrEmpty(this.databaseOwner) && !this.databaseOwner.EndsWith(".", StringComparison.OrdinalIgnoreCase))
            {
                this.databaseOwner += ".";
            }
        }

        /// <summary>
        /// Gets the name prefix for Engage: Rotator database objects.
        /// </summary>
        /// <value>The name prefix for Engage: Rotator database objects.</value>
        public string NamePrefix
        {
            get
            {
                return this.databaseOwner + this.objectQualifier + ModuleQualifier;
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
        /// <returns>
        /// The ID of the content item created in the database
        /// </returns>
        public override int InsertContentItem(string description, string thumbnailUrl, string linkUrl, DateTime startDate, DateTime? endDate, int moduleId, string title, string positionThumbnailUrl, int sortOrder)
        {
            return (int)(decimal)this.ExecuteScalar(
                "InsertContentItem",
                Engage.Utility.CreateTextParam("@description", description),
                Engage.Utility.CreateVarcharParam("@thumbnailUrl", thumbnailUrl, TextFieldSize),
                Engage.Utility.CreateVarcharParam("@positionThumbnailUrl", positionThumbnailUrl, TextFieldSize),
                Engage.Utility.CreateVarcharParam("@linkUrl", linkUrl, TextFieldSize),
                Engage.Utility.CreateDateTimeParam("@startDate", startDate),
                Engage.Utility.CreateDateTimeParam("@endDate", endDate),
                Engage.Utility.CreateIntegerParam("@moduleId", moduleId),
                Engage.Utility.CreateVarcharParam("@title", title, TextFieldSize),
                Engage.Utility.CreateIntegerParam("@sortOrder", sortOrder));
        }

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
        public override void UpdateContentItem(int contentItemId, string description, string thumbnailUrl, string linkUrl, DateTime startDate, DateTime? endDate, string title, string positionThumbnailUrl, int sortOrder)
        {
            this.ExecuteNonQuery(
                "UpdateContentItem",
                Engage.Utility.CreateIntegerParam("@contentItemId", contentItemId),
                Engage.Utility.CreateTextParam("@description", description),
                Engage.Utility.CreateVarcharParam("@thumbnailUrl", thumbnailUrl, TextFieldSize),
                Engage.Utility.CreateVarcharParam("@linkUrl", linkUrl, TextFieldSize),
                Engage.Utility.CreateDateTimeParam("@startDate", startDate),
                Engage.Utility.CreateDateTimeParam("@endDate", endDate),
                Engage.Utility.CreateVarcharParam("@title", title, TextFieldSize),
                Engage.Utility.CreateVarcharParam("@positionThumbnailUrl", positionThumbnailUrl, TextFieldSize),
                Engage.Utility.CreateIntegerParam("@sortOrder", sortOrder));
        }

        /// <summary>
        /// Deletes the content item with the given <paramref name="contentItemId"/>.
        /// </summary>
        /// <param name="contentItemId">The ID of the content item to delete.</param>
        public override void DeleteContentItem(int contentItemId)
        {
            this.ExecuteNonQuery(
                "DeleteContentItem",
                Engage.Utility.CreateIntegerParam("@contentItemId", contentItemId));
        }

        /// <summary>
        /// Gets the content item with the given <paramref name="contentItemId"/>.
        /// </summary>
        /// <param name="contentItemId">The ID of the content item to retrieve.</param>
        /// <returns>The content item with the given <paramref name="contentItemId"/></returns>
        public override IDataReader GetContentItem(int contentItemId)
        {
            return this.ExecuteReader(
                "GetContentItem",
                Engage.Utility.CreateIntegerParam("@contentItemId", contentItemId));
        }

        /// <summary>
        /// Gets all of the content items for the given <paramref name="moduleId"/>, getting either only items which have started but not ended, or all items if <paramref name="getOutdatedItems"/> is true.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <param name="getOutdatedItems">if set to <c>true</c> gets all content items, regardless of their start date or end date, otherwise only returns items that have started but not ended.</param>
        /// <returns>All of the content items for the given <paramref name="moduleId"/></returns>
        public override IDataReader GetContentItems(int moduleId, bool getOutdatedItems)
        {
            return this.ExecuteReader(
                "GetContentItems",
                Engage.Utility.CreateIntegerParam("@moduleId", moduleId),
                Engage.Utility.CreateBitParam("@getOutdatedItems", getOutdatedItems));
        }

        /// <summary>
        /// Executes a SQL stored procedure without returning any value.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.  Does not include any prefix, for example "InsertContentItem" is translated to "dnn_EngageRotator_spInsertContentItem."</param>
        /// <param name="parameters">The parameters for this query.</param>
        private void ExecuteNonQuery(string storedProcedureName, params SqlParameter[] parameters)
        {
            SqlHelper.ExecuteNonQuery(
                this.connectionString,
                CommandType.StoredProcedure,
                this.NamePrefix + "sp" + storedProcedureName,
                parameters);
        }

        /// <summary>
        /// Executes a SQL stored procedure, returning the results as a <see cref="SqlDataReader"/>.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.  Does not include any prefix, for example "GetGetContentItem" is translated to "dnn_EngageRotator_spGetContentItem."</param>
        /// <param name="parameters">The parameters for this query.</param>
        /// <returns>A <see cref="SqlDataReader"/> with the results of the stored procedure call</returns>
        private SqlDataReader ExecuteReader(string storedProcedureName, params SqlParameter[] parameters)
        {
            return SqlHelper.ExecuteReader(
                this.connectionString,
                CommandType.StoredProcedure,
                this.NamePrefix + "sp" + storedProcedureName,
                parameters);
        }

        /// <summary>
        /// Executes a SQL stored procedure, returning a single value.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.  Does not include any prefix, for example "InsertContentItem" is translated to "dnn_EngageRotator_spInsertContentItem."</param>
        /// <param name="parameters">The parameters for this query.</param>
        /// <returns>The single value returned from the stored procedure call</returns>
        private object ExecuteScalar(string storedProcedureName, params SqlParameter[] parameters)
        {
            return SqlHelper.ExecuteScalar(
                this.connectionString,
                CommandType.StoredProcedure,
                this.NamePrefix + "sp" + storedProcedureName,
                parameters);
        }
    }
}