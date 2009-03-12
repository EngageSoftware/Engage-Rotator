// <copyright file="DisplayType.cs" company="Engage Software">
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
}