/*************************************************************************************************
 ** FILE:   AddressController	
 ** DATE:	30/05/2006
 ** AUTHOR:	Paul Nichols
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 30/5/06		1.0		    PJN	    Initial Version
 ************************************************************************************************/
using System;
using System.Collections.Generic;
using Discovery.Components.ComponentServices;

namespace Discovery.BusinessObjects.Controllers
{
    /*************************************************************************************************
    ** CLASS:	AddressController
    **
    ** OVERVIEW:
    ** Allows an address to be checked against the QAS Quick address software and a PAF address to be 
    ** returned. The check address method uses a singleton to check the address
    **
    ** MODIFICATION HISTORY:
    **
    ** Date:		Version:    Who:	Change:
    ** 19/7/06		1.0			PJN		Initial Version
    ************************************************************************************************/

    /// <summary>
    /// A class to provide the address controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public static class AddressController
    {
        #region Singleton
        //this section uses an implementation of the singlton pattern to define
        //a static method which should be
        //used to access the QASWrapper class end ensures that the same single instance
        //is used by all callers. This saves the overhead of starting up and shuting down
        //the api and also stops the api from erroring when accessed by many threads.
        private static QASWrapper Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {

            }

            internal static readonly QASWrapper instance = new QASWrapper();
        }
        
        # endregion

        /// <summary>
        /// Checks the address against Quick address and returns a PAFAddress instance.
        /// A single instance of Quick address will be used
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="customerName">Name of the customer.</param>
        /// <returns></returns>
        public static PAFAddress CheckAddress(Address address,string customerName)
        {
            //call the CheckAddress method on the QASWrapper class.
            //If successful this will return a disctionary containing the address fields
            //as defined in Quick Addresses ini file
            Dictionary<string, object> formattedAddress = Instance.CheckAddress(address, customerName);
            
            //create an instance of our PAFAddress class to populate and return
            PAFAddress pafAddress = new PAFAddress();
            
            //extract the expected address fields from the returned list
            //line 1
            object line1;
            formattedAddress.TryGetValue("Line1", out line1);
            if (line1!=null) pafAddress.Line1 = line1.ToString();
            //line 2
            object line2;
            formattedAddress.TryGetValue("Line2", out line2);
            if (line2 != null) pafAddress.Line2 = line2.ToString();
            //line 3
            object line3;
            formattedAddress.TryGetValue("Line3", out line3);
            if (line3 != null) pafAddress.Line3 = line3.ToString();
            //line 4
            object line4;
            formattedAddress.TryGetValue("Line4", out line4);
            if (line4 != null) pafAddress.Line4 = line4.ToString();
            //post code
            object postCode;
            formattedAddress.TryGetValue("PostCode", out postCode);
            if (postCode != null) pafAddress.PostCode = postCode.ToString();

            //DPS
            object DPS;
            formattedAddress.TryGetValue("DPS", out DPS);
            if (DPS != null) pafAddress.DPS = (string) DPS;
            
            //northing
            object northing;
            formattedAddress.TryGetValue("Northing", out northing);
            if (northing!= null && northing.ToString()!=string.Empty) pafAddress.Northing= Convert.ToInt32(northing);

            //easting
            object easting;
            formattedAddress.TryGetValue("Easting", out easting);
            if (easting != null && easting.ToString() != string.Empty) pafAddress.Easting = Convert.ToInt32(easting);
            
            //status/matchtype
            object matchType;
            formattedAddress.TryGetValue("MatchType", out matchType);
            if (matchType != null)
            {
                pafAddress.Match = matchType.ToString();
               
            }
           
            return pafAddress;
        }
    }
}