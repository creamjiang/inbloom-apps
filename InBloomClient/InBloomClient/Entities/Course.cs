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
    public class Course
    {
        /// <summary>
        /// Indicates the career cluster or pathway the course is associated with as part of a CTE curriculum.
        /// values: Agriculture, Food and Natural Resources, Architecture and Construction, Arts, A/V Technology and Communications
        /// Business, Management and Administration, Education and Training, Finance, Government and Public Administration, Health Science
        /// Hospitality and Tourism, Human Services, Information Technology, Law, Public Safety, Corrections and Security
        /// Manufacturing, Marketing, Sales and Service, Science, Technology, Engineering and Mathematics, Transportation, Distribution and Logistics
        /// </summary>
        public string careerPathway { get; set; }

        /// <summary>
        /// The code that identifies the organization of subject matter and related learning experiences provided for the instruction of students.
        /// </summary>
        public List<CourseCode> courseCode { get; set; }

        /// <summary>
        /// Specifies whether the course was defined by the SEA, LEA, School, or national organization.
        /// values: LEA, National Organization, SEA, School
        /// </summary>
        public string courseDefinedBy { get; set; }

        /// <summary>
        /// A description of the content standards and goals covered in the course. Reference may be made to state or national content standards.
        /// </summary>
        [StringLength(1024)]
        public string courseDescription { get; set; }

        /// <summary>
        /// An indicator of whether or not this course being described is included in the computation of the student’s Grade Point Average, 
        /// and if so, if it weighted differently from regular courses.
        /// values: Applicable, Not Applicable, Weighted
        /// </summary>
        public string courseGPAApplicability { get; set; }

        /// <summary>
        /// The course's level of rigor.
        /// vaues: Basic or remedial, Enriched or advanced, General or regular, Honors
        /// </summary>
        public string courseLevel { get; set; }

        /// <summary>
        /// Specification of the specific program or designation associated with the course. For example: Ap, IB, Dual Credit, CTE. etc.
        /// values: Advanced, Advanced Placement, Core Subject, Correspondence, CTE, Distance Learning, Dual Credit, Graduation Credit
        /// Honors, IB Course, Magnet, Pre-AP, Pre-IB, Remedial, Students with disabilities, Basic, General, Gifted and Talented
        /// College level, English Language Learner, Untracked, Accepted as high school equivalent, Other
        /// </summary>
        public List<string> courseLevelCharacteristics { get; set; }


        /// <summary>
        /// The descriptive name given to a course of study offered in a school or other institution or organization. 
        /// In departmentalized classes at the elementary, secondary, and postsecondary levels (and for staff development activities), 
        /// this refers to the name by which a course is identified (e.g., American History, English III). 
        /// For elementary and other non-departmentalized classes, 
        /// it refers to any portion of the instruction for which a grade or report is assigned (e.g., reading, composition, spelling, and language arts).
        /// </summary>
        [StringLength(60)]
        public string courseTitle { get; set; }


        /// <summary>
        /// Date the course was adopted by the education agency.
        /// </summary>
        public DateTime dateCourseAdopted { get; set; }

        /// <summary>
        /// The grade levels in which the course is offered.
        /// values: Adult Education, Early Education, Eighth grade, Eleventh grade, Fifth grade, First grade, Fourth grade, Grade 13, Infant/toddler, Kindergarten
        /// Ninth grade, Other, Postsecondary, Preschool/Prekindergarten, Second grade, Seventh grade, Sixth grade, Tenth grade, Third grade, Transitional Kindergarten,
        /// Twelfth grade, Ungraded, Not Available
        /// </summary>
        public List<string> gradesOffered { get; set; }

        /// <summary>
        /// An indication that this course may satisfy high school graduation requirements in the course's subject area.
        /// </summary>
        public bool highSchoolCourseRequirement { get; set; }

        /// <summary>
        /// The maximum amount of credit available to a student who successfully completes the course
        /// </summary>
        public Credits maximumAvailableCredit { get; set; }

        /// <summary>
        /// The minimum amount of credit available to a student who successfully completes the course
        /// </summary>
        public Credits minimumAvailableCredit { get; set; }

        /// <summary>
        /// The number of parts identified for a course.
        /// </summary>
        public int numberOfParts { get; set; }

        /// <summary>
        /// The intended major subject area of the course.
        /// values: English Language and Literature, English, ELA, Reading, Mathematics, Life and Physical Sciences, Social Sciences and History, Social Studies
        /// Science, Fine and Performing Arts, Foreign Language and Literature, Religious Education and Theology, Physical, Health, and Safety Education, Military Science
        /// Computer and Information Sciences, Communication and Audio/Visual Technology, Composite, Critical Reading, Business and Marketing, Manufacturing, 
        /// Health Care Sciences, Public, Protective, and Government Service, Hospitality and Tourism, Architecture and Construction, Agriculture, Food, and Natural Resources, 
        /// Human Services, Transportation, Distribution and Logistics, Engineering and Technology, Writing, Miscellaneous, Other
        /// </summary>
        public string subjectArea { get; set; }

        /// <summary>
        /// The Education Organization that defines the curriculum and courses offered - often the LEA or school.
        /// </summary>
        public EducationOrganization school { get; set; }

        /// <summary>
        /// Course Identifier
        /// </summary>
        public string id { get; set; }
    }
}