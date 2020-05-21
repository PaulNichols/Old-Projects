using System;
using System.Configuration;
using System.Net.Mail;
using System.Transactions;
using Discovery.ComponentServices.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration.Design;
using Microsoft.Practices.ObjectBuilder;

namespace Discovery.Integration.ExceptionHandling.Design
{
    internal sealed class EmailCommandRegistrar : CommandRegistrar
    {
        public EmailCommandRegistrar(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public override void Register()
        {
            AddEmailExceptionHandlerCommand();
            AddDefaultCommands(typeof(IntegrationEmailHandlerNode));
            AddMoveUpDownCommands(typeof(IntegrationEmailHandlerNode));
        }

        private void AddEmailExceptionHandlerCommand()
        {
            ConfigurationUICommand cmd = ConfigurationUICommand.CreateMultipleUICommand(ServiceProvider,
                                                                                        Resources.HandlerName,
                                                                                        string.Format(
                                                                                            Resources.Culture,
                                                                                            Resources.
                                                                                                GenericCreateStatusText,
                                                                                            Resources.HandlerName),
                                                                                        new EmailExceptionHandlerCommand
                                                                                            (ServiceProvider),
                                                                                        typeof(ExceptionHandlerNode));
            AddUICommand(cmd, typeof(ExceptionTypeNode));
        }
    }

    /// <summary>
    /// Represents the design manager for the Email exception handler.
    /// </summary>
    public sealed class EmailConfigurationDesignManager : ConfigurationDesignManager
    {
        /// <summary>
        /// Register the commands and node maps needed for the design manager into the design time.
        /// </summary>
        /// <param name="serviceProvider">The a mechanism for retrieving a service object; that is, an object that provides custom support to other objects.</param>
        public override void Register(IServiceProvider serviceProvider)
        {
            CommandRegistrar commandRegistrar = new EmailCommandRegistrar(serviceProvider);
            commandRegistrar.Register();

            NodeMapRegistrar nodeMapRegistrar = new EmailNodeMapRegistrar(serviceProvider);
            nodeMapRegistrar.Register();
        }
    }

    public class EmailExceptionHandlerCommand : AddChildNodeCommand
    {
        public EmailExceptionHandlerCommand(IServiceProvider serviceProvider)
            : base(serviceProvider, typeof(IntegrationEmailHandlerNode))
        {
        }

        protected override void OnExecuted(EventArgs e)
        {
            base.OnExecuted(e);
            IntegrationEmailHandlerNode node = ChildNode as IntegrationEmailHandlerNode;
            if (null == node) return;
        }
    }


    /// <summary>
    /// Represents a design time representation of a <see cref="IntegrationEmailHandlerData"/> configuration element.
    /// </summary>
    public class IntegrationEmailHandlerNode : ExceptionHandlerNode
    {
        private string policy;

        /// <summary>
        /// Initialize a new instance of the <see cref="IntegrationEmailHandlerNode"/> class.
        /// </summary>
        public IntegrationEmailHandlerNode()
            : this(new IntegrationEmailHandlerData(Resources.HandlerName, ""))
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="IntegrationEmailHandlerNode"/> class with a <see cref="IntegrationEmailHandlerData"/> instance.
        /// </summary>
        /// <param name="EmailExceptionHandlerData">A <see cref="IntegrationEmailHandlerData"/> instance.</param>
        public IntegrationEmailHandlerNode(IntegrationEmailHandlerData EmailExceptionHandlerData)
        {
            if (null == EmailExceptionHandlerData) throw new ArgumentNullException("EmailExceptionHandlerData");

            Rename(EmailExceptionHandlerData.Name);
            Policy = EmailExceptionHandlerData.Policy;
        }

        /// <summary>
        /// Gets or sets the exception message to use.
        /// </summary>
        /// <value>
        /// The exception message to use.
        /// </value>
        [SRDescription("AppHandlerNodeMessageDescription", typeof(Resources))]
        [SRCategory("CategoryGeneral", typeof(Resources))]
        public string Policy
        {
            get { return policy; }
            set { policy = value; }
        }

        /// <summary>
        /// Gets the <see cref="IntegrationEmailHandlerData"/> this node represents.
        /// </summary>
        /// <value>
        /// The <see cref="IntegrationEmailHandlerData"/> this node represents.
        /// </value>
        public override ExceptionHandlerData ExceptionHandlerData
        {
            get { return new IntegrationEmailHandlerData(Resources.HandlerName, Policy); }
        }
    }

    public class EmailNodeMapRegistrar : NodeMapRegistrar
    {
        public EmailNodeMapRegistrar(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public override void Register()
        {
            AddSingleNodeMap(Resources.HandlerName,
                             typeof(IntegrationEmailHandlerNode),
                             typeof(IntegrationEmailHandlerData));
        }
    }
}

namespace Discovery.Integration.ExceptionHandling
{
    [ConfigurationElementType(typeof(IntegrationEmailHandlerData))]
    public class DiscoveryIntegrationEmailHandler : IExceptionHandler
    {
        //public DiscoveryIntegrationEmailHandler(NameValueCollection ignore)
        //{
        //}

