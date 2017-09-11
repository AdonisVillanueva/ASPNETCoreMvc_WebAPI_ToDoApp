using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Helpers
{
    /// <summary>
    /// Custom DynamicObject as a wrapper for inspecting the value via reflection to allow unit testing objectresult in controllers
    /// </summary>
    public class DynamicObjectResultValue : DynamicObject, IEquatable<DynamicObjectResultValue>
    {
        private readonly object value;

        /// <summary>
        /// DynamicObjectResultValue
        /// </summary>
        public DynamicObjectResultValue(object value)
        {
            this.value = value;
        }

        /// <summary>
        /// Operators
        /// </summary>
        #region Operators
        public static bool operator ==(DynamicObjectResultValue a, DynamicObjectResultValue b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }
            // If one is null, but not both, return false.
            if (ReferenceEquals((object)a, null) || ReferenceEquals((object)b, null))
            {
                return false;
            }
            // Return true if the fields match:
            return a.value == b.value;
        }

        /// <summary>
        /// Operators
        /// </summary>
        public static bool operator !=(DynamicObjectResultValue a, DynamicObjectResultValue b)
        {
            return !(a == b);
        }
        #endregion

        /// <summary>
        /// GetDynamicMemberNames
        /// </summary>
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return value.GetType().GetProperties().Select(p => p.Name);
        }

        /// <summary>
        /// TryGetMember
        /// </summary>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            //initialize value
            result = null;
            //Search possible matches and get its value
            var property = value.GetType().GetProperty(binder.Name);
            if (property != null)
            {
                // If the property is found, 
                // set the value parameter and return true. 
                var propertyValue = property.GetValue(value, null);
                result = propertyValue;
                return true;
            }
            // Otherwise, return false. 
            return false;
        }

        /// <summary>
        /// Equals(object obj)
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is DynamicObjectResultValue)
                return Equals(obj as DynamicObjectResultValue);
            // If parameter is null return false.
            if (ReferenceEquals(obj, null)) return false;
            // Return true if the fields match:
            return this.value == obj;
        }

        /// <summary>
        /// Equals(DynamicObjectResultValue other)
        /// </summary>
        public bool Equals(DynamicObjectResultValue other)
        {
            // If parameter is null return false.
            if (ReferenceEquals(other, null)) return false;
            // Return true if the fields match:
            return this.value == other.value;
        }

        /// <summary>
        /// GetHashCode()
        /// </summary>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// ToString()
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}", value);
        }
    }
}
