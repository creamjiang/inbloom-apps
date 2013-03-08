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
    /// This descriptor defines various levels or thresholds for performance on the assessment.
    /// </summary>
    public class PerformanceLevelDescriptor
    {
        /// <summary>
        /// A code or abbreviation that is used to refer to a Performance Level,
        /// </summary>
        [StringLength(20)]
        public string codeValue { get; set; }

        /// <summary>
        /// A code or abbreviation that is used to refer to a Performance Level,
        /// </summary>
        [StringLength(1024)]
        public string description { get; set; }

        /// <summary>
        /// A conversion of the level to a standard set of performance levels.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PerformanceBaseType performanceBaseConversion { get; set; }
    }
}