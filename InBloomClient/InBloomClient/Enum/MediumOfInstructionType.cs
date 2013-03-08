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
    public enum MediumOfInstructionType
    {
        [EnumMember(Value = "Televised")]
        Televised,
        [EnumMember(Value = "Telepresence/video conference")]
        TelepresenceVideoConference,
        [EnumMember(Value = "Videotaped/prerecorded video")]
        VideotapedPrerecordedVideo,
        [EnumMember(Value = "Other technology-based instruction")]
        OtherTechnologyBasedInstruction,
        [EnumMember(Value = "Technology-based instruction in classroom")]
        TechnologyBasedInstructionInClassroom,
        [EnumMember(Value = "Correspondence instruction")]
        CorrespondenceInstruction,
        [EnumMember(Value = "Face-to-face instruction")]
        FaceToFaceInstruction,
        [EnumMember(Value = "Virtual/On-line Distance learning")]
        VirtualOnlineDistanceLearning,
        [EnumMember(Value = "Center-based instruction")]
        CenterBasedInstruction,
        [EnumMember(Value = "Independent study")]
        IndependentStudy,
        [EnumMember(Value = "Internship")]
        Internship,
        [EnumMember(Value = "Other")]
        Other
    }
}