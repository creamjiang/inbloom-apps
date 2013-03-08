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

namespace InBloomClient.Entities
{
    public class BirthData
    {
        [DataType(DataType.DateTime)]
        public DateTime birthDate { get; set; }

        [Range(2, 30)]
        [StringLength(30)]
        public string cityOfBirth { get; set; } //2-30

        [Range(1, 2)]
        [StringLength(2)]
        public string countryOfBirthCode { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime dateEnteredUS { get; set; }

        public bool multipleBirthStatus { get; set; }

        [StringLength(30)]
        public string stateOfBirthAbbreviation { get; set; }
    }
}