﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System.Web.Configuration;

namespace Zipper.DAL
{
    public static class DbLayer
    {

        public static MongoDatabase GetDatabase()
        {
            return (IsDebugMode() ? GetLocalDatabase() : GetRemoteDatabase());
        }


        public static bool IsDebugMode()
        {
            return Convert.ToBoolean(WebConfigurationManager.AppSettings["DebugMode"]);
        }

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

            var credential = MongoCredential.CreateMongoCRCredential("verifieddb", "admin", "mongo23");

            settings.Credentials = new[] {credential};

            // Create server object to communicate with our server
            MongoServer server = new MongoServer(settings);
           
            // Get our database instance to reach collections and data
            var database = server.GetDatabase("verifieddb");

            return database;
        }

    }
}