using System;
using System.Collections.Generic;
using System.Text;

namespace Discovery.BusinessObjects
{
   public  class Note:Shipment
    {
       private string salesLocationDescription;

       public string SalesLocationDescription
       {
           get { return salesLocationDescription; }
           set { salesLocationDescription = value; }
       }
    }
}