        public DiscoveryIntegrationEmailHandler(string policy)
        {
            this.policy = policy;
        }

        private readonly string policy;

        public string Policy
        {
            get { return policy; }
        }

        #region IExceptionHandler Members

        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            //if (exception is DiscoveryException && exception != null)
            //{
            //    //only attempt to send an email if we are handling a DiscoveryException, otherwise we will be 
            //    //unable to find the email address

            //    DiscoveryException discoveryException = (DiscoveryException)exception;

            //    //this email handler has been related to the exception so we will email someone if we
            //    //can find the email address from the DB using the exception type, the policy name and the opcocode

            //    //check the properties collection of the DiscoveryException object 

            //    try
            //    {
            //        if (discoveryException.Properties["OpCoCode"] != null)
            //        {
            //            string opCoCode = discoveryException.Properties["OpCoCode"].ToString();
            //            string exceptionType = exception.GetType().ToString();

            //            //use OpCoCode + exceptionType + Policy (name) to look in the database and see if 
            //            //there is an email address to send the exception message to

            //            ErrorType errorType;
                        
            //            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //            {
            //                errorType = DiscoveryController.GetErrorType(exceptionType, opCoCode, Policy);
            //                scope.Complete();
                            
            //            }

            //            if (errorType != null)
            //            {
            //                //get from object
            //                string toAddress = errorType.EmailRecipients;
 
            //                if (!string.IsNullOrEmpty(toAddress))
            //                {
            //                    //get from config file?
            //                    string emailServerName = "tigger";
            //                    string fromAddress = "paul.nichols@roberthorne.co.uk";
            //                    int smtpPort = 25;
            //                    string emailSubject = "Test Subject";
            //                    //get from exception
            //                    string emailBody = exception.Message;
            //                    SmtpClient mailClient = new SmtpClient(emailServerName, smtpPort);
            //                    MailMessage message = new MailMessage();
            //message.To.Add(fromAddress.Replace(';',','));
            //                    //message.To.Add(toAddress.Replace(';',','));
            //                    message.From=new MailAddress(fromAddress);
            //                    message.Subject=emailSubject;
            //                    message.Body = emailBody;
            //                    mailClient.Send(message);
                //            }
                //        }
                //    }
                //}
                //catch (Exception e)
                //{


                //}

           // }

            return exception;
        }

        #endregion
    }

    [Assembler(typeof(IntegrationEmailHandlerAssembler))]
    public class IntegrationEmailHandlerData : BaseEmailHandlerData
    {
         public IntegrationEmailHandlerData() : base()
        {
        }

        public IntegrationEmailHandlerData(string name, string policy)
            : base(name, typeof (DiscoveryIntegrationEmailHandler))
        {
            Policy = policy;
        }
      
      
    }

    /// <summary>
    /// This type supports the Enterprise Library infrastructure and is not intended to be used directly from your code.
    /// Represents the process to build a <see cref="DiscoveryIntegrationEmailHandler"/> described by a <see cref="IntegrationEmailHandlerData"/> configuration object.
    /// </summary>
    /// <remarks>This type is linked to the <see cref="IntegrationEmailHandlerData"/> type and it is used by the <see cref="ExceptionHandlerCustomFactory"/> 
    /// to build the specific <see cref="IExceptionHandler"/> object represented by the configuration object.
    /// </remarks>
    public class IntegrationEmailHandlerAssembler : IAssembler<IExceptionHandler, ExceptionHandlerData>
    {
        /// <summary>
        /// This method supports the Enterprise Library infrastructure and is not intended to be used directly from your code.
        /// Builds a <see cref="DiscoveryIntegrationEmailHandler"/> based on an instance of <see cref="IntegrationEmailHandlerData"/>.
        /// </summary>
        /// <seealso cref="ExceptionHandlerCustomFactory"/>
        /// <param name="context">The <see cref="IBuilderContext"/> that represents the current building process.</param>
        /// <param name="objectConfiguration">The configuration object that describes the object to build. Must be an instance of <see cref="IntegrationEmailHandlerData"/>.</param>
        /// <param name="configurationSource">The source for configuration objects.</param>
        /// <param name="reflectionCache">The cache to use retrieving reflection information.</param>
        /// <returns>A fully initialized instance of <see cref="DiscoveryIntegrationEmailHandler"/>.</returns>
        public IExceptionHandler Assemble(IBuilderContext context, ExceptionHandlerData objectConfiguration,
                                          IConfigurationSource configurationSource,
                                          ConfigurationReflectionCache reflectionCache)
        {
            IntegrationEmailHandlerData castedObjectConfiguration
                = (IntegrationEmailHandlerData)objectConfiguration;

            DiscoveryIntegrationEmailHandler createdObject
                = new DiscoveryIntegrationEmailHandler(castedObjectConfiguration.Policy);

            return createdObject;
        }
    }
}