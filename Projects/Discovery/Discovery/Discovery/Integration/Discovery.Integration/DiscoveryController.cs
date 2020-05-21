
using System;
using System.Data;
using Discovery.Integration.DataAccess;
using Discovery.Integration.ExceptionHandling;
using Discovery.Utility.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using Microsoft.Practices.EnterpriseLibrary.SqlConfigurationSource;

namespace Discovery.Integration
{
    public class DiscoveryController
    {
     

        public static ErrorType GetErrorType(string exceptionType, string opCoCode, string policyName)
        {
            ErrorType errorType = null;

            //read the config details from file or other source (db) to find the error handling section

            //find the configuration source section
            ConfigurationSourceSection section = ConfigurationSourceSection.GetConfigurationSourceSection();

            //find the selected source where our config sections are stored
            string selectedSource = section.SelectedSource;
            NameTypeConfigurationElementCollection<ConfigurationSourceElement> sources = section.Sources;

            ConfigurationSourceElement element = sources.Get(selectedSource);

            if (element is SqlConfigurationSourceElement)
            {
                SqlConfigurationSourceElement sqlElement = element as SqlConfigurationSourceElement;

                SqlConfigurationSource configurationSource =
                    new SqlConfigurationSource(sqlElement.ConnectionString, sqlElement.GetStoredProcedure, sqlElement.SetStoredProcedure,
                                               sqlElement.RefreshStoredProcedure, sqlElement.RemoveStoredProcedure);

                //find all the exception policies
                NamedElementCollection<ExceptionPolicyData> policies =
                    ExceptionHandlingSettings.GetExceptionHandlingSettings(configurationSource).ExceptionPolicies;

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
                                                                           errorType = PopulateErrorType(policyName, opCoCode, currentExceptionType);
                                                                       }

                                                                   }

                            );
                    }
                }
            }
            return errorType;
        }

        private static ErrorType PopulateErrorType(string policyName, string opCoCode, ExceptionTypeData currentExceptionType)
        {
            ErrorType errorType;
            //go to db for some details
            
            errorType = CBO<ErrorType>.FillObject(DiscoveryDataAccessProvider.Instance().GetErrorType(currentExceptionType.Type.ToString(),
                                                                                             policyName, opCoCode),
                                                  PopulateEmailAddresses, true);

            if (errorType == null)
            {
                //there is no data related to this exception type in the database yet so
                //just create a new objects and fill the rest of the properties in
                errorType = new ErrorType();
                errorType.Policy = policyName;
                //errorType.OpCoId = opCoId;
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
                    if (currentHandlerData is IntegrationEmailHandlerData)
                    {
                        errorType.HasEmailHandler = true;
                        //could do with a break here!
                    }

                });
            return errorType;
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

    }
}