//Copyright (c) 2004-2008
//by Engage Software ( http://www.engagesoftware.net )

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
//TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
//THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
//CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
//DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Schema;

namespace Engage.Dnn.ContentRotator
{
    internal class TemplateManifest
    {
        #region Properties

        private readonly string TemplateFilePath;

        //[DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _templateName;
        //public string TemplateName
        //{
        //    [DebuggerStepThrough]
        //    get { return _templateName; }
        //}

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _description;
        public string Description
        {
            [DebuggerStepThrough]
            get { return _description; }
        }

        //[DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _stylesheetFilename;
        //public string StylesheetFilename
        //{
        //    [DebuggerStepThrough]
        //    get { return _stylesheetFilename; }
        //}

        public string StylesheetFilePath
        {
            get { return Engage.Utility.HasValue(_stylesheetFilename) ? TemplateFilePath + _stylesheetFilename : string.Empty; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] 
        private readonly string _previewImageFilename;
        public string PreviewImageFilename
        {
            [DebuggerStepThrough]
            get { return _previewImageFilename; }
        }

        //public string PreviewImageFilePath
        //{
        //    get { return Engage.Utility.HasValue(_previewImageFilename) ? TemplateFilePath + _previewImageFilename : string.Empty; }
        //}

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Dictionary<string, string> _settings;
        public IDictionary<string, string> Settings
        {
            [DebuggerStepThrough]
            get { return new Dictionary<string, string>(_settings); }
        }

        #endregion

        private TemplateManifest(string filePath, string templateName)
        {
            _settings = new Dictionary<string, string>();
            _templateName = templateName;
            TemplateFilePath = Utility.DesktopModuleVirtualPath + Utility.StyleTemplatesFolderName + _templateName + "/";
            try
            {
                using (FileStream manifestStream = new FileStream(HostingEnvironment.MapPath(filePath), FileMode.Open, FileAccess.Read))
                {
                    XmlReaderSettings readerSettings = new XmlReaderSettings();
                    readerSettings.IgnoreWhitespace = true;
                    readerSettings.ValidationType = ValidationType.Schema;
                    string schemaUri = (new Uri(HostingEnvironment.MapPath(Utility.DesktopModuleVirtualPath + Utility.StyleTemplatesFolderName + "Manifest.xsd"))).AbsoluteUri;
                    readerSettings.Schemas.Add(string.Empty, schemaUri);
                    using (XmlReader manifestReader = XmlReader.Create(manifestStream, readerSettings))
                    {
                        //manifestReader.WhitespaceHandling = WhitespaceHandling.None;
                        manifestReader.Read();
                        manifestReader.ReadStartElement("RotatorManifest");
                        if (manifestReader.IsStartElement("Description"))
                        {
                            manifestReader.ReadStartElement("Description");
                            _description = manifestReader.ReadContentAsString();
                            if (manifestReader.LocalName == "Description") //needed in case element is self-closing
                            {
                                manifestReader.ReadEndElement(); //</description>
                            }
                        }
                        if (manifestReader.IsStartElement("Preview"))
                        {
                            manifestReader.ReadStartElement("Preview");
                            _previewImageFilename = manifestReader.ReadContentAsString();
                            if (manifestReader.LocalName == "Preview") //needed in case element is self-closing
                            {
                                manifestReader.ReadEndElement(); //</preview>
                            }
                        }
                        if (manifestReader.IsStartElement("Stylesheet"))
                        {
                            manifestReader.ReadStartElement("Stylesheet");
                            _stylesheetFilename = manifestReader.ReadContentAsString();
                            if (manifestReader.LocalName == "Stylesheet") //needed in case element is self-closing
                            {
                                manifestReader.ReadEndElement(); //</stylesheet>
                            }
                        }
                        if (manifestReader.IsStartElement("Settings"))
                        {
                            manifestReader.ReadStartElement("Settings");
                            while (manifestReader.IsStartElement("Setting"))
                            {
                                manifestReader.ReadStartElement("Setting");
                                if (manifestReader.IsStartElement("Name"))
                                {
                                    manifestReader.ReadStartElement("Name");
                                    string name = manifestReader.ReadContentAsString();
                                    manifestReader.ReadEndElement(); //</name>

                                    if (manifestReader.IsStartElement("Value"))
                                    {
                                        manifestReader.ReadStartElement("Value");
                                        string value = manifestReader.ReadContentAsString();
                                        //if element is empty (and self-closing)
                                        if (manifestReader.LocalName == "Value")
                                        {
                                            manifestReader.ReadEndElement(); //</value>
                                        }

                                        _settings.Add(name, value);
                                    }
                                }
                                manifestReader.ReadEndElement(); //</setting>
                            }
                            manifestReader.ReadEndElement(); //</settings>
                        }
                        manifestReader.ReadEndElement(); //</rotatorManifest>
                    }
                }
            }
            catch (FileNotFoundException)
            {
                //if there is no manifest, just return an empty object.
                return;
            }
        }

        /// <summary>
        /// Creates a representation of the supplied template
        /// </summary>
        /// <param name="templateName">The name of the template</param>
        /// <exception cref="IOException">If the file cannot be accessed</exception>
        /// <exception cref="XmlSchemaValidationException">If the file is not in a valid format</exception>
        public static TemplateManifest CreateTemplateManifest(string templateName)
        {
            return new TemplateManifest(Utility.DesktopModuleVirtualPath + Utility.StyleTemplatesFolderName + templateName + "/Manifest.xml", templateName);
        }
    }
}