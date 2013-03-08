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
using System.Web;

namespace InBloomClient.Entities
{
    public class CourseCode
    {
        /// <summary>
        /// The organization code or name assigning the staff Identification Code.
        /// </summary>
        [StringLength(60)]
        public string assigningOrganizationCode { get; set; }

        /// <summary>
        /// A unique number or alphanumeric code assigned to a course by a school, school system, a state, or other agency or entity. 
        /// For multi-part course codes, concatenate the parts separated by a "/". 
        /// For example, consider the following SCED code: subject = 20 Math course = 272 Geometry level = G General credits = 1.00 course sequence 1 of 1 
        /// would be entered as 20/272/G/1.00/1 of 1
        /// </summary>
        [StringLength(60)]
        public string ID { get; set; }

        /// <summary>
        /// A system that is used to identify the organization of subject matter and related learning experiences provided for the instruction of students.
        /// values: CSSC course code, Intermediate agency course code, LEA course code, NCES Pilot SNCCS course code, Other, SCED course code, School course code
        /// State course code, University course code
        /// </summary>
        public string identificationSystem { get; set; }
    }
}