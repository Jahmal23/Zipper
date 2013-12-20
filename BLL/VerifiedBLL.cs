using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zipper.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;


namespace Zipper.BLL
{
    public static class VerifiedBLL
    {
        private static MongoDatabase GetLocalDatabase()
        {
            // Create server settings to pass connection string, timeout, etc.
            MongoServerSettings settings = new MongoServerSettings();
            settings.Server = new MongoServerAddress("localhost", 27017);

            // Create server object to communicate with our server
            MongoServer server = new MongoServer(settings);
            // Get our database instance to reach collections and data
            var database = server.GetDatabase("VerifiedDB");

            return database;
        }

        private static MongoDatabase GetRemoteDatabase()
        {
            // Create server settings to pass connection string, timeout, etc.
            MongoServerSettings settings = new MongoServerSettings();
            settings.Server = new MongoServerAddress("ds053778.mongolab.com", 53778);

           // Create server object to communicate with our server
            MongoServer server = new MongoServer(settings);

            // Get our database instance to reach collections and data
            var database = server.GetDatabase("verifieddb",new MongoCredentials("admin", "mongo23"));
            
            return database;
        }

        public static void Add(VerifiedPersons person)
        {
            var dbLab = GetRemoteDatabase();
            
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
            var db = GetRemoteDatabase();

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