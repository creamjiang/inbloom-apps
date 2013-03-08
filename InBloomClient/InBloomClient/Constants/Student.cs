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

namespace InBloomClient.Constants
{
    public class Student
    {
        public const string ALL = "students/";
        public const string STUDENT_ASSESSMENT_ASSOCIATION_ASSESSMENTS = "students/{studentId}/studentAssessments/assessments";
        public const string STUDENT_SECTION_ASSOCIATION_SECTIONS = "students/{studentId}/studentSectionAssociations/sections";
        public const string STUDENT_ACADEMIC_RECORDS_ALL = "studentAcademicRecords";
    }
}