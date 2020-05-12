using System;
using System.Collections.Generic;

namespace CountryName.DAL.Models
{
    public class RootSubdivision
    {
        public List<Subdivision> data { get; set; }
    }
    public class Subdivision
    {
        public string name { get; set; }
        public string code { get; set; }
        public int? id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        public Error Error { get; set; }
    }
}
