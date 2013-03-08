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
    /// <summary>
    /// This entity represents a tool, instrument, process, or exhibition composed of a systematic sampling of behavior for measuring a student's competence, knowledge, 
    /// skills or behavior. An assessment can be used to measure differences in individuals or groups and changes in performance from one occasion to the next.
    /// </summary>
    public class Assessment
    {
        /// <summary>
        /// The content or subject area of an assessment, such as arts, mathematics, reading, stenography, or a foreign language.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public AcademicSubjectType academicSubject { get; set; }

        /// <summary>
        /// The category of an assessment based on format and content, such as "Achievement test," "Advanced placement test," 
        /// "Alternate assessment/grade-level standards," "Attitudinal test," and "Cognitive and perceptual skills test."
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public AssessmentCategoryType assessmentCategory { get; set; }

        /// <summary>
        /// A hierarchical name for the assessment using '.' separators.
        /// </summary>
        public string assessmentFamilyHierarchyName { get; set; }

        /// <summary>
        /// The form or format of assessment, such as a regular versus makeup form, or multiple choice versus constructed response.
        /// </summary>
        [StringLength(60)]
        public string assessmentForm { get; set; }

        /// <summary>
        /// A unique number or alphanumeric code assigned to an assessment by a school, school system, a state, or other agency.
        /// </summary>
        public List<AssessmentIdentificationCode> assessmentIdentificationCode { get; set; }

        /// <summary>
        /// Individual test items, if appropriate.
        /// </summary>
        public List<AssessmentItem> assessmentItem { get; set; }

        /// <summary>
        /// Definition of the performance levels and the associated cut scores. Three styles are supported: 
        /// (1) specification of performance level by min and max score, 
        /// (2) specification of performance level by cut score - min only, and 
        /// (3) specification of performance level without any mapping to scores.
        /// </summary>
        public List<AssessmentPerformanceLevel> assessmentPerformanceLevel { get; set; }

        /// <summary>
        /// The period or window in which an assessment is supposed to be administered. 
        /// Examples include "Beginning of Year," "Middle of Year," and "End of Year."
        /// </summary>
        public AssessmentPeriodDescriptor assessmentPeriodDescriptor { get; set; }

        /// <summary>
        /// The title or name of the assessment.
        /// </summary>
        [StringLength(60)]
        public string assessmentTitle { get; set; }

        /// <summary>
        /// Identifier for whether an assessment conforms to a standard. Examples include "Local standard," 
        /// "Statewide standard," "Regional standard," "Association standard," and "School standard."
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ContentStandardType contentStandard { get; set; }

        /// <summary>
        /// The grade level this assessment is most typically associated with. If it spans multiple grade levels, 
        /// this should be the highest grade level for the assessment, and the lowest grade level is provided in the "lowestGradeLevelAsssessed" attribute. 
        /// Examples include "Adult," "Prekindergarten," "First Grade," "Second Grade," and so on.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public GradeLevelType gradeLevelAssessed { get; set; }

        /// <summary>
        /// The lowest grade assessed when the assessments spans a range of grade levels (see "gradeLevelAssessed"), 
        /// otherwise omitted. Examples include "Adult," "Prekindergarten," "First Grade," "Second Grade," and so on.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public GradeLevelType lowestGradeLevelAssessed { get; set; }

        /// <summary>
        /// The maximum raw score achievable across all assessment items that are correct and scored at the maximum.
        /// </summary>
        public int maxRawScore { get; set; }

        /// <summary>
        /// The minimum raw score achievable across all assessment items that are correct and scored at the minimum.
        /// </summary>
        public int minRawScore { get; set; }

        /// <summary>
        /// The specific nomenclature used for assessment.
        /// </summary>
        public string nomenclature { get; set; }

        /// <summary>
        /// Subsets of the assessment that address specific learning objectives.
        /// </summary>
        public List<ObjectiveAssessment> objectiveAssessment { get; set; }

        /// <summary>
        /// The month, day, and year that the conceptual design for the assessment had its most recent substantial revision.
        /// </summary>
        public DateTime revisionDate { get; set; }

        /// <summary>
        /// The version identifier for the assessment.
        /// </summary>
        public int version { get; set; }
    }
}