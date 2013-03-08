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
    /// This entity represents one of many single measures that make up an assessment.
    /// </summary>
    public class AssessmentItem
    {
        /// <summary>
        /// The correct response for the assessment item.
        /// </summary>
        [StringLength(20)]
        public string correctResponse { get; set; }

        /// <summary>
        /// The correct response for the assessment item.
        /// </summary>
        [StringLength(60)]
        public string identificationCode { get; set; }

        /// <summary>
        /// Category or type of the assessment item. For example: Multiple choice Analytic Prose ....
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemCategoryType itemCategory { get; set; }

        /// <summary>
        /// Learning Standard tested by this item.
        /// </summary>
        public List<LearningStandardId> learningStandardId { get; set; }

        /// <summary>
        /// The maximum raw score achievable is all assessment items are correct and scored at the maximum.
        /// </summary>
        public int maxRawScore { get; set; }

        /// <summary>
        /// The correct response for the assessment item.
        /// </summary>
        [StringLength(35)]
        public string nomenclature { get; set; }
    }
}