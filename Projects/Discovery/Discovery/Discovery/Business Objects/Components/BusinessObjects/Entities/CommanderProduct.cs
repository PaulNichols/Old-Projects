using System;
using Discovery.ComponentServices.Parsing;

namespace Discovery.BusinessObjects
{
    /// <summary>
    /// This class holds the state of a single Commander Sales Order. The CommanderSalesOrderParsing subscriber will typically create an instance of this class.
    /// </summary>
    [Serializable]
    public class CommanderProduct : PersistableBusinessObject
    {
        #region Private Fields

        private string productCode;
        private string description;
        private string shortDescription;
        private string account;
        private string uom;
        private bool isUpdate;
        private string recordSubType;

        #endregion

        #region Public Properties

      
        /// <summary>
        /// Gets or sets the product code.
        /// </summary>
        /// <value>The product code.</value>
        public string ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Gets or sets the short description.
        /// </summary>
        /// <value>The short description.</value>
        public string ShortDescription
        {
            get { return shortDescription; }
            set { shortDescription = value; }
        }

        /// <summary>
        /// Gets or sets the account.
        /// </summary>
        /// <value>The account.</value>
        public string Account
        {
            get { return account; }
            set { account = value; }
        }

        /// <summary>
        /// Gets or sets the UOM.
        /// </summary>
        /// <value>The UOM.</value>
        public string UOM
        {
            get { return uom; }
            set { uom = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is update.
        /// </summary>
        /// <value><c>true</c> if this instance is update; otherwise, <c>false</c>.</value>
        public bool IsUpdate
        {
            get { return isUpdate; }
        }

        /// <summary>
        /// Sets the type of the record sub.
        /// </summary>
        /// <value>The type of the record sub.</value>
        public string RecordSubType
        {
            set
            {
                recordSubType = value;
                isUpdate = (value.ToUpper() == "M");
            }
        }

        #endregion

        #region Public Method(s)

        /// <summary>
        /// Sets the product maintenance file header schema.
        /// </summary>
        /// <param name="fields">The fields.</param>
        public static void SetProductMaintenanceFileHeaderSchema(TextFieldCollection fields)
        {
            fields.Clear();

            AddCommonFields(fields);
            fields.Add(new TextField("Date", TypeCode.String, 10));
            fields.Add(new TextField("Time", TypeCode.String, 8));
        }

        /// <summary>
        /// Adds the common fields.
        /// </summary>
        /// <param name="fields">The fields.</param>
        private static void AddCommonFields(TextFieldCollection fields)
        {
            fields.Add(new TextField("LineNumber", TypeCode.Int32, 5));
            fields.Add(new TextField("RecordType", TypeCode.Int32, 3));
            fields.Add(new TextField("RecordSubType", TypeCode.String, 1));
        }

        /// <summary>
        /// Sets the commander product maintenance add schema.
        /// </summary>
        /// <param name="fields">The fields.</param>
        public static void SetCommanderProductMaintenanceAddSchema(TextFieldCollection fields)
        {
            //set-up fields collection, make sure the field names are the same as the 
            //CommanderSalesOrder class properties so that reflection can be used in the parsing subscriber

            fields.Clear();

            AddCommonFields(fields);
            fields.Add(new TextField("ProductCode", TypeCode.String, 20));
            fields.Add(new TextField("Description", TypeCode.String, 40));
            fields.Add(new TextField("ShortDescription", TypeCode.String, 30));
            fields.Add(new TextField("Account", TypeCode.String, 20));
            fields.Add(new TextField("UOM", TypeCode.String, 10));
        }

        /// <summary>
        /// Sets the commander product maintenance modify schema.
        /// </summary>
        /// <param name="fields">The fields.</param>
        public static void SetCommanderProductMaintenanceModifySchema(TextFieldCollection fields)
        {
            //set-up fields collection, make sure the field names are the same as the 
            //CommanderSalesOrder class properties so that reflection can be used in the parsing subscriber

            fields.Clear();

            AddCommonFields(fields);
            fields.Add(new TextField("ProductCode", TypeCode.String, 20));
            fields.Add(new TextField("Description", TypeCode.String, 40));
            fields.Add(new TextField("ShortDescription", TypeCode.String, 30));
            fields.Add(new TextField("Account", TypeCode.String, 20));
            
        }

        #endregion

        #region Protected Method(s)

        #endregion

        #region Private Method(s)

        #endregion

        #region Constructor(s)

        #endregion
    }
}