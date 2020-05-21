using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Diagnostics;

namespace Discovery.Utility
{
    public class UIHelper
    {
        public static string GenerateEmailUrl(string emailAddress)
        {
            return "<a href='mailto:" + emailAddress + "'>" + emailAddress + "</a>";
        }

        public static string GenerateUrlReferrer(HttpContext context, string[] queryParams)
        {
            StringBuilder sbQueryString = new StringBuilder();

            // Append the request path
            sbQueryString.Append(context.Request.Path);
            // See if we have any query parameters to append
            if (queryParams.Length > 0)
            {
                // The first param must not start with "?" or "&"
                bool firstParam = true;
                // Append the params
                foreach (string queryParam in queryParams)
                {
                    // Make sure that there's an ='s in the param as it should be a name value pair
                    Debug.Assert(queryParam.IndexOf('=') != -1);

                    // See if we start with a "?" or "&" and it's the first param
                    if (firstParam)
                    {
                        switch (queryParam.ToCharArray()[0])
                        {
                            case '?':
                                {
                                    sbQueryString.Append(queryParam);
                                    break;
                                }
                            case '&':
                                {
                                    sbQueryString.AppendFormat("?{0}", queryParam.Substring(1));
                                    break;
                                }
                            default:
                                {
                                    sbQueryString.AppendFormat("?{0}", queryParam);
                                    break;
                                }
                        }
                        // Processed the first param
                        firstParam = false;
                    }
                    else
                    {
                        switch (queryParam.ToCharArray()[0])
                        {
                            case '?':
                                {
                                    sbQueryString.AppendFormat("&{0}", queryParam.Substring(1));
                                    break;
                                }
                            case '&':
                                {
                                    sbQueryString.AppendFormat("{0}", queryParam);
                                    break;
                                }
                            default:
                                {
                                    sbQueryString.AppendFormat("&{0}", queryParam);
                                    break;
                                }
                        }
                    }
                }
            }
            // Encode the referrer url
            string encodedUrl = context.Server.UrlEncode(sbQueryString.ToString());
            // Done
            return string.Concat("UrlReferrer=", encodedUrl);
        }

    }
}
