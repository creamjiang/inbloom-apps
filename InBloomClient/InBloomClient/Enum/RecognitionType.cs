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
    public enum RecognitionType
    {
        [EnumMember(Value = "Athletic awards")]
        AthleticAwards,
        [EnumMember(Value = "Awarding of units of value")]
        AwardingOfUnitsOfValue,
        [EnumMember(Value = "Citizenship award/recognition")]
        CitizenshipAwardRecognition,
        [EnumMember(Value = "Completion of requirement, but no units of value awarded")]
        CompletionOfRequirementButNoUnitsOfValueAwarded,
        [EnumMember(Value = "Certificate")]
        Certificate,
        [EnumMember(Value = "Honor award")]
        HonorAward,
        [EnumMember(Value = "Letter of commendation")]
        LetterOfCommendation,
        [EnumMember(Value = "Medals")]
        Medals,
        [EnumMember(Value = "Other")]
        MonogramLetter,
        [EnumMember(Value = "Other")]
        Points,
        [EnumMember(Value = "Other")]
        PromotionOrAdvancement,
        [EnumMember(Value = "Other")]
        Other
    }
}