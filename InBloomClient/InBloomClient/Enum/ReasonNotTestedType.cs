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
    public enum ReasonNotTestedType
    {
        [EnumMember(Value = "Absent")]
        Absent,
        [EnumMember(Value = "LEP exempt")]
        LEPExempt,
        [EnumMember(Value = "LEP postponement")]
        LEPPostponement,
        [EnumMember(Value = "Not appropriate (ARD decision)")]
        NotAppropriateARDDecision,
        [EnumMember(Value = "Not tested (ARD decision)")]
        NotTestedARDDecision,
        [EnumMember(Value = "Alternate assessment administered")]
        AlternateAssessmentAdministered,
        [EnumMember(Value = "Parental waiver")]
        ParentalWaiver,
        [EnumMember(Value = "Foreign exchange student waiver")]
        ForeignExchangeStudentWaiver,
        [EnumMember(Value = "Refusal by parent")]
        RefusalByParent,
        [EnumMember(Value = "Refusal by student")]
        RefusalByStudent,
        [EnumMember(Value = "Medical waiver")]
        MedicalWaiver,
        [EnumMember(Value = "Disruptive behavior")]
        DisruptiveBehavior,
        [EnumMember(Value = "Previously passed the examination")]
        PreviouslyPassedTheExamination,
        [EnumMember(Value = "Other")]
        Other
    }
}