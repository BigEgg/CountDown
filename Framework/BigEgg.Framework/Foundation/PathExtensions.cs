using System;
using System.IO;

namespace BigEgg.Framework.Foundation
{
    /// <summary>
    /// Extends the <see cref="Path"/> class with new methods.
    /// </summary>
    public static class PathExtensions
    {
        /// <summary>
        /// Get the relative path fo the specific two path.
        /// </summary>
        /// <param name="fromPath">Contains the directory that defines the start of the relative path.</param> 
        /// <param name="toPath">Contains the path that defines the endpoint of the relative path.</param> 
        /// <returns>The relative path from the start directory to the end path.</returns> 
        /// <exception cref="ArgumentNullException"></exception> 
        /// <example>
        /// @"..\..\regedit.exe" = GetRelativePath(@"D:\Windows\Web\Wallpaper\", @"D:\Windows\regedit.exe" );
        /// </example>
        public static string GetRelativePath(string fromPath, string toPath)
        {
            if (String.IsNullOrEmpty(fromPath)) { throw new ArgumentNullException("fromPath"); }
            if (!Path.IsPathRooted(fromPath)) { throw new ArgumentException("fromPath"); }

            if (String.IsNullOrEmpty(toPath)) { throw new ArgumentNullException("toPath"); }
            if (!Path.IsPathRooted(toPath)) { throw new ArgumentException("toPath"); }

            Uri fromUri = new Uri(fromPath);
            Uri toUri = new Uri(toPath);

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            String relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            return relativePath.Replace('/', Path.DirectorySeparatorChar);
        }
    }
}
