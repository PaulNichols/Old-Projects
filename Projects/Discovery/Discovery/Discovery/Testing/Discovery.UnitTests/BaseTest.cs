using System.Collections.Generic;
using System.Reflection;
using Discovery.BusinessObjects;
using NUnit.Framework;

namespace Discovery.UnitTests
{
    public class BaseTest
    {
        protected bool PropertiesAreUnChanged<T>(T changedObject, T originalObject, List<string> propertiesToExclude)
        {
            bool unchanged = true;
            foreach (PropertyInfo propertyInfo in changedObject.GetType().GetProperties())
            {
                if (!propertiesToExclude.Contains(propertyInfo.Name))
                {
                    if (propertyInfo.CanRead)
                    {
                        object originalValue =
                            originalObject.GetType().GetProperty(propertyInfo.Name).GetValue(originalObject, null);
                        object changedValue = propertyInfo.GetValue(changedObject, null);
                        if ((changedValue != null && originalValue != null) &&
                             !changedValue.Equals(originalValue))
                        {
                            unchanged = false;
                            break;
                        }
                    }
                }
            }
            return unchanged;
        }

        protected void BasicPropertyTest<T>(object newValue, List<string> propertiesToExclude, string propertyToTest) where T : DiscoveryBusinessObject, new()
        {
            //create new trip object
            T objectToChange = new T();
            //clone so we can keep an original copy
            T originalObject = objectToChange.DeepClone<T>();

            objectToChange.GetType().GetProperty(propertyToTest).SetValue(objectToChange, newValue, null);

            //check all properties except the ones specified above remain unchanged after the assignment of the test value
            //and also test the test property returns the correct test value
            Assert.IsTrue(PropertiesAreUnChanged(originalObject, objectToChange, propertiesToExclude) &&
                          (!objectToChange.GetType().GetProperty(propertyToTest).CanRead ||
                          objectToChange.GetType().GetProperty(propertyToTest).GetValue(objectToChange, null).Equals(newValue)));
        }
    }
}