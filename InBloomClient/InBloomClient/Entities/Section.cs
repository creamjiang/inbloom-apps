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
using Newtonsoft.Json.Converters;
using InBloomClient.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InBloomClient.Entities
{
    public class Section
    {
        public string id {get; set;}

        /// <summary>
        /// The amount of credit available to a student who successfully meets the objectives of the course. 
        /// Available credits are measured in Carnegie units, A course meeting every day for one period of the school day over the span of a school year offers one Carnegie unit. 
        /// See publication: U.S. Department of Education, NCES, 2007-341, Secondary School Course Classification System: School Codes for the Exchange of Data (SCED).
        /// </summary>
        public Credits availableCredit { get; set; }

        /// <summary>
        /// The setting in which a child receives education and related services; 
        /// for example: Center-based instruction Home-based instruction Hospital class Mainstream Residential care and treatment facility ....
        /// values: Classroom, Homebound, Hospital class, In-school suspension, Laboratory, Mainstream (Special Education), Off-school center, Pull-out class,
        /// Resource room, Self-contained (Special Education), Self-study, Shop
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EducationalEnvironmentType educationalEnvironment { get; set; }

        /// <summary>
        /// The media through which teachers provide instruction to students and students and teachers communicate about instructional matters; 
        /// for example: Technology-based instruction in classroom Correspondence instruction Face-to-face instruction Virtual/On-line Distance learning Center-based instruction ...
        /// values: Televised, Telepresence/video conference, Videotaped/prerecorded video, Other technology-based instruction, Technology-based instruction in classroom
        /// Correspondence instruction, Face-to-face instruction, Virtual/On-line Distance learning, Center-based instruction, Independent study, Internship, Other
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public MediumOfInstructionType mediumOfInstruction { get; set; }

        /// <summary>
        /// The population for which the course was designed; 
        /// for example: Bilingual students Remedial education students Gifted and talented students Career and Technical Education students Special education students ....
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PopulationServedType populationServed { get; set; }

        /// <summary>
        /// When a section is part of a sequence of parts for a course, the number if the sequence. If the course has only one part, the value of this section attribute should be 1.
        /// </summary>
        public int sequenceOfCourse { get; set; }

        /// <summary>
        /// A unique identifier for the section that is defined by the classroom, the subjects taught, and the instructors that are assigned.
        /// </summary>
        [StringLength(30)]
        public string uniqueSectionCode { get; set; }

        /// <summary>
        /// The course associated with the section. This reference is required.
        /// </summary>
        public Course course { get; set; }

        /// <summary>
        /// Optional reference to program (e.g., CTE, etc.) the section is associated with.
        /// </summary>
        public List<Program> programs { get; set; }

        /// <summary>
        /// The school where the section is taught.
        /// </summary>
        public EducationOrganization school { get; set; }

        /// <summary>
        /// The session associated with the section. This reference is required.
        /// </summary>
        public Session session { get; set; }

        public string courseOfferingId { get; set; }

        public string sessionId { get; set; }
    }
}