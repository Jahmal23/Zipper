using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Net;
using System.Text;

namespace Zipper.Helpers
{
    public class ZipRequest
    {
        private string ZipCode { get; set; }
        private string LastName { get; set; }

        public ZipRequest(string lastName, string zipCode)
        {
            LastName = lastName;
            ZipCode = zipCode;
        }


        /// <summary>
        /// Generate the web request to search zipcodes
        /// </summary>
        public WebRequest GetWebRequest()
        {
            WebRequest req;

            try
            {
                req = WebRequest.Create(BuildUri());
                req.Method = "GET";
                req.ContentType = "application/x-www-form-urlencoded";
            }
            catch { req = null; }

            return req;
        }

        private string BuildUri()
        {
            StringBuilder uri = new StringBuilder(GetBaseUrl());

            uri.Append("lastname=");
            uri.Append(LastName);
            uri.Append(";");
            uri.Append("zip=");
            uri.Append(ZipCode);
            uri.Append(";");
            uri.Append("api_key=");
            uri.Append(GetApiKey());
            uri.Append(";");
            uri.Append("outputtype=JSON");

            return uri.ToString();
        }

        private string GetBaseUrl()
        {
            return WebConfigurationManager.AppSettings["ZipSearchUri"];
        }

        private string GetApiKey()
        {
            return WebConfigurationManager.AppSettings["WhitePagesApiKey"];
        }
    }
}