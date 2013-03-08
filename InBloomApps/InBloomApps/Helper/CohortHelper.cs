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
 * 
 */
using Newtonsoft.Json;
using InBloomClient.Entities;
using InBloomClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using InBloomApps.Models;

namespace InBloomApps.Helper
{
    public class CohortHelper
    {
        /// <summary>
        /// Get all information about a cohort
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="cohort"></param>
        /// <returns></returns>
        public static async Task<CohortDisplayObject> GetCohortDisplayObject(CohortService cs, Cohort cohort)
        {
            //check to see if the item is already in cache. if so, return the cache item
            var cache = (CohortDisplayObject)HttpContext.Current.Cache[cohort.id];
            if (cache != null)
                return cache;

            var students = GetStudentsByCohortId(cs, cohort.id);
            var custom = GetCohortCustomByCohortId(cs, cohort.id);

            await Task.WhenAll(students, custom);

            var displayObject = new CohortDisplayObject();
            displayObject.cohort = cohort;
            displayObject.students = from s in students.Result select s.id;
            displayObject.custom = JsonConvert.DeserializeObject<CohortCustom>(custom.Result); ;

            HttpContext.Current.Cache.Insert(cohort.id, displayObject);
            return displayObject;
        }

        public static async Task<Cohort> GetCohortById(string accessToken, string id)
        {
            var cs = new CohortService(accessToken.ToString());
            var result = await cs.GetById(id);
            return result;
        }

        public static Cohort GetGroupById(string accessToken, string id)
        {
            var cs = new CohortService(accessToken.ToString());
            var result = cs.GetById(id);
            return result.Result;
        }

        #region private
        private static async Task<IEnumerable<Student>> GetStudentsByCohortId(CohortService cs, string cohortId)
        {
            var result = await cs.GetStudentsByCohortId(cohortId);
            return result;
        }

        /// <summary>
        /// Get cohort custom
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="cohortId">the cohort id to get custom for</param>
        /// <returns></returns>
        private static async Task<string> GetCohortCustomByCohortId(CohortService cs, string cohortId)
        {
            try
            {
                var result = await cs.GetCustomById(cohortId);
                return result;
            }
            catch (Exception ex)
            {
                //when there isn't a custom yet, inBloom throw a 404
                return "";
            }

        }
        #endregion
    }
}