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
        public static List<WPerson> RemoveVerified(List<WPerson> persons, ZipCodes zip)
        {
            var db = DbLayer.GetRemoteDatabase();

            var newPersons = new List<WPerson>();

            var collection = db.GetCollection<VerifiedPersons>("verified");

            var verifiedForZip = collection.AsQueryable<VerifiedPersons>();
            foreach (WPerson p in persons)
            {
                bool newName = true;

                foreach (var vp in verifiedForZip)
                {
                    
                    string verified = vp.StreetAddress.ToLower();

                    bool streetMatch = verified.Contains(p.Address.ToLower());

                    bool zipMatch = vp.ZipCode.ToString() == p.Zip;
                
                    if (zipMatch && streetMatch) //not already verified!
                    {
                        newName = false;
                    }
                    
                }

                if (newName)
                {
                    newPersons.Add(p);
                }
            }

            return newPersons;
           
        }
    }
}