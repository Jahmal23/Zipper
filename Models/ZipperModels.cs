using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; 

namespace Zipper.Models
{
    public class VerifiedPersons
    {
        public MongoDB.Bson.ObjectId _id { get; set; }

        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public int ZipCode { get; set; }
    }

    public class ZipCodes
    {
        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage="Please enter a valid US zip code")] 
        public string ZipCode { get; set; }
    }

    public class NameSource
    {
        public IEnumerable<Name> Names { get; set; }
    }


    public class Name
    {
        public MongoDB.Bson.ObjectId _id { get; set; }

        public string Value { get; set; }
        public NameType Type { get; set; }
        public Language Lang { get; set; }
    }

    public enum NameType { FirstName, LastName, MiddleName }
    public enum Language { English, Portuguese, French }
}