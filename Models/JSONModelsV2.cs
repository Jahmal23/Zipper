using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zipper.Models
{
    //conforms to whitepages api 2.0
    public class Id
    {
        public string key { get; set; }
        public string url { get; set; }
        public string type { get; set; }
        public string uuid { get; set; }
        public string durability { get; set; }
    }

    public class Name
    {
        public object salutation { get; set; }
        public string first_name { get; set; }
        public object middle_name { get; set; }
        public string last_name { get; set; }
        public object suffix { get; set; }
        public object valid_for { get; set; }
    }

    public class Id2
    {
        public string key { get; set; }
        public string url { get; set; }
        public string type { get; set; }
        public string uuid { get; set; }
        public string durability { get; set; }
    }

    public class Start
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
    }

    public class Stop
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
    }

    public class ValidFor
    {
        public Start start { get; set; }
        public Stop stop { get; set; }
    }

    public class Location
    {
        public Id2 id { get; set; }
        public ValidFor valid_for { get; set; }
        public bool is_historical { get; set; }
        public string contact_type { get; set; }
        public int contact_creation_date { get; set; }
    }

    public class Id3
    {
        public string key { get; set; }
        public string url { get; set; }
        public string type { get; set; }
        public string uuid { get; set; }
        public string durability { get; set; }
    }

    public class BestLocation
    {
        public Id3 id { get; set; }
    }

    public class PersonCc95649e27bd48c9B80d311ee9cd276eDurable
    {
        public Id id { get; set; }
        public string type { get; set; }
        public List<Name> names { get; set; }
        public object age_range { get; set; }
        public List<Location> locations { get; set; }
        public object phones { get; set; }
        public string best_name { get; set; }
        public BestLocation best_location { get; set; }
    }
}