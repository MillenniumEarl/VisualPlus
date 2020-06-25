#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: InvalidValue.cs
//
// Copyright (c) 2016 - 2019 VisualPlus <https://darkbyte7.github.io/VisualPlus/>
// All Rights Reserved.
//
// -----------------------------------------------------------------------------------------------------------
//
// GNU General Public License v3.0 (GPL-3.0)
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF
// MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
// This file is subject to the terms and conditions defined in the file
// 'LICENSE.md', which should be in the root directory of the source code package.
//
// -----------------------------------------------------------------------------------------------------------

#endregion License

#region Namespace

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

using VisualPlus.Constants;
using VisualPlus.Extensibility;
using VisualPlus.Utilities.Debugging;

#endregion Namespace

namespace VisualPlus.Attributes
{
    /// <summary>Represents the <see cref="InvalidValue" /> class for attributes.</summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    [DebuggerDisplay(DefaultConstants.DefaultDebuggerDisplay)]
    [Serializable]
    public class InvalidValue : VisualAttribute
    {
        #region Static Fields

        /// <summary>
        ///     Specifies the default value for the <see cref="InvalidValue" />, that represents the current object. This
        ///     <see langword="static" /> field is read-only.
        /// </summary>
        public static readonly InvalidValue Default = new InvalidValue();

        #endregion Static Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="InvalidValue" /> class.</summary>
        /// <param name="triggerValue">The value against which a property value will be compared for validity.</param>
        /// <param name="trigger">The trigger type.</param>
        /// <param name="expectedType">The expected property value type.</param>
        public InvalidValue(object triggerValue, TriggerType trigger = TriggerType.Valid, Type expectedType = null)
        {
            // Determine if type is of the intrinsic type
            if (triggerValue.GetType().IsIntrinsic())
            {
                // Initialize
                Trigger = trigger;

                // Check for expected type
                if (expectedType != null)
                {
                    // Determine expected type to validate into an object
                    if (expectedType.IsDateTime())
                    {
                        // Calculate ticks from the trigger
                        long ticks = Math.Min(Math.Max(0, Convert.ToInt64(triggerValue)), long.MaxValue);

                        // Instantiate a datetime with the ticks
                        TriggerValue = new DateTime(ticks);
                    }
                    else
                    {
                        TriggerValue = triggerValue;
                    }

                    ExpectedType = expectedType;
                }
                else
                {
                    TriggerValue = triggerValue;
                    ExpectedType = triggerValue.GetType();
                }
            }
            else
            {
                throw new ArgumentException("The " + nameof(InvalidValue) + " parameter must be a primitive, string, or DateTime, and must match the type of the attributed property.");
            }
        }

        /// <summary>Initializes a new instance of the <see cref="InvalidValue" /> class.</summary>
        public InvalidValue()
        {
        }

        #endregion Constructors and Destructors

        #region Enums

        /// <summary>A flag used to indicate how the comparison for validity is performed.</summary>
        public enum TriggerType
        {
            /// <summary>Comparison returns TRUE if the property value is <see langword="!=" /> the trigger value .</summary>
            Valid = 0,

            /// <summary>Comparison returns TRUE if the property value is <see langword="==" /> the trigger value </summary>
            Equal = 1,

            /// <summary>Comparison returns TRUE if the property value is <see langword="!=" /> the trigger value.</summary>
            NotEqual = 2,

            /// <summary>Comparison returns TRUE if the property value is <see langword="&lt;=" /> the trigger value.</summary>
            Over = 3,

            /// <summary>Comparison returns TRUE if the property value is <see langword=">=" /> the trigger value .</summary>
            Under = 4
        }

        #endregion Enums

        #region Public Properties

        /// <summary>Gets/sets the expected type that the property value will/should be.</summary>
        public virtual Type ExpectedType { get; protected set; }

        /// <summary>Gets/sets the value that was compared against the trigger value so that the TriggerMessage can be constructed.</summary>
        public virtual object PropertyValue { get; protected set; }

        /// <summary>Gets/sets a flag indicating how the valid status is determined.</summary>
        public virtual TriggerType Trigger { get; protected set; }

