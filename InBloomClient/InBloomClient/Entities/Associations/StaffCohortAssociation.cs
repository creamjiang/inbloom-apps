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
using System.Web;

namespace InBloomClient.Entities
{
    /// <summary>
    /// This association indicates the staff associated with a cohort of students.
    /// </summary>
    public class StaffCohortAssociation
    {
        public string id { get; set; }

        /// <summary>
        /// The staff associated with the cohort of students.
        /// </summary>
        public string staffId { get; set; }

        /// <summary>
        /// Reference to the cohort associated with the staff.
        /// </summary>
        public string cohortId { get; set; }

        /// <summary>
        /// Start date for the association of staff to this cohort.
        /// </summary>
        public DateTime beginDate { get; set; }

        /// <summary>
        /// End date for the association of staff to this cohort.
        /// </summary>
        public DateTime endDate { get; set; }

        /// <summary>
        /// Indicator of whether the staff has access to the student records of the cohort per district interpretation of FERPA and other privacy laws, regulations and policies.
        /// </summary>
        public bool studentRecordAccess { get; set; }

        /// <summary>
        /// Only serialize id if it's not null
        /// </summary>
        /// <returns>Whether id should be serialized</returns>
        public bool ShouldSerializeid()
        {
            return (id != null);
        }

        /// <summary>
        /// Only serialize endDate if it's not null
        /// </summary>
        /// <returns>Whether endDate should be serialized</returns>
        public bool ShouldSerializeendDate()
        {
            return (endDate != null);
        }
        ///// <summary>
        ///// Reference to the cohort associated with the staff.
        ///// </summary>
        //public List<Cohort> cohorts { get; set; }

        ///// <summary>
        ///// The staff associated with the cohort of students.
        ///// </summary>
        //public List<Staff> staffs { get; set; }
    }
}