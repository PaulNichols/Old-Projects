using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net.Mail;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration.Design;
using Microsoft.Practices.ObjectBuilder;

namespace Discovery.ComponentServices.ExceptionHandling.Design
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
            AddDefaultCommands(typeof(EmailHandlerNode));
            AddMoveUpDownCommands(typeof(EmailHandlerNode));
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
            : base(serviceProvider, typeof(EmailHandlerNode))
        {
        }

        protected override void OnExecuted(EventArgs e)
        {
            base.OnExecuted(e);
            EmailHandlerNode node = ChildNode as EmailHandlerNode;
            if (null == node) return;
        }
    }


    /// <summary>
    /// Represents a design time representation of a <see cref="EmailHandlerData"/> configuration element.
    /// </summary>
    public class EmailHandlerNode : ExceptionHandlerNode
    {
        private string policy;

        /// <summary>
        /// Initialize a new instance of the <see cref="EmailHandlerNode"/> class.
        /// </summary>
        public EmailHandlerNode()
            : this(new EmailHandlerData(Resources.HandlerName, ""))
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="EmailHandlerNode"/> class with a <see cref="EmailHandlerData"/> instance.
        /// </summary>
        /// <param name="EmailExceptionHandlerData">A <see cref="EmailHandlerData"/> instance.</param>
        public EmailHandlerNode(BaseEmailHandlerData EmailExceptionHandlerData)
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
        /// Gets the <see cref="EmailHandlerData"/> this node represents.
        /// </summary>
        /// <value>
        /// The <see cref="EmailHandlerData"/> this node represents.
        /// </value>
        public override ExceptionHandlerData ExceptionHandlerData
        {
            get { return new EmailHandlerData(Resources.HandlerName, Policy); }
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
                             typeof(EmailHandlerNode),
                             typeof(EmailHandlerData));
        }
    }
}

namespace Discovery.ComponentServices.ExceptionHandling
{
    [ConfigurationElementType(typeof(EmailHandlerData))]
    public class DiscoveryEmailHandler : IExceptionHandler
    {
        //public DiscoveryEmailHandler(NameValueCollection ignore)
        //{
        //}

        public DiscoveryEmailHandler(string policy)
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
            if (exception != null && exception is DiscoveryException)
            {
                //only attempt to send an email if we are handling a DiscoveryException, otherwise we will be 
                //unable to find the email address

                DiscoveryException discoveryException = (DiscoveryException)exception;

                //this email handler has been related to the exception so we will email someone if we
                //can find the email address from the DB using the exception type, the policy name and the opcocode

                //check the properties collection of the DiscoveryException object 
                string opCoCode = discoveryException.Properties["OpCoCode"].ToString();
                string operatorEmail = discoveryException.Properties["OperatorEmail"].ToString();
                string exceptionType = exception.GetType().ToString();

                //use OpCoId + exceptionType + CategoryName (name) to look in the database and see if 
                //there is an email address to send the exception message to

                ErrorType errorType = ErrorTypeController.GetErrorType(exceptionType, opCoCode, Policy);

                if (errorType != null)
                {
                    //get from object
                    bool emailOperator = errorType.EmailOperator;
                    string toAddress = errorType.EmailRecipients;

                    if (emailOperator & !string.IsNullOrEmpty(operatorEmail))
                    {
                        if (!string.IsNullOrEmpty(toAddress))
                        {
                            //add the operator email to the recipient list if there is a list and
                            //there is a operatorEmail
                            toAddress = string.Concat(toAddress, ",", operatorEmail);
                        }
                        else
                        {
                            //set the toaddress to be the operatorEmail if there isn't a list retrived from the DB and
                            //there is a operatorEmail
                            toAddress = operatorEmail;
                        }
                    }

                    if (!string.IsNullOrEmpty(toAddress))
                    {
                        string emailServerName = ConfigurationManager.AppSettings["EmailServerName"];
                        string fromAddress = ConfigurationManager.AppSettings["FromAddress"];
                        int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);

                        //get from exception
                        string emailBody = exception.Message;
                        SmtpClient mailClient = new SmtpClient(emailServerName, smtpPort);
                        MailMessage message = new MailMessage(fromAddress.Replace(";", ","), toAddress.Replace(";", ","), errorType.EmailSubject, emailBody);
                        mailClient.Send(message);
                    }
                }

            }

            return exception;
        }

        #endregion
    }

    [Assembler(typeof(EmailHandlerAssembler))]
    public class EmailHandlerData : BaseEmailHandlerData
    {
        public EmailHandlerData()
            : base()
        {
        }

        public EmailHandlerData(string name, string policy)
            : base(name, typeof(DiscoveryEmailHandler))
        {
            Policy = policy;
        }
    }

    /// <summary>
    /// This type supports the Enterprise Library infrastructure and is not intended to be used directly from your code.
    /// Represents the process to build a <see cref="DiscoveryEmailHandler"/> described by a <see cref="EmailHandlerData"/> configuration object.
    /// </summary>
    /// <remarks>This type is linked to the <see cref="EmailHandlerData"/> type and it is used by the <see cref="ExceptionHandlerCustomFactory"/> 
    /// to build the specific <see cref="IExceptionHandler"/> object represented by the configuration object.
    /// </remarks>
    public class EmailHandlerAssembler : IAssembler<IExceptionHandler, ExceptionHandlerData>
    {
        /// <summary>
        /// This method supports the Enterprise Library infrastructure and is not intended to be used directly from your code.
        /// Builds a <see cref="DiscoveryEmailHandler"/> based on an instance of <see cref="EmailHandlerData"/>.
        /// </summary>
        /// <seealso cref="ExceptionHandlerCustomFactory"/>
        /// <param name="context">The <see cref="IBuilderContext"/> that represents the current building process.</param>
        /// <param name="objectConfiguration">The configuration object that describes the object to build. Must be an instance of <see cref="EmailHandlerData"/>.</param>
        /// <param name="configurationSource">The source for configuration objects.</param>
        /// <param name="reflectionCache">The cache to use retrieving reflection information.</param>
        /// <returns>A fully initialized instance of <see cref="DiscoveryEmailHandler"/>.</returns>
        public IExceptionHandler Assemble(IBuilderContext context, ExceptionHandlerData objectConfiguration,
                                          IConfigurationSource configurationSource,
                                          ConfigurationReflectionCache reflectionCache)
        {
            BaseEmailHandlerData castedObjectConfiguration
                = (EmailHandlerData)objectConfiguration;

            DiscoveryEmailHandler createdObject
                = new DiscoveryEmailHandler(castedObjectConfiguration.Policy);

            return createdObject;
        }
    }
}