        /// <summary>
        ///     Gets the trigger message called by the method performing the validity check, usually if the value is not
        ///     valid.
        /// </summary>
        public virtual string TriggerMessage
        {
            get
            {
                string format;

                // Determine trigger type formatting
                switch (Trigger)
                {
                    case TriggerType.Valid:
                    case TriggerType.Equal:
                        {
                            format = "equal to";
                            break;
                        }

                    case TriggerType.NotEqual:
                        {
                            format = "not equal to";
                            break;
                        }

                    case TriggerType.Over:
                        {
                            format = "greater than";
                            break;
                        }

                    case TriggerType.Under:
                        {
                            format = "less than";
                            break;
                        }

                    default:
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                }

                if (!string.IsNullOrEmpty(format))
                {
                    format = string.Concat("Cannot be ", format, " '{0}'. \r\n      Current value is '{1}'.\r\n");
                }

                // Returns formatted trigger message output
                return !string.IsNullOrEmpty(format) ? string.Format(format, TriggerValue, PropertyValue) : string.Empty;
            }
        }

        /// <summary>Gets/sets the value that will be used to determine if the property value is valid.</summary>
        public virtual object TriggerValue { get; protected set; }

        #endregion Public Properties

        #region Public Methods and Operators

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }

            switch (obj)
            {
                case InvalidValue invalidValue:
                    {
                        bool equal;

                        // Validate the property's
                        if ((invalidValue.DescriptionValue == DescriptionValue) &&
                            (invalidValue.Description == Description) &&
                            (invalidValue.Target == Target) &&
                            (invalidValue.ExpectedType == ExpectedType) &&
                            (invalidValue.TriggerMessage == TriggerMessage) &&
                            (invalidValue.Trigger == Trigger) &&
                            (invalidValue.TriggerValue == TriggerValue))
                        {
                            equal = true;
                        }
                        else
                        {
                            equal = false;
                        }

                        return equal;
                    }

                default:
                    {
                        return false;
                    }
            }
        }

        public override int GetHashCode()
        {
            return DescriptionValue.GetHashCode();
        }

        /// <summary>Determines if this attribute is the default.</summary>
        /// <returns>
        ///     <see langword="true" /> if the attribute is the default value for this attribute class; otherwise,
        ///     <see langword="false" />.
        /// </returns>
        public override bool IsDefaultAttribute()
        {
            return Equals(Default);
        }

        /// <summary>Determines if the specified object is currently valid.</summary>
        /// <param name="source">The value of the property attached to this attribute instance.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public bool IsValid(object source)
        {
            // Variables
            bool result;

            // Save the value for use in the TriggerMessage
            PropertyValue = source;

            // Get the type represented by the object source
            Type valueType = source.GetType();

            // Determine type handling
            if (valueType.IsDateTime())
            {
                // ensure that the trigger value is a datetime
                TriggerValue = ToDateTime();

                // and set the ExpectedType for the following comparison.
                ExpectedType = typeof(DateTime);
            }

            // If the type is what we're expecting, we can compare the objects
            if (valueType == ExpectedType)
            {
                switch (Trigger)
                {
                    case TriggerType.Equal:
                        {
                            result = IsEqual(source, TriggerValue);
                            break;
                        }

                    case TriggerType.Valid:
                    case TriggerType.NotEqual:
                        {
                            result = IsNotEqual(source, TriggerValue);
                            break;
                        }

                    case TriggerType.Over:
                        {
                            result = !GreaterThan(source, TriggerValue);
                            break;
                        }

                    case TriggerType.Under:
                        {
                            result = !LessThan(source, TriggerValue);
                            break;
                        }

                    default:
                        {
                            throw new ArgumentOutOfRangeException(nameof(Trigger), $@"The {nameof(Trigger)} value is out of range.");
                        }
                }
            }
            else
            {
                throw new InvalidOperationException("The property value and trigger value are not of compatible types.");
            }

            return result;
        }

        public override string ToString()
        {
            if (Debugger.IsAttached)
            {
                return this.ToDebug("TargetName");
            }
            else
            {
                return base.ToString();
            }
        }

        #endregion Public Methods and Operators

        #region Methods

        /// <summary>
        ///     Returns a <see cref="bool" /> indicating whether this instance is greater than to a specified
        ///     <see cref="bool" /> object.
        /// </summary>
        /// <param name="source">The object source.</param>
        /// <param name="comparison">The obj to compare to.</param>
        /// <returns>The <see cref="bool" />.</returns>
        protected bool GreaterThan(object source, object comparison)
        {
            var result = false;
            Type objectSourceType = source.GetType();

            if (objectSourceType.IsInteger())
            {
                result = objectSourceType.IsUnsignedInteger() && comparison.GetType().IsUnsignedInteger() ? Convert.ToUInt64(source) > Convert.ToUInt64(comparison) : Convert.ToInt64(source) > Convert.ToInt64(comparison);
            }
            else if (objectSourceType.IsFloatingPoint())
            {
                result = Convert.ToDouble(source) > Convert.ToDouble(comparison);
            }
            else if (objectSourceType.IsDecimal())
            {
                result = Convert.ToDecimal(source) > Convert.ToDecimal(comparison);
            }
            else if (objectSourceType.IsDateTime())
            {
                result = Convert.ToDateTime(source) > Convert.ToDateTime(comparison);
            }
            else if (objectSourceType.IsString())
            {
                result = string.Compare(Convert.ToString(source), Convert.ToString(comparison), StringComparison.Ordinal) > 0;
            }

            return result;
        }

        /// <summary>
        ///     Returns a <see cref="bool" /> indicating whether this instance is equal to a specified <see cref="bool" />
        ///     object.
        /// </summary>
        /// <param name="source">The object source.</param>
        /// <param name="comparison">The obj to compare to.</param>
        /// <returns>The <see cref="bool" />.</returns>
        protected bool IsEqual(object source, object comparison)
        {
            // Variables
            var result = false;
            Type objectSourceType = source.GetType();

            // Determine equal object type is valid
            if (objectSourceType.IsInteger())
            {
                result = objectSourceType.IsUnsignedInteger() && comparison.GetType().IsUnsignedInteger() ? Convert.ToUInt64(source) == Convert.ToUInt64(comparison) : Convert.ToInt64(source) == Convert.ToInt64(comparison);
            }
            else if (objectSourceType.IsFloatingPoint())
            {
                result = Convert.ToDouble(source).Equals(Convert.ToDouble(comparison));
            }
            else if (objectSourceType.IsDecimal())
            {
                result = Convert.ToDecimal(source) == Convert.ToDecimal(comparison);
            }
            else if (objectSourceType.IsDateTime())
            {
                result = Convert.ToDateTime(source) == Convert.ToDateTime(comparison);
            }
            else if (objectSourceType.IsString())
            {
                result = string.Compare(Convert.ToString(source), Convert.ToString(comparison), StringComparison.Ordinal) == 0;
            }

            return result;
        }

        /// <summary>
        ///     Returns a <see cref="bool" /> indicating whether this instance is not equal to a specified <see cref="bool" />
        ///     object.
        /// </summary>
        /// <param name="source">The object source.</param>
        /// <param name="comparison">The obj to compare to.</param>
        /// <returns>The <see cref="bool" />.</returns>
        protected bool IsNotEqual(object source, object comparison)
        {
            return !IsEqual(source, comparison);
        }

        /// <summary>
        ///     Returns a <see cref="bool" /> indicating whether this instance is less than to a specified
        ///     <see cref="bool" /> object.
        /// </summary>
        /// <param name="source">The object source.</param>
        /// <param name="comparison">The obj to compare to.</param>
        /// <returns>The <see cref="bool" />.</returns>
        protected bool LessThan(object source, object comparison)
        {
            var result = false;
            Type objectSourceType = source.GetType();

            if (objectSourceType.IsInteger())
            {
                result = objectSourceType.IsUnsignedInteger() && comparison.GetType().IsUnsignedInteger() ? Convert.ToUInt64(source) < Convert.ToUInt64(comparison) : Convert.ToInt64(source) < Convert.ToInt64(comparison);
            }
            else if (objectSourceType.IsFloatingPoint())
            {
                result = Convert.ToDouble(source) < Convert.ToDouble(comparison);
            }
            else if (objectSourceType.IsDecimal())
            {
                result = Convert.ToDecimal(source) < Convert.ToDecimal(comparison);
            }
            else if (objectSourceType.IsDateTime())
            {
                result = Convert.ToDateTime(source) < Convert.ToDateTime(comparison);
            }
            else if (objectSourceType.IsString())
            {
                result = string.Compare(Convert.ToString(source), Convert.ToString(comparison), StringComparison.Ordinal) < 0;
            }

            return result;
        }

        /// <summary>Parses the <see cref="DateTime" /> using the <see cref="TriggerValue" /> if its an integer.</summary>
        /// <returns>The <see cref="DateTime" />.</returns>
        private DateTime ToDateTime()
        {
            DateTime parseDateTime = new DateTime(0);

            // Determine trigger value type
            if (TriggerValue.GetType().IsInteger())
            {
                // Calculate the ticks
                long ticks = Math.Min(Math.Max(0, Convert.ToInt64(TriggerValue)), long.MaxValue);
                parseDateTime = new DateTime(ticks);
            }
            else if (TriggerValue.GetType().IsDateTime())
            {
                parseDateTime = Convert.ToDateTime(TriggerValue);
            }

            return parseDateTime;
        }

        #endregion Methods
    }
}