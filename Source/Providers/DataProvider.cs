//Copyright (c) 2004-2008
//by Engage Software ( http://www.engagesoftware.net )

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
//TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
//THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
//CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
//DEALINGS IN THE SOFTWARE.

using System;
using System.Data;
using System.Diagnostics;

namespace Engage.Dnn.ContentRotator
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// An abstract cls for the data access layer
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    public abstract class DataProvider
    {
        #region "Shared/Static Methods"
        // singleton reference to the instantiated object 
        private static readonly DataProvider objProvider = (DataProvider)DotNetNuke.Framework.Reflection.CreateObject("data", "Engage.Dnn.ContentRotator", "");

        // return the provider
        [DebuggerStepThrough]
        public static DataProvider Instance()
        {
            return objProvider;
        }
        #endregion

        #region "Abstract methods"
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "7#")]
        public abstract void InsertContentItem(string description, string thumbnailUrl, string linkUrl, DateTime startDate, DateTime? endDate, int tabModuleId, string title, string positionThumbnailUrl, int sortOrder);
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "3#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "8#")]
        public abstract void UpdateContentItem(int contentItemId, string description, string thumbnailUrl, string linkUrl, DateTime startDate, DateTime? endDate, int tabModuleId, string title, string positionThumbnailUrl, int sortOrder);
        public abstract void DeleteContentItem(int contentItemId);
        public abstract IDataReader GetContentItem(int contentItemId);
        public abstract DataSet GetContentItems(int tabModuleId);
        public abstract DataSet GetContentItems(int tabModuleId, bool getOutdatedItems);
        #endregion
    }
}