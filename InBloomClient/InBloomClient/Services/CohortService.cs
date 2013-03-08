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
using InBloomClient.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InBloomClient.Services
{
    public class CohortService : IInBloomService<Cohort>
    {
        private InBloomHttpClient _client;

        public CohortService(string token)
        {
            _client = new InBloomHttpClient(token);
        }

        /// <summary>
        /// Get all cohorts that the current user has access to
        /// </summary>
        /// <returns>all cohorts the current user has access to</returns>
        public async Task<IEnumerable<Cohort>> GetAll()
        {
            try
            {
                var response = await _client.GetDataString(Constants.Cohort.ALL);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var cohorts = JsonConvert.DeserializeObject<IEnumerable<Cohort>>(content);
                    return cohorts; //successful request, return list of cohorts
                }
                return new List<Cohort>(); //problem with getting cohorts back, return empty list
            }
            catch (Exception e)
            {
                //log here
                throw;
            }           
        }

        /// <summary>
        /// Get a cohort by Id
        /// </summary>
        /// <param name="id">Id of the cohort to get</param>
        /// <returns>the requested cohort</returns>
        public async Task<Cohort> GetById(string id)
        {
            var response = await _client.GetDataString(Constants.Cohort.ALL + id);
            var content = await response.Content.ReadAsStringAsync();
            var cohort = JsonConvert.DeserializeObject<Cohort>(content);
            return cohort;
        }

        /// <summary>
        /// Create a cohort
        /// </summary>
        /// <param name="obj">the cohort to create</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Create(Cohort obj)
        {
            var response = await _client.PostData(Constants.Cohort.ALL, obj);
            //var content = await response.Content.ReadAsStringAsync();
            return response;
        }

        /// <summary>
        /// Update a cohort by ID
        /// </summary>
        /// <param name="obj">the cohort to update</param>
        /// <returns>request response</returns>
        public async Task<HttpResponseMessage> Update(Cohort obj)
        {
            var response = await _client.PutData(Constants.Cohort.ALL + obj.id, obj);
            return response;
        }

        /// <summary>
        /// Delete a cohort by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>request response</returns>
        public async Task<HttpResponseMessage> DeleteById(string id)
        {
            var response = await _client.DeleteData(Constants.Cohort.ALL, id);
            return response;
        }

        /// <summary>
        /// Get the students that belong to the cohort in context
        /// </summary>
        /// <param name="id">cohortId</param>
        /// <returns>list of students in cohort with id = cohortId</returns>
        public async Task<IEnumerable<Student>> GetStudentsByCohortId(string id)
        {
            var strEndPoint = Constants.Cohort.STUDENT_COHORT_ASSOCIATION_STUDENTS.Replace("{cohortId}", id);
            var response = await _client.GetDataString(strEndPoint);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var students = JsonConvert.DeserializeObject<IEnumerable<Student>>(content);
                return students;
            }
            return new List<Student>();
        }

        /// <summary>
        /// Get the student cohort association
        /// </summary>
        /// <param name="id">cohortId</param>
        /// <returns>list of studentCohortAssociations in a cohort with id = cohortId</returns>
        public async Task<IEnumerable<StudentCohortAssociation>> GetStudentCohortAssociationsByCohortId(string id)
        {
            var strEndPoint = Constants.Cohort.GET_STUDENT_COHORT_ASSOCIATION.Replace("{cohortId}", id);
            var response = await _client.GetDataString(strEndPoint);
            var content = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<IEnumerable<StudentCohortAssociation>>(content);
            return list;
        }

        /// <summary>
        /// Associate a student with a cohort
        /// </summary>
        /// <param name="obj">StudentCohortAssociation requires: cohortId, studentId, beginDate</param>
        /// <returns>request response</returns>
        public async Task<HttpResponseMessage> CreateStudentCohortAssociation(StudentCohortAssociation obj)
        {
            var response = await _client.PostData(Constants.Cohort.STUDENT_COHORT_ASSOCIATION, obj);
            return response;
        }

        /// <summary>
        /// Remove a student from a cohort
        /// </summary>
        /// <param name="id">the association ID</param>
        /// <returns>request response</returns>
        public async Task<HttpResponseMessage> DeleteStudentCohortAssociationById(string id)
        {
            var response = await _client.DeleteData(Constants.Cohort.STUDENT_COHORT_ASSOCIATION, id);
            return response;
        }

        /// <summary>
        /// Associate a staff with a cohort
        /// </summary>
        /// <param name="obj">StaffCohortAssociation requires: cohortId, staffId, beginDate</param>
        /// <returns>request response</returns>
        public async Task<HttpResponseMessage> CreateStaffCohortAssociation(StaffCohortAssociation obj)
        {
            var response = await _client.PostData(Constants.Cohort.STAFF_COHORT_ASSOCIATION, obj);           
            return response;
        }

        /// <summary>
        /// Remove a staff from a cohort
        /// </summary>
        /// <param name="id">the association ID</param>
        /// <returns>request response</returns>
        public async Task<HttpResponseMessage> DeleteStaffCohortAssociationById(string id)
        {
            var result = await _client.DeleteData(Constants.Cohort.STUDENT_COHORT_ASSOCIATION, id);
            return result;
        }

        /// <summary>
        /// Get a cohort custom entity
        /// </summary>
        /// <param name="id">Id of the cohort to get</param>
        /// <returns>everything in the custom entity as string format</returns>
        public async Task<string> GetCustomById(string id)
        {
            try
            {
                var strEndPoint = Constants.Cohort.COHORT_CUSTOM.Replace("{cohortId}", id);
                var response = await _client.GetDataString(strEndPoint);
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return ""; //custom doesn't exist yet
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception e)
            {
                //inBloom throws a 404 if custom doesn't exist yet
                return "";
            }            
        }

        /// <summary>
        /// Create a cohort custom
        /// </summary>
        /// <param name="obj">the cohort to create</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> CreateCohortCustom(string id, string param)
        {
            var response = await SubmitCohortCustomData(id, param, HttpMethod.Post);
            return response;
        }

        /// <summary>
        /// Update the cohort custom entity
        /// </summary>
        /// <param name="obj">the cohort to update</param>
        /// <returns>request response</returns>
        public async Task<HttpResponseMessage> UpdateCohortCustom(string id, string param)
         {
            try
            {
                var response = await SubmitCohortCustomData(id, param, HttpMethod.Put);
                return response;
            }
            catch (Exception e)
            {
                //inBloom throws a 404 if custom doesn't exist yet
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }            
        }

        private async Task<HttpResponseMessage> SubmitCohortCustomData(string id, string param, HttpMethod method)
        {
            //using InBloomHttpClient throw an Internal Server Error from inBloom so we're constructing a new HttpClient 
            //for a clean HttpRequestMessage for custom only
            var _httpClient = new HttpClient();
            var strEndPoint = Constants.Cohort.COHORT_CUSTOM.Replace("{cohortId}", id);
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(Properties.Settings.Default.InBloomApiSandboxUrl + strEndPoint),
                Method = method,
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _client.token);

            request.Content = new System.Net.Http.StringContent(param, Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            return response;
        }
    }
}
