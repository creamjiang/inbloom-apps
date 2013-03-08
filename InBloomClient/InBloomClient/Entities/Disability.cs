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
using System.ComponentModel.DataAnnotations;
using InBloomClient.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InBloomClient.Entities
{
    public class Disability
    {
        /// <summary>
        /// A disability category that describes a child's impairment.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public DisabilityType disability { get; set; }

        /// <summary>
        /// A description of the disability diagnosis.
        /// </summary>
        [Range(1, 80)]
        [StringLength(80)]
        public string disabilityDiagnosis { get; set; }

        /// <summary>
        /// The order by severity of student's disabilities: 1- Primary, 2 - Secondary, 3 - Tertiary, etc.
        /// </summary>
        public int orderOfDisability { get; set; }
    }
}