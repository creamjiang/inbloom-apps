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
    public enum TeachingCredentialType
    {
        [EnumMember(Value = "Emergency")]
        Emergency,
        [EnumMember(Value = "Emergency Certified")]
        EmergencyCertified,
        [EnumMember(Value = "Emergency Non-Certified")]
        EmergencyNonCertified,
        [EnumMember(Value = "Emergency Teaching")]
        EmergencyTeaching,
        [EnumMember(Value = "Intern")]
        Intern,
        [EnumMember(Value = "Master")]
        Master,
        [EnumMember(Value = "Nonrenewable")]
        Nonrenewable,
        [EnumMember(Value = "One Year")]
        OneYear,
        [EnumMember(Value = "Other")]
        Other,
        [EnumMember(Value = "Paraprofessional")]
        Paraprofessional,
        [EnumMember(Value = "Professional")]
        Professional,
        [EnumMember(Value = "Probationary")]
        Probationary,
        [EnumMember(Value = "Provisional")]
        Provisional,
        [EnumMember(Value = "Regular")]
        Regular,
        [EnumMember(Value = "Retired")]
        Retired,
        [EnumMember(Value = "Specialist")]
        Specialist,
        [EnumMember(Value = "Substitute")]
        Substitute,
        [EnumMember(Value = "TeacherAssistant")]
        TeacherAssistant,
        [EnumMember(Value = "Temporary")]
        Temporary,
        [EnumMember(Value = "Special Assignment")]
        SpecialAssignment,
        [EnumMember(Value = "Standard")]
        Standard,
        [EnumMember(Value = "Standard Professional")]
        StandardProfessional,
        [EnumMember(Value = "Temporary Classroom")]
        TemporaryClassroom,
        [EnumMember(Value = "Temporary Exemption")]
        TemporaryExemption,
        [EnumMember(Value = "Unknown")]
        Unknown,
        [EnumMember(Value = "Unknown Permit")]
        UnknownPermit,
        [EnumMember(Value = "Vocational")]
        Vocational,
        [EnumMember(Value = "Standard Paraprofessional")]
        StandardParaprofessional,
        [EnumMember(Value = "Probationary Extension")]
        ProbationaryExtension,
        [EnumMember(Value = "Probationary Second Extension")]
        ProbationarySecondExtension,
        [EnumMember(Value = "Visiting International Teacher")]
        VisitingInternationalTeacher,
        [EnumMember(Value = "District Local")]
        DistrictLocal
    }
}