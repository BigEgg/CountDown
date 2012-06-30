using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace BigEgg.Framework.Foundation.Validations
{
    /// <summary>
    /// Specifies that a string field value contains absolute or relative path information.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class PathAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validation method.
        /// </summary>
        /// <param name="value">The data field.</param>
        /// <returns>True if the data field pass the validation. Otherwise, return false.</returns>
        public override bool IsValid(object value)
        {
            string path = value as String;
            if (String.IsNullOrWhiteSpace(path))
                return false;

            return Path.IsPathRooted(path);
        }
    }
}
