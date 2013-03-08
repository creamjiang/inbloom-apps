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
using System.ComponentModel.DataAnnotations;
using InBloomClient.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InBloomClient.Entities
{
    public class Cohort
    {
        public string id { get; set; }

        [Required(ErrorMessage = "educationOrgId is required")]
        public string educationOrgId { get; set; }

        /// <summary>
        /// The academic subject associated with an academic intervention.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public AcademicSubjectType academicSubject { get; set; }

        /// <summary>
        /// The description of he cohort and its purpose.
        /// </summary>
        [StringLength(1024)]
        public string cohortDescription { get; set; }

        /// <summary>
        /// The name or ID for the cohort.
        /// </summary>
        [StringLength(20)]
        [Required(ErrorMessage = "Cohort name is required")]
        public string cohortIdentifier { get; set; }

        //District, School, Classroom, Teacher, Principal, Counselor, Statewide
        [JsonConverter(typeof(StringEnumConverter))]
        public CohortScopeType cohortScope { get; set; }

        //Academic Intervention, Attendance Intervention, Discipline Intervention, Classroom Pullout, Extracurricular Activity, Field Trip, 
        //Principal Watch List, Counselor List, In-school Suspension, Study Hall, Other
        /// <summary>
        /// The type of cohort (e.g., academic intervention, classroom breakout)
        /// </summary>
        [Required(ErrorMessage = "Cohort type is required")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CohortType cohortType { get; set; }

        ///// <summary>
        ///// The education organization associated with and owner of the cohort.
        ///// </summary>
        //public EducationOrganization educationOrg { get; set; }

        ///// <summary>
        ///// The (optional) program associated with this cohort (e.g., special education)
        ///// </summary>
        //public List<Program> programs { get; set; }
    }
}