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
using System.Runtime.Serialization;

namespace InBloomClient.Enum
{
    [DataContract]
    public enum PopulationServedType
    {
        [EnumMember(Value = "Regular Students")]
        RegularStudents,
        [EnumMember(Value = "Bilingual Students")]
        BilingualStudents,
        [EnumMember(Value = "Compensatory/Remedial Education Students")]
        CompensatoryRemedialEducationStudents,
        [EnumMember(Value = "Gifted and Talented Students")]
        GiftedAndTalentedStudents,
        [EnumMember(Value = "Career and Technical Education Students")]
        CareerAndTechnicalEducationStudents,
        [EnumMember(Value = "Special Education Students")]
        SpecialEducationStudents,
        [EnumMember(Value = "ESL Students")]
        ESLStudents,
        [EnumMember(Value = "Adult Basic Education Students")]
        AdultBasicEducationStudents,
        [EnumMember(Value = "Honors Students")]
        HonorsStudents,
        [EnumMember(Value = "Migrant Students")]
        MigrantStudents
    }
}