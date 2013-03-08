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
using System.ComponentModel;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace InBloomClient.Enum
{
    [DataContract]
    public enum AcademicSubjectType
    {
        [EnumMember(Value = "English Language and Literature")]
        EnglishLanguageAndLiterature,
        [EnumMember(Value = "English")]
        English,
        [EnumMember(Value = "ELA")]
        ELA,
        [EnumMember(Value = "Reading")]
        Reading,
        [EnumMember(Value = "Mathematics")]
        Mathematics,
        [EnumMember(Value = "Life and Physical Sciences")]
        LifeAndPhysicalSciences,
        [EnumMember(Value = "Social Sciences and History")]
        SocialSciencesAndHistory,
        [EnumMember(Value = "Social Studies")]
        SocialStudies,
        [EnumMember(Value = "Science")]
        Science,
        [EnumMember(Value = "Fine and Performing Arts")]
        FineAndPerformingArts,
        [EnumMember(Value = "Foreign Language and Literature")]
        ForeignLanguageAndLiterature,
        [EnumMember(Value = "Religious Education and Theology")]
        ReligiousEducationAndTheology,
        [EnumMember(Value = "Physical, Health, and Safety Education")]
        PhysicalHealthAndSafetyEducation,
        [EnumMember(Value = "Military Science")]
        MilitaryScience,
        [EnumMember(Value = "Computer and Information Sciences")]
        ComputerAndInformationSciences,
        [EnumMember(Value = "Communication and Audio/Visual Technology")]
        CommunicationAndAudioVisualTechnology,
        [EnumMember(Value = "Composite")]
        Composite,
        [EnumMember(Value = "Critical Reading")]
        CriticalReading,
        [EnumMember(Value = "Business and Marketing")]
        BusinessAndMarketing,
        [EnumMember(Value = "Manufacturing")]
        Manufacturing,
        [EnumMember(Value = "Health Care Sciences")]
        HealthCareSciences,
        [EnumMember(Value = "Public, Protective, and Government Service")]
        PublicProtectiveAndGovernmentService,
        [EnumMember(Value = "Hospitality and Tourism")]
        HospitalityAndTourism,
        [EnumMember(Value = "Architecture and Construction")]
        ArchitectureAndConstruction,
        [EnumMember(Value = "Agriculture, Food, and Natural Resources")]
        AgricultureFoodAndNaturalResources,
        [EnumMember(Value = "Human Services")]
        HumanServices,
        [EnumMember(Value = "Transportation, Distribution and Logistics")]
        TransportationDistributionAndLogistics,
        [EnumMember(Value = "Engineering and Technology")]
        EngineeringAndTechnology,
        [EnumMember(Value = "Writing")]
        Writing,
        [EnumMember(Value = "Miscellaneous")]
        Miscellaneous,
        [EnumMember(Value = "Other")]
        Other
    }
}