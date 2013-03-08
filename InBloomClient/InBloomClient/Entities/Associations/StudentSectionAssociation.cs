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

using InBloomClient.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InBloomClient.Entities
{
    public class StudentSectionAssociation
    {
        public string id { get; set; }

        /// <summary>
        /// The section the student is enrolled in.
        /// </summary>
        public string sectionId { get; set; }

        public string cohortId { get; set; }

        /// <summary>
        /// Month, day and year of the student's entry or assignment to the section. If blank, default is the start date of the first grading period.
        /// </summary>
        public DateTime beginDate { get; set; }

        /// <summary>
        /// Month, day, and year of the withdrawal or exit of the student from the section.
        /// </summary>
        public DateTime endDate { get; set; }

        /// <summary>
        /// Indicates the section is the student's homeroom. Homeroom period may the convention for taking daily attendance.
        /// </summary>
        public bool homeroomIndicator { get; set; }

        /// <summary>
        /// An indication as to whether a student has previously taken a given course. Repeated, counted in grade point average Repeated, 
        /// not counted in grade point average Not repeated Other
        /// </summary>
        public RepeatIdentifierType repeatIdentifier { get; set; }

        ///// <summary>
        ///// The section the student is enrolled in.
        ///// </summary>
        //public Section section { get; set; }

        ///// <summary>
        ///// The student enrolled in the section.
        ///// </summary>
        //public Student student { get; set; }
    }
}