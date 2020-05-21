/*************************************************************************************************
 ** FILE:	OpCoShipmentLine.cs
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
using Discovery.ComponentServices.Parsing;

namespace Discovery.BusinessObjects
{
    /// <summary>
    /// A Class 'OpCoShipmentLine' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from ShipmentLine
    /// The class holds the OpCo shipment line details
    /// </summary>
    [Serializable]
    public class OpCoShipmentLine : ShipmentLine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:OpCoShipmentLine"/> class.
        /// </summary>
        public OpCoShipmentLine()
            : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:OpCoShipmentLine"/> class.
        /// </summary>
        /// <param name="ShipmentId">The shipment id.</param>
        public OpCoShipmentLine(int ShipmentId)
            : base(ShipmentId)
        {

        }


        /// <summary>
        /// Sets the parser schema.
        /// </summary>
        /// <param name="fields">The fields.</param>
        public static void SetParserSchema(TextFieldCollection fields)
        {
            // Clear fields
            fields.Clear();

            // Add fields for schema
            fields.Add(new TextField("Type", TypeCode.String, false));
            fields.Add(new TextField("OpCoCode", TypeCode.String, false));
            fields.Add(new TextField("ShipmentNumber", TypeCode.String, false));
            fields.Add(new TextField("DespatchNumber", TypeCode.String, false));
            fields.Add(new TextField("LineNumber", TypeCode.Int32, false));
            fields.Add(new TextField("ProductCode", TypeCode.String, false));
            fields.Add(new TextField("Description1", TypeCode.String, false));
            fields.Add(new TextField("Description2", TypeCode.String, false));
            fields.Add(new TextField("Quantity", TypeCode.Int32, false));
            fields.Add(new TextField("QuantityUnit", TypeCode.String, false));
            fields.Add(new TextField("NetWeight", TypeCode.Decimal, false));
            fields.Add(new TextField("CustomerReference", TypeCode.String, false));
            fields.Add(new TextField("ConversionQuantity", TypeCode.Int32, false));
            fields.Add(new TextField("ConversionInstructions", TypeCode.String, false));
            fields.Add(new TextField("Volume", TypeCode.Decimal, false));
            fields.Add(new TextField("Width", TypeCode.Int32, false));
            fields.Add(new TextField("Length", TypeCode.Int32, false));
            fields.Add(new TextField("Grammage", TypeCode.Int32, false));
            fields.Add(new TextField("Microns", TypeCode.Int32, false));
            fields.Add(new TextField("Packing", TypeCode.String, false));
            fields.Add(new TextField("IsPanel", TypeCode.Boolean, false));
            fields.Add(new TextField("LoadCategoryCode", TypeCode.String, false));
            fields.Add(new TextField("ProductGroup", TypeCode.String, false));
            fields.Add(new TextField("IsISO9000Approved", TypeCode.Boolean, false));
        }
    }
}
