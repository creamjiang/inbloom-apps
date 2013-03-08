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
    public enum EducationalEnvironmentType
    {
        [EnumMember(Value = "Classroom")]
        Classroom,
        [EnumMember(Value = "Homebound")]
        Homebound,
        [EnumMember(Value = "Hospital class")]
        HospitalClass,
        [EnumMember(Value = "In-school suspension")]
        InSchoolSuspension,
        [EnumMember(Value = "Laboratory")]
        Laboratory,
        [EnumMember(Value = "Mainstream (Special Education)")]
        MainstreamSpecialEducation,
        [EnumMember(Value = "Off-school center")]
        OffSchoolCenter,
        [EnumMember(Value = "Pull-out class")]
        PullOutClass,
        [EnumMember(Value = "Resource room")]
        ResourceRoom,
        [EnumMember(Value = "Self-contained (Special Education)")]
        SelfContainedSpecialEducation,
        [EnumMember(Value = "Self-study")]
        SelfStudy,
        [EnumMember(Value = "Other")]
        Other
    }
}