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

namespace InBloomClient.Entities
{
    public class StudentCohortAssociation
    {
        public string id { get; set; }
        [Required]
        public string studentId { get; set; }
        [Required]
        public string cohortId { get; set; }

        /// <summary>
        /// The month, day, and year on which the student was first identified as part of the cohort.
        /// </summary>
        [Required]
        public DateTime beginDate { get; set; }

        /// <summary>
        /// The month, day, and year on which the student was removed as part of the cohort.
        /// </summary>
        public DateTime? endDate { get; set; }

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
    }
}