using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using Discovery.BusinessObjects;

// <summary>
// This class "wraps" the QAS API calls.
// </summary>
namespace Discovery.Components.ComponentServices
{
    /// <summary>
    /// A sealed class QASWrapper is to wrap the QAS API calls
    /// </summary>
    public sealed class QASWrapper
    {
        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="T:QASWrapper"/> class.
        /// </summary>
        internal QASWrapper()
        {
            Startup();
        }

        #endregion

        #region Deconstructor(s)

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="T:Discovery.Components.ComponentServices.QASWrapper"/> is reclaimed by garbage collection.
        /// </summary>
        ~QASWrapper()
        {
            ShutDown();
        }

        #endregion

        #region "DllImports for QAS"

        [DllImport(@"QABWVED.DLL", CharSet = CharSet.Ansi)]
        private static extern int QABatchWV_Startup(long vlFlags);

        [DllImport(@"QABWVED.DLL", CharSet = CharSet.Ansi)]
        private static extern int QABatchWV_Shutdown();

        [DllImport(@"QABWVED.DLL", CharSet = CharSet.Ansi)]
        private static extern int QABatchWV_Clean(int viHandle, string vsSearch, ref int riSearchHandle,
                                                  [MarshalAs(UnmanagedType.LPStr)] StringBuilder rsPostcode,
                                                  int viPostcodeLength,
                                                  [MarshalAs(UnmanagedType.LPStr)] StringBuilder rsIsoCode,
                                                  [MarshalAs(UnmanagedType.LPStr)] StringBuilder rsReturnCode,
                                                  int viReturnLength);

        [DllImport(@"QABWVED.DLL", CharSet = CharSet.Ansi)]
        private static extern int QABatchWV_Close(int viHandle);

        [DllImport(@"QABWVED.DLL", CharSet = CharSet.Ansi)]
        private static extern int QABatchWV_EndSearch(int viSearchHandle);

        [DllImport(@"QABWVED.DLL", CharSet = CharSet.Ansi)]
        private static extern int QABatchWV_FormattedLineCount(int viSearchHandle, ref int riCount);

        [DllImport(@"QABWVED.DLL", CharSet = CharSet.Ansi)]
        private static extern int QABatchWV_Open(string vsIniFile, string vsLayout, long vlFlags, ref int riHandle);

        [DllImport(@"QABWVED.DLL", CharSet = CharSet.Ansi)]
        private static extern int QABatchWV_GetMatchInfo(int viSearchHandle,
                                                         [MarshalAs(UnmanagedType.LPStr)] StringBuilder rsIsoCode,
                                                        [MarshalAs(UnmanagedType.LPStr)]   StringBuilder rsMatchType,
                                                         ref int riConfidence, ref int riPostcodeAction,
                                                         ref int riAddressAction, ref long rlGenericInfo,
                                                         ref long rlCountryInfo, ref long rlCountryInfo2);

        [DllImport(@"QABWVED.DLL", CharSet = CharSet.Ansi)]
        private static extern int QABatchWV_GetFormattedLine(int viSearchHandle, int viLine,
                                                             [MarshalAs(UnmanagedType.LPStr)] StringBuilder rsBuffer,
                                                             int viBuffLen);

        #endregion

        private static int apihandle = 0;
        private static int sourceHandle = 0;

        /// <summary>
        /// Checks the ToString() of the specified address and returns a "corrected" and matched address.
        /// </summary>
        /// <param name="address">The address to lookup.</param>
        /// <param name="customerName">Name of the customer.</param>
        /// <returns></returns>
        public Dictionary<string, object> CheckAddress(Address address,string customerName)
        {
            //clean out the empty lines
            Search(string.Concat(customerName,",", address.ToString()));

            Dictionary<string, object> addressFields = new Dictionary<string, object>();

            //if the number of lines are returned from the clean operation equals the number specified in the ini file then
            //set up the return values
            if (FormattedLineCount() == 8)
            {
                addressFields.Add("Line1", GetFormattedLine(0));
                addressFields.Add("Line2", GetFormattedLine(1));
                addressFields.Add("Line3", GetFormattedLine(2));
                addressFields.Add("Line4", GetFormattedLine(3));
                addressFields.Add("PostCode", GetFormattedLine(4));
                addressFields.Add("Easting", GetFormattedLine(5));
                addressFields.Add("Northing", GetFormattedLine(6));
                addressFields.Add("DPS", GetFormattedLine(7));
                addressFields.Add("MatchType", GetMatchType());
            }

            EndSearch();
            return addressFields;
        }

