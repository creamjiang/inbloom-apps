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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBloomClient.Entities
{
    public class CourseOffering
    {
        /// <summary>
        /// Course offering identifier
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// The local code assigned by the School that identifies the course offering provided for the instruction of students.
        /// </summary>
        [StringLength(30)]
        public string localCourseCode { get; set; }

        /// <summary>
        /// The descriptive name given to a course of study offered in the school, if different from the CourseTitle.
        /// </summary>
        [StringLength(60)]
        public string localCourseTitle { get; set; }

        /// <summary>
        /// Reference to the session the course is offered.
        /// </summary>
        public string courseId { get; set; }

        /// <summary>
        /// Reference to the session the course is offered.
        /// </summary>
        public string sessionId { get; set; }
    }
}
