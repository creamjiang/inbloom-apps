/*
 * Copyright 2012-2013 inBloom, Inc. and its affiliates.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Configuration;

namespace InBloomClient
{
    /// <summary>
    /// HttpClient to interact with the INBLOOM API
    /// </summary>
    class InBloomHttpClient
    {
        private HttpClient _httpClient;
        private string _token;
        private const string INBLOOM_API_SANDBOX_URL = "InBloomApiSandboxUrl";

        /// <summary>
        /// Get the token
        /// </summary>
        public string token { get { return _token; } }

        public InBloomHttpClient(string token, string apiEndPoint = "")
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.BaseAddress = new Uri(apiEndPoint == "" ? Properties.Settings.Default.InBloomApiSandboxUrl : apiEndPoint);
            _token = token;
        }

        public async Task<HttpResponseMessage> GetDataString(string endPoint)
        {
            try
            {               
                var response = _httpClient.GetAsync(endPoint);
                //response.EnsureSuccessStatusCode(); //throw exception if status is non-successful

                //var responseBodyAsText = await response.Content.ReadAsStringAsync();
                return response.Result;
            }
            catch (Exception ex)
            {
                //log exception here
                throw;
            }
        }

        public async Task<HttpResponseMessage> PostData(string endPoint, object param)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(endPoint, param);
                //response.EnsureSuccessStatusCode();

                //var strResponse = await response.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> PutData(string endPoint, object param)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(endPoint, param);
                //response.EnsureSuccessStatusCode();

                //var strResponse = await response.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> DeleteData(string endPoint, string id)
        {
            try
            {
                var strEndPoint = string.Format("{0}{1}", endPoint, id);
                var response = await _httpClient.DeleteAsync(strEndPoint);
                //response.EnsureSuccessStatusCode();

                //var strResponse = await response.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
