using System;
using System.Collections.Generic;

namespace CountryName.DAL.Models
{
    public class Error
    {
        public bool error { get; set; }
        public int errorCode { get; set; }
        public IList<string> errorMessages { get; set; }
    }
}
