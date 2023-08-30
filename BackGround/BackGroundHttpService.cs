using System.Text.Json;
using System;
using wise_api.Entities;
using System.Xml;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;

using System.Text.Json.Serialization;
using System.Text.Json.Nodes;
using System.Reflection.Metadata;
using System.Collections.Generic;

namespace wise_api.BackGround

{
    public class BackGroundHttpService : IHostedService, IDisposable
    {
        private readonly HttpClient _httpClient;
        private Timer _timer;

        public BackGroundHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
           // _timer = new Timer(DoHttpPost, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
           // _timer = new Timer(DoHttpGetRangeTime, null, TimeSpan.Zero, TimeSpan.FromMinutes(1)); // Ejecuta cada 0.3 minutos
            _timer = new Timer(DoHttpGetApiServer, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        //private async void DoHttpPost(object state)
        //{
        //    try
        //    {
        //        var jsonObject = new { id = "1", name = "a", lastname = "b",age=1 };

        //        string json = System.Text.Json.JsonSerializer.Serialize(jsonObject);
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //        HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:7139/api/Clientes", content);


        //        if (response.IsSuccessStatusCode)
        //        {
        //            string responseBody = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine("POST request successful. Response: " + response);
        //        }
        //        else
        //        {
        //            Console.WriteLine("POST request failed. Status code: " + response.StatusCode);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("An error occurred: " + ex.Message);
        //    }
        //}
        private async void DoHttpGet(object state)
        {

            try
            {
                
                // HttpResponseMessage response = await _httpClient.GetAsync("http://192.168.0.230:5001/api/Linea"); Servidor
                HttpResponseMessage response = await _httpClient.GetAsync("http://192.168.0.230:5001/api/Linea");


                if (response.IsSuccessStatusCode)
                {

                     string responseBody = await response.Content.ReadAsStringAsync();
                     JArray json = JArray.Parse(responseBody);
                     var finalJson = json.Last();
                    string jsonString = finalJson.ToString();
                    
                     var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    // HttpResponseMessage res = await _httpClient.PutAsync("http://192.168.0.230:5001/api/Linea", content); // servidor
                     HttpResponseMessage res = await _httpClient.PutAsync("https://localhost:7139/api/Linea", content);  //localhost

                     
                    Console.WriteLine("GET request successful. Response: " + content );
                }
                else
                {
                    Console.WriteLine("GET request failed. Status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
        private async void DoHttpGetRangeTime(object state)
        {
            var currentTime = DateTime.Now.TimeOfDay;

            // Ejecutar solo si está dentro del rango de tiempo deseado (por ejemplo, de 8 AM a 6 PM)
            if ((currentTime >= TimeSpan.FromHours(11) && currentTime <= TimeSpan.FromHours(12)) || currentTime >= TimeSpan.FromHours(12.1) && currentTime <= TimeSpan.FromHours(12.15))
            {

            
                HttpResponseMessage response = await _httpClient.GetAsync("http://192.168.0.230:5001/api/Linea");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    JArray json = JArray.Parse(responseBody);
                    var finalJson = json.Last();
                    string jsonString = finalJson.ToString();

                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    // HttpResponseMessage res = await _httpClient.PutAsync("http://192.168.0.230:5001/api/Linea", content); // servidor
                    HttpResponseMessage res = await _httpClient.PutAsync("https://localhost:7139/api/Linea", content);  //localhost
                    if (res.IsSuccessStatusCode)
                    {
                        HttpResponseMessage timeResponse = await _httpClient.GetAsync("https://localhost:7139/api/BackGroundTime");  //localhost
                        if (timeResponse.IsSuccessStatusCode)
                        {
                            Console.WriteLine("GET request successful. Response: " + timeResponse);
                        }
                        else
                        {
                            Console.WriteLine("GET request failed. Status code: " + timeResponse.StatusCode);
                        }

                    }
                    else
                    {
                        Console.WriteLine("PUT request failed. Status code: " + res.StatusCode);
                    }
                    Console.WriteLine("GET request successful. Response: " + content);
                }
                else
                {
                    Console.WriteLine("GET request failed. Status code: " + response.StatusCode);
                }
            }
        }


        private async void DoHttpGetApiServer(object state)
        {
            var currentTime = DateTime.Now.TimeOfDay;

            if ((currentTime >= TimeSpan.FromHours(9.65) && currentTime <= TimeSpan.FromHours(9.7)) || currentTime >= TimeSpan.FromHours(9.8) && currentTime <= TimeSpan.FromHours(9.9))
            {
                try
                {

                    // HttpResponseMessage response = await _httpClient.GetAsync("http://192.168.0.230:5001/api/BackNamesState"); Servidor
                    HttpResponseMessage responseData = await _httpClient.GetAsync("http://192.168.0.230:5001/api/BackNamesState");

                    if (responseData.IsSuccessStatusCode)
                    {

                        string responseBody = await responseData.Content.ReadAsStringAsync();
                        // string responseBodyLocal = await responseLocal.Content.ReadAsStringAsync();
                        JArray json = JArray.Parse(responseBody);
                        string jsonString = json.ToString();
                        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                        HttpResponseMessage responseLocal = await _httpClient.PostAsync("https://localhost:7139/api/BackNames/Server", content);
                        if (responseLocal.IsSuccessStatusCode)
                        {
                            string responseBodyLocal = await responseLocal.Content.ReadAsStringAsync();
                            // string responseBodyLocal = await responseLocal.Content.ReadAsStringAsync();
                            JArray jsonLocal = JArray.Parse(responseBodyLocal);
                            string jsonStringLocal = jsonLocal.ToString();
                            var contentLocal = new StringContent(jsonStringLocal, Encoding.UTF8, "application/json");
                            string responseFinal = await responseLocal.Content.ReadAsStringAsync();
                            HttpResponseMessage responseServer = await _httpClient.PostAsync("http://192.168.0.230:5001/api/BackNamesState/Update", contentLocal);
                            string responseFinalServer = await responseServer.Content.ReadAsStringAsync();
                            if (responseServer.IsSuccessStatusCode)
                            {
                                Console.WriteLine("POST request successful. Response: " + responseFinalServer);
                            }
                            else
                            {
                                Console.WriteLine("POST request successful. Response: " + responseFinalServer);

                            }

                            Console.WriteLine("POST request successful. Response: " + responseFinal);
                        }
                        else
                        {
                            Console.WriteLine("POST request failed. Status code: " + responseLocal.StatusCode);
                        }
                        //     Console.WriteLine("GET server request successful. Response: " + res);



                        //JArray jsonLocal = JArray.Parse(responseBodyLocal);

                        //var resultados = from item1 in json
                        //                 join item2 in jsonLocal on (string)item1["name"] equals (string)item2["name"]
                        //                 select item1;

                        //// Imprimir los resultados
                        //Console.WriteLine("Estos son los resultados" + JArray.FromObject(resultados).ToString());
                        // HttpResponseMessage res = await _httpClient.PostAsJsonAsync("http://192.168.0.230:5001/api/Linea", json);
                        //var finalJson = json.Last();
                        //string jsonString = finalJson.ToString();

                        //var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                        //// HttpResponseMessage res = await _httpClient.PutAsync("http://192.168.0.230:5001/api/Linea", content); // servidor
                        //HttpResponseMessage res = await _httpClient.PutAsync("https://localhost:7139/api/Linea", content);  //localhost


                        //  Console.WriteLine("GET server request successful. Response: " + json );

                        // Console.WriteLine("GET localhost request successful. Response: " + jsonLocal);
                    }
                    else
                    {
                        Console.WriteLine("GET request failed. Status code: " + responseData.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
