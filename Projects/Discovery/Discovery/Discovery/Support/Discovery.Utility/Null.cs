/*************************************************************************************************
 ** FILE:	Null.cs
 ** DATE:	01/08/2004
 ** AUTHOR:	Lee Spring
 **
 ** COPYRIGHT:
 ** Lee Spring
 ** LAS Solutions Ltd - www.las-solutions.co.uk
 ** Copyright (c) 2004 LAS Solutions Ltd
 **
 ** THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
 ** TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
 ** THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
 ** CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 ** DEALINGS IN THE SOFTWARE.
 **
 ** OVERVIEW:
 ** Supports Null for various types.
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 1/8/04		1.0			LAS		Initial Version
 ************************************************************************************************/

using System;
using System.Reflection;

namespace Discovery.Utility
{
    /*************************************************************************************************
    ** CLASS:	Null
    **
    ** OVERVIEW:
    ** Manages NULL values for properties and db values
    **
    ** MODIFICATION HISTORY:
    **
    ** Date:		Version:    Who:	Change:
    ** 1/8/04		1.0			LAS		Initial Version
    ************************************************************************************************/

    public class Null
    {

        // Define application encoded null values
        public static int NullInteger
        {
            get { return -1; }
        }

        public static DateTime NullDate
        {
            get { return DateTime.MinValue; }
        }

        public static string NullString
        {
            get { return @""; }
        }

        public static bool NullBoolean
        {
            get { return false; }
        }

        public static Guid NullGuid
        {
            get { return Guid.Empty; }
        }

        // Sets a field to an application encoded null value (used in Presentation layer)
        public static Object SetNull(Object objField)
        {
            if (objField != null)
            {
                if (objField is DBNull)
                {
                    return DBNull.Value;
                }
                if (objField is int)
                {
                    return NullInteger;
                }
                else if (objField is Double)
                {
                    return NullInteger;
                }
                else if (objField is Decimal)
                {
                    return NullInteger;
                }
                else if (objField is DateTime)
                {
                    return NullDate;
                }
                else if (objField is string)
                {
                    return NullString;
                }
                else if (objField is bool)
                {
                    return NullBoolean;
                }
                else if (objField is Guid)
                {
                    return NullGuid;
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            else
            {
                // Assume string
                return NullString;
            }
        }

        // Sets a field to an application encoded null value ( used in BLL layer )
        // It does not actually set the passed param to null, it returns it's types null equivelent.
        public static Object SetNull(PropertyInfo propertyInfo)
        {

            switch (propertyInfo.PropertyType.ToString())
            {
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                    {
                        return NullInteger;
                    }
                case "System.Single":
                    {
                        return (Single)NullInteger;
                    }
                case "System.Double":
                    {
                        return (double)NullInteger;
                    }
                case "System.Decimal":
                    {
                        return (decimal)NullInteger;
                    }
                case "System.DateTime":
                    {
                        return NullDate;
                    }
                case "System.String":
                case "System.Char":
                    {
                        return NullString;
                    }
                case "System.Boolean":
                    {
                        return NullBoolean;
                    }
                case "System.Guid":
                    {
                        return NullGuid;
                    }
                default:
                    {
                        // Enumerations default to the first entry
                        Type propType = propertyInfo.PropertyType;
                        if (propType.BaseType.Equals(typeof(Enum)))
                        {
                            Array objEnumValues = Enum.GetValues(propType);
                            Array.Sort(objEnumValues);
                            return Enum.ToObject(propType, objEnumValues.GetValue(0));
                        }
                        else
                        {
                            throw new NullReferenceException();
                        }
                    }
            }

        }

        // Convert an application encoded null value to a database null value ( used in DAL )
        public static Object GetNull(Object objField, Object objDBNull, Object objNonNull)
        {
            return (IsNull(objField)) ? objDBNull : objNonNull;
        }

        public static Object GetNull(Object objField, Object objDBNull)
        {
            if (objField == null)
            {
                return objDBNull;
            }
            else if (objField is int)
            {
                if (Convert.ToInt32(objField) == NullInteger)
                {
                    return objDBNull;
                }
            }
            else if (objField is Single)
            {
                if (Convert.ToSingle(objField) == NullInteger)
                {
                    return objDBNull;
                }
            }
            else if (objField is Double)
            {
                if (Convert.ToDouble(objField) == NullInteger)
                {
                    return objDBNull;
                }
            }
            else if (objField is Decimal)
            {
                if (Convert.ToDecimal(objField) == NullInteger)
                {
                    return objDBNull;
                }
            }
            else if (objField is DateTime)
            {
                if (Convert.ToDateTime(objField) == NullDate)
                {
                    return objDBNull;
                }
            }
            else if (objField is string)
            {
                if (objField == null)
                {
                    return objDBNull;
                }
                else
                {
                    if (objField.ToString() == NullString)
                    {
                        return objDBNull;
                    }
                }
            }
            else if (objField is Boolean)
            {
                if (Convert.ToBoolean(objField) == NullBoolean)
                {
                    return objDBNull;
                }
            }
            else if (objField is Guid)
            {
                if (((Guid)objField).Equals(NullGuid))
                {
                    return objDBNull;
                }
            }
            else
            {
                // Unknown type
                throw new NullReferenceException();
            }
            // Return the original field value
            // Type known, but not a null value
            return objField;
        }

        // checks if a field contains an application encoded null value
        public static bool IsNull(Object objField)
        {
            if (objField.Equals(SetNull(objField)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}