// <copyright file="DatePickerOptions.cs" company="Engage Software">
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
    using System.Diagnostics;
    using System.Globalization;
    using System.Web.Script.Serialization;
    using DotNetNuke.Services.Localization;

    /// <summary>
    /// The options and settings to send to the DatePicker plugin
    /// </summary>
    public class DatePickerOptions
    {
        /// <summary>
        /// Backing field for <see cref="CloseText"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string closeText;

        /// <summary>
        /// Backing field for <see cref="CurrentText"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string currentText;

        /// <summary>
        /// Backing field for <see cref="DateFormat"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string dateFormat;

        /// <summary>
        /// Backing field for <see cref="GetDayNames"/> and <see cref="SetDayNames"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] dayNames;

        /// <summary>
        /// Backing field for <see cref="GetDayNamesMin"/> and <see cref="SetDayNamesMin"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] dayNamesMin;

        /// <summary>
        /// Backing field for <see cref="GetDayNamesShort"/> and <see cref="SetDayNamesShort"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] dayNamesShort;

        /// <summary>
        /// Backing field for <see cref="FirstDay"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int firstDay;

        /// <summary>
        /// Backing field for <see cref="IsRightToLeft"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool isRightToLeft;

        /// <summary>
        /// Backing field for <see cref="GetMonthNames"/> and <see cref="SetMonthNames"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] monthNames;

        /// <summary>
        /// Backing field for <see cref="GetMonthNamesShort"/> and <see cref="SetMonthNamesShort"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] monthNamesShort;

        /// <summary>
        /// Backing field for <see cref="NextText"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string nextText;

        /// <summary>
        /// Backing field for <see cref="PreviousText"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string previousText;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatePickerOptions"/> class.
        /// </summary>
        /// <param name="displayCulture">The culture which should control the date format.</param>
        /// <param name="resourceFile">The resource file from which strings should be retrieved.</param>
        public DatePickerOptions(CultureInfo displayCulture, string resourceFile)
        {
            this.closeText = Localization.GetString("CalendarCloseText.Text", resourceFile);
            this.currentText = Localization.GetString("CalendarCurrentText.Text", resourceFile);
            this.nextText = Localization.GetString("CalendarNextText.Text", resourceFile);
            this.previousText = Localization.GetString("CalendarPrevText.Text", resourceFile);

            DateTimeFormatInfo dateTimeFormat = displayCulture.DateTimeFormat;
            this.dateFormat = ConvertToDatePickerFormatString(dateTimeFormat.ShortDatePattern);
            this.dayNames = dateTimeFormat.DayNames;
            this.dayNamesShort = dateTimeFormat.AbbreviatedDayNames;
            this.dayNamesMin = dateTimeFormat.ShortestDayNames;
            this.firstDay = (int)dateTimeFormat.FirstDayOfWeek;
            this.monthNames = dateTimeFormat.MonthNames;
            this.monthNamesShort = dateTimeFormat.AbbreviatedMonthNames;
            
            this.isRightToLeft = displayCulture.TextInfo.IsRightToLeft;
        }

        /// <summary>Gets or sets Display text for close link</summary>
        public string CloseText
        {
            get
            {
                return this.closeText;
            }

            set
            {
                this.closeText = value;
            }
        }

        /// <summary>Gets or sets Display text for current month link</summary>
        public string CurrentText
        {
            get
            {
                return this.currentText;
            }

            set
            {
                this.currentText = value;
            }
        }

        /// <summary>Gets or sets See format options on parseDate</summary>
        public string DateFormat
        {
            get
            {
                return this.dateFormat;
            }

            set
            {
                this.dateFormat = value;
            }
        }

        /// <summary>Gets or sets The first day of the week, Sun = 0, Mon = 1, ...</summary>
        public int FirstDay
        {
            get
            {
                return this.firstDay;
            }

            set
            {
                this.firstDay = value;
            }
        }

        /// <summary>Gets or sets a value indicating whether it is a right-to-left language</summary>
        public bool IsRightToLeft
        {
            get
            {
                return this.isRightToLeft;
            }

            set
            {
                this.isRightToLeft = value;
            }
        }

        /// <summary>
        /// Gets or sets Display text for next month link
        /// </summary>
        public string NextText
        {
            get
            {
                return this.nextText;
            }

            set
            {
                this.nextText = value;
            }
        }

        /// <summary>
        /// Gets or sets Display text for previous month link
        /// </summary>
        public string PreviousText
        {
            get
            {
                return this.previousText;
            }

            set
            {
                this.previousText = value;
            }
        }

        /// <summary>
        /// Gets the list of day name to use for formatting
        /// </summary>
        /// <returns>The list of day names</returns>
        public string[] GetDayNames()
        {
            return this.dayNames;
        }

        /// <summary>
        /// Gets the list of column headings for days, starting with <see cref="DayOfWeek.Sunday"/>
        /// </summary>
        /// <returns>The list of column headings for days</returns>
        public string[] GetDayNamesMin()
        {
            return this.dayNamesMin;
        }

        /// <summary>
        /// Gets the list of short days names to use for formatting
        /// </summary>
        /// <returns>The list of short day names</returns>
        public string[] GetDayNamesShort()
        {
            return this.dayNamesShort;
        }

        /// <summary>
        /// Gets the names of the months for drop-down and formatting
        /// </summary>
        /// <returns>The list of the month names.</returns>
        public string[] GetMonthNames()
        {
            return this.monthNames;
        }

        /// <summary>
        /// Gets the list of short month names for formatting
        /// </summary>
        /// <returns>The list of short month names.</returns>
        public string[] GetMonthNamesShort()
        {
            return this.monthNamesShort;
        }

        /// <summary>
        /// Sets the list of day names to use for formatting
        /// </summary>
        /// <param name="dayNamesList">The list of day names.</param>
        public void SetDayNames(string[] dayNamesList)
        {
            this.dayNames = dayNamesList;
        }

        /// <summary>
        /// Sets the list of column headings for days, starting with <see cref="DayOfWeek.Sunday"/>
        /// </summary>
        /// <param name="dayNamesList">The list of day column headings.</param>
        public void SetDayNamesMin(string[] dayNamesList)
        {
            this.dayNamesMin = dayNamesList;
        }

        /// <summary>
        /// Sets the list of short days names to use for formatting
        /// </summary>
        /// <param name="dayNamesList">The list of short day names.</param>
        public void SetDayNamesShort(string[] dayNamesList)
        {
            this.dayNamesShort = dayNamesList;
        }

        /// <summary>
        /// Sets the names of the months for drop-down and formatting
        /// </summary>
        /// <param name="monthNamesList">The list of the month names..</param>
        public void SetMonthNames(string[] monthNamesList)
        {
            this.monthNames = monthNamesList;
        }

        /// <summary>
        /// Sets the list of short month names for formatting
        /// </summary>
        /// <param name="value">The list of short month names.</param>
        public void SetMonthNamesShort(string[] value)
        {
            this.monthNamesShort = value;
        }

        /// <summary>
        /// Converts this instance into a JSON string.
        /// </summary>
        /// <returns>The serialized JSON string</returns>
        public string Serialize()
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new DatePickerOptionsConverter() });
            return serializer.Serialize(this);
        }

        /// <summary>
        /// Converts the .NET <see cref="DateTime"/> format string into the format required by the DatePicker plugin.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        ///     <listheader><description>The format can be combinations of the following:</description></listheader>
        ///     <item><term>d</term><description>day of month (no leading zero)</description></item>
        ///     <item><term>dd</term><description>day of month (two digit)</description></item>
        ///     <item><term>D</term><description>day name short</description></item>
        ///     <item><term>DD</term><description>day name long</description></item>
        ///     <item><term>m</term><description>month of year (no leading zero)</description></item>
        ///     <item><term>mm</term><description>month of year (two digit)</description></item>
        ///     <item><term>M</term><description>month name short</description></item>
        ///     <item><term>MM</term><description>month name long</description></item>
        ///     <item><term>y</term><description>year (two digit)</description></item>
        ///     <item><term>yy</term><description>year (four digit)</description></item>
        ///     <item><term>@</term><description>Unix timestamp (ms since 01/01/1970)</description></item>
        ///     <item><term>'...'</term><description>literal text</description></item>
        ///     <item><term>''</term><description>single quote</description></item>
        /// </list>
        /// </remarks>
        /// <param name="pattern">The <see cref="DateTime"/> format pattern.</param>
        /// <returns>The given <paramref name="pattern"/> converted into the format required by the DatePicker plugin</returns>
        private static string ConvertToDatePickerFormatString(string pattern)
        {
            const string DummyText = "`~!@@!~`";

            return pattern
                .Replace("dddd", "DD")
                .Replace("ddd", "D")
                .Replace("MMMM", DummyText)
                .Replace("MMM", DummyText + DummyText)
                .Replace("MM", "mm")
                .Replace("M", "m")
                .Replace(DummyText + DummyText, "M")
                .Replace(DummyText, "MM")
                .Replace("yyyy", DummyText)
                .Replace("yy", "y")
                .Replace(DummyText, "yy");
        }
    }
}