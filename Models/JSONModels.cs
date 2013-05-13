using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Zipper.Models
{
    /// <summary>
    /// Generated using http://json2csharp.com/
    /// </summary>
    [DataContract]
    public class Findneighbors
    {
        [DataMember]
        public string linktext { get; set; }
        [DataMember]
        public string url { get; set; }
    }

    [DataContract]
    public class Viewmap
    {
        [DataMember]
        public string linktext { get; set; }
        [DataMember]
        public string url { get; set; }
    }

    [DataContract]
    public class Drivingdirections
    {
        [DataMember]
        public string linktext { get; set; }
        [DataMember]
        public string url { get; set; }
    }

    [DataContract]
    public class Viewdetails
    {
        [DataMember]
        public string linktext { get; set; }
        [DataMember]
        public string url { get; set; }
    }

    [DataContract]
    public class Moreinfolinks
    {
        [DataMember]
        public Findneighbors findneighbors { get; set; }
        [DataMember]
        public Viewmap viewmap { get; set; }
        [DataMember]
        public Drivingdirections drivingdirections { get; set; }
        [DataMember]
        public Viewdetails viewdetails { get; set; }
    }

    [DataContract]
    public class Listingmeta
    {
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string lastvalidated { get; set; }
        [DataMember]
        public Moreinfolinks moreinfolinks { get; set; }
    }

    [DataContract]
    public class Person
    {
        [DataMember]
        public string firstname { get; set; }
        [DataMember]
        public string lastname { get; set; }
        [DataMember]
        public string rank { get; set; }
        [DataMember]
        public string middlename { get; set; }
    }

    [DataContract]
    public class Geodata
    {
        [DataMember]
        public string longitude { get; set; }
        [DataMember]
        public string latitude { get; set; }
        [DataMember]
        public string geoprecision { get; set; }
    }

    [DataContract]
    public class Address
    {
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public string house { get; set; }
        [DataMember]
        public string zip4 { get; set; }
        [DataMember]
        public string street { get; set; }
        [DataMember]
        public string fullstreet { get; set; }
        [DataMember]
        public string state { get; set; }
        [DataMember]
        public string apttype { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string zip { get; set; }
        [DataMember]
        public string streettype { get; set; }
        [DataMember]
        public string aptnumber { get; set; }
        [DataMember]
        public string deliverable { get; set; }
    }

    [DataContract]
    public class Phonenumber
    {
        [DataMember]
        public string linenumber { get; set; }
        [DataMember]
        public string areacode { get; set; }
        [DataMember]
        public string fullphone { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string rank { get; set; }
        [DataMember]
        public string exchange { get; set; }
    }

    [Serializable, DataContract(Name = "Listing")]
    public class Listing
    {
        [DataMember]
        public Listingmeta listingmeta { get; set; }
        [DataMember]
        public List<Person> people { get; set; }
        [DataMember]
        public Geodata geodata { get; set; }
        [DataMember]
        public Address address { get; set; }
        [DataMember]
        public List<Phonenumber> phonenumbers { get; set; }
        [DataMember]
        public string displayname { get; set; }
    }

    [DataContract]
    public class Homepage
    {
        [DataMember]
        public string linktext { get; set; }
        [DataMember]
        public string url { get; set; }
    }

    [DataContract]
    public class Mapallresults
    {
        [DataMember]
        public string linktext { get; set; }
        [DataMember]
        public string url { get; set; }
    }

    [DataContract]
    public class Self
    {
        [DataMember]
        public string linktext { get; set; }
        [DataMember]
        public string url { get; set; }
    }

    [DataContract]
    public class Allresults
    {
        [DataMember]
        public string linktext { get; set; }
        [DataMember]
        public string url { get; set; }
    }

    [DataContract]
    public class Searchlinks
    {
        [DataMember]
        public Homepage homepage { get; set; }
        [DataMember]
        public Mapallresults mapallresults { get; set; }
        [DataMember]
        public Self self { get; set; }
        [DataMember]
        public Allresults allresults { get; set; }
    }

    [DataContract]
    public class Recordrange
    {
        [DataMember]
        public string lastrecord { get; set; }
        [DataMember]
        public string firstrecord { get; set; }
        [DataMember]
        public int totalavailable { get; set; }
    }

    [DataContract]
    public class Meta
    {
        [DataMember]
        public string apiversion { get; set; }
        [DataMember]
        public string linkexpiration { get; set; }
        [DataMember]
        public Searchlinks searchlinks { get; set; }
        [DataMember]
        public Recordrange recordrange { get; set; }
        [DataMember]
        public string searchid { get; set; }
    }

    [DataContract]
    public class Result
    {
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string message { get; set; }
    }

     [Serializable, DataContract(Name = "RootObject")]
    public class RootObject
    {
        [DataMember]
        public List<Listing> listings { get; set; }
        [DataMember]
        public Meta meta { get; set; }
        [DataMember]
        public Result result { get; set; }
    }
}