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
    public class AssessmentIdentificationCode
    {
        /// <summary>
        /// The organization code or name assigning the assessment identification code.
        /// </summary>
        [StringLength(60)]
        public string assigningOrganizationCode { get; set; }

        /// <summary>
        /// A unique number or alphanumeric code assigned to an assessment by a school, school system, a state, or other agency or entity.
        /// </summary>
        [StringLength(60)]
        public string ID { get; set; }

        /// <summary>
        /// A coding scheme that is used for identification and record-keeping purposes by schools, social services, or other agencies to refer to an assessment.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public AssessmentIdentificationSystemType identificationSystem { get; set; }
    }
}