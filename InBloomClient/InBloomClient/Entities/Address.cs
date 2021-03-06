﻿/*
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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using InBloomClient.Enum;

namespace InBloomClient.Entities
{
    public class Address
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public AddressType addressType { get; set; }

        [Range(1, 20)]
        [StringLength(20)]
        public string apartmentRoomSuiteNumber { get; set; }//1-20

        [Range(1, 20)]
        [StringLength(20)]
        public string buildingSiteNumber { get; set; } //1-20

        [Range(2, 30)]
        [StringLength(30)]
        public string city { get; set; } //2-30

        [DataType(DataType.Date)]
        public DateTime closeDate { get; set; }

        [Range(1, 2)]
        [StringLength(2)]
        public string countryCode { get; set; }

        [Range(3, 5)]
        [StringLength(5)]
        public string countyFIPSCode { get; set; }

        [Range(1, 20)]
        [StringLength(20)]
        public string latitude { get; set; }

        [Range(1, 20)]
        [StringLength(20)]
        public string longitude { get; set; }

        [Range(1, 30)]
        [StringLength(30)]
        public string nameOfCountry { get; set; }

        [DataType(DataType.Date)]
        public DateTime openDate { get; set; }

        [Range(1, 17)]
        [StringLength(17)]
        public string postalCode { get; set; }

        [Range(1, 2)]
        [StringLength(2)]
        public string stateAbbreviation { get; set; }

        [Range(1, 150)]
        [StringLength(150)]
        public string streetNumberName { get; set; }
    }
}