        /// <summary>
        /// Initialise QAS API
        /// </summary>
        /// <returns>Returns if ok</returns>
        private bool Startup()
        {
            //Console.WriteLine("Starting Up " + GetHashCode().ToString());
            long status = 0;
            try
            {
                status = QABatchWV_Startup(0);
                if (status!=0)
                {
                    throw new Exception("Could not start Quick Address, error code: " + status);
                }
                return Open();
            }
            catch
            {
                if (status == 0) ShutDown();
                throw;
            }
        }


        /// <summary>
        /// ShutDown QAS API
        /// </summary>
        private void ShutDown()
        {
            // Console.WriteLine("Shuting Down");
            QABatchWV_Close(apihandle);
            QABatchWV_Shutdown();
            //int status = QABatchWV_ShutDown();
        }


        /// <summary>
        /// Opens an instance of the API, specifying the name of the configuration file to be used, and the layout to use within that file.
        /// </summary>
        /// <returns>Returns if ok</returns>
        private static bool Open()
        {
            int status;

            //open the the ini settings held in the file which is specified in the calling applications configuration file.
            status =
                QABatchWV_Open(ConfigurationManager.AppSettings["QAWORLD_INI"], "GBR", 0,
                               ref apihandle);
            if (status != 0) throw new Exception("Unable to Open QAS layout, return code = " + status.ToString());

            return status == 0;
        }

        /// <summary>
        /// Performs a search on the specified input address.
        /// </summary>
        /// <param name="searchData">The search data, normall customer name and address.</param>
        /// <returns>QAS match code for the address</returns>
        private static string Search(string searchData)
        {
            StringBuilder returnPostCode = new StringBuilder(10);
            StringBuilder returnISO = new StringBuilder(10);
            StringBuilder matchCode = new StringBuilder(20);
            int status;

            status = QABatchWV_Clean(apihandle, searchData, ref sourceHandle, returnPostCode, 10, returnISO, matchCode, 10);
            if (status != 0)
                throw new Exception("Error encountered durning address Clean, return code = " + status.ToString());

            return matchCode.ToString();
        }


        /// <summary>
        /// Deallocates resources and the search handle used by a call to QABatchWV_Clean.
        /// </summary>
        private static void EndSearch()
        {
            int status;

            status = QABatchWV_EndSearch(sourceHandle);
            if (status != 0) throw new Exception("Error ending Clean, return code = " + status.ToString());
        }


        /// <summary>
        /// Returns the number of formatted lines that a search has resulted in.
        /// </summary>
        /// <returns>Formatted line count</returns>
        private static int FormattedLineCount()
        {
            int lineCount = 0;
            int status;

            status = QABatchWV_FormattedLineCount(sourceHandle, ref lineCount);
            if (status != 0)
                throw new Exception("An error occured returning line count, return code = " + status.ToString());

            return lineCount;
        }


        /// <summary>
        /// This function gets one formatted address line from the latest retrieved address.
        /// </summary>
        /// <param name="lineNumber">Line number to retrieve</param>
        /// <returns>formatted address line</returns>
        private static string GetFormattedLine(int lineNumber)
        {
            int bufferLength = 250;
            StringBuilder returnedLine = new StringBuilder(bufferLength);
            string formattedLine;
            int status;

            status = QABatchWV_GetFormattedLine(sourceHandle, lineNumber, returnedLine, bufferLength);
            if (status != 0)
                throw new Exception("An error occured returning a formatted line, return code = " +
                                    status.ToString());
            formattedLine = returnedLine.ToString();

            return formattedLine;
        }


        /// <summary>
        /// This function provides access to detailed match information. Most values are returned as 
        /// integers to ease processing. Each parameter provides a discrete component of the full match code.
        /// </summary>
        /// <returns>Combination of match type and confidence level</returns>
        private static string GetMatchType()
        {
            StringBuilder isoCode = new StringBuilder(10);
            StringBuilder matchType = new StringBuilder(2);
            int confidence = 0;
            int postcodeAction = 0;
            int addressAction = 0;
            long genericInfo = 0;
            long countryInfo = 0;
            long countryInfo2 = 0;
           
            int status;

            status =
                QABatchWV_GetMatchInfo(sourceHandle, isoCode,  matchType, ref confidence, ref postcodeAction,
                                       ref addressAction,
                                       ref genericInfo, ref countryInfo, ref countryInfo2);
            if (status != 0)
                throw new Exception("An error occured retrieving the match info, return code = " + status.ToString());
            //  matchtype = matchType.ToString() + " " + confidence;

            return matchType + confidence.ToString();
        }
    }
}