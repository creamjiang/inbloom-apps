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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using InBloomApps.Models;
using System.Net;
using System.Net.Http;

namespace InBloomApps.Helper
{
    /// <summary>
    /// Contruct data elements to display
    /// </summary>
    public class GlobalHelper
    {
        /// <summary>
        /// Get data elements from file
        /// </summary>
        /// <returns>List of data elements in file in ~/Data/DataElements.txt</returns>
        public static async Task<IEnumerable<DataElement>> InitializeDataElements()
        {
            try
            {
                    const string path = "\\Data\\DataElements.txt";
                    string s = await GetJsonFromFile(path);
                    var list = JsonConvert.DeserializeObject<IEnumerable<DataElement>>(s);
                    return list;
            }
            catch(Exception e)
            {
                ExceptionHelper.LogCaughtException(e);
                return new List<DataElement>();
            }
        }

        /// <summary>
        /// Get list of color from file
        /// </summary>
        /// <returns>List of colors in file in ~/Data/Colors.txt</returns>
        public static async Task<IEnumerable<Color>> InitializeColors()
        {
            try
            {
                const string path = "\\Data\\Colors.txt";
                string s = await GetJsonFromFile(path);
                var list = JsonConvert.DeserializeObject<IEnumerable<Color>>(s);
                return list;

            }
            catch (Exception e)
            {
                ExceptionHelper.LogCaughtException(e);
                return new List<Color>();
            }
        }

        /// <summary>
        /// Return data from a file
        /// </summary>
        /// <param name="path">path of the file</param>
        /// <returns>json data in the file</returns>
        public static async Task<string> GetJsonFromFile(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + path))
                {
                    string s = await sr.ReadToEndAsync();
                    return s;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Get a Result object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Result GetExceptionResult(string id, Exception e)
        {
            ExceptionHelper.LogCaughtException(e);
            return new Result
            {
                completedSuccessfully = false,
                objectActionResult =
                    new ActionResponseResult
                    {
                        objectId = id,
                        status = HttpStatusCode.InternalServerError,
                        message = "Message: " + e.Message + " Inner Exception: " + (e.InnerException == null ? "" : e.InnerException.Message),
                        isSuccess = false
                    }
            };
        }

        /// <summary>
        /// Create a new ActionResponseResult object base on the HttpResponseMessage parameter
        /// </summary>
        /// <param name="objId">The related object id</param>
        /// <param name="objName">The related object name</param>
        /// <param name="successStatus">The HttpStatusCode that indicates a successful response</param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static ActionResponseResult GetActionResponseResult(string objId, string objName, HttpResponseMessage m, HttpStatusCode successStatus)
        {
            return new ActionResponseResult
            {
                objectId = objId,
                objectName = objName,
                status = m.StatusCode,
                message = m.Content == null ? "" : m.Content.ReadAsStringAsync().Result,
                isSuccess = m.StatusCode == successStatus
            };
        }

        /// <summary>
        /// Get a Result object with an expired status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Result GetSessionExpiredResult(string id = "")
        {
            return new Result
            {
                completedSuccessfully = false,
                objectActionResult =
                    new ActionResponseResult
                    {
                        objectId = id,
                        status = HttpStatusCode.ProxyAuthenticationRequired,
                        message = "Session expired",
                        isSuccess = false
                    }
            };
        }
    }
}