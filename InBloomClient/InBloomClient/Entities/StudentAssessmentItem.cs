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
using System.Linq;
using System.Web;

namespace InBloomClient.Entities
{
    public class StudentAssessmentItem
    {
        /// <summary>
        /// The AssessmentItem to which this studentAssessmentItem is a response to.
        /// </summary>
        public AssessmentItem assessmentItem { get; set; }

        /// <summary>
        /// The analyzed result of a student's response to an assessment item.. For example: Correct Incorrect Met standard ...
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public AssessmentItemResultType assessmentItemResult { get; set; }

        /// <summary>
        /// A student's response to a stimulus on a test.
        /// </summary>
        public string assessmentResponse { get; set; }

        /// <summary>
        /// A meaningful raw score of the performance of an individual on an assessment item.
        /// </summary>
        public int rawScoreResult { get; set; }

        /// <summary>
        /// Indicator of the response. For example: Nonscorable response Ineffective response Effective response Partial response ...
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ResponseIndicatorType responseIndicator { get; set; }
    }
}