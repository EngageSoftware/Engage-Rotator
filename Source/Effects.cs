// <copyright file="Effects.cs" company="Engage Software">
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
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The transition effects available to the Cycle plugin
    /// </summary>
    [Flags]
    public enum Effects
    {
        /// <summary>
        /// No effect.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// A fade of opacity
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "fade", Justification = "Name needs to match name in Cycle plugin")]
        fade = 0x1,

        /// <summary>
        /// Animates position up
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "scroll", Justification = "Name needs to match name in Cycle plugin")]
        scrollUp = 0x2,

        /// <summary>
        /// Animates position down
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "scroll", Justification = "Name needs to match name in Cycle plugin")]
        scrollDown = 0x4,

        /// <summary>
        /// Animates position left
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "scroll", Justification = "Name needs to match name in Cycle plugin")]
        scrollLeft = 0x8,

        /// <summary>
        /// Animates position right
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "scroll", Justification = "Name needs to match name in Cycle plugin")]
        scrollRight = 0x10,

        /// <summary>
        /// Animates position left or right
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Horz", Justification = "Name needs to match name in Cycle plugin")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "scroll", Justification = "Name needs to match name in Cycle plugin")]
        scrollHorz = 0x20,

        /// <summary>
        /// Animates postion up or down
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Vert", Justification = "Name needs to match name in Cycle plugin")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "scroll", Justification = "Name needs to match name in Cycle plugin")]
        scrollVert = 0x40,

        /// <summary>
        /// Animates width from the left side
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "slide", Justification = "Name needs to match name in Cycle plugin")]
        slideX = 0x80,

        /// <summary>
        /// Animates height from the top
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "slide", Justification = "Name needs to match name in Cycle plugin")]
        slideY = 0x100,

        /// <summary>
        /// Animates position down and left, then back behind, like shuffling cards
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "shuffle", Justification = "Name needs to match name in Cycle plugin")]
        shuffle = 0x200,

        /// <summary>
        /// Animates height to top for out transition, from bottom for in transition
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "turn", Justification = "Name needs to match name in Cycle plugin")]
        turnUp = 0x400,

        /// <summary>
        ///  Animates height to bottom for out transition, from top for in transition
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "turn", Justification = "Name needs to match name in Cycle plugin")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "turnDown", Justification = "Not a compound word, intent is the two words 'turn' and 'down'")]
        turnDown = 0x800,

        /// <summary>
        /// Animates width to left for out transition, from right for in transitions
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "turn", Justification = "Name needs to match name in Cycle plugin")]
        turnLeft = 0x1000,

        /// <summary>
        /// Animates width to right for out transition, from left for in transitions
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "turn", Justification = "Name needs to match name in Cycle plugin")]
        turnRight = 0x2000,

        /// <summary>
        /// Animates height and width into center
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "zoom", Justification = "Name needs to match name in Cycle plugin")]
        zoom = 0x4000,

        /// <summary>
        /// Animates height and width into center while animating opacity
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "fade", Justification = "Name needs to match name in Cycle plugin")]
        fadeZoom = 0x8000,

        /// <summary>
        /// Slides to and from right side
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "blind", Justification = "Name needs to match name in Cycle plugin")]
        blindX = 0x10000,

        /// <summary>
        /// Slides to and from bottom
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "blind", Justification = "Name needs to match name in Cycle plugin")]
        blindY = 0x20000,

        /// <summary>
        /// Slides to and from bottom right corner
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "blind", Justification = "Name needs to match name in Cycle plugin")]
        blindZ = 0x40000,

        /// <summary>
        /// Grow horizontally from centered 0 width
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "grow", Justification = "Name needs to match name in Cycle plugin")]
        growX = 0x80000,

        /// <summary>
        /// Grow vertically from centered 0 height
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "grow", Justification = "Name needs to match name in Cycle plugin")]
        growY = 0x100000,

        /// <summary>
        /// Squeeze in both edges horizontally
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "curtain", Justification = "Name needs to match name in Cycle plugin")]
        curtainX = 0x200000,

        /// <summary>
        /// Squeeze in both edges vertically
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "curtain", Justification = "Name needs to match name in Cycle plugin")]
        curtainY = 0x400000,

        /// <summary>
        /// Current slide covered by next sliding from right
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "cover", Justification = "Name needs to match name in Cycle plugin")]
        cover = 0x800000,

        /// <summary>
        /// Next slide uncovered by current sliding to left
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "uncover", Justification = "Name needs to match name in Cycle plugin")]
        uncover = 0x10000000,

        /// <summary>
        /// Move top slide to up and right and fade opacity
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "toss", Justification = "Name needs to match name in Cycle plugin")]
        toss = 0x20000000,

        /// <summary>
        /// Reveal next slide over current slide, from upper left corner
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "wipe", Justification = "Name needs to match name in Cycle plugin")]
        wipe = 0x40000000
    }
}