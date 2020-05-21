using System;
using Discovery.ComponentServices.Parsing;
using Discovery.RequestManagement;

namespace Discovery.BusinessSubscribers
{
    /// <summary>
    /// A class to parse commander goods in
    /// </summary>
    public class CommanderGoodsInParsing : CommanderStockAndGoodsInParsing
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:CommanderStockParsing"/> class.
        /// </summary>
        /// <param name="requestProcessor">The request processor.</param>
        public CommanderGoodsInParsing(RequestProcessor requestProcessor)
            : base(requestProcessor)
        {
        }

        /// <summary>
        /// Gets the type of the item.
        /// </summary>
        /// <returns></returns>
        protected override string GetItemType()
        {
            return Stock_Receipt;
        }

        /// <summary>
        /// Gets the type of the sub item.
        /// </summary>
        /// <returns></returns>
        protected override string GetSubItemType()
        {
            return "S";
        }

        /// <summary>
        /// Sets the item schema. These fields describe the format of a single line
        /// </summary>
        /// <param name="fields">The fields.</param>
        protected override void SetItemSchema(TextFieldCollection fields)
        {
            fields.Clear();
            AddCommonFields(fields);
            fields.Add(new TextField("Site", TypeCode.String, 10));
            fields.Add(new TextField("RecieptRef", TypeCode.String, 20));
            fields.Add(new TextField("LineNumber", TypeCode.Int32, 5));
            fields.Add(new TextField("ProductCode", TypeCode.String, 20));
            fields.Add(new TextField("QuantityReceived", TypeCode.Int32, 19));
            fields.Add(new TextField("CustomerReferenceNumber", TypeCode.String, 20));
            fields.Add(new TextField("IsAdviceNoteRequired", TypeCode.Int32, 2));
            fields.Add(new TextField("AdviceNoteNumber", TypeCode.String, 30));
        }
    }
}