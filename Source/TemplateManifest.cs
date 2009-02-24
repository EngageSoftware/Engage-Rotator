// <copyright file="TemplateManifest.cs" company="Engage Software">
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
    using System.Diagnostics;
    using System.IO;
    using System.Web.Hosting;
    using System.Xml;
    using System.Xml.Schema;

    /// <summary>
    /// Represents a template manifest
    /// </summary>
    internal class TemplateManifest
    {
        /// <summary>
        /// Backing field for <see cref="Description"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string description;

        /// <summary>
        /// Backing field for <see cref="PreviewImageFilename"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string previewImageFilename;

        /// <summary>
        /// Backing field for <see cref="StylesheetFilePath"/>
        /// </summary>
        private readonly string stylesheetFilename;

        /// <summary>
        /// The name of the template
        /// </summary>
        private readonly string templateName;

        /// <summary>
        /// Backing field for <see cref="StylesheetFilePath"/>
        /// </summary>
        private readonly string templateFilePath;

        /// <summary>
        /// Backing field for <see cref="Settings"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Dictionary<string, string> settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateManifest"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="templateName">Name of the template.</param>
        private TemplateManifest(string filePath, string templateName)
        {
            this.settings = new Dictionary<string, string>();
            this.templateName = templateName;
            this.templateFilePath = Utility.DesktopModuleVirtualPath + Utility.StyleTemplatesFolderName
                                    + this.templateName + "/";
            try
            {
                using (
                        FileStream manifestStream = new FileStream(
                                HostingEnvironment.MapPath(filePath), FileMode.Open, FileAccess.Read))
                {
                    XmlReaderSettings readerSettings = new XmlReaderSettings();
                    readerSettings.IgnoreWhitespace = true;
                    readerSettings.ValidationType = ValidationType.Schema;
                    string schemaUri =
                            (new Uri(
                                    HostingEnvironment.MapPath(
                                            Utility.DesktopModuleVirtualPath + Utility.StyleTemplatesFolderName
                                            + "Manifest.xsd"))).AbsoluteUri;
                    readerSettings.Schemas.Add(string.Empty, schemaUri);
                    using (XmlReader manifestReader = XmlReader.Create(manifestStream, readerSettings))
                    {
                        // manifestReader.WhitespaceHandling = WhitespaceHandling.None;
                        manifestReader.Read();
                        manifestReader.ReadStartElement("RotatorManifest");
                        if (manifestReader.IsStartElement("Description"))
                        {
                            manifestReader.ReadStartElement("Description");
                            this.description = manifestReader.ReadContentAsString();
                            if (manifestReader.LocalName == "Description")
                            {
                                // needed in case element is self-closing
                                manifestReader.ReadEndElement(); // </description>
                            }
                        }

                        if (manifestReader.IsStartElement("Preview"))
                        {
                            manifestReader.ReadStartElement("Preview");
                            this.previewImageFilename = manifestReader.ReadContentAsString();
                            if (manifestReader.LocalName == "Preview")
                            {
                                // needed in case element is self-closing
                                manifestReader.ReadEndElement(); // </preview>
                            }
                        }

                        if (manifestReader.IsStartElement("Stylesheet"))
                        {
                            manifestReader.ReadStartElement("Stylesheet");
                            this.stylesheetFilename = manifestReader.ReadContentAsString();
                            if (manifestReader.LocalName == "Stylesheet")
                            {
                                // needed in case element is self-closing
                                manifestReader.ReadEndElement(); // </stylesheet>
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
                                    manifestReader.ReadEndElement(); // </name>

                                    if (manifestReader.IsStartElement("Value"))
                                    {
                                        manifestReader.ReadStartElement("Value");
                                        string value = manifestReader.ReadContentAsString();

                                        // if element is empty (and self-closing)
                                        if (manifestReader.LocalName == "Value")
                                        {
                                            manifestReader.ReadEndElement(); // </value>
                                        }

                                        this.settings.Add(name, value);
                                    }
                                }

                                manifestReader.ReadEndElement(); // </setting>
                            }

                            manifestReader.ReadEndElement(); // </settings>
                        }

                        manifestReader.ReadEndElement(); // </rotatorManifest>
                    }
                }
            }
            catch (FileNotFoundException)
            {
                // if there is no manifest, just return an empty object.
                return;
            }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            [DebuggerStepThrough]
            get
            {
                return this.description;
            }
        }

        /// <summary>
        /// Gets the preview image filename.
        /// </summary>
        /// <value>The preview image filename.</value>
        public string PreviewImageFilename
        {
            [DebuggerStepThrough]
            get
            {
                return this.previewImageFilename;
            }
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public IDictionary<string, string> Settings
        {
            [DebuggerStepThrough]
            get
            {
                return new Dictionary<string, string>(this.settings);
            }
        }

        /// <summary>
        /// Gets the stylesheet file path.
        /// </summary>
        /// <value>The stylesheet file path.</value>
        public string StylesheetFilePath
        {
            get
            {
                return Engage.Utility.HasValue(this.stylesheetFilename)
                               ? this.templateFilePath + this.stylesheetFilename
                               : string.Empty;
            }
        }

        /// <summary>
        /// Creates a representation of the supplied template
        /// </summary>
        /// <param name="templateName">The name of the template</param>
        /// <returns>A template manifest</returns>
        /// <exception cref="IOException">If the file cannot be accessed</exception>
        /// <exception cref="XmlSchemaValidationException">If the file is not in a valid format</exception>
        public static TemplateManifest CreateTemplateManifest(string templateName)
        {
            return
                    new TemplateManifest(
                            Utility.DesktopModuleVirtualPath + Utility.StyleTemplatesFolderName + templateName
                            + "/Manifest.xml",
                            templateName);
        }
    }
}