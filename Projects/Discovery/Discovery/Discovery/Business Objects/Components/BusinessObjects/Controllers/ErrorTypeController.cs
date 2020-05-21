/*************************************************************************************************
 ** FILE:	ErrorTypeController.cs
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
using System.Data;
using Discovery.ComponentServices.DataAccess;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.Utility;
using Discovery.Utility.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using Microsoft.Practices.EnterpriseLibrary.SqlConfigurationSource;

namespace Discovery.BusinessObjects.Controllers
{
    public class ErrorTypeController
    {
        public static Dictionary<string, string> GetPriorities()
        {
            //set up the return collection
            Dictionary<string, string> prorities = new Dictionary<string, string>();
            //get all the names in the enum
            List<string> enumNames = new List<string>(Enum.GetNames(typeof(ErrorType.PriorityEnum)));
            //sort them alphabetically
            enumNames.Sort();
            //iterate through adding dictionay entries with a value which has been split on upper case
            foreach (string prority in enumNames)
            {
                prorities.Add(prority, StringManipulation.SplitStringOnUpperCase(prority));
            }
            //return collection
            return prorities;
        }

        public static List<ExceptionPolicyData> GetPolicies()
        {
            List<ExceptionPolicyData> allPolicies = new List<ExceptionPolicyData>();
            //read the config details from file or other source (db) to find the error handling section

            ////find the configuration source section
            //ConfigurationSourceSection section = ConfigurationSourceSection.GetConfigurationSourceSection();

            ////find the selected source where our config sections are stored
            //string selectedSource = section.SelectedSource;
            //NameTypeConfigurationElementCollection<ConfigurationSourceElement> sources = section.Sources;

            //ConfigurationSourceElement element = sources.Get(selectedSource);

            //if (element is SqlConfigurationSourceElement)
            //{
            //    SqlConfigurationSourceElement sqlElement = element as SqlConfigurationSourceElement;

            //    SqlConfigurationSource configurationSource =
            //        new SqlConfigurationSource(sqlElement.ConnectionString, sqlElement.GetStoredProcedure, sqlElement.SetStoredProcedure,
            //                                   sqlElement.RefreshStoredProcedure, sqlElement.RemoveStoredProcedure);

            //find all the exception policies
            NamedElementCollection<ExceptionPolicyData> policies =
                ((ExceptionHandlingSettings)ConfigurationSourceFactory.Create().GetSection("exceptionHandling")).ExceptionPolicies;

            //find just the one specified
            if (policies != null)
            {
                policies.ForEach(delegate(ExceptionPolicyData currentPolicy)
                                     {
                                         allPolicies.Add(currentPolicy);
                                     }
                    );
            }
            //}

            allPolicies.Sort(new UniversalComparer<ExceptionPolicyData>("Name"));
            return allPolicies;
        }

        public static ErrorType GetErrorType(string exceptionType, string opCoCode, string policyName)
        {
            ErrorType errorType = null;

            //read the config details from file or other source (db) to find the error handling section

            //find all the exception policies
            NamedElementCollection<ExceptionPolicyData> policies =
                ((ExceptionHandlingSettings)ConfigurationSourceFactory.Create().GetSection("exceptionHandling")).ExceptionPolicies;

            //find just the one specified
            if (policies != null)
            {
                ExceptionPolicyData specifiedPolicy = policies.Get(policyName);

                if (specifiedPolicy != null)
                {
                    specifiedPolicy.ExceptionTypes.ForEach(delegate(ExceptionTypeData currentExceptionType)
                                                               {
                                                                   if (currentExceptionType.Type.ToString() == exceptionType)
                                                                   {
                                                                       errorType = PopulateErrorType(specifiedPolicy.Name, opCoCode, currentExceptionType);
                                                                       
                                                                   }

                                                               }

                        );
                }
            }

            return errorType;
        }

        private static ErrorType PopulateErrorType(string policyName, string opCoCode, ExceptionTypeData currentExceptionType)
        {
            ErrorType errorType;
            //go to db for some details
            errorType = CBO<ErrorType>.FillObject(DataAccessProvider.Instance().GetErrorType(currentExceptionType.Type.ToString(),
                                                                                             policyName, opCoCode),
                                                  PopulateEmailAddresses, true);

            if (errorType == null)
            {
                //there is no data related to this exception type in the database yet so
                //just create a new objects and fill the rest of the properties in
                errorType = new ErrorType();
                errorType.Policy = policyName;
                errorType.OpcoCode = opCoCode;
                if (!string.IsNullOrEmpty(opCoCode)) errorType.OpCoId = OpcoController.GetOpCo(opCoCode, false).Id;
                errorType.OpcoCode = opCoCode;
            }
            //set the description property by using the Exception types name
            errorType.SetExceptionDescription = currentExceptionType.Name;
            errorType.ExceptionType = currentExceptionType.Type.FullName;

            //see if this type of exception within the specified policy
            //has an email handler assigned to it
            currentExceptionType.ExceptionHandlers.ForEach(
                delegate(
                    ExceptionHandlerData
                    currentHandlerData)
                {
                    if (currentHandlerData is BaseEmailHandlerData)
                    {
                        errorType.HasEmailHandler = true;
                        //could do with a break here!
                    }

                });
            return errorType;
        }



        public static List<ErrorType> GetErrorTypes(string opCoCode, string policyName, string sortExpression)
        {

            List<ErrorType> errorTypes = new List<ErrorType>();

            //find all the exception policies
            NamedElementCollection<ExceptionPolicyData> allPolicies = null;

            allPolicies = ((ExceptionHandlingSettings)ConfigurationSourceFactory.Create().GetSection("exceptionHandling")).ExceptionPolicies;

            
            if (allPolicies != null)
            {
                NamedElementCollection<ExceptionPolicyData> policies = new NamedElementCollection<ExceptionPolicyData>();

                if (!string.IsNullOrEmpty(policyName))
                {
                    //find just the one specified
                    ExceptionPolicyData specifiedPolicy = allPolicies.Get(policyName);

                    policies.Add(specifiedPolicy);
                }
                else
                {
                    policies = allPolicies;
                }

                foreach (ExceptionPolicyData policy in policies)
                {
                    policy.ExceptionTypes.ForEach(delegate(ExceptionTypeData currentExceptionType)
                                                              {
                                                                  //go to db for some details
                                                                  ErrorType errorType;
                                                                  errorType =
                                                                      PopulateErrorType(policy.Name, opCoCode,
                                                                                        currentExceptionType);

                                                                  errorTypes.Add(errorType);
                                                              }
                   );
                }

            }


            #region Useful Code
            //policies.ForEach(delegate(ExceptionPolicyData currentPolicy)
            //                     {
            //                         Response.Write("CategoryName: " + currentPolicy.Name + "<br>");

            //                         currentPolicy.ExceptionTypes.ForEach(delegate(ExceptionTypeData currentExceptionType)
            //                                                                  {
            //                                                                      Response.Write("Exception Type: " +
            //                                                                                     currentExceptionType.Name +
            //                                                                                     "<br>");

            //                                                                      currentExceptionType.ExceptionHandlers.
            //                                                                          ForEach(
            //                                                                          delegate(
            //                                                                              ExceptionHandlerData
            //                                                                              currentHandlerData)
            //                                                                              {
            //                                                                                  Response.Write(
            //                                                                                      "Exception Handler: " +
            //                                                                                      currentHandlerData.Name +
            //                                                                                      "<br>");
            //                                                                                  if (currentHandlerData is LoggingExceptionHandlerData)
            //                                                                                  {
            //                                                                                      Response.Write(
            //                                                                                          "CategoryName: " + (currentHandlerData as LoggingExceptionHandlerData).CategoryName.ToString() + "<br>");
            //                                                                                      Response.Write(
            //                                                                                          "Event Id: " + (currentHandlerData as LoggingExceptionHandlerData).EventId.ToString() + "<br>");
            //                                                                                  }

            //                                                                              });
            //                                                                  }
            //                             );
            //                     }
            //    );
            #endregion


            //sort the results
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "ExceptionDescription";
            }
            errorTypes.Sort(new UniversalComparer<ErrorType>(sortExpression));


            return errorTypes;
        }

        private static void PopulateEmailAddresses(ErrorType errorTypeToPolulate, IDataReader dataReader, bool fullyPopulate)
        {
            //get email addresses using opCoId
            //ErrorOpCoSettings
            if (dataReader["EmailRecipients"] != DBNull.Value)
            {
                errorTypeToPolulate.EmailRecipients = dataReader["EmailRecipients"].ToString();
            }
        }

        /// <summary>
        /// Saves the ErrorType.
        /// </summary>
        /// <param name="errorType">The ErrorType.</param>
        /// <returns></returns>
        public static int SaveErrorType(ErrorType errorType)
        {
            try
            {
                if (errorType.IsValid)
                {
                    // Save entity
                    errorType.Id = DataAccessProvider.Instance().SaveErrorType(errorType);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(errorType);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return errorType.Id;
        }


    }
}