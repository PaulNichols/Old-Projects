using System;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;

namespace Discovery.ComponentServices.ExceptionHandling
{
    public class BaseEmailHandlerData : CustomHandlerData
    {
        public BaseEmailHandlerData()
        {
        }

        private const string policyProperty = "policy";

        [ConfigurationProperty(policyProperty, IsRequired = true)]
        public string Policy
        {
            get { return (string) this[policyProperty]; }
            set { this[policyProperty] = value; }
        }
        
          public BaseEmailHandlerData(string name, Type type)
            : base(name, type)
        {
            
        }
    }
    
}