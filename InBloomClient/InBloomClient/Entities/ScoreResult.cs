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
    /// A meaningful raw score or statistical expression of the performance of an individual. 
    /// The results can be expressed as a number, percentile, range, level, etc.
    /// </summary>
    public class ScoreResult
    {
        /// <summary>
        /// The method that the administrator of the assessment uses to report the performance and achievement of all students. 
        /// It may be a qualitative method such as performance level descriptors or a quantitative method such as a numerical grade or cut score. 
        /// More than one type of reporting method may be used.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public AssessmentReportingMethodType assessmentReportingMethod { get; set; }

        /// <summary>
        /// The value of a meaningful raw score or statistical expression of the performance of an individual. 
        /// The results can be expressed as a number, percentile, range, level, etc.
        /// </summary>
        [StringLength(35)]
        public string result { get; set; }
    }
}