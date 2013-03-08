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
    /// <summary>
    /// An indication of the pre-determined criteria for granting the teaching credential that an individual holds. 
    /// </summary>
    [DataContract]
    public enum TeachingCredentialBasisType
    {
        [EnumMember(Value = "4-year bachelor's degree")]
        FourYearBachelorsDegree,
        [EnumMember(Value = "5-year bachelor's degree")]
        FiveYearBachelorsDegree,
        [EnumMember(Value = "Master's degree")]
        MastersDegree,
        [EnumMember(Value = "Doctoral degree")]
        DoctoralDegree,
        [EnumMember(Value = "Met state testing requirement")]
        MetStateTestingRequirement,
        [EnumMember(Value = "Special/alternative program completion")]
        SpecialAlternativeProgramCompletion,
        [EnumMember(Value = "Relevant experience")]
        RelevantExperience,
        [EnumMember(Value = "Credentials based on reciprocation with another state")]
        CredentialsBasedOnReciprocationWithAnotherState
    }
}