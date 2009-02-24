// <copyright file="CycleOptionsConverter.cs" company="Engage Software">
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
    using System.Collections.ObjectModel;
    using System.Web.Script.Serialization;

    /// <summary>
    /// Implementation of <see cref="JavaScriptConverter"/> for <see cref="CycleOptions"/>
    /// </summary>
    public class CycleOptionsConverter : JavaScriptConverter
    {
        /// <summary>
        /// Gets a collection of the supported types
        /// </summary>
        /// <value>An object that implements <see cref="IEnumerable{T}"/> that represents the types supported by the converter. </value>
        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                return new ReadOnlyCollection<Type>(new Type[] { typeof(CycleOptions) });
            }
        }

        /// <summary>
        /// Converts the provided dictionary into an object of the specified type. 
        /// </summary>
        /// <param name="dictionary">An <see cref="IDictionary{TKey,TValue}"/> instance of property data stored as name/value pairs. </param>
        /// <param name="type">The type of the resulting object.</param>
        /// <param name="serializer">The <see cref="JavaScriptSerializer"/> instance. </param>
        /// <returns>The deserialized object. </returns>
        /// <exception cref="InvalidOperationException">We only serialize <see cref="CycleOptions"/></exception>
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new InvalidOperationException("We only serialize CycleOptions");
        }

        /// <summary>
        /// Builds a dictionary of name/value pairs
        /// </summary>
        /// <param name="obj">The object to serialize. </param>
        /// <param name="serializer">The object that is responsible for the serialization. </param>
        /// <returns>An object that contains key/value pairs that represent the object�s data. </returns>
        /// <exception cref="InvalidOperationException"><paramref name="obj"/> must be of the <see cref="CycleOptions"/> type</exception>
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            CycleOptions opts = obj as CycleOptions;
            if (opts == null)
            {
                throw new InvalidOperationException("object must be of the CycleOptions type");
            }

            IDictionary<string, object> cycleOptions = new Dictionary<string, object>(24);
            cycleOptions.Add("fx", opts.TransitionEffects.ToString());
            cycleOptions.Add("timeout", opts.MillisecondsBetweenTransitions);
            cycleOptions.Add("continuous", opts.Continuous);
            cycleOptions.Add("speed", opts.TransitionSpeed);
            cycleOptions.Add("speedIn", opts.TransitionInSpeed);
            cycleOptions.Add("speedOut", opts.TransitionOutSpeed);
            cycleOptions.Add("next", opts.NextButton);
            cycleOptions.Add("prev", opts.PreviousButton);
            cycleOptions.Add("pager", opts.PagerContainer);
            cycleOptions.Add("pagerEvent", opts.PagerEvent);
            cycleOptions.Add("height", opts.ContainerHeight.IsEmpty ? "auto" : opts.ContainerHeight.ToString());
            cycleOptions.Add("startingSlide", opts.StartingSlideIndex);
            cycleOptions.Add("sync", opts.SimultaneousInOutTransitions);
            cycleOptions.Add("random", opts.RandomOrder);
            cycleOptions.Add("fit", opts.SlidesFitContainer);
            cycleOptions.Add("containerResize", opts.ContainerResize);
            cycleOptions.Add("pause", opts.PauseOnHover);
            cycleOptions.Add("pauseOnPagerHover", opts.PauseOnPagerHover);
            cycleOptions.Add("autostop", opts.AutoStop);
            cycleOptions.Add("autostopCount", opts.AutoStopCount);
            cycleOptions.Add("delay", opts.InitialDelay);
            cycleOptions.Add("cleartype", opts.ClearType);
            cycleOptions.Add("nowrap", opts.Nowrap);
            cycleOptions.Add("fastOnEvent", opts.ManuallyTriggeredTransitionSpeed);

            return cycleOptions;
        }
    }
}