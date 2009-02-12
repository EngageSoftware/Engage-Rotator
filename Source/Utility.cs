//Copyright (c) 2004-2008
//by Engage Software ( http://www.engagesoftware.net )

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
//TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
//THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
//CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
//DEALINGS IN THE SOFTWARE.

using System.Globalization;
using System.Text;

namespace Engage.Dnn.ContentRotator
{
    public enum DisplayType
	{
        None = 0,
        Content,
        Link,
        RotateContent
    }

    public enum ControlPlacement
    {
        Above = 0,
        Below
    }

    internal static class Utility
    {
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

        public const string DesktopModuleVirtualPath = "~/DesktopModules/EngageRotator/";
        public const string StyleTemplatesFolderName = "StyleTemplates/";
    }
}