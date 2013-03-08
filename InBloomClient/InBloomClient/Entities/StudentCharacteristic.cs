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
    /// <summary>
    /// Reflects important characteristics of the student's home situation: Displaced Homemaker, Homeless, Immigrant, 
    /// Migratory, Military Parent, Pregnant Teen, Single Parent, Unaccompanied Youth
    /// </summary>
    public class StudentCharacteristic
    {
        /// <summary>
        /// The date the characteristic was designated.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime beginDate { get; set; }

        /// <summary>
        /// The characteristic designated for the student.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public StudentCharacteristicType characteristic { get; set; }

        /// <summary>
        /// The person, organization, or department that designated the characteristic.
        /// </summary>
        [StringLength(60)]
        public string designatedBy { get; set; }

        /// <summary>
        /// The date the characteristic was removed.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }
    }
}