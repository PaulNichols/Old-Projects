using System;
using System.Collections.Generic;
using System.Text;
using Discovery.BusinessObjects;
using Discovery.Utility;

namespace Discovery.Integration
{
    public class ErrorType : PersistableBusinessObject
    {
        public enum PriorityEnum
        {
            Immediate = 1,
            OneWorkingDay = 2,
            Normal = 3
        }

        private bool requiresAcknoledgement;
        private string policy;
        private bool emailOperator;
        private PriorityEnum priority = PriorityEnum.Normal;
        private string emailRecipients;
        private string exceptionDescription;
        private bool hasEmailHandler;
        private string exceptionType;
        private int opCoId;
        private string opcoCode;
        
        /// <summary>
        /// Gets or sets a value indicating whether [requires acknoledgement].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [requires acknoledgement]; otherwise, <c>false</c>.
        /// </value>
        public bool RequiresAcknoledgement
        {
            get { return requiresAcknoledgement; }
            set { requiresAcknoledgement = value; }
        }

        /// <summary>
        /// Gets or sets the policy.
        /// </summary>
        /// <value>The policy.</value>
        public string Policy
        {
            get { return policy; }
            set { policy = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [email operator].
        /// </summary>
        /// <value><c>true</c> if [email operator]; otherwise, <c>false</c>.</value>
        public bool EmailOperator
        {
            get { return emailOperator; }
            set { emailOperator = value; }
        }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public PriorityEnum Priority
        {
            get { return priority; }
            set { priority = value; }
        }


        /// <summary>
        /// Gets or sets the email recipients.
        /// </summary>
        /// <value>The email recipients.</value>
        public string EmailRecipients
        {
            get { return emailRecipients; }
            set { emailRecipients = value; }
        }

        public string ExceptionDescription
        {
            get { return exceptionDescription; }
        }

        /// <summary>
        /// Sets the set exception description. This property is designed to take a
        /// Excption Type and split it on the upper case chararters as our nameing convension is
        /// Pascal casing
        /// </summary>
        /// <value>The set exception description.</value>
        internal string SetExceptionDescription
        {
            set
            {
                exceptionDescription = "";
                exceptionDescription=StringManipulation.SplitStringOnUpperCase(value);
            }
        }

        public bool HasEmailHandler
        {
            get { return hasEmailHandler; }
            set { hasEmailHandler = value; }
        }

        public string ExceptionType
        {
            get { return exceptionType; }
            set { exceptionType = value; }
        }

        public int OpCoId
        {
            get { return opCoId; }
            set { opCoId = value; }
        }

        public string OpcoCode
        {
            get { return opcoCode; }
            set { opcoCode = value; }
        }
    }



}

