/*************************************************************************************************
 ** FILE:	TDCShipment2.cs
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

using Discovery.BusinessObjects.Controllers;
using Discovery.ComponentServices.Parsing;
using Discovery.Utility;

namespace Discovery.BusinessObjects
{
    /// <summary>
    /// A Class 'TDCShipment' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from Shipment
    /// The class holds the TDC shipment details
    /// </summary>

    public partial class TDCShipment : Shipment
    {
        /// <summary>
        /// Routings the populate site fields.
        /// </summary>
        /// <param name="siteSchema">The optrak site file schema.</param>
        public void RoutingPopulateSiteFields(TextFieldCollection siteSchema)
        {

            //check for nulls
            siteSchema["SiteCode"].Value = LocationCode;
            siteSchema["OpenTime"].Value = "Z";
            siteSchema["MaximumGrossWeight"].Value = VehicleMaxWeight==Null.NullInteger||(int)VehicleMaxWeight==0?"":((int)VehicleMaxWeight).ToString();
            siteSchema["CheckInDuration"].Value = CheckInTime;
            siteSchema["LoadMethod"].Value = "Load";
            siteSchema["UnloadMethod"].Value = "Unload";
            siteSchema["DefaultDepot"].Value = DeliveryWarehouseCode;
            siteSchema["Name"].Value = ShipmentName;
            siteSchema["Address1"].Value = PAFAddress.Line1;
            siteSchema["Address2"].Value = PAFAddress.Line2;
            siteSchema["Address3"].Value = PAFAddress.Line3;
            siteSchema["Town"].Value = PAFAddress.Line4;
            siteSchema["Postcode"].Value = PAFAddress.PostCode;
            siteSchema["Region"].Value = PAFAddress.Line5;
            siteSchema["X"].Value = PAFAddress.Easting;
            siteSchema["Y"].Value = PAFAddress.Northing;
            siteSchema["Telephone"].Value = ShipmentContact.TelephoneNumber;
            siteSchema["RHRegion"].Value = DeliveryWarehouse.OptrakRegion.Code;
            siteSchema["Route"].Value = RouteCode;
            siteSchema["Category"].Value = "";
            siteSchema["TailLiftRequired"].Value = TailLiftRequired ? "1" : "0";
            siteSchema["OriginalX"].Value = siteSchema["X"].Value;
            siteSchema["OriginalY"].Value = siteSchema["Y"].Value;
        }

        /// <summary>
        /// Routings the populate shipment fields.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <param name="allRoutes">All routes.</param>
        /// <param name="allTransactionTypes">All transaction types.</param>
        /// <param name="nextWorkingDate">The next working date.</param>
        public void RoutingPopulateShipmentFields(TextFieldCollection schema, List<Route> allRoutes, List<TransactionType> allTransactionTypes, 
            DateTime nextWorkingDate)
        {
            schema["OrderCode"].Value = string.Concat(OpCoCode,"-",ShipmentNumber, "-", DespatchNumber);

            Route currentRoute = allRoutes.Find(
                    delegate(Route route)
                    {
                        return (route.Code == this.RouteCode);
                    });

            TransactionType currentTransactionTypeDetail = allTransactionTypes.Find(
                  delegate(TransactionType transactionType)
                  {
                      return (transactionType.Code == this.TransactionTypeCode);
                  });

            if (currentRoute != null)
            {
                if (currentTransactionTypeDetail != null)
                {
                    bool isTDCCollection = currentTransactionTypeDetail.IsCollection;
                    bool isSample = currentTransactionTypeDetail.IsSample;

                    //get the times but drop the seconds
                    string formattedAfterTime = "";
                    if (!Null.IsNull(AfterTime ))
                    {
                        formattedAfterTime = AfterTime.Substring(0, 5);
                    }

                    string formattedBeforeTime = "";
                    if (!Null.IsNull(BeforeTime))
                    {
                        formattedBeforeTime = BeforeTime.Substring(0, 5);
                    }
                                    

                    const string dateFormat = "dd/MM/yy";

                    schema["CollectAfter"].Value = isTDCCollection
                                                       ? string.Concat(formattedAfterTime, " ", nextWorkingDate.ToString(dateFormat))
                                                       : "";
                    schema["CollectBefore"].Value = isTDCCollection
                                                        ? string.Concat(formattedBeforeTime, " ", nextWorkingDate.ToString(dateFormat))
                                                        : "";
                    schema["DeliverAfter"].Value = isTDCCollection
                                                       ? ""
                                                       :
                                                   string.Concat(formattedAfterTime, " ",
                                                                 nextWorkingDate.ToString(dateFormat));
                    schema["DeliverBefore"].Value = isTDCCollection
                                                        ? ""
                                                        :
                                                    string.Concat(formattedBeforeTime, " ",
                                                                  nextWorkingDate.ToString(dateFormat));
                    schema["CollectFrom"].Value = isTDCCollection ? LocationCode : DeliveryWarehouseCode;
                    schema["DeliverTo"].Value = isTDCCollection ? DeliveryWarehouseCode : LocationCode;
                    schema["Sample"].Value = isSample ? "1" : "0";
                    schema["JP2"].Value = "";
                    schema["TrampingNo"].Value = "";
                    schema["OrigCustCode"].Value = "";
                    schema["AssocDepot"].Value = DeliveryWarehouse.Code;
                    schema["CustReturn"].Value = isTDCCollection ? "1" : "0";
                    schema["TimeCode"].Value = "";
                }
                else
                {
                    throw new Exception(string.Format("The transaction type '{0}' could not be found", TransactionType));
                }
            }
            else
            {
                throw new Exception(string.Format("The Route Code '{0}' could not be found", RouteCode));
            }
        }
    }
}