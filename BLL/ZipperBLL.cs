using System.Collections.Generic;
using System.Linq;
using Zipper.Models;
using Zipper.Helpers;
using Newtonsoft.Json.Linq;

namespace Zipper.BLL
{
    public static class ZipperBLL
    {

        public static List<WPerson> GetSearchResults(NameSource nameSource, ZipCodes zip)
        {
            List<WPerson> found = new List<WPerson>();

            if (IsValid(nameSource, zip))
            {
                foreach (Name name in nameSource.Names)
                {
                    var zipRequest = new ZipRequest(name.Value, zip.ZipCode);

                    zipRequest.CreateWebRequest();

                    string response = zipRequest.GetWebResponseString();

                    BuildPersonSearchResults(response, zip, found);

                    Pause();

                }
            }
            
            var unique = GetUniqueResults(found);

            var finalList = VerifiedBLL.RemoveVerified(unique, zip);

            return finalList.OrderByDescending(x => x.Street).ThenBy(x => x.House).ToList();
        }

        /// <summary>
        /// White pages has a max of 2 queries per second
        /// </summary>
        private static void Pause()
        {
            System.Threading.Thread.Sleep(3000);
        }


        /// <summary>
        /// Attempt to get a response from the uri and translate it into our business object
        /// </summary>
        private static void BuildPersonSearchResults(string response, ZipCodes zip, List<WPerson> found)
        {
            if (!string.IsNullOrEmpty(response))
            {
                try
                {
                    dynamic rootObject = JObject.Parse(response);

                    List<WPerson> persons = BuildPersonList(rootObject);
                    CleanAndAdd(persons, zip, found);
                }
                catch { }
            }
        }


        private static List<WPerson> BuildPersonList(dynamic rootObject)
        {
            //these will be person keys since we a do "person" search on the api
            JArray keys = rootObject.results as JArray;

            List<WPerson> whitePagePersons = new List<WPerson>();

            foreach (string k in keys)
            {
                var wperson = new WPerson();
                dynamic dPerson = rootObject.dictionary[k] as dynamic;

                wperson.Name = dPerson.best_name;

                //now get the location object.
                if (dPerson.locations != null && dPerson.locations.Count > 0)
                {
                    wperson.LocationKey = dPerson.locations[0].id.key;
                }


                //get the phone object
                if (dPerson.phones != null && dPerson.phones.Count > 0)
                {
                    wperson.PhoneKey = dPerson.phones[0].id.key;
                }

                whitePagePersons.Add(wperson);
            }


            foreach (WPerson p in whitePagePersons) //using the Location and Phone keys, complete the rest of the object.
            {
                if (!string.IsNullOrEmpty(p.LocationKey)) //else discard
                {
                    dynamic location = rootObject.dictionary[p.LocationKey] as dynamic;

                    if (location != null)
                    {
                        p.Address = location.standard_address_line1;
                        p.Zip = location.postal_code;
                        p.Street = location.street_name;
                        p.House = location.house;

                        dynamic latlon = location.lat_long;

                        if (latlon  != null)
                        {
                            p.Lat = latlon.latitude;
                            p.Lon = latlon.longitude;
                        }

                    }
                }


                if (!string.IsNullOrEmpty(p.PhoneKey))
                {
                    dynamic phone = rootObject.dictionary[p.PhoneKey] as dynamic;
                    
                    if (phone != null)
                    {
                        p.Phone = Utils.FormatPhoneNumber(phone.phone_number.ToString());    
                    }
                }
            }

            return whitePagePersons;

        }

        /// <summary>
        /// Fill the found list with the true matches from white pages.
        /// </summary>
        public static void CleanAndAdd(List<WPerson> persons, ZipCodes zip, List<WPerson> found)
        {
            if (persons != null && persons.Count > 0)
            {
                 //white page will return nearby zipcodes on non matches, we want exact
                var hits = persons.Where(x => !string.IsNullOrEmpty(x.Address)  && x.Zip == zip.ZipCode);

                if (hits.Count() > 0)
                {
                    found.AddRange(hits);
                }
            }
        }

       


        /// <summary>
        /// Ensure data is sound before searching
        /// </summary>
        private static bool IsValid(NameSource nameSource, ZipCodes zip)
        {
            return (nameSource != null && zip != null && nameSource.Names != null);
        }

        /// <summary>
        /// White pages likes to return multiple results for each secondary member of household/
        /// </summary>
        private static List<WPerson> GetUniqueResults(List<WPerson> found)
        {
            //get all where there doesn't exist another entry of the same address
            foreach (var i in found)
            {
                //you've already been marked as a dup, next!
                if (!i.IsUnique) continue;

                foreach (var j in found)
                {
                    if (i != j && (i.Address == j.Address))
                    {
                       j.IsUnique = false;
                    }
                }
              
            }

            return found.Where(x => x.IsUnique).ToList();
       }

    }
}