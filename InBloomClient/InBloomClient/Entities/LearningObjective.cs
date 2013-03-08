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
    /// This entity represents identified learning objectives for courses in specific grades.
    /// </summary>
    public class LearningObjective
    {
        /// <summary>
        /// The description of the content or subject area (e.g., arts, mathematics, reading, stenography, or a foreign language) of an assessment.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public AcademicSubjectType academicSubject { get; set; }

        /// <summary>
        /// The description of the learning objective.
        /// </summary>
        [StringLength(1024)]
        public string description { get; set; }

        /// <summary>
        /// The Identifier for the specific learning objective in the context of a standard (e.g., 111.15.3.1.A)
        /// </summary>
        public LearningStandardId learningObjectiveId { get; set; }

        /// <summary>
        /// The designated title of the learning objective.
        /// </summary>
        [StringLength(60)]
        public string objective { get; set; }

        /// <summary>
        /// The grade level for which the learning objective is targeted,
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public GradeLevelType objectiveGradeLevel { get; set; }

        /// <summary>
        /// Learning standard(s) included in this objective.
        /// </summary>
        public List<LearningStandard> learningStandards { get; set; }

        /// <summary>
        /// Parent learning objective this is a sub-objective for.
        /// </summary>
        public LearningObjective parentLearningObjective { get; set; }
    }
}