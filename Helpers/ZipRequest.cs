using System.Net;
using System.Text;
using System.IO;

namespace Zipper.Helpers
{
    public class ZipRequest
    {
        private string ZipCode { get; set; }
        private string LastName { get; set; }

        private WebRequest _req;

        public ZipRequest(string lastName, string zipCode)
        {
            LastName = lastName;
            ZipCode = zipCode;
        }


        /// <summary>
        /// Generate the web request to search zipcodes
        /// </summary>
        public void CreateWebRequest()
        {
            try
            {
                _req = WebRequest.Create(BuildUri());
                _req.Method = "GET";
                _req.ContentType = "application/x-www-form-urlencoded";
            }
            catch { _req = null; }
          
        }



        public string GetWebResponseString()
        {
            string response = string.Empty;

            if (_req != null)
            {
                try
                {
                    using (WebResponse wp = _req.GetResponse())
                    {
                        using (Stream stream = wp.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                 response = reader.ReadToEnd();
                            }
                        }
                    }
                }
                catch { } //todo report something
            }

            return response;
        }

        private string BuildUri()
        {
            StringBuilder uri = new StringBuilder(GetBaseUrl());

            uri.Append("last_name=");
            uri.Append(LastName);
            uri.Append("&");
            uri.Append("postal_code=");
            uri.Append(ZipCode);
            uri.Append("&");
            uri.Append("api_key=");
            uri.Append(GetApiKey());
          
            return uri.ToString();
        }

        private string GetBaseUrl()
        {
            return Utils.GetConfigSetting("ZipSearchUri"); 
        }

        private string GetApiKey()
        {
            return Utils.GetConfigSetting("WhitePagesApiKey"); 
        }
    }
}