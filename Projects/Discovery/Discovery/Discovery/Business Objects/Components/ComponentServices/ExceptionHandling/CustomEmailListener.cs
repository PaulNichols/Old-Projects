using System;
using System.Collections.Generic;
using System.Text;
//custom email listener (inherits email listener), that handles any exceptions that have as property for opco (interface) and then along with the eventid we can look up the recipients from this message(if none then do nothing)

//expection table:
//event id
//opcoid
//recipients (comma seperated) 
//trace listener 
//use operators details
//autoack

namespace Discovery.Components.ComponentServices.ExceptionHandling
{
    class CustomEmailListener
    {
    }
}
