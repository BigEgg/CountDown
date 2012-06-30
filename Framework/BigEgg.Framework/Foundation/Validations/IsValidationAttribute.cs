using System;
using System.ComponentModel.DataAnnotations;

namespace BigEgg.Framework.Foundation.Validations
{
    /// <summary>
    /// Specifies that a data field value is "EqualTo" (or other operator which set) to another dependent data.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class IsAttribute : ValidationAttribute
    {
        /// <summary>
        /// The operator to compare, see <seealso cref="Operator"/>.
        /// </summary>
        public Operator Operator { get; set; }
        /// <summary>
        /// An object to compare.
        /// </summary>
        public object DependentObject { get; set; }
        /// <summary>
        /// Specifies that if the data field pass the validation when it is null.
        /// </summary>
        public bool PassOnNull { get; set; }


        /// <summary>
        /// Initializes a new instance of the IsAttribute class with the specified object to compare and the Operator value.
        /// </summary>
        /// <param name="dependentObject">The specified object to compare</param>
        /// <param name="operator">The compare operator.</param>
        public IsAttribute(object dependentObject, Operator @operator = Operator.EqualTo)
        {
            if (dependentObject == null) { throw new ArgumentException("dependentObject"); }
            if (!TypeCheck(dependentObject)) { throw new ArgumentException("The dependentObject type is not a comparable type."); }

            Operator = @operator;
            DependentObject = dependentObject;

            PassOnNull = false;
        }


        /// <summary>
        /// Validation method.
        /// </summary>
        /// <param name="value">The data field.</param>
        /// <returns>True if the data field pass the validation. Otherwise, return false.</returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                if (PassOnNull) { return true; }
                else { return false; }
            }

            if (!TypeCheck(value)) { throw new ArgumentException("The value type is not a comparable type."); }

            int result = (value as IComparable).CompareTo(DependentObject);

            if (result < 0)
            {
                if ((Operator == Operator.LessThan)
                    || (Operator == Operator.LessThanOrEqualTo)
                    || (Operator == Operator.NotEqualTo))
                    return true;
                else
                    return false;
            }
            else if (result == 0)
            {
                if ((Operator == Operator.EqualTo)
                    || (Operator == Operator.LessThanOrEqualTo)
                    || (Operator == Operator.GreaterThanOrEqualTo))
                    return true;
                else
                    return false;
            }
            else
            {
                if ((Operator == Operator.GreaterThan)
                    || (Operator == Operator.GreaterThanOrEqualTo)
                    || (Operator == Operator.NotEqualTo))
                    return true;
                else
                    return false;
            }
        }


        /// <summary>
        /// The methods for the type check. Use to check if the type is support by the the validation.
        /// </summary>
        /// <param name="value">Input value.</param>
        /// <returns>True if the validation is support. Otherwise, return false.</returns>
        protected virtual bool TypeCheck(object value)
        {
            return (value is IComparable);
        }
    }
}
