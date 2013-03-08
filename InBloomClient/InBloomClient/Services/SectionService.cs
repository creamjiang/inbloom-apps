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
using System.Text;
using System.Threading.Tasks;

namespace InBloomClient.Services
{
    public class SectionService : IInBloomService<Section>
    {
        private InBloomHttpClient _client;

        public SectionService(string token)
        {
            _client = new InBloomHttpClient(token);
        }

        public async Task<IEnumerable<Section>> GetAll()
        {
            var response = await _client.GetDataString(Constants.Section.ALL);
            var content = await response.Content.ReadAsStringAsync();
            var sections = JsonConvert.DeserializeObject<IEnumerable<Section>>(content);
            return sections;
        }

        public async Task<Section> GetById(string id)
        {
            var response = await _client.GetDataString(Constants.Section.ALL + id);
            var content = await response.Content.ReadAsStringAsync();
            var section = JsonConvert.DeserializeObject<Section>(content);
            return section;
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            var response = await _client.GetDataString(Constants.Section.ALL_COURSES);
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<IEnumerable<Course>>(content);
            return data;
        }

        public async Task<IEnumerable<CourseOffering>> GetAllCourseOfferings()
        {
            var response = await _client.GetDataString(Constants.Section.ALL_COURSE_OFFERINGS);
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<IEnumerable<CourseOffering>>(content);
            return data;
        }

        public async Task<HttpResponseMessage> Create(Section obj)
        {
            throw new NotImplementedException("Creating section is not allowed");
        }

        public async Task<HttpResponseMessage> Update(Section obj)
        {
            throw new NotImplementedException("Updating section is not allowed");
        }

        public async Task<HttpResponseMessage> DeleteById(string id)
        {
            throw new NotImplementedException("Deleting section is not allowed");
        }
    }
}
