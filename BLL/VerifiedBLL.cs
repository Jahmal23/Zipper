using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zipper.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using Zipper.DAL;

namespace Zipper.BLL
{
    public static class VerifiedBLL
    {
      
        public static void Add(VerifiedPersons person)
        {
            NameSource namesToSearch = ZipperBLL.GetAllNames();

            var dbLab = DbLayer.GetRemoteDatabase();

            var labCollection = dbLab.GetCollection<VerifiedPersons>("verified");

            var query = labCollection.AsQueryable<VerifiedPersons>().Where(e => e.StreetAddress == person.StreetAddress
                                                                                && e.ZipCode == person.ZipCode);
            if (!query.Any())
            {
                labCollection.Insert(person);
            }
        }

        /// <summary>
        /// Return all listings that have not already been verified.
        /// </summary>
        /// <returns></returns>
        public static List<Listing> RemoveVerified(List<Listing> listings, ZipCodes zip)
        {
            var db = DbLayer.GetRemoteDatabase();

            var names = new List<Listing>();

            var collection = db.GetCollection<VerifiedPersons>("verified");

            var verifiedForZip = collection.AsQueryable<VerifiedPersons>();
            foreach (var l in listings)
            {
                bool newName = true;

                if (l.address == null || string.IsNullOrWhiteSpace(l.address.street))
                {
                     //this result is useless
                    newName = false;
                    continue;
                }


                foreach (var vp in verifiedForZip)
                {
                    
                    string verified = vp.StreetAddress.ToLower();

                    bool streetMatch = verified.Contains(l.address.street.ToLower());

                    bool zipMatch = vp.ZipCode.ToString() == l.address.zip;

                    if (!string.IsNullOrEmpty(l.address.aptnumber))
                    {
                        streetMatch &= verified.Contains(l.address.aptnumber.ToLower());
                    }


                    if (zipMatch && streetMatch) //not already verified!
                    {
                        newName = false;
                    }
                    
                }


                if (newName)
                {
                    names.Add(l);
                }
            }

            return names;
           
        }
    }
}