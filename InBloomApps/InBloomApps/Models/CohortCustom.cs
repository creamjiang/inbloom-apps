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

namespace InBloomApps.Models
{
    /// <summary>
    /// custom data to store in the inBloom custom entity
    /// </summary>
    public class CohortCustom
    {
        /// <summary>
        /// The ID of the Cohort this custom is associated to
        /// </summary>
        public string cohortId { get; set; }

        /// <summary>
        /// The list of data elements to display for students
        /// </summary>
        public IEnumerable<DataElement> dataElements { get; set; }

        /// <summary>
        /// Lesson plan object
        /// </summary>
        public LessonPlan lessonPlan { get; set; }

        /// <summary>
        /// The last modified date time of the cohort in context
        /// </summary>
        public DateTime lastModifiedDate { get; set; }
    }
}