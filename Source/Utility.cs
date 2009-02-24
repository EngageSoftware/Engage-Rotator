// <copyright file="Utility.cs" company="Engage Software">
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
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// How content is displayed
    /// </summary>
    public enum DisplayType
    {
        /// <summary>
        /// Content is not displayed
        /// </summary>
        None = 0,

        /// <summary>
        /// Content is displayed by itself
        /// </summary>
        Content,

        /// <summary>
        /// Content is displayed wrapped in a link
        /// </summary>
        Link,

        /// <summary>
        /// Content is displayed wrapped in an event handler that causes the rotator tranistion to another piece of content
        /// </summary>
        RotateContent
    }

    /// <summary>
    /// Utility methods and common, static data for Engage: Rotator
    /// </summary>
    internal static class Utility
    {
        /// <summary>
        /// The virtual path to the desktop module folder
        /// </summary>
        public const string DesktopModuleVirtualPath = "~/DesktopModules/EngageRotator/";

        /// <summary>
        /// The template folder name
        /// </summary>
        public const string StyleTemplatesFolderName = "StyleTemplates/";

        /// <summary>
        /// Adds a style declaration to the specified <see cref="StringBuilder"/> <paramref name="style"/>.
        /// Adds a style with the given <paramref name="styleName"/> and <paramref name="styleValue"/>.
        /// If <paramref name="styleValue"/> does not have a value, nothing is added.
        /// </summary>
        /// <param name="style">The complete style declaration.</param>
        /// <param name="styleName">Style attribute to set, e.g. "width" or "margin-top."</param>
        /// <param name="styleValue">Value of the style in pixels, or null if the style has no value.</param>
        internal static void AddStyle(StringBuilder style, string styleName, int? styleValue)
        {
            if (styleValue.HasValue)
            {
                style.AppendFormat(CultureInfo.InvariantCulture, "{0}:{1}px;", styleName, styleValue.Value);
            }
        }
    }
}