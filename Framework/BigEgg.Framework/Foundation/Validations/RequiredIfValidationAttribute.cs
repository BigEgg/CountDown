using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BigEgg.Framework.Foundation.Validations
{
    /// <summary>
    /// Specifies that a data field value is required if other Property is is "EqualTo" (or other operator which set) to another dependent data.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RequiredIfAttribute : IsAttribute
    {
        /// <summary>
        /// The property name which is Required.
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// The property name which need to be compared.
        /// </summary>
        public string DependentPropertyName { get; set; }


        /// <summary>
        /// Initializes a new instance of the RequiredIfAttribute class with the specified property name and object to compare and the Operator value.
        /// </summary>
        /// <param name="propertyName">The specified property name which is Required.</param>
        /// <param name="dependentPropertyName">The specified object name which need to be compared.</param>
        /// <param name="dependentObject">The specified object to compare.</param>
        /// <param name="operator">The compare operator.</param>
        public RequiredIfAttribute(string propertyName, string dependentPropertyName, 
            object dependentObject, Operator @operator = Operator.EqualTo)
            : base (dependentObject,@operator)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) { throw new ArgumentException("propertyName"); }
            if (string.IsNullOrWhiteSpace(dependentPropertyName)) { throw new ArgumentException("dependentPropertyName"); }

            PropertyName = propertyName;
            DependentPropertyName = dependentPropertyName;
        }

        /// <summary>
        /// Validation method.
        /// </summary>
        /// <param name="value">The class object.</param>
        /// <returns>True if the data field pass the validation. Otherwise, return false.</returns>
        public override bool IsValid(object value)
        {
            Type type =value.GetType();
            PropertyInfo property = type.GetProperty(PropertyName);
            PropertyInfo dependentProperty = type.GetProperty(DependentPropertyName);

            if (property == null) { throw new ArgumentException("PropertyName"); }
            if (dependentProperty == null) { throw new ArgumentException("DependentPropertyName"); }

            bool check = base.IsValid(dependentProperty.GetValue(null, null));
            if (!check)     //  DependentPropertyName is not "Operator" to the DependentObject
                return true;

            object propertyValue = property.GetValue(null, null);
            if (propertyValue is string)
            {
                return !string.IsNullOrWhiteSpace(propertyValue as string);
            }
            else
            {
                return (propertyValue != null);
            }
        }
    }
}
