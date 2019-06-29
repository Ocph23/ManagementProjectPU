using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringPU
{
    public class RestServices : HttpClient, IDisposable
    {
        public RestServices()
        {
            this.MaxResponseContentBufferSize = 25600000;
            this.BaseAddress = new Uri(Helper.Server);

            SetHeader();
        }

        private async void SetHeader()
        {
            var token = await Helper.GetToken();
            if (token != null)
            {
                //  this.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.access_token);
            }
        }

        public async Task<T> Post<T>(string uri, object t) where T : class
        {
            try
            {
                await Task.Delay(200);
                var result = await PostAsync(uri, Helper.Content(t));
                var responseText = await result.Content.ReadAsStringAsync();
                var obj = Activator.CreateInstance<T>();
                if (result.IsSuccessStatusCode)
                {
                    obj = JsonConvert.DeserializeObject<T>(responseText);
                }
                else
                {
                    throw new SystemException(responseText);
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<T> Get<T>(string uri)
        {
            try
            {
                await Task.Delay(200);
                var result = await GetAsync(uri);
                var responseText = await result.Content.ReadAsStringAsync();
                var obj = Activator.CreateInstance<T>();
                if (result.IsSuccessStatusCode)
                {
                    obj = JsonConvert.DeserializeObject<T>(responseText);
                }
                else
                {
                    throw new SystemException(responseText);
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<T> Put<T>(string uri, T t)
        {
            try
            {
                await Task.Delay(200);
                var result = await PutAsync(uri, Helper.Content(t));
                var responseText = await result.Content.ReadAsStringAsync();
                var obj = Activator.CreateInstance<T>();
                if (result.IsSuccessStatusCode)
                {
                    obj = JsonConvert.DeserializeObject<T>(responseText);
                }
                else
                {
                    throw new SystemException(responseText);
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<AuthenticationToken> GenerateTokenAsync(string user, string password)
        {
            try
            {
                var str = string.Format("grant_type=password&username={0}&password={1}", user, password);
                var result = await PostAsync("Token", new StringContent(str, Encoding.UTF8));
                var content = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {

                    var Token = JsonConvert.DeserializeObject<AuthenticationToken>(content);
                    var main = await Helper.GetBaseApp();
                    main.Token = Token;
                    return Token;
                }
                else
                {
                    throw new System.Exception(content);
                }

            }
            catch (Exception ex)
            {
                var message = JsonConvert.DeserializeObject<ErrorMessage>(ex.Message);
                throw new SystemException(message.error_description);
            }

        }
    }

      
}
