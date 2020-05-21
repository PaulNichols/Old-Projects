/*************************************************************************************************
 ** FILE:	TDCShipmentLine.cs
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
using Discovery.ComponentServices.Parsing;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using ValidationFramework;

namespace Discovery.BusinessObjects
{
    /// <summary>
    /// A Class 'TDCShipmentLine' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from ShipmentLine
    /// The class holds the TDC shipment line details
    /// </summary>
    [Serializable]
    public class TDCShipmentLine : ShipmentLine
    {
        // Load category
        private LoadCategory loadCategory = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TDCShipmentLine"/> class.
        /// </summary>
        public TDCShipmentLine()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TDCShipmentLine"/> class.
        /// </summary>
        /// <param name="ShipmentId">The shipment id.</param>
        public TDCShipmentLine(int ShipmentId)
            : base(ShipmentId)
        {
        }


        /// <summary>
        /// Routings the populate shipment line fields.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <param name="parentShipment">The parent shipment.</param>
        public void RoutingPopulateShipmentLineFields(TextFieldCollection schema,TDCShipment parentShipment)
        {
            schema["Order"].Value = string.Concat(parentShipment.OpCoCode,"-",parentShipment.ShipmentNumber, "-", parentShipment.DespatchNumber);
            schema["LineCode"].Value = LineNumber.ToString();
            schema["Product"].Value = ProductCode;
            schema["MaximumUnits"].Value = Quantity.ToString();
            schema["MaximumWeight"].Value = GrossWeight.ToString("######0.##");
            schema["MaximumVolume"].Value = Volume.ToString("###0.###");
            schema["Width"].Value = Width.ToString(); ;
            schema["Length"].Value = Length.ToString() ;
        }

        /// <summary>
        /// Routings the populate product fields.
        /// </summary>
        /// <param name="schema">The schema.</param>
        public void RoutingPopulateProductFields(TextFieldCollection schema)
        {
            schema["Name"].Value = ProductCode;
            //only description 1 has been outputted because that field alone is 50 chars and the optrak field is 32 max
            schema["Description"].Value = Description1;// string.Concat(Description1, Description1.EndsWith(" ") || Description2.StartsWith(" ") ? "" : " ", Description2);
            schema["LoadCategory"].Value = LoadCategoryCode;
            schema["TypeData"].Value = IsPanel?"1":"0";
            schema["ProductGroup"].Value = ProductGroup;
            schema["Width"].Value = Width.ToString();
            schema["Length"].Value = Length.ToString();
        }

        /// <summary>
        /// The load category used by Optrak. Options are:BP – Bulk Packed, BU – Bundle, BX – Box, CT – Carton, DK - ?,LG – Large,MR – Misc Reel, NA – Unknown, PK – Packet, R3 – 3K Ree, lR7 – 7K Reel, R9 – 9K Reel, UN – Unit
        /// </summary>
        //***LAS** 1.6 Spec, if not specified we make it "NA"
        [RequiredValidator("Load Category Code is required.", "*")]
        [LengthValidator(0, 2, "Load Category Code must be between {0} and {1} characters in length.", "*")]
        [CustomValidator("Load Category Code must be a valid value", "ValidLoadCategoryCode", "*")]
        public override string LoadCategoryCode
        {
            get 
            { 
                return loadCategoryCode; 
            }
            set 
            { 
                // Load the load category from the code
                loadCategoryCode = (string.IsNullOrEmpty(value))?"NA":value.ToUpper(); 
                // Load the load category by code
                loadCategory = LoadCategoryController.GetLoadCategory(loadCategoryCode);
            }
        }

        /// <summary>
        /// Custom validation method to validate sales branch location
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:ValidationFramework.CustomValidationEventArgs"/> instance containing the event data.</param>
        public void ValidLoadCategoryCode(object sender, CustomValidationEventArgs e)
        {
            // Seed the message
            e.ErrorMessage = string.Format("Load Category Code '{0}' is not a valid Load Category Code.", this.LoadCategoryCode);
            // If a sales branch code was specified see if it's valid
            e.IsValid = (null != this.loadCategory && -1 != this.loadCategory.Id);
        }

        public LoadCategory LoadCategory
        {
            get { return loadCategory; }
        }
    }
}