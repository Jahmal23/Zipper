using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Zipper.Models
{

    public class WPerson
    {
        private bool _isUnique = true;

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string House { get; set; }
        public string Street { get; set; }
        public string LocationKey { get; set; }
        public string PhoneKey { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }


        public bool IsUnique
        {
            get { return _isUnique; }
            set { _isUnique = value; }
        }
    }

}