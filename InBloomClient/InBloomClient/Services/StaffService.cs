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
using InBloomClient.Entities.Associations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InBloomClient.Services
{
    public class StaffService
    {
        private InBloomHttpClient _client;

        public StaffService(string token)
        {
            _client = new InBloomHttpClient(token);
        }

        public async Task<IEnumerable<Staff>> GetAll()
        {
            var response = await _client.GetDataString(Constants.Staff.ALL);
            var content = await response.Content.ReadAsStringAsync();
            var Staffs = JsonConvert.DeserializeObject<IEnumerable<Staff>>(content);
            return Staffs;
        }

        public async Task<Staff> GetById(string id)
        {
            var response = await _client.GetDataString(Constants.Staff.ALL + id);
            var content = await response.Content.ReadAsStringAsync();
            var Staff = JsonConvert.DeserializeObject<Staff>(content);
            return Staff;
        }

        public async Task<IEnumerable<StaffEducationOrganizationAssociation>> GetStaffEducationOrganizationAssociations(string id)
        {
            var strEndPoint = Constants.Staff.STAFF_EDUCATION_ORG_ASSIGNMENT_ASSOCIATIONS.Replace("{staffId}", id);
            var response = await _client.GetDataString(strEndPoint);
            var content = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<IEnumerable<StaffEducationOrganizationAssociation>>(content);
            return list;
        }

        public async Task<HttpResponseMessage> Create(Staff obj)
        {
            throw new NotImplementedException("Creating Staff is not allowed");
        }

        public async Task<HttpResponseMessage> Update(Staff obj)
        {
            throw new NotImplementedException("Updating Staff is not allowed");
        }

        public async Task<HttpResponseMessage> DeleteById(string id)
        {
            throw new NotImplementedException("Deleting Staff is not allowed");
        }
    }
}
