﻿using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using BigEgg.Framework.Foundation;

namespace BigEgg.Framework.Applications
{
    /// <summary>
    /// Represents a recent file.
    /// </summary>
    public sealed class RecentFile : Model, IXmlSerializable
    {
        private bool isPinned;
        private string path;


        /// <summary>
        /// This constructor overload is reserved and should not be used. It is used internally by the XmlSerializer.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public RecentFile() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentFile"/> class.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <exception cref="ArgumentException">The argument path must not be null or empty.</exception>
        public RecentFile(string path)
        {
            if (string.IsNullOrEmpty(path)) { throw new ArgumentException("The argument path must not be null or empty."); }

            this.path = path;
        }


        /// <summary>
        /// Gets or sets a value indicating whether this recent file is pinned.
        /// </summary>
        public bool IsPinned
        {
            get { return this.isPinned; }
            set
            {
                if (this.isPinned != value)
                {
                    this.isPinned = value;
                    RaisePropertyChanged("IsPinned");
                }
            }
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        public string Path { get { return this.path; } }


        XmlSchema IXmlSerializable.GetSchema() { return null; }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (reader == null) { throw new ArgumentNullException("reader"); }

            isPinned = bool.Parse(reader.GetAttribute("IsPinned"));
            this.path = reader.ReadElementContentAsString();
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if (writer == null) { throw new ArgumentNullException("writer"); }

            writer.WriteAttributeString("IsPinned", isPinned.ToString());
            writer.WriteString(path);
        }
    }
}
