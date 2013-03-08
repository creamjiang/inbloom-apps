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
using System.ComponentModel;

namespace InBloomClient.Enum
{
    public enum AcademicHonorsType
    {
        [EnumMember(Value = "Honor roll")]
        [Description("Honor roll")]
        Honorroll,
        [EnumMember(Value = "Honor society")]
        [Description("Honor society")]
        HonorSociety,
        [EnumMember(Value = "Honorable mention")]
        [Description("Honorable mention")]
        HonorableMention,
        [EnumMember(Value = "Honors program")]
        [Description("Honors program")]
        HonorsProgram,
        [EnumMember(Value = "Prize awards")]
        [Description("Prize awards")]
        PrizeAwards,
        [EnumMember(Value = "Scholarships")]
        Scholarships,
        [EnumMember(Value = "Awarding of units of value")]
        AwardingOfUnitsOfValue,
        [EnumMember(Value = "Citizenship award/recognition")]
        CitizenshipAwardRecognition,
        [EnumMember(Value = "Completion of requirement, but no units of value awarded")]
        CompletionOfRequirementButNoUnitsOfValueAwarded,
        [EnumMember(Value = "Attendance award")]
        AttendanceAward,
        [EnumMember(Value = "Certificate")]
        Certificate,
        [EnumMember(Value = "Honor award")]
        HonorAward,
        [EnumMember(Value = "Letter of student commendation")]
        LetterOfStudentCommendation,
        [EnumMember(Value = "Medals")]
        Medals,
        [EnumMember(Value = "National Merit scholar")]
        NationalMeritScholar,
        [EnumMember(Value = "Points")]
        Points,
        [EnumMember(Value = "Promotion or advancement")]
        PromotionOrAdvancement,
        [EnumMember(Value = "Other")]
        Other
    }
}