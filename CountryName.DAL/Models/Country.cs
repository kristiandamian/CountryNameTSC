using System;
using System.Collections.Generic;

namespace CountryName.DAL.Models
{
    public class RootCountry
    {
        public List<Country> data { get; set; }
    }
    public class RootSingleCountry
    {
        public Country data { get; set; }
    }
    public class Country
    {
        public string name { get; set; }
        public string alpha2 { get; set; }
        public string alpha3 { get; set; }
        public int? code { get; set; }
        public string iso_3166_2 { get; set; }
        public bool? is_independent { get; set; }
        public int? id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        public Error Error { get; set; }
    }
}
