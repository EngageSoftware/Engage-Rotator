// <copyright file="AssemblyInfo.cs" company="Engage Software">
// Engage: Rotator - http://www.engagemodules.com
// Copyright (c) 2004-2009
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web.UI;

[assembly: AssemblyTitle("Engage: Rotator")]
[assembly: AssemblyDescription("The Content Rotation Module from Engage Software")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Engage Software")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("Copyright © Engage Software 2009")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]
[assembly: Guid("3d5900ae-111a-45be-96b3-d9e4606ca793")]
[assembly: CLSCompliant(true)]

[assembly: AssemblyVersion("2.0.0.*")]
[assembly: AssemblyFileVersion("2.0.0.0")]

[assembly: WebResource("Engage.Dnn.ContentRotator.JavaScript.jquery.cycle.all.min.js", "text/javascript")]
[assembly: WebResource("Engage.Dnn.ContentRotator.JavaScript.jquery-ui-datepicker-1.5.3.min.js", "text/javascript")]
#if DEBUG
[assembly: WebResource("Engage.Dnn.ContentRotator.JavaScript.jquery.cycle.all.js", "text/javascript")]
[assembly: WebResource("Engage.Dnn.ContentRotator.JavaScript.jquery-ui-datepicker-1.5.3.js", "text/javascript")]
#endif