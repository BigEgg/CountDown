using System;
using System.ComponentModel.DataAnnotations;

namespace BigEgg.Framework.Foundation.Validations
{
    /// <summary>
    /// Specifies that a data field value is "GreaterThanOrEqualTo" (or other operator which set) to another dependent data.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class GreaterThanOrEqualToAttribute : IsAttribute
    {
        /// <summary>
        /// Initializes a new instance of the GreaterThanOrEqualToAttribute class with the specified object to compare.
        /// </summary>
        /// <param name="dependentObject">The specified object to compare</param>
        public GreaterThanOrEqualToAttribute(object dependentObject)
            : base(dependentObject, Operator.GreaterThanOrEqualTo)
        {
        }
    }
}
