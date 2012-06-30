namespace BigEgg.Framework
 {
    /// <summary>
    /// Configuration settings for the BigEgg Application Framework.
    /// </summary>
    public static class BEConfiguration
    {
#if (DEBUG)
        private static bool debug = true;
#else
        private static bool debug = false;
#endif

        /// <summary>
        /// Gets or sets a value indicating whether the application should run in Debug mode.
        /// </summary>
        /// <remarks>
        /// The Debug mode helps to find errors in the application but it might reduce the
        /// performance.
        /// </remarks>
        public static bool Debug
        {
            get { return debug; }
            set { debug = value; }
        }
    }
}
