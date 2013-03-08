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
    [DataContract]
    public enum DisabilityType
    {
        [EnumMember(Value = "Autistic/Autism")]
        [Description("Autistic/Autism")]
        AutisticAutism,
        [EnumMember(Value = "Deaf-Blindness")]
        [Description("Deaf-Blindness")]
        DeafBlindness,
        [EnumMember(Value = "Deafness")]
        [Description("Deafness")]
        Deafness,
        [EnumMember(Value = "Developmental Delay")]
        [Description("Developmental Delay")]
        DevelopmentalDelay,
        [EnumMember(Value = "Emotional Disturbance")]
        [Description("Emotional Disturbance")]
        EmotionalDisturbance,
        [EnumMember(Value = "Hearing/Auditory Impairment")]
        [Description("Hearing/Auditory Impairment")]
        HearingAuditoryImpairment,
        [EnumMember(Value = "Infants and Toddlers with Disabilities")]
        [Description("Infants and Toddlers with Disabilities")]
        InfantsAndToddlersWithDisabilities,
        [EnumMember(Value = "Mental Retardation")]
        [Description("Mental Retardation")]
        MentalRetardation,
        [EnumMember(Value = "Multiple Disabilities")]
        [Description("Multiple Disabilities")]
        MultipleDisabilities,
        [EnumMember(Value = "Orthopedic Impairment")]
        [Description("Orthopedic Impairment")]
        OrthopedicImpairment,
        [EnumMember(Value = "Other Health Impairment")]
        [Description("Other Health Impairment")]
        OtherHealthImpairment,
        [EnumMember(Value = "Speech or Language Impairment")]
        [Description("Speech or Language Impairment")]
        SpeechOrLanguageImpairment,
        [EnumMember(Value = "Specific Learning Disability")]
        [Description("Specific Learning Disability")]
        SpecificLearningDisability,
        [EnumMember(Value = "Traumatic Brain Delay")]
        [Description("Traumatic Brain Delay")]
        TraumaticBrainDelay,
        [EnumMember(Value = "Visual Impairment")]
        [Description("Visual Impairment")]
        VisualImpairment
    }
}