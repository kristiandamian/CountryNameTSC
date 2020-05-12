using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CountryName.DAL
{
    public class Subdivision : BaseClient
    {
        public async Task<List<Models.Subdivision>> AllFromCountryAsync(int idCountry)
        {
            List<Models.Subdivision> data = null;

            try
            {
                var uri = new Uri($"{this.URL}/countries/{idCountry}/subdivisions");

                var response = await cliente.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    var root = JsonConvert.DeserializeObject<Models.RootSubdivision>(contentResponse);
                    if (root != null && root.data != null)
                        data = root.data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return data;
        }

        public async Task<Models.Subdivision> EditAsync(int idCountry, Models.Subdivision subdivision)
        {
            Models.Subdivision data = null;
            try
            {
                var uri = new Uri($"{this.URL}/countries/{idCountry}/subdivisions/{subdivision.id}");

                var json = JsonConvert.SerializeObject(subdivision);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri);
                request.Content = content;
                var response = await cliente.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<Models.Subdivision>(contentResponse);
                }
                if ((int)response.StatusCode > 400)
                {
                    data = new Models.Subdivision();
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    data.Error = JsonConvert.DeserializeObject<Models.Error>(contentResponse);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return data;
        }

        public async Task<Models.Subdivision> SaveAsync(int idCountry,Models.Subdivision subdivision)
        {
            Models.Subdivision data = null;
            try
            {
                var uri = new Uri($"{this.URL}/countries/{idCountry}/subdivisions");

                var json = JsonConvert.SerializeObject(subdivision);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await cliente.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<Models.Subdivision>(contentResponse);
                }
                if ((int)response.StatusCode > 400)
                {
                    data = new Models.Subdivision();
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    data.Error = JsonConvert.DeserializeObject<Models.Error>(contentResponse);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return data;
        }

        public async Task<Models.Subdivision> DeleteAsync(int idCountry, int idSubdivision)
        {
            Models.Subdivision data = null;
            try
            {
                var uri = new Uri($"{this.URL}/countries/{idCountry}/subdivisions/{idSubdivision}");

                var response = await cliente.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<Models.Subdivision>(contentResponse);
                }
                if ((int)response.StatusCode > 400)
                {
                    data = new Models.Subdivision();
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    data.Error = JsonConvert.DeserializeObject<Models.Error>(contentResponse);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return data;
        }
    }
}
