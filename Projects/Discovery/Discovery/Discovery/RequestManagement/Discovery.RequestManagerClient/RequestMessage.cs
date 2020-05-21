/*************************************************************************************************
 ** FILE:	RequestMessage.cs
 ** DATE:	30/05/2006
 ** AUTHOR:	Lee Spring
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 30/5/06		1.0		    LAS	    Initial Version
 ************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Discovery.RequestManagerClient
{
    /// <summary>
    /// A Class 'RequestMessage' which is an entity with namespace Discovery.RequestManagerClient
    /// The class holds the request message details
    /// </summary>
    [Serializable]
    public class RequestMessage
    {
        private string sourceSystem;

        /// <summary>
        /// Gets or sets the source system.
        /// </summary>
        /// <value>The source system.</value>
        public string SourceSystem
        {
            get { return sourceSystem; }
            set { sourceSystem = value; }
        }
        private string destinationSystem;

        /// <summary>
        /// Gets or sets the destination system.
        /// </summary>
        /// <value>The destination system.</value>
        public string DestinationSystem
        {
            get { return destinationSystem; }
            set { destinationSystem = value; }
        }
        private int sequence;

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>The sequence.</value>
        public int Sequence
        {
            get { return sequence; }
            set { sequence = value; }
        }
        private string type;

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private string body;

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public string Body
        {
            get { return body; }
            set { body = value; }
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <value>The label.</value>
        public string Label
        {
            get
            {
                return "SRC:" + sourceSystem + ";DEST:" + destinationSystem + ";TYPE:" + type + ";NAME:" + name;
            }
        }

        private string name;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RequestMessage"/> class.
        /// </summary>
        /// <param name="body">The body.</param>
        public RequestMessage(string body)
        {
            this.body = body;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RequestMessage"/> class.
        /// </summary>
        public RequestMessage()
        {
        }
    }
}
