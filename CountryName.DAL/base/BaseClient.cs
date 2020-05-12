using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CountryName.DAL
{
    public class BaseClient
    {
        protected string URL= "https://exam-api.tsc-dev.xyz/api/";

        protected HttpClient cliente;

        public BaseClient()
        {
            cliente = new HttpClient();

        }
    }
}
