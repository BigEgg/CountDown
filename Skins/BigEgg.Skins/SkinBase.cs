using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace BigEgg.Skins
{
    public abstract class SkinBase : ISkin 
    {
        private string name;
        private string description;
        private string assemblyName;
        private string uri;
        private BitmapImage demoGraph;

        /// <summary>
        /// Initializes a new instance of the Skin.
        /// </summary>
        /// <param name="name">The name of the Skin.</param>
        /// <param name="description">The description of the Skin.</param>
        /// <param name="graph">The demo graph of the Skin.</param>
        public SkinBase(string name, string description, Bitmap graph)
        {
            if (string.IsNullOrEmpty(name)) { throw new ArgumentException("name"); }
            if (string.IsNullOrEmpty(description)) { throw new ArgumentException("description"); }
            if (graph == null) { throw new ArgumentNullException("graph"); }

            this.name = name;
            this.description = description;
            this.assemblyName = this.GetType().Assembly.FullName.Split(',').First();
            this.uri = "component/Skin.xaml";

            MemoryStream ms = new MemoryStream();
            graph.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            this.demoGraph = new BitmapImage();
            this.demoGraph.BeginInit();
            this.demoGraph.StreamSource = ms;
            this.demoGraph.EndInit(); 
        }

        public string Name { get { return this.name; } }

        public string Description { get { return this.description; } }

        public string AssemblyName { get { return this.assemblyName; } }

        public string Uri { get { return this.uri; } }

        public BitmapImage DemoGraph { get { return this.demoGraph; } }
    }
}
