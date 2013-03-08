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
    public enum ContentStandardType
    {
        [EnumMember(Value = "National Standard")]
        NationalStandard,
        [EnumMember(Value = "State Standard")]
        StateStandard,
        [EnumMember(Value = "College Entrance Standard")]
        CollegeEntranceStandard,
        [EnumMember(Value = "LEA Standard")]
        LEAStandard,
        [EnumMember(Value = "Texas Essential Knowledge and Skills")]
        TexasEssentialKnowledgeAndSkills,
        [EnumMember(Value = "SAT")]
        SAT,
        [EnumMember(Value = "PSAT")]
        PSAT,
        [EnumMember(Value = "ACT")]
        ACT,
        [EnumMember(Value = "Advanced Placement")]
        AdvancedPlacement,
        [EnumMember(Value = "International Baccalaureate")]
        InternationalBaccalaureate
    }
}