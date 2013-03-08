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
    public class Cohort
    {
        public const string ALL = "cohorts/";
        public const string STUDENT_COHORT_ASSOCIATION = "studentCohortAssociations/";
        public const string GET_STUDENT_COHORT_ASSOCIATION = "cohorts/{cohortId}/studentCohortAssociations/";
        public const string STAFF_COHORT_ASSOCIATION = "staffCohortAssociations/";
        public const string STUDENT_COHORT_ASSOCIATION_STUDENTS = "cohorts/{cohortId}/studentCohortAssociations/students";
        public const string COHORT_CUSTOM = "cohorts/{cohortId}/custom";
    }
}