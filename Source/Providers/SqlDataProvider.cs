//Copyright (c) 2004-2008
//by Engage Software ( http://www.engagesoftware.net )

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
//TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
//THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
//CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
//DEALINGS IN THE SOFTWARE.

using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using DotNetNuke.Framework.Providers;

namespace Engage.Dnn.ContentRotator
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// SQL Server implementation of the abstract DataProvider class
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    public class SqlDataProvider : DataProvider
    {
        #region Private Members
        private const string ProviderType = "data";
        private const string ModuleQualifier = "EngageRotator_";
        private readonly ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration(ProviderType);
        private readonly string _connectionString;
        private readonly string _providerPath;
        private readonly string _objectQualifier;
        private readonly string _databaseOwner;
        #endregion

        #region Constructor
        public SqlDataProvider()
        {
            //  Read the configuration specific information for this provider
            Provider objProvider = (Provider)_providerConfiguration.Providers[_providerConfiguration.DefaultProvider];
            if (!String.IsNullOrEmpty(objProvider.Attributes["connectionStringName"]) && !String.IsNullOrEmpty(ConfigurationManager.AppSettings[objProvider.Attributes["connectionStringName"]]))
            {
                _connectionString = ConfigurationManager.AppSettings[objProvider.Attributes["connectionStringName"]];
            }
            else
            {
                _connectionString = objProvider.Attributes["connectionString"];
            }
            _providerPath = objProvider.Attributes["providerPath"];
            _objectQualifier = objProvider.Attributes["objectQualifier"];
            if (!String.IsNullOrEmpty(_objectQualifier) && !_objectQualifier.EndsWith("_", StringComparison.OrdinalIgnoreCase))
            {
                _objectQualifier += "_";
            }
            _databaseOwner = objProvider.Attributes["databaseOwner"];
            if (!String.IsNullOrEmpty(_databaseOwner) && !_databaseOwner.EndsWith(".", StringComparison.OrdinalIgnoreCase))
            {
                _databaseOwner += ".";
            }
        }
        #endregion

        #region Properties
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public string ProviderPath
        {
            get
            {
                return _providerPath;
            }
        }

        public string ObjectQualifier
        {
            get
            {
                return _objectQualifier;
            }
        }

        public string DatabaseOwner
        {
            get
            {
                return _databaseOwner;
            }
        }

        public string NamePrefix
        {
            get
            {
                return DatabaseOwner + ObjectQualifier + ModuleQualifier;
            }
        }
        #endregion

        public override void InsertContentItem(string description, string thumbnailUrl, string linkUrl, DateTime startDate, DateTime? endDate, int tabModuleId, string title, string positionThumbnailUrl, int sortOrder)
        {
            StringBuilder sql = new StringBuilder(200);
            sql.AppendFormat(CultureInfo.InvariantCulture, "insert into {0}ContentItem(Description, LinkUrl, StartDate, EndDate, TabModuleId, Title, ThumbnailUrl, PositionThumbnailUrl, SortOrder) ", NamePrefix);
            sql.AppendFormat(CultureInfo.InvariantCulture, " values (@description, @linkUrl, @startDate, @endDate, @tabModuleId, @title, @thumbnailUrl, @positionThumbnailUrl, @sortOrder)");

            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql.ToString(), Engage.Utility.CreateVarcharParam("@description", description), Engage.Utility.CreateVarcharParam("@thumbnailUrl", thumbnailUrl), Engage.Utility.CreateVarcharParam("@positionThumbnailUrl", positionThumbnailUrl), Engage.Utility.CreateVarcharParam("@linkUrl", linkUrl, 255), Engage.Utility.CreateDateTimeParam("@startDate", startDate), Engage.Utility.CreateDateTimeParam("@endDate", endDate), Engage.Utility.CreateIntegerParam("@tabModuleId", tabModuleId), Engage.Utility.CreateVarcharParam("@title", title, 255), Engage.Utility.CreateIntegerParam("@sortOrder", sortOrder));
        }

        public override void UpdateContentItem(int contentItemId, string description, string thumbnailUrl, string linkUrl, DateTime startDate, DateTime? endDate, int tabModuleId, string title, string positionThumbnailUrl, int sortOrder)
        {
            StringBuilder sql = new StringBuilder(250);
            sql.AppendFormat(CultureInfo.InvariantCulture, "update {0}ContentItem ", NamePrefix);
            sql.AppendFormat(CultureInfo.InvariantCulture, " SET Description = @description, ");
            sql.AppendFormat(CultureInfo.InvariantCulture, "  LinkUrl = @linkUrl, ");
            sql.AppendFormat(CultureInfo.InvariantCulture, "  ThumbnailUrl = @thumbnailUrl, ");
            sql.AppendFormat(CultureInfo.InvariantCulture, "  StartDate = @startDate, ");
            sql.AppendFormat(CultureInfo.InvariantCulture, "  EndDate = @endDate, ");
            sql.AppendFormat(CultureInfo.InvariantCulture, "  TabModuleId = @tabModuleId, ");
            sql.AppendFormat(CultureInfo.InvariantCulture, "  Title = @title ");
            sql.AppendFormat(CultureInfo.InvariantCulture, ",  PositionThumbnailUrl = @positionThumbnailUrl ");
            sql.AppendFormat(CultureInfo.InvariantCulture, ",  SortOrder = @sortOrder ");
            sql.AppendFormat(CultureInfo.InvariantCulture, " WHERE ContentItemId = @contentItemId");

            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql.ToString(), Engage.Utility.CreateIntegerParam("@contentItemId", contentItemId), Engage.Utility.CreateVarcharParam("@description", description), Engage.Utility.CreateVarcharParam("@thumbnailUrl", thumbnailUrl), Engage.Utility.CreateVarcharParam("@linkUrl", linkUrl, 255), Engage.Utility.CreateDateTimeParam("@startDate", startDate), Engage.Utility.CreateDateTimeParam("@endDate", endDate), Engage.Utility.CreateIntegerParam("@tabModuleId", tabModuleId), Engage.Utility.CreateVarcharParam("@title", title, 255), Engage.Utility.CreateVarcharParam("@positionThumbnailUrl", positionThumbnailUrl), Engage.Utility.CreateIntegerParam("@sortOrder", sortOrder));
        }

        public override void DeleteContentItem(int contentItemId)
        {
            StringBuilder sql = new StringBuilder(150);
            sql.AppendFormat(CultureInfo.InvariantCulture, "delete from {0}ContentItem where ContentItemId = @contentItemId", NamePrefix);

            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql.ToString(), Engage.Utility.CreateIntegerParam("@contentItemId", contentItemId));
        }

        public override IDataReader GetContentItem(int contentItemId)
        {
            StringBuilder sql = new StringBuilder(300);
            sql.AppendFormat(CultureInfo.InvariantCulture, "select ContentItemId, Description, ThumbnailUrl,  LinkUrl, StartDate, EndDate, TabModuleId, Title, PositionThumbnailUrl, SortOrder ");
            sql.AppendFormat(CultureInfo.InvariantCulture, " from {0}ContentItem ", NamePrefix);
            sql.AppendFormat(CultureInfo.InvariantCulture, " where contentItemId = @contentItemId");

            return SqlHelper.ExecuteReader(ConnectionString, CommandType.Text, sql.ToString(), Engage.Utility.CreateIntegerParam("@contentItemId", contentItemId));
        }

        public override DataSet GetContentItems(int tabModuleId)
        {
            return GetContentItems(tabModuleId, false);
        }

        /// <summary>
        /// Gets all of the content items for the given <paramref name="tabModuleId"/>, getting either only items which have started but not ended, or all items if <paramref name="getOutdatedItems"/> is true.
        /// </summary>
        /// <param name="tabModuleId">The tab module id.</param>
        /// <param name="getOutdatedItems">if set to <c>true</c> gets all content items, regardless of their start date or end date, otherwise only returns items that have started but not ended.</param>
        /// <returns>All of the content items for the given <paramref name="tabModuleId"/></returns>
        public override DataSet GetContentItems(int tabModuleId, bool getOutdatedItems)
        {
            StringBuilder sql = new StringBuilder(200);
            sql.AppendFormat(CultureInfo.InvariantCulture, "select ContentItemId, Description, LinkUrl, ThumbnailUrl, StartDate, EndDate, TabModuleId, Title, PositionThumbnailUrl, SortOrder ");
            sql.AppendFormat(CultureInfo.InvariantCulture, " from {0}ContentItem where TabModuleId = @tabModuleId ", NamePrefix);
            if (!getOutdatedItems)
            {
                sql.AppendFormat(CultureInfo.InvariantCulture, " and StartDate < GETDATE() and (EndDate > GETDATE() or enddate is null) ");
            }
            sql.AppendFormat(CultureInfo.InvariantCulture, " ORDER BY SortOrder, StartDate, Title");

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql.ToString(), Engage.Utility.CreateIntegerParam("@tabModuleId", tabModuleId));
        }
    }
}