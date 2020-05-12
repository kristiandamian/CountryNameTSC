using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CountryName.DAL
{
    public class Country : BaseClient
    {
        public async Task<List<Models.Country>> AllAsync()
        {
            List<Models.Country> data = null;

            try
            {
                var uri = new Uri($"{this.URL}/countries");

                var response = await cliente.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    var root = JsonConvert.DeserializeObject<Models.RootCountry>(contentResponse);
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

        public async Task<Models.Country> EditAsync(Models.Country Country)
        {
            Models.Country data = null;
            try
            {
                var uri = new Uri($"{this.URL}/countries/{Country.id}");

                var json = JsonConvert.SerializeObject(Country);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri);
                request.Content = content;
                var response = await cliente.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<Models.Country>(contentResponse);
                }
                if ((int)response.StatusCode > 400)
                {
                    data = new Models.Country();
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
        public async Task<Models.Country> SaveAsync( Models.Country Country)
        {
            Models.Country data = null;
            try
            {
                if (Country.id.HasValue)
                    return await EditAsync(Country);
                var uri = new Uri($"{this.URL}/countries");

                var json = JsonConvert.SerializeObject(Country);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await cliente.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<Models.Country>(contentResponse);
                }
                if((int)response.StatusCode >400)
                {
                    data = new Models.Country();
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


        public async Task<Models.Country> DeleteAsync(int idCountry)
        {
            Models.Country data = null;
            try
            {
                var uri = new Uri($"{this.URL}/countries/{idCountry}");

                var response = await cliente.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<Models.Country>(contentResponse);
                }
                if ((int)response.StatusCode > 400)
                {
                    data = new Models.Country();
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
