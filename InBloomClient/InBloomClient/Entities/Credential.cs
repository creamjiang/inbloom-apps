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
    /// <summary>
    /// The legal document or authorization giving authorization to perform teaching assignment services.
    /// </summary>
    public class Credential
    {
        /// <summary>
        /// The month, day, and year on which an active credential held by an individual will expire.
        /// </summary>
        public DateTime credentialExpirationDate { get; set; }

        /// <summary>
        /// The field of certification for the certificate (e.g., Mathematics, Music)
        /// </summary>
        public CredentialFieldDescriptor credentialField { get; set; }

        /// <summary>
        /// The month, day, and year on which an active credential was issued to an individual.
        /// </summary>
        public DateTime credentialIssuanceDate { get; set; }

        /// <summary>
        /// An indication of the category of credential an individual holds.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public CredentialType credentialType { get; set; }

        /// <summary>
        /// The grade level(s) certified for teaching.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public LevelType level { get; set; }

        /// <summary>
        /// An indication of the pre-determined criteria for granting the teaching credential that an individual holds.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public TeachingCredentialBasisType teachingCredentialBasis { get; set; }

        /// <summary>
        /// An indication of the category of a legal document giving authorization to perform teaching assignment services.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public TeachingCredentialType teachingCredentialType { get; set; }
    }
}