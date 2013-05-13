using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zipper.Models;
using Zipper.Helpers;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Zipper.BLL
{
    public static class ZipperBLL
    {

        public static List<Listing> GetAddresses(NameSource nameSource, ZipCodes zip)
        {
            List<Listing> found = new List<Listing>();
            
            if (IsValid(nameSource, zip))
            {
                foreach (Name name in nameSource.Names)
                {
                    var zipRequest = new ZipRequest(name.Value, zip.ZipCode);
                    BuildPersonSearchResults(zipRequest.GetWebRequest(),zip, found);
                    Pause();
                }
            }
            
            var unique = GetUniqueResults(found);

            var finalList = VerifiedBLL.RemoveVerified(unique, zip);

            return finalList.OrderByDescending(x => x.address.street).ThenBy(x => x.address.house).ToList();
        }

        /// <summary>
        /// White pages has a max of 2 queries per second
        /// </summary>
        private static void Pause()
        {
            System.Threading.Thread.Sleep(1000);
        }


        /// <summary>
        /// Attempt to get a response from the uri and translate it into our business object
        /// </summary>
        private static void BuildPersonSearchResults(WebRequest req, ZipCodes zip, List<Listing> found)
        {
            if (req != null)
            {
                try
                {
                    using (WebResponse response = req.GetResponse())
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                 string webresponse = reader.ReadToEnd();

                                 var ser = new JavaScriptSerializer();

                                 RootObject results = ser.Deserialize<RootObject>(webresponse);
                                
                                 ProcessResults(results, zip, found);

                           }
                        }
                    }
                }
                catch {  } //todo report something
            }

        }

        /// <summary>
        /// Return the true matches from white pages.
        /// </summary>
        public static void ProcessResults(RootObject results, ZipCodes zip, List<Listing> found)
        {
            if (results != null && results.listings != null && results.listings.Count > 0)
            {
                 //white page will return nearby zipcodes on non match, we want exact
                var hits = results.listings.Where(x => x.address != null && x.address.zip == zip.ZipCode);

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
        /// White pages likes to return multiple results for each secondary member of household, 
        /// while listing the primary as the display name.  Clean it up.
        /// </summary>
        private static List<Listing> GetUniqueResults(List<Listing> found)
        {
            //get all where there doesn't exist another entry of the same display name or address
            List<Listing> unique = new List<Listing>();

            foreach (var i in found)
            {
                bool dupe = false;
                foreach (var j in found)
                {
                    if (i != j && (i.address.fullstreet == j.address.fullstreet))
                    {
                        dupe = true;
                    }
                }

                if (!dupe)
                {
                    unique.Add(i);
                }
            }

            return unique;
       }

        public static NameSource GetAllNames()
        {
            NameSource nameSource = new NameSource();

           // Name namea = new Name { Value = "Almeida", Lang = Language.Portuguese, Type = NameType.LastName };
           //Name namec = new Name { Value = "Antunes", Lang = Language.Portuguese, Type = NameType.LastName };
            //Name namee= new Name { Value = "Esteves", Lang = Language.Portuguese, Type = NameType.LastName };
           // Name nameh = new Name { Value = "Figueira", Lang = Language.Portuguese, Type = NameType.LastName };
            //Name namel = new Name { Value = "Martins", Lang = Language.Portuguese, Type = NameType.LastName };
            //Name namen = new Name { Value = "Meneses", Lang = Language.Portuguese, Type = NameType.LastName };
            //Name namet = new Name { Value = "Ribeiro", Lang = Language.Portuguese, Type = NameType.LastName };
            //Name nameu = new Name { Value = "Rodrigues", Lang = Language.Portuguese, Type = NameType.LastName };
            //Name namez = new Name { Value = "Tavares", Lang = Language.Portuguese, Type = NameType.LastName };
           // Name nameaa = new Name { Value = "Teixeira", Lang = Language.Portuguese, Type = NameType.LastName };
         
     
            nameSource.Names = new List<Name> { 
                new Name { Value = "Andrade", Lang = Language.Portuguese, Type = NameType.LastName }, 
                new Name { Value = "Carvalho", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Fernandes", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Ferreira", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Gonçalves", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Lopes", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Moreira", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Nogueira", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Oliveira", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Pires", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Pereira", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "dos Santos", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Silva", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Siqueira", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Soares", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "da Silva", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Brandão", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Pinheiro", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Machado", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Leite", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Gonsalves", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Gomes", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Domingues", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Lima", Lang = Language.Portuguese, Type = NameType.LastName },
                new Name { Value = "Rodrigues", Lang = Language.Portuguese, Type = NameType.LastName }
            };

            return nameSource;
        }

       


    }
}