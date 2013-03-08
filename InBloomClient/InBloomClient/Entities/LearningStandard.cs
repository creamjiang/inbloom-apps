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
    /// This entity is a subelement of a learning objective consisting of a precise statement of the expectation of a student's proficiency.
    /// </summary>
    public class LearningStandard
    {
        /// <summary>
        /// A reference to the type of content standard e.g., state, national, etc.)
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ContentStandardType contentStandard { get; set; }

        /// <summary>
        /// The official Course Title with which this learning standard is associated.
        /// </summary>
        [StringLength(60)]
        public string courseTitle { get; set; }

        /// <summary>
        /// A detailed statement of the expectation that is is assessed.
        /// </summary>
        [StringLength(1024)]
        public string description { get; set; }

        /// <summary>
        /// The grade level for the specific learning standard.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public GradeLevelType gradeLevel { get; set; }

        /// <summary>
        /// The Identifier for the specific learning standard (e.g., 111.15.3.1.A)
        /// </summary>
        public LearningStandardId learningStandardId { get; set; }

        /// <summary>
        /// Subject area for the learning standard.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public AcademicSubjectType subjectArea { get; set; }
    }
}