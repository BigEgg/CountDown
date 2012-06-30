using System.Windows.Media.Imaging;

namespace BigEgg.Skins
{
    /// <summary>
    /// Use to save the infomation of the skin.
    /// </summary>
    public interface ISkin
    {
        /// <summary>
        /// The name of the Skin.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// the description of the Skin.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The assebmly Name of the Skin.
        /// </summary>
        string AssemblyName { get; }

        /// <summary>
        /// The Uri of the Root Resource Dictionary file (*.xaml), Which contains colors and styles for controls.
        /// </summary>
        string Uri { get; }

        /// <summary>
        /// The graph for the demo of the Skin.
        /// </summary>
        BitmapImage DemoGraph { get; }
    }
}
