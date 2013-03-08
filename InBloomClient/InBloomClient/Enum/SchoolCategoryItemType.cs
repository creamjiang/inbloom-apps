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
    public enum SchoolCategoryItemType
    {
        [EnumMember(Value = "Elementary Secondary School")]
        ElementarySecondarySchool,
        [EnumMember(Value = "Elementary School")]
        ElementarySchool,
        [EnumMember(Value = "High School")]
        HighSchool,
        [EnumMember(Value = "Middle School")]
        MiddleSchool,
        [EnumMember(Value = "Junior High School")]
        JuniorHighSchool,
        [EnumMember(Value = "SecondarySchool")]
        SecondarySchool,
        [EnumMember(Value = "Ungraded")]
        Ungraded,
        [EnumMember(Value = "Adult School")]
        AdultSchool,
        [EnumMember(Value = "Infant/toddler School")]
        InfantToddlerSchool,
        [EnumMember(Value = "Preschool/early childhood")]
        PreschoolEarlyChildhood,
        [EnumMember(Value = "Primary School")]
        PrimarySchool,
        [EnumMember(Value = "Intermediate School")]
        IntermediateSchool
    }
}