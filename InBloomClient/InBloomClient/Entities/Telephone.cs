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
    public class Telephone
    {
        /// <summary>
        /// An indication that the telephone number should be used as the principal number for an individual or organization.
        /// </summary>
        public bool primaryTelephoneNumberIndicator { get; set; }

        /// <summary>
        /// The telephone number including the area code, and extension, if applicable.
        /// </summary>
        [StringLength(24)]
        public string telephoneNumber { get; set; }

        /// <summary>
        /// The type of communication number listed for an individual or organization.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public TelephoneNumberType telephoneNumberType { get; set; }
    }